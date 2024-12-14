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
    public partial class Employer : Form
    {
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader sdr;
        string title = "Car Wash Management System";
        public Employer()
        {
            InitializeComponent();
            loadEmployer();     //to call this function, this form starting
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            EmployerModule module = new EmployerModule(this);
            module.btnUpdate.Enabled = false;   //this is for save not for update
            module.ShowDialog();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadEmployer();
        }

        private void dgvEmployer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvEmployer.Columns[e.ColumnIndex].Name;
            if(colName == "Edit")
            {
                //to sent employee data to the employer module
                EmployerModule module = new EmployerModule(this);
                module.lblEid.Text = dgvEmployer.Rows[e.RowIndex].Cells[1].Value.ToString();
                module.txtFullName.Text = dgvEmployer.Rows[e.RowIndex].Cells[2].Value.ToString();
                module.txtPhone.Text = dgvEmployer.Rows[e.RowIndex].Cells[3].Value.ToString();
                module.txtAddress.Text = dgvEmployer.Rows[e.RowIndex].Cells[4].Value.ToString();
                module.dtpDateOfBirth.Text = dgvEmployer.Rows[e.RowIndex].Cells[5].Value.ToString();
                module.rdoMale.Checked = dgvEmployer.Rows[e.RowIndex].Cells[6].Value.ToString() == "Male" ? true:false; //like if condtion
                module.cmbRole.Text = dgvEmployer.Rows[e.RowIndex].Cells[7].Value.ToString();
                module.txtSalary.Text = dgvEmployer.Rows[e.RowIndex].Cells[8].Value.ToString();
                module.txtPassword.Text = dgvEmployer.Rows[e.RowIndex].Cells[9].Value.ToString();

                module.btnSave.Enabled = false;
                module.ShowDialog();
            }
            else if(colName == "Delete")    //if you want to delete  the record to click the delete icon on the datagridview
            {
                try
                {
                    if(MessageBox.Show("Are you sure you want to delete this record?","Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                    {
                        cmd = new SqlCommand("DELETE FROM tbEmployer WHERE id LIKE'" + dgvEmployer.Rows[e.RowIndex].Cells[1].Value.ToString() + "'",dbcon.connect());
                        dbcon.open();
                        cmd.ExecuteNonQuery();
                        dbcon.close();
                        MessageBox.Show("Employer data has been succsessfully removed!",title,MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,title);
                }
            }
        }
        #region method
        //query employer list  data from  the database  to the datagridview
        public void loadEmployer()
        {
            try
            {
                int i = 0;  //to show number for employer list
                dgvEmployer.Rows.Clear();

                cmd = new SqlCommand("SELECT * FROM tbEmployer WHERE CONCAT (name,address,role) LIKE '%" + txtSearch.Text + "%'",dbcon.connect());
                
                dbcon.open();
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    i++;
                    //to add data to the datagridview  from the database
                    dgvEmployer.Rows.Add(i, sdr[0].ToString(), sdr[1].ToString(), sdr[2].ToString(), sdr[3].ToString(),DateTime.Parse( sdr[4].ToString()).ToShortDateString(), sdr[5].ToString(), sdr[6].ToString(), sdr[7].ToString(), sdr[8].ToString());
                }
                dbcon.close();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,title);
            }
        }
        #endregion method

        
    }
}
