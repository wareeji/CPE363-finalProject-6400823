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
    public partial class ExpensesForm : UserControl
    {
        String stringConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\wareeji\Documents\MoneyTrackerDB.mdf;Integrated Security=True;Connect Timeout=30";

        public void displayExpensesData()
        {
            ExpensesData eData = new ExpensesData();
            List<ExpensesData> listData = eData.ExpensesListData();

            dataGridViewExpenses.DataSource = listData;
        }


        public ExpensesForm()
        {
            InitializeComponent();

            displayExpensesData();
            displayCategorylist();
        }



        public void displayCategorylist()
        {
            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                conn.Open();

                string selectData = "SELECT category FROM categories WHERE type = @type AND status = @status";

                using (SqlCommand cmd = new SqlCommand(selectData, conn))
                {
                    cmd.Parameters.AddWithValue("@type", "Expense");
                    cmd.Parameters.AddWithValue("@status", "Active");

                    cbbxCategoryTpyeExpenses.Items.Clear();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        cbbxCategoryTpyeExpenses.Items.Add(reader["category"].ToString());
                    }
                }
            }
        }


        public void clearFields()
        {
            cbbxCategoryTpyeExpenses.SelectedIndex = -1;
            tbxItemExpenses.Text = "";
            tbxExpensesCost.Text = "";
            tbxDescExpenses.Text = "";
        }
        private void btnClearExpenses_Click(object sender, EventArgs e)
        {
            clearFields();
        }


        private void btnAddExpenses_Click(object sender, EventArgs e)
        {
            if (tbxItemExpenses.Text == "" || tbxExpensesCost.Text == "" || cbbxCategoryTpyeExpenses.SelectedIndex == -1 || tbxDescExpenses.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(stringConnection))
                {
                    conn.Open();

                    String insertData = "INSERT INTO expenses (category , item , cost ,description, date_expense , date_insert )"
                                        + "VALUES (@cat , @item , @cost , @description , @date_expense , @date_insert )";

                    using (SqlCommand cmd = new SqlCommand(insertData, conn))
                    {
                        cmd.Parameters.AddWithValue("@cat", cbbxCategoryTpyeExpenses.SelectedItem);
                        cmd.Parameters.AddWithValue("@item", tbxItemExpenses.Text.Trim());
                        cmd.Parameters.AddWithValue("@cost", tbxExpensesCost.Text.Trim());
                        cmd.Parameters.AddWithValue("@description", tbxDescExpenses.Text);
                        cmd.Parameters.AddWithValue("@date_expense", dateExpenses.Value);

                        DateTime today = DateTime.Today;
                        cmd.Parameters.AddWithValue("@date_insert", today);

                        cmd.ExecuteNonQuery();
                        clearFields();

                        MessageBox.Show("Added seccessfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    conn.Close();
                }
            }
            displayExpensesData();

        }


        private int getID = 0;
        private void dataGridViewExpenses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridViewExpenses.Rows[e.RowIndex];

                getID = Convert.ToInt32(row.Cells[0].Value);
                cbbxCategoryTpyeExpenses.SelectedItem = row.Cells[1].Value.ToString();
                tbxItemExpenses.Text = row.Cells[2].Value.ToString();
                tbxExpensesCost.Text = row.Cells[3].Value.ToString();
                tbxDescExpenses.Text = row.Cells[4].Value.ToString();
                dateExpenses.Value = Convert.ToDateTime(row.Cells[5].Value.ToString());
            }
        }

        private void btnUpdateExpenses_Click(object sender, EventArgs e)
        {
            if (tbxItemExpenses.Text == "" || tbxExpensesCost.Text == "" || cbbxCategoryTpyeExpenses.SelectedIndex == -1 || tbxDescExpenses.Text == "")
            {
                MessageBox.Show("Please select item first", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to Update ID :" + getID + " ?", "Confirmation Message"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(stringConnection))
                    {
                        conn.Open();

                        String updateData = "UPDATE expenses SET category = @cat , item = @item ,"
                                             + "cost = @cost ,description = @description , date_expense = @date_expense WHERE id = @id";

                        using (SqlCommand cmd = new SqlCommand(updateData, conn))
                        {
                            cmd.Parameters.AddWithValue("@cat", cbbxCategoryTpyeExpenses.SelectedItem);
                            cmd.Parameters.AddWithValue("@item", tbxItemExpenses.Text.Trim());
                            cmd.Parameters.AddWithValue("@cost", tbxExpensesCost.Text.Trim());
                            cmd.Parameters.AddWithValue("@description", tbxDescExpenses.Text);
                            cmd.Parameters.AddWithValue("@date_expense", dateExpenses.Value);
                            cmd.Parameters.AddWithValue("@id", getID);

                            cmd.ExecuteNonQuery();
                            clearFields();

                            MessageBox.Show("Updated seccessfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        conn.Close();
                    }
                }
            }
            displayExpensesData();
        }

        private void btnDeleteExpenses_Click(object sender, EventArgs e)
        {
            if (tbxItemExpenses.Text == "" || tbxExpensesCost.Text == "" || cbbxCategoryTpyeExpenses.SelectedIndex == -1 || tbxDescExpenses.Text == "")
            {
                MessageBox.Show("Please select item first", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to Delete ID :" + getID + " ?", "Confirmation Message"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(stringConnection))
                    {
                        conn.Open();

                        String deleteData = "DELETE FROM expenses WHERE id = @id";

                        using (SqlCommand cmd = new SqlCommand(deleteData, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", getID);

                            cmd.ExecuteNonQuery();
                            clearFields();

                            MessageBox.Show("Deleted seccessfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        conn.Close();
                    }
                }
            }
            displayExpensesData();
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

            displayExpensesData();
            displayCategorylist();

        }
    }
  
}
