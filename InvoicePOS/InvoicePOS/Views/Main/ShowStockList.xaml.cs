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

namespace InvoicePOS.Views.Main
{
    /// <summary>
    /// Interaction logic for ShowStock.xaml
    /// </summary>
    public partial class ShowStockList : Window
    {
        ShowStockViewModel ViewModel;
        public static TextBox ItemNaReff;
        public static TextBox BarrcodeReff;
        public static TextBox SalepriceReff;
        public static ItemNameAutoComplete ItemNameReff;
        public static DataGrid ListGridRefr;
        public ShowStockList()
        {
            InitializeComponent();
            ViewModel = new ShowStockViewModel();
            this.DataContext = ViewModel;

            ObservableCollection<ItemModel> ListGrid = new ObservableCollection<ItemModel>();

            var ItemNameList = App.Current.Properties["AutoItemNameList"] as List<ItemNameAutoListModel>;
            foreach (var item in ItemNameList)
            {
                textBox2.AddItem(new ItemNameAutoListModel
                {
                    DisplayId = item.DisplayId,
                    DisplayName = item.DisplayName
                });
            }

            ItemNameReff = textBox2;

            //ItemNameStock.Text = "";
            //ItemNameReff = ItemNameStock;
            ListGrid = App.Current.Properties["StockGrid"] as ObservableCollection<ItemModel>;



            ItemNaReff = ItemNAme;
            ItemNAme = ItemNaReff;

            BarrcodeReff = Barcood;
            Barcood = BarrcodeReff;

            SalepriceReff = SalePrice;
            SalePrice = SalepriceReff;




            dataGrid1.ItemsSource = ListGrid;
            ListGridRefr = dataGrid1;
        }
    }
}
