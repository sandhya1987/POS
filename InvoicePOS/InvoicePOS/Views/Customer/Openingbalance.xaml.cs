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

namespace InvoicePOS.Views.Customer
{
    /// <summary>
    /// Interaction logic for Openingbalance.xaml
    /// </summary>
    public partial class Openingbalance : Window
    {
        CustomerViewModel _CVM;
        public static TextBox BussRef;
        public Openingbalance()
        {
            InitializeComponent();
            _CVM = new CustomerViewModel();
            this.DataContext = _CVM;

            textBox2_Copy4.Text = "";
            BussRef = textBox2_Copy4;
        }
    }
}
