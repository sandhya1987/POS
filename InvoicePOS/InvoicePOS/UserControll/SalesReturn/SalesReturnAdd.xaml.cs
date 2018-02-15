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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
namespace InvoicePOS.UserControll.SalesReturn
{
    /// <summary>
    /// Interaction logic for SalesReturnAdd.xaml
    /// </summary>
    /// 
    public partial class SalesReturnAdd : Window
    {
        SalesReturnViewModel _SalesReturnModel;
        public static TextBox BussReff;
        public static TextBox GodownReff;
        public static TextBox InvoiceReff;
        public static DataGrid ListGridRef;
        public static TextBox TotalReff;
        public static TextBox CusReff;
        public static TextBox GrandReff;
        public static TextBox QtyReff;
        public static TextBox TotitemReff;
        public static TextBox SlNo;
        ObservableCollection<TaxManagementModel> ListGrid= new ObservableCollection<TaxManagementModel>();
        public SalesReturnAdd()
        {
            InitializeComponent();
            _SalesReturnModel = new SalesReturnViewModel();
            this.DataContext = _SalesReturnModel;

            BussLocation.Text = "";
            BussReff = BussLocation;

            invoice.Text = "";
            InvoiceReff = invoice;

            dataGrid1.ItemsSource = ListGrid;
            ListGridRef = dataGrid1;

            GodownSalesReturn.Text = "";
            GodownReff = GodownSalesReturn;

            tot.Text = "";
            TotalReff = tot;

            grandtot.Text = "";
            GrandReff = grandtot;

            cus.Text = "";
            CusReff = cus;

            qty.Text = "";
            QtyReff = qty;

            totitem.Text="";
            TotitemReff=totitem;

            slno.Text = "";
            SlNo = slno;
        }

        private void slno_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
