using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace IncomeAndExpensesTracker_Program
{
    internal class CategoryData
    {
        String stringConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\wareeji\Documents\MoneyTrackerDB.mdf;Integrated Security=True;Connect Timeout=30";


        public int ID { set; get; }
        public string Category { set; get; }
        public string Type { set; get; }
        public string Status { set; get; }
        public string Date { set; get; }


        public List<CategoryData> categoryListData()
        {
            List<CategoryData> listData = new List<CategoryData>();


            using(SqlConnection conn = new SqlConnection(stringConnection))
            {
                conn.Open();

                string selectData = "SELECT * FROM categories";

                using (SqlCommand cmd = new SqlCommand(selectData, conn))
                {
                    SqlDataReader render = cmd.ExecuteReader();

                    while (render.Read())
                    {
                        CategoryData cData = new CategoryData();
                        cData.ID = (int)render["id"];
                        cData.Category = render["category"].ToString();
                        cData.Type = render["type"].ToString();
                        cData.Status = render["status"].ToString();
                        cData.Date = ((DateTime)render["date_insert"]).ToString("MM-dd-yyyy");

                        listData.Add(cData);
                    }
                }
            }
            return listData;
        }
    }
}
