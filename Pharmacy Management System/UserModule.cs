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
    public partial class UserModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        string title = "Pharmacy Management System";

        bool check = false;
        UserForm userForm;
        public UserModule(UserForm user)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            userForm = user;
            cbRole.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CheckField();
                if(check) { 
                    if(MessageBox.Show("Are you sure you want to register this user?", "User Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("INSERT INTO tbUser(name,address,phone,role,dob,password,licenseNum)VALUES(@name,@address,@phone,@role,@dob,@password,@licenseNum)", cn);
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cm.Parameters.AddWithValue("@role", cbRole.Text);
                        cm.Parameters.AddWithValue("@dob", dtDob.Value);
                        cm.Parameters.AddWithValue("@password", txtPassword.Text);
                        cm.Parameters.AddWithValue("@licenseNum", txtLicenseNumber.Text);

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("User has been successfully registered!", title);
                        Clear();
                        this.Dispose();
                        userForm.LoadUser();
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
                if (MessageBox.Show("Are you sure you want to update this record?", "Edit Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbUser SET name=@name, address=@address, phone=@phone, role=@role, dob=@dob, password=@password, licenseNum=@licenseNum WHERE id=@id", cn);
                    cm.Parameters.AddWithValue("@id", lbluid.Text);
                    cm.Parameters.AddWithValue("@name", txtName.Text);
                    cm.Parameters.AddWithValue("@address", txtAddress.Text);
                    cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cm.Parameters.AddWithValue("@role", cbRole.Text);
                    cm.Parameters.AddWithValue("@dob", dtDob.Value);
                    cm.Parameters.AddWithValue("@password", txtPassword.Text);
                    cm.Parameters.AddWithValue("@licenseNum", txtLicenseNumber.Text);

                    cn.Open();
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("User's data has been successfully updated!", title);
                    Clear();
                    userForm.LoadUser();
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

        private void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbRole.Text == "Pharmacist")
            {
                this.Height = 564;
                lblLicense.Visible = true;
                txtLicenseNumber.Visible = true;
            }
            else
            {
                lblLicense.Visible = false;
                txtLicenseNumber.Visible = false;
                this.Height = 564 - 26;
            }
        }

        #region Method

        public void Clear()
        {
            txtName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtPassword.Clear();
            cbRole.SelectedIndex = 0;
            dtDob.Value = DateTime.Now;

            btnUpdate.Enabled = false;
        }

        public void CheckField()
        {
            if(txtName.Text == "" | txtAddress.Text == "")
            {
                MessageBox.Show("Required data field", "Warning");
                return;
            }
            if(checkAge(dtDob.Value) < 18)
            {
                MessageBox.Show("User is under 18", "Warning");
                return;
            }
            check = true;
        }

        private static int checkAge(DateTime dateofBirth)
        {
            int age = DateTime.Now.Year - dateofBirth.Year;
            if(DateTime.Now.DayOfYear < dateofBirth.DayOfYear)
            {
                age = age - 1;
            }
            return age;
        }
        #endregion Method
    }
}
