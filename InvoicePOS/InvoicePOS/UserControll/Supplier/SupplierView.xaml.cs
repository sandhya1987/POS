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
    /// Interaction logic for SupplierView.xaml
    /// </summary>
    public partial class SupplierView : Window
    {
        public SupplierView()
        {
            SupplierViewModel _SupplierViewModel;
            InitializeComponent();
            _SupplierViewModel = new SupplierViewModel();
            this.DataContext = _SupplierViewModel;

            //ImgSource.Source = App.Current.Properties["ItemViewImg"] as ImageSource;
            //App.Current.Properties["ItemViewImg"] = null;

            if (App.Current.Properties["SupplierView"] != null)
            {
                ImgSource.Source = App.Current.Properties["ItemViewImg"] as ImageSource;
                App.Current.Properties["ItemViewImg"] = null;
            }
            
        }
    }
}
