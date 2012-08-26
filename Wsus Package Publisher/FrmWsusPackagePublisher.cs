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
            updateCtrl = new UpdateControl();
            updateCtrl.Dock = DockStyle.Fill;
            computerCtrl = new ComputerControl();
            computerCtrl.Dock = DockStyle.Fill;
            wsus.UpdatePublished += new WsusWrapper.UpdatePublisedEventHandler(UpdatePublished);
            wsus.UpdateRevised += new WsusWrapper.UpdateRevisedEventHandler(UpdateRevised);
        }

        internal void Approve(IUpdate updateToApprove)
        {
            FrmApprovalSet approvalSet = new FrmApprovalSet(computerGroups);

            if (approvalSet.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Dictionary<string, string> approvals = approvalSet.Approvals;

                foreach (string computerGroup in approvals.Keys)
                {
                    switch (approvals[computerGroup])
                    {
                        case "Approve For Installation":
                            //updateToApprove.Approve(UpdateApprovalAction.Install, wsus.GetComputerTargetGroup(computerGroups[computerGroup]));
                            break;
                        case "Approve For Optionnal Installation":
                            //updateToApprove.ApproveForOptionalInstall(wsus.GetComputerTargetGroup(computerGroups[computerGroup]));
                            break;
                        case "Approve For Desinstallation":
                            //updateToApprove.Approve(UpdateApprovalAction.Uninstall, wsus.GetComputerTargetGroup(computerGroups[computerGroup]));
                            break;
                        case "Not Approve":
                            //updateToApprove.Approve(UpdateApprovalAction.NotApproved, wsus.GetComputerTargetGroup(computerGroups[computerGroup]));
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Version 0.1.0.0");
        }

        private void btnConnectToServer_Click(object sender, EventArgs e)
        {
            btnConnectToServer.Enabled = false;
            if (wsus.Connect(Properties.Settings.Default.ServerName, Properties.Settings.Default.UseSSL, Properties.Settings.Default.ServerPort, Properties.Settings.Default.Language))
            {
                string rootComputerGroupName;
                Guid rootComputerGroupId;

                serverNode = new TreeNode(Properties.Settings.Default.ServerName);
                serverNode.Tag = "Server";
                trvWsus.Nodes.Add(serverNode);
                rootComputerGroupName = wsus.GetAllComputerTargetGroup().Name;
                rootComputerGroupId = wsus.GetAllComputerTargetGroup().Id;

                allComputersNode = new TreeNode(rootComputerGroupName);
                allComputersNode.Tag = "ComputerGroup";
                serverNode.Nodes.Add(allComputersNode);

                computerGroups.Add(rootComputerGroupName, rootComputerGroupId);
                PopulateTreeviewWithComputerGroups(new KeyValuePair<string, Guid>(rootComputerGroupName, rootComputerGroupId), allComputersNode);

                allUpdatesNode = new TreeNode(resMan.GetString("updates"));
                allUpdatesNode.Tag = "Updates";
                serverNode.Nodes.Add(allUpdatesNode);
                CollectUpdates();
                serverNode.Expand();
                allUpdatesNode.Expand();
                updateCtrl.SetComputerGroups(computerGroups);
            }
            else
                btnConnectToServer.Enabled = true;
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

        private void PopulateTreeviewWithComputerGroups(KeyValuePair<string, Guid> group, TreeNode node)
        {
            foreach (KeyValuePair<string, Guid> childGroup in wsus.GetChildComputerTargetGroupNameAndId(group.Value))
            {
                TreeNode newNode = new TreeNode(childGroup.Key);

                newNode.Tag = "ComputerGroup";
                computerGroups.Add(childGroup.Key, childGroup.Value);
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
            FrmSettings settings = new FrmSettings();
            settings.ShowDialog();
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
    }
}
