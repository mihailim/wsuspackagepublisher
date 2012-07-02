using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.UpdateServices.Administration;

namespace Wsus_Package_Publisher
{
    internal sealed class WsusWrapper
    {
        private AdminProxy proxy = new AdminProxy();
        private IUpdateServer wsus;
        private static WsusWrapper instance;

        private WsusWrapper() { }

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
        internal bool Connect(string serverName, bool useSSL, int serverPort, string preferredCulture)
        {
            try
            {
                wsus = AdminProxy.GetUpdateServer(serverName, useSSL, serverPort);
                //wsus = proxy.GetRemoteUpdateServerInstance(serverName, useSSL, serverPort);
                wsus.PreferredCulture = preferredCulture;
            }
            catch (WsusConnectionException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
            return true;
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
        /// <returns>An IUpdate representing an update.</returns>
        internal IEnumerable<IUpdate> GetAllUpdates()
        {
            foreach (IUpdate update in wsus.GetUpdates())
            {
                yield return update;
            }
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

            sdp.Title = informationsWizard.Title;
            sdp.Description = informationsWizard.Description;
            sdp.VendorName = informationsWizard.VendorName;
            sdp.ProductNames.Add(informationsWizard.ProductName);
            sdp.IsInstalled = isInstalledRulesWizard.GetXmlFormattedRule();
            sdp.IsInstallable = isInstallableRulesWizard.GetXmlFormattedRule();
            tmpFolderPath = Environment.GetEnvironmentVariable("Temp") + "\\Wsus Package Publisher\\";
            if (!System.IO.Directory.Exists(tmpFolderPath))
                System.IO.Directory.CreateDirectory(tmpFolderPath);
            if (System.IO.Directory.Exists(tmpFolderPath + sdp.PackageId))
                System.IO.Directory.Delete(tmpFolderPath + sdp.PackageId);
            System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId);
            System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId + "\\Xml\\");
            System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId + "\\Bin\\");

            System.IO.FileInfo updateFile = new System.IO.FileInfo(filesWizard.updateFileName);
            updateFile.CopyTo(tmpFolderPath + sdp.PackageId + "\\Bin\\" + updateFile.Name);

            sdp.Save(tmpFolderPath + sdp.PackageId + "\\Xml\\" + sdp.PackageId.ToString() + ".xml");
            IPublisher publisher = wsus.GetPublisher(tmpFolderPath + sdp.PackageId + "\\Xml\\" + sdp.PackageId.ToString() + ".xml");
            publisher.ProgressHandler += new EventHandler<PublishingEventArgs>(publisher_Progress);
            publisher.PublishPackage(tmpFolderPath + sdp.PackageId + "\\Bin\\", null);
            System.IO.Directory.Delete(tmpFolderPath + sdp.PackageId, true);
            if (UpdatePublished != null)
                UpdatePublished(GetUpdate(new UpdateRevisionId(sdp.PackageId)));
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

        internal void ApproveUpdate(IUpdate updateToApprove)
        {

        }

        internal void ExpireUpdate(IUpdate updateToExpire)
        {
            updateToExpire.ExpirePackage();
            if (UpdateExpired != null)
                UpdateExpired(GetUpdate(updateToExpire.Id));
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

        private void publisher_Progress(object sender, EventArgs e)
        {
            if (UpdatePublishingProgress != null)
                UpdatePublishingProgress(sender, e);
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
    }
}
