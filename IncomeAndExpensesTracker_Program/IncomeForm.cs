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
    public partial class IncomeForm : UserControl
    {

        String stringConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\wareeji\Documents\MoneyTrackerDB.mdf;Integrated Security=True;Connect Timeout=30";

        public void displayIncomeData()
        {
            IncomeData iData = new IncomeData();
            List<IncomeData> listData = iData.IncomeListData();

            dataGridViewIncome.DataSource = listData;   
        }



        public void displayCategorylist()
        {
            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                conn.Open();

                string selectData = "SELECT category FROM categories WHERE type = @type AND status = @status";

                using (SqlCommand cmd = new SqlCommand(selectData, conn))
                {
                    cmd.Parameters.AddWithValue("@type", "Income");
                    cmd.Parameters.AddWithValue("@status", "Active");

                    cbbxCategoryTpyeIncome.Items.Clear();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        cbbxCategoryTpyeIncome.Items.Add(reader["category"].ToString());
                    }
                }
            }
        }



        public IncomeForm()
        {
            InitializeComponent();

            displayIncomeData();
            displayCategorylist();
        }



        public void clearFields()
        {
            cbbxCategoryTpyeIncome.SelectedIndex = -1;
            tbxItemIncome.Text = "";
            tbxIncome.Text = "";
            tbxDescIncome.Text = "";
        }
        private void btnClearIncome_Click(object sender, EventArgs e)
        {
            clearFields();
        }



        private void btnAddIncome_Click(object sender, EventArgs e)
        {
            if (tbxItemIncome.Text == "" || tbxIncome.Text == "" || cbbxCategoryTpyeIncome.SelectedIndex == -1 || tbxDescIncome.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(stringConnection))
                {
                    conn.Open();

                    String insertData = "INSERT INTO income (category , item , income ,description, date_income , date_insert )" 
                                        + "VALUES (@cat , @item , @income , @description , @date_income , @date_insert )";

                    using (SqlCommand cmd = new SqlCommand(insertData, conn))
                    {
                        cmd.Parameters.AddWithValue("@cat", cbbxCategoryTpyeIncome.SelectedItem);
                        cmd.Parameters.AddWithValue("@item", tbxItemIncome.Text.Trim());
                        cmd.Parameters.AddWithValue("@income", tbxIncome.Text.Trim());
                        cmd.Parameters.AddWithValue("@description", tbxDescIncome.Text);
                        cmd.Parameters.AddWithValue("@date_income", dateIncome.Value);

                        DateTime today = DateTime.Today;
                        cmd.Parameters.AddWithValue("@date_insert", today);

                        cmd.ExecuteNonQuery();
                        clearFields();

                        MessageBox.Show("Added seccessfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    conn.Close();
                }
            }
            displayIncomeData();
        }



        private int getID = 0;
        private void dataGridViewIncome_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridViewIncome.Rows[e.RowIndex];

                getID = Convert.ToInt32(row.Cells[0].Value);
                cbbxCategoryTpyeIncome.SelectedItem = row.Cells[1].Value.ToString();
                tbxItemIncome.Text = row.Cells[2].Value.ToString();
                tbxIncome.Text = row.Cells[3].Value.ToString();
                tbxDescIncome.Text = row.Cells[4].Value.ToString();
                dateIncome.Value = Convert.ToDateTime(row.Cells[5].Value.ToString());
            }
        }

        private void btnUpdateIncome_Click(object sender, EventArgs e)
        {
            if (tbxItemIncome.Text == "" || tbxIncome.Text == "" || cbbxCategoryTpyeIncome.SelectedIndex == -1 || tbxDescIncome.Text == "")
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

                        String updateData = "UPDATE income SET category = @cat , item = @item ,"
                                             + "income = @income ,description = @description , date_income = @date_income WHERE id = @id";

                        using (SqlCommand cmd = new SqlCommand(updateData, conn))
                        {
                            cmd.Parameters.AddWithValue("@cat", cbbxCategoryTpyeIncome.SelectedItem);
                            cmd.Parameters.AddWithValue("@item", tbxItemIncome.Text.Trim());
                            cmd.Parameters.AddWithValue("@income", tbxIncome.Text.Trim());
                            cmd.Parameters.AddWithValue("@description", tbxDescIncome.Text);
                            cmd.Parameters.AddWithValue("@date_income", dateIncome.Value);
                            cmd.Parameters.AddWithValue("@id", getID);

                            cmd.ExecuteNonQuery();
                            clearFields();

                            MessageBox.Show("Updated seccessfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        conn.Close();
                    }
                }
            }
            displayIncomeData();
        }


        private void btnDeleteIncome_Click(object sender, EventArgs e)
        {
            if (tbxItemIncome.Text == "" || tbxIncome.Text == "" || cbbxCategoryTpyeIncome.SelectedIndex == -1 || tbxDescIncome.Text == "")
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

                        String deleteData = "DELETE FROM income WHERE id = @id";         

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
            displayIncomeData();
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

            displayIncomeData();
            displayCategorylist();
        }
    }

}
