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
using InvoicePOS.Models;

namespace InvoicePOS.UserControll.SuppPayment
{
    /// <summary>
    /// Interaction logic for SuppPayAdd.xaml
    /// </summary>
    public partial class SuppPayAdd : Window
    {
        SuppPayViewModel _SuppVwModel;
        public static TextBox BussRef;
        public static TextBox SuppRef;
        public static TextBox ChequeBranchRef;
        public static TextBox ChequeAcRef;
        public static TextBox BankAcRef;
        public static TextBox BankBranchRef;
        public static TextBox FinancialAcRef;
        public static TextBox SuppEmail;
        public static TextBox Gencode;
        public static TextBox TOTALPAYMENT_MODES;
        public static TextBox SuppSMS;
        public static TextBox TotPendingRef;
        public static TextBox TotCashRef;
        public static DataGrid ListGridRef1;
        public SuppPaymentModel SelectedItem;
        public static SuppPaymentModel suppay = new SuppPaymentModel();
        public ObservableCollection<SuppPaymentModel> ListGrid12 = new ObservableCollection<SuppPaymentModel>();
        public ObservableCollection<SuppPaymentModel> ListGrid1 = new ObservableCollection<SuppPaymentModel>();
        SuppPaymentModel[] _ListGrid_Temp;
        public SuppPaymentModel[] _ListGrid_Temp1;
        decimal amount = 0;
        decimal amount1 = 0;
        public SuppPayAdd()
        {
            InitializeComponent();
            _SuppVwModel = new SuppPayViewModel();
            this.DataContext = _SuppVwModel;
            ObservableCollection<SuppPaymentModel> ListGrid1 = new ObservableCollection<SuppPaymentModel>();

            textBox.Text = "";
            BussRef = textBox;

            textBox1.Text = "";
            SuppRef = textBox1;

            SUPP_EMAIL.Text = "";
            SuppEmail = SUPP_EMAIL;

            SUPP_SMS.Text = "";
            SuppSMS = SUPP_SMS;
            //try
            //{
            //    if (Gencode.Text != "")
            //    {

            //    }
            //}
            //catch
            //{ }
            //textBox_Copy.Text = "";

            //Gencode = textBox_Copy;

            TOTAL_PAYMENT_MODES.Text = "";
            TOTALPAYMENT_MODES = TOTAL_PAYMENT_MODES;

            CHEQUE_BANK_BRANCH.Text = "";
            ChequeBranchRef = CHEQUE_BANK_BRANCH;

            CHEQUE_BANK_AC.Text = "";
            ChequeAcRef = CHEQUE_BANK_AC;

            TRANSFER_BANK_AC.Text = "";
            BankAcRef = TRANSFER_BANK_AC;

            TRANSFER_BANK_BRANCH.Text = "";
            BankBranchRef = TRANSFER_BANK_BRANCH;

            FINACIAL_AC.Text = "";
            FinancialAcRef = FINACIAL_AC;

            CASH_REG_AMT.Text = "";
            TotPendingRef = CASH_REG_AMT;

            TOTAL_PANDING.Text = "";
            TotCashRef = TOTAL_PANDING;

            dataGrid1.ItemsSource = ListGrid1;
            ListGridRef1 = dataGrid1;

        }
        private void chkDiscontinue_Click(object sender, RoutedEventArgs e)
        {
            //rcv.getcheck(e);
            try
            {


                CheckBox checkBox = (CheckBox)e.OriginalSource;
                DataGridRow dataGridRow = FindAncestor<DataGridRow>(checkBox);
                int i = dataGrid1.SelectedIndex;
                if (App.Current.Properties["getAllSupplierList"] != null)
                {
                    ListGrid1 = App.Current.Properties["getAllSupplierList"] as ObservableCollection<SuppPaymentModel>;
                }

                SuppPaymentModel produit1 = (SuppPaymentModel)dataGridRow.DataContext;
                if ((bool)checkBox.IsChecked)
                {
                    ListGrid1[i].ischecck = true;
                    amount = Convert.ToDecimal(produit1.PENDING_AMT) + amount;
                    produit1.ischecck = true;


                }
                else
                {
                    ListGrid1[i].ischecck = false;
                    amount = amount - Convert.ToDecimal(produit1.PENDING_AMT);
                    produit1.ischecck = false;
                }
                App.Current.Properties["getAllSupplierList"] = ListGrid1;
                //SelectedItem.getAllSupplier = ListGrid1;
                SELECTED_AMT.Text = amount.ToString();
                //dataGrid1.ItemsSource = ListGrid1;
                //TOTAL_PANDING.Text = amount.ToString();
                //produit1.TOTAL_REC_AMT1 = amount;
                App.Current.Properties["Supppendamt"] = amount;
                //if (App.Current.Properties["GEtSupplierName"] != null)
                //{
                //    textBox1.Text = App.Current.Properties["GEtSupplierName"].ToString();
                //}
                if (App.Current.Properties["getTotalPending"] != null)
                {
                    TOTAL_PANDING.Text = App.Current.Properties["getTotalPending"].ToString();
                    PENDING_AFTE_PAYMENT.Text = App.Current.Properties["getTotalPending"].ToString();
                }
            }
            catch(Exception ex)
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
            if (App.Current.Properties["getTotalPending"] != null)
            {
                try
                {

                    amount1 = Convert.ToDecimal(CASH_REG_AMT.Text);
                    if (Convert.ToDecimal(App.Current.Properties["getTotalPending"].ToString()) < amount1)
                    {
                        TOTAL_PAYMENT_MODES.Text = CASH_REG_AMT.Text;
                        CURRENT_PAYMENT.Text = (amount1 - Convert.ToDecimal(App.Current.Properties["getTotalPending"].ToString())).ToString();
                        PENDING_AFTE_PAYMENT.Text = "0";
                    }
                    else
                    {
                        amount1 = Convert.ToDecimal(App.Current.Properties["getTotalPending"].ToString()) - amount1;
                        PENDING_AFTE_PAYMENT.Text = amount1.ToString();
                        TOTAL_PAYMENT_MODES.Text = CASH_REG_AMT.Text;
                        CURRENT_PAYMENT.Text = "0";
                    }
                }
                catch
                {
                    if (App.Current.Properties["Supppendamt"] != null)
                    {
                        PENDING_AFTE_PAYMENT.Text = App.Current.Properties["getTotalPending"].ToString();
                        //showpendingamt.Text = App.Current.Properties["Supppendamt"].ToString();

                        TOTAL_PAYMENT_MODES.Text = "0";
                        CURRENT_PAYMENT.Text = "0";
                    }
                }
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            _SuppVwModel.getadjust();
            //if (CASH_REG_AMT.Text != "0" && CASH_REG_AMT.Text != "")

            //{
            //    decimal gettotpaid = Convert.ToDecimal(CASH_REG_AMT.Text);
            //    decimal adjustamout = 0;
            //    decimal totalpending = 0;
            //    int getindex = dataGrid1.SelectedIndex;
            //    if (App.Current.Properties["getAllSupplierList"] != null)
            //    {
            //        ListGrid1 = App.Current.Properties["getAllSupplierList"] as ObservableCollection<SuppPaymentModel>;
            //    }
            //    for (int i = 0; i < ListGrid1.Count; i++)
            //    {
            //        if (i != getindex)
            //        {
            //            adjustamout = Convert.ToDecimal(ListGrid1[i].TOTAL_AMT) + adjustamout;
            //            ListGrid1[i].ROUND_OFF_ADJUSTMENTAMT = Convert.ToDecimal(ListGrid1[i].TOTAL_AMT);
            //            ListGrid1[i].PENDING_AMT = 0;
            //        }

            //    }
            //    adjustamout = gettotpaid - adjustamout;
            //    ListGrid1[getindex].ROUND_OFF_ADJUSTMENTAMT = adjustamout;
            //    ListGrid1[getindex].PENDING_AMT = Convert.ToDecimal(ListGrid1[getindex].TOTAL_AMT) - adjustamout;
            //    dataGrid1.ItemsSource = null;
            //    ListGridRef1.ItemsSource = null;
            //    dataGrid1.ItemsSource = ListGrid1;
            //    ListGridRef1.ItemsSource = ListGrid1;
            //    for (int n = 0; n < ListGrid1.Count; n++)
            //    {
            //        if (ListGrid1[n].ischecck == true)
            //        {
            //            totalpending = totalpending + ListGrid1[n].TOTAL_PANDING;
            //        }
            //    }
            //    //SELECTED_AMT.Text = totalpending.ToString();
            //    //TOTALPAYMENT_MODES.Text = (totalpending - Convert.ToDecimal(CASH_REG_AMT.Text)).ToString();

            //    App.Current.Properties["getAllSupplierList"] = ListGrid1;
            //    App.Current.Properties["getAdjutamount"] = 1;
            //    //suppay.getAllSupplier = ListGrid1;
            //    //TextBlock textblock = (TextBlock)sender;
            //    //decimal id = (decimal)textblock.Tag;
            //    //decimal getafterselect = Convert.ToDecimal(TOTAL_PANDING.Text) - id;

            //    //if (App.Current.Properties["getData"] != null)
            //    //{
            //    //    _ListGrid_Temp1 = App.Current.Properties["getData"] as SuppPaymentModel[];

            //    //    foreach (SuppPaymentModel a in _ListGrid_Temp1)
            //    //    {
            //    //        if (a.ROUND_OFF_ADJUSTMENTAMT == id)
            //    //        {
            //    //            a.ROUND_OFF_ADJUSTMENTAMT = Convert.ToDecimal(CASH_REG_AMT.Text);
            //    //        }
            //    //    }

            //    //}
            //}
            //else
            //{
            //    MessageBox.Show("Please enter payble amount");
            //    return;
            //}
        }

        private void DataGridTemplateColumn_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }
    }
}
