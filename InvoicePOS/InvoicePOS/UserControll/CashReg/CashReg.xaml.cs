using InvoicePOS.Models;
using InvoicePOS.UserControll.CashReg;
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

namespace InvoicePOS.UserControll.Cash_Reg
{
    /// <summary>
    /// Interaction logic for CashReg.xaml
    /// </summary>
    public partial class CashReg : UserControl
    {
        CashRegViewModel _CashRegViewModel;
        public CashReg()
        {
            InitializeComponent();
            _CashRegViewModel = new CashRegViewModel();
            this.DataContext = _CashRegViewModel;
        }

        private void grdCash_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var str = (CashRegModel)dataGrid1.SelectedItem;
            _CashRegViewModel.View_CashReg();
        } 
    }
}
