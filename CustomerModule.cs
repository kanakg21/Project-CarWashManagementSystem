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
    public partial class CustomerModule : Form
    {
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        string title = "Car Wash Management System";
        bool check = false;
        public int vid = 0;
        Customer customer;

        public CustomerModule(Customer cust)
        {
            InitializeComponent();
            customer = cust;
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                checkField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to register  this Customer? ", "Customer Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("INSERT INTO tbCustomer(vid,name,phone,carno,carmodel,address,points)VALUES(@vid,@name,@phone,@carno,@carmodel,@address,@points)", dbcon.connect());

                        cmd.Parameters.AddWithValue("@vid", cmbCarType.SelectedValue);  //to save id number of vehicle type
                        cmd.Parameters.AddWithValue("@name", txtFullName.Text);
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@carno", txtCarno.Text);
                        cmd.Parameters.AddWithValue("@carmodel", txtCarModel.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@points", udPoints.Text);

                        dbcon.open();   //TO OPEN CONNECTION
                        cmd.ExecuteNonQuery();
                        dbcon.close();  //TO CLOSE CONNECTION
                        MessageBox.Show("Customer has been successfully Registered!", title);
                        check = false;
                        Clear();    //to clear data field, after data inserted into the database
                    }
                }
                customer.loadCustomer();
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
                checkField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to update  this Customer? ", "Customer Updation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("UPDATE tbCustomer SET  vid=@vid, name=@name, phone=@phone, carno=@carno, carmodel=@carmodel, address=@address, points=@points WHERE id=@id", dbcon.connect());

                        cmd.Parameters.AddWithValue("@id", lblCid.Text);
                        cmd.Parameters.AddWithValue("@vid", cmbCarType.SelectedValue);  //to save id number of vehicle type
                        cmd.Parameters.AddWithValue("@name", txtFullName.Text);
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@carno", txtCarno.Text);
                        cmd.Parameters.AddWithValue("@carmodel", txtCarModel.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@points", udPoints.Text);

                        dbcon.open();   //TO OPEN CONNECTION
                        cmd.ExecuteNonQuery();
                        dbcon.close();  //TO CLOSE CONNECTION
                        MessageBox.Show("Customer has been successfully Updated!", title);
                        this.Dispose();
                    }
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

        private void CustomerModule_Load(object sender, EventArgs e)
        {
            //to add ehicle list in the combobox
            cmbCarType.DataSource = vehicleType();
            cmbCarType.DisplayMember = "name";
            cmbCarType.ValueMember = "id";
            if (vid > 0)
            {
                cmbCarType.SelectedValue = vid;
            }
        }

        #region method
        //to create a function vehicle type
        public DataTable vehicleType()
        {
            cmd = new SqlCommand("SELECT * FROM tbVehicleType", dbcon.connect());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable datatable = new DataTable();

            adapter.SelectCommand = cmd;
            adapter.Fill(datatable);

            return datatable;
        }

        //to create a function for data field
        public void Clear()
        {
            txtAddress.Clear();
            txtCarModel.Clear();
            txtCarno.Clear();
            txtFullName.Clear();
            txtPhone.Clear();

            cmbCarType.SelectedIndex = 0;
            udPoints.Value = 0;

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        public void checkField()
        {
            if (txtAddress.Text == " " || txtFullName.Text == " " || txtPhone.Text == "" || txtCarno.Text == ""|| txtCarModel.Text=="")
            {
                MessageBox.Show("Required data Field!", "Warning");
                return;     //return to the datafield and form
            }

            check = true;
        }
        #endregion method


    }
}
