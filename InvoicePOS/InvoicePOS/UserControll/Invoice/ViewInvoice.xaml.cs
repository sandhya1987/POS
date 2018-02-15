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

namespace InvoicePOS.UserControll.Invoice
{
    /// <summary>
    /// Interaction logic for ViewInvoice.xaml
    /// </summary>
    public partial class ViewInvoice : Window
    {
        InvoiceViewModel _InvoiceViewModel;
        public ViewInvoice()
        {
            InitializeComponent();
            _InvoiceViewModel = new InvoiceViewModel();
            this.DataContext = _InvoiceViewModel;
        }
    }
}
