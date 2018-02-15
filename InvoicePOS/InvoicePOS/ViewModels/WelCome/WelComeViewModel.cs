using InvoicePOS.Helpers;
using InvoicePOS.Views.WelCome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InvoicePOS.UserControll;
using System.ComponentModel;
using System.Windows.Controls;
using InvoicePOS.UserControll.Customer;
using InvoicePOS.Views.Main;
using InvoicePOS.UserControll.Item;
using InvoicePOS.Models;
using InvoicePOS.UserControll.StockTransfer;
using InvoicePOS.UserControll.Cash_Reg;
using InvoicePOS.UserControll.Supplier;
using InvoicePOS.UserControll.SalesReturn;
using InvoicePOS.UserControll.SuppPayment;
using InvoicePOS.UserControll.Employee;
using InvoicePOS.UserControll.Payment;
using InvoicePOS.UserControll.PO;
using InvoicePOS.UserControll.Company;
using InvoicePOS.UserControll.GoDown;
using InvoicePOS.UserControll.Category;
using InvoicePOS.UserControll.Unit;
using InvoicePOS.UserControll.StockLedger;
using InvoicePOS.UserControll.Loyalty;
using InvoicePOS.UserControll.Bank;
using InvoicePOS.UserControll.TaxManagement;
using InvoicePOS.UserControll.DailySales;
using InvoicePOS.UserControll.Report;
using InvoicePOS.UserControll.Invoice;
using InvoicePOS.UserControll.AccessRide;
using InvoicePOS.UserControll.Designation;
using InvoicePOS.UserControll.Department;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;
using System.Net;
using System.IO;
using InvoicePOS.UserControll.Estimate;
using InvoicePOS.Views.Company;
namespace InvoicePOS.ViewModels.WelCome
{
    public class WelComeViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {


        public WelComeViewModel()
        {
            ItemModel _ITM = new ItemModel();
            App.Current.Properties["Action"] = "";
            App.Current.Properties["ItemEdit"] = _ITM;
            SecondVisible = "Collapsed";
            FristVisible = "Visible";


        }
        private ICommand _LogOut { get; set; }
        public ICommand LogOut
        {
            get
            {
                if (_LogOut == null)
                {
                    _LogOut = new DelegateCommand(LogOutTill);
                }
                return _LogOut;
            }
        }

        public void LogOutTill()
        {

            Application.Current.Shutdown();
        }

        private ICommand _Loyalty { get; set; }
        public ICommand Loyalty
        {
            get
            {
                if (_Loyalty == null)
                {
                    _Loyalty = new DelegateCommand(LoyaltyPage);
                }
                return _Loyalty;
            }

        }

