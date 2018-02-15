using InvoicePOS.Helpers;
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
using WpfAutoComplete.Controls;
using InvoicePOS.Models;
using InvoicePOS.Views;
using System.Collections.ObjectModel;

namespace InvoicePOS.UserControll.Item
{
    /// <summary>
    /// Interaction logic for ItemAdd.xaml
    /// </summary>
    public partial class ItemAdd : Window
    {
        ItemViewModel _ItemViewModel;
        public static AutoCategoryList CatRef;
        public static AutoCompleteTaxPaid TaxRef1;
        public static AutoCompleteTaxCollect TaxRef2;
        public static AutoPurchaseList UnitRef;
        public static SaleUnitAutoComplete SaleUnitRef;
        public static TextBox SalesPrice;
        public static TextBox SalesPriceBeforeTax;
        public static TextBox PurchasePrice;
        public static TextBox BarCodeReff;
        public static TextBox ItemNameReff;
        public static CheckBox chk;

        List<string> lsState = new List<string>();
        public ItemAdd()
        {
            
            InitializeComponent();

            _ItemViewModel = new ItemViewModel();
            //textBox2.Text = App.Current.Properties["ManualBarcode"].ToString();
            //if (App.Current.Properties["ManualBarcode"].ToString() != "" || App.Current.Properties["ManualBarcode"].ToString() != null)
            //{
            //    textBox2.Text = App.Current.Properties["ManualBarcode"].ToString();
            //}
            this.DataContext = _ItemViewModel;
            var cdfr = App.Current.Properties["AutoCatList"] as List<CategoryAutoListModel>;
            foreach (var item in cdfr)
            {
                textBox2_Copy4.AddItem(new CategoryAutoListModel
                {
                    DisplayId = item.DisplayId,
                    DisplayName = item.DisplayName
                });
            }

            CatRef = textBox2_Copy4;

            var TaxPaid = App.Current.Properties["TaxPaidListAuto"] as List<AutoCompleteTax>;

            foreach (var item in TaxPaid)
            {
                textBox3.AddItem(new AutoCompleteTax
                {

                    DisplayId = item.DisplayId,
                    DisplayName = item.DisplayName

                });
                textBox3_Copy.AddItem(new AutoCompleteTax
                {

                    DisplayId = item.DisplayId,
                    DisplayName = item.DisplayName

                });
            }
            TaxRef1 = textBox3;
            TaxRef2 = textBox3_Copy;

            var UnitList = App.Current.Properties["unitAutoList"] as List<UnitAutoListModel>;
            foreach (var item in UnitList)
            {
                textBox4.AddItem(new UnitAutoListModel
                {

                    DisplayId = item.DisplayId,
                    DisplayName = item.DisplayName

                });
                textBox3_Copy1.AddItem(new UnitAutoListModel
                {

                    DisplayId = item.DisplayId,
                    DisplayName = item.DisplayName

                });
            }

            // textBox4.Text = "";
            UnitRef = textBox4;

            // textBox3_Copy1.Text = "";
            SaleUnitRef = textBox3_Copy1;

            textBox3_Copy2.Text = "";
            SalesPrice = textBox3_Copy2;

            SALES_PRICE_BEFOR_TAX.Text = "";
            SalesPriceBeforeTax = SALES_PRICE_BEFOR_TAX;
            SalesPriceBeforeTax = SALES_PRICE_BEFOR_TAX;

            textBox7.Text = "";
            PurchasePrice = textBox7;
            PurchasePrice = textBox7;

            textBox2.Text = "";
            BarCodeReff = textBox2;

            textBox2_Copy.Text = "";
            ItemNameReff = textBox2_Copy;


            //checkBox.Checked == null;
            chk = checkBox;

            
            if (App.Current.Properties["ItemEdit"] != null)
            {
                ImgSource.Source = App.Current.Properties["ItemViewImg"] as ImageSource;
                App.Current.Properties["ItemViewImg"] = null;
            }

        }

