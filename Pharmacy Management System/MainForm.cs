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
    public partial class MainForm : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        string title = "Pharmacy Management System";
        public MainForm()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            openChildForm(new Dashboard());
            loadDailySale();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            openChildForm(new Dashboard());
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            openChildForm(new CustomerForm());
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            openChildForm(new UserForm());
        }

        private void btnMedicine_Click(object sender, EventArgs e)
        {
            openChildForm(new ProductForm());
        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            openChildForm(new CashForm(this));
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Logout Application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LoginForm login = new LoginForm();
                this.Dispose();
                login.ShowDialog();
            }
        }

        #region Method
        private Form activeForm = null;
        public void openChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            lblTitle.Text = childForm.Text;
            panelChild.Controls.Add(childForm);
            panelChild.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        public void loadDailySale()
        {
            string sdate = DateTime.Now.ToString("yyyyMMdd");
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT ISNULL(SUM(total),0) AS total FROM tbCash WHERE transno LIKE'" + sdate + "%'", cn);
                lblDailySales.Text = double.Parse(cm.ExecuteScalar().ToString()).ToString("#,##0.00");
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }
        }
        #endregion Method
    }
}
