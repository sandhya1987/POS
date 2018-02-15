using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Estimate;
using InvoicePOS.Views.Main;
using InvoicePOS.Views.WelCome;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using InvoicePOS.UserControll.Estimate;

namespace InvoicePOS.ViewModels
{
    public class EstimateInvoiceViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {

        EstimateModel[] data = null;
        ItemModel[] data12 = null;
        List<ItemModel> _ListGrid_Temp12 = new List<ItemModel>();
        public List<EstimateModel> _ListGrid { get; set; }
        public List<EstimateModel> ListGrid
        {
            get
            {
                return _ListGrid;
            }
            set
            {
                this._ListGrid = value;
                OnPropertyChanged("ListGrid");
            }
        }
        public EstimateInvoiceViewModel()
        {
            
            GetEstimate("1");
            if (App.Current.Properties["Action"].ToString() == "EditEstimate") 
            {
                SelectedEstimate = App.Current.Properties["DataGridL"] as EstimateModel;
            }

            if (App.Current.Properties["Action"].ToString() == "ViewEstimate")
            {
              SelectedEstimate = App.Current.Properties["ViewEstimate"] as EstimateModel ;
              ListGrid.Clear();
              ListGrid.Add(new EstimateModel
              {
                  Barcode = SelectedEstimate.Barcode,
                  EstimateID = SelectedEstimate.EstimateID,
                  EstimateNo = SelectedEstimate.EstimateNo,
                  BusinessLocation = SelectedEstimate.BusinessLocation,
                  CashRegister = SelectedEstimate.CashRegister,
                  CountItem = SelectedEstimate.CountItem,
                  CustomerName = SelectedEstimate.CustomerName,
                  EmployeeLogin = SelectedEstimate.EmployeeLogin,
                  TotalItemQty = SelectedEstimate.TotalItemQty,
                  TotalPrice = SelectedEstimate.TotalPrice
              });
              EstimateNo = SelectedEstimate.EstimateNo;
              TotalPrice = SelectedEstimate.TotalPrice;
            }

        }
        private EstimateModel _SelectedEstimate;
        public EstimateModel SelectedEstimate
        {
            get { return _SelectedEstimate; }
            set
            {
                if (_SelectedEstimate != value)
                {
                    _SelectedEstimate = value;
                    OnPropertyChanged("SelectedEstimate");
                }
            }
        }
       
        //private EstimateModel _EditEstimate;
        //public EstimateModel EditEstimate
        //{
        //    get { return _EditEstimate; }
        //    set
        //    {
        //        if (_EditEstimate != value)
        //        {
        //            _EditEstimate = value;
        //            OnPropertyChanged("SelectedEstimate");
        //        }
        //    }
        //}
       
