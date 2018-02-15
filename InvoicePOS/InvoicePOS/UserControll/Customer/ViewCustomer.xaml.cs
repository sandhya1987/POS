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
    /// Interaction logic for ViewCustomer.xaml
    /// </summary>
    public partial class ViewCustomer : Window
    {
        CustomerViewModel _CVM;
        public ViewCustomer()
        {
            InitializeComponent();
            _CVM = new CustomerViewModel();
            this.DataContext = _CVM;
            if (App.Current.Properties["Action"] != null)
            {
                ImgSource.Source = App.Current.Properties["ItemViewImg"] as ImageSource;
                App.Current.Properties["ItemViewImg"] = null;
            }
        }
    }
}
