using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarWashManagementSystem
{
    public partial class Service : Form
    {
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader sdr;
        string title = "Car Wash Management System";
        public Service()
        {
            InitializeComponent();
            loadService();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            ServiceModule module = new ServiceModule(this);
            module.btnUpdate.Enabled = true;
            module.ShowDialog();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadService()
;       }

        private void dgvService_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvService.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                //to sent vehicle data to the Vehicle  module
                ServiceModule module = new ServiceModule(this);
                module.lblSid.Text = dgvService.Rows[e.RowIndex].Cells[1].Value.ToString();
                module.txtName.Text = dgvService.Rows[e.RowIndex].Cells[2].Value.ToString();
                module.txtPrice.Text = dgvService.Rows[e.RowIndex].Cells[3].Value.ToString();


                module.btnSave.Enabled = false;
                module.ShowDialog();
            }
            else if (colName == "Delete")    //if you want to delete  the record to click the delete icon on the datagridview
            {
                try
                {
                    if (MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("DELETE FROM tbService WHERE id LIKE'" + dgvService.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", dbcon.connect());
                        dbcon.open();
                        cmd.ExecuteNonQuery();
                        dbcon.close();
                        MessageBox.Show("Service Type data has been succsessfully removed!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, title);
                }
            }
            loadService(); // reload the service list after  update the record
        }

        #region method

        public void loadService()
        {
            try
            {
                int i = 0;  //to show number for Service list
                dgvService.Rows.Clear();

                cmd = new SqlCommand("SELECT * FROM tbService WHERE name LIKE '%" + txtSearch.Text + "%'", dbcon.connect());

                dbcon.open();
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    i++;
                    //to add data to the datagridview  from the database
                    dgvService.Rows.Add(i, sdr[0].ToString(), sdr[1].ToString(), sdr[2].ToString());
                }
                dbcon.close();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, title);
            }
        }
        #endregion method

        
    }
}
