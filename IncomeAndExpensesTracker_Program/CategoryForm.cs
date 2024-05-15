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

    public partial class CategoryForm : UserControl
    {
        public void displayCategoryList()
        {
            CategoryData cData = new CategoryData();
            List<CategoryData> listData = cData.categoryListData();

            dataGridViewCategory.DataSource = listData;
        }

        public CategoryForm()
        {
            InitializeComponent();

            displayCategoryList();
        }


        public void clearFields()
        {
            tbxCategoryName.Text = "";
            cbbxCategoryTpye.SelectedIndex = -1;
            cbbxCategoryStatus.SelectedIndex = -1;
        }
        private void btnClearCategory_Click(object sender, EventArgs e)
        {
            clearFields();
        }


        String stringConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\wareeji\Documents\MoneyTrackerDB.mdf;Integrated Security=True;Connect Timeout=30";

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (tbxCategoryName.Text == "" || cbbxCategoryTpye.SelectedIndex == -1 || cbbxCategoryStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using(SqlConnection conn = new SqlConnection(stringConnection))
                {
                    conn.Open();

                    String insertData = "INSERT INTO categories (category , type ,status , date_insert )" + "VALUES (@cat , @type , @status , @date)";
                    
                    using(SqlCommand cmd = new SqlCommand(insertData, conn))
                    {
                        cmd.Parameters.AddWithValue("@cat" , tbxCategoryName.Text.Trim());
                        cmd.Parameters.AddWithValue("@type", cbbxCategoryTpye.SelectedItem);
                        cmd.Parameters.AddWithValue("@status", cbbxCategoryStatus.SelectedItem);

                        DateTime today = DateTime.Today;
                        cmd.Parameters.AddWithValue("@date", today);

                        cmd.ExecuteNonQuery();
                        clearFields();


                        MessageBox.Show("Added seccessfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    conn.Close();
                }
            }

            displayCategoryList();
        }



        private int getID = 0;
        private void dataGridViewCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridViewCategory.Rows[e.RowIndex];

                getID = Convert.ToInt32(row.Cells[0].Value);
                tbxCategoryName.Text = row.Cells[1].Value.ToString();
                cbbxCategoryTpye.SelectedItem = row.Cells[2].Value.ToString();
                cbbxCategoryStatus.SelectedItem = row.Cells[3].Value.ToString();
            }
        }

        private void btnUpateCategory_Click(object sender, EventArgs e)
        {
            if (tbxCategoryName.Text == "" || cbbxCategoryTpye.SelectedIndex == -1 || cbbxCategoryStatus.SelectedIndex == -1)
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

                        String updateData = "UPDATE categories SET category = @cat , type = @type ,status = @status WHERE id = @id";

                        using (SqlCommand cmd = new SqlCommand(updateData, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", getID);
                            cmd.Parameters.AddWithValue("@cat", tbxCategoryName.Text.Trim());
                            cmd.Parameters.AddWithValue("@type", cbbxCategoryTpye.SelectedItem);
                            cmd.Parameters.AddWithValue("@status", cbbxCategoryStatus.SelectedItem);

                            cmd.ExecuteNonQuery();
                            clearFields();

                            MessageBox.Show("Updated seccessfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        conn.Close();
                    }
                }
            }

            displayCategoryList();

        }


        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (tbxCategoryName.Text == "" || cbbxCategoryTpye.SelectedIndex == -1 || cbbxCategoryStatus.SelectedIndex == -1)
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

                        String deleteData = "DELETE FROM categories WHERE id = @id";

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
            displayCategoryList();
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

            displayCategoryList();

        }

    }
}
