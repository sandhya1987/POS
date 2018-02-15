using InvoicePOS.Models;
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

namespace InvoicePOS.UserControll.Payment
{
    /// <summary>
    /// Interaction logic for ReceivePayment.xaml
    /// </summary>
    public partial class ReceivePayment : UserControl
    {
        ReceivePaymentViewModel _PaymentViewModel;
        public ReceivePayment()
        {
            InitializeComponent();
            _PaymentViewModel = new ReceivePaymentViewModel();
            this.DataContext = _PaymentViewModel;
        }

        private void grdRec_Payment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var str = (RecPayModel)dataGrid1.SelectedItem;
            _PaymentViewModel.ViewPayReceive_Click();
        }
    }
}
