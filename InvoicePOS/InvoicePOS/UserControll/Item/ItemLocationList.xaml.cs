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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using InvoicePOS.Models;

namespace InvoicePOS.UserControll.Item
{
    /// <summary>
    /// Interaction logic for ItemLocationList.xaml
    /// </summary>
    public partial class ItemLocationList : UserControl
    {
        ItemLocation ViewModel;
        public static DataGrid ListGridRef;
        public static DataGrid ListGridMainRef;
        public ItemLocationList()
        {
            InitializeComponent();
            ViewModel = new ItemLocation();
            this.DataContext = ViewModel;
            ObservableCollection<SalesReturnModel> Listgd = new ObservableCollection<SalesReturnModel>();
            ObservableCollection<SalesReturnModel> ListgdMain = new ObservableCollection<SalesReturnModel>();


            dataGrid.ItemsSource = Listgd;
            ListGridRef = dataGrid;

            dataGrid1.ItemsSource = ListgdMain;
            ListGridMainRef = dataGrid1;
        }
       

        //private void DataGrid_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    // Lookup for the source to be DataGridCell
        //    //if (e.OriginalSource.GetType() == typeof(DataGridCell))
        //    //{
        //    //     //Starts the Edit on the row;
        //    //    DataGrid grd = (DataGrid)sender;
        //    //    grd.BeginEdit(e);
        //    //}


        //    //ItemLocation item1 = new ItemLocation();
        //    // //int  comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
        //    //item1.GetItem1(1,1);
    
             
            
        //}
    }
}
