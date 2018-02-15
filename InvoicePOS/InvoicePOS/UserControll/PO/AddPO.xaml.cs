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
using InvoicePOS;
using InvoicePOS.Models;
using System.Collections.ObjectModel;

namespace InvoicePOS.UserControll.PO
{
    /// <summary>
    /// Interaction logic for AddPO.xaml
    /// </summary>
    public partial class AddPO : Window
    {
        POViewModel _POViewModel;
        public static TextBox BussRef;
        public static TextBox ItemRef;
        public static TextBox SuppRef;
        public static TextBox SuppEmailRef;
        public static TextBox DeliveryRef;
        public static DataGridTextColumn TotalCelRef;
        public static TextBox TotRef;
        public static TextBox ScrRef;
        public static TextBox TotTaxRef;
        public static TextBox TotalQty;
        public static DataGrid ListGridRef;
        public static TextBox PoNo;
        ObservableCollection<POrderModel> AddListGrid1 = new ObservableCollection<POrderModel>();
        public AddPO()
        {
            InitializeComponent();
            _POViewModel = new POViewModel();
            this.DataContext = _POViewModel;
            //ObservableCollection<POrderModel> ListGrid1 = new ObservableCollection<POrderModel>();
            ObservableCollection<ItemModel> ListGrid1 = new ObservableCollection<ItemModel>();
           
            textBox2_Copy4.Text = "";
            BussRef = textBox2_Copy4;


            textBox5.Text = "";
            ItemRef = textBox5;

            textScr.Text = "";
            ScrRef = textScr; 

            textmail.Text = "";
            SuppEmailRef = textmail; 

            dataGrid1.ItemsSource = ListGrid1;
            ListGridRef = dataGrid1;

            total.Text = "";
            TotRef = total;

            totTax.Text = "";
            TotTaxRef = totTax;

            txtqty.Text = "";
            TotalQty = txtqty;

            textBox4.Text = "";
            SuppRef = textBox4;

                DeliveryPo.Text = "";
                DeliveryRef = DeliveryPo;

                txtpono.Text = "";
                PoNo = txtpono;

                
        }

        private void dataGrid1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {

                AddListGrid1 = App.Current.Properties["DataGridPO"] as ObservableCollection<POrderModel>;
                int i = dataGrid1.SelectedIndex;
                AddListGrid1.RemoveAt(i);
                App.Current.Properties["DataGridPO"] = AddListGrid1;
                dataGrid1.ItemsSource = AddListGrid1;
               

            }
        }

        private void dataGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (dataGrid1.SelectedItem != null)
                {
                    this.dataGrid1.Items.Remove(dataGrid1.SelectedItem);
                }
            }
        }
    }
}

