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
        //private string serverName = "wsus01";
        //private int serverPort = 443;
        //private bool useSecureConnection = true;
        private AdminProxy proxy = new AdminProxy();
        private IUpdateServer wsus;
        private Dictionary<string, Guid> computerGroups = new Dictionary<string, Guid>();
        private Dictionary<string, Company> companies = new Dictionary<string, Company>();
        private ComputerListViewer computerViewer = null;
        private ComputerDetailViewer computerDetail = null;
        private UpdateListViewer updateListViewer = null;
        private TreeNode serverNode;
        private TreeNode allComputersNode;
        private TreeNode allUpdatesNode;
        private TreeNode allMetaGroupsNode;

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

            trvWsus.ExpandAll();
        }

        internal void DeleteUpdate(Guid updateId, Company company, Product product)
        {
            wsus.DeleteUpdate(updateId);

            foreach (IUpdate update in product.Updates)
            {
                if (update.Id.UpdateId == updateId)
                {
                    product.Updates.Remove(update);
                    break;
                }
            }

            if (product.Updates.Count == 0)
            {
                company.Products.Remove(product.ProductName);
                if (company.Products.Count == 0)
                    companies.Remove(company.CompanyName);
            }
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
                            updateToApprove.Approve(UpdateApprovalAction.Install, wsus.GetComputerTargetGroup(computerGroups[computerGroup]));
                            break;
                        case "Approve For Optionnal Installation":
                            updateToApprove.ApproveForOptionalInstall(wsus.GetComputerTargetGroup(computerGroups[computerGroup]));
                            break;
                        case "Approve For Desinstallation":
                            updateToApprove.Approve(UpdateApprovalAction.Uninstall, wsus.GetComputerTargetGroup(computerGroups[computerGroup]));
                            break;
                        case "Not Approve":
                            updateToApprove.Approve(UpdateApprovalAction.NotApproved, wsus.GetComputerTargetGroup(computerGroups[computerGroup]));
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
            wsus = proxy.GetRemoteUpdateServerInstance(Properties.Settings.Default.ServerName, Properties.Settings.Default.UseSSL, Properties.Settings.Default.ServerPort);
            wsus.PreferredCulture = "fr";

            serverNode = new TreeNode(Properties.Settings.Default.ServerName);
            trvWsus.Nodes.Add(serverNode);

            IComputerTargetGroup allComputerGroup = wsus.GetComputerTargetGroup(ComputerTargetGroupId.AllComputers);
            allComputersNode = new TreeNode(allComputerGroup.Name);
            serverNode.Nodes.Add(allComputersNode);

            allComputersNode.Tag = "ComputerGroup";
            computerGroups.Add(allComputerGroup.Name, allComputerGroup.Id);
            PopulateTreeviewWithComputerGroups(allComputerGroup, allComputersNode);

            allUpdatesNode = new TreeNode("Mises à jour");
            serverNode.Nodes.Add(allUpdatesNode);
            PopulateDictionaries(wsus.GetUpdates());
            PopulateTreeviewWithUpdates(allUpdatesNode);
            btnConnectToServer.Enabled = true;
        }

        private void PopulateDictionaries(UpdateCollection updates)
        {
            foreach (IUpdate update in updates)
            {
                if (update.CompanyTitles != null && update.CompanyTitles.Count != 0 && update.ProductTitles != null && update.ProductTitles.Count != 0)
                {
                    if (!companies.ContainsKey(update.CompanyTitles[0]))
                        companies.Add(update.CompanyTitles[0], new Company(update.CompanyTitles[0]));
                    if (!companies[update.CompanyTitles[0]].Products.ContainsKey(update.ProductTitles[0]))
                        companies[update.CompanyTitles[0]].AddProduct(update.ProductTitles[0]);
                    companies[update.CompanyTitles[0]].Products[update.ProductTitles[0]].AddUpdate(update);
                }
            }
        }

        private void PopulateTreeviewWithUpdates(TreeNode treeNode)
        {
            foreach (Company company in companies.Values)
            {
                TreeNode companyNode = new TreeNode(company.CompanyName);
                companyNode.Tag = "Company";
                treeNode.Nodes.Add(companyNode);

                foreach (Product product in company.Products.Values)
                {
                    TreeNode productNode = new TreeNode(product.ProductName);
                    productNode.Tag = "Product";
                    companyNode.Nodes.Add(productNode);
                }
            }
        }

        private void PopulateTreeviewWithComputerGroups(IComputerTargetGroup group, TreeNode node)
        {
            ComputerTargetGroupCollection groups = group.GetChildTargetGroups();

            foreach (IComputerTargetGroup childGroup in groups)
            {
                TreeNode newNode = new TreeNode(childGroup.Name);

                newNode.Tag = "ComputerGroup";
                computerGroups.Add(childGroup.Name, childGroup.Id);
                node.Nodes.Add(newNode);
                if (childGroup.GetChildTargetGroups().Count != 0)
                    PopulateTreeviewWithComputerGroups(childGroup, newNode);
            }
        }

        private void createUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUpdateWizard updateWizard = new FrmUpdateWizard(wsus, companies);
            updateWizard.ShowDialog();
        }

        private void trvWsus_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (e.Node.Tag != null && e.Node.Tag.ToString() == "ComputerGroup")
            {
                IComputerTargetGroup computerTargetGroup = wsus.GetComputerTargetGroup(computerGroups[e.Node.Text]);
                ComputerTargetCollection computerTargets = computerTargetGroup.GetComputerTargets(false);

                if ((splitContainer2.Panel1.Controls.Count == 1) && (splitContainer2.Panel1.Controls[0].GetType() == typeof(ComputerListViewer)))
                {
                    computerViewer = (ComputerListViewer)splitContainer2.Panel1.Controls[0];
                    computerDetail = (ComputerDetailViewer)splitContainer2.Panel2.Controls[0];
                    computerViewer.ComputerCollection = computerTargets;
                }
                else
                {
                    if (splitContainer2.Panel1.Controls.Count == 1)
                    {
                        splitContainer2.Panel1.Controls[0].Dispose();
                        splitContainer2.Panel1.Controls.Clear();
                        splitContainer2.Panel2.Controls[0].Dispose();
                        splitContainer2.Panel2.Controls.Clear();
                    }
                    computerDetail = new ComputerDetailViewer();
                    computerViewer = new ComputerListViewer(computerTargets, computerDetail, wsus);
                    splitContainer2.Panel1.Controls.Add(computerViewer);
                    computerViewer.Dock = DockStyle.Fill;
                    splitContainer2.Panel2.Controls.Add(computerDetail);
                    computerDetail.Dock = DockStyle.Fill;
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
                        Product product = company.Products[e.Node.Text];
                        updateListViewer = new UpdateListViewer(this, company, product);
                        splitContainer2.SuspendLayout();
                        splitContainer2.Panel1.Controls.Clear();
                        splitContainer2.Panel1.Controls.Add(updateListViewer);
                        updateListViewer.Dock = DockStyle.Fill;
                        splitContainer2.ResumeLayout();
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
        /// Return the ComputerTargetGroup which have for name 'targetGroupName'.
        /// </summary>
        /// <param name="targetGroupName">The name of the group</param>
        /// <returns>Return the object IComputerTargetGroup corresponding to the group.</returns>
        internal IComputerTargetGroup GetTargetGroup(string targetGroupName)
        {
            return wsus.GetComputerTargetGroup(computerGroups[targetGroupName]);
        }

        /// <summary>
        /// Return the name of a computer.
        /// </summary>
        /// <param name="id">Guid of the computer.</param>
        /// <returns>The name of the computer.</returns>
        internal string GetComputerName(string id)
        {
            return wsus.GetComputerTarget(id).FullDomainName;
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


        internal void Decline(IUpdate ViewedUpdate)
        {
            ViewedUpdate.Decline();
        }
    }
}
