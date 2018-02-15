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

namespace InvoicePOS.UserControll.Bank
{
    /// <summary>
    /// Interaction logic for BankAccountView.xaml
    /// </summary>
    public partial class BankAccountView : Window
    {
        BankViewModel _BankViewModel;
        public BankAccountView()
        {
            InitializeComponent();
            _BankViewModel = new BankViewModel();
            this.DataContext = _BankViewModel;
        }
    }
}
