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

namespace InvoicePOS.UserControll.Department
{
    /// <summary>
    /// Interaction logic for Department_list.xaml
    /// </summary>
    public partial class Department_list : UserControl
    {
        DepartmentViewModel _DepartmentModel;
        public Department_list()
        {
            
            InitializeComponent();
            _DepartmentModel = new DepartmentViewModel();
            this.DataContext = _DepartmentModel;
        }
        private void Department_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var str = (DepartmentModel)dataGrid.SelectedItem;
            _DepartmentModel.Department_View();
        }

        
        
    }
}
