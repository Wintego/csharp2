using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //ObservableCollection<Department> deps;
        public static SqlConnection connection;
        public MainWindow()
        {
            InitializeComponent();
            //deps = new ObservableCollection<Department>()
            //{
            //    new Department("department1", new ObservableCollection<Employee>(){new Employee("dep1", "emp1", 11),new Employee("dep1", "emp2",12),new Employee("dep1", "emp3",13)}),
            //    new Department("department2", new ObservableCollection<Employee>(){new Employee("dep2", "emp1", 21),new Employee("dep2", "emp2", 22)}),
            //    new Department("department3", new ObservableCollection<Employee>(){new Employee("dep3", "emp1", 31)})

            //};
            //cb.ItemsSource = deps;
            SqlRun();
            Employee.SqlEmployee();
            lb.ItemsSource = Employee.dtEmployee.DefaultView;
            Department.SqlDepartment();
            cb.ItemsSource = Department.dtDepartment.DefaultView;
        }

        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            //Department a = cb.SelectedItem as Department;
            //a.Employees.Add(new Employee(fname.Text, lname.Text, Convert.ToInt32(age.Text)));

            
            DataRow newRow = Employee.dtEmployee.NewRow();
            newRow["firstName"] = fname.Text;
            newRow["lastName"] = lname.Text;
            newRow["age"] = age.Text;
            Employee.dtEmployee.Rows.Add(newRow);
            Employee.dataAdapterEmployee.Update(Employee.dtEmployee);
        }

        private void RemoveEmployee(object sender, RoutedEventArgs e)
        {
            //deps[cb.SelectedIndex].Employees.RemoveAt(lb.SelectedIndex);
            DataRowView newRow = lb.SelectedItem as DataRowView;
            newRow.Row.Delete();
            Employee.dataAdapterEmployee.Update(Employee.dtEmployee);
        }

        private void AddDepartment(object sender, RoutedEventArgs e)
        {
            //deps.Add(new Department(newDepName.Text));
            DataRow newRow = Department.dtDepartment.NewRow();
            newRow["depName"] = newDepName.Text;
            Department.dtDepartment.Rows.Add(newRow);
            Department.dataAdapterDepartment.Update(Department.dtDepartment);
        }

        private void RemoveDepartment(object sender, RoutedEventArgs e)
        {
            //deps.RemoveAt(cb.SelectedIndex);
            DataRowView newRow = cb.SelectedItem as DataRowView;
            newRow.Row.Delete();
            Department.dataAdapterDepartment.Update(Department.dtDepartment);
        }
        /// <summary>
        /// настройка соединения
        /// </summary>
        public static void SqlRun()
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "lession7",
                Pooling = true
            };
            connection = new SqlConnection(connectionStringBuilder.ConnectionString);
        }

        private void ChangeDepartment(object sender, RoutedEventArgs e)
        {
            changeDepNamePanel.Visibility = Visibility.Collapsed;
            DataRowView newRow = (DataRowView)cb.SelectedItem;
            newRow["depName"] = changeDepName.Text;
            Department.dataAdapterDepartment.Update(Department.dtDepartment);

        }

        private void ShowChangeDepNamePanel(object sender, RoutedEventArgs e)
        {
            changeDepNamePanel.Visibility = Visibility.Visible;
        }
    }
}
