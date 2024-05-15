using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace IncomeAndExpensesTracker_Program
{
    internal class ExpensesData
    {
        String stringConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\wareeji\Documents\MoneyTrackerDB.mdf;Integrated Security=True;Connect Timeout=30";

        public int ID { set; get; }
        public string Category { set; get; }
        public string Item { set; get; }
        public string Cost { set; get; }
        public string Description { set; get; }
        public string DateExpense { set; get; }

        public List<ExpensesData> ExpensesListData()
        {
            List<ExpensesData> listData = new List<ExpensesData>();

            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                conn.Open();

                string selectData = "SELECT * FROM expenses";

                using (SqlCommand cmd = new SqlCommand(selectData, conn))
                {
                    SqlDataReader render = cmd.ExecuteReader();

                    while (render.Read())
                    {
                        ExpensesData eData = new ExpensesData();
                        eData.ID = (int)render["id"];
                        eData.Category = render["category"].ToString();
                        eData.Item = render["item"].ToString();
                        eData.Cost = render["cost"].ToString();
                        eData.Description = render["description"].ToString();
                        eData.DateExpense = ((DateTime)render["date_expense"]).ToString("MM-dd-yyyy");

                        listData.Add(eData);
                    }
                }
            }

            return listData;

        }
    }
}
