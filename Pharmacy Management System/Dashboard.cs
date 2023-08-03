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

namespace Pharmacy_Management_System
{ 
    public partial class Dashboard : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        string title = "Pharmacy Management System";

        public Dashboard()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
        }

        #region method
        public int extractData(string sql)
        {
            int data = 0;
            try
            {
                cn.Open();
                cm = new SqlCommand(sql, cn);
                data = int.Parse(cm.ExecuteScalar().ToString());
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }
            return data;
        }
        #endregion method

        private void Dashboard_Load(object sender, EventArgs e)
        {
            lblTotalPatients.Text = extractData("SELECT COUNT(*) AS total_customers FROM tbCustomer").ToString();
            lblDrugsOutofStock.Text = extractData("SELECT COUNT(*) AS out_of_stock_count FROM tbProduct WHERE pqty = 0; ").ToString();
            lblStockedDrugs.Text = extractData("SELECT COUNT(*) AS stocked_drugs FROM tbProduct WHERE pqty > 0;").ToString();
            lblPharmacists.Text = extractData("SELECT COUNT(*) AS pharmacist_count FROM tbUser WHERE role = 'pharmacist'").ToString();
        }
    }
}
