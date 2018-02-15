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
using System.Collections.ObjectModel;

namespace InvoicePOS.UserControll.GoDown
{
    /// <summary>
    /// Interaction logic for Window_GodownList.xaml
    /// </summary>
    public partial class Window_GodownList : Window
    {
        GodownViewModel CMPViewModel;
        public static DataGrid GridRef;
        public Window_GodownList()
        {
            ObservableCollection<GodownModel> ListGrid = new ObservableCollection<GodownModel>();
            InitializeComponent();
            CMPViewModel = new GodownViewModel();
            this.DataContext = CMPViewModel;
            //dataGrid1.ItemsSource = ListGrid;
            //GridRef = dataGrid1;

        }
        private void Win_grdGodown_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //var str = (TaxManagement)dataGrid1.SelectedItem;
            CMPViewModel.Select_Ok();
        }
    }
}
