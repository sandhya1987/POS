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
using InvoicePOS.Models;
using System.Collections.ObjectModel;



namespace InvoicePOS.UserControll.Item
{
    /// <summary>
    /// Interaction logic for ReceiveAddItem.xaml
    /// </summary>
    public partial class ReceiveAddItem : Window
    {
        ReceiveItemViewModel _WCVM;
        public static TextBox BussRef;
        public static TextBox ItemReff;
        public static TextBox SearchItemReff;
        public static TextBox EmpReff;
        public static TextBox PoReff;
        public static TextBox GodownReff;
        public static TextBox SupplierReff;
        public static TextBox PaymentReff;
        public static DataGrid ListGridRef;
        public static TextBox TaxReff;
        public static TextBox SubTotalBfrTxReff;
        public static TextBox TotAmtReff;
        public static TextBox SubTotalReff;
        public static DataGrid ListGridItemRef;
        public static TextBox SubTotalRoundOffReff;
        public ReceiveAddItem()
        {
            InitializeComponent();
            _WCVM = new ReceiveItemViewModel();
            this.DataContext = _WCVM;
            ObservableCollection<ItemModel> ListGrid1 = new ObservableCollection<ItemModel>();
            ObservableCollection<ItemModel> ListGridd = new ObservableCollection<ItemModel>();

            SELECT_BUSINESS_LOCATION_ID.Text = "";
            BussRef = SELECT_BUSINESS_LOCATION_ID;

            ITEM_NAME.Text = "";
            ItemReff = ITEM_NAME;

            SEARCH_CODE.Text = "";
            SearchItemReff = SEARCH_CODE;

            textBox4_Copy.Text = "";
            EmpReff = textBox4_Copy;

            PO_NUMBER.Text = "";
            PoReff = PO_NUMBER;

            GODOWN_ID.Text = "";
            GodownReff = GODOWN_ID;

            SUPPLIER.Text = "";
            SupplierReff = SUPPLIER;

            PAYMENT_TERMS.Text = "";
            PaymentReff = PAYMENT_TERMS;

            TOTAL_TAX_AMT.Text = "";
            TaxReff = TOTAL_TAX_AMT;

            TOTAL_AMT.Text="";
            TotAmtReff = TOTAL_AMT;

            SUB_TOTAL.Text = "";
            SubTotalReff = SUB_TOTAL;

            SUB_TOTAL_BEFORETAX.Text = "";
            SubTotalBfrTxReff = SUB_TOTAL_BEFORETAX;

            ROUND_OFF_ADJUSTMENTAMT.Text="";
            SubTotalRoundOffReff = ROUND_OFF_ADJUSTMENTAMT;

            dataGrid1.ItemsSource = ListGrid1;
            ListGridRef = dataGrid1;

            dataGridd.ItemsSource = ListGridd;
            ListGridItemRef = dataGridd;
        }




        
        private void btnShowStock_Click(object sender, RoutedEventArgs e)
        {


            //MyStack.Children.Clear();
            //ItemAdd _AC = new ItemAdd();
            //MyStack.Children.Add(_AC);


        }
        private void btnApplyDiscount_Click(object sender, RoutedEventArgs e)
        {


            //MyStack.Children.Clear();
            //ItemAdd _AC = new ItemAdd();
            //MyStack.Children.Add(_AC);


        }
        

            
        
    }
}
