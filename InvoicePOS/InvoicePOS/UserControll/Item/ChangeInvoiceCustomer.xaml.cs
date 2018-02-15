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
using InvoicePOS.ViewModels;

namespace InvoicePOS.UserControll.Item
{
    /// <summary>
    /// Interaction logic for ChangeInvoiceCustomer.xaml
    /// </summary>
    public partial class ChangeInvoiceCustomer : Window
    {
        CustomerViewModel _CustomerViewModel = new CustomerViewModel();
        public ChangeInvoiceCustomer()
        {
            InitializeComponent();
            _CustomerViewModel = new CustomerViewModel();
            this.DataContext = _CustomerViewModel;
        }
    }
}
