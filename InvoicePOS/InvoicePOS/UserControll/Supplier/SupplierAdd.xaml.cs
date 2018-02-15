//using InvoicePOS.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

using InvoicePOS.Helpers;
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
using WpfAutoComplete.Controls;
using InvoicePOS.Models;
using InvoicePOS.Views;
using System.Collections.ObjectModel;

namespace InvoicePOS.UserControll.Supplier
{
    /// <summary>
    /// Interaction logic for SupplierAdd.xaml
    /// </summary>
    public partial class SupplierAdd : Window
    {
        //public static Image ImageReff;

        public static Image ImageReff;

        SupplierViewModel _SupplierViewModel;
        public SupplierAdd()
        {
            InitializeComponent();
            _SupplierViewModel = new SupplierViewModel();
            this.DataContext = _SupplierViewModel;

            ImgSource.Source = null;
            ImageReff = ImgSource;
            

            if (App.Current.Properties["SupplierEdit"] != null)
            {
                ImgSource.Source = App.Current.Properties["ItemViewImg"] as ImageSource;
                App.Current.Properties["ItemViewImg"] = null;
            }

            //Image1.Source = null;

            //ImageReff = Image1;

            //ImgSource.Source = null;
            //ImageReff = ImgSource;
        }
    }



}
