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

namespace InvoicePOS.UserControll.Bank
{
    /// <summary>
    /// Interaction logic for BankList.xaml
    /// </summary>
    public partial class BankList : UserControl
    {
        BankViewModel BVM;
        public BankList()
        {
            InitializeComponent();
            BVM = new BankViewModel();
            this.DataContext = BVM;
        }
    }
}
