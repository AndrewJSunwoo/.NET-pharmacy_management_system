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
    public partial class CashProduct : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        string title = "Pharmacy Management System";
        SqlDataReader dr;
        CashForm cashForm;
        public string uname;
        public CashProduct(CashForm form)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            LoadProduct();
            cashForm = form;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow dr in dgvProduct.Rows)
            {
                bool chkbox = Convert.ToBoolean(dr.Cells["Select"].Value);
                if(chkbox)
                {
                    try
                    {
                        cm = new SqlCommand("INSERT INTO tbCash(transno, pcode, pname, qty, price, cashier) VALUES (@transno, @pcode, @pname, @qty, @price, @cashier)", cn);
                        cm.Parameters.AddWithValue("@transno", cashForm.lblTransno.Text);
                        cm.Parameters.AddWithValue("@pcode", dr.Cells[1].Value.ToString());
                        cm.Parameters.AddWithValue("@pname", dr.Cells[2].Value.ToString());
                        cm.Parameters.AddWithValue("@qty", 1);
                        cm.Parameters.AddWithValue("@price", Convert.ToDouble(dr.Cells[5].Value.ToString()));
                        cm.Parameters.AddWithValue("@cashier", uname);

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                    }
                    catch(Exception ex)
                    {
                        cn.Close();
                        MessageBox.Show(ex.Message, title);
                    }
                }
            }
            cashForm.loadCash();
            this.Dispose();
        }

        #region Method
        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT pcode, pname, ptype,plotnum, pprice FROM tbProduct WHERE CONCAT(pname,ptype,plotnum) LIKE '%" + txtSearch.Text + "%' AND pqty > " + 0 +"", cn);
            cn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
            }
            dr.Close();
            cn.Close();
        }
        #endregion Method
    }
}
