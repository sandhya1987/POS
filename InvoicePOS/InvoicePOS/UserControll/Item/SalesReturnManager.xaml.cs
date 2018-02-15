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

namespace InvoicePOS.UserControll.Item
{
    /// <summary>
    /// Interaction logic for SalesReturnManager.xaml
    /// </summary>
    public partial class SalesReturnManager : Window
    {
        ItemViewModel _ItemViewModel = new ItemViewModel();
        public SalesReturnManager()
        {
            InitializeComponent();
            _ItemViewModel = new ItemViewModel();
            this.DataContext = _ItemViewModel;
        }
    }
}
