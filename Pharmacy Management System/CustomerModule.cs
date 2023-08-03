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
    public partial class CustomerModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        string title = "Pharmacy Management System";

        bool check = false;
        CustomerForm customerForm;
        public CustomerModule(CustomerForm customer)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            customerForm = customer;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CheckField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to register this customer?", "Customer Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("INSERT INTO tbCustomer(name,address,phone,dob,insuranceNum)VALUES(@name,@address,@phone,@dob,@insuranceNum)", cn);
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cm.Parameters.AddWithValue("@dob", dtDob.Value);
                        cm.Parameters.AddWithValue("@insuranceNum", txtInsuranceNumber.Text);

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Customer has been successfully registered!", title);
                        Clear();
                        this.Dispose();
                        customerForm.LoadCustomer();
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
                if (MessageBox.Show("Are you sure you want to update this customer?", "Edit Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbCustomer SET name=@name, address=@address, phone=@phone, dob=@dob, insuranceNum=@insuranceNum WHERE cid=@cid", cn);
                    cm.Parameters.AddWithValue("@cid", lblcid.Text);
                    cm.Parameters.AddWithValue("@name", txtName.Text);
                    cm.Parameters.AddWithValue("@address", txtAddress.Text);
                    cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cm.Parameters.AddWithValue("@dob", dtDob.Value);
                    cm.Parameters.AddWithValue("@insuranceNum", txtInsuranceNumber.Text);

                    cn.Open();
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Customer's data has been successfully updated!", title);
                    Clear();
                    this.Dispose();
                    customerForm.LoadCustomer();
                    this.Dispose();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Method

        public void Clear()
        {
            txtName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtInsuranceNumber.Clear();
            dtDob.Value = DateTime.Now;

            btnUpdate.Enabled = false;
        }

        public void CheckField()
        {
            if (txtName.Text == "" | txtAddress.Text == "" | txtPhone.Text == "" | txtInsuranceNumber.Text == "")
            {
                MessageBox.Show("Required data field", "Warning");
                return;
            }
            if (checkAge(dtDob.Value) < 18)
            {
                MessageBox.Show("Customer is under 18", "Warning");
                return;
            }
            check = true;
        }

        private static int checkAge(DateTime dateofBirth)
        {
            int age = DateTime.Now.Year - dateofBirth.Year;
            if (DateTime.Now.DayOfYear < dateofBirth.DayOfYear)
            {
                age = age - 1;
            }
            return age;
        }
        #endregion Method
    }
}
