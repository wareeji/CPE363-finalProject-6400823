﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IncomeAndExpensesTracker_Program
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to close this program?","Confirmation Message" , MessageBoxButtons.YesNo,MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                Application.Exit();
            }
            
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            dashboardForm1.Visible = true;
            categoryForm1.Visible = false;
            incomeForm1.Visible = false ;
            expensesForm1.Visible = false ;

            DashboardForm dForm = dashboardForm1 as DashboardForm;

            if(dForm != null)
            {
                dForm.refreshData();
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            dashboardForm1.Visible = false;
            categoryForm1.Visible = true;
            incomeForm1.Visible = false;
            expensesForm1.Visible = false;

            CategoryForm cForm= categoryForm1 as CategoryForm;

            if (cForm != null)
            {
                cForm.refreshData();
            }
        }

        private void btnIncome_Click(object sender, EventArgs e)
        {
            dashboardForm1.Visible = false;
            categoryForm1.Visible = false;
            incomeForm1.Visible = true;
            expensesForm1.Visible = false;

            IncomeForm iForm = incomeForm1 as IncomeForm;

            if (iForm != null)
            {
                iForm.refreshData();
            }
        }

        private void btnExpenses_Click(object sender, EventArgs e)
        {
            dashboardForm1.Visible = false;
            categoryForm1.Visible = false;
            incomeForm1.Visible = false;
            expensesForm1.Visible = true;

            ExpensesForm eForm = expensesForm1 as ExpensesForm;

            if (eForm != null)
            {
                eForm.refreshData();
            }
        }
    }
}
