using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.UpdateServices.Administration;

namespace Wsus_Package_Publisher
{
    internal sealed class WsusWrapper
    {
        private IUpdateServer wsus;
        private static WsusWrapper instance;
        private WsusServer _wsusServer;
        private System.Windows.Forms.Timer _timer;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(WsusWrapper).Assembly);

        private WsusWrapper()
        {
            _timer = new System.Windows.Forms.Timer();
        }

        internal bool IsConnected {get; private set;}

        internal bool IsReplica { get; private set; }

        /// <summary>
        /// Allow to always get the same instance of this Class.
        /// </summary>
        /// <returns>An instance of WsusWrapper.</returns>
        internal static WsusWrapper GetInstance()
        {
            if (instance == null)
                instance = new WsusWrapper();
            return instance;
        }

        /// <summary>
        /// Connect to the Wsus server.
        /// </summary>
        /// <param name="serverName">Name of the server to connect to.</param>
        /// <param name="useSSL">indicate if https must be used or not.</param>
        /// <param name="serverPort">Communication port to use (80, 443, 8530, 8531)</param>
        /// <param name="preferredCulture">Culture to use for displaying computer group name.</param>
        /// <returns></returns>
        internal bool Connect(WsusServer serverToConnect, string preferredCulture)
        {
            try
            {
                if (serverToConnect.IsLocal)
                    wsus = AdminProxy.GetUpdateServer();
                else
                {
#if DEBUG
                    System.Windows.Forms.MessageBox.Show("Connecting to remote server : " + serverToConnect);
#endif
                    wsus = AdminProxy.GetUpdateServer(serverToConnect.Name, serverToConnect.UseSSL, serverToConnect.Port);
                    _timer.Interval = 10000;
                    _timer.Tick += new EventHandler(_timer_Tick);
                    _timer.Start();
                }
                wsus.PreferredCulture = preferredCulture;
                _wsusServer = serverToConnect;
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Windows.Forms.MessageBox.Show("Connection failed : \r\n" + ex.Message);
#endif
                return false;
            }
            IsConnected = true;
            IsReplica = wsus.GetConfiguration().IsReplicaServer;

            return true;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            GetAllComputerTargetGroup();
            _timer.Start();
        }

        internal WsusServer Server
        {
            get { return _wsusServer; }
        }

        /// <summary>
        /// Get all computers in a target group.
        /// </summary>
        /// <param name="computerGroup">The Guid of the group which contain computers.</param>
        /// <returns>A collection of target computers.</returns>
        internal ComputerTargetCollection GetComputerTargets(Guid computerGroup)
        {
            return wsus.GetComputerTargetGroup(computerGroup).GetComputerTargets(false);
        }

        /// <summary>
        /// Return an object representing the 'All Computer' group.
        /// </summary>
        /// <returns>Return a IComputerTargetGroup which represent the 'All Computer' group.</returns>
        internal IComputerTargetGroup GetAllComputerTargetGroup()
        {
            return wsus.GetComputerTargetGroup(ComputerTargetGroupId.AllComputers);
        }

        /// <summary>
        /// Allow to iterate each child computer Target Group of a Computer Target Group.
        /// </summary>
        /// <returns>A KeyValue pair which represent the Name and the Id of a child group.</returns>
        internal IEnumerable<KeyValuePair<string, Guid>> GetChildComputerTargetGroupNameAndId(Guid id)
        {
            foreach (IComputerTargetGroup group in wsus.GetComputerTargetGroup(id).GetChildTargetGroups())
            {
                yield return new KeyValuePair<string, Guid>(group.Name, group.Id);
            }
        }

        /// <summary>
        /// Get the name of the computer coresponding to this ID.
        /// </summary>
        /// <param name="computerId">Id of the computer.</param>
        /// <returns>Name of the computer which have this Id.</returns>
        internal string GetComputerName(string computerId)
        {
            return wsus.GetComputerTarget(computerId).FullDomainName;
        }

        /// <summary>
        /// Allow to iterate each udpdate in the server.
        /// </summary>
        /// <returns>Collection of updates.</returns>
        internal UpdateCollection GetAllUpdates()
        {
            return wsus.GetUpdates();
        }

        internal IUpdate GetUpdate(UpdateRevisionId updateId)
        {            
            return wsus.GetUpdate(updateId);
        }

        internal UpdateInstallationInfoCollection GetUpdateInstallationInfo(string computerName, System.DateTime from, System.DateTime to)
        {
            UpdateScope scope = new UpdateScope();
            scope.FromArrivalDate = from;
            scope.ToArrivalDate = to;
            scope.IncludedInstallationStates = UpdateInstallationStates.Installed;

            return wsus.GetComputerTargetByName(computerName).GetUpdateInstallationInfoPerUpdate(scope);
        }

