using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class db
    {
        private SqlConnection connection = WpfApp1.MainWindow.connection;

        public db()
        {
            //SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder
            //{
            //    DataSource = @"(localdb)\MSSQLLocalDB",
            //    InitialCatalog = "lession7",
            //    Pooling = true
            //};
            //connection = new SqlConnection(connectionString.ConnectionString);
            connection.Open();
        }

        public List<Employees> GetList()
        {
            List<Employees> list = new List<Employees>();
            string sql = @"SELECT * FROM Employees";
            using (SqlCommand com = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employees employee = new Employees()
                        {
                            firstName = reader["firstName"].ToString(),
                            lastName = reader["lastName"].ToString(),
                            age = reader["age"].ToString(),
                        };
                        list.Add(employee);
                    }
                }
            }
            return list;
        }

        public Employees GetById(int ID)
        {
            Employees employee = new Employees();
            string sql = @"SELECT * FROM Employees WHERE ID = "+ ID;
            using (SqlCommand com = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employee = new Employees()
                        {
                            firstName = reader["firstName"].ToString(),
                            lastName = reader["lastName"].ToString(),
                            age = reader["age"].ToString(),
                        };
                    }
                }
            }
            return employee;
        }

        public bool AddEmployee(Employees employee)
        {
            try
            {
                string sqlAdd = $@" INSERT INTO Employees(firstName, lastName, age)
                               VALUES(N'{employee.firstName}',
                                      N'{employee.lastName}',
                                      N'{employee.age}' ) ";

                using (var com = new SqlCommand(sqlAdd, connection))
                {
                    com.ExecuteNonQuery();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}