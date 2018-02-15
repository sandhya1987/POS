using InvoicePOS.ViewModels;
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
using System.Windows.Shapes;

namespace InvoicePOS.UserControll.Employee
{
    /// <summary>
    /// Interaction logic for Window_Employee.xaml
    /// </summary>
    public partial class Window_Employee : Window
    {
        EmployeeViewModel _EmployeeViewModel;
        public Window_Employee()
        {
            InitializeComponent();
            _EmployeeViewModel = new EmployeeViewModel();
            this.DataContext = _EmployeeViewModel;
        }
    }
}
