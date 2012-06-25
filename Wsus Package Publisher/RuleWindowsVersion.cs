using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    internal partial class RuleWindowsVersion : GenericRule
    {
        private string _comparison;
        private enum ComparisonType
        {
            LessThan,
            LessThanOrEqualTo,
            EqualTo,
            GreaterThanOrEqualTo,
            GreaterThan
        }
        
        System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(RuleWindowsVersion).Assembly);

        public RuleWindowsVersion()
        {
            InitializeComponent();

            txtBxDescription.Text = resManager.GetString("DescriptionRuleWindowsVersion");

            cmbBxComparison.Items.Add(resManager.GetString("ComparisonLessThan"));
            cmbBxComparison.Items.Add(resManager.GetString("ComparisonLessThanOrEqualTo"));
            cmbBxComparison.Items.Add(resManager.GetString("ComparisonEqualTo"));
            cmbBxComparison.Items.Add(resManager.GetString("ComparisonGreaterThanOrEqualTo"));
            cmbBxComparison.Items.Add(resManager.GetString("ComparisonGreaterThan"));

            chkBxComparison.Select();
        }

        #region (Properties - Propriétés)

        internal bool UseComparison
        {
            get { return chkBxComparison.Checked; }
            set { chkBxComparison.Checked = value; }
        }

        internal string Comparison
        {
            get
            {
                if (cmbBxComparison.SelectedIndex != -1)
                    _comparison = Enum.GetNames(typeof(ComparisonType))[cmbBxComparison.SelectedIndex];
                else
                    _comparison = "";
                return _comparison;
            }
            set { _comparison = value; }
        }

        internal bool UseMajorVersion
        {
            get { return chkBxMajorVersion.Checked; }
            set { chkBxMajorVersion.Checked = value; }
        }

        internal uint MajorVersion
        {
            get { return (uint)nupMajorVersion.Value; }
            set { nupMajorVersion.Value = value; }
        }

        internal bool UseMinorVersion
        {
            get { return chkBxMinorVersion.Checked; }
            set { chkBxMinorVersion.Checked = value; }
        }

        internal uint MinorVersion
        {
            get { return (uint)nupMinorVersion.Value; }
            set { nupMinorVersion.Value = value; }
        }

        internal bool UseBuildNumber
        {
            get { return chkBxBuildNumber.Checked; }
            set { chkBxBuildNumber.Checked = value; }
        }

        internal uint BuildNumber
        {
            get { return (uint)nupBuildNumber.Value; }
            set { nupBuildNumber.Value = value; }
        }

        internal bool UseServicePackMajor
        {
            get { return chkBxServicePackMajor.Checked; }
            set { chkBxServicePackMajor.Checked = value; }
        }

        internal ushort ServicePackMajor
        {
            get { return (ushort)nupServicePackMajor.Value; }
            set { nupServicePackMajor.Value = value; }
        }

        internal bool UseServicePackMinor
        {
            get { return chkBxServicePackMinor.Checked; }
            set { chkBxServicePackMinor.Checked = value; }
        }

        internal ushort ServicePackMinor
        {
            get { return (ushort)nupServicePackMinor.Value; }
            set { nupServicePackMinor.Value = value; }
        }

        internal bool UseProductType
        {
            get { return chkBxProductType.Checked; }
            set { chkBxProductType.Checked = value; }
        }

        internal ushort ProductType
        {
            get
            {
                switch (cmbBxProductType.SelectedIndex)
                {
                    case 0:
                        return 1;
                    case 1:
                        return 3;
                    case 2:
                        return 2;
                    default:
                        return 255;
                }
            }
            set
            {
                switch (value)
                {
                    case 1:
                        cmbBxProductType.SelectedIndex = 0;
                        break;
                    case 2:
                        cmbBxProductType.SelectedIndex = 2;
                        break;
                    case 3:
                        cmbBxProductType.SelectedIndex = 1;
                        break;
                    default:
                        UseProductType = false;
                        break;
                }
            }
        }

        #endregion

        #region(Responses to events - Réponses aux événements)

        private void cmbBxComparison_SelectedIndexChanged(object sender, EventArgs e)
        {
            UseComparison = true;
        }

        private void cmbBxOperatingSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbBxOperatingSystem.SelectedIndex)
            {
                case 0:
                    SelectOS(6, 1, 0); // Windows 7
                    break;
                case 1:
                    SelectOS(6, 1, 2); // Windows Server 2008R2
                    break;
                case 2:
                    SelectOS(6, 0, 0); // Windows Vista
                    break;
                case 3:
                    SelectOS(6, 0, 2); // Windows Server 2008
                    break;
                case 4:
                    SelectOS(5, 2, 2); // Windows Server 2003R1 & R2
                    break;
                case 5:
                    SelectOS(5, 1, 0); // Windows XP
                    break;
                case 6:
                    SelectOS(5, 0, 0); // Windows 2000
                    break;
                default:
                    break;
            }
        }

        private void chkBxMajorVersion_CheckedChanged(object sender, EventArgs e)
        {
            nupMajorVersion.Enabled = chkBxMajorVersion.Checked;
            UpdateOkBtn();
        }

        private void chkBxMinorVersion_CheckedChanged(object sender, EventArgs e)
        {
            nupMinorVersion.Enabled = chkBxMinorVersion.Checked;
        }

        private void chkBxBuildNumber_CheckedChanged(object sender, EventArgs e)
        {
            nupBuildNumber.Enabled = chkBxBuildNumber.Checked;
        }
        
        private void chkBxServicePackMajor_CheckedChanged(object sender, EventArgs e)
        {
            nupServicePackMajor.Enabled = chkBxServicePackMajor.Checked;
        }

        private void chkBxSericePackMinor_CheckedChanged(object sender, EventArgs e)
        {
            nupServicePackMinor.Enabled = chkBxServicePackMinor.Checked;
        }

        private void chkBxProductType_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOkBtn();
        }

        private void cmbBxProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UseProductType = true;
            UpdateOkBtn();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region(Methods - Méthodes)

        private void SelectOS(int majorVersion, int minorVersion, int productType)
        {
            nupMajorVersion.Value = majorVersion;
            UseMajorVersion = true;
            nupMinorVersion.Value = minorVersion;
            UseMinorVersion = true;
            cmbBxProductType.SelectedIndex = productType;
            UseProductType = true;
            UpdateOkBtn();
        }

        private void UpdateOkBtn()
        {
            if (UseMajorVersion || UseProductType)
                btnOk.Enabled = true;
            else
                btnOk.Enabled = false;
        }

        internal override string GetRtfFormattedRule(string rtf, int tabulation)
        {
            RichTextBox rTxtBx = new RichTextBox();
            string tab = new string(' ', tabulation);
            rTxtBx.Rtf += rtf;
            rTxtBx.Select(rTxtBx.Text.Length - 1, 1);

            if (UseMajorVersion || UseProductType)
            {
                print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, tab);

                print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "<bar:");
                print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.red, "WindowsVersion");

                if (UseComparison)
                {
                    print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " Comparison");
                    print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
                    print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, Comparison);
                    print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "\"");
                }

                if (UseMajorVersion)
                {
                    print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " MajorVersion");
                    print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
                    print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, MajorVersion.ToString());
                    print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "\"");
                }                

                if (UseMinorVersion)
                {
                    print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " MinorVersion");
                    print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
                    print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, MinorVersion.ToString());
                    print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "\"");
                }

                if (UseBuildNumber)
                {
                    print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " BuildNumber");
                    print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
                    print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, BuildNumber.ToString());
                    print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "\"");
                }

                if (UseServicePackMajor)
                {
                    print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " ServicePackMajor");
                    print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
                    print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, ServicePackMajor.ToString());
                    print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "\"");
                }

                if (UseServicePackMinor)
                {
                    print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " ServicePackMinor");
                    print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
                    print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, ServicePackMinor.ToString());
                    print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "\"");
                }

                if (UseProductType)
                {
                    print(rTxtBx, RulesViewer.elementAndAttributeFont, RulesViewer.blue, " ProductType");
                    print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "=\"");
                    print(rTxtBx, RulesViewer.boldFont, RulesViewer.black, ProductType.ToString());
                    print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "\"");
                }

                print(rTxtBx, RulesViewer.normalFont, RulesViewer.black, "/>\r\n");
            }
            return rTxtBx.Rtf;
        }

        internal override string GetXmlFormattedRule()
        {
            string result = "";

            if (UseMajorVersion || UseProductType)
            {
                result += "<bar:WindowsVersion";

                if(UseComparison)
                    result += " Comparison=\"" + Comparison + "\"";
                
                if (UseMajorVersion)
                    result += " MajorVersion=\"" + MajorVersion + "\"";

                if (UseMinorVersion)
                    result += " MinorVersion=\"" + MinorVersion + "\"";

                if (UseBuildNumber)
                    result += " BuildNumber=\"" + BuildNumber + "\"";

                if (UseServicePackMajor)
                    result += " ServicePackMajor=\"" + ServicePackMajor + "\"";

                if (UseServicePackMinor)
                    result += " ServicePackMinor=\"" + ServicePackMinor + "\"";

                if (UseProductType)
                    result += " ProductType=\"" + ProductType + "\"";

                result += "/>\r\n";
            }

            return result;
        }

        public override string ToString()
        {
            return resManager.GetString("WindowsVersion");
        }

        #endregion
    }
}
