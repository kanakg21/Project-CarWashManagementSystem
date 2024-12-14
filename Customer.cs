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
    public partial class Customer : Form
    {
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader sdr;
        string title = "Car Wash Management System";
        bool check = false;
        public Customer()
        {
            InitializeComponent();
            loadCustomer();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            CustomerModule module = new CustomerModule(this);
            module.btnUpdate.Enabled = false;
            module.ShowDialog();
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                //to sent employee data to the customer module
                CustomerModule module = new CustomerModule(this);
                module.lblCid.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                module.txtFullName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
                module.txtPhone.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();
                module.txtCarno.Text = dgvCustomer.Rows[e.RowIndex].Cells[4].Value.ToString();
                module.txtCarModel.Text = dgvCustomer.Rows[e.RowIndex].Cells[5].Value.ToString();
                module.vid = vehicleIdbyName(dgvCustomer.Rows[e.RowIndex].Cells[6].Value.ToString()); //only value
                module.txtAddress.Text = dgvCustomer.Rows[e.RowIndex].Cells[7].Value.ToString();
                module.udPoints.Text = dgvCustomer.Rows[e.RowIndex].Cells[8].Value.ToString();

                module.btnSave.Enabled = false;
                module.udPoints.Enabled = true;
                module.ShowDialog();
            }
            else if (colName == "Delete")    //if you want to delete  the record to click the delete icon on the datagridview
            {
                try
                {
                    if (MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("DELETE FROM tbCustomer WHERE id LIKE'" + dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", dbcon.connect());
                        dbcon.open();
                        cmd.ExecuteNonQuery();
                        dbcon.close();
                        MessageBox.Show("Customer data has been succsessfully removed!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, title);
                }
            }
            loadCustomer();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadCustomer();
        }

        #region method
        public void loadCustomer()
        {
            try
            {
                int i = 0;  //to show number for Customer list
                dgvCustomer.Rows.Clear();

                cmd = new SqlCommand("SELECT C.id,C.name, phone, carno, carmodel, V.name, address, points FROM tbCustomer AS C INNER JOIN tbVehicleType AS V ON C.vid=V.id WHERE CONCAT (C.name,carno,carmodel,address) LIKE '%" + txtSearch.Text + "%'", dbcon.connect());

                dbcon.open();
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    i++;
                    //to add data to the datagridview  from the database
                    dgvCustomer.Rows.Add(i, sdr[0].ToString(), sdr[1].ToString(), sdr[2].ToString(), sdr[3].ToString(), sdr[4].ToString(), sdr[5].ToString(), sdr[6].ToString(), sdr[7].ToString());
                }
                dbcon.close();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, title);
            }
        }

        public int vehicleIdbyName(string str)
        {
            int i = 0;
            cmd = new SqlCommand("SELECT id FROM tbVehicleType WHERE name LIKE '"+ str + "'",dbcon.connect());
            dbcon.open();
            sdr = cmd.ExecuteReader();
            sdr.Read();
            if (sdr.HasRows)
            {
                i = int.Parse(sdr["id"].ToString());
            }
            dbcon.close();
            return i;
        }
        #endregion method

    }
}
