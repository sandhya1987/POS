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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InvoicePOS.UserControll.StockTransfer
{
    /// <summary>
    /// Interaction logic for AddStockTransfer.xaml
    /// </summary>
    public partial class AddStockTransfer : Window
    {
        StockTransferViewModel _StockTransferViewModel;
        public static DataGrid ListGridRef;
        public static TextBox BussRef;
        public static TextBox getbarcode;
        public static TextBox ItemRef1;
        public static TextBox ItemRef2;
        public static TextBox FrmGodownRef;
        public static TextBox ToGodownRef;
        public static TextBox SuppRef;
        public static TextBox stockEmail;
        public static Button buttoncopy;
        public static Button buttoncopy2;
        public ObservableCollection<StockTransferModel> _ListGrid_Temp = new ObservableCollection<StockTransferModel>();
        public ObservableCollection<StockTransferModel> _ListGrid_Temp1 = new ObservableCollection<StockTransferModel>();

        public AddStockTransfer()
        {
            InitializeComponent();
            _StockTransferViewModel = new StockTransferViewModel();
            this.DataContext = _StockTransferViewModel;

            BUSINESS_LOCATION_ID.Text = "";
            BussRef = BUSINESS_LOCATION_ID;
            BARCODE.Text = "";
            getbarcode = BARCODE;

            ITEM_NAME.Text = "";
            ItemRef1 = ITEM_NAME;

            SEARCH_CODE.Text = "";
            ItemRef2 = SEARCH_CODE;

            FROM_GODOWN_ID.Text = "";
            FrmGodownRef = FROM_GODOWN_ID;

            TO_GODOWN_ID.Text = "";
            ToGodownRef = TO_GODOWN_ID;

            RECEIVED_BY_ID.Text = "";
            SuppRef = RECEIVED_BY_ID;

            EMAIL.Text = "";
            stockEmail = EMAIL;
            button_Copy1.IsEnabled = false;
            buttoncopy = button_Copy1;

            button_Copy2.IsEnabled = false;
            buttoncopy2 = button_Copy2;

            ObservableCollection<StockTransferModel> _ListGrid_Temp = new ObservableCollection<StockTransferModel>();
            ObservableCollection<POrderModel> AddListGrid1 = new ObservableCollection<POrderModel>();
            //ListGrid = App.Current.Properties["DataGridTrn"] as ObservableCollection<ItemModel>;
            dataGrid1.ItemsSource = _ListGrid_Temp;
            ListGridRef = dataGrid1;
        }

        private void dataGrid1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            _ListGrid_Temp = App.Current.Properties["GETStock"] as ObservableCollection<StockTransferModel>;
            int i = dataGrid1.SelectedIndex;
            var editedTextbox = e.EditingElement as TextBox;
            _ListGrid_Temp[i].TRANS_QUANTITY = Convert.ToInt32(editedTextbox.Text);
            dataGrid1.ItemsSource = _ListGrid_Temp;
            App.Current.Properties["GETStock"] = _ListGrid_Temp;
            //if (editedTextbox != null)
            //    MessageBox.Show("Value after edit: " + editedTextbox.Text);
        }

        private void dataGrid1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                var get = _ListGrid_Temp1;
                _ListGrid_Temp = App.Current.Properties["GETStock"] as ObservableCollection<StockTransferModel>;
                int i = dataGrid1.SelectedIndex;

                _ListGrid_Temp.RemoveAt(i);
                App.Current.Properties["GETStock"] = _ListGrid_Temp;
                dataGrid1.ItemsSource = _ListGrid_Temp;


            }
        }

        private void ITEM_NAME_TextChanged(object sender, TextChangedEventArgs e)
        {
            _StockTransferViewModel.GetStockbybarcode();
        }

        private void SEARCH_CODE_TextChanged(object sender, TextChangedEventArgs e)
        {
            _StockTransferViewModel.GetStockbybarcode();
        }

        //private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    object item = dataGrid1.SelectedItem;
        //    string ID = (dataGrid1.SelectedCells[0].Column.GetCellContent(item) as TextBox).Text;
        //    MessageBox.Show(ID);
        //}
    }
}
