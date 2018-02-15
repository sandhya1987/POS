using InvoicePOS.Models;
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

namespace InvoicePOS.Views.CashRegister
{
    /// <summary>
    /// Interaction logic for CashRegisterAmountDetails.xaml
    /// </summary>
    public partial class CashRegisterAmountDetails : Window
    {
        MainViewModel ViewModel;
        public static TextBox BusLocationName;       
        public static TextBox CashRegNo;
        public static TextBox CurrentCash;
        public CashRegisterAmountDetails()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
            this.DataContext = ViewModel;

            textBox.Text = "";
            BusLocationName = textBox;

            textBox1.Text = "";
            CashRegNo = textBox1;
            CurrentCash = textBox2;
            var BussinessNameList = App.Current.Properties["BussLocListforCashAmount"] as List<AutoBussinessModel>;
            //var data = App.Current.Properties["SelectData"] as CashRegModel;
            //textBox.Text = data.BUSINESS_LOCATION;
            //textBox1.Text = data.CASH_REG_NAME;
            //textBox2.Text = data.CASH_AMOUNT.ToString();
            //textBox2.Text = Convert.ToDecimal(data.NETAMT);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
