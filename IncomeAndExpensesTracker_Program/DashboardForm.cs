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


namespace IncomeAndExpensesTracker_Program
{
    public partial class DashboardForm : UserControl
    {
        String stringConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\wareeji\Documents\MoneyTrackerDB.mdf;Integrated Security=True;Connect Timeout=30";

        public DashboardForm()
        {
            InitializeComponent();

            todayIncome();
            yesterdayIncome();
            ThisMonthIncome();
            ThisYearIncome();
            totalIncome();

            todayExpense();
            yesterdayExpense();
            ThisMonthExpense();
            ThisYearExpense();
            totalExpense();
        }

        public void refreshData()
        {
            if(InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

            todayIncome();
            yesterdayIncome();
            ThisMonthIncome();
            ThisYearIncome();
            totalIncome();

            todayExpense();
            yesterdayExpense();
            ThisMonthExpense();
            ThisYearExpense();
            totalExpense();
        }

        //income
        public void todayIncome()
        {
            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                conn.Open();

                string query = "SELECT SUM(income) FROM income WHERE date_income = @date_income";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    DateTime today = DateTime.Today;
                    cmd.Parameters.AddWithValue("@date_income", today);

                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        decimal totalTodayIcome = Convert.ToDecimal(result);    
                        lbTodayIncome.Text = " ฿ " + totalTodayIcome.ToString("0.00");
                    }
                    else
                    {
                        lbTodayIncome.Text = " ฿ 0.00 " ;
                    }
                }
            }
        }

        public void yesterdayIncome()
        {
            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                conn.Open();

                string query = "SELECT SUM(income) FROM income WHERE CONVERT(DATE,date_income) = DATEADD(day , DATEDIFF(day,0,GETDATE()),-1)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        decimal totalYesterdayIcome = Convert.ToDecimal(result);
                        lbYesterdayIncome.Text = " ฿ " + totalYesterdayIcome.ToString("0.00");
                    }
                    else
                    {
                        lbYesterdayIncome.Text = " ฿ 0.00 ";
                    }
                }
            }
        }


        public void ThisMonthIncome()
        {
            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                conn.Open();

                DateTime today = DateTime.Now.Date;
                DateTime startMonth = new DateTime(today.Year, today.Month,1);
                DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);

                string query = "SELECT SUM(income) FROM income WHERE date_income >= @startMonth AND date_income <= @endMonth";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@startMonth",startMonth);
                    cmd.Parameters.AddWithValue("@endMonth", endMonth);

                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        decimal totalThisMonthIcome = Convert.ToDecimal(result);
                        lbThisMonthIncome.Text = " ฿ " + totalThisMonthIcome.ToString("0.00");
                    }
                    else
                    {
                        lbThisMonthIncome.Text = " ฿ 0.00 ";
                    }
                }
            }
        }

        public void ThisYearIncome()
        {
            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                conn.Open();

                DateTime today = DateTime.Now.Date;
                DateTime startYear = new DateTime(today.Year,1,1);
                DateTime endYear = startYear.AddYears(1).AddDays(-1);

                string query = "SELECT SUM(income) FROM income WHERE date_income >= @startYear AND date_income <= @endYear";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@startYear", startYear);
                    cmd.Parameters.AddWithValue("@endYear", endYear);

                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        decimal totalThisYearIcome = Convert.ToDecimal(result);
                        lbThisYearIncome.Text = " ฿ " + totalThisYearIcome.ToString("0.00");
                    }
                    else
                    {
                        lbThisYearIncome.Text = " ฿ 0.00 ";
                    }
                }
            }
        }

        public void totalIncome()
        {
            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                conn.Open();

                string query = "SELECT SUM(income) FROM income";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        decimal totalIncome = Convert.ToDecimal(result);
                        lbTotalIncome.Text = " ฿ " + totalIncome.ToString("0.00");
                    }
                    else
                    {
                        lbTotalIncome.Text = " ฿ 0.00 ";
                    }
                }
            }

        }


        //Expense
        public void todayExpense()
        {
            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                conn.Open();

                string query = "SELECT SUM(cost) FROM expenses WHERE date_expense = @date_expense";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    DateTime today = DateTime.Today;
                    cmd.Parameters.AddWithValue("@date_expense", today);

                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        decimal totalTodayExpense = Convert.ToDecimal(result);
                        lbTodayExpense.Text = " ฿ " + totalTodayExpense.ToString("0.00");
                    }
                    else
                    {
                        lbTodayExpense.Text = " ฿ 0.00 ";
                    }
                }
            }
        }

        public void yesterdayExpense()
        {
            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                conn.Open();

                string query = "SELECT SUM(cost) FROM expenses WHERE CONVERT(DATE,date_expense) = DATEADD(day , DATEDIFF(day,0,GETDATE()),-1)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        decimal totalYesterdayExpense = Convert.ToDecimal(result);
                        lbYesterdayExpense.Text = " ฿ " + totalYesterdayExpense.ToString("0.00");
                    }
                    else
                    {
                        lbYesterdayExpense.Text = " ฿ 0.00 ";
                    }
                }
            }
        }


        public void ThisMonthExpense()
        {
            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                conn.Open();

                DateTime today = DateTime.Now.Date;
                DateTime startMonth = new DateTime(today.Year, today.Month, 1);
                DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);

                string query = "SELECT SUM(cost) FROM expenses WHERE date_expense >= @startMonth AND date_expense <= @endMonth";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@startMonth", startMonth);
                    cmd.Parameters.AddWithValue("@endMonth", endMonth);

                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        decimal totalThisMonthExpense = Convert.ToDecimal(result);
                        lbThisMonthExpense.Text = " ฿ " + totalThisMonthExpense.ToString("0.00");
                    }
                    else
                    {
                        lbThisMonthExpense.Text = " ฿ 0.00 ";
                    }
                }
            }
        }

        public void ThisYearExpense()
        {
            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                conn.Open();

                DateTime today = DateTime.Now.Date;
                DateTime startYear = new DateTime(today.Year, 1, 1);
                DateTime endYear = startYear.AddYears(1).AddDays(-1);

                string query = "SELECT SUM(cost) FROM expenses WHERE date_expense >= @startYear AND date_expense <= @endYear";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@startYear", startYear);
                    cmd.Parameters.AddWithValue("@endYear", endYear);

                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        decimal totalThisYearExpense = Convert.ToDecimal(result);
                        lbThisYearExpense.Text = " ฿ " + totalThisYearExpense.ToString("0.00");
                    }
                    else
                    {
                        lbThisYearExpense.Text = " ฿ 0.00 ";
                    }
                }
            }
        }

        public void totalExpense()
        {
            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                conn.Open();

                string query = "SELECT SUM(cost) FROM expenses";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        decimal totalExpense = Convert.ToDecimal(result);
                        lbTotalExpense.Text = " ฿ " + totalExpense.ToString("0.00");
                    }
                    else
                    {
                        lbTotalExpense.Text = " ฿ 0.00 ";
                    }
                }
            }

        }
    }
}