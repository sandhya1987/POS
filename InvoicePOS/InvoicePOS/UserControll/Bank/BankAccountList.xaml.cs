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
    /// Interaction logic for BankAccountList.xaml
    /// </summary>
    public partial class BankAccountList : Window
    {
        BankViewModel _BankViewModel;
        public BankAccountList()
        {
            InitializeComponent();
            _BankViewModel = new BankViewModel();
            this.DataContext = _BankViewModel;
        }
    }
}
