﻿using System;
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
    public partial class CashCustomer : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        string title = "Pharmacy Management System";
        SqlDataReader dr;
        CashForm cashForm;
        public CashCustomer(CashForm form)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection()); 
            cashForm = form;
            LoadCustomer();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (colName == "Choice")
            {
                dbcon.executeQuery("UPDATE tbCash SET cid=" + dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString() + " WHERE transno=" + cashForm.lblTransno.Text + "");
                cashForm.loadCash();
                this.Dispose();
            }
        }

        #region Method
        public void LoadCustomer()
        {
            try
            {
                int i = 0;
                dgvCustomer.Rows.Clear();
                cm = new SqlCommand("SELECT cid,name,phone FROM tbCustomer WHERE name LIKE '%" + txtSearch.Text + "%'", cn);
                cn.Open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
        }
        #endregion Method
    }
}