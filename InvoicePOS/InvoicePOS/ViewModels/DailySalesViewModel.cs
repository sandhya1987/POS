using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Company;
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

namespace InvoicePOS.ViewModels
{
    public class DailySalesViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        public HttpResponseMessage response;
        // public HttpClient client = new HttpClient();
        public ICommand select { get; set; }
        private readonly ItemModel OprModel;

        ItemModel[] data = null;

        List<ItemModel> _ListGrid_Temp = new List<ItemModel>();




        public List<ItemModel> _ListGrid { get; set; }
        public List<ItemModel> ListGrid
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

        public DailySalesViewModel()
        {
            SelectedBusinessLoca = new BusinessLocationModel();

        }
        private long _BUSINESS_LOCATION_ID;
        public long BUSINESS_LOCATION_ID
        {
            get
            {
                return SelectedBusinessLoca.BUSINESS_LOCATION_ID;
            }
            set
            {
                SelectedBusinessLoca.BUSINESS_LOCATION_ID = 1;

                if (SelectedBusinessLoca.BUSINESS_LOCATION_ID != value)
                {
                    SelectedBusinessLoca.BUSINESS_LOCATION_ID = value;
                    //App.Current.Properties["ItemName"] = value;
                    OnPropertyChanged("BUSINESS_LOCATION_ID");
                }
            }
        }

        private string _BUSINESS_LOCATION;
        public string BUSINESS_LOCATION
        {
            get
            {
                return SelectedBusinessLoca.BUSINESS_LOCATION;
            }
            set
            {
                SelectedBusinessLoca.BUSINESS_LOCATION = value;

                if (SelectedBusinessLoca.BUSINESS_LOCATION != value)
                {
                    SelectedBusinessLoca.BUSINESS_LOCATION = value;
                    //App.Current.Properties["ItemName"] = value;
                    OnPropertyChanged("BUSINESS_LOCATION");
                }
            }
        }



        private BusinessLocationModel _SelectedBusinessLoca;
        public BusinessLocationModel SelectedBusinessLoca
        {
            get { return _SelectedBusinessLoca; }
            set
            {
                if (_SelectedBusinessLoca != value)
                {
                    _SelectedBusinessLoca = value;
                    OnPropertyChanged("SelectedBusinessLoca");
                }

            }
        }

        public ICommand _ScrBussLocItm;
        public ICommand ScrBussLocItm
        {
            get
            {
                if (_ScrBussLocItm == null)
                {
                    _ScrBussLocItm = new DelegateCommand(ScrBussLoc_Item);
                }
                return _ScrBussLocItm;
            }
        }


        public void ScrBussLoc_Item()
        {
            SelectedBusinessLoca.BUSINESS_LOCATION_ID = 1;
            string iid = App.Current.Properties["Company_Id"].ToString();
            SelectedBusinessLoca.COMPANY_ID = Convert.ToInt32(iid);
            GetItemByLocation(SelectedBusinessLoca);
        }



        private DateTime _FromDt;
        public DateTime FromDt
        {
            get
            {
                return SelectedBusinessLoca.FromDt;
            }
            set
            {
                if (SelectedBusinessLoca.FromDt != value)
                {
                    SelectedBusinessLoca.FromDt = value;
                    OnPropertyChanged("FromDt");
                }
            }
        }

        private DateTime _ToDt;
        public DateTime ToDt
        {
            get
            {
                return SelectedBusinessLoca.ToDt;
            }
            set
            {
                if (SelectedBusinessLoca.ToDt != value)
                {
                    SelectedBusinessLoca.ToDt = value;
                    OnPropertyChanged("ToDt");
                }
            }
        }











        private long _COMPANY_ID;
        public long COMPANY_ID
        {
            get
            {
                return SelectedBusinessLoca.COMPANY_ID;
            }
            set
            {
                if (SelectedBusinessLoca.COMPANY_ID != value)
                {
                    SelectedBusinessLoca.COMPANY_ID = value;
                    OnPropertyChanged("COMPANY_ID");
                }
            }
        }

        private ItemModel _selectedItem;
        public ItemModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;

