using System;
using System.Collections.Generic;
using System.Globalization;
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
using Checkpoint.Singletons;
using Checkpoint.Converters;

namespace Checkpoint.Pages
{
    public partial class EmployeePage : Page
    {
        public EmployeePage()
        {
            InitializeComponent();

            var allEmployees = AllEmployeesSingleton.Instance.Employees;
            var currentEmployee = EmployeeSingleton.Instance.Employee;

            foreach (var employee in allEmployees)
            {
                employee.IsCurrentEmployee = (employee.ID == currentEmployee.ID);
            }

            EmployeeLV.ItemsSource = allEmployees;
        }
    }
}
