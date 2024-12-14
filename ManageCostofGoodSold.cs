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
    public partial class ManageCostofGoodSold : Form
    {
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        string title = "Car Wash Management System";
        setting setting;
        bool check = false;

        public ManageCostofGoodSold(setting sett)
        {
            InitializeComponent();
            setting = sett;
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
                    if (MessageBox.Show("Are you sure you want to register  this cost of good sold? ", "Cost of Good Sold Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("INSERT INTO tbCostofGood(costname,cost,date)VALUES(@costname,@cost,@date)", dbcon.connect());

                        cmd.Parameters.AddWithValue("@costname", txtCostName.Text);
                        cmd.Parameters.AddWithValue("@cost", txtCost.Text);
                        cmd.Parameters.AddWithValue("@date", dtpCog.Value);

                        dbcon.open();   //TO OPEN CONNECTION
                        cmd.ExecuteNonQuery();
                        dbcon.close();  //TO CLOSE CONNECTION
                        MessageBox.Show("Cost of Good Sold has been successfully Registered!", title);
                        Clear();    //to clear data field, after data inserted into the database
                        setting.loadCostofGood();
                    }
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
                checkField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to edit this cost of good sold? ", "Cost of Good Sold Editing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("UPDATE tbCostofGood SET costname=@costname,cost=@cost,date=@date Where id=@id", dbcon.connect());

                        cmd.Parameters.AddWithValue("@id", lblCid.Text);
                        cmd.Parameters.AddWithValue("@costname", txtCostName.Text);
                        cmd.Parameters.AddWithValue("@cost", txtCost.Text);
                        cmd.Parameters.AddWithValue("@date", dtpCog.Value);

                        dbcon.open();   //TO OPEN CONNECTION
                        cmd.ExecuteNonQuery();
                        dbcon.close();  //TO CLOSE CONNECTION
                        MessageBox.Show("Cost of Good Sold has been successfully Updated!", title);
                        Clear();    //to clear data field, after data inserted into the database
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

        private void txtCost_KeyPress_1(object sender, KeyPressEventArgs e)
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

        #region method
        public void checkField()
        {
            if (txtCostName.Text == "" || txtCost.Text == "")
            {
                MessageBox.Show("Required data Field!", "Warning");
                return; //return to the data field and form
            }
            check = true;
        }

        public void Clear()
        {
            txtCostName.Clear();
            txtCost.Clear();
            dtpCog.Value = DateTime.Now;

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }



        #endregion method

        
    }
}
