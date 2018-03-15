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
using InvoicePOS.ViewModels.WelCome;
using InvoicePOS.UserControll;
using InvoicePOS.UserControll.Customer;
//using InvoicePOS.UserControll.Vendor;
using InvoicePOS.UserControll.PO;
using InvoicePOS.UserControll.Employee;
using InvoicePOS.UserControll.Item;
using InvoicePOS.Helpers;
using InvoicePOS.UserControll.StockTransfer;
using InvoicePOS.UserControll.SuppPayment;
using InvoicePOS.UserControll.Cash_Reg;
using InvoicePOS.UserControll.Supplier;
using InvoicePOS.UserControll.SalesReturn;
using InvoicePOS.UserControll.Payment;
using InvoicePOS.Views.Main;
using System.Threading;

namespace InvoicePOS.Views.WelCome
{
    /// <summary>
    /// Interaction logic for WelComePage.xaml
    /// </summary>
    public partial class WelComePage : Window, IModalService
    {
        WelComeViewModel _WCVM = new WelComeViewModel();
        public static Button CustReff;
        public static Button ItemPRef;
        public static Button VenReff;
        public static Button POrdReff;
        public static Button ReptdReff;
        public static Button InvoiceReff;
        public static Button dailySalesReff;
        public static Button stockLegerReff;
        public static Button UnitReff;
        public static Button catagoryReff;
        public static Button ItemLocationReff;
        public static Button GodownReff;
        public static Button BussLocationReff;
        public static Button payrecivedReff;
        public static Button Employee1Reff;
        public static Button cashRegReff;
        public static Button transfercashReff;
        public static Button SalesReturnReff;
        public static Button RecItemReff;
        public static Button SubpayReff;
        public static Button StockReff;
        public static Button bankReff;
        public static Button TaxReff;
        public static Button LoyaltyReff;
        public static Button AccessReff;
        public static Button DesignationReff;
        public static Button DepermentReff;
        public static Button RInvoiceReff;
        public static Button EstimateReff;
        public static Button SettingsReff;
        public static Button DashboardReff;
        public static Button ReportReff;
        private Thread t = null;


        public WelComePage()
        {
            InitializeComponent();
            _WCVM = new WelComeViewModel();
            this.DataContext = _WCVM;
            ItemPRef = Product;
            CustReff = Customer;
            VenReff = Vendor;
            POrdReff = POrder;
            ReptdReff = Report;
            InvoiceReff = Invoice;
            //dailySalesReff = dailySales;
            stockLegerReff = stockLeger;
            UnitReff = Unit;
            catagoryReff = catagory;
            ItemLocationReff = ItemLocation;
            GodownReff = Godown;
            BussLocationReff = BussLocation;
            payrecivedReff = payrecived;
            Employee1Reff = Employee1;
            cashRegReff = cashReg;
            transfercashReff = TransferCash;
            SalesReturnReff = SalesReturn;
            RecItemReff = RecItem;
            SubpayReff = Subpay;
            StockReff = Stock;
            bankReff = bank;
            TaxReff = Tax;
            LoyaltyReff = Loyalty;
            AccessReff = Access;
            DesignationReff = Designation;
            DepermentReff = Deperment;
            RInvoiceReff = RInvoice;
            EstimateReff = Estimate;
            SettingsReff = Settings;
            DashboardReff = Dashboard;
            ReportReff = Report;
        }

        private void Synchronize(object sender, RoutedEventArgs e)
        {
            CreateNewThread();
        }

        public void CreateNewThread()
        {
            t = ((App)Application.Current).SyncThread;
            ProgressBarWindow pbw;
            if ((t == null) || (!t.IsAlive))
            {
                t = new Thread(delegate()
                {
                    pbw = new ProgressBarWindow();
                    pbw.Closing += OnProgressBarWindowClosed;
                    pbw.Show();
                    System.Windows.Threading.Dispatcher.Run();
                });
                t.SetApartmentState(ApartmentState.STA);
                t.IsBackground = true;
                t.Start();
                ((App)Application.Current).SyncThread = t;
            }

        }

        void OnProgressBarWindowClosed(object sender, EventArgs e)
        {
            System.Windows.Threading.Dispatcher.FromThread(t).InvokeShutdown();
        }

        #region IMainWindow Members

        private Stack<BackNavigationEventHandler> _backFunctions
            = new Stack<BackNavigationEventHandler>();

        void IModalService.NavigateTo(UserControl uc, BackNavigationEventHandler backFromDialog)
        {
            foreach (UIElement item in modalGrid.Children)
            {
                item.IsEnabled = false;
                item.Visibility = System.Windows.Visibility.Collapsed;
            }
            modalGrid.Children.Add(uc);

            _backFunctions.Push(backFromDialog);
        }

        void IModalService.GoBackward(bool dialogReturnValue)
        {
            modalGrid.Children.RemoveAt(modalGrid.Children.Count - 1);

            UIElement element = modalGrid.Children[modalGrid.Children.Count - 1];
            element.IsEnabled = true;

            BackNavigationEventHandler handler = _backFunctions.Pop();
            if (handler != null)
                handler(dialogReturnValue);
        }


        public static IModalService ModalService
        {
            get { return (IModalService)Application.Current.MainWindow; }
        }
        #endregion





    }
}
