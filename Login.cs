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
    public partial class Login : Form
    {
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader sdr;
        string title = "Car Wash Management System";
        public Login()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand("SELECT name FROM tbEmployer WHERE name='"+txtUsername.Text+"' AND password='"+txtPassword.Text+"'",dbcon.connect());
                dbcon.open();
                sdr = cmd.ExecuteReader();
                sdr.Read();
                if (sdr.HasRows)
                {
                    MessageBox.Show("WELCOME " + sdr[0].ToString()+ " | ","ACCESS Granted", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this.Hide();
                    Form1 main = new Form1();
                    main.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid username or Password","Access Denied",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                dbcon.close();
                sdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                txtPassword.UseSystemPasswordChar = false;
            else
                txtPassword.UseSystemPasswordChar = true;
        }
    }
}
