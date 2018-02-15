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

namespace InvoicePOS.UserControll.Payment
{
    /// <summary>
    /// Interaction logic for ViewReceivePayment.xaml
    /// </summary>
    public partial class ViewReceivePayment : Window
    {
        ReceivePaymentViewModel _PaymentViewModel;
        public ViewReceivePayment()
        {
            InitializeComponent();
            _PaymentViewModel = new ReceivePaymentViewModel();
            this.DataContext = _PaymentViewModel;
        }
    }
}
