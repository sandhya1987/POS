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
    /// Interaction logic for View_Transfer_Cash.xaml
    /// </summary>
    public partial class View_Transfer_Cash : Window
    {
        CashRegViewModel _CashRegViewModel;
        public View_Transfer_Cash()
        {
            InitializeComponent();
            _CashRegViewModel = new CashRegViewModel();
            this.DataContext = _CashRegViewModel;
        }


    }
}
