using InvoicePOS.Models;
using InvoicePOS.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace InvoicePOS.UserControll.CashReg
{
    /// <summary>
    /// Interaction logic for Window_CashRegList.xaml
    /// </summary>
    public partial class Window_CashRegList : Window
    {
        CashRegViewModel CRViewModel;
        public Window_CashRegList()
        {

            CashRegViewModel CMPViewModel;
            ObservableCollection<CashRegModel> ListGrid = new ObservableCollection<CashRegModel>();
            InitializeComponent();
            CMPViewModel = new CashRegViewModel();
            this.DataContext = CMPViewModel;
       // public static DataGrid GridRef;
        
        }
        private void Win_CashRegList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var str = (CashRegModel)dataGrid1.SelectedItem;
            CRViewModel.Select_Ok();
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