        public void LoyaltyPage()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));

            WelComePage.LoyaltyReff.Background = color;
            ModalService.NavigateTo(new LoyaltyList(), delegate(bool returnValue) { });
        }





        private ICommand _BankDetailsList { get; set; }
        public ICommand BankDetailsList
        {
            get
            {
                if (_BankDetailsList == null)
                {
                    _BankDetailsList = new DelegateCommand(Bank_DetailsList);
                }
                return _BankDetailsList;
            }

        }

        public void Bank_DetailsList()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));

            WelComePage.bankReff.Background = color;
            ModalService.NavigateTo(new BankList(), delegate(bool returnValue) { });
        }

        private ICommand _TaxManage { get; set; }
        public ICommand TaxManage
        {
            get
            {
                if (_TaxManage == null)
                {
                    _TaxManage = new DelegateCommand(Tax_Manage);
                }
                return _TaxManage;
            }

        }

        public void Tax_Manage()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));

            WelComePage.TaxReff.Background = color;
            ModalService.NavigateTo(new TaxList(), delegate(bool returnValue) { });
        }

        private ICommand _ACCESSRIDE { get; set; }
        public ICommand ACCESSRIDE
        {
            get
            {
                if (_ACCESSRIDE == null)
                {
                    _ACCESSRIDE = new DelegateCommand(ACCESS_RIDE);
                }
                return _ACCESSRIDE;
            }

        }

        public void ACCESS_RIDE()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));

            WelComePage.AccessReff.Background = color;
            ModalService.NavigateTo(new AccessRide(), delegate(bool returnValue) { });
        }

        private ICommand _ClickInvoice { get; set; }
        public ICommand ClickInvoice
        {
            get
            {
                if (_ClickInvoice == null)
                {
                    _ClickInvoice = new DelegateCommand(_Click_Invoice);
                }
                return _ClickInvoice;
            }

        }
        public void _Click_Invoice()
        {

            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));
            WelComePage.RInvoiceReff.Background = color;
            ModalService.NavigateTo(new Invoice(), delegate(bool returnValue) { });
        }

        private ICommand _ReportList { get; set; }
        public ICommand ReportList
        {
            get
            {
                if (_ReportList == null)
                {
                    _ReportList = new DelegateCommand(Report_List);
                }
                return _ReportList;
            }

        }

        public void Report_List()
        {

            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f6f7f9"));
            var color2 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f6f7f9"));
            var color3 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f6f7f9"));
            var color4 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f6f7f9"));
            var Rcolor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));
            WelComePage.ItemPRef.Background = color;
            WelComePage.CustReff.Background = color2;
            WelComePage.VenReff.Background = color3;
            WelComePage.POrdReff.Background = color4;
            WelComePage.ReptdReff.Background = Rcolor;



            ModalService.NavigateTo(new ReportList(), delegate(bool returnValue) { });
        }

        private ICommand _EstimateClick { get; set; }
        public ICommand EstimateClick
        {
            get
            {
                if (_EstimateClick == null)
                {
                    _EstimateClick = new DelegateCommand(Estimate_Click);
                }
                return _EstimateClick;
            }

        }

        public void Estimate_Click()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));

            WelComePage.EstimateReff.Background = color;
            ModalService.NavigateTo(new Estimate(), delegate(bool returnValue) { });
        }

        private ICommand _DailySales { get; set; }
        public ICommand DailySales
        {
            get
            {
                if (_DailySales == null)
                {
                    _DailySales = new DelegateCommand(Daily_Sales);
                }
                return _DailySales;
            }

        }

        public void Daily_Sales()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));

            WelComePage.dailySalesReff.Background = color;
            ModalService.NavigateTo(new DailySales(), delegate(bool returnValue) { });
        }


        private ICommand _LoyaltyList { get; set; }
        public ICommand LoyaltyList
        {
            get
            {
                if (_LoyaltyList == null)
                {
                    _LoyaltyList = new DelegateCommand(Loyalty_List);
                }
                return _LoyaltyList;
            }

        }

        public void Loyalty_List()
        {
            ModalService.NavigateTo(new LoyaltyList(), delegate(bool returnValue) { });
        }



        private ICommand _ItemLocationClick { get; set; }
        public ICommand ItemLocationClick
        {
            get
            {
                if (_ItemLocationClick == null)
                {
                    _ItemLocationClick = new DelegateCommand(ItemLocation_Click);
                }
                return _ItemLocationClick;
            }

        }

        public void ItemLocation_Click()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));

            WelComePage.ItemLocationReff.Background = color;
            ModalService.NavigateTo(new ItemLocationList(), delegate(bool returnValue) { });
        }
        private ICommand _CategoryListClick { get; set; }
        public ICommand CategoryListClick
        {
            get
            {
                if (_CategoryListClick == null)
                {
                    _CategoryListClick = new DelegateCommand(CategoryList_Click);
                }
                return _CategoryListClick;
            }

        }

        public void CategoryList_Click()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));

            WelComePage.catagoryReff.Background = color;
            ModalService.NavigateTo(new CategoryList(), delegate(bool returnValue) { });
        }

        private ICommand _UnitClick { get; set; }
        public ICommand UnitClick
        {
            get
            {
                if (_UnitClick == null)
                {
                    _UnitClick = new DelegateCommand(Unit_Click);
                }
                return _UnitClick;
            }

        }

        public void Unit_Click()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));

            WelComePage.ItemPRef.Background = color;
            ModalService.NavigateTo(new UnitList(), delegate(bool returnValue) { });
        }
        private ICommand _GodownListClick { get; set; }
        public ICommand GodownListClick
        {
            get
            {
                if (_GodownListClick == null)
                {
                    _GodownListClick = new DelegateCommand(GodownList_Click);
                }
                return _GodownListClick;
            }

        }

        public void GodownList_Click()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));

            WelComePage.GodownReff.Background = color;
            ModalService.NavigateTo(new GodownList(), delegate(bool returnValue) { });
        }





        private ICommand _ItemList { get; set; }
        public ICommand ItemList
        {
            get
            {
                if (_ItemList == null)
                {
                    _ItemList = new DelegateCommand(NewItemList);
                }
                return _ItemList;
            }

        }

        public void NewItemList()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));
            var color2 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f6f7f9"));
            var color3 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f6f7f9"));
            var color4 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f6f7f9"));
            var Rcolor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f6f7f9"));
            WelComePage.ItemPRef.Background = color;
            WelComePage.CustReff.Background = color2;
            WelComePage.VenReff.Background = color3;
            WelComePage.POrdReff.Background = color4;
            WelComePage.ReptdReff.Background = Rcolor;




            ModalService.NavigateTo(new Items(), delegate(bool returnValue) { });



        }
        private ICommand _LocationClick { get; set; }
        public ICommand LocationClick
        {
            get
            {
                if (_LocationClick == null)
                {
                    _LocationClick = new DelegateCommand(Location_Click);
                }
                return _LocationClick;
            }

        }

        public void Location_Click()
        {

            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));

            WelComePage.BussLocationReff.Background = color;
            ModalService.NavigateTo(new BussinessLocationList(), delegate(bool returnValue) { });



        }
        private ICommand _POListClick { get; set; }
        public ICommand POListClick
        {
            get
            {
                if (_POListClick == null)
                {
                    _POListClick = new DelegateCommand(POList_Click);
                }
                return _POListClick;
            }

        }

        public void POList_Click()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f6f7f9"));
            var color2 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f6f7f9"));
            var color3 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f6f7f9"));
            var color4 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));
            var Rcolor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f6f7f9"));
            WelComePage.ItemPRef.Background = color;
            WelComePage.CustReff.Background = color2;
            WelComePage.VenReff.Background = color3;
            WelComePage.POrdReff.Background = color4;
            WelComePage.ReptdReff.Background = Rcolor;
            ModalService.NavigateTo(new POList(), delegate(bool returnValue) { });
        }

        private ICommand _InvoiceListClick { get; set; }
        public ICommand InvoiceListClick
        {
            get
            {
                if (_InvoiceListClick == null)
                {
                    _InvoiceListClick = new DelegateCommand(InvoiceList_Click);
                }
                return _InvoiceListClick;
            }

        }

        public void InvoiceList_Click()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));

            WelComePage.InvoiceReff.Background = color;
            ModalService.NavigateTo(new Invoice(), delegate(bool returnValue) { });
        }

        private ICommand _CompanyClick { get; set; }
        public ICommand CompanyClick
        {
            get
            {
                if (_CompanyClick == null)
                {
                    _CompanyClick = new DelegateCommand(CompanyClick_Click);
                }
                return _CompanyClick;
            }

        }

        public void CompanyClick_Click()
        {
            App.Current.Properties["EditCompany"] = "CompanyEdit";
            Company_Window cmp = new Company_Window();
            cmp.Show();
            //ModalService.NavigateTo(new Invoice(), delegate(bool returnValue) { });
        }




        private ICommand _SupplierListClick { get; set; }
        public ICommand SupplierListClick
        {
            get
            {
                if (_SupplierListClick == null)
                {
                    _SupplierListClick = new DelegateCommand(SupplierList_Click);
                }
                return _SupplierListClick;
            }

        }
        public void SupplierList_Click()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f6f7f9"));
            var color2 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f6f7f9"));
            var color3 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));
            var color4 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f6f7f9"));
            var Rcolor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f6f7f9"));
            WelComePage.ItemPRef.Background = color;
            WelComePage.CustReff.Background = color;
            WelComePage.VenReff.Background = color3;
            WelComePage.POrdReff.Background = color;
            WelComePage.ReptdReff.Background = color;
            ModalService.NavigateTo(new SupplierList(), delegate(bool returnValue) { });



        }
        public void clear()
        {
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f6f7f9"));
            WelComePage.ItemPRef.Background = color;
            WelComePage.CustReff.Background = color;
            WelComePage.VenReff.Background = color;
            WelComePage.POrdReff.Background = color;
            WelComePage.ReptdReff.Background = color;
            WelComePage.DepermentReff.Background = color;
            WelComePage.DesignationReff.Background = color;
            WelComePage.AccessReff.Background = color;
            WelComePage.LoyaltyReff.Background = color;
            WelComePage.TaxReff.Background = color;
            WelComePage.bankReff.Background = color;
            WelComePage.StockReff.Background = color;
            WelComePage.SubpayReff.Background = color;
            WelComePage.RecItemReff.Background = color;
            WelComePage.SalesReturnReff.Background = color;
            WelComePage.cashRegReff.Background = color;
            WelComePage.Employee1Reff.Background = color;
            WelComePage.payrecivedReff.Background = color;
            WelComePage.BussLocationReff.Background = color;
            WelComePage.GodownReff.Background = color;
            WelComePage.ItemLocationReff.Background = color;
            WelComePage.catagoryReff.Background = color;
            WelComePage.stockLegerReff.Background = color;
            WelComePage.dailySalesReff.Background = color;
            WelComePage.InvoiceReff.Background = color;
            WelComePage.RInvoiceReff.Background = color;
            WelComePage.EstimateReff.Background = color;
        }


        private ICommand _CustomerListClick { get; set; }
        public ICommand CustomerListClick
        {
            get
            {
                if (_CustomerListClick == null)
                {
                    _CustomerListClick = new DelegateCommand(CustomerList_Click);
                }
                return _CustomerListClick;
            }

        }

        public void CustomerList_Click()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f6f7f9"));
            var color2 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));
            var color3 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f6f7f9"));
            var color4 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f6f7f9"));
            var Rcolor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f6f7f9"));
            WelComePage.ItemPRef.Background = color;
            WelComePage.CustReff.Background = color2;
            WelComePage.VenReff.Background = color3;
            WelComePage.POrdReff.Background = color4;
            WelComePage.ReptdReff.Background = Rcolor;
            ModalService.NavigateTo(new CustomerList(), delegate(bool returnValue) { });



        }







        private ICommand _RecvItems { get; set; }
        public ICommand RecvItems
        {
            get
            {
                if (_RecvItems == null)
                {
                    _RecvItems = new DelegateCommand(RecvItemList);
                }
                return _RecvItems;
            }

        }

        public void RecvItemList()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));

            WelComePage.RecItemReff.Background = color;
            ModalService.NavigateTo(new ReceiveItems(), delegate(bool returnValue) { });
        }
        private ICommand _CASH_REG { get; set; }
        public ICommand CASH_REG
        {
            get
            {
                if (_CASH_REG == null)
                {
                    _CASH_REG = new DelegateCommand(CashRegList);
                }
                return _CASH_REG;
            }

        }

        public void CashRegList()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));

            WelComePage.cashRegReff.Background = color;
            ModalService.NavigateTo(new CashReg(), delegate(bool returnValue) { });
        }

        private ICommand _SUPPLIER { get; set; }
        public ICommand SUPPLIER
        {
            get
            {
                if (_SUPPLIER == null)
                {
                    _SUPPLIER = new DelegateCommand(SupplierList);
                }
                return _SUPPLIER;
            }

        }

        public void SupplierList()
        {
            ModalService.NavigateTo(new SupplierList(), delegate(bool returnValue) { });
        }
        private ICommand _ManageStock { get; set; }
        public ICommand ManageStock
        {
            get
            {
                if (_ManageStock == null)
                {
                    _ManageStock = new DelegateCommand(AddStockTransfer);
                }
                return _ManageStock;
            }

        }
        public void AddStockTransfer()
        {

            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));
            var color12 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));

            WelComePage.StockReff.Background = color;
            ModalService.NavigateTo(new StockList(), delegate(bool returnValue) { });



        }



        private ICommand _EmployeeList { get; set; }
        public ICommand EmployeeList
        {
            get
            {
                if (_EmployeeList == null)
                {
                    _EmployeeList = new DelegateCommand(Employee_List);
                }
                return _EmployeeList;
            }
        }
        public void Employee_List()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));

            WelComePage.Employee1Reff.Background = color;
            ModalService.NavigateTo(new Employee(), delegate(bool returnValue) { });
        }

        private ICommand _StockLedgerList { get; set; }
        public ICommand StockLedgerList
        {
            get
            {
                if (_StockLedgerList == null)
                {
                    _StockLedgerList = new DelegateCommand(StockLedger_List);
                }
                return _StockLedgerList;
            }
        }
        public void StockLedger_List()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));

            WelComePage.stockLegerReff.Background = color;
            ModalService.NavigateTo(new StockLedgerList(), delegate(bool returnValue) { });
        }
        private ICommand _RecvPaymentClick { get; set; }
        public ICommand RecvPaymentClick
        {
            get
            {
                if (_RecvPaymentClick == null)
                {
                    _RecvPaymentClick = new DelegateCommand(RecvPayment_Click);
                }
                return _RecvPaymentClick;
            }
        }
        public void RecvPayment_Click()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));

            WelComePage.payrecivedReff.Background = color;
            ModalService.NavigateTo(new ReceivePayment(), delegate(bool returnValue) { });
        }




        private ICommand _Customer { get; set; }
        public ICommand Customer
        {
            get
            {
                if (_Customer == null)
                {
                    _Customer = new DelegateCommand(AddCustomer);
                }
                return _Customer;
            }
        }
        public void AddCustomer()
        {
            ModalService.NavigateTo(new CustomerList(), delegate(bool returnValue) { });
        }

        private ICommand _SalesReturn { get; set; }
        public ICommand SalesReturn
        {
            get
            {
                if (_SalesReturn == null)
                {
                    _SalesReturn = new DelegateCommand(AddSalesReturn);
                }
                return _SalesReturn;
            }
        }
        public void AddSalesReturn()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));

            WelComePage.SalesReturnReff.Background = color;
            ModalService.NavigateTo(new salesReturnList(), delegate(bool returnValue) { });
        }
        private ICommand _SuppPyment { get; set; }
        public ICommand SuppPyment
        {
            get
            {
                if (_SuppPyment == null)
                {
                    _SuppPyment = new DelegateCommand(AddSuppPyment);
                }
                return _SuppPyment;
            }
        }
        public void AddSuppPyment()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));

            WelComePage.SubpayReff.Background = color;
            ModalService.NavigateTo(new SuppPayList(), delegate(bool returnValue) { });
        }
        private ICommand _BackMain { get; set; }
        public ICommand BackMain
        {
            get
            {
                if (_BackMain == null)
                {
                    _BackMain = new DelegateCommand(OpenMainPage);
                }
                return _BackMain;
            }

        }



        public void OpenMainPage()
        {
            this.Close();

            //Main tt = new Main();
            //Application.Current.MainWindow = tt;
            //// ModalService.NavigateTo(new CustomerList(), delegate(bool returnValue) { });
            //tt.Show();
            //Close();


        }

        public void Close()
        {

            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }


        private string _SecondVisible { get; set; }
        public string SecondVisible
        {

            get { return _SecondVisible; }
            set
            {
                if (value != _SecondVisible)
                {
                    _SecondVisible = value;
                    OnPropertyChanged("SecondVisible");
                }
            }
        }

        private string _FristVisible { get; set; }
        public string FristVisible
        {

            get { return _FristVisible; }
            set
            {
                if (value != _FristVisible)
                {
                    _FristVisible = value;
                    OnPropertyChanged("FristVisible");
                }
            }
        }


        public ICommand _NextPage;
        public ICommand NextPage
        {
            get
            {
                if (_NextPage == null)
                {
                    _NextPage = new DelegateCommand(Next_Page);
                }
                return _NextPage;
            }
        }



        public void Next_Page()
        {
            SecondVisible = "Visible";
            FristVisible = "Collapsed";
        }
        public ICommand _PreVious;
        public ICommand PreVious
        {
            get
            {
                if (_PreVious == null)
                {
                    _PreVious = new DelegateCommand(PreVious_Page);
                }
                return _PreVious;
            }
        }



        public void PreVious_Page()
        {
            SecondVisible = "Collapsed";
            FristVisible = "Visible";
        }








        public bool Add()
        {
            return true;
        }
        public bool Select()
        {
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        private ICommand _DepartmentClick { get; set; }
        public ICommand DepartmentClick
        {
            get
            {
                if (_DepartmentClick == null)
                {
                    _DepartmentClick = new DelegateCommand(Department_Click);
                }
                return _DepartmentClick;
            }
        }
        public void Department_Click()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));

            WelComePage.DepermentReff.Background = color;
            ModalService.NavigateTo(new Department_list(), delegate(bool returnValue) { });
        }
        private ICommand _DesignationList { get; set; }
        public ICommand DesignationList
        {
            get
            {
                if (_DesignationList == null)
                {
                    _DesignationList = new DelegateCommand(Designation_List);
                }
                return _DesignationList;
            }

        }

        public void Designation_List()
        {
            clear();
            var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));

            WelComePage.DesignationReff.Background = color;
            ModalService.NavigateTo(new DesignationList(), delegate(bool returnValue) { });
        }

        private Stack<BackNavigationEventHandler> _backFunctions
           = new Stack<BackNavigationEventHandler>();

        void IModalService.NavigateTo(UserControl uc, BackNavigationEventHandler backFromDialog)
        {

        }

        void IModalService.GoBackward(bool dialogReturnValue)
        {
        }


        public static IModalService ModalService
        {
            get { return (IModalService)Application.Current.MainWindow; }
        }
    }
}
