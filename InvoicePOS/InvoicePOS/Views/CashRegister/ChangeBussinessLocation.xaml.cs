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
using InvoicePOS.Models;
using System.ComponentModel;
using InvoicePOS.Helpers;

namespace InvoicePOS.Views.CashRegister
{
    /// <summary>
    /// Interaction logic for ChangeBussinessLocation.xaml
    /// </summary>
    public partial class ChangeBussinessLocation : Window
    {
        InvoicePOS.ViewModels.ChangeBussinessLocationViewModel ViewModel;
        //ChangeBussinessLocation ShowLocation;
        public static TextBox BussinessLocationName;
        public static TextBox Address;
        public static TextBox CashReg;
        public static Button ClickCadhReg;
        public ChangeBussinessLocation()
        {
            ViewModel = new InvoicePOS.ViewModels.ChangeBussinessLocationViewModel();
            //ShowLocation = new ChangeBussinessLocation();
            this.DataContext = ViewModel;
            InitializeComponent();

            BussinessLocationName = textBox;
            Address = textBox2;
            CashReg = textBox1;
            ClickCadhReg = buttonCash;
            App.Current.Properties["CASH_REG_NAME_BUSINESSLOC"] = 2;
            var data = App.Current.Properties["ShowBussinessLocation"] as BusinessLocationModel;
            if (data != null)
            {
                textBox1.IsEnabled = true;
                buttonCash.IsEnabled = true;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
