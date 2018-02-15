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

namespace InvoicePOS.UserControll.Item
{
    /// <summary>
    /// Interaction logic for ItemLocationAdd.xaml
    /// </summary>
    public partial class ItemLocationAdd : Window
    {
        ItemLocation ViewModel;
        public static TextBox ItemLocationReff;
        public static TextBox ItemLocReff;
        public static TextBox ItemSortReff;
        public static ComboBox comboReff;
        public static ComboBox combosortReff;
        public ItemLocationAdd()
        {
            InitializeComponent();
            ViewModel = new ItemLocation();
            this.DataContext = ViewModel;

            ItemName.Text = "";
            ItemLocationReff = ItemName;

            ItemLoc.Text = "";
            ItemLocReff = ItemLoc;

            com.Text = "";
            comboReff = com;

            com2.Text = "";
            combosortReff = com2;

            sort.Text = "";
            ItemSortReff = sort;
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
           
            //ItemLoc.Text = com.SelectedItem.ToString();
            //ItemLoc.Text = com.SelectedItem.ToString();
          
            //MessageBox.Show(com.SelectionBoxItem.ToString());
            //MessageBox.Show(com.Text);
            //ComboBoxItem citem = (ComboBoxItem)com.SelectedItem;
            //MessageBox.Show(citem.Content.ToString());

        }

        private void ItemLoc_TextChanged(object sender, TextChangedEventArgs e)
        {


            ItemLocation k = new ItemLocation();
            ItemLocReff.Text = ItemLoc.Text;
            k.GetValue11();

        }

    }
}
