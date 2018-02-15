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

namespace InvoicePOS.Views.ChangeItem
{
    /// <summary>
    /// Interaction logic for ChangeItemPrice.xaml
    /// </summary>
    public partial class ChangeItemPrice : Window
    {
        ChangeItemViewModel ViewModel;
        public static TextBox ItemCode;
        public static TextBox ItemName;
        public static TextBox ItemOldMRP;
        public static TextBox ItemOldPrice;


        public ChangeItemPrice()
        {

            //textBox.Text = "";
            //ItemCode = textBox;

            //textBox1.Text = "";
            //ItemName = textBox1;

            //textBox2.Text = "";
            //ItemOldMRP = textBox2;

            //textBox3.Text = "";
            //ItemOldPrice = textBox3;
            //var data = App.Current.Properties["ChangePriceItem"] as ItemModel;
            //ItemCode.Text = Convert.ToString(data.ITEM_ID);
            //textBox1.Text = data.ITEM_NAME;
            //textBox2.Text = Convert.ToString(data.MRP);
            //textBox3.Text = Convert.ToString(data.ITEM_PRICE);


            InitializeComponent();
            ViewModel = new ChangeItemViewModel();

            this.DataContext = ViewModel;

            var data = App.Current.Properties["SelectData"] as ItemModel;
            textBox.Text = "";
            textBox.Text = Convert.ToString(data.ITEM_ID);

            textBox1.Text = "";
            textBox1.Text = Convert.ToString(data.ITEM_NAME);

            textBox2.Text = "";
            textBox2.Text = Convert.ToString(data.MRP);

            textBox3.Text = "";
            textBox3.Text = Convert.ToString(data.SALES_PRICE);

           
            //textBox5.Text = "";
            //textBox5.Text = "";
            //App.Current.Properties["NewMRP"] = textBox5;
            //App.Current.Properties["NewSalesPrice"] = textBox6;
            //textBox5.Text = "";
            //ItemSalesPrice = textBox5;

            //textBox7.Text = "";
            //ItemMrp = textBox7;
            //ItemOldMRP.Text = textBox5.Text;
            //ItemOldPrice.Text = textBox6.Text;
            //var data = App.Current.Properties["ChangePriceItem"] as ItemModel;
            //ItemCode.Text = Convert.ToString(data.ITEM_ID);
            //ItemName.Text = data.ITEM_NAME;
            //ItemOldMRP.Text = Convert.ToString(data.MRP);
            //ItemOldPrice.Text = Convert.ToString(data.ITEM_PRICE);
        }

    }
}
