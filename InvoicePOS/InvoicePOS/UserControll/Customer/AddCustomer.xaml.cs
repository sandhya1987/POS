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
using InvoicePOS.Models;
using InvoicePOS.Views.Autocomplete;

namespace InvoicePOS.UserControll.Customer
{
    /// <summary>
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {
        CustomerViewModel _CVM;
        public static TextBox LoyaltyRef;
        public static CustomerAutoComplete ReferredRef;
        public static AutoCustGroup CustGroupRef;
        public static TextBox BusinessLoc;
        public static TextBlock OpBalReff;
        public static TextBlock showloyality;
        public static TextBlock BalanceTyprReff;
        //public static TextBox emailAddressReff;
        public static TextBox ShippingemailAddressReff;
        public static Image ImageReff;
        public AddCustomer()
        {
            InitializeComponent();
            _CVM = new CustomerViewModel();
            this.DataContext = _CVM;

            LOYALTY_NO.Text = "";
            LoyaltyRef = LOYALTY_NO;

            textBlock7.Text = "";
            showloyality = textBlock7;
            

            SHIPPING_EMAIL_ADDRESS.Text = "";
            ShippingemailAddressReff = SHIPPING_EMAIL_ADDRESS;

            ImgSource.Source = null;
            ImageReff = ImgSource;


            var CustNameList = App.Current.Properties["AutoCustList"] as List<CustomerAutoCompleteListModel>;
            foreach (var item in CustNameList)
            {
                REFERRED_BY.AddItem(new CustomerAutoCompleteListModel
                {
                    DisplayId = item.DisplayId,
                    DisplayName = item.DisplayName
                });
            }


            REFERRED_BY.Text = "";
            ReferredRef = REFERRED_BY;

            var CustGroupList = App.Current.Properties["AutoCustGroup"] as List<CustomerGroupAutoModel>;
            foreach (var item in CustGroupList)
            {
                textBox6.AddItem(new CustomerGroupAutoModel
                {
                    DisplayId = item.DisplayId,
                    DisplayName = item.DisplayName
                });
            }

            textBox6.Text = "";
            CustGroupRef = textBox6;

            //textboxBusLoc.Text = "";
            //BusinessLoc = textboxBusLoc;

            textBlock17_Copy.Text = ""; 
             OpBalReff = textBlock17_Copy;


            textBlock17_Copy1.Text = "";
            BalanceTyprReff = textBlock17_Copy1;

            if (App.Current.Properties["Action"] != null)
            {
                ImgSource.Source = App.Current.Properties["ItemViewImg"] as ImageSource;
                App.Current.Properties["ItemViewImg"] = null;
            }
            if (App.Current.Properties["getTotalPending"] != null)
            {
                textBlock7.Text = App.Current.Properties["getTotalPending"].ToString();
            }
            //if (App.Current.Properties["Action1"] == "1")
            //{
            //    ImgSource.Source = null;
            //    App.Current.Properties["Action1"] = null;

            //}
        }

       
    }
}
