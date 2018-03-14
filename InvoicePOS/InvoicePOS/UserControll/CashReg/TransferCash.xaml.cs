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

namespace InvoicePOS.UserControll.CashReg
{
    /// <summary>
    /// Interaction logic for TransferCash.xaml
    /// </summary>
    public partial class TransferCash : Window
    {

        CashRegViewModel _CashRegViewModel;
        public static TextBox BussRef;
        public static TextBox FromCashRef;
        public static TextBox ToCashRef;
        public static TextBox TotRef;
        public static TextBox TransferNoRef;
        public static TextBox SubmitedRef;
        public TransferCash()
        {
            InitializeComponent();
            _CashRegViewModel = new CashRegViewModel();
            this.DataContext = _CashRegViewModel;


            textBoxbus.Text = "";
            BussRef = textBoxbus;

            textBox_FromCash.Text = "";
            FromCashRef = textBox_FromCash;

            textBox_ToCash.Text = "";
            ToCashRef = textBox_ToCash;


            txtotamt.Text = "";
            TotRef = txtotamt;

            txtransfer.Text = "";
            SubmitedRef = txtransfer;

            txttransferno.Text = "";
            TransferNoRef = txttransferno;


        }

    }
}
