using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace IncomeAndExpensesTracker_Program
{
    internal class IncomeData
    {
        String stringConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\wareeji\Documents\MoneyTrackerDB.mdf;Integrated Security=True;Connect Timeout=30";

        public int ID { set; get; }
        public string Category { set; get; }
        public string Item { set; get; }
        public string Income { set; get; }
        public string Description { set; get; }
        public string DateIncome { set; get; }

        public List<IncomeData> IncomeListData()
        {
            List<IncomeData> listData = new List<IncomeData>();


            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                conn.Open();

                string selectData = "SELECT * FROM income";

                using (SqlCommand cmd = new SqlCommand(selectData, conn))
                {
                    SqlDataReader render = cmd.ExecuteReader();

                    while (render.Read())
                    {
                        IncomeData iData = new IncomeData();
                        iData.ID = (int)render["id"];
                        iData.Category = render["category"].ToString();
                        iData.Item = render["item"].ToString();
                        iData.Income = render["income"].ToString();
                        iData.Description = render["description"].ToString();
                        iData.DateIncome = ((DateTime)render["date_income"]).ToString("MM-dd-yyyy");

                        listData.Add(iData);
                    }
                }
            }

            return listData;

        }

    }
}
