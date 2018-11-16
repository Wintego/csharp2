using System.Data.SqlClient;
using System.Data;

namespace WpfApp1
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Employee(string fn, string ln, int a)
        {
            FirstName = fn;
            LastName = ln;
            Age = a;
        }
        public Employee() { }
        public static DataTable dtEmployee;
        public static SqlDataAdapter dataAdapterEmployee;
        /// <summary>
        /// работа с таблицей Employee
        /// </summary>
        public static void SqlEmployee()
        {
            dataAdapterEmployee = new SqlDataAdapter();

            //select
            dataAdapterEmployee.SelectCommand = new SqlCommand(@"SELECT Id, firstName, lastName, age FROM Employees", MainWindow.connection);

            //insert
            SqlCommand insert = new SqlCommand(@"INSERT INTO Employees (firstName, lastName, age) VALUES (@firstName, @lastName, @age); SET @Id = @@IDENTITY;", MainWindow.connection);
            insert.Parameters.Add("@firstName", SqlDbType.NVarChar, -1, "firstName");
            insert.Parameters.Add("@lastName", SqlDbType.NVarChar, -1, "lastName");
            insert.Parameters.Add("@age", SqlDbType.SmallInt, -1, "age");

            SqlParameter param = insert.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.Direction = ParameterDirection.Output;
            dataAdapterEmployee.InsertCommand = insert;

            //update
            SqlCommand update = new SqlCommand(@"UPDATE Employees SET @firstName = firstName, @lastName = lastName, @age = age WHERE ID = @ID", MainWindow.connection);
            update.Parameters.Add("@firstName", SqlDbType.NVarChar, -1, "firstName");
            update.Parameters.Add("@lastName", SqlDbType.NVarChar, -1, "lastName");
            update.Parameters.Add("@age", SqlDbType.SmallInt, -1, "age");
            param = update.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.SourceVersion = DataRowVersion.Original;
            dataAdapterEmployee.UpdateCommand = update;

            //delete
            SqlCommand delete = new SqlCommand("DELETE FROM Employees WHERE ID = @ID", MainWindow.connection);
            param = delete.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.SourceVersion = DataRowVersion.Original;
            dataAdapterEmployee.DeleteCommand = delete;

            dtEmployee = new DataTable();
            dataAdapterEmployee.Fill(dtEmployee);
        }
    }
}