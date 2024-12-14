using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CarWashManagementSystem
{
    //to get connection string between application and database
    class dbConnect
    {
        SqlCommand cmd = new SqlCommand();
        private SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=DBCarWash1;Integrated Security=True");

        public SqlConnection connect()
        {
            return con;
        }

        public void open()
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
        }
        public void close()
        {
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
        }

        public void executeQuery(string sql)
        {
            try
            {
                open();
                cmd = new SqlCommand(sql, connect());
                cmd.ExecuteNonQuery();
                close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Car Wash Management system");
            }
        }
    }

    
}
