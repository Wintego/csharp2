using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace WpfApp1
{
    class Department: INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Name)));
            }
        }
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        public event PropertyChangedEventHandler PropertyChanged;
        public Department(string n, ObservableCollection<Employee> e)
        {
            Name = n;
            Employees = e;
        }
        public Department(string n)
        {
            Name = n;
        }

        public static DataTable dtDepartment;
        public static SqlDataAdapter dataAdapterDepartment;
        /// <summary>
        /// работа с таблицей Departments
        /// </summary>
        public static void SqlDepartment()
        {
            dataAdapterDepartment = new SqlDataAdapter();

            //select
            dataAdapterDepartment.SelectCommand = new SqlCommand(@"SELECT Id, depName FROM Departments", MainWindow.connection);

            //insert
            SqlCommand insert = new SqlCommand(@"INSERT INTO Departments (depName) VALUES (@depName); SET @Id = @@IDENTITY;", MainWindow.connection);
            insert.Parameters.Add("@depName", SqlDbType.NVarChar, -1, "depName");

            SqlParameter param = insert.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.Direction = ParameterDirection.Output;
            dataAdapterDepartment.InsertCommand = insert;

            //update
            SqlCommand update = new SqlCommand(@"UPDATE Departments SET @depName = depName WHERE ID = @ID", MainWindow.connection);
            update.Parameters.Add("@depName", SqlDbType.NVarChar, -1, "depName");
            param = update.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.SourceVersion = DataRowVersion.Original;
            dataAdapterDepartment.UpdateCommand = update;

            //delete
            SqlCommand delete = new SqlCommand("DELETE FROM Departments WHERE ID = @ID", MainWindow.connection);
            param = delete.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.SourceVersion = DataRowVersion.Original;
            dataAdapterDepartment.DeleteCommand = delete;

            dtDepartment = new DataTable();
            dataAdapterDepartment.Fill(dtDepartment);
        }
    }
}
