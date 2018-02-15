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
    /// Interaction logic for Customergroup.xaml
    /// </summary>
    public partial class Customergroup : Window
    {
        CustomerGroupViewModel _CVM;
        public Customergroup()
        {
            InitializeComponent();
            _CVM = new CustomerGroupViewModel();
            this.DataContext = _CVM;
        }
    }
}
