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
    internal partial class FrmApprovalSet : Form
    {
        private Dictionary<string, Guid> _computersGroup;
        private List<ApprovalObject> _approval = new List<ApprovalObject>();
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmApprovalSet).Assembly);
        private WsusWrapper _wsus;

        internal FrmApprovalSet(Dictionary<string, Guid> computersGroup, UpdateCollection updatesToApprove)
        {
            InitializeComponent();

            _wsus = WsusWrapper.GetInstance();
            _computersGroup = computersGroup;
            dtDeadLine.Value = DateTime.Now.AddDays(_wsus.Server.DeadLineDaysSpan);
            nupHour.Value = _wsus.Server.DeadLineHour;
            nupMinute.Value = _wsus.Server.DeadLineMinute;
            FillDataGridView(updatesToApprove);
        }

        private void FillDataGridView(UpdateCollection updatesToApprove)
        {
            object[] approvalsObj = new object[]
            { resMan.GetString(ApprovalObject.Approvals.Unchanged.ToString()), 
              resMan.GetString(ApprovalObject.Approvals.ApproveForInstallation.ToString()),
              resMan.GetString(ApprovalObject.Approvals.ApproveForOptionalInstallation.ToString()),
              resMan.GetString(ApprovalObject.Approvals.ApproveForUninstallation.ToString()),
              resMan.GetString(ApprovalObject.Approvals.NotApproved.ToString())
            };
            DateTime noDeadLineSet = new DateTime(3155378975999999999);

            dgvTargetGroup.SuspendLayout();
            DataGridViewComboBoxColumn approvalColumn = (DataGridViewComboBoxColumn)dgvTargetGroup.Columns[1];
            approvalColumn.Items.AddRange(approvalsObj);
            cmbBxApproval.Items.AddRange(approvalsObj);
            cmbBxApproval.SelectedIndex = 0;

            foreach (string group in _computersGroup.Keys)
            {
                dgvTargetGroup.Rows.Add(group);
            }
            foreach (DataGridViewRow row in dgvTargetGroup.Rows)
            {
                if (updatesToApprove.Count == 1)
                {
                    UpdateApprovalCollection approvals = _wsus.GetUpdateApprovalStatus(_computersGroup[row.Cells[0].Value.ToString()], updatesToApprove[0]);
                    if (approvals.Count != 0)
                    {
                        switch (approvals[0].Action)
                        {
                            case UpdateApprovalAction.All:
                                row.Cells[1].Value = resMan.GetString(ApprovalObject.Approvals.Unchanged.ToString());
                                break;
                            case UpdateApprovalAction.Install:
                                if (approvals[0].IsOptional)
                                    row.Cells[1].Value = resMan.GetString(ApprovalObject.Approvals.ApproveForOptionalInstallation.ToString());
                                else
                                    row.Cells[1].Value = resMan.GetString(ApprovalObject.Approvals.ApproveForInstallation.ToString());
                                if (approvals[0].Deadline != noDeadLineSet)
                                    row.Cells[2].Value = approvals[0].Deadline;
                                break;
                            case UpdateApprovalAction.NotApproved:
                                row.Cells[1].Value = resMan.GetString(ApprovalObject.Approvals.NotApproved.ToString());
                                break;
                            case UpdateApprovalAction.Uninstall:
                                row.Cells[1].Value = resMan.GetString(ApprovalObject.Approvals.ApproveForUninstallation.ToString());
                                if (approvals[0].Deadline != noDeadLineSet)
                                    row.Cells[2].Value = approvals[0].Deadline;
                                break;
                            default:
                                row.Cells[1].Value = resMan.GetString(ApprovalObject.Approvals.Unchanged.ToString());
                                break;
                        }
                    }
                    else
                        row.Cells[1].Value = resMan.GetString(ApprovalObject.Approvals.Unchanged.ToString());
                }
                else
                    row.Cells[1].Value = resMan.GetString(ApprovalObject.Approvals.Unchanged.ToString());
            }
            dgvTargetGroup.ResumeLayout();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ApprovalObject.Approvals[] approvalsArray = new ApprovalObject.Approvals[]
            { ApprovalObject.Approvals.Unchanged, 
              ApprovalObject.Approvals.ApproveForInstallation,
              ApprovalObject.Approvals.ApproveForOptionalInstallation,
              ApprovalObject.Approvals.ApproveForUninstallation,
              ApprovalObject.Approvals.NotApproved
            };

            _approval.Clear();

            foreach (DataGridViewRow row in dgvTargetGroup.Rows)
            {
                if (row.Cells[1].Value != null && !string.IsNullOrEmpty(row.Cells[1].Value.ToString()))
                {
                    ApprovalObject.Approvals approval = GetApproval(row.Cells[1].Value.ToString());
                    if (approval != ApprovalObject.Approvals.Unchanged)
                    {
                        if (row.Cells[2].Value != null)
                        {
                            DateTime deadLine;
                            if (DateTime.TryParse(row.Cells[2].Value.ToString(), out deadLine))
                                _approval.Add(new ApprovalObject(_computersGroup[row.Cells[0].Value.ToString()], approval, deadLine));
                            else
                                _approval.Add(new ApprovalObject(_computersGroup[row.Cells[0].Value.ToString()], approval));
                        }
                        else
                            _approval.Add(new ApprovalObject(_computersGroup[row.Cells[0].Value.ToString()], approval));
                    }

                }
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private ApprovalObject.Approvals GetApproval(string searchValue)
        {
            foreach (ApprovalObject.Approvals approval in Enum.GetValues(typeof(ApprovalObject.Approvals)))
            {
                if (resMan.GetString(approval.ToString()) == searchValue)
                    return approval;
            }
            return ApprovalObject.Approvals.Unchanged;
        }

        internal List<ApprovalObject> Approvals
        {
            get { return _approval; }
        }

        private void btnSetDeadLine_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvTargetGroup.SelectedRows)
            {
                if (row.Cells[1].Value != null && !string.IsNullOrEmpty(row.Cells[1].Value.ToString()) &&
                    row.Cells[1].Value.ToString() != resMan.GetString(ApprovalObject.Approvals.NotApproved.ToString()) &&
                    row.Cells[1].Value.ToString() != resMan.GetString(ApprovalObject.Approvals.ApproveForOptionalInstallation.ToString()))
                    row.Cells[2].Value = new System.DateTime(dtDeadLine.Value.Year, dtDeadLine.Value.Month, dtDeadLine.Value.Day, (int)nupHour.Value, (int)nupMinute.Value, 0);
            }
        }

        private void btnSetApproval_Click(object sender, EventArgs e)
        {
            if (cmbBxApproval.SelectedItem != null)
            {
                foreach (DataGridViewRow row in dgvTargetGroup.SelectedRows)
                {
                    row.Cells[1].Value = cmbBxApproval.SelectedItem;
                    if (cmbBxApproval.SelectedItem.ToString() == resMan.GetString(ApprovalObject.Approvals.NotApproved.ToString()) ||
                        cmbBxApproval.SelectedItem.ToString() == resMan.GetString(ApprovalObject.Approvals.ApproveForOptionalInstallation.ToString()))
                        row.Cells[2].Value = null;
                }
            }
        }

        private void dgvTargetGroup_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewRow row in dgvTargetGroup.SelectedRows)
            {
                if (row.Cells[1].Value != null && !string.IsNullOrEmpty(row.Cells[1].Value.ToString()))
                    row.Cells[2].Value = null;
            }
        }
                
    }
}