        private int _Estimate_ID;
        public int Estimate_ID
        {
            get
            {
                return Estimate_ID;
            }
            set
            {
                _Estimate_ID = value;

                _Estimate_ID = value;
                    OnPropertyChanged("EstimateID");
              
            }
        }
        private int _Barcode;
        public int Barcode
        {
            get
            {
                return Barcode;
            }
            set
            {
                _Barcode = value;

                _Barcode = value;
                OnPropertyChanged("Barcode");

            }
        }
        private string _ItemName;
        public string ItemName
        {
            get
            {
                return ItemName;
            }
            set
            {
                _ItemName = value;

                _ItemName = value;
                OnPropertyChanged("Barcode");

            }
        }
        private int _CountItem;
        public int CountItem
        {
            get
            {
                return _CountItem;
            }
            set
            {
                _CountItem = value;


                _CountItem = value;
                    OnPropertyChanged("CountItem");
              
            }
        }
        private DateTime _EstimateDatetime;
        public DateTime EstimateDatetime
        {
            get
            {
                return _EstimateDatetime;
            }
            set
            {
                _EstimateDatetime = value;

                _EstimateDatetime = value;
                    OnPropertyChanged("EstimateDate");
              
            }
        }
        private decimal _TotalPrice;
        public decimal TotalPrice
        {
            get
            {
                return _TotalPrice;
            }
            set
            {
                _TotalPrice = value;


                _TotalPrice = value;
                    OnPropertyChanged("TotalPrice");
                
            }
        }
        private string _BusinessLoc;
        public  string BusinessLoc
        {
            get
            {
                return _BusinessLoc;
            }
            set
            {

                _BusinessLoc = value;
                    OnPropertyChanged("BusinessLocation");
              
            }
        }
        private string _EstimateNo;
        public string EstimateNo
        {
            get
            {
                return _EstimateNo;
            }
            set
            {
                _EstimateNo = value;
                    OnPropertyChanged("EstimateNo");
               
            }
        }
        private DateTime _EstimateDate;
        public DateTime EstimateDate
        {
            get
            {
                return _EstimateDate;
            }
            set
            {

                _EstimateDate = value;
                    OnPropertyChanged("EstimateDate");
                
            }
        }
        private decimal _GrandTotal;
        public decimal GrandTotal
        {
            get
            {
                return _GrandTotal;
            }
            set
            {

                _GrandTotal = value;
                    OnPropertyChanged("TotalPrice");
                
            }
        }
        private decimal _TaxGrandTotal;
        public decimal TaxGrandTotal
        {
            get
            {
                return _TaxGrandTotal;
            }
            set
            {
                _TaxGrandTotal = value;
                OnPropertyChanged("TaxGrandTotal");
            }
        }
        private decimal _TotalTax;
        public decimal TotalTax
        {
            get
            {
                return _TotalTax;
            }
            set
            {
                _TotalTax = value;
                OnPropertyChanged("TotalTax");
            }
        }
        private int _TotalItemQty;
        public int TotalItemQty
        {
            get
            {
                return _TotalItemQty;
            }
            set
            {
                _TotalItemQty = value;
                OnPropertyChanged("TotalTax");
            }
        }
        private string _CustomerName;
        public string CustomerName
        {
            get
            {
                return _CustomerName;
            }
            set
            {
                _CustomerName = value;
                OnPropertyChanged("CustomerName");
            }
        }
        private string _InnnvoiceNote;
        public string InnnvoiceNote
        {
            get
            {
                return _InnnvoiceNote;
            }
            set
            {
                _CustomerName = value;
                OnPropertyChanged("InnnvoiceNote");
            }
        }
        private string _HoldNote;
        public string HoldNote
        {
            get
            {
                return _HoldNote;
            }
            set
            {
                _HoldNote = value;
                OnPropertyChanged("HoldNote");
            }
        }
        private bool _InvoiceStatus;
        public bool InvoiceStatus
        {
            get
            {
                return _InvoiceStatus;
            }
            set
            {
                _InvoiceStatus = value;
                OnPropertyChanged("InvoiceStatus");
            }
        }
        private string _EmployeeLogin;
        public string EmployeeLogin
        {
            get
            {
                return _EmployeeLogin;
            }
            set
            {
                _EmployeeLogin = value;
                OnPropertyChanged("EmployeeLogin");
            }
        }
        private string _SalesExecutiveName;
        public string SalesExecutiveName
        {
            get
            {
                return _SalesExecutiveName;
            }
            set
            {
                _SalesExecutiveName = value;
                OnPropertyChanged("SalesExecutiveName");
            }
        }
        private DateTime _DeliveryDate;
        public DateTime DeliveryDate
        {
            get
            {
                return _DeliveryDate;
            }
            set
            {
                _DeliveryDate = value;
                OnPropertyChanged("DeliveryDate");
            }
        }
        private DateTime _DeliveryTimeSlot;
        public DateTime DeliveryTimeSlot
        {
            get
            {
                return _DeliveryTimeSlot;
            }
            set
            {
                _DeliveryTimeSlot = value;
                OnPropertyChanged("DeliveryTimeSlot");
            }
        }
        private string _CustomerComment;
        public string CustomerComment
        {
            get
            {
                return _CustomerComment;
            }
            set
            {
                _CustomerComment = value;
                OnPropertyChanged("CustomerComment");
            }
        }
        private int _PrintCount;
        public int PrintCount
        {
            get
            {
                return _PrintCount;
            }
            set
            {
                _PrintCount = value;
                OnPropertyChanged("PrintCount");
            }
        }
        private string _InvoiceNo;
        public string InvoiceNo
        {
            get
            {
                return _InvoiceNo;
            }
            set
            {
                _InvoiceNo = value;
                OnPropertyChanged("InvoiceNo");
            }
        }
        private string _InvoiceRefNo;
        public string InvoiceRefNo
        {
            get
            {
                return _InvoiceRefNo;
            }
            set
            {
                _InvoiceRefNo = value;
                OnPropertyChanged("InvoiceRefNo");
            }
        }
        private DateTime _InvoiceDate;
        public DateTime InvoiceDate
        {
            get
            {
                return _InvoiceDate;
            }
            set
            {
                _InvoiceDate = value;
                OnPropertyChanged("InvoiceDate");
            }
        }


