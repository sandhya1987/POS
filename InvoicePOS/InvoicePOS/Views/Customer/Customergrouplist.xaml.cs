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

namespace InvoicePOS.UserControll.Customer
{
    /// <summary>
    /// Interaction logic for Customergrouplist.xaml
    /// </summary>
    public partial class Customergrouplist : Window
    {
        CustomerGroupViewModel _CVM;
        public Customergrouplist()
        {
            InitializeComponent();
            _CVM = new CustomerGroupViewModel();
            this.DataContext = _CVM;
        }
        private void Win_grdCustomerGroup_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //var str = (TaxManagement)dataGrid1.SelectedItem;
            _CVM.Select_Ok();
        }
    }
}
