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

namespace InvoicePOS.UserControll.CashReg
{
    /// <summary>
    /// Interaction logic for ViewCashReg.xaml
    /// </summary>
    public partial class ViewCashReg : Window
    {
        CashRegViewModel _CashRegModel;
        public ViewCashReg()
        {
            InitializeComponent();
            _CashRegModel = new CashRegViewModel();
            this.DataContext = _CashRegModel;
        }
    }
}
