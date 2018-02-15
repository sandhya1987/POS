using InvoicePOS.Models;
using InvoicePOS.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace InvoicePOS.UserControll.Payment
{
    /// <summary>
    /// Interaction logic for AddReceivePayment.xaml
    /// </summary>
    public partial class AddReceivePayment : Window
    {
        ReceivePaymentViewModel _PaymentViewModel;
        public static TextBox CustomerRecivePaymentReff;
        public static TextBox ReceivePaymentBussReff;
        public static TextBox ReceiveCustomer;
        public static TextBox ReceiveCustomeremail;
        public static TextBox ReceiveCustomercontact;
        public static TextBox getreceivePay;
        public static DataGrid ListGridRef;
        public ObservableCollection<GetReceiveAmt> ListGrid1 = new ObservableCollection<GetReceiveAmt>();
        ReceivePaymentViewModel rcv = new ReceivePaymentViewModel();
        decimal amount = 0;
        decimal amount1 = 0;
        public AddReceivePayment()
        {
            InitializeComponent();
            _PaymentViewModel = new ReceivePaymentViewModel();
            this.DataContext = _PaymentViewModel;

            ObservableCollection<GetReceiveAmt> ListGrid1 = new ObservableCollection<GetReceiveAmt>();
            BussLocation.Text = "";
            ReceivePaymentBussReff = BussLocation;

            Customer.Text = "";
            CustomerRecivePaymentReff = Customer;
            Customer_email.Text = "";
            ReceiveCustomeremail = Customer_email;
            CUSTOMER_CONTACT_NO.Text = "";
            ReceiveCustomercontact = CUSTOMER_CONTACT_NO;
            recivepay.Text = "";
            getreceivePay = recivepay;
            dataGrid1.ItemsSource = ListGrid1;
            ListGridRef = dataGrid1;

        }
        private void chkDiscontinue_Click(object sender, RoutedEventArgs e)
        {
            //rcv.getcheck(e);
            try
            {


                CheckBox checkBox = (CheckBox)e.OriginalSource;
                DataGridRow dataGridRow = FindAncestor<DataGridRow>(checkBox);

                GetReceiveAmt produit1 = (GetReceiveAmt)dataGridRow.DataContext;
                if ((bool)checkBox.IsChecked)
                {

                    amount = Convert.ToDecimal(produit1.Pending_Amount) + amount;

                }
                else
                {
                    amount = amount - Convert.ToDecimal(produit1.Pending_Amount);
                }

                totselectamount.Text = amount.ToString();
                showpendingamt.Text = amount.ToString();
                produit1.TOTAL_REC_AMT1 = amount;
                App.Current.Properties["pendamt"] = amount;

            }
            catch
            { }
        }
        public static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            current = VisualTreeHelper.GetParent(current);
            while (current != null)
            {
                if (current is T)
                {
                    return (T)current;
                }

                current = VisualTreeHelper.GetParent(current);
            };
            return null;
        }

        private void CASH_REG_AMT_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.Current.Properties["pendamt"] != null)
            {
                try
                {

                    amount1 = Convert.ToDecimal(CASH_REG_AMT.Text);
                    if (Convert.ToDecimal(App.Current.Properties["pendamt"].ToString()) < amount1)
                    {
                        totreceiveamt.Text = CASH_REG_AMT.Text;
                        returnable.Text = (amount1 - Convert.ToDecimal(App.Current.Properties["pendamt"].ToString())).ToString();
                        showpendingamt.Text = "0";
                    }
                    else
                    {
                        amount1 = Convert.ToDecimal(App.Current.Properties["pendamt"].ToString()) - amount1;
                        showpendingamt.Text = amount1.ToString();
                        totreceiveamt.Text = CASH_REG_AMT.Text;
                        returnable.Text = "0";
                    }
                }
                catch
                {
                    if (App.Current.Properties["pendamt"] != null)
                    {
                        totselectamount.Text = App.Current.Properties["pendamt"].ToString();
                        showpendingamt.Text = App.Current.Properties["pendamt"].ToString();
                        
                        totreceiveamt.Text = "0";
                        returnable.Text = "0";
                    }
                }
            }
        }
    }
}
