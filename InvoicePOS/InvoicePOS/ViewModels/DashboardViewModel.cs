using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoicePOS.Helpers;
using InvoicePOS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace InvoicePOS.ViewModels
{
    class DashboardViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {

        public DashboardViewModel()
        {
            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            GetPendingInvoices(comp);
            GetTopCustomers(comp);
            GetTopProducts(comp);
            GetTopVendors(comp);
            GetPendingPurchaseOrders(comp);
            GetRecentInvoices(comp);
            GetPendingPayments(comp);
            GetAvailableStock(comp);
        }

        #region DashBoard

        private ObservableCollection<TopVendorModel> _ListTopVendorModel;
        public ObservableCollection<TopVendorModel> ListTopVendorModel
        {
            get
            {
                return _ListTopVendorModel;
            }
            set
            {
                this._ListTopVendorModel = value;
                OnPropertyChanged("ListTopVendorModel");
            }
        }

        public async void GetTopVendors(int id)
        {
            ObservableCollection<TopVendorModel> _ListGrid_Temp = new ObservableCollection<TopVendorModel>();
            TopVendorModel[] data = null; ;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            _ListGrid_Temp.Clear();
            HttpResponseMessage response = client.GetAsync("api/SupplierAPI/GetTopVendors?id=" + id + "").Result;
            if (response.IsSuccessStatusCode)
            {
                var list = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<TopVendorModel[]>(await response.Content.ReadAsStringAsync());
                for (int i = 0; i < data.Length; i++)
                {
                    _ListGrid_Temp.Add(new TopVendorModel
                    {
                        SUPPLIER_CODE = data[i].SUPPLIER_CODE,
                        SUPPLIER_NAME = data[i].SUPPLIER_NAME,
                        TOTAL_NO_OF_PURCHASEORDER = data[i].TOTAL_NO_OF_PURCHASEORDER,
                        TOTAL_PURCHASE = data[i].TOTAL_PURCHASE

                    });
                }
            }
            ListTopVendorModel = _ListGrid_Temp;
        }


        private ObservableCollection<TopCustomerModel> _ListTopCustomerModel;
        public ObservableCollection<TopCustomerModel> ListTopCustomerModel
        {
            get
            {
                return _ListTopCustomerModel;
            }
            set
            {
                this._ListTopCustomerModel = value;
                OnPropertyChanged("ListTopCustomerModel");
            }
        }

        public async void GetTopCustomers(int id)
        {
            ObservableCollection<TopCustomerModel> _ListGrid_Temp = new ObservableCollection<TopCustomerModel>();
            TopCustomerModel[] data = null; ;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            _ListGrid_Temp.Clear();
            HttpResponseMessage response = client.GetAsync("api/CustomerAPI/GetTopCustomers?id=" + id + "").Result;
            if (response.IsSuccessStatusCode)
            {
                var list = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<TopCustomerModel[]>(await response.Content.ReadAsStringAsync());
                for (int i = 0; i < data.Length; i++)
                {
                    _ListGrid_Temp.Add(new TopCustomerModel
                    {
                        CUSTOMER_CODE = data[i].CUSTOMER_CODE,
                        CUSTOMER_ID = data[i].CUSTOMER_ID,
                        CUSTOMER_NAME = data[i].CUSTOMER_NAME,
                        TOTAL_NO_OF_INVOICE = data[i].TOTAL_NO_OF_INVOICE,
                        TOTAL_PURCHASE = data[i].TOTAL_PURCHASE

                    });
                }
            }
            ListTopCustomerModel = _ListGrid_Temp;
        }

       
        private ObservableCollection<DashBoardPendingInvoiceModel> _ListPendingInvoice;
        public ObservableCollection<DashBoardPendingInvoiceModel> ListPendingInvoice
        {
            get
            {
                return _ListPendingInvoice;
            }
            set
            {
                this._ListPendingInvoice = value;
                OnPropertyChanged("ListPendingInvoice");
            }
        }

        public async void GetPendingInvoices(int id)
        {
            ObservableCollection<DashBoardPendingInvoiceModel> _ListGrid_Temp = new ObservableCollection<DashBoardPendingInvoiceModel>();
            DashBoardPendingInvoiceModel[] data = null; ;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            _ListGrid_Temp.Clear();
            HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/GetPendingInvoices?id=" + id + "").Result;
            if (response.IsSuccessStatusCode)
            {
                var list = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<DashBoardPendingInvoiceModel[]>(await response.Content.ReadAsStringAsync());
                for (int i = 0; i < data.Length; i++)
                {
                    _ListGrid_Temp.Add(new DashBoardPendingInvoiceModel
                    {
                        CUSTOMER_FIRST_NAME = data[i].CUSTOMER_FIRST_NAME,
                        CUSTOMER_LAST_NAME = data[i].CUSTOMER_LAST_NAME,
                        CUSTOMER_NAME = data[i].CUSTOMER_FIRST_NAME + " " + data[i].CUSTOMER_LAST_NAME,
                        CUSTOMER_NUMBER = data[i].CUSTOMER_NUMBER,
                        INVOICE_DATE = data[i].INVOICE_DATE,
                        INVOICE_NO = data[i].INVOICE_NO,
                        PENDING_AMOUNT = data[i].PENDING_AMOUNT,
                        TOTAL_AMOUNT = data[i].TOTAL_AMOUNT

                    });
                }
            }
            ListPendingInvoice = _ListGrid_Temp;
        }


        private ObservableCollection<DashBoardPendingInvoiceModel> _ListPendingPayments;
        public ObservableCollection<DashBoardPendingInvoiceModel> ListPendingPayments
        {
            get
            {
                return _ListPendingPayments;
            }
            set
            {
                this._ListPendingPayments = value;
                OnPropertyChanged("ListPendingInvoice");
            }
        }

        public async void GetPendingPayments(int id)
        {
            ObservableCollection<DashBoardPendingInvoiceModel> _ListGrid_Temp = new ObservableCollection<DashBoardPendingInvoiceModel>();
            DashBoardPendingInvoiceModel[] data = null; ;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            _ListGrid_Temp.Clear();
            HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/GetPendingPayments?id=" + id + "").Result;
            if (response.IsSuccessStatusCode)
            {
                var list = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<DashBoardPendingInvoiceModel[]>(await response.Content.ReadAsStringAsync());
                for (int i = 0; i < data.Length; i++)
                {
                    _ListGrid_Temp.Add(new DashBoardPendingInvoiceModel
                    {
                        CUSTOMER_FIRST_NAME = data[i].CUSTOMER_FIRST_NAME,
                        CUSTOMER_LAST_NAME = data[i].CUSTOMER_LAST_NAME,
                        CUSTOMER_NAME = data[i].CUSTOMER_FIRST_NAME + " " + data[i].CUSTOMER_LAST_NAME,
                        CUSTOMER_NUMBER = data[i].CUSTOMER_NUMBER,
                        INVOICE_DATE = data[i].INVOICE_DATE,
                        INVOICE_NO = data[i].INVOICE_NO,
                        PENDING_AMOUNT = data[i].PENDING_AMOUNT,
                        TOTAL_AMOUNT = data[i].TOTAL_AMOUNT

                    });
                }
            }
            ListPendingPayments = _ListGrid_Temp;
        }


        private ObservableCollection<TopProductModel> _ListTOPProductModel;
        public ObservableCollection<TopProductModel> ListTOPProductModel
        {
            get
            {
                return _ListTOPProductModel;
            }
            set
            {
                this._ListTOPProductModel = value;
                OnPropertyChanged("ListTOPProductModel");
            }
        }

        public async void GetTopProducts(int id)
        {
            ObservableCollection<TopProductModel> _ListGrid_Temp = new ObservableCollection<TopProductModel>();
            TopProductModel[] data = null; ;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            _ListGrid_Temp.Clear();
            HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetTopProducts?id=" + id + "").Result;
            if (response.IsSuccessStatusCode)
            {
                var list = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<TopProductModel[]>(await response.Content.ReadAsStringAsync());
                for (int i = 0; i < data.Length; i++)
                {
                    _ListGrid_Temp.Add(new TopProductModel
                    {
                        AVAILABLE_QUANTITY = data[i].AVAILABLE_QUANTITY,
                        ITEM_CODE = data[i].ITEM_CODE,
                        ITEM_ID = data[i].ITEM_ID,
                        ITEM_NAME = data[i].ITEM_NAME,
                        QUANTITY_SOLD = data[i].QUANTITY_SOLD

                    });
                }
            }
            ListTOPProductModel = _ListGrid_Temp;
        }


        private ObservableCollection<PendingPOModel> _ListPendingPOModel;
        public ObservableCollection<PendingPOModel> ListPendingPOModel
        {
            get
            {
                return _ListPendingPOModel;
            }
            set
            {
                this._ListPendingPOModel = value;
                OnPropertyChanged("ListPendingPOModel");
            }
        }

        public async void GetPendingPurchaseOrders(int id)
        {
            ObservableCollection<PendingPOModel> _ListGrid_Temp = new ObservableCollection<PendingPOModel>();
            PendingPOModel[] data = null; ;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            _ListGrid_Temp.Clear();
            HttpResponseMessage response = client.GetAsync("api/POAPI/GetPendingPurchseOrders?id=" + id + "").Result;
            if (response.IsSuccessStatusCode)
            {
                var list = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<PendingPOModel[]>(await response.Content.ReadAsStringAsync());
                for (int i = 0; i < data.Length; i++)
                {
                    _ListGrid_Temp.Add(new PendingPOModel
                    {
                        PO_NUMBER = data[i].PO_NUMBER,
                         SUPPLIER_CODE = data[i].SUPPLIER_CODE,
                        SUPPLIER_NAME = data[i].SUPPLIER_NAME,
                        PO_AMOUNT = data[i].PO_AMOUNT

                    });
                }
            }
            ListPendingPOModel = _ListGrid_Temp;
        }


        private ObservableCollection<DashBoardPendingInvoiceModel> _ListDashBoardPendingInvoiceModel;
        public ObservableCollection<DashBoardPendingInvoiceModel> ListDashBoardPendingInvoiceModel
        {
            get
            {
                return _ListDashBoardPendingInvoiceModel;
            }
            set
            {
                this._ListDashBoardPendingInvoiceModel = value;
                OnPropertyChanged("ListDashBoardPendingInvoiceModel");
            }
        }

        public async void GetRecentInvoices(int id)
        {
            ObservableCollection<DashBoardPendingInvoiceModel> _ListGrid_Temp = new ObservableCollection<DashBoardPendingInvoiceModel>();
            DashBoardPendingInvoiceModel[] data = null; ;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            _ListGrid_Temp.Clear();
            HttpResponseMessage response = client.GetAsync("api/InvoiceAPI/GetRecentInvoices?id=" + id + "").Result;
            if (response.IsSuccessStatusCode)
            {
                var list = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<DashBoardPendingInvoiceModel[]>(await response.Content.ReadAsStringAsync());
                for (int i = 0; i < data.Length; i++)
                {
                    _ListGrid_Temp.Add(new DashBoardPendingInvoiceModel
                    {
                        CUSTOMER_FIRST_NAME = data[i].CUSTOMER_FIRST_NAME,
                        CUSTOMER_LAST_NAME = data[i].CUSTOMER_LAST_NAME,
                        CUSTOMER_NAME = data[i].CUSTOMER_FIRST_NAME + " " + data[i].CUSTOMER_LAST_NAME,
                        CUSTOMER_NUMBER = data[i].CUSTOMER_NUMBER,
                        INVOICE_DATE = data[i].INVOICE_DATE,
                        INVOICE_NO = data[i].INVOICE_NO,
                        PENDING_AMOUNT = data[i].PENDING_AMOUNT,
                        TOTAL_AMOUNT = data[i].TOTAL_AMOUNT

                    });
                }
            }
            ListDashBoardPendingInvoiceModel = _ListGrid_Temp;
        }

        private ObservableCollection<AvailableStockModel> _ListAvailableStockModel;
        public ObservableCollection<AvailableStockModel> ListAvailableStockModel
        {
            get
            {
                return _ListAvailableStockModel;
            }
            set
            {
                this._ListAvailableStockModel = value;
                OnPropertyChanged("ListAvailableStockModel");
            }
        }

        public async void GetAvailableStock(int id)
        {
            ObservableCollection<AvailableStockModel> _ListGrid_Temp = new ObservableCollection<AvailableStockModel>();
            AvailableStockModel[] data = null; ;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            _ListGrid_Temp.Clear();
            HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAvailableStock?id=" + id + "").Result;
            if (response.IsSuccessStatusCode)
            {
                var list = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<AvailableStockModel[]>(await response.Content.ReadAsStringAsync());
                for (int i = 0; i < data.Length; i++)
                {
                    _ListGrid_Temp.Add(new AvailableStockModel
                    {
                        BAR_CODE = data[i].BAR_CODE,
                        STOCK = data[i].STOCK??0
                    });
                }
            }
            ListAvailableStockModel = _ListGrid_Temp;
        }
        #endregion


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
