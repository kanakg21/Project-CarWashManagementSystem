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
    public partial class EmployerModule : Form
    {
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        string title = "Car Wash Management System";
        bool check = false;
        Employer employer;
        public EmployerModule(Employer emp)
        {
            InitializeComponent();
            employer = emp;
            cmbRole.SelectedIndex = 3;   //set worker as default
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //to insert employer data in the database
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                checkField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to register  this employer? ", "Employer Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("INSERT INTO tbEmployer(name,phone,address,dob,gender,role,salary,password)VALUES(@name,@phone,@address,@dob,@gender,@role,@salary,@password)", dbcon.connect());

                        cmd.Parameters.AddWithValue("@name", txtFullName.Text);
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@dob", dtpDateOfBirth.Value);
                        cmd.Parameters.AddWithValue("@gender", rdoMale.Checked ? "Male" : "Female");    //like if condition
                        cmd.Parameters.AddWithValue("@role", cmbRole.Text);
                        cmd.Parameters.AddWithValue("@salary", txtSalary.Text);
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                        dbcon.open();   //TO OPEN CONNECTION
                        cmd.ExecuteNonQuery();
                        dbcon.close();  //TO CLOSE CONNECTION
                        MessageBox.Show("Employee has been successfully Registered!", title);
                        check = false;
                        Clear();    //to clear data field, after data inserted into the database
                        employer.loadEmployer();    //refresh the employer list after insert data in the table
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,title);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                checkField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to edit this record? ", "Employer Editing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("UPDATE tbEmployer SET name=@name,phone=@phone,address=@address,dob=@dob,gender=@gender,role=@role,salary=@salary,password=@password WHERE id=@id", dbcon.connect());

                        cmd.Parameters.AddWithValue("@id", lblEid.Text);
                        cmd.Parameters.AddWithValue("@name", txtFullName.Text);
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@dob", dtpDateOfBirth.Value);
                        cmd.Parameters.AddWithValue("@gender", rdoMale.Checked ? "Male" : "Female");    //like if condition
                        cmd.Parameters.AddWithValue("@role", cmbRole.Text);
                        cmd.Parameters.AddWithValue("@salary", txtSalary.Text);
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                        dbcon.open();   //TO OPEN CONNECTION
                        cmd.ExecuteNonQuery();
                        dbcon.close();  //TO CLOSE CONNECTION
                        MessageBox.Show("Employee has been successfully Registered!", title);
                        Clear();    //to clear data field, after data inserted into the database
                        this.Dispose();
                        employer.loadEmployer();
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
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
        }

        //to create a function  for clear  all field
        #region method
        public void Clear()
        {
            txtFullName.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            //dtpDateOfBirth.ResetText();
            dtpDateOfBirth.Value = DateTime.Now;
            cmbRole.SelectedIndex = 3;  //default is worker
            rdoMale.Checked = false;
            rdoFemale.Checked = false;
            txtSalary.Clear();
            txtPassword.Clear();

        }
        
        //to check the data field
        public void checkField()
        {
            if(txtAddress.Text == " "|| txtFullName.Text == " "|| txtPhone.Text == "" || txtSalary.Text == "")
            {
                MessageBox.Show("Required data Field!", "Warning");
                return;     //return to the datafield and form
            }

            if (CheckAge(dtpDateOfBirth.Value)<18)
            {
                MessageBox.Show("Employer is under 18!", "Warning");
                return;
            }
            check = true;
        }

        //to check the age  and calculate for under 18
        private static  int CheckAge(DateTime dateofBirth)
        {
            int age = DateTime.Now.Year - dateofBirth.Year;
            if(DateTime.Now.DayOfYear < dateofBirth.DayOfYear)
            {
                age = age - 1;
            }
            return age;
        }
        #endregion method

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            //only allow digit number
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            //onlu allow one decimal
            if((e.KeyChar=='.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbRole.Text=="Supervisor" || cmbRole.Text == "Worker")
            {
                txtPassword.Clear();
                lblPassword.Visible = false;    // to hide password label and textbox
                txtPassword.Visible = false;
                //this.Height = 463 - 30;
            }
            else
            {
                lblPassword.Visible = true;    // to unhide password label and textbox
                txtPassword.Visible = true;
                //this.Height = 463;
            }
        }
    }
}
