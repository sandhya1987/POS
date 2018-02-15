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
    /// Interaction logic for Window_DepartmentList.xaml
    /// </summary>
    public partial class Window_DepartmentList : Window
    {
        DepartmentViewModel _DepartmentViewModel;
        public Window_DepartmentList()
        {
            InitializeComponent();
            _DepartmentViewModel = new DepartmentViewModel();
            this.DataContext = _DepartmentViewModel;
        }
        private void Win_grdDesignation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //var str = (TaxManagement)dataGrid1.SelectedItem;
            _DepartmentViewModel.Select_Ok();
        }
    }
}