        private int _ViewEstimate_ID;
        public int ViewEstimate_ID
        {
            get
            {
                return SelectedEstimate.EstimateID;
            }
            set
            {
                SelectedEstimate.EstimateID = value;

                if (SelectedEstimate.EstimateID != value)
                {
                    SelectedEstimate.EstimateID = value;
                    OnPropertyChanged("EstimateID");
                }


            }
        }
        private int _ViewCountItem;
        public int ViewCountItem
        {
            get
            {
                return SelectedEstimate.CountItem;
            }
            set
            {
                SelectedEstimate.CountItem = value;

                if (SelectedEstimate.CountItem != value)
                {
                    SelectedEstimate.CountItem = value;
                    OnPropertyChanged("CountItem");
                }


            }
        }
        private DateTime _ViewEstimateDatetime;
        public DateTime ViewEstimateDatetime
        {
            get
            {
                return SelectedEstimate.EstimateDate;
            }
            set
            {
                SelectedEstimate.EstimateDate = value;

                if (SelectedEstimate.EstimateDate != value)
                {
                    SelectedEstimate.EstimateDate = value;
                    OnPropertyChanged("EstimateDate");
                }


            }
        }
        private decimal _ViewTotalPrice;
        public decimal ViewTotalPrice
        {
            get
            {
                return SelectedEstimate.TotalPrice;
            }
            set
            {
                SelectedEstimate.TotalPrice = value;

                if (SelectedEstimate.TotalPrice != value)
                {
                    SelectedEstimate.TotalPrice = value;
                    OnPropertyChanged("TotalPrice");
                }


            }
        }
        private string _ViewBusinessLoc;
        public string ViewBusinessLoc
        {
            get
            {
                return SelectedEstimate.BusinessLocation;
            }
            set
            {
                SelectedEstimate.BusinessLocation = value;

                if (SelectedEstimate.BusinessLocation != value)
                {
                    SelectedEstimate.BusinessLocation = value;
                    OnPropertyChanged("BusinessLocation");
                }


            }
        }
        private string _ViewEstimateNo;
        public string ViewEstimateNo
        {
            get
            {
                return SelectedEstimate.EstimateNo;
            }
            set
            {
                SelectedEstimate.EstimateNo = value;

                if (SelectedEstimate.EstimateNo != value)
                {
                    SelectedEstimate.EstimateNo = value;
                    OnPropertyChanged("EstimateNo");
                }


            }
        }
        private DateTime _ViewEstimateDate;
        public DateTime ViewEstimateDate
        {
            get
            {
                return SelectedEstimate.EstimateDate;
            }
            set
            {
                SelectedEstimate.EstimateDate = value;

                if (SelectedEstimate.EstimateDate != value)
                {
                    SelectedEstimate.EstimateDate = value;
                    OnPropertyChanged("EstimateDate");
                }


            }
        }
        private decimal _ViewGrandTotal;
        public decimal ViewGrandTotal
        {
            get
            {
                return SelectedEstimate.GrnadTotal;
            }
            set
            {
                SelectedEstimate.GrnadTotal = value;

                if (SelectedEstimate.GrnadTotal != value)
                {
                    SelectedEstimate.GrnadTotal = value;
                    OnPropertyChanged("GrnadTotal");
                }


            }
        }
        private decimal _ViewTaxGrandTotal;
        public decimal ViewTaxGrandTotal
        {
            get
            {
                return SelectedEstimate.TaxGrandTotal;
            }
            set
            {
                SelectedEstimate.TaxGrandTotal = value;

                if (SelectedEstimate.TaxGrandTotal != value)
                {
                    SelectedEstimate.TaxGrandTotal = value;
                    OnPropertyChanged("TaxGrandTotal");
                }


            }
        }
        private decimal _ViewTotalTax;
        public decimal ViewTotalTax
        {
            get
            {
                return SelectedEstimate.TotalTax;
            }
            set
            {
                SelectedEstimate.TotalTax = value;

                if (SelectedEstimate.TotalTax != value)
                {
                    SelectedEstimate.TotalTax = value;
                    OnPropertyChanged("TotalTax");
                }

            }
        }
        private string _ViewTotalItemQty;
        public string ViewTotalItemQty
        {
            get
            {
                return SelectedEstimate.TotalItemQty;
            }
            set
            {
                SelectedEstimate.TotalItemQty = value;

                if (SelectedEstimate.TotalItemQty != value)
                {
                    SelectedEstimate.TotalItemQty = value;
                    OnPropertyChanged("TotalItemQty");
                }

            }
        }
        private string _ViewCustomerName;
        public string ViewCustomerName
        {
            get
            {
                return SelectedEstimate.CustomerName;
            }
            set
            {
                SelectedEstimate.CustomerName = value;

                if (SelectedEstimate.CustomerName != value)
                {
                    SelectedEstimate.CustomerName = value;
                    OnPropertyChanged("CustomerName");
                }

            }
        }
        private DateTime _ViewInnnvoiceNote;
        public DateTime ViewInnnvoiceNote
        {
            get
            {
                return SelectedEstimate.InvoiceDate;
            }
            set
            {
                SelectedEstimate.InvoiceDate = value;

                if (SelectedEstimate.InvoiceDate != value)
                {
                    SelectedEstimate.InvoiceDate = value;
                    OnPropertyChanged("InvoiceDate");
                }

            }
        }
        private string _ViewHoldNote;
        public string ViewHoldNote
        {
            get
            {
                return SelectedEstimate.HoldNote;
            }
            set
            {
                SelectedEstimate.HoldNote = value;

                if (SelectedEstimate.HoldNote != value)
                {
                    SelectedEstimate.HoldNote = value;
                    OnPropertyChanged("HoldNote");
                }

            }
        }
        public async Task<ObservableCollection<EstimateModel>> GetEstimate(string comp)
        {
            try
            {
                List<EstimateModel> _ListGrid_Temp = new List<EstimateModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/EstimateAPI/GetEstimate?id='" + comp + "'").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<EstimateModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;
                        _ListGrid_Temp.Add(new EstimateModel
                        {
                            EstimateNo = data[i].EstimateNo,
                            EstimateID = data[i].EstimateID,
                            CountItem = data[i].CountItem,
                            TotalPrice = data[i].TotalPrice,
                            TotalTax = data[i].TotalTax,
                            BusinessLocation = data[i].BusinessLocation,
                            CashRegister = data[i].CashRegister,
                            CustomerName = data[i].CustomerName,
                            EmployeeLogin = data[i].EmployeeLogin,
                            EstimateDate = data[i].EstimateDate,
                            EstimateNumber = data[i].EstimateNumber,
                            InvoiceDate = data[i].InvoiceDate,
                            TotalItemQty = data[i].TotalItemQty,
                            GrnadTotal = data[i].GrnadTotal,
                            HoldNote = data[i].HoldNote,
                            InvoiceNote = data[i].InvoiceNote,
                            InvoiceStatus = data[i].InvoiceStatus,
                            TaxGrandTotal = data[i].TaxGrandTotal,
                            Barcode = data[i].Barcode,
                            ItemName = data[i].ItemName
                        });
                    }
                    ListGrid = _ListGrid_Temp;
                }
                return new ObservableCollection<EstimateModel>(_ListGrid_Temp);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

       
        public ICommand _EditEstimate;
        public ICommand EditEstimate
        {
            get
            {
                if (_EditEstimate == null)
                {
                    _EditEstimate = new DelegateCommand(EditEstimate_Ok);
                }
                return _EditEstimate;
            }
        }
      
