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

namespace InvoicePOS.UserControll.StockLedger
{
    /// <summary>
    /// Interaction logic for StockLedgerList.xaml
    /// </summary>
    public partial class StockLedgerList : UserControl
    {
        StockLedgerViewModel _StockLedgerViewModel;
        public static TextBox GodownStockLadger;
        public static TextBox ItemStockLadger;
        public static TextBox getCompanyName;
        public static DatePicker fromdate1;
        public static DatePicker todate1;

        public static TextBlock getopeningstock;
        public static TextBlock getstockin;
        public static TextBlock getstockout;
        public static TextBlock gettotaltrans;
        public StockLedgerList()
        {
            InitializeComponent();
            _StockLedgerViewModel = new StockLedgerViewModel();
            this.DataContext = _StockLedgerViewModel;

            Godown.Text = "";
            GodownStockLadger = Godown;

            Item.Text = "";
            ItemStockLadger = Item;

            comapany.Text = "";
            getCompanyName = comapany;

            fromdate.Text = "";
            fromdate1 = fromdate;

            todate.Text = "";
            todate1 = todate;

            openingstock.Text = "";
            getopeningstock = openingstock;

            stockin.Text = "";
            getstockin = stockin;

            stockout.Text = "";
            getstockout = stockout;

            totaltransaction.Text = "";
            gettotaltrans = totaltransaction;
        }
    }
}
