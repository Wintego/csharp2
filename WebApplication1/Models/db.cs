using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class db
    {
        private SqlConnection connection;

        public db()
        {
            //SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder
            //{
            //    DataSource = @"(localdb)\MSSQLLocalDB",
            //    InitialCatalog = "lession7",
            //    Pooling = true
            //};
            //connection = new SqlConnection(connectionString.ConnectionString);
            WpfApp1.MainWindow.SqlRun();
            connection = WpfApp1.MainWindow.connection;
            connection.Open();
        }

        public List<WpfApp1.Employee> GetList()
        {
            List<WpfApp1.Employee> list = new List<WpfApp1.Employee>();
            string sql = @"SELECT * FROM Employees";
            using (SqlCommand com = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        WpfApp1.Employee employee = new WpfApp1.Employee()
                        {
                            FirstName = reader["firstName"].ToString(),
                            LastName = reader["lastName"].ToString(),
                            Age = Convert.ToInt32(reader["age"]),
                        };
                        list.Add(employee);
                    }
                }
            }
            return list;
        }

        public WpfApp1.Employee GetById(int ID)
        {
            WpfApp1.Employee employee = new WpfApp1.Employee();
            string sql = @"SELECT * FROM Employees WHERE ID = "+ ID;
            using (SqlCommand com = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employee = new WpfApp1.Employee()
                        {
                            FirstName = reader["firstName"].ToString(),
                            LastName = reader["lastName"].ToString(),
                            Age = Convert.ToInt32(reader["age"]),
                        };
                    }
                }
            }
            return employee;
        }

        public bool AddEmployee(WpfApp1.Employee employee)
        {
            try
            {
                string sqlAdd = $@" INSERT INTO Employees(firstName, lastName, age)
                               VALUES(N'{employee.FirstName}',
                                      N'{employee.LastName}',
                                      N'{employee.Age}' ) ";

                using (var com = new SqlCommand(sqlAdd, connection))
                {
                    com.ExecuteNonQuery();
                }

                //var a = WpfApp1.Employee.dtEmployee;
                //var b = WpfApp1.Employee.dataAdapterEmployee.Update(WpfApp1.Employee.dtEmployee);
                //WpfApp1.Employee.dtEmployee = new DataTable();                
                //DataRow newRow = WpfApp1.Employee.dtEmployee.NewRow();
                //WpfApp1.Employee.dataAdapterEmployee = new SqlDataAdapter();
                //WpfApp1.Employee.dataAdapterEmployee.Fill(WpfApp1.Employee.dtEmployee);
                //newRow["firstName"] = employee.FirstName;
                //newRow["lastName"] = employee.LastName;
                //newRow["age"] = employee.Age;
                //WpfApp1.Employee.dtEmployee.Rows.Add(newRow);
                //WpfApp1.Employee.dataAdapterEmployee.Update(WpfApp1.Employee.dtEmployee);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}