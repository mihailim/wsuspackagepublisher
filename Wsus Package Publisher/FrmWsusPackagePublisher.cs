using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.UpdateServices.Administration;

namespace Wsus_Package_Publisher
{
    public partial class FrmWsusPackagePublisher : Form
    {
        private WsusWrapper wsus = WsusWrapper.GetInstance();
        private ComputerGroup computerGroups;
        private Dictionary<string, Guid> computerGroupGuidConverter = new Dictionary<string, Guid>();
        private Dictionary<string, Company> companies = new Dictionary<string, Company>();
        private UpdateControl updateCtrl;
        private ComputerControl computerCtrl = null;
        private TreeNode serverNode;
        private TreeNode allComputersNode;
        private TreeNode allUpdatesNode;
        private TreeNode allMetaGroupsNode;
        private FrmWaiting _waitingForm;
        private System.Threading.Thread _waitingThread;
        private List<WsusServer> serverList = new List<WsusServer>();
        private WsusServer currentWsusServer = null;
        private FrmSettings settings = new FrmSettings();
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmWsusPackagePublisher).Assembly);

        public FrmWsusPackagePublisher()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);

            InitializeComponent();
            if (Properties.Settings.Default.Language == "fr")
            {
                frenchtoolStripMenuItem.Checked = true;
                englishtoolStripMenuItem.Checked = false;
            }
            else
            {
                frenchtoolStripMenuItem.Checked = false;
                englishtoolStripMenuItem.Checked = true;
            }
            updateCtrl = new UpdateControl();
            ClearBeforeConnecting();
            wsus.UpdatePublished += new WsusWrapper.UpdatePublisedEventHandler(UpdatePublished);
            wsus.UpdateRevised += new WsusWrapper.UpdateRevisedEventHandler(UpdateRevised);
            wsus.UpdateSuperseded += new WsusWrapper.UpdateSupersededEventHandler(wsus_UpdateSuperseded);
            imgLstServer.Images.Add(Properties.Resources.UpStream);
            imgLstServer.Images.Add(Properties.Resources.DownStream);
        }

        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbout about = new FrmAbout();
            about.ShowDialog();
        }

        private void btnConnectToServer_Click(object sender, EventArgs e)
        {
            IComputerTargetGroup allComputerTargetGroup;
            btnConnectToServer.Enabled = false;
            ClearBeforeConnecting();
            WsusServer serverWsus = (WsusServer)cmbBxServerList.SelectedItem;
            StartWaitingForm(resMan.GetString("connecting"));
            if (wsus.Connect(serverWsus, Properties.Settings.Default.Language))
            {
                string rootComputerGroupName;
                Guid rootComputerGroupId;
                currentWsusServer = serverWsus;

                if (wsus.IsReplica)
                    serverNode = new TreeNode(serverWsus.Name + " (" + resMan.GetString("ReplicaServer") + ")");
                else
                    serverNode = new TreeNode(serverWsus.Name);
                serverNode.Tag = "Server";
                if (wsus.StreamType == WsusWrapper.StreamTypeServer.UpStream)
                    serverNode.StateImageIndex = 0;
                else
                    serverNode.StateImageIndex = 1;
                trvWsus.Nodes.Add(serverNode);
                allComputerTargetGroup = wsus.GetAllComputerTargetGroup();
                rootComputerGroupName = allComputerTargetGroup.Name;
                rootComputerGroupId = allComputerTargetGroup.Id;
                computerGroupGuidConverter.Add(rootComputerGroupName, rootComputerGroupId);

                allComputersNode = new TreeNode(rootComputerGroupName);
                allComputersNode.Tag = "ComputerGroup";
                serverNode.Nodes.Add(allComputersNode);

                PopulateTreeviewWithComputerGroups(allComputerTargetGroup, allComputersNode);
                trvWsus.Sort();
                computerGroups = new ComputerGroup(rootComputerGroupName, rootComputerGroupId, rootComputerGroupName);
                PopulateComputerGroups(allComputersNode, computerGroups, 3);

                if (!wsus.IsReplica)
                {
                    FillMetaGroups();
                    allMetaGroupsNode = new TreeNode(resMan.GetString("MetaGroup"));
                    allMetaGroupsNode.Tag = "MetaGroupRoot";
                    PopulateMetaGroupNode();
                    serverNode.Nodes.Add(allMetaGroupsNode);
                }

                allUpdatesNode = new TreeNode(resMan.GetString("updates"));
                allUpdatesNode.Tag = "Updates";
                serverNode.Nodes.Add(allUpdatesNode);
                CollectUpdates();
                serverNode.Expand();
                allUpdatesNode.Expand();
                updateCtrl.SetComputerGroups(computerGroups, allComputersNode);
                updateCtrl.SetMetaGroups(currentWsusServer.MetaGroups);
                if (wsus.IsReplica)
                {
                    createUpdatetoolStripMenuItem.Enabled = false;
                    certificatetoolStripMenuItem.Enabled = false;
                    updateCtrl.LockFunctionnalities(true);
                }
                else
                {
                    createUpdatetoolStripMenuItem.Enabled = true;
                    certificatetoolStripMenuItem.Enabled = true;
                    updateCtrl.LockFunctionnalities(false);
                    trvWsus.AllowDrop = true;
                }
            }
            else
            {
                btnConnectToServer.Enabled = true;
                StopWaitingForm();
                MessageBox.Show(this, resMan.GetString("FailToConnectToServer"));
                return;
            }
            StopWaitingForm();
        }

        private void PopulateMetaGroupNode()
        {
            allMetaGroupsNode.Nodes.Clear();
            foreach (MetaGroup metaGroupToAdd in currentWsusServer.MetaGroups)
            {
                TreeNode nodeToAdd = new TreeNode(metaGroupToAdd.Name);
                nodeToAdd.Tag = "MetaGroup";
                allMetaGroupsNode.Nodes.Add(nodeToAdd);
            }
        }

        private void ClearBeforeConnecting()
        {
            trvWsus.CollapseAll();
            trvWsus.AllowDrop = false;
            computerGroups = null;
            computerGroupGuidConverter.Clear();
            companies.Clear();
            splitContainer1.Panel2.Controls.Clear();
            computerCtrl = null;
            updateCtrl.ClearDisplay();
            updateCtrl.Dock = DockStyle.Fill;
            computerCtrl = new ComputerControl();
            computerCtrl.Dock = DockStyle.Fill;
            serverNode = null;
            allComputersNode = null;
            allUpdatesNode = null;
            allMetaGroupsNode = null;
            trvWsus.Nodes.Clear();
            currentWsusServer = null;
        }

        private void StartWaitingForm(string description)
        {
            _waitingForm = new FrmWaiting();
            _waitingForm.Description = description;
            _waitingForm.GoOn = true;
            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.BelowNormal;
            _waitingThread = new System.Threading.Thread(new System.Threading.ThreadStart(_waitingForm.ShowForm));
            _waitingThread.Priority = System.Threading.ThreadPriority.AboveNormal;
            _waitingThread.Start();
            this.Refresh();
            System.Threading.Thread.Sleep(200);
        }

        private void StopWaitingForm()
        {
            _waitingForm.GoOn = false;
            _waitingThread.Join(900);
            _waitingThread = null;
            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Normal;
            this.Focus();
        }

        private void CollectUpdates()
        {
            string companyName;
            string productName;

            foreach (IUpdate update in wsus.GetAllUpdates())
            {
                if (update.CompanyTitles != null && update.CompanyTitles.Count != 0 && update.ProductTitles != null && update.ProductTitles.Count != 0)
                {
                    companyName = update.CompanyTitles[0];
                    productName = update.ProductTitles[0];

                    if (!companies.ContainsKey(companyName))
                        CreateNewCompany(companyName);

                    if (!companies[companyName].Products.ContainsKey(productName))
                        CreateNewProduct(companyName, productName);
                    companies[companyName].Products[productName].AddUpdate(update);
                }
            }
        }

        private void CreateNewCompany(string companyName)
        {
            Company newCompanyInstance = new Company(companyName);

            companies.Add(companyName, newCompanyInstance);
            TreeNode companyNode = new TreeNode(newCompanyInstance.CompanyName);
            companyNode.Tag = "Company";
            companyNode.Name = newCompanyInstance.CompanyName;
            allUpdatesNode.Nodes.Add(companyNode);
            trvWsus.Sort();
            trvWsus.Refresh();
            updateCtrl.Companies = companies;

            newCompanyInstance.NoMoreProductsForThisCompany += new Company.NoMoreProductsForThisCompanyEventHandler(CompanyRunOutofProducts);
            newCompanyInstance.ProductRemoved += new Company.ProductRemovedEventHandler(Company_ProductRemoved);
            newCompanyInstance.ProductAdded += new Company.ProductAddedEventHandler(Company_ProductAdded);
            newCompanyInstance.ProductRefreshed += new Company.ProductRefreshedEventHandler(Company_ProductRefreshed);
        }

        private void CreateNewProduct(string companyName, string productName)
        {
            Company vendor = companies[companyName];
            vendor.AddProduct(productName);
        }

        private void Company_ProductRefreshed(Company company, Product refreshedProduct)
        {
            updateCtrl.RefreshDisplay();
        }

        private void Company_ProductAdded(Company company, Product productAdded)
        {
            TreeNode companyNode = allUpdatesNode.Nodes.Find(company.CompanyName, false)[0];

            TreeNode productNode = new TreeNode(productAdded.ProductName);
            productNode.Tag = "Product";
            productNode.Name = productAdded.ProductName;
            companyNode.Nodes.Add(productNode);
            trvWsus.Sort();
            productAdded.UpdateAdded += new Product.UpdateAddedEventHandler(product_UpdateAdded);
        }

        private void Company_ProductRemoved(Company company, Product productRemoved)
        {
            TreeNode companyNode = allUpdatesNode.Nodes.Find(company.CompanyName, false)[0];
            TreeNode productNode = companyNode.Nodes.Find(productRemoved.ProductName, false)[0];

            companyNode.Nodes.Remove(productNode);
            splitContainer1.Panel2.Controls.Clear();
        }

        private void CompanyRunOutofProducts(Company companyWithoutProducts)
        {
            companies.Remove(companyWithoutProducts.CompanyName);
            trvWsus.SuspendLayout();
            allUpdatesNode.Nodes.RemoveByKey(companyWithoutProducts.CompanyName);
            trvWsus.ResumeLayout();
        }

        private void product_UpdateAdded(Product updatedProduct, IUpdate addedUpdate)
        {
            if (updateCtrl.Product == updatedProduct)
                updateCtrl.RefreshDisplay();
        }

        private void PopulateTreeviewWithComputerGroups(IComputerTargetGroup group, TreeNode node)
        {
            foreach (IComputerTargetGroup childGroup in wsus.GetChildComputerTargetGroupNameAndId(group.Id))
            {
                TreeNode newNode = new TreeNode(childGroup.Name);
                computerGroupGuidConverter.Add(childGroup.Name, childGroup.Id);
                newNode.Tag = "ComputerGroup";
                node.Nodes.Add(newNode);
                PopulateTreeviewWithComputerGroups(childGroup, newNode);
            }
        }

        private void PopulateComputerGroups(TreeNode node, ComputerGroup computersGroup, int indent)
        {
            foreach (TreeNode childNode in node.Nodes)
            {
                string tab = new string(' ', indent);
                ComputerGroup newComputerGroup = new ComputerGroup(childNode.Text, computerGroupGuidConverter[childNode.Text], tab + childNode.Text);

                computersGroup.InnerComputerGroup.Add(newComputerGroup);
                PopulateComputerGroups(childNode, newComputerGroup, indent + 3);
            }
        }

        private void createUpdatetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUpdateWizard updateWizard;

            if (trvWsus.SelectedNode != null)
            {
                if (trvWsus.SelectedNode.Tag.ToString() == "Product")
                    updateWizard = new FrmUpdateWizard(companies, updateCtrl.Product.Vendor, updateCtrl.Product);
                else
                    if (trvWsus.SelectedNode.Tag.ToString() == "Company")
                        updateWizard = new FrmUpdateWizard(companies, companies[trvWsus.SelectedNode.Text]);
                    else
                        updateWizard = new FrmUpdateWizard(companies);
            }
            else
                updateWizard = new FrmUpdateWizard(companies);
            updateWizard.ShowDialog(this);
        }

        private void trvWsus_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (e.Node.Tag != null)
            {
                switch (e.Node.Tag.ToString())
                {
                    case "ComputerGroup":
                        if ((splitContainer1.Panel2.Controls.Count == 1) && (splitContainer1.Panel2.Controls[0].GetType() == typeof(ComputerControl)))
                        {
                            computerCtrl.Display(GetTargetGroupId(e.Node.Text));
                        }
                        else
                        {
                            splitContainer1.Panel2.Controls.Clear();
                            splitContainer1.Panel2.Controls.Add(computerCtrl);
                            computerCtrl.Display(GetTargetGroupId(e.Node.Text));
                        }
                        InitializeContextMenuForComputerGroup();
                        break;
                    case "Company":
                    case "Updates":
                        //Company company = companies[e.Node.Text];
                        InitializeContextMenuForCompany();
                        break;
                    case "Product":
                        if (companies.ContainsKey(e.Node.Parent.Text))
                        {
                            Company company = companies[e.Node.Parent.Text];
                            if (company.Products.ContainsKey(e.Node.Text))
                            {
                                if (splitContainer1.Panel2.Controls.Count == 0 || (splitContainer1.Panel2.Controls[0].GetType() != typeof(UpdateControl)))
                                {
                                    splitContainer1.SuspendLayout();
                                    splitContainer1.Panel2.Controls.Clear();
                                    splitContainer1.Panel2.Controls.Add(updateCtrl);
                                    splitContainer1.ResumeLayout();
                                }
                                updateCtrl.Product = company.Products[e.Node.Text];
                            }
                        }
                        InitializeContextMenuForProduct();
                        break;
                    case "MetaGroup":
                        InitializeContextMenuForMetaGroup(false);
                        break;
                    case "MetaGroupRoot":
                        InitializeContextMenuForMetaGroup(true);
                        break;
                    case "Server":
                        InitializeContextMenuForServer();
                        break;
                    default:
                        BlankContextMenu();
                        break;
                }
            }

            this.Cursor = Cursors.Default;
        }

        private void BlankContextMenu()
        {
            ctxMnuTreeview.Items.Clear();
            trvWsus.ContextMenu = null;
        }

        private void InitializeContextMenuForComputerGroup()
        {
            ctxMnuTreeview.Items.Clear();
            ctxMnuTreeview.Items.Add(GetItem("SendDetectNow"));
            ctxMnuTreeview.Items.Add(GetItem("SendReportNow"));
            trvWsus.ContextMenuStrip = ctxMnuTreeview;
        }

        private void InitializeContextMenuForCompany()
        {
            ctxMnuTreeview.Items.Clear();
            if (!wsus.IsReplica)
                ctxMnuTreeview.Items.Add(GetItem("CreateUpdate"));
            trvWsus.ContextMenuStrip = ctxMnuTreeview;
        }

        private void InitializeContextMenuForProduct()
        {
            ctxMnuTreeview.Items.Clear();
            if (!wsus.IsReplica)
                ctxMnuTreeview.Items.Add(GetItem("CreateUpdate"));
            trvWsus.ContextMenuStrip = ctxMnuTreeview;
        }

        private void InitializeContextMenuForMetaGroup(bool isRoot)
        {
            ctxMnuTreeview.Items.Clear();
            if (!wsus.IsReplica)
            {
                if (isRoot)
                    ctxMnuTreeview.Items.Add(GetItem("ManageMetaGroups"));
                else
                {
                    ctxMnuTreeview.Items.Add(GetItem("ManageThisMetaGroup"));
                    ctxMnuTreeview.Items.Add(GetItem("DeleteThisMetaGroup"));
                }
            }
            trvWsus.ContextMenuStrip = ctxMnuTreeview;
        }

        private void InitializeContextMenuForServer()
        {
            ctxMnuTreeview.Items.Clear();
            ctxMnuTreeview.Items.Add(GetItem("CleanUpdateServicesPackagesFolder"));

            trvWsus.ContextMenuStrip = ctxMnuTreeview;
        }

        private ToolStripMenuItem GetItem(string text)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(resMan.GetString(text));
            item.Tag = text;
            return item;
        }

        internal ComputerGroup ComputerGroups
        {
            get { return computerGroups; }
        }

        /// <summary>
        /// Return the Guid which have for name 'targetGroupName'.
        /// </summary>
        /// <param name="targetGroupName">The name of the group</param>
        /// <returns>Return the Guid corresponding to the group.</returns>
        internal Guid GetTargetGroupId(string targetGroupName)
        {
            return computerGroupGuidConverter[targetGroupName];
        }

        private void françaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frenchtoolStripMenuItem.Checked = true;
            englishtoolStripMenuItem.Checked = false;
            Properties.Settings.Default.Language = "fr";
            Properties.Settings.Default.Save();

            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr");
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            englishtoolStripMenuItem.Checked = true;
            frenchtoolStripMenuItem.Checked = false;
            Properties.Settings.Default.Language = "en";
            Properties.Settings.Default.Save();

            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
        }

        private void paramètresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.ShowDialog();
            FillServerList();
        }

        private void FillServerList()
        {
            serverList = settings.LoadServerSettings();

            cmbBxServerList.Items.Clear();
            foreach (WsusServer server in serverList)
                cmbBxServerList.Items.Add(server);

            if (cmbBxServerList.Items.Count != 0)
                cmbBxServerList.SelectedIndex = 0;
        }

        private void FillMetaGroups()
        {
            foreach (WsusServer wsusServer in serverList)
            {
                Dictionary<string, MetaGroup> metaGroups = new Dictionary<string, MetaGroup>();
                InitializeMetaGroups(wsusServer, metaGroups);
                InitializeComputerGroups(wsusServer, metaGroups);
                for (int i = 0; i < wsusServer.MetaGroups.Count; i++)
                {
                    wsusServer.MetaGroups[i] = metaGroups[wsusServer.MetaGroups[i].Name];
                }
            }
        }

        private void InitializeComputerGroups(WsusServer wsusServer, Dictionary<string, MetaGroup> metaGroups)
        {
            foreach (MetaGroup metaGroup in wsusServer.MetaGroups)
                if (metaGroup.InnerComputerGroups.Count != 0)
                    foreach (ComputerGroup computerGroup in metaGroup.InnerComputerGroups)
                    {
                        metaGroups[metaGroup.Name].InnerComputerGroups.Add(GetComputerGroupByName(computerGroups, computerGroup.Name));
                    }
        }

        private ComputerGroup GetComputerGroupByName(ComputerGroup groupToSearch, string nameToFind)
        {
            if (groupToSearch.Name == nameToFind)
                return groupToSearch;
            else
                foreach (ComputerGroup group in groupToSearch.InnerComputerGroup)
                {
                    ComputerGroup candidate = GetComputerGroupByName(group, nameToFind);
                    if (candidate != null)
                        return candidate;
                }
            return null;
        }

        private void InitializeMetaGroups(WsusServer wsusServer, Dictionary<string, MetaGroup> metaGroups)
        {
            // Create all root MetaGroups.
            foreach (MetaGroup metaGroup in wsusServer.MetaGroups)
                if (!metaGroups.ContainsKey(metaGroup.Name))
                    metaGroups.Add(metaGroup.Name, new MetaGroup(metaGroup.Name));

            // Populate InnerMetaGroup for each root MetaGroup.
            foreach (MetaGroup metaGroup in wsusServer.MetaGroups)
            {
                if (metaGroup.InnerMetaGroups.Count != 0)
                    foreach (MetaGroup innerMetaGroup in metaGroup.InnerMetaGroups)
                    {
                        metaGroups[metaGroup.Name].InnerMetaGroups.Add(metaGroups[innerMetaGroup.Name]);
                    }
            }
        }

        private void CleanUpdateServicesPackagesFolder()
        {
            List<string> presentId = new List<string>();
            List<System.IO.DirectoryInfo> obsoleteFolders = new List<System.IO.DirectoryInfo>();
            string serverName = currentWsusServer.Name;

            foreach (KeyValuePair<string, Company> pair in companies)
            {
                Company vendor = pair.Value;
                foreach (KeyValuePair<string, Product> productPair in vendor.Products)
                {
                    Product product = productPair.Value;
                    foreach (IUpdate update in product.Updates)
                        presentId.Add(update.Id.UpdateId.ToString().ToLower());
                }
            }
            System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(@"\\" + serverName + @"\UpdateServicesPackages");
            System.IO.DirectoryInfo[] directories = dirInfo.GetDirectories();
            foreach (System.IO.DirectoryInfo directory in directories)
            {
                if (!presentId.Contains(directory.Name.ToLower()))
                    obsoleteFolders.Add(directory);
            }

            FrmDeleteObsoleteFolders frmDeleteFolders = new FrmDeleteObsoleteFolders(obsoleteFolders);
            frmDeleteFolders.ShowDialog();
        }

        private void UpdatePublished(IUpdate publishedUpdate)
        {
            string companyName = publishedUpdate.CompanyTitles[0];
            string productName = publishedUpdate.ProductTitles[0];

            if (!companies.ContainsKey(companyName))
                CreateNewCompany(companyName);
            Company vendor = companies[companyName];

            if (!vendor.Products.ContainsKey(productName))
                vendor.AddProduct(productName);

            Product product = vendor.Products[productName];
            product.AddUpdate(publishedUpdate);
        }

        private void UpdateRevised(IUpdate oldUpdate, IUpdate revisedUpdate)
        {
            string oldCompanyName = oldUpdate.CompanyTitles[0];
            string oldProductName = oldUpdate.ProductTitles[0];
            string newCompanyName = revisedUpdate.CompanyTitles[0];
            string newProductName = revisedUpdate.ProductTitles[0];

            if (!companies.ContainsKey(newCompanyName))
                CreateNewCompany(newCompanyName);
            Company vendor = companies[newCompanyName];

            if (!vendor.Products.ContainsKey(newProductName))
            {
                vendor.AddProduct(newProductName);
            }
            Product product = vendor.Products[newProductName];

            if ((oldCompanyName != newCompanyName) || (oldProductName != newProductName))
            {
                companies[oldCompanyName].Products[oldProductName].RemoveUpdate(oldUpdate);
                companies[newCompanyName].Products[newProductName].AddUpdate(revisedUpdate);
            }

            product.RefreshUpdate(revisedUpdate);
        }

        private void wsus_UpdateSuperseded(IList<Guid> SupersededUpdates)
        {
            Product productsToRefresh;
            IUpdate supersededUpdate = wsus.GetUpdate(new UpdateRevisionId(SupersededUpdates[0]));

            foreach (KeyValuePair<string, Company> pair in companies)
            {
                foreach (KeyValuePair<string, Product> productPair in pair.Value.Products)
                {
                    foreach (IUpdate update in productPair.Value.Updates)
                    {
                        if (update.Id.UpdateId == supersededUpdate.Id.UpdateId)
                        {
                            productsToRefresh = productPair.Value;
                            foreach (Guid id in SupersededUpdates)
                            {
                                supersededUpdate = wsus.GetUpdate(new UpdateRevisionId(id));
                                productsToRefresh.RefreshUpdate(supersededUpdate);
                            }
                            return;
                        }
                    }
                }
            }
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmWsusPackagePublisher_Shown(object sender, EventArgs e)
        {
            FillServerList();
        }

        private void cmbBxServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBxServerList.SelectedItem != null && cmbBxServerList.SelectedItem.GetType() == typeof(WsusServer) && (cmbBxServerList.SelectedItem as WsusServer) != wsus.Server)
                btnConnectToServer.Enabled = true;
            else
                btnConnectToServer.Enabled = false;
        }

        private void certificatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wsus.IsConnected)
            {
                FrmCertificateManagement certificateMgmt = new FrmCertificateManagement();

                certificateMgmt.StartPosition = FormStartPosition.CenterParent;
                certificateMgmt.ShowDialog(this);
            }
            else
            {
                MessageBox.Show(resMan.GetString("ConnectToWsusFirst"));
                certificatetoolStripMenuItem.Enabled = false;
            }
        }

        private void mSIPropertyReaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMSIPropertyReader frmMSIReader = new FrmMSIPropertyReader();
            frmMSIReader.ShowDialog(this);
        }

        private void trvWsus_DragEnter(object sender, DragEventArgs e)
        {
            DragDropEffects effect = DragDropEffects.None;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                Array tab = (Array)e.Data.GetData(DataFormats.FileDrop);
                if (tab != null && tab.GetValue(0) != null && tab.Length == 1)
                {
                    string fileName = tab.GetValue(0).ToString();
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
                    string ext = fileInfo.Extension.ToLower();
                    if (ext == ".msi" || ext == ".msp" || ext == ".exe")
                        effect = DragDropEffects.Copy;
                }
            }
            e.Effect = effect;
        }

        private void trvWsus_DragDrop(object sender, DragEventArgs e)
        {
            Product selectedProduct = null;
            Company selectedCompany = null;
            FrmUpdateWizard updateWizard = new FrmUpdateWizard(companies);

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
                Point clientLocation = trvWsus.PointToClient(new Point(e.X, e.Y));
                TreeNode node = trvWsus.GetNodeAt(clientLocation);
                if (node != null && !string.IsNullOrEmpty(node.Text) && node.Tag != null && !string.IsNullOrEmpty(node.Tag.ToString()))
                {
                    if (node.Tag.ToString().ToLower() == "product")
                    {
                        selectedProduct = GetProduct(node.Text);
                        selectedCompany = selectedProduct.Vendor;
                    }
                    if (node.Tag.ToString().ToLower() == "company")
                        selectedCompany = GetCompany(node.Text);
                }
                if (fileNames != null && fileNames.GetValue(0) != null)
                {
                    string fileName = fileNames[0];
                    if (selectedProduct != null && selectedCompany != null)
                        updateWizard = new FrmUpdateWizard(companies, selectedCompany, selectedProduct, fileName);
                    else
                        if (selectedCompany != null)
                            updateWizard = new FrmUpdateWizard(companies, selectedCompany, fileName);
                        else
                            updateWizard = new FrmUpdateWizard(companies, fileName);

                    updateWizard.StartPosition = FormStartPosition.CenterScreen;
                    updateWizard.Show(this);
                }
            }
        }

        /// <summary>
        /// Search the Company which have for name 'companyToFind'.
        /// </summary>
        /// <param name="companyToFind">The name of the company to find.</param>
        /// <returns>Return the Company if found or else return Null.</returns>
        private Company GetCompany(string companyToFind)
        {
            Company foundCompany = null;
            if (companies.ContainsKey(companyToFind))
                foundCompany = companies[companyToFind];

            return foundCompany;
        }

        /// <summary>
        /// Search the Product which have for name productToFind.
        /// </summary>
        /// <param name="productToFind">Name of the Product to find.</param>
        /// <returns>Return the Product if found. Return Null in other case.</returns>
        private Product GetProduct(string productToFind)
        {
            Product foundProduct = null;

            foreach (KeyValuePair<string, Company> pair in companies)
            {
                if (pair.Value.Products.ContainsKey(productToFind))
                {
                    foundProduct = pair.Value.Products[productToFind];
                    break;
                }
            }

            return foundProduct;
        }

        private void FrmWsusPackagePublisher_FormClosing(object sender, FormClosingEventArgs e)
        {
            updateCtrl.Dispose();
            computerCtrl.Dispose();
        }

        private void ctxMnuTreeview_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag.ToString())
            {
                case "ManageMetaGroups":
                    frmMetaGroups _frmMetaGroupForCreation = new frmMetaGroups(currentWsusServer.MetaGroups, computerGroups);
                    if (_frmMetaGroupForCreation.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        PopulateMetaGroupNode();
                        FrmSettings settingsForm = new FrmSettings();
                        settingsForm.SaveSettings(serverList);
                        updateCtrl.SetMetaGroups(currentWsusServer.MetaGroups);
                    }
                    break;
                case "ManageThisMetaGroup":
                    frmMetaGroups _frmMetaGroupForEdition = new frmMetaGroups(currentWsusServer.MetaGroups, computerGroups, trvWsus.SelectedNode.Text);
                    if (_frmMetaGroupForEdition.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        PopulateMetaGroupNode();
                        FrmSettings settingsForm = new FrmSettings();
                        settingsForm.SaveSettings(serverList);
                        updateCtrl.SetMetaGroups(currentWsusServer.MetaGroups);
                    }
                    break;
                case "DeleteThisMetaGroup":
                    string metaGroupName = trvWsus.SelectedNode.Text;
                    MetaGroup metaGroupToDelete = new MetaGroup();
                    bool found = false;
                    foreach (MetaGroup metagroup in currentWsusServer.MetaGroups)
                    {
                        if (metagroup.Name == metaGroupName)
                        {
                            metaGroupToDelete = metagroup;
                            found = true;
                            break;
                        }
                    }
                    if (found)
                    {
                        currentWsusServer.MetaGroups.Remove(metaGroupToDelete);
                        PopulateMetaGroupNode();
                        FrmSettings settingsForm = new FrmSettings();
                        settingsForm.SaveSettings(serverList);
                        updateCtrl.SetMetaGroups(currentWsusServer.MetaGroups);
                        trvWsus.SelectedNode = allMetaGroupsNode;
                        trvWsus_AfterSelect(trvWsus, new TreeViewEventArgs(allMetaGroupsNode));
                    }
                    break;
                case "CreateUpdate":
                    createUpdatetoolStripMenuItem.PerformClick();
                    break;
                case "SendDetectNow":
                case "SendReportNow":
                    List<string> targetComputers = new List<string>();
                    string login = null;
                    string password = null;
                    string groupName = trvWsus.SelectedNode.Text;
                    ComputerTargetCollection computerCollection = wsus.GetComputerTargets(computerGroupGuidConverter[groupName]);
                    FrmRemoteExecution remoteExecution = new FrmRemoteExecution();

                    foreach (IComputerTarget computer in computerCollection)
                    {
                        targetComputers.Add(computer.FullDomainName);
                    }
                    ctxMnuTreeview.Hide();
                    switch (Properties.Settings.Default.Credential)
                    {
                        case "Ask":
                            Credentials cred = new Credentials();
                            if (cred.ShowDialog() == DialogResult.OK)
                            {
                                login = cred.Login;
                                password = cred.Password;
                            }
                            break;
                        case "Specified":
                            login = Properties.Settings.Default.Login;
                            password = Properties.Settings.Default.Password;
                            break;
                        default:
                            break;
                    }
                    remoteExecution.Show(this);
                    switch (e.ClickedItem.Tag.ToString())
                    {
                        case "SendDetectNow":
                            remoteExecution.SendDetectNow(targetComputers, login, password);
                            break;
                        case "SendReportNow":
                            remoteExecution.SendReportNow(targetComputers, login, password);
                            break;
                        case "RebootNow":
                            remoteExecution.SendRebootNow(targetComputers, login, password);
                            break;
                        default:
                            break;
                    }
                    break;
                case "CleanUpdateServicesPackagesFolder":
                    CleanUpdateServicesPackagesFolder();
                    break;
                default:
                    break;
            }
        }

    }
}
