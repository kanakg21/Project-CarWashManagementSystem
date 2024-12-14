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
    public partial class ServiceModule : Form
    {
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        string title = "Car Wash Management System";
        Service service;
        public ServiceModule(Service ser)
        {
            InitializeComponent();
            service = ser;
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            //only allow digit number
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            //onlu allow one decimal
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "" || txtPrice.Text == "")
                {
                    MessageBox.Show("Required data Field!", "Warning");
                    return; //return to the data field and form
                }
                if (MessageBox.Show("Are you sure you want to register  this Service? ", "Service Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO tbService(name,price)VALUES(@name,@price)", dbcon.connect());

                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@price", txtPrice.Text);

                    dbcon.open();   //TO OPEN CONNECTION
                    cmd.ExecuteNonQuery();
                    dbcon.close();  //TO CLOSE CONNECTION
                    MessageBox.Show("Service has been successfully Registered!", title);
                    Clear();    //to clear data field, after data inserted into the database
                    service.loadService();
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
                if (txtName.Text == "" || txtPrice.Text == "")
                {
                    MessageBox.Show("Required data Field!", "Warning");
                    return; //return to the data field and form
                }
                if (MessageBox.Show("Are you sure you want to update  this Service? ", "Service Updation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE tbService SET name=@name, price=@price WHERE id=@id", dbcon.connect());

                    cmd.Parameters.AddWithValue("@id", lblSid.Text);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@price", txtPrice.Text);

                    dbcon.open();   //TO OPEN CONNECTION
                    cmd.ExecuteNonQuery();
                    dbcon.close();  //TO CLOSE CONNECTION
                    MessageBox.Show("Service has been successfully updated!", title);
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
            txtPrice.Clear();

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }
        #endregion method
    }
}