        public async void EditEstimate_Ok()
        {
            App.Current.Properties["SelectEstimatedItem"] = SelectedEstimate;
            App.Current.Properties["Estimate_Grid"] = App.Current.Properties["DataGrid"];
            _ListGrid = App.Current.Properties["SelectEstimatedItem"] as List<EstimateModel>;
            if (SelectedEstimate.EstimateID != null && SelectedEstimate.EstimateID != 0)
            {
                
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/EstimateAPI/GetEstimateItem?id=" + SelectedEstimate.EstimateID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data12 = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < data12.Length; i++)
                    {
                        x++;
                        _ListGrid_Temp12.Add(new ItemModel
                        {
                            SLNO = x,
                            IMAGE_PATH = data12[i].IMAGE_PATH,
                            //IMAGE_PATH = System.IO.Path.Combine(dir.FullName + "\\", data[3].IMAGE_PATH),
                            // IMAGE_PATH="C:/Users/Suvendu/Desktop/24.04.2017/InvoicePOS/InvoicePOS/bin/Debug/uploaded/319200865402.jpg",
                            ITEM_NAME = data12[i].ITEM_NAME,
                            ITEM_ID = data12[i].ITEM_ID,
                            BARCODE = data12[i].BARCODE,
                            ACCESSORIES_KEYWORD = data12[i].ACCESSORIES_KEYWORD,
                            CATAGORY_ID = data12[i].CATAGORY_ID,
                            ITEM_DESCRIPTION = data12[i].ITEM_DESCRIPTION,
                            ITEM_INVOICE_ID = data12[i].ITEM_INVOICE_ID,
                            ITEM_PRICE = data12[i].ITEM_PRICE,
                            ITEM_PRODUCT_ID = data12[i].ITEM_PRODUCT_ID,
                            KEYWORD = data12[i].KEYWORD,
                            MRP = data12[i].MRP,
                            PURCHASE_UNIT = data12[i].PURCHASE_UNIT,
                            PURCHASE_UNIT_PRICE = data12[i].PURCHASE_UNIT_PRICE,
                            SALES_PRICE = data12[i].SALES_PRICE,
                            SALES_UNIT = data12[i].SALES_UNIT,
                            SEARCH_CODE = data12[i].SEARCH_CODE,
                            TAX_COLLECTED = data12[i].TAX_COLLECTED,
                            TAX_PAID = data12[i].TAX_PAID,
                            ALLOW_PURCHASE_ON_ESHOP = data12[i].ALLOW_PURCHASE_ON_ESHOP,
                            CATEGORY_NAME = data12[i].CATEGORY_NAME,
                            DISPLAY_INDEX = data12[i].DISPLAY_INDEX,
                            INCLUDE_TAX = data12[i].INCLUDE_TAX,
                            ITEM_GROUP_NAME = data12[i].ITEM_GROUP_NAME,
                            ITEM_UNIQUE_NAME = data12[i].ITEM_UNIQUE_NAME,
                            OPN_QNT = data12[i].OPN_QNT,
                            REGIONAL_LANGUAGE = data12[i].REGIONAL_LANGUAGE,
                            SALES_PRICE_BEFOR_TAX = data12[i].SALES_PRICE_BEFOR_TAX,
                            MODIFICATION_DATE = data12[i].MODIFICATION_DATE,
                            IS_ACTIVE = data12[i].IS_ACTIVE,
                            Current_Qty = data12[i].Current_Qty,
                            Total = data12[i].Total
                            
                        });
                    }
                    App.Current.Properties["DataGrid"] = _ListGrid_Temp12;
                    App.Current.Properties["DataGridL"] = _ListGrid_Temp12;
                    //TotalBottom();
                    Main.ListGridRef.ItemsSource = null;
                    Main.ListGridRef.ItemsSource = _ListGrid_Temp12.ToList();
                    Application.Current.MainWindow.Close();
                }
            }
        }
        public ICommand _EstimateOk;
        public ICommand EstimateOk
        {
            get
            {
                if (_EstimateOk == null)
                {
                    _EstimateOk = new DelegateCommand(Estimate_Ok);
                }
                return _EstimateOk;
            }
        }

