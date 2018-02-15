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

namespace InvoicePOS.Views.Customer
{
    /// <summary>
    /// Interaction logic for CustomerAdd.xaml
    /// </summary>
    public partial class CustomerAdd : Window
    {
        CustomerViewModel CMPViewModel; 
        public CustomerAdd()
        {
            InitializeComponent();
            CMPViewModel = new CustomerViewModel();
            this.DataContext = CMPViewModel;
        }
    }
}
