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

namespace InvoicePOS.UserControll.Department
{
    /// <summary>
    /// Interaction logic for Add_department.xaml
    /// </summary>
    public partial class Add_department : Window
    {
        DepartmentViewModel _DepartmentModel;
        public Add_department()
        {
            InitializeComponent();
            _DepartmentModel = new DepartmentViewModel();
            this.DataContext = _DepartmentModel;
        }
    }
}
