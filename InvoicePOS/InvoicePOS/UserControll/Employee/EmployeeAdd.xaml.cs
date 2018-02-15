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
    /// Interaction logic for EmployeeAdd.xaml
    /// </summary>
    public partial class EmployeeAdd : Window
    {
        EmployeeViewModel _EmployeeViewModel;
        public static TextBox EmpDesignationReff;
        public static TextBox EmpDepartmentReff;
        public static TextBox BussRef;
        public EmployeeAdd()
        {
            InitializeComponent();
            _EmployeeViewModel = new EmployeeViewModel();
            this.DataContext = _EmployeeViewModel;
            //dtpick.DisplayDate = DateTime.Now;
            //dtpick1.DisplayDate = DateTime.Now;
            //dtpick.SelectedDate = DateTime.Now;

            Dep.Text = "";
            EmpDepartmentReff = Dep;

            Deg.Text = "";
            EmpDesignationReff = Deg;

            textBox.Text = "";
            BussRef = textBox;

        }

        
    }
}
