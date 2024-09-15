using Microsoft.Data.SqlClient;
using System.Data;

namespace Task_2_day
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connection = "Server=DESKTOP-0UI2TTC\\SQLEXPRESS;Database=Ado_net_task1db;Integrated Security=SSPI;TrustServerCertificate=True";
            SqlDataAdapter adapter = new SqlDataAdapter("select * from student", connection);
            DataSet dataset = new DataSet();
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dataset, "student");
            foreach(DataRow row in dataset.Tables["student"].Rows)
            {
                Console.WriteLine(row["studentid"] + " " + row["Firstname"] + " " + row["Lastname"] + " " + row["Age"]);
            }


            foreach(DataRow rows in dataset.Tables["Student"].Rows)
            {
                if (rows["Firstname"].ToString() == "nandha")
                {
                    rows["Age"] = 28;
                }
            }
            Console.WriteLine("age updated");

            DataView dv = new DataView(dataset.Tables["Student"]);
            dv.RowFilter = "Age > 30";
            foreach(DataRowView drv in dv)
            {
                Console.WriteLine(drv["studentid"]+" " + drv["Firstname"]+" " + drv["Lastname"]+" " + drv["Age"]);
            }
            adapter.Update(dataset, "student");

        }
    }
}
