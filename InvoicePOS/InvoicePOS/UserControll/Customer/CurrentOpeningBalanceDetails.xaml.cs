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
using InvoicePOS.ViewModels.Customer;
using InvoicePOS.ViewModels;

namespace InvoicePOS.UserControll.Customer
{
    /// <summary>
    /// Interaction logic for CurrentOpeningBalanceDetails.xaml
    /// </summary>
    public partial class CurrentOpeningBalanceDetails : Window
    {
        InvoicePOS.ViewModels.CustomerViewModel _CVM = new CustomerViewModel();  
        public static TextBox BussinessLocationName;
        public static DatePicker Date;
        public CurrentOpeningBalanceDetails()
        {
            InitializeComponent();
            BussinessLocationName = textBox2_Copy4;
            Date = TrnscDate;
            _CVM = new CustomerViewModel();
            this.DataContext = _CVM;
        }
    }
}
