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
    /// Interaction logic for ViewCashTranscation.xaml
    /// </summary>
    public partial class ViewCashTranscation : Window
    {
        CashRegViewModel _CashRegViewModel;
        //public static TextBox BussRef;
        public ViewCashTranscation()
        {
            InitializeComponent();
            _CashRegViewModel = new CashRegViewModel();
            this.DataContext = _CashRegViewModel;

            //txtbusloc.Text = "";
            //BussRef = txtbusloc;
        }

    }
}
