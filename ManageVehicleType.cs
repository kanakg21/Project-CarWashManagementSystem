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
    public partial class ManageVehicleType : Form
    {
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        string title = "Car Wash Management System";
        setting setting;

        public ManageVehicleType(setting sett)
        {
            InitializeComponent();
            setting = sett;
            cmbClass.SelectedIndex = 0;
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Required Vehicle type name!", "Warning");
                    return; //return to the data field and form
                }
                    if (MessageBox.Show("Are you sure you want to register  this vehicle type? ", "Vehicle Type Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("INSERT INTO tbVehicleType(name,class)VALUES(@name,@class)", dbcon.connect());

                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@class", cmbClass.Text);

                        dbcon.open();   //TO OPEN CONNECTION
                        cmd.ExecuteNonQuery();
                        dbcon.close();  //TO CLOSE CONNECTION
                        MessageBox.Show("Employee has been successfully Registered!", title);
                        Clear();    //to clear data field, after data inserted into the database
                        setting.loadVehicleType();
                    }
                    
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Required Vehicle type name!", "Warning");
                    return; //return to the data field and form
                }
                if (MessageBox.Show("Are you sure you want to edit  this vehicle type? ", "Vehicle Type Editing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE tbVehicleType SET name=@name,class=@class WHERE id=@id", dbcon.connect());

                    cmd.Parameters.AddWithValue("@id", lblVid.Text);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@class", cmbClass.Text);

                    dbcon.open();   //TO OPEN CONNECTION
                    cmd.ExecuteNonQuery();
                    dbcon.close();  //TO CLOSE CONNECTION
                    MessageBox.Show("Employee has been successfully Edited!", title);
                    Clear();    //to clear data field, after data inserted into the database
                    this.Dispose();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        #region method

        public void Clear()
        {
            txtName.Clear();
            cmbClass.SelectedIndex = 0;

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }
        #endregion method
    }
}
