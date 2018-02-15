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

namespace InvoicePOS.UserControll.Supplier
{
    /// <summary>
    /// Interaction logic for Window_SupplierList.xaml
    /// </summary>
    public partial class Window_SupplierList : Window
    {
        SupplierViewModel _SupplierViewModel;
        public Window_SupplierList()
        {
            InitializeComponent();
            _SupplierViewModel = new SupplierViewModel();
            this.DataContext = _SupplierViewModel;
        }

        private void dataGrid1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _SupplierViewModel.Select_Ok();
        }
    }
}