        internal UpdateInstallationInfoCollection GetUpdateInstallationInfoPerComputerTarget(Guid groupId, IUpdate update)
        {
            return GetComputerGroup(groupId).GetUpdateInstallationInfoPerComputerTarget(update);
        }

        internal void PublishUpdate(FrmUpdateFilesWizard filesWizard, FrmUpdateInformationsWizard informationsWizard, FrmUpdateRulesWizard isInstalledRulesWizard, FrmUpdateRulesWizard isInstallableRulesWizard)
        {
            if (!_wsusServer.IsLocal)
                _timer.Stop();
            SoftwareDistributionPackage sdp = new SoftwareDistributionPackage();
            string tmpFolderPath;

            switch (filesWizard.FileType)
            {
                case FrmUpdateFilesWizard.UpdateType.WindowsInstaller:
                    sdp.PopulatePackageFromWindowsInstaller(filesWizard.updateFileName);
                    break;
                case FrmUpdateFilesWizard.UpdateType.WindowsInstallerPatch:
                    sdp.PopulatePackageFromWindowsInstallerPatch(filesWizard.updateFileName);
                    break;
                case FrmUpdateFilesWizard.UpdateType.Executable:
                    sdp.PopulatePackageFromExe(filesWizard.updateFileName);
                    break;
                default:
                    break;
            }

            sdp = InitializeSdp(sdp, informationsWizard, isInstalledRulesWizard, isInstallableRulesWizard);

            tmpFolderPath = GetTempFolder();

            if (!System.IO.Directory.Exists(tmpFolderPath + sdp.PackageId))
                System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId);
            if (!System.IO.Directory.Exists(tmpFolderPath + sdp.PackageId + "\\Xml\\"))
                System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId + "\\Xml\\");
            if (!System.IO.Directory.Exists(tmpFolderPath + sdp.PackageId + "\\Bin\\"))
                System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId + "\\Bin\\");

            System.IO.FileInfo updateFile = new System.IO.FileInfo(filesWizard.updateFileName);
            updateFile.CopyTo(tmpFolderPath + sdp.PackageId + "\\Bin\\" + updateFile.Name);

            sdp.Save(tmpFolderPath + sdp.PackageId + "\\Xml\\" + sdp.PackageId.ToString() + ".xml");
            IPublisher publisher = wsus.GetPublisher(tmpFolderPath + sdp.PackageId + "\\Xml\\" + sdp.PackageId.ToString() + ".xml");
            publisher.ProgressHandler += new EventHandler<PublishingEventArgs>(publisher_Progress);
            publisher.PublishPackage(tmpFolderPath + sdp.PackageId + "\\Bin\\", null);
            //System.IO.Directory.Delete(tmpFolderPath + sdp.PackageId, true);
            if (!_wsusServer.IsLocal)
                _timer.Start();
            if (UpdatePublished != null)
                UpdatePublished(GetUpdate(new UpdateRevisionId(sdp.PackageId)));
        }

        private SoftwareDistributionPackage InitializeSdp(SoftwareDistributionPackage sdp, FrmUpdateInformationsWizard informationsWizard, FrmUpdateRulesWizard isInstalledRulesWizard, FrmUpdateRulesWizard isInstallableRulesWizard)
        {
            sdp.Title = informationsWizard.Title;
            sdp.Description = informationsWizard.Description;
            sdp.VendorName = informationsWizard.VendorName;
            sdp.ProductNames.Clear();
            sdp.ProductNames.Add(informationsWizard.ProductName);
            sdp.IsInstalled = isInstalledRulesWizard.GetXmlFormattedRule();
            sdp.IsInstallable = isInstallableRulesWizard.GetXmlFormattedRule();
            sdp.InstallableItems[0].InstallBehavior.CanRequestUserInput = informationsWizard.CanRequestUserInput;
            sdp.InstallableItems[0].InstallBehavior.Impact = informationsWizard.Impact;
            sdp.InstallableItems[0].InstallBehavior.RebootBehavior = informationsWizard.Behavior;
            sdp.InstallableItems[0].InstallBehavior.RequiresNetworkConnectivity = informationsWizard.CanRequestNetworkConnectivity;

            if (!string.IsNullOrEmpty(informationsWizard.UrlMoreInfo))
            {
                sdp.AdditionalInformationUrls.Clear();
                sdp.AdditionalInformationUrls.Add(new Uri(informationsWizard.UrlMoreInfo));
            }

            if (!string.IsNullOrEmpty(informationsWizard.UrlSupport))
                sdp.SupportUrl = new Uri(informationsWizard.UrlSupport);

            sdp.PackageUpdateClassification = informationsWizard.UpdateClassification;

            if (!string.IsNullOrEmpty(informationsWizard.SecurityBulletinId))
                sdp.SecurityBulletinId = informationsWizard.SecurityBulletinId;

            sdp.SecurityRating = informationsWizard.Severity;

            if (!string.IsNullOrEmpty(informationsWizard.Cve))
            {
                sdp.CommonVulnerabilitiesIds.Clear();
                sdp.CommonVulnerabilitiesIds.Add(informationsWizard.Cve);
            }

            if (!string.IsNullOrEmpty(informationsWizard.KbArticle))
                sdp.KnowledgebaseArticleId = informationsWizard.KbArticle;

            return sdp;
        }

        internal void ReviseUpate(FrmUpdateInformationsWizard informationsWizard, FrmUpdateRulesWizard isInstalledRulesWizard, FrmUpdateRulesWizard isInstallableRulesWizard, SoftwareDistributionPackage sdp)
        {
            if (!_wsusServer.IsLocal)
                _timer.Stop();
            string tmpFolderPath;
            IUpdate oldUpdate = GetUpdate(new UpdateRevisionId(sdp.PackageId));

            sdp = InitializeSdp(sdp, informationsWizard, isInstalledRulesWizard, isInstallableRulesWizard);

            tmpFolderPath = GetTempFolder();
            if (!System.IO.Directory.Exists(tmpFolderPath + sdp.PackageId))
                System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId);
            if (!System.IO.Directory.Exists(tmpFolderPath + sdp.PackageId + "\\Xml\\"))
                System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId + "\\Xml\\");

            sdp.Save(tmpFolderPath + sdp.PackageId + "\\Xml\\" + sdp.PackageId.ToString() + ".xml");
            IPublisher publisher = wsus.GetPublisher(tmpFolderPath + sdp.PackageId + "\\Xml\\" + sdp.PackageId.ToString() + ".xml");
            publisher.ProgressHandler += new EventHandler<PublishingEventArgs>(publisher_Progress);
            publisher.RevisePackage();
            if (!_wsusServer.IsLocal)
                _timer.Start();
            if (UpdateRevised != null)
                UpdateRevised(oldUpdate, GetUpdate(new UpdateRevisionId(sdp.PackageId)));
        }

        internal string ResignPackage(IUpdate update)
        {
            string result = resMan.GetString("SuccessfullyResignPackage");

            try
            {
                string tmpFile = GetTempFolder() + update.Id.UpdateId.ToString() + ".xml";
                wsus.ExportPackageMetadata(update.Id, tmpFile);
                SoftwareDistributionPackage sdp = new SoftwareDistributionPackage(tmpFile);
                IPublisher publisher = wsus.GetPublisher(tmpFile);
                publisher.ResignPackage();
                System.IO.File.Delete(tmpFile);
            }
            catch (Exception ex)
            {
                result = resMan.GetString("ResignPackageFailed" + " : " + ex.Message);
            }

            return result;
        }

        private string GetTempFolder()
        {
            string tmpFolderPath = Environment.GetEnvironmentVariable("Temp") + "\\Wsus Package Publisher\\";
            if (!System.IO.Directory.Exists(tmpFolderPath))
                System.IO.Directory.CreateDirectory(tmpFolderPath);

            return tmpFolderPath;
        }

        /// <summary>
        /// Delete an Update.
        /// </summary>
        /// <param name="id">Update to delete.</param>
        internal void DeleteUpdate(IUpdate updateToDelete)
        {
            wsus.DeleteUpdate(updateToDelete.Id.UpdateId);
            if (UpdateDeleted != null)
                UpdateDeleted(updateToDelete);
        }

        /// <summary>
        /// Decline the update.
        /// </summary>
        /// <param name="updateToDecline">Update to Decline</param>
        internal void DeclineUpdate(IUpdate updateToDecline)
        {
            updateToDecline.Decline();
            if (UpdateDeclined != null)
                UpdateDeclined(GetUpdate(updateToDecline.Id));
        }

        internal void ApproveUpdateForInstallation(Guid computerGroupId, IUpdate updateToApprove, DateTime deadLine)
        {
            updateToApprove.Approve(UpdateApprovalAction.Install, GetComputerGroup(computerGroupId), deadLine);
        }

        internal void ApproveUpdateForInstallation(Guid computerGroupId, IUpdate updateToApprove)
        {
            updateToApprove.Approve(UpdateApprovalAction.Install, GetComputerGroup(computerGroupId));
        }

        internal void ApproveUpdateForUninstallation(Guid computerGroupId, IUpdate updateToApprove, DateTime deadLine)
        {
            updateToApprove.Approve(UpdateApprovalAction.Uninstall, GetComputerGroup(computerGroupId), deadLine);
        }

        internal void ApproveUpdateForUninstallation(Guid computerGroupId, IUpdate updateToApprove)
        {
            updateToApprove.Approve(UpdateApprovalAction.Uninstall, GetComputerGroup(computerGroupId));
        }

        internal void ApproveUpdateForOptionalInstallation(Guid computerGroupId, IUpdate updateToApprove)
        {
            updateToApprove.ApproveForOptionalInstall(GetComputerGroup(computerGroupId));
        }

        internal void DisapproveUpdate(Guid computerGroupId, IUpdate udpateToDisapprove)
        {
            udpateToDisapprove.Approve(UpdateApprovalAction.NotApproved, GetComputerGroup(computerGroupId));
        }

        internal void ExpireUpdate(IUpdate updateToExpire)
        {
            updateToExpire.ExpirePackage();
            UpdateRevisionId id = new UpdateRevisionId(updateToExpire.Id.UpdateId, ++updateToExpire.Id.RevisionNumber);
            if (UpdateExpired != null)
                UpdateExpired(GetUpdate(id));
        }

        internal UpdateApprovalCollection GetUpdateApprovalStatus(Guid groupId, IUpdate update)
        {
            return update.GetUpdateApprovals(GetComputerGroup(groupId));
        }

        internal SoftwareDistributionPackage GetMetaData(IUpdate update)
        {
            string tmpFile = GetTempFolder() + update.Id.UpdateId.ToString() + ".xml";
            wsus.ExportPackageMetadata(update.Id, tmpFile);
            SoftwareDistributionPackage sdp = new SoftwareDistributionPackage(tmpFile);
            string installedRule = sdp.IsInstalled;

            return sdp;
        }

        /// <summary>
        /// Return the IComputerTarget of the 'computerName'.
        /// </summary>
        /// <param name="computerName">Name of the computer you want to obtain the IComputerTarget object.</param>
        /// <returns>IComputerTarget of the computer.</returns>
        internal IComputerTarget GetComputerTargetByName(string computerName)
        {
            return wsus.GetComputerTargetByName(computerName);
        }

        /// <summary>
        /// Get a group of target computers.
        /// </summary>
        /// <param name="groupId">Id of the group you want.</param>
        /// <returns>A IComputerTargetGroup coresponding of the Id.</returns>
        internal IComputerTargetGroup GetComputerGroup(Guid groupId)
        {
            return wsus.GetComputerTargetGroup(groupId);
        }

        internal void GenerateSelfSignedCertificate()
        {
            Microsoft.UpdateServices.Administration.IUpdateServerConfiguration wsusConfiguration = wsus.GetConfiguration();

            wsusConfiguration.SetSigningCertificate();
            wsusConfiguration.Save();
        }

        internal void UseExistingCertificate(string filePath, string password)
        {
            Microsoft.UpdateServices.Administration.IUpdateServerConfiguration wsusConfiguration = wsus.GetConfiguration();

            wsusConfiguration.SetSigningCertificate(filePath, password);
            wsusConfiguration.Save();
        }

        internal void SaveCertificate(string filePath)
        {
            Microsoft.UpdateServices.Administration.IUpdateServerConfiguration wsusConfiguration = wsus.GetConfiguration();

            wsusConfiguration.GetSigningCertificate(filePath);
        }

        private void publisher_Progress(object sender, EventArgs e)
        {
            if (UpdatePublishingProgress != null)
                UpdatePublishingProgress(sender, e);
        }



        ~WsusWrapper()
        {
            _timer.Stop();
            _timer = null;
        }

        public delegate void UpdateDeclinedEventHandler(IUpdate declinedUpdate);
        public event UpdateDeclinedEventHandler UpdateDeclined;

        public delegate void UpdateExpiredEventHandler(IUpdate expiredUpdate);
        public event UpdateExpiredEventHandler UpdateExpired;

        public delegate void UpdateDeletedEventHandler(IUpdate deletedUpdate);
        public event UpdateDeletedEventHandler UpdateDeleted;

        public delegate void UpdatePublishingProgressEventHandler(object sender, EventArgs e);
        public event UpdatePublishingProgressEventHandler UpdatePublishingProgress;

        public delegate void UpdatePublisedEventHandler(IUpdate PublishedUpdate);
        public event UpdatePublisedEventHandler UpdatePublished;

        public delegate void UpdateRevisedEventHandler(IUpdate oldUpdate, IUpdate RevisedUpdate);
        public event UpdateRevisedEventHandler UpdateRevised;
    }
}
