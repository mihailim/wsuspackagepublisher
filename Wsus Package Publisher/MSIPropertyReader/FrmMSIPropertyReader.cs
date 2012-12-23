using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MsiReader;

namespace Wsus_Package_Publisher
{
    public partial class FrmMSIPropertyReader : Form
    {
        MsiReader.MsiReader reader = new MsiReader.MsiReader();

        public FrmMSIPropertyReader()
        {
            InitializeComponent();
        }

        private void btnLoadMSIFile_Click(object sender, EventArgs e)
        {
            MsiReader.MsiReader msiReader = new MsiReader.MsiReader();

            if (openFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                reader.MsiFilePath = openFileDialog1.FileName;
                dtGrvProperties.Rows.Clear();
                dtGrvProperties.Columns.Clear();
                cmbBxTables.Items.Clear();

                SortedDictionary<string, Table> tables = reader.GetAllMSITables();

                foreach (KeyValuePair<string, Table> pair in tables)
                {
                    if (pair.Value.IsOrdered)
                        cmbBxTables.Items.Add(pair.Value);
                }
            }
        }

        private void cmbBxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtGrvProperties.Rows.Clear();
            dtGrvProperties.Columns.Clear();

            if (cmbBxTables.SelectedIndex != -1 && cmbBxTables.SelectedItem != null)
            {
                Table table = (Table)cmbBxTables.SelectedItem;

                table = reader.GetAllMSIValueFromTable(table);

                SortedDictionary<int, Column> columns = new SortedDictionary<int, Column>();

                foreach (Column column in table.Columns)
                {
                    columns.Add(column.Order, column);
                }

                foreach (KeyValuePair<int, Column> pair in columns)
                {
                    dtGrvProperties.Columns.Add(pair.Value.Name, pair.Value.Name);
                }

                for (int i = 0; i < columns[1].Values.Count; i++)
                {
                    int index = dtGrvProperties.Rows.Add();
                    DataGridViewRow row = dtGrvProperties.Rows[index];

                    foreach (KeyValuePair<int, Column> pair in columns)
                    {
                        if (pair.Value.Values.Count != 0)
                            row.Cells[pair.Value.Name].Value = pair.Value.Values[i];
                    }
                }
            }
        }
    }
}
