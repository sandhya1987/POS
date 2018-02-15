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

namespace InvoicePOS.UserControll.StockTransfer
{
    /// <summary>
    /// Interaction logic for StockTransferView.xaml
    /// </summary>
    public partial class StockTransferView : Window
    {
        StockTransferViewModel _StockTransferViewModel;
        public StockTransferView()
        {
            InitializeComponent();
            _StockTransferViewModel = new StockTransferViewModel();
            this.DataContext = _StockTransferViewModel;
        }
    }
}
