
using InvoicePOS.ViewModels;
using InvoicePOS.Views.CashRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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

namespace InvoicePOS.Views.Main
{
    /// <summary>
    /// Interaction logic for PayNow.xaml
    /// </summary>
    public partial class PayNow : Window
    {
        InvoiceViewModel ViewModel;
        public static TextBox InvoiceCustomerReff;
        public static TextBox InvoiceChequeBankBranchReff;
        public static TextBox InvoiceChequeBankAcReff;
        public static TextBox InvoicetransferBankAcReff;
        public static TextBox InvoicetransferBankBranchReff;
        public static TextBox InvoiceFinancialBankAccountReff;
        public static TextBox InvoiceCustomerMobileReff;
        public static TextBox InvoiceCustomerEmailReff;
        public static TextBlock InvoiceCustomerLimits;
        public static TextBlock InvoiceCustomerLimits1;
        public static TextBox Cash_RegName;
        public static TextBox Received_Amount;
        public static TextBlock Available_Amount;
        public PayNow()
        {
            InitializeComponent();
            ViewModel = new InvoiceViewModel();
            this.DataContext = ViewModel;

            Customer.Text = "";
            InvoiceCustomerReff = Customer;

            CHEQUE_BANK_BRANCH.Text = "";
            InvoiceChequeBankBranchReff = CHEQUE_BANK_BRANCH;

            CHEQUE_BANK_AC.Text = "";
            InvoiceChequeBankAcReff = CHEQUE_BANK_AC;

            TRANSFER_BANK_AC.Text = "";
            InvoicetransferBankAcReff = TRANSFER_BANK_AC;

            TRANSFER_BANK_BRANCH.Text = "";
            InvoicetransferBankBranchReff = TRANSFER_BANK_BRANCH;

            FINACIAL_AC.Text = "";
            InvoiceFinancialBankAccountReff = FINACIAL_AC;

            MobileNo.Text = "";
            InvoiceCustomerMobileReff = MobileNo;

            //CreditAmount.Text = "";
            //Available_Amount = CreditAmount;

            Email.Text = "";
            InvoiceCustomerEmailReff = Email;
            GetCreditsLimits.Text = "";
            InvoiceCustomerLimits = GetCreditsLimits;
            GetCreditsLimits1.Text = "";
            InvoiceCustomerLimits1 = GetCreditsLimits1;
            //textboxCash.Text = "";
            //Cash_RegName = textboxCash;
            //BussLoc.Text = App.Current.Properties["BussMainName"].ToString();
            CASH_REG_NAME.Text = "";
            Cash_RegName = CASH_REG_NAME;
            //PrintDialog imgControlPrint = new PrintDialog();
            /////img Control wpf
            //imageCapture.Source = InvoiceViewModel.CopyScreen();
            //if ((bool)imgControlPrint.ShowDialog().GetValueOrDefault())
            //{
            //    imageCapture.Measure(new Size(imgControlPrint.PrintableAreaWidth, imgControlPrint.PrintableAreaHeight));
            //    imageCapture.Arrange(new Rect(new Point(0, 0), imageCapture.DesiredSize));
            //    imgControlPrint.PrintVisual(imageCapture, "Screen Shot");
            //}
          

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowPendingAmtLbl.Visibility = Visibility.Visible;
            ShowPendingAmtText.Visibility = Visibility.Visible;
            Received_Amount = textbox;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            PrintCharts(this.ReportPanel);
            
            
        }
        private void PrintCharts(Grid grid)
        {
            PrintDialog print = new PrintDialog();
            if (print.ShowDialog() == true)
            {
                PrintCapabilities capabilities = print.PrintQueue.GetPrintCapabilities(print.PrintTicket);

                double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / grid.ActualWidth,
                                        capabilities.PageImageableArea.ExtentHeight / grid.ActualHeight);

                Transform oldTransform = grid.LayoutTransform;

                grid.LayoutTransform = new ScaleTransform(scale, scale);

                Size oldSize = new Size(grid.ActualWidth, grid.ActualHeight);
                Size sz = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);
                grid.Measure(sz);
                ((UIElement)grid).Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight),
                    sz));

                print.PrintVisual(grid, "Print Results");
                grid.LayoutTransform = oldTransform;
                grid.Measure(oldSize);

                ((UIElement)grid).Arrange(new Rect(new Point(0, 0),
                    oldSize));
            }
        }
    }
}