                    OnPropertyChanged("SelectedItem");
                }
            }
        }
        private string _ITEM_NAME;
        public string ITEM_NAME
        {
            get
            {
                return SelectedItem.ITEM_NAME;
            }
            set
            {
                SelectedItem.ITEM_NAME = value;

                if (SelectedItem.ITEM_NAME != value)
                {
                    SelectedItem.ITEM_NAME = value;
                    App.Current.Properties["ItemName"] = value;
                    OnPropertyChanged("ITEM_NAME");
                }
            }
        }
        private string _BARCODE;
        public string BARCODE
        {
            get
            {
                //var ss = GetbarcodeCode();
                //SelectedItem.BARCODE = Convert.ToString(ss);
                return SelectedItem.BARCODE;
            }
            set
            {
                SelectedItem.BARCODE = value;


                if (SelectedItem.BARCODE != value)
                {
                    SelectedItem.BARCODE = value;
                    OnPropertyChanged("BARCODE");
                }
            }
        }
        private decimal _MRP;
        public decimal MRP
        {
            get
            {
                return SelectedItem.MRP;
            }
            set
            {
                SelectedItem.MRP = value;

                if (SelectedItem.MRP != value)
                {
                    SelectedItem.MRP = value;
                    OnPropertyChanged("MRP");
                }
            }
        }
        private string _CATEGORY_NAME;
        public string CATEGORY_NAME
        {
            get
            {
                return SelectedItem.CATEGORY_NAME;
            }
            set
            {
                // SelectedItem.CATEGORY_NAME = value;

                if (SelectedItem.CATEGORY_NAME == value)
                {
                    SelectedItem.CATEGORY_NAME = value;
                    OnPropertyChanged("CATEGORY_NAME");
                }
            }
        }
        private int _OPN_QNT;
        public int? OPN_QNT
        {
            get
            {
                return SelectedItem.OPN_QNT;
            }
            set
            {
                SelectedItem.OPN_QNT = value;

                if (SelectedItem.OPN_QNT != value)
                {
                    SelectedItem.OPN_QNT = value;
                    OnPropertyChanged("OPN_QNT");
                }
            }
        }
        private decimal? _TOTAL;
        public decimal? TOTAL
        {
            get
            {
                return TOTAL;
            }
            set
            {
                TOTAL = Convert.ToDecimal(SelectedItem.OPN_QNT * SelectedItem.ITEM_PRICE);
                //SelectedItem.SALES_PRICE_BEFOR_TAX = value;

                //if (SelectedItem.SALES_PRICE_BEFOR_TAX != value)
                //{
                //    SelectedItem.SALES_PRICE_BEFOR_TAX = value;
                //    OnPropertyChanged("REGIONAL_LANGUAGE");
                //}
            }
        }



        public async void GetItemByLocation(BusinessLocationModel SelectedBusinessLoca)
        {
            try
            {
                decimal Tot = 0;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                var response = await client.PostAsJsonAsync("api/POAPI/GetAllItemByLocation/", SelectedBusinessLoca);
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;

                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;
                        _ListGrid_Temp.Add(new ItemModel
                        {
                            SLNO = x,
                            ITEM_NAME = data[i].ITEM_NAME,
                            ITEM_ID = data[i].ITEM_ID,
                            BARCODE = data[i].BARCODE,
                            ACCESSORIES_KEYWORD = data[i].ACCESSORIES_KEYWORD,
                            CATAGORY_ID = data[i].CATAGORY_ID,
                            ITEM_DESCRIPTION = data[i].ITEM_DESCRIPTION,
                            ITEM_INVOICE_ID = data[i].ITEM_INVOICE_ID,
                            ITEM_PRICE = data[i].ITEM_PRICE,
                            ITEM_PRODUCT_ID = data[i].ITEM_PRODUCT_ID,
                            KEYWORD = data[i].KEYWORD,
                            MRP = data[i].MRP,
                            PURCHASE_UNIT = data[i].PURCHASE_UNIT,
                            PURCHASE_UNIT_PRICE = data[i].PURCHASE_UNIT_PRICE,
                            SALES_PRICE = data[i].SALES_PRICE,
                            SALES_UNIT = data[i].SALES_UNIT,
                            SEARCH_CODE = data[i].SEARCH_CODE,
                            TAX_COLLECTED = data[i].TAX_COLLECTED,
                            TAX_PAID = data[i].TAX_PAID,
                            ALLOW_PURCHASE_ON_ESHOP = data[i].ALLOW_PURCHASE_ON_ESHOP,
                            CATEGORY_NAME = data[i].CATEGORY_NAME,
                            DISPLAY_INDEX = data[i].DISPLAY_INDEX,
                            INCLUDE_TAX = data[i].INCLUDE_TAX,
                            ITEM_GROUP_NAME = data[i].ITEM_GROUP_NAME,
                            ITEM_UNIQUE_NAME = data[i].ITEM_UNIQUE_NAME,
                            OPN_QNT = data[i].OPN_QNT,
                            REGIONAL_LANGUAGE = data[i].REGIONAL_LANGUAGE,
                            SALES_PRICE_BEFOR_TAX = data[i].SALES_PRICE_BEFOR_TAX,
                            MODIFICATION_DATE = data[i].MODIFICATION_DATE,


                            WEIGHT_OF_CARDBOARD = data[i].WEIGHT_OF_CARDBOARD,
                            WEIGHT_OF_FOAM = data[i].WEIGHT_OF_FOAM,
                            WEIGHT_OF_PAPER = data[i].WEIGHT_OF_PAPER,
                            WEIGHT_OF_PLASTIC = data[i].WEIGHT_OF_PLASTIC,
                        });
                        Tot = Tot + data[i].MRP;
                    }
                }
                ListGrid = _ListGrid_Temp;
                //TOTAL = Tot;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public ICommand _BusinessLocationClick;
        public ICommand BusinessLocationClick
        {
            get
            {
                if (_BusinessLocationClick == null)
                {
                    _BusinessLocationClick = new DelegateCommand(BusinessLocationList_Click);
                }
                return _BusinessLocationClick;
            }
        }


        public void BusinessLocationList_Click()
        {
            App.Current.Properties["DailySalesBussLocation"]=1;
            Window_BusinessLocationList BL = new Window_BusinessLocationList();
            BL.Show();
            //if (Convert.ToInt64(App.Current.Properties["LOC_ID"].ToString()) != 0)
            //{
            //    SelectedBusinessLoca.BUSINESS_LOCATION_ID = Convert.ToInt64(App.Current.Properties["LOC_ID"].ToString());
            //    SelectedBusinessLoca.BUSINESS_LOCATION = App.Current.Properties["BUSSINESS_LOC"].ToString();
            //}
        }





        public ICommand _DateSearchClick;
        public ICommand DateSearchClick
        {
            get
            {
                if (_DateSearchClick == null)
                {
                    _DateSearchClick = new DelegateCommand(DateSearch_Click);
                }
                return _DateSearchClick;
            }
        }


        public async void DateSearch_Click()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            HttpResponseMessage response = client.GetAsync("api/POAPI/GetAllItemByDate?LOCATION_MODEL=" + SelectedBusinessLoca + "").Result;
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                int x = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    x++;
                    _ListGrid_Temp.Add(new ItemModel
                    {
                        SLNO = x,
                        ITEM_NAME = data[i].ITEM_NAME,
                        ITEM_ID = data[i].ITEM_ID,
                        BARCODE = data[i].BARCODE,
                        ACCESSORIES_KEYWORD = data[i].ACCESSORIES_KEYWORD,
                        CATAGORY_ID = data[i].CATAGORY_ID,
                        ITEM_DESCRIPTION = data[i].ITEM_DESCRIPTION,
                        ITEM_INVOICE_ID = data[i].ITEM_INVOICE_ID,
                        ITEM_PRICE = data[i].ITEM_PRICE,
                        ITEM_PRODUCT_ID = data[i].ITEM_PRODUCT_ID,
                        KEYWORD = data[i].KEYWORD,
                        MRP = data[i].MRP,
                        PURCHASE_UNIT = data[i].PURCHASE_UNIT,
                        PURCHASE_UNIT_PRICE = data[i].PURCHASE_UNIT_PRICE,
                        SALES_PRICE = data[i].SALES_PRICE,
                        SALES_UNIT = data[i].SALES_UNIT,
                        SEARCH_CODE = data[i].SEARCH_CODE,
                        TAX_COLLECTED = data[i].TAX_COLLECTED,
                        TAX_PAID = data[i].TAX_PAID,
                        ALLOW_PURCHASE_ON_ESHOP = data[i].ALLOW_PURCHASE_ON_ESHOP,
                        CATEGORY_NAME = data[i].CATEGORY_NAME,
                        DISPLAY_INDEX = data[i].DISPLAY_INDEX,
                        INCLUDE_TAX = data[i].INCLUDE_TAX,
                        ITEM_GROUP_NAME = data[i].ITEM_GROUP_NAME,
                        ITEM_UNIQUE_NAME = data[i].ITEM_UNIQUE_NAME,
                        OPN_QNT = data[i].OPN_QNT,
                        REGIONAL_LANGUAGE = data[i].REGIONAL_LANGUAGE,
                        SALES_PRICE_BEFOR_TAX = data[i].SALES_PRICE_BEFOR_TAX,
                        MODIFICATION_DATE = data[i].MODIFICATION_DATE,
                    });
                }
                //}
            }
            ListGrid = _ListGrid_Temp;


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
