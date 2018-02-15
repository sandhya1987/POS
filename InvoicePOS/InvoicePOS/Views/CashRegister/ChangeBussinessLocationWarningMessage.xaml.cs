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
    /// Interaction logic for ChangeBussinessLocationWarningMessage.xaml
    /// </summary>
    public partial class ChangeBussinessLocationWarningMessage : Window
    {
        MainViewModel ViewModel;
        //ChangeBussinessLocation BussinessModel;
        public ChangeBussinessLocationWarningMessage()
        {
            ViewModel = new MainViewModel();
            //BussinessModel = new ChangeBussinessLocation();
            this.DataContext = ViewModel;
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
