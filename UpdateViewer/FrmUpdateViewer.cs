using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.UpdateServices.Administration;

namespace UpdateViewer
{
    public partial class FrmUpdateViewer : Form
    {
        AdminProxy proxy = new AdminProxy();
        IUpdateServer wsus;
        string serverName = "wsus01";
        int serverPort = 443;
        bool useSSLConnection = true;
        Dictionary<string, Company> companies = new Dictionary<string, Company>();
        Company _selectedCompany;
        Product _selectedProduct;

        public FrmUpdateViewer()
        {
            InitializeComponent();

            wsus = proxy.GetRemoteUpdateServerInstance(serverName, useSSLConnection, serverPort);
            UpdateCollection updates = wsus.GetUpdates();

            PopulateDictionaries(updates);
            trvWsus.Nodes.Add("Updates");
            PopulateTreeviewWithUpdates(trvWsus.Nodes[0]);
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
                    companyNode.Nodes.Add(product.ProductName);
                    companyNode.LastNode.Tag = "Product";
                }
            }
        }

        private void trvWsus_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag.ToString() == "Product")
            {
                SelectedCompany = companies[e.Node.Parent.Text];
                SelectedProduct = SelectedCompany.Products[e.Node.Text];

                lstBxUpdates.Items.Clear();
                foreach (IUpdate update in SelectedProduct.Updates)
                {
                    lstBxUpdates.Items.Add(update.Title);
                }
            }
        }

        private void lstBxUpdates_SelectedIndexChanged(object sender, EventArgs e)
        {
            IUpdate update = SelectedProduct.Updates[lstBxUpdates.SelectedIndex];

            txtBxRule.Text = update.Title;
            string folder = Environment.GetEnvironmentVariable("temp");
            update.ExportPackageMetadata(folder + @"\package.txt");
            

        }

        private Company SelectedCompany
        {
            get { return _selectedCompany; }
            set { _selectedCompany = value; }
        }

        private Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; }
        }


    }
}
