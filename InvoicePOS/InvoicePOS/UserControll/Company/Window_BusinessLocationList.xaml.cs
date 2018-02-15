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
using System.Collections.ObjectModel;

namespace InvoicePOS.UserControll.Company
{
    /// <summary>
    /// Interaction logic for Window_BusinessLocationList.xaml
    /// </summary>
    public partial class Window_BusinessLocationList : Window
    {
        BussinessLocationViewModel CMPViewModel;
       // public static DataGrid GridRef;
        public Window_BusinessLocationList()
        {
            ObservableCollection<BusinessLocationModel> ListGrid = new ObservableCollection<BusinessLocationModel>();
            App.Current.Properties["BussLocList"] = ListGrid;
            InitializeComponent();
            CMPViewModel = new BussinessLocationViewModel();
            this.DataContext = CMPViewModel;

            //dataGrid1.ItemsSource = ListGrid;
            //GridRef = dataGrid1;
        }
        private void Win_grdGodown_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var str = (BusinessLocationModel)dataGrid1.SelectedItem;
            
            CMPViewModel.Select_Ok();
        }
    }
}
