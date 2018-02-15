using InvoicePOS.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InvoicePOS.UserControll.Employee
{
    /// <summary>
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : UserControl
    {
        EmployeeViewModel _EmployeeViewModel;
        public Employee()
        {
            InitializeComponent();
            _EmployeeViewModel = new EmployeeViewModel();
            this.DataContext = _EmployeeViewModel;
        }

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    MyStack.Children.Clear();
        //    EmployeeAdd _AC = new EmployeeAdd();
        //    MyStack.Children.Add(_AC);
        //}
        private void grdEmp_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var str = (EmployeeModel)dataGrid1.SelectedItem;
            _EmployeeViewModel.View_Employee();
        }

    }
}
