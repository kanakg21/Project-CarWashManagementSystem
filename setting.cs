using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CarWashManagementSystem
{
    public partial class setting : Form
    {
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader sdr;
        string title = "Car Wash Management System";
        bool hasDetail = false;
        public setting()
        {
            InitializeComponent();
            loadVehicleType();
            loadCostofGood();
            loadCompany();
        }

        #region VehicleType

        public void loadVehicleType()
        {
            try
            {
                int i = 0;  //to show number for Vehicle list
                dgvVehicle.Rows.Clear();

                cmd = new SqlCommand("SELECT * FROM tbVehicleType WHERE CONCAT (name,class) LIKE '%" + txtSearchVt.Text + "%'", dbcon.connect());

                dbcon.open();
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    i++;
                    //to add data to the datagridview  from the database
                    dgvVehicle.Rows.Add(i, sdr[0].ToString(), sdr[1].ToString(), sdr[2].ToString());
                }
                dbcon.close();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, title);
            }
        }



        private void txtSearchVt_TextChanged(object sender, EventArgs e)
        {
            loadVehicleType();
        }

        private void btnaddVechicle_Click_1(object sender, EventArgs e)
        {
            ManageVehicleType module = new ManageVehicleType(this);
            module.btnUpdate.Enabled = false;
            module.ShowDialog();
        }

        private void dgvVehicle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvVehicle.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                //to sent vehicle data to the Vehicle  module
                ManageVehicleType module = new ManageVehicleType(this);
                module.lblVid.Text = dgvVehicle.Rows[e.RowIndex].Cells[1].Value.ToString();
                module.txtName.Text = dgvVehicle.Rows[e.RowIndex].Cells[2].Value.ToString();
                module.cmbClass.Text = dgvVehicle.Rows[e.RowIndex].Cells[3].Value.ToString();


                module.btnSave.Enabled = false;
                module.ShowDialog();
            }
            else if (colName == "Delete")    //if you want to delete  the record to click the delete icon on the datagridview
            {
                try
                {
                    if (MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("DELETE FROM tbVehicleType WHERE id LIKE'" + dgvVehicle.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", dbcon.connect());
                        dbcon.open();
                        cmd.ExecuteNonQuery();
                        dbcon.close();
                        MessageBox.Show("Vehicle Type data has been succsessfully removed!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, title);
                }
            }
            loadVehicleType(); // reload the vehicle list after  update the record
        }


        #endregion VehicleType

        #region CostofGoodSold
        private void picAddCostofGood_Click(object sender, EventArgs e)
        {
            ManageCostofGoodSold module = new ManageCostofGoodSold(this);
            module.btnUpdate.Enabled = false;
            module.ShowDialog();
        }

        private void txtCog_TextChanged(object sender, EventArgs e)
        {
            loadCostofGood();
        }

        private void dgvCostofGoodSold_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCostofGoodSold.Columns[e.ColumnIndex].Name;
            if (colName == "EditCoG")
            {
                //to sent cost of good sold data to the manage cost of good sold module
                ManageCostofGoodSold module = new ManageCostofGoodSold(this);
                module.lblCid.Text = dgvCostofGoodSold.Rows[e.RowIndex].Cells[1].Value.ToString();
                module.txtCostName.Text = dgvCostofGoodSold.Rows[e.RowIndex].Cells[2].Value.ToString();
                module.txtCost.Text = dgvCostofGoodSold.Rows[e.RowIndex].Cells[3].Value.ToString();
                module.dtpCog.Text = dgvCostofGoodSold.Rows[e.RowIndex].Cells[4].Value.ToString();



                module.btnSave.Enabled = false;
                module.ShowDialog();
            }
            else if (colName == "DeleteCoG")    //if you want to delete  the record to click the delete icon on the datagridview
            {
                try
                {
                    if (MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("DELETE FROM tbCostofGood WHERE id LIKE'" + dgvCostofGoodSold.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", dbcon.connect());
                        dbcon.open();
                        cmd.ExecuteNonQuery();
                        dbcon.close();
                        MessageBox.Show("Cost of Good sold data has been succsessfully removed!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, title);
                }
            }
            loadCostofGood(); // reload the Cost of Good sold list after edit and update the record
        }

        public void loadCostofGood()
        {
            try
            {
                int i = 0;  //to show number for Cost of Good sold list
                dgvCostofGoodSold.Rows.Clear();

                cmd = new SqlCommand("SELECT * FROM tbCostofGood WHERE CONCAT (costname,date) LIKE '%" + txtCog.Text + "%'", dbcon.connect());

                dbcon.open();
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    i++;
                    //to add data to the datagridview  from the database
                    dgvCostofGoodSold.Rows.Add(i, sdr[0].ToString(), sdr[1].ToString(), sdr[2].ToString(),DateTime.Parse(sdr[3].ToString()).ToShortDateString());
                }
                dbcon.close();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, title);
            }
        }
        #endregion CostofGoodSold


        #region Company Details
        //frst we need to load the data from the database
        public void loadCompany()
        {
            try
            {
                dbcon.open();
                cmd = new SqlCommand("SELECT * FROM tbCompany", dbcon.connect());
                sdr = cmd.ExecuteReader();
                sdr.Read();
                if (sdr.HasRows)
                {
                    hasDetail = true;
                    txtComName.Text = sdr["name"].ToString();
                    txtComAddr.Text = sdr["address"].ToString();
                }
                else
                {
                    txtComName.Clear();
                    txtComAddr.Clear();
                }
                sdr.Close();
                dbcon.close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
            
        }
        

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Save Company details?", "Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    //now we create a function for execute query only one line
                    if (hasDetail)
                    {
                        dbcon.executeQuery("UPDATE tbCompany SET name='" + txtComName.Text + "',address='" + txtComAddr.Text + "'");
                    }
                    else
                    {
                        dbcon.executeQuery("INSERT INTO tbCompany (name,address)VALUES('" + txtComName.Text + "','" + txtComAddr.Text + "')");
                    }
                    MessageBox.Show("Company detail has been successfully saved!", "Save Record",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            catch ( Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtComName.Clear();
            txtComAddr.Clear();
        }
        #endregion Company Details
    }
}
