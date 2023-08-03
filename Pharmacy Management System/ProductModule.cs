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
    public partial class ProductModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        string title = "Pharmacy Management System";

        bool check = false;
        ProductForm productForm;
        public ProductModule(ProductForm form)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            productForm = form;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CheckField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to register this product?", "Product Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("INSERT INTO tbProduct(pname,ptype,pqty,plotnum,pprice)VALUES(@pname,@ptype,@pqty,@plotnum,@pprice)", cn);
                        cm.Parameters.AddWithValue("@pname", txtName.Text);
                        cm.Parameters.AddWithValue("@ptype", txtType.Text);
                        cm.Parameters.AddWithValue("@pqty", int.Parse(txtQty.Text));
                        cm.Parameters.AddWithValue("@plotnum", txtLot.Text);
                        cm.Parameters.AddWithValue("@pprice", double.Parse(txtPrice.Text));

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Product has been successfully registered!", title);
                        Clear();
                        this.Dispose();
                        productForm.LoadProduct();
                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                CheckField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to update this item?", "Edit Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("UPDATE tbProduct SET pname=@pname, ptype=@ptype, pqty=@pqty, plotnum=@plotnum, pprice=@pprice WHERE pcode=@pcode", cn);
                        cm.Parameters.AddWithValue("@pcode", lblPcode.Text);
                        cm.Parameters.AddWithValue("@pname", txtName.Text);
                        cm.Parameters.AddWithValue("@ptype", txtType.Text);
                        cm.Parameters.AddWithValue("@pqty", int.Parse(txtQty.Text));
                        cm.Parameters.AddWithValue("@plotnum", txtLot.Text);
                        cm.Parameters.AddWithValue("@pprice", double.Parse(txtPrice.Text));

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Item data has been successfully updated!", title);
                        Clear();
                        this.Dispose();
                        productForm.LoadProduct();
                        this.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) // only allowing digit number
            {
                e.Handled = true;
            }

            if((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1 )) // only allowing one decimal point
            {
                e.Handled = true;
            }
        }

        #region Method
        public void Clear()
        {
            txtName.Clear();
            txtPrice.Clear();
            txtQty.Clear();
            txtType.Clear();
            txtLot.Clear();

            btnUpdate.Enabled = false;
        }
        public void CheckField()
        {
            if (txtName.Text == "" | txtPrice.Text == "" | txtQty.Text == "" | txtType.Text == "" | txtLot.Text == "")
            {
                MessageBox.Show("Required data field", "Warning");
                return;
            }
            check = true;
        }
        #endregion Method
    }
}