        public async void Estimate_Ok()
        {
            if (SelectedEstimate.EstimateNo != null && SelectedEstimate.EstimateNo != "")
            {
                App.Current.Properties["Action"] = "EditEstimate";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/EstimateAPI/GetEstimateItem?id=" + SelectedEstimate.EstimateNo + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data12 = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < data12.Length; i++)
                    {
                        x++;
                        _ListGrid_Temp12.Add(new ItemModel
                        {
                            SLNO = x,
                            IMAGE_PATH = data12[i].IMAGE_PATH,
                            //IMAGE_PATH = System.IO.Path.Combine(dir.FullName + "\\", data[3].IMAGE_PATH),
                            // IMAGE_PATH="C:/Users/Suvendu/Desktop/24.04.2017/InvoicePOS/InvoicePOS/bin/Debug/uploaded/319200865402.jpg",
                            ITEM_NAME = data12[i].ITEM_NAME,
                            ITEM_ID = data12[i].ITEM_ID,
                            BARCODE = data12[i].BARCODE,
                            ACCESSORIES_KEYWORD = data12[i].ACCESSORIES_KEYWORD,
                            CATAGORY_ID = data12[i].CATAGORY_ID,
                            ITEM_DESCRIPTION = data12[i].ITEM_DESCRIPTION,
                            ITEM_INVOICE_ID = data12[i].ITEM_INVOICE_ID,
                            ITEM_PRICE = data12[i].ITEM_PRICE,
                            ITEM_PRODUCT_ID = data12[i].ITEM_PRODUCT_ID,
                            KEYWORD = data12[i].KEYWORD,
                            MRP = data12[i].MRP,
                            PURCHASE_UNIT = data12[i].PURCHASE_UNIT,
                            PURCHASE_UNIT_PRICE = data12[i].PURCHASE_UNIT_PRICE,
                            SALES_PRICE = data12[i].SALES_PRICE,
                            SALES_UNIT = data12[i].SALES_UNIT,
                            SEARCH_CODE = data12[i].SEARCH_CODE,
                            TAX_COLLECTED = data12[i].TAX_COLLECTED,
                            TAX_PAID = data12[i].TAX_PAID,
                            ALLOW_PURCHASE_ON_ESHOP = data12[i].ALLOW_PURCHASE_ON_ESHOP,
                            CATEGORY_NAME = data12[i].CATEGORY_NAME,
                            DISPLAY_INDEX = data12[i].DISPLAY_INDEX,
                            INCLUDE_TAX = data12[i].INCLUDE_TAX,
                            ITEM_GROUP_NAME = data12[i].ITEM_GROUP_NAME,
                            ITEM_UNIQUE_NAME = data12[i].ITEM_UNIQUE_NAME,
                            OPN_QNT = data12[i].OPN_QNT,
                            REGIONAL_LANGUAGE = data12[i].REGIONAL_LANGUAGE,
                            SALES_PRICE_BEFOR_TAX = data12[i].SALES_PRICE_BEFOR_TAX,
                            MODIFICATION_DATE = data12[i].MODIFICATION_DATE,
                            IS_ACTIVE = data12[i].IS_ACTIVE,
                            Current_Qty = data12[i].Current_Qty,
                            Total = data12[i].Total,
                        });
                    }
                    App.Current.Properties["DataGrid"] = _ListGrid_Temp12;
                    App.Current.Properties["DataGridL"] = _ListGrid_Temp12;
                    TotalBottom();
                    Main.ListGridRef.ItemsSource = null;
                    Main.ListGridRef.ItemsSource = _ListGrid_Temp12.ToList();
                    Application.Current.MainWindow.Close();
                }

            }
        }
        public void TotalBottom()
        {
            decimal Dis = 0;
            decimal vatAmount = 0;
            decimal netAmount = 0;
            decimal GossAmount = 0;
            decimal ItemToTal = 0;
            int QUNT_TOTAL = 0;
            if (ListGrid.Count == 0)
            {
            }
            else
            {
                ItemToTal = ListGrid.Count;

                foreach (var qunt in _ListGrid_Temp12)
                {
                    QUNT_TOTAL = Convert.ToInt32(qunt.Current_Qty + QUNT_TOTAL);

                }
                for (int i = 0; i < _ListGrid_Temp12.Count; i++)
                {
                    if (_ListGrid_Temp12[i].Discount != 0)
                    {
                        decimal ItemNet = _ListGrid_Temp12[i].SALES_PRICE_BEFOR_TAX - _ListGrid_Temp12[i].Discount;
                        decimal ItemVat = (ItemNet / 100) * _ListGrid_Temp12[i].TAX_COLLECTED;
                        decimal ItemGoss = ItemNet + ItemVat;
                        decimal ItemDis = _ListGrid_Temp12[i].Discount;


                        decimal netAmount1 = netAmount + (ItemNet * Convert.ToDecimal(_ListGrid_Temp12[i].Current_Qty));
                        decimal vatAmount1 = (ItemVat * Convert.ToDecimal(_ListGrid_Temp12[i].Current_Qty)) + vatAmount;
                        decimal GossAmount1 = (ItemGoss * Convert.ToDecimal(_ListGrid_Temp12[i].Current_Qty)) + GossAmount;

                        _ListGrid_Temp12[i].Total = GossAmount1;
                        decimal salPrice = Convert.ToDecimal(_ListGrid_Temp12[i].Current_Qty * (_ListGrid_Temp12[i].SALES_PRICE_BEFOR_TAX - _ListGrid_Temp12[i].Discount));
                        _ListGrid_Temp12[i].TotalTax = salPrice * (_ListGrid_Temp12[i].TAX_COLLECTED / 100);
                        _ListGrid_Temp12[i].SALES_PRICE_BEFOR_TAX_QTY = Convert.ToDecimal(_ListGrid_Temp12[i].Current_Qty * _ListGrid_Temp12[i].SALES_PRICE_BEFOR_TAX);
                        _ListGrid_Temp12[i].SalePriceWithDiscount = Convert.ToDecimal(_ListGrid_Temp12[i].Current_Qty * _ListGrid_Temp12[i].SALES_PRICE_BEFOR_TAX) - _ListGrid_Temp12[i].Discount;
                    }
                    else
                    {
                        _ListGrid_Temp12[i].TotalTax = _ListGrid_Temp12[i].Current_Qty * (_ListGrid_Temp12[i].SALES_PRICE - _ListGrid_Temp12[i].SALES_PRICE_BEFOR_TAX);
                        _ListGrid_Temp12[i].SALES_PRICE_BEFOR_TAX_QTY = Convert.ToDecimal(_ListGrid_Temp12[i].Current_Qty * _ListGrid_Temp12[i].SALES_PRICE_BEFOR_TAX);
                        _ListGrid_Temp12[i].SalePriceWithDiscount = Convert.ToDecimal(_ListGrid_Temp12[i].Current_Qty * _ListGrid_Temp12[i].SALES_PRICE_BEFOR_TAX);
                    }
                }
                foreach (var Disss in _ListGrid_Temp12)
                {
                    if (Disss.Discount == 0 || Disss.Discount == null)
                    {

                        decimal ItemGoss = 0;
                        ItemGoss = Disss.Total;
                        decimal VatItem = Convert.ToDecimal(Disss.Current_Qty) * (Disss.SALES_PRICE - Disss.SALES_PRICE_BEFOR_TAX);
                        vatAmount = VatItem + vatAmount;
                        netAmount = (ItemGoss - VatItem) + netAmount;
                        GossAmount = ItemGoss + GossAmount;

                    }
                    else
                    {
                        decimal ItemNet = Disss.SALES_PRICE_BEFOR_TAX - Disss.Discount;
                        decimal ItemVat = (ItemNet / 100) * Disss.TAX_COLLECTED;
                        decimal ItemGoss = ItemNet + ItemVat;
                        decimal ItemDis = Disss.Discount;


                        netAmount = netAmount + (ItemNet * Convert.ToDecimal(Disss.Current_Qty));
                        vatAmount = (ItemVat * Convert.ToDecimal(Disss.Current_Qty)) + vatAmount;
                        GossAmount = (ItemGoss * Convert.ToDecimal(Disss.Current_Qty)) + GossAmount;
                        GossAmount = Math.Round(GossAmount);
                        Dis = ItemDis + Dis;
                    }

                }

            }
            Main.GrossamountReff.Text = Convert.ToString(GossAmount.ToString("0.00"));
            Main.DiscountMainReff.Text = Convert.ToString(Dis.ToString("0.00"));
            Main.VatMainReff.Text = Convert.ToString(vatAmount.ToString("0.00"));
            Main.NetAmountMainReff.Text = Convert.ToString(netAmount.ToString("0.00"));
            Main.ListQnt.Text = Convert.ToString(ItemToTal);
            Main.ItemTotalMainReff.Text = Convert.ToString(QUNT_TOTAL);
        }
        public ICommand _Cancel;
        public ICommand Cancel
        {
            get
            {
                if (_Cancel == null)
                {
                    _Cancel = new DelegateCommand(Cancel_Welcome);
                }
                return _Cancel;
            }
        }
        public void Cancel_Welcome()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }
        public ICommand _DeleteEstimate;
        public ICommand DeleteEstimate
        {
            get
            {
                if (_DeleteEstimate == null)
                {
                    _DeleteEstimate = new DelegateCommand(Delete_Estimate);
                }
                return _DeleteEstimate;
            }
        }

        public void Cancel_Estimate()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }
        public async void Delete_Estimate()
        {
            if (SelectedEstimate.EstimateID != null && SelectedEstimate.EstimateID != 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure delete this estimate " + SelectedEstimate.EstimateNo + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var id = SelectedEstimate.EstimateID;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    HttpResponseMessage response = client.GetAsync("api/EstimateAPI/DeleteEstimate?id=" + id + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {
                        ModalService.NavigateTo(new Estimate(), delegate(bool returnValue) { });
                        MessageBox.Show("Estimate Deleted Successfully");
                    }
                }
                else
                {
                    Cancel_Estimate();
                }
            }
            else
            {
                MessageBox.Show("Select Customer");
            }

        }

        public ICommand _ViewEstimateClick;
        public ICommand ViewEstimateClick
        {
            get
            {
                if (_ViewEstimateClick == null)
                {
                    _ViewEstimateClick = new DelegateCommand(ViewEstimateClick_Ok);
                }
                return _ViewEstimateClick;
            }
        }
        public void ViewEstimateClick_Ok()
        {
            ViewEstimate _AC = new ViewEstimate();
            _AC.ShowDialog();

        }
        public ICommand _ViewEstimate;
        public ICommand ViewEstimate
        {
            get
            {
                if (_ViewEstimate == null)
                {
                    _ViewEstimate = new DelegateCommand(ViewEstimate_Ok);
                }
                return _ViewEstimate;
            }
        }
       
        public async void ViewEstimate_Ok() 
        {
            if (SelectedEstimate.EstimateID != null && SelectedEstimate.EstimateID != 0)
            {
                App.Current.Properties["Action"] = "ViewEstimate";
                List<EstimateModel> _ListGrid_Temp = new List<EstimateModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                int id = SelectedEstimate.EstimateID;
                HttpResponseMessage response = client.GetAsync("api/EstimateAPI/ViewEstimateItem?id=" + SelectedEstimate.EstimateID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<EstimateModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {

                            SelectedEstimate.EstimateNo = data[i].EstimateNo;
                            SelectedEstimate.EstimateID = data[i].EstimateID;
                            SelectedEstimate.Barcode = data[i].Barcode;
                            SelectedEstimate.BusinessLocation = data[i].BusinessLocation;
                            SelectedEstimate.CashRegister = data[i].CashRegister;
                            SelectedEstimate.CountItem = data[i].CountItem;
                            SelectedEstimate.CustomerName = data[i].CustomerName;
                            SelectedEstimate.EmployeeLogin = data[i].EmployeeLogin;
                            SelectedEstimate.TotalItemQty = data[i].TotalItemQty;
                            SelectedEstimate.TotalPrice = data[i].TotalPrice;
                            SelectedEstimate.EstimateDate = data[i].EstimateDate;
                            //SelectedEstimate.TotalTax = data[i].TotalTax;
                        }
                        ListGrid.Add(new EstimateModel
                        {
                            Barcode = SelectedEstimate.Barcode,
                            EstimateID = SelectedEstimate.EstimateID,
                            EstimateNo = SelectedEstimate.EstimateNo,
                            BusinessLocation = SelectedEstimate.BusinessLocation,
                            CashRegister = SelectedEstimate.CashRegister,
                            CountItem = SelectedEstimate.CountItem,
                            CustomerName = SelectedEstimate.CustomerName,
                            EmployeeLogin = SelectedEstimate.EmployeeLogin,
                            TotalItemQty = SelectedEstimate.TotalItemQty,
                            TotalPrice = SelectedEstimate.TotalPrice,
                            ItemName = SelectedEstimate.ItemName
                        });
                        App.Current.Properties["ViewEstimate"] = SelectedEstimate;
                        //InvoicePOS.UserControll.Estimate.ViewEstimate.TotalAmount.Text = SelectedEstimate.TotalPrice.ToString();
                        //InvoicePOS.UserControll.Estimate.ViewEstimate.EstimateNo.Text = SelectedEstimate.EstimateNo.ToString();
                        //InvoicePOS.UserControll.Estimate.ViewEstimate.EstimateDate.Text = SelectedEstimate.EstimateDate.ToString();
                        //InvoicePOS.UserControll.Estimate.ViewEstimate.TotalQty.Text = SelectedEstimate.TotalItemQty.ToString();
                        //InvoicePOS.UserControll.Estimate.ViewEstimate.TaxAmount.Text = SelectedEstimate.TotalTax.ToString();
                        //List<EstimateModel> _hft = App.Current.Properties["ViewEstimate"] as List<EstimateModel>;
                        //Estimate.ListGridRef.ItemsSource = _hft;
                        //ListGrid = _hft;
                        ViewEstimateClick_Ok();
                    }
                }
            }
            
         
            else
            {
                MessageBox.Show("Select Customer first", "Customer Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        
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

        private Stack<BackNavigationEventHandler> _backFunctions = new Stack<BackNavigationEventHandler>();

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