        private void txtTax_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.Current.Properties["TaxListsession"] == null)
            {


                var tyu = App.Current.Properties["TaxesList"] as List<string>;
                lsState = tyu;
                string typedstring = textBox3.Text;
                List<string> autoList = new List<string>();
                autoList.Clear();

                foreach (string item in lsState)
                {
                    if (!string.IsNullOrEmpty(textBox3.Text))
                    {
                        if (item.ToUpper().StartsWith(typedstring.ToUpper(), StringComparison.Ordinal))
                        {
                            autoList.Add(item);
                        }
                    }
                }
                if (autoList.Count() > 0)
                {
                    // lstState.ItemsSource = autoList;
                    // lstState.Visibility = Visibility.Visible;
                    //lstSuggession.Focus();
                }
                else if (textBox3.Text.Equals(""))
                {
                    //  lstState.ItemsSource = null;
                    //  lstState.Visibility = Visibility.Collapsed;
                    //txtCity.Focus();
                }
                else
                {
                    textBox3.Text = "";
                    // lstState.ItemsSource = null;
                    // lstState.Visibility = Visibility.Collapsed;
                    //txtCity.Focus();
                }
            }
            else
            {

                // lstState.Visibility = Visibility.Collapsed;
                App.Current.Properties["TaxListsession"] = "";
            }
            //checktextvalidate();
        }

        private void txtTaxColl_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.Current.Properties["TaxListsession"] == null)
            {
                var tyu = App.Current.Properties["TaxesList"] as List<string>;

                lsState = tyu;
                string typedstring = textBox3_Copy.Text;
                List<string> autoList = new List<string>();
                autoList.Clear();

                foreach (string item in lsState)
                {
                    if (!string.IsNullOrEmpty(textBox3_Copy.Text))
                    {
                        if (item.ToUpper().StartsWith(typedstring.ToUpper(), StringComparison.Ordinal))
                        {
                            autoList.Add(item);
                        }
                    }
                }
                if (autoList.Count() > 0)
                {
                    //lstTaxColl.ItemsSource = autoList;
                    //lstTaxColl.Visibility = Visibility.Visible;
                    //lstSuggession.Focus();
                }
                else if (textBox3_Copy.Text.Equals(""))
                {
                    // lstTaxColl.ItemsSource = null;
                    // lstTaxColl.Visibility = Visibility.Collapsed;
                    //txtCity.Focus();
                }
                else
                {
                    textBox3_Copy.Text = "";
                    //lstTaxColl.ItemsSource = null;
                    // lstTaxColl.Visibility = Visibility.Collapsed;
                    //txtCity.Focus();
                }
            }
            else
            {
                //lstTaxColl.Visibility = Visibility.Collapsed;
                App.Current.Properties["TaxListsession"] = null;
            }
        }

        private void lstTax_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (lstState.ItemsSource != null)
            //{
            //    lstState.Visibility = Visibility.Collapsed;
            //   // textBox3.TextChanged -= new TextChangedEventHandler(txtTax_TextChanged);
            //    if (lstState.SelectedIndex != -1)
            //    {
            //        textBox3.Text = lstState.SelectedItem.ToString();
            //    }
            //    //textBox3.TextChanged += new TextChangedEventHandler(txtTax_TextChanged);
            //}
        }






        private void lstTaxColl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (lstTaxColl.ItemsSource != null)
            //{
            //    lstTaxColl.Visibility = Visibility.Collapsed;
            //    textBox3_Copy.TextChanged -= new TextChangedEventHandler(txtTaxColl_TextChanged);
            //    if (lstTaxColl.SelectedIndex != -1)
            //    {
            //        textBox3_Copy.Text = lstTaxColl.SelectedItem.ToString();
            //    }
            //    textBox3_Copy.TextChanged += new TextChangedEventHandler(txtTaxColl_TextChanged);
            //}
        }
        #region IMainWindow Members

        private Stack<BackNavigationEventHandler> _backFunctions
            = new Stack<BackNavigationEventHandler>();

        //void IModalService.NavigateTo(UserControl uc, BackNavigationEventHandler backFromDialog)
        //{
        //    foreach (UIElement item in modalGrid.Children)
        //        item.IsEnabled = false;
        //    modalGrid.Children.Add(uc);

        //    _backFunctions.Push(backFromDialog);
        //}

        //void IModalService.GoBackward(bool dialogReturnValue)
        //{
        //    modalGrid.Children.RemoveAt(modalGrid.Children.Count - 1);

        //    UIElement element = modalGrid.Children[modalGrid.Children.Count - 1];
        //    element.IsEnabled = true;

        //    BackNavigationEventHandler handler = _backFunctions.Pop();
        //    if (handler != null)
        //        handler(dialogReturnValue);
        //}


        public static IModalService ModalService
        {
            get { return (IModalService)Application.Current.MainWindow; }
        }
        #endregion
    }
}
