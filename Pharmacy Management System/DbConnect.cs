using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Pharmacy_Management_System
{
    class DbConnect
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        private string con;


        public string connection()
        {
            // Replace "full_path_to_your_MDF_file" with the actual full path to your .mdf file
            string mdfFilePath = @"C:\Users\Andrew\Desktop\projects\Projects_on_resume\Pharmacy_Management_System\Pharmacy Management System\Pharmacy Management System\Pharmacydb.mdf";


            con = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={mdfFilePath};Integrated Security=True;Connect Timeout=30";
            return con;
        }

        public void executeQuery(string sql)
        {
            try
            {
                cn.ConnectionString = connection();
                cn.Open();
                cm = new SqlCommand(sql, cn);
                cm.ExecuteNonQuery();
                cn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
