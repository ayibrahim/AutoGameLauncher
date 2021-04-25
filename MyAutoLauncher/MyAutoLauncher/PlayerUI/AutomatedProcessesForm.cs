using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace PlayerUI
{
    public partial class AutomatedProcessesForm : Form
    {
        string con2 = ConfigurationManager.ConnectionStrings["APDB"].ConnectionString;
        string mainQueryLoad = "SELECT * FROM tblProcess";
        public AutomatedProcessesForm()
        {
            InitializeComponent();
        }
        int selectedID = -1;
        private void AutomatedProcessesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            var form2 = new MainPage();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        private void AutomatedProcessesForm_Load(object sender, EventArgs e)
        {
            try
            {
                APGridView.DataSource = GetAutomatedProcessList(mainQueryLoad);

                int colCount = APGridView.ColumnCount - 2;

                for (int i = 0; i < colCount; i++)
                {
                    APGridView.Columns[i].Width = 150;
                }

                //Styles
                APGridView.AllowUserToResizeRows = false;
                APGridView.AllowUserToResizeColumns = false;

                APGridView.Columns["PSWRD"].Visible = false;
                APGridView.Columns["ProcessID"].Visible = false;

                APGridView.BorderStyle = BorderStyle.None;
                APGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                APGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                APGridView.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                APGridView.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                APGridView.BackgroundColor = Color.White;

                APGridView.EnableHeadersVisualStyles = false;
                APGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                APGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                APGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                //ComboBox properties
                cmbSearch.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbSearch.SelectedItem = "ProcessName";
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        public void ClearFields()
        {
            selectedID = -1;
            txtProcessName.Text = string.Empty;
            txtProcessPath.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtKeyboardShortcut.Text = string.Empty;
        }

        private DataTable GetAutomatedProcessList(string query) {
            DataTable dtAutomatedProcess = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(con2))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        dtAutomatedProcess.Load(reader);
                    }
                }
                return dtAutomatedProcess;
            }
            catch (Exception ex){
                MessageBox.Show(ex.ToString());
                return dtAutomatedProcess;
            }
        }

        private void APGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try { 
                if (e.RowIndex > -1)
                {
                    APGridView.CurrentRow.Selected = true;
                    selectedID = (int)APGridView.Rows[e.RowIndex].Cells["ProcessID"].Value;
                    txtProcessName.Text = APGridView.Rows[e.RowIndex].Cells["ProcessName"].FormattedValue.ToString();
                    txtProcessPath.Text = APGridView.Rows[e.RowIndex].Cells["ProcessPath"].FormattedValue.ToString();
                    txtKeyboardShortcut.Text = APGridView.Rows[e.RowIndex].Cells["KeyboardShortcut"].FormattedValue.ToString();
                    txtUserName.Text = APGridView.Rows[e.RowIndex].Cells["UserName"].FormattedValue.ToString();
                    txtPassword.Text = APGridView.Rows[e.RowIndex].Cells["PSWRD"].FormattedValue.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedID > -1)
                {
                    SqlConnection con = new SqlConnection(con2);
                    SqlCommand cmd = new SqlCommand("UpdateProcess", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProcessID", selectedID.ToString());
                    cmd.Parameters.AddWithValue("@ProcessName", txtProcessName.Text.ToString());
                    cmd.Parameters.AddWithValue("@ProcessPath", txtProcessPath.Text.ToString());
                    cmd.Parameters.AddWithValue("@KeyboardShortcut", txtKeyboardShortcut.Text.ToString());
                    cmd.Parameters.AddWithValue("@UserName", txtUserName.Text.ToString());
                    cmd.Parameters.AddWithValue("@PSWRD", txtPassword.Text.ToString());
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    APGridView.DataSource = GetAutomatedProcessList(mainQueryLoad);
                    ClearFields();
                    MessageBox.Show("Succesfully Updated");

                }
                else
                {
                    MessageBox.Show("No Table Row Selected For Editing");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedID == -1)
                {
                    SqlConnection con = new SqlConnection(con2);
                    SqlCommand cmd = new SqlCommand("InsertNewProcess", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProcessName", txtProcessName.Text.ToString());
                    cmd.Parameters.AddWithValue("@ProcessPath", txtProcessPath.Text.ToString());
                    cmd.Parameters.AddWithValue("@KeyboardShortcut", txtKeyboardShortcut.Text.ToString());
                    cmd.Parameters.AddWithValue("@UserName", txtUserName.Text.ToString());
                    cmd.Parameters.AddWithValue("@PSWRD", txtPassword.Text.ToString());
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    APGridView.DataSource = GetAutomatedProcessList(mainQueryLoad);
                    ClearFields();
                    MessageBox.Show("Succesfully Added");

                }
                else
                {
                    MessageBox.Show("A Table Row is Selected For Editing - Clear Data Before Adding");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedID > -1)
                {
                    if (DialogResult.Yes == MessageBox.Show("Do You Want Delete Shortcut Process: " + txtKeyboardShortcut.Text + " ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        SqlConnection con = new SqlConnection(con2);
                        SqlCommand cmd = new SqlCommand("DeleteProcess", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProcessID", selectedID.ToString());
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        con.Close();

                        APGridView.DataSource = GetAutomatedProcessList(mainQueryLoad);
                        ClearFields();
                        MessageBox.Show("Succesfully Deleted");
                    }


                }
                else
                {
                    MessageBox.Show("No Table Row Selected For Deleting");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            APGridView.DataSource = GetAutomatedProcessList(mainQueryLoad);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string query = "SELECT * FROM tblProcess WHERE ";
            if (cmbSearch.SelectedItem.ToString() == "ProcessName") { query += "ProcessName LIKE '"; }
            else if (cmbSearch.SelectedItem.ToString() == "ProcessPath") { query += "ProcessPath LIKE '"; }
            else if (cmbSearch.SelectedItem.ToString() == "KeyboardShortcut"){ query += "KeyboardShortcut LIKE '"; }
            else if (cmbSearch.SelectedItem.ToString() == "UserName") { query += "UserName LIKE '";}
            query += txtSearch.Text.ToString() + "%'";
            APGridView.DataSource = GetAutomatedProcessList(query);

        }
    }
}
