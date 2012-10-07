using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.UpdateServices.Administration;

namespace Wsus_Package_Publisher
{
    public partial class FrmWsusPackagePublisher : Form
    {
        private WsusWrapper wsus = WsusWrapper.GetInstance();
        private Dictionary<string, Guid> computerGroups = new Dictionary<string, Guid>();
        private Dictionary<string, Company> companies = new Dictionary<string, Company>();
        private UpdateControl updateCtrl = null;
        private ComputerControl computerCtrl = null;
        private TreeNode serverNode;
        private TreeNode allComputersNode;
        private TreeNode allUpdatesNode;
        private TreeNode allMetaGroupsNode;
        private FrmWaiting _waitingForm;
        private System.Threading.Thread _waitingThread;
        private List<WsusServer> serverList = new List<WsusServer>();
        private FrmSettings settings = new FrmSettings();
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmWsusPackagePublisher).Assembly);

        public FrmWsusPackagePublisher()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);

            InitializeComponent();
            if (Properties.Settings.Default.Language == "fr")
            {
                françaisToolStripMenuItem.Checked = true;
                englishToolStripMenuItem.Checked = false;
            }
            else
            {
                françaisToolStripMenuItem.Checked = false;
                englishToolStripMenuItem.Checked = true;
            }
            ClearBeforeConnecting();
            wsus.UpdatePublished += new WsusWrapper.UpdatePublisedEventHandler(UpdatePublished);
            wsus.UpdateRevised += new WsusWrapper.UpdateRevisedEventHandler(UpdateRevised);
        }

        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Version ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            MessageBox.Show("Version " + ver.ToString());
        }

        private void btnConnectToServer_Click(object sender, EventArgs e)
        {
            IComputerTargetGroup allComputerTargetGroup;
            btnConnectToServer.Enabled = false;
            WsusServer serverWsus = (WsusServer)cmbBxServerList.SelectedItem;
            StartWaitingForm(resMan.GetString("connecting"));
            if (wsus.Connect(serverWsus, Properties.Settings.Default.Language))
            {
                string rootComputerGroupName;
                Guid rootComputerGroupId;
                ClearBeforeConnecting();

                if (wsus.IsReplica)
                    serverNode = new TreeNode(serverWsus.Name + " (" + resMan.GetString("ReplicaServer") + ")");
                else
                    serverNode = new TreeNode(serverWsus.Name);
                serverNode.Tag = "Server";
                trvWsus.Nodes.Add(serverNode);
                allComputerTargetGroup = wsus.GetAllComputerTargetGroup();
                rootComputerGroupName = allComputerTargetGroup.Name;
                rootComputerGroupId = allComputerTargetGroup.Id;

                allComputersNode = new TreeNode(rootComputerGroupName);
                allComputersNode.Tag = "ComputerGroup";
                serverNode.Nodes.Add(allComputersNode);

                computerGroups.Add(rootComputerGroupName, rootComputerGroupId);
                PopulateTreeviewWithComputerGroups(allComputerTargetGroup, allComputersNode);

                allUpdatesNode = new TreeNode(resMan.GetString("updates"));
                allUpdatesNode.Tag = "Updates";
                serverNode.Nodes.Add(allUpdatesNode);
                CollectUpdates();
                serverNode.Expand();
                allUpdatesNode.Expand();
                updateCtrl.SetComputerGroups(computerGroups);
                certificateToolStripMenuItem.Enabled = true;
                if (wsus.IsReplica)
                {
                    createUpdateToolStripMenuItem.Enabled = false;
                    certificateToolStripMenuItem.Enabled = false;
                    updateCtrl.LockFunctionnalities(true);
                }
                else
                {
                    createUpdateToolStripMenuItem.Enabled = true;
                    certificateToolStripMenuItem.Enabled = true;
                    updateCtrl.LockFunctionnalities(false);
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

        private void ClearBeforeConnecting()
        {
            computerGroups.Clear();
            companies.Clear();
            splitContainer1.Panel2.Controls.Clear();
            updateCtrl = null;
            computerCtrl = null;
            updateCtrl = new UpdateControl();
            updateCtrl.Dock = DockStyle.Fill;
            computerCtrl = new ComputerControl();
            computerCtrl.Dock = DockStyle.Fill;
            serverNode = null;
            allComputersNode = null;
            allUpdatesNode = null;
            allMetaGroupsNode = null;
            trvWsus.Nodes.Clear();
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

                newNode.Tag = "ComputerGroup";
                computerGroups.Add(childGroup.Name, childGroup.Id);
                node.Nodes.Add(newNode);
                PopulateTreeviewWithComputerGroups(childGroup, newNode);
            }
        }

        private void createUpdateToolStripMenuItem_Click(object sender, EventArgs e)
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
            updateWizard.ShowDialog();
        }

        private void trvWsus_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (e.Node.Tag != null && e.Node.Tag.ToString() == "ComputerGroup")
            {
                if ((splitContainer1.Panel2.Controls.Count == 1) && (splitContainer1.Panel2.Controls[0].GetType() == typeof(ComputerControl)))
                {
                    computerCtrl.Display(computerGroups[e.Node.Text]);
                }
                else
                {
                    splitContainer1.Panel2.Controls.Clear();
                    splitContainer1.Panel2.Controls.Add(computerCtrl);
                    computerCtrl.Display(computerGroups[e.Node.Text]);
                }
            }

            if (e.Node.Tag != null && e.Node.Tag.ToString() == "Company")
            {
                Company company = companies[e.Node.Text];
            }

            if (e.Node.Tag != null && e.Node.Tag.ToString() == "Product")
            {
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
            }
            this.Cursor = Cursors.Default;
        }

        internal Dictionary<string, Guid> ComputerGroups
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
            return computerGroups[targetGroupName];
        }

        private void françaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            françaisToolStripMenuItem.Checked = true;
            englishToolStripMenuItem.Checked = false;
            Properties.Settings.Default.Language = "fr";
            Properties.Settings.Default.Save();

            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr");
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            englishToolStripMenuItem.Checked = true;
            françaisToolStripMenuItem.Checked = false;
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
            {
                cmbBxServerList.Items.Add(server);
            }
            if (cmbBxServerList.Items.Count != 0)
                cmbBxServerList.SelectedIndex = 0;
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
            if (cmbBxServerList.SelectedItem != null && cmbBxServerList.SelectedItem.GetType() == typeof(WsusServer) && (cmbBxServerList.SelectedItem as WsusServer) != wsus.Server )
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
                certificateToolStripMenuItem.Enabled = false;
            }
        }
    }
}
