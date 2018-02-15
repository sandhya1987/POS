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
using InvoicePOS.Views.Autocomplete;
using InvoicePOS.Views;
using InvoicePOS.Models;
using InvoicePOS.Views;

namespace InvoicePOS.Views.CashRegister
{
    /// <summary>
    /// Interaction logic for CashReg.xaml
    /// </summary>
    /// 
    

            
    public partial class CashReg : Window
    {
        MainViewModel ViewModel;
        public static TextBox BusLocationName;
        public static TextBox BussLoc;
        public static TextBox CashRegName;
        public static TextBox BussAddress;
        public CashReg()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
            this.DataContext = ViewModel;

            textBox8.Text = "";
            BusLocationName = textBox;

            BussAddress = textBox1;
            CashRegName = textBox8;

            var BussinessNameList = App.Current.Properties["BussLocList"] as List<AutoBussinessModel>;
            //foreach (var item in BussinessNameList)
            //{
            //    textBox8.AddItem(new AutoBussinessModel
            //    {
            //        DisplayId = item.DisplayId,
            //        DisplayName = item.DisplayName
            //    });
            //}

            //BusLocationName = textBox8;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
