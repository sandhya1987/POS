using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Company;
using InvoicePOS.UserControll.Item;
using InvoicePOS.UserControll.PO;
using InvoicePOS.UserControll.Report.PO_Report;
using InvoicePOS.UserControll.Supplier;
using Newtonsoft.Json;
using System;
using System.Collections;
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
using InvoicePOS.UserControll.PO;
using System.ComponentModel;

namespace InvoicePOS.ViewModels
{
    public class POViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        //ItemModel[] data = null;
        public HttpResponseMessage response;
        private readonly POrderModel POModel;
        POrderModel _POModel = new POrderModel();
        public ICommand select { get; set; }
        POrderModel[] data = null;
        //POrderModel[] data1 = null;
        List<POrderModel> _ListGrid_Temp = new List<POrderModel>();
        List<POrderModel> _ListGrid_Temp1 = new List<POrderModel>();
        int x = 0;
        //ObservableCollection<ItemModel> _ListGrid_Temp1 = new ObservableCollection<ItemModel>();
        ItemModel[] data1 = null;
        public ObservableCollection<POrderModel> _ListGrid1 { get; set; }
        ObservableCollection<POrderModel> _ListGrid_PoItem = new ObservableCollection<POrderModel>();

        private POrderModel _SelectedPO;
        public POrderModel SelectedPO
        {
            get { return _SelectedPO; }
            set
            {
                if (_SelectedPO != value)
                {
                    _SelectedPO = value;
                    OnPropertyChanged("SelectedPO");
                }

            }
        }

        //public List<POrderModel> _ListGrid1 { get; set; }
        //public List<POrderModel> ListGrid1
        //{
        //    get
        //    {
        //        return _ListGrid1;
        //    }
        //    set
        //    {
        //        this._ListGrid1 = value;
        //        OnPropertyChanged("ListGridItem");
        //    }
        //}

        public ObservableCollection<POrderModel> ListGrid1
        {
            get
            {
                return _ListGrid1;

            }
            set
            {
                if (value != _ListGrid1)
                {
                    this._ListGrid1 = value;
                    OnPropertyChanged("ListGrid1");
                }
            }
        }

        private long _PO_ID;
        public long PO_ID
        {
            get
            {
                return SelectedPO.PO_ID;
            }
            set
            {
                SelectedPO.PO_ID = value;


                if (SelectedPO.PO_ID != value)
                {
                    SelectedPO.PO_ID = value;
                    OnPropertyChanged("PO_ID");
                }
            }
        }
        private int _PO_NUMBER;
        public int PO_NUMBER
        {
            get
            {
                return SelectedPO.PO_NUMBER;
            }
            set
            {
                SelectedPO.PO_NUMBER = value;


                if (SelectedPO.PO_NUMBER != value)
                {
                    SelectedPO.PO_NUMBER = value;
                    OnPropertyChanged("PO_NUMBER");
                }
            }
        }

        private string _PO_NUMBER1;
        public string PO_NUMBER1
        {
            get
            {
                return SelectedPO.PO_NUMBER1;
            }
            set
            {
                SelectedPO.PO_NUMBER1 = value;


                if (SelectedPO.PO_NUMBER1 != value)
                {
                    SelectedPO.PO_NUMBER1 = value;
                    OnPropertyChanged("PO_NUMBER");
                }
            }
        }
        private DateTime _DELIVERY_DATE;
        public DateTime DELIVERY_DATE
        {
            get
            {
                return SelectedPO.DELIVERY_DATE;
            }
            set
            {
                SelectedPO.DELIVERY_DATE = value;



                if (SelectedPO.DELIVERY_DATE != value)
                {
                    SelectedPO.DELIVERY_DATE = System.DateTime.Now;
                    OnPropertyChanged("DELIVERY_DATE");
                }
            }
        }
        private DateTime _PO_DATE;
        public DateTime PO_DATE
        {
            get
            {
                return SelectedPO.PO_DATE;
            }
            set
            {
                SelectedPO.PO_DATE = DateTime.Now;


                if (SelectedPO.PO_DATE != value)
                {
                    SelectedPO.PO_DATE = DateTime.Now;
                    OnPropertyChanged("PO_DATE");
                }
            }
        }
        private bool _IS_SEND_MAIL;
        public bool IS_SEND_MAIL
        {
            get
            {
                return SelectedPO.IS_SEND_MAIL;
            }
            set
            {
                SelectedPO.IS_SEND_MAIL = value;


                if (SelectedPO.IS_SEND_MAIL != value)
                {
                    SelectedPO.IS_SEND_MAIL = value;
                    OnPropertyChanged("IS_SEND_MAIL");
                }
            }
        }
        private string _BAR_CODE;
        public string BAR_CODE
        {
           
                get
            {
                return SelectedPO.BAR_CODE;
            }
            set
            {
                SelectedPO.BAR_CODE = value;
                    if (string.IsNullOrEmpty(value))
                    {
                        throw new ApplicationException("Field not be blank");
                    }

                    if (SelectedPO.BAR_CODE != value)
                {
                    SelectedPO.BAR_CODE = value;
                    OnPropertyChanged("BAR_CODE");
                }
            }
        }

        private string _PO_STATUS;
        public string POSTATUS
        {
            get
            {
                return SelectedPO.PO_STATUS;
            }
            set
            {
                SelectedPO.PO_STATUS = value;


                if (SelectedPO.PO_STATUS != value)
                {
                    SelectedPO.PO_STATUS = value;
                    OnPropertyChanged("PO_STATUS");
                }
            }
        }


        private string _PO_TYPE;
        public string PO_TYPE
        {
            get
            {
                return SelectedPO.PO_TYPE;
            }
            set
            {
                SelectedPO.PO_TYPE = value;


                if (SelectedPO.PO_TYPE != value)
                {
                    SelectedPO.PO_TYPE = value;
                    OnPropertyChanged("PO_TYPE");
                }
            }
        }

        private string _ITEM_NAME;
        public string ITEM_NAME
        {
            get
            {
                return SelectedPO.ITEM_NAME;
            }
            set
            {
                SelectedPO.ITEM_NAME = value;


                if (SelectedPO.ITEM_NAME != value)
                {
                    SelectedPO.ITEM_NAME = value;


                    OnPropertyChanged("ITEM_NAME");
                }
            }
        }

        //List<POrderModel> _ListGrid_PoItem = new List<POrderModel>();
        private POrderModel _SelectItem;
        public POrderModel SelectItem
        {

            get { return _SelectItem; }
            set
            {
                _SelectItem = value;
                OnPropertyChanged("SelectItem");
            }

        }

        public async Task<ObservableCollection<POrderModel>> GetPoItem(int comp)
        {
            ReportPO.ListGridRef = new DataGrid();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/POAPI/GetPoItem?id=" + SelectedPO.PO_ID + "").Result;
            if (response.IsSuccessStatusCode)
            {
                data1 = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                // POData = new List<POrderModel>();
                // int x = 0;
                for (int i = 0; i < data1.Length; i++)
                {

                    _ListGrid_PoItem.Add(new POrderModel
                    {
                        SLNO = i + 1,

                        //PO_DATE = data1[i].PO_DATE,

                        ITEM_NAME = data1[i].ITEM_NAME,
                        ITEM_ID = data1[i].ITEM_ID,
                        BAR_CODE = data1[i].BARCODE,
                        PURCHASE_UNIT_PRICE = data1[i].PURCHASE_UNIT_PRICE,
                        MRP = data1[i].MRP,
                        SEARCH_CODE = data1[i].SEARCH_CODE,
                        TOTAL_QTY = data1[i].Current_Qty,

                        TAX_PAID = data1[i].TAX_PAID,
                        //PURCHASE_PRICE_BEFORE_TAX = ((decimal)(data1[i].PURCHASE_UNIT_PRICE * 100) / ((data1[i].TAX_PAID) + 100)),
                        //SUB_TOTAL_AFTER_TAX = ((decimal)(data1[i].Current_Qty) * (data1[i].PURCHASE_UNIT_PRICE)),
                        //SUB_TOTAL_BEFORE_TAX = ((decimal)(data1[i].Current_Qty) * (PURCHASE_PRICE_BEFORE_TAX)),

                    });
                }

                for (int i = 0; i < _ListGrid_PoItem.Count; i++)
                {
                    (_ListGrid_PoItem[i].PURCHASE_PRICE_BEFORE_TAX) = ((decimal)(_ListGrid_PoItem[i].PURCHASE_UNIT_PRICE * 100) / ((_ListGrid_PoItem[i].TAX_PAID) + 100));
                    (_ListGrid_PoItem[i].SUB_TOTAL_AFTER_TAX) = ((decimal)(_ListGrid_PoItem[i].TOTAL_QTY) * (_ListGrid_PoItem[i].PURCHASE_UNIT_PRICE));
                    (_ListGrid_PoItem[i].SUB_TOTAL_BEFORE_TAX) = ((decimal)(_ListGrid_PoItem[i].TOTAL_QTY) * (_ListGrid_PoItem[i].PURCHASE_PRICE_BEFORE_TAX));
                }

                //_ListGrid_PoItem = _ListGrid_PoItem;
                ListGrid1 = _ListGrid_PoItem;
                App.Current.Properties["EditGridData"] = ListGrid1;




            }
            return new ObservableCollection<POrderModel>(_ListGrid_PoItem);
        }

        private string _SEARCH_PO;
        public string SEARCH_PO
        {
            get
            {
                return _SEARCH_PO;
            }
            set
            {


                if (_SEARCH_PO != value)
                {
                    _SEARCH_PO = value;

                    if (_SEARCH_PO != "" && _SEARCH_PO != null)
                    {

                        GetPOList(COMPANY_ID);
                    }
                    else
                    {
                        GetPOList(COMPANY_ID);

                    }
                    OnPropertyChanged("SEARCH_PO");
                }
            }
        }
        private long _COMPANY_ID;
        public long COMPANY_ID
        {
            get
            {
                return SelectedPO.COMPANY_ID;
            }
            set
            {
                SelectedPO.COMPANY_ID = value;


                if (SelectedPO.COMPANY_ID != value)
                {
                    SelectedPO.COMPANY_ID = value;
                    OnPropertyChanged("COMPANY_ID");
                }
            }
        }
        private string _SEARCH_CODE;
        public string SEARCH_CODE
        {
            get
            {
                return SelectedPO.SEARCH_CODE;
            }
            set
            {
                SelectedPO.SEARCH_CODE = value;


                if (SelectedPO.SEARCH_CODE != value)
                {
                    SelectedPO.SEARCH_CODE = value;
                    OnPropertyChanged("SEARCH_CODE");
                }
            }
        }
        private long _DELIVER_ID;
        public long DELIVER_ID
        {
            get
            {
                return SelectedPO.DELIVER_ID;
            }
            set
            {
                SelectedPO.DELIVER_ID = value;


                if (SelectedPO.DELIVER_ID != value)
                {
                    SelectedPO.DELIVER_ID = value;
                    OnPropertyChanged("DELIVER_ID");
                }
            }
        }



        private string _DELIVER;
        public string DELIVER
        {
            get
            {
                return SelectedPO.DELIVER;
            }
            set
            {
                SelectedPO.DELIVER = value;


                if (SelectedPO.DELIVER != value)
                {
                    SelectedPO.DELIVER = value;
                    OnPropertyChanged("DELIVER");
                }
            }
        }
        private long _SUPPLIER_ID;
        public long SUPPLIER_ID
        {
            get
            {
                return SelectedPO.SUPPLIER_ID;
            }
            set
            {
                SelectedPO.SUPPLIER_ID = value;


                if (SelectedPO.SUPPLIER_ID != value)
                {
                    SelectedPO.SUPPLIER_ID = value;
                    OnPropertyChanged("SUPPLIER_ID");
                }
            }
        }
        private string _SUPPLIER;
        public string SUPPLIER
        {
            get
            {
                return SelectedPO.SUPPLIER;
            }
            set
            {
                SelectedPO.SUPPLIER = value;


                if (SelectedPO.SUPPLIER != value)
                {
                    SelectedPO.SUPPLIER = value;
                    OnPropertyChanged("SUPPLIER");
                }
            }
        }
        private long _BUSINESS_LOCATION_ID;
        public long BUSINESS_LOCATION_ID
        {
            get
            {
                return _BUSINESS_LOCATION_ID;
            }
            set
            {
                _BUSINESS_LOCATION_ID = value;
                OnPropertyChanged("BUSINESS_LOCATION_ID");

            }
        }
        private string _BUSINESS_LOCATION;
        public string BUSINESS_LOCATION
        {
            get
            {
                return SelectedPO.BUSINESS_LOCATION;
            }
            set
            {

                SelectedPO.BUSINESS_LOCATION = value;
                if (SelectedPO.BUSINESS_LOCATION != value)
                {
                    SelectedPO.BUSINESS_LOCATION = value;
                    OnPropertyChanged("BUSINESS_LOCATION");
                }
            }
        }
        private string _DELIVER_TO;
        public string DELIVER_TO
        {
            get
            {
                return SelectedPO.DELIVER_TO;
            }
            set
            {
                SelectedPO.DELIVER_TO = value;
                if (SelectedPO.DELIVER_TO != value)
                {
                    SelectedPO.DELIVER_TO = value;
                    OnPropertyChanged("DELIVER_TO");
                }
            }
        }
        private string _SUPPLIER_EMAIL;
        public string SUPPLIER_EMAIL
        {
            get
            {
                return SelectedPO.SUPPLIER_EMAIL;
            }
            set
            {
                SelectedPO.SUPPLIER_EMAIL = value;


                if (SelectedPO.SUPPLIER_EMAIL != value)
                {
                    SelectedPO.SUPPLIER_EMAIL = value;
                    OnPropertyChanged("SUPPLIER_EMAIL");
                }
            }
        }
        private string _TERMS;
        public string TERMS
        {
            get
            {
                return SelectedPO.TERMS;
            }
            set
            {
                SelectedPO.TERMS = value;


                if (SelectedPO.TERMS != value)
                {
                    SelectedPO.TERMS = value;
                    OnPropertyChanged("TERMS");
                }
            }
        }
        private decimal _TOTAL_TAX;
        public decimal TOTAL_TAX
        {
            get
            {
                return SelectedPO.TOTAL_TAX;
            }
            set
            {
                SelectedPO.TOTAL_TAX = value;


                if (SelectedPO.TOTAL_TAX != value)
                {
                    SelectedPO.TOTAL_TAX = value;
                    OnPropertyChanged("TOTAL_TAX");
                }
            }
        }
        private string _SearchStock;
        public string SearchStock
        {
            get
            {
                return SelectedPO.SearchStock;
            }
            set
            {
                SelectedPO.SearchStock = value;
                OnPropertyChanged("TOTAL_TAX");

            }
        }


        private decimal _TOTAL_AMOUNT;
        public decimal TOTAL_AMOUNT
        {
            get
            {
                return SelectedPO.TOTAL_AMOUNT;
            }
            set
            {
                SelectedPO.TOTAL_AMOUNT = value;


                if (SelectedPO.TOTAL_AMOUNT != value)
                {
                    SelectedPO.TOTAL_AMOUNT = value;
                    OnPropertyChanged("TOTAL_AMOUNT");
                }
            }
        }


        private decimal _TAX_PAID;
        public decimal TAX_PAID
        {
            get
            {
                return SelectedPO.TAX_PAID;
            }
            set
            {
                TAX_PAID = value;


                if (TAX_PAID != value)
                {
                    TAX_PAID = value;
                    OnPropertyChanged("TAX_PAID");
                }
            }
        }



        private decimal? _SUB_TOTAL_AFTER_TAX;
        public decimal? SUB_TOTAL_AFTER_TAX
        {
            get
            {
                return SelectedPO.SUB_TOTAL_AFTER_TAX;
            }
            set
            {
                SelectedPO.SUB_TOTAL_AFTER_TAX = value;


                if (SUB_TOTAL_AFTER_TAX != value)
                {
                    SelectedPO.SUB_TOTAL_AFTER_TAX = value;
                    OnPropertyChanged("SUB_TOTAL_AFTR_TAX");
                }
            }
        }



        private decimal? _SUB_TOTAL_BEFORE_TAX;
        public decimal? SUB_TOTAL_BEFORE_TAX
        {
            get
            {
                return SelectedPO.SUB_TOTAL_BEFORE_TAX;
            }
            set
            {
                SelectedPO.SUB_TOTAL_BEFORE_TAX = value;


                if (SelectedPO.SUB_TOTAL_BEFORE_TAX != value)
                {
                    SelectedPO.SUB_TOTAL_BEFORE_TAX = value;
                    OnPropertyChanged("SUB_TOTAL_BEFORE_TAX");
                }
            }
        }

        private decimal _PURCHASE_PRICE_BEFORE_TAX;
        public decimal PURCHASE_PRICE_BEFORE_TAX
        {
            get
            {
                return SelectedPO.PURCHASE_PRICE_BEFORE_TAX;
            }
            set
            {
                SelectedPO.PURCHASE_PRICE_BEFORE_TAX = value;


                if (SelectedPO.PURCHASE_PRICE_BEFORE_TAX != value)
                {
                    SelectedPO.PURCHASE_PRICE_BEFORE_TAX = value;
                    OnPropertyChanged("PURCHASE_PRICE_BEFORE_TAX");
                }
            }
        }

        private decimal _PURCHASE_UNIT_PRICE;
        public decimal PURCHASE_UNIT_PRICE
        {
            get
            {
                return SelectedPO.PURCHASE_UNIT_PRICE;
            }
            set
            {
                PURCHASE_UNIT_PRICE = value;


                if (PURCHASE_UNIT_PRICE != value)
                {
                    PURCHASE_UNIT_PRICE = value;
                    OnPropertyChanged("PURCHASE_UNIT_PRICE");
                }
            }
        }


        private ICommand _CALCULATE { get; set; }
        public ICommand CALCULATE
        {
            get
            {
                if (_CALCULATE == null)
                {
                    _CALCULATE = new DelegateCommand(Calculation);
                }
                return _CALCULATE;
            }
        }


        private int? _TOTAL_QTY;
        public int? TOTAL_QTY
        {
            get
            {
                return _TOTAL_QTY;
            }
            set
            {
                if (SelectedItem != null)
                {
                    _TOTAL_QTY = value;
                }
                else
                    MessageBox.Show("Please Select Item");
                OnPropertyChanged("TOTAL_QTY");

            }
        }

        private long? _QUNT_TOTAL;
        public long? QUNT_TOTAL
        {
            get
            {
                return _QUNT_TOTAL;
            }
            set
            {
                _QUNT_TOTAL = value;
                OnPropertyChanged("QUNT_TOTAL");

            }
        }
        private POrderModel _selectedItem;
        public POrderModel SelectedItem
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

        public void LoadGrid()
        {
            var comp = 1;
        }
        public void Calculation()
        {
            if (ListGrid1 != null)
            {
                if (SelectedItem != null)
                {
                    if (SelectedItem.BAR_CODE != null && SelectedItem.BAR_CODE != "")
                    {
                        var Item = (from a in ListGrid1 where a.BAR_CODE == SelectedItem.BAR_CODE || a.ITEM_NAME == AddPO.ItemRef.Text || a.SEARCH_CODE == AddPO.ScrRef.Text select a).FirstOrDefault();
                        var RemoveItem = (from a in ListGrid1 where a.BAR_CODE == SelectedItem.BAR_CODE || a.ITEM_NAME == AddPO.ItemRef.Text || a.SEARCH_CODE == AddPO.ScrRef.Text select a).FirstOrDefault();


                        ListGrid1.Remove(RemoveItem);
                        //x = x + 1;
                        ListGrid1.Add(new POrderModel
                        {

                            SLNO = x,
                            ITEM_NAME = Item.ITEM_NAME,
                            ITEM_ID = Item.ITEM_ID,
                            BAR_CODE = Item.BAR_CODE,
                            SEARCH_CODE = Item.SEARCH_CODE,
                            PURCHASE_UNIT_PRICE = Item.PURCHASE_UNIT_PRICE,

                            MRP = Item.MRP,

                            TAX_PAID = Item.TAX_PAID,


                            PURCHASE_PRICE_BEFORE_TAX = ((decimal)(Item.PURCHASE_UNIT_PRICE * 100) / ((Item.TAX_PAID) + 100)),

                            //PURCHASE_PRICE_BEFORE_TAX = Math.Round((Item.PURCHASE_UNIT_PRICE * 100) / ((Item.TAX_PAID) + 100)),
                            TOTAL_QTY = TOTAL_QTY,

                            SUB_TOTAL_AFTER_TAX = ((TOTAL_QTY) * (Item.PURCHASE_UNIT_PRICE)),
                            SUB_TOTAL_BEFORE_TAX = ((TOTAL_QTY) * (Item.PURCHASE_PRICE_BEFORE_TAX)),

                            //}


                        });

                        //decimal b = SUB_TOTAL_BEFORE_TAX;

                        //Math.Round(b, 2);
                        App.Current.Properties["DataGridLPO"] = ListGrid1;
                        ListGrid1 = ListGrid1;
                    }
                    else
                    {
                        MessageBox.Show("Please Select a Row From Grid For Quantity Calculation", "Item Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                    MessageBox.Show("Please Select Item");

                TOTAL_AMOUNT = 0;
                TOTAL_TAX = 0;
                for (int i = 0; i < ListGrid1.Count; i++)
                {
                    TOTAL_AMOUNT = Convert.ToDecimal(ListGrid1[i].SUB_TOTAL_AFTER_TAX + TOTAL_AMOUNT);
                    //App.Current.Properties["CurrentGrosAmount1"] = TOTAL_AMOUNT;
                    TOTAL_TAX = Convert.ToDecimal((ListGrid1[i].SUB_TOTAL_AFTER_TAX - ListGrid1[i].SUB_TOTAL_BEFORE_TAX) + TOTAL_TAX);

                }
                TOTAL_TAX = Math.Round(TOTAL_TAX, 2);

                //decimal a1 = TOTAL_AMOUNT == 0 ? 0 : TOTAL_AMOUNT;
                //if (a1 != 0)
                //{
                //    string[] str1 = a1.ToString().Split('.');
                //    decimal num2 = Convert.ToDecimal("0." + str1[1]);
                //    decimal res2;
                //    if (Convert.ToDouble(num2) < 0.50)
                //    {
                //        res2 = Math.Floor(a1);
                //        TOTAL_AMOUNT = Convert.ToInt32(res2);
                //    }
                //    else
                //    {
                //        res2 = Math.Ceiling(a1);
                //        TOTAL_AMOUNT = Convert.ToInt32(res2);
                //    }

                //}
                //else
                //{
                //    TOTAL_AMOUNT = 0;
                //}


                //decimal b1 = TOTAL_TAX == 0 ? 0 : TOTAL_TAX;
                //if (b1 != 0)
                //{
                //    string[] str1 = a1.ToString().Split('.');
                //    decimal num2 = Convert.ToDecimal("0." + str1[1]);
                //    decimal res2;
                //    if (Convert.ToDouble(num2) < 0.50)
                //    {
                //        res2 = Math.Floor(b1);
                //        TOTAL_TAX = Convert.ToInt32(res2);
                //    }
                //    else
                //    {
                //        res2 = Math.Ceiling(b1);
                //        TOTAL_TAX = Convert.ToInt32(res2);
                //    }

                //}
                //else
                //{
                //    TOTAL_TAX = 0;
                //}
                AddPO.TotalQty.Text = "";
                AddPO.TotTaxRef.Text = TOTAL_TAX.ToString();
                AddPO.TotRef.Text = TOTAL_AMOUNT.ToString();





            }
            else
            {
                MessageBox.Show("First Enter Barcode/Item Name/Search Code For Quantity Calculation", "Item Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }


        public ICommand _AddPOClick { get; set; }
        public ICommand AddPOClick
        {
            get
            {
                if (_AddPOClick == null)
                {

                    _AddPOClick = new DelegateCommand(AddPO_Click);
                }
                return _AddPOClick;
            }
        }
        public void AddPO_Click()
        {
            AddPO _po = new AddPO();
            _po.Show();
            // ModalService.NavigateTo(new AddPO(), delegate(bool returnValue) { });

        }
        public ICommand _InsertPO { get; set; }
        public ICommand InsertPO
        {

            get
            {
                if (_InsertPO == null)
                {
                    //isenable = false;
                    //_isenable = false;
                    _InsertPO = new DelegateCommand(Add_PurchageOrder);
                }
                return _InsertPO;
            }
        }


        # region tab Change on main page and Show Stock Page

        private string _Select_BarCode { get; set; }
        public string Select_BarCode
        {
            get
            {

                return _Select_BarCode;

            }
            set
            {
                _Select_BarCode = value;
                App.Current.Properties["CurrentBarcode"] = value;
                OnPropertyChanged("Select_BarCode");


            }

        }

        private ICommand _TabChangeCode { get; set; }
        public ICommand TabChangeCode
        {
            get
            {
                if (_TabChangeCode == null)
                {
                    _TabChangeCode = new DelegateCommand(TabChange_Code);
                }
                return _TabChangeCode;
            }
        }

        public void TabChange_Code()
        {

            //GetBarcodeSearch(comp);
            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            GetPOList1(comp);


            _Select_BarCode = "";
            Select_BarCode = "";
            AddPO.ItemRef.Text = "";
            AddPO.ScrRef.Text = "";
        }


        #endregion


        private int _ITEM_ID;
        public int ITEM_ID
        {
            get
            {
                return SelectedPO.ITEM_ID;
            }
            set
            {
                SelectedPO.ITEM_ID = value;
                OnPropertyChanged("ROUNDOFF_AMOUNT");

            }
        }




        public async void Add_PurchageOrder()
        {
            isenable = false;
            _isenable = false;
            var datagrid = App.Current.Properties["DataGridLPO"] as ObservableCollection<POrderModel>;

            SelectedPO.SelectedItem = datagrid;

            //if (datagrid != null)
            //{
            //    //    DISCOUNT_INCLUDED = 0;
            //    for (int i = 0; i < datagrid.Count; i++)
            //    {
            //        //if(datagrid[i].TOTAL_QTY!=0)
            //        //NUMBER_OF_ITEM = Convert.ToInt32(i + 1);
            //        //QUANTITY_TOTAL = Convert.ToInt32(datagrid[i].Current_Qty + QUANTITY_TOTAL);
            //        //BEFORE_ROUNDOFF = Convert.ToDecimal((datagrid[i].Total + BEFORE_ROUNDOFF).ToString("0.00"));
            //        //decimal asas = ((decimal)(datagrid[i].Current_Qty) * (datagrid[i].Total));
            //        //GROSSAMT = Convert.ToDecimal(((asas) + GROSSAMT).ToString("0.00"));
            //        //ITEM_ID = datagrid[i].ITEM_ID;
            //        //Current_Qty = datagrid[i].TOTAL_QTY;
            //        //DISCOUNT_INCLUDED = datagrid[i].Discount + Convert.ToDecimal(DISCOUNT_INCLUDED);
            //    }
            //}
            if (App.Current.Properties["AddPOBussLocation"] != null)
            {
                SelectedPO.BUSINESS_LOCATION = App.Current.Properties["AddPOBussLocation"].ToString();
            }

            if (App.Current.Properties["AddPODeliveryTo"] != null)
            {
                SelectedPO.DELIVER_TO = App.Current.Properties["AddPODeliveryTo"].ToString();
            }

            if (App.Current.Properties["AddPOsupplier"] != null)
            {
                SelectedPO.SUPPLIER = App.Current.Properties["AddPOsupplier"].ToString();
            }
            if (App.Current.Properties["AddPOsuppEmail"] != null)
            {
                SelectedPO.SUPPLIER_EMAIL = App.Current.Properties["AddPOsuppEmail"].ToString();
            }

            if (App.Current.Properties["AddPOItem"] != null)
            {
                SelectedPO.ITEM_NAME = App.Current.Properties["AddPOItem"].ToString();
            }

            SelectedPO.PO_STATUS = "Pending";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            SelectedPO.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            var response = await client.PostAsJsonAsync("api/POAPI/PurchaseOrdAdd/", SelectedPO);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Po insert Successfully");
                AddPO.TotTaxRef.Text = null;
                AddPO.TotRef.Text = null;
                isenable = true;
                _isenable = true;
                Cancel_PO();
                ModalService.NavigateTo(new POList(), delegate (bool returnValue) { });
            }




        }
        public ICommand _UpdatePO { get; set; }
        public ICommand UpdatePO
        {
            get
            {
                if (_UpdatePO == null)
                {

                    _UpdatePO = new DelegateCommand(Update_PurchageOrder);
                }
                return _UpdatePO;
            }
        }



        public ICommand _QtyCalculate { get; set; }
        public ICommand QtyCalculate
        {
            get
            {
                if (_QtyCalculate == null)
                {

                    _QtyCalculate = new DelegateCommand(Calculation);
                }
                return _QtyCalculate;
            }
        }

        public async void Update_PurchageOrder()
        {
            if (ListGrid1 != null)
            {

                //var datagrid1 = App.Current.Properties["DataGridLPO"] as ObservableCollection<POrderModel>;
                //SelectedPO.SelectedItem = datagrid1;

                SelectedPO.SelectedItem = ListGrid1;
            }

            SelectedPO.PO_NUMBER1 = AddPO.PoNo.Text;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            SelectedPO.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            var response = await client.PostAsJsonAsync("api/POAPI/UpdatePO/", SelectedPO);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Po update Successfully");
                Cancel_PO();
                ModalService.NavigateTo(new POList(), delegate (bool returnValue) { });
            }


        }
        private List<POrderModel> _POData;
        public List<POrderModel> POData
        {
            get { return _POData; }
            set
            {
                if (_POData != value)
                {
                    _POData = value;
                }
            }
        }
        public ICommand _DeletePO;
        public ICommand DeletePO
        {
            get
            {
                if (_DeletePO == null)
                {
                    _DeletePO = new DelegateCommand(Delete_PO);
                }
                return _DeletePO;
            }
        }
        public async void Delete_PO()
        {
            if (SelectedPO.PO_ID != null && SelectedPO.PO_ID != 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure Delete this PO " + SelectedPO.PO_NUMBER + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {

                    var id = SelectedPO.PO_ID;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    HttpResponseMessage response = client.GetAsync("api/POAPI/DeletePO?id=" + id + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {
                        //ModalService.NavigateTo(new Items(), delegate(bool returnValue) { });
                        MessageBox.Show("Po Delete Successfully");
                        ModalService.NavigateTo(new POList(), delegate (bool returnValue) { });
                    }
                }
                else
                {
                    Cancel_PO();
                }
            }
            else
            {
                MessageBox.Show("Select PO");
            }

        }
        public ICommand _EditPO { get; set; }
        public ICommand EditPO
        {
            get
            {
                if (_EditPO == null)
                {
                    _EditPO = new DelegateCommand(Edit_PO);
                }
                return _EditPO;
            }
        }
        public async void Edit_PO()
        {
            if (SelectedPO.PO_ID != null && SelectedPO.PO_ID != 0)
            {
                App.Current.Properties["Action"] = "Edit";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/POAPI/PurchaseOrdEdit?id=" + SelectedPO.PO_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<POrderModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedPO.BAR_CODE = data[i].BAR_CODE;
                            SelectedPO.BUSINESS_LOCATION = data[i].BUSINESS_LOCATION;
                            SelectedPO.BUSINESS_LOCATION_ID = data[i].BUSINESS_LOCATION_ID;
                            SelectedPO.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedPO.DELIVER = data[i].DELIVER;
                            SelectedPO.DELIVER_ID = data[i].DELIVER_ID;
                            SelectedPO.DELIVER_TO = data[i].DELIVER_TO;
                            SelectedPO.DELIVERY_DATE = data[i].DELIVERY_DATE;
                            SelectedPO.IS_SEND_MAIL = data[i].IS_SEND_MAIL;
                            SelectedPO.ITEM_NAME = data[i].ITEM_NAME;
                            SelectedPO.PO_ID = data[i].PO_ID;
                            SelectedPO.PO_NUMBER1 = data[i].PO_NUMBER1;
                            SelectedPO.SEARCH_CODE = data[i].SEARCH_CODE;
                            SelectedPO.SUPPLIER = data[i].SUPPLIER;
                            SelectedPO.SUPPLIER_EMAIL = data[i].SUPPLIER_EMAIL;
                            SelectedPO.SUPPLIER_ID = data[i].SUPPLIER_ID;
                            SelectedPO.TERMS = data[i].TERMS;
                            SelectedPO.TOTAL_AMOUNT = data[i].TOTAL_AMOUNT;
                            SelectedPO.TOTAL_TAX = data[i].TOTAL_TAX;
                            SelectedPO.PO_DATE = data[i].PO_DATE;
                            SelectedPO.SearchStock = data[i].SearchStock;
                        }
                        App.Current.Properties["POEdit"] = SelectedPO;
                        AddPO _po = new AddPO();

                        _po.Show();
                        //ModalService.NavigateTo(new AddPO(), delegate(bool returnValue) { });
                    }
                }
            }
            else
            {
                MessageBox.Show("Select Item first", "Item Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }
        public ICommand _Cancel;
        public ICommand Cancel
        {
            get
            {
                if (_Cancel == null)
                {
                    _Cancel = new DelegateCommand(Cancel_PO);
                }
                return _Cancel;
            }
        }



        public void Cancel_PO()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    if (ListGrid1 != null)
                    {
                        ListGrid1.Clear();
                    }
                    window.Close();
                }
            }
        }
        public ICommand _ViewPO { get; set; }
        public ICommand ViewPO
        {
            get
            {
                if (_ViewPO == null)
                {
                    _ViewPO = new DelegateCommand(View_PO);
                }
                return _ViewPO;
            }
        }
        public async void View_PO()
        {
            if (SelectedPO.PO_ID != null && SelectedPO.PO_ID != 0)
            {
                App.Current.Properties["Action"] = "View";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/POAPI/PurchaseOrdEdit?id=" + SelectedPO.PO_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<POrderModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedPO.BAR_CODE = data[i].BAR_CODE;
                            SelectedPO.BUSINESS_LOCATION = data[i].BUSINESS_LOCATION;
                            SelectedPO.BUSINESS_LOCATION_ID = data[i].BUSINESS_LOCATION_ID;
                            SelectedPO.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedPO.DELIVER = data[i].DELIVER;
                            SelectedPO.DELIVER_ID = data[i].DELIVER_ID;
                            SelectedPO.DELIVER_TO = data[i].DELIVER_TO;
                            SelectedPO.DELIVERY_DATE = data[i].DELIVERY_DATE;
                            SelectedPO.IS_SEND_MAIL = data[i].IS_SEND_MAIL;
                            SelectedPO.ITEM_NAME = data[i].ITEM_NAME;
                            SelectedPO.PO_ID = data[i].PO_ID;
                            SelectedPO.PO_NUMBER1 = data[i].PO_NUMBER1;
                            SelectedPO.SEARCH_CODE = data[i].SEARCH_CODE;
                            SelectedPO.SUPPLIER = data[i].SUPPLIER;
                            SelectedPO.SUPPLIER_EMAIL = data[i].SUPPLIER_EMAIL;
                            SelectedPO.SUPPLIER_ID = data[i].SUPPLIER_ID;
                            SelectedPO.TERMS = data[i].TERMS;
                            SelectedPO.TOTAL_AMOUNT = data[i].TOTAL_AMOUNT;
                            SelectedPO.TOTAL_TAX = data[i].TOTAL_TAX;
                            SelectedPO.PO_DATE = data[i].PO_DATE;
                        }
                        App.Current.Properties["POView"] = SelectedPO;
                        ViewPO _as = new ViewPO();
                        _as.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Select PO first", "PO Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }

        private ObservableCollection<ItemModel> _ItemData;
        public ObservableCollection<ItemModel> ItemData
        {
            get { return _ItemData; }
            set
            {
                if (_ItemData != value)
                {
                    _ItemData = value;
                }
            }

        }
        ObservableCollection<POrderModel> AddListGrid1 = new ObservableCollection<POrderModel>();
        public async void GetPOList1(int comp)
        {
            ObservableCollection<POrderModel> _ListGrid_Temp1 = new ObservableCollection<POrderModel>();
            //ItemData = new ObservableCollection<ItemModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            HttpResponseMessage response = client.GetAsync("api/ItemAPI/GetAllItem?id=" + comp + "").Result;
            if (response.IsSuccessStatusCode)
            {
                data1 = JsonConvert.DeserializeObject<ItemModel[]>(await response.Content.ReadAsStringAsync());
                if (data1.Length > 0)
                {
                    for (int i = 0; i < data1.Length; i++)
                    {
                        _ListGrid_Temp1.Add(new POrderModel
                        {

                            //Discount = data1[i].Discount,
                            SLNO = i + 1,
                            ITEM_NAME = data1[i].ITEM_NAME,
                            ITEM_ID = data1[i].ITEM_ID,
                            BAR_CODE = data1[i].BARCODE,
                            PURCHASE_UNIT_PRICE = data1[i].PURCHASE_UNIT_PRICE,
                            MRP = data1[i].MRP,
                            SEARCH_CODE = data1[i].SEARCH_CODE,
                            TAX_PAID = data1[i].TAX_PAID,
                            TaxName = data1[i].TAX_PAID_NAME,
                            PURCHASE_PRICE_BEFORE_TAX = ((decimal)(data1[i].PURCHASE_UNIT_PRICE * 100) / ((data1[i].TAX_PAID) + 100)),
                            //SUB_TOTAL_AFTR_TAX = ((decimal)(data1[i].TOTAL_QNT) * (data[i].PURCHASE_PRICE_BEFOR_TAX)),

                            //SUB_TOTAL_AFTER_TAX = ((TOTAL_QTY) * (data1[i].PURCHASE_UNIT_PRICE)),
                            //SUB_TOTAL_BEFORE_TAX = ((TOTAL_QTY) * (PURCHASE_PRICE_BEFORE_TAX)),
                        });
                    }

                    App.Current.Properties["DataGridPuzzale"] = _ListGrid_Temp1;
                }

                if (App.Current.Properties["DataGridLPO"] != null)
                {
                    AddListGrid1 = App.Current.Properties["DataGridLPO"] as ObservableCollection<POrderModel>;
                    if (App.Current.Properties["EditGridData"] != null)
                    {
                        AddListGrid1 = App.Current.Properties["EditGridData"] as ObservableCollection<POrderModel>;
                        App.Current.Properties["EditItemGrid"] = (from a in AddListGrid1 where a.BAR_CODE == Select_BarCode || a.ITEM_NAME == AddPO.ItemRef.Text || a.SEARCH_CODE == AddPO.ScrRef.Text select a).FirstOrDefault();


                    }
                }
                else
                {
                    AddListGrid1 = new ObservableCollection<POrderModel>();
                    if (App.Current.Properties["EditGridData"] != null)
                    {
                        AddListGrid1 = App.Current.Properties["EditGridData"] as ObservableCollection<POrderModel>;
                        App.Current.Properties["EditItemGrid"] = (from a in AddListGrid1 where a.BAR_CODE == Select_BarCode || a.ITEM_NAME == AddPO.ItemRef.Text || a.SEARCH_CODE == AddPO.ScrRef.Text select a).FirstOrDefault();


                    }

                }


                if ((Select_BarCode != null && Select_BarCode != "") || (AddPO.ItemRef.Text != null && AddPO.ItemRef.Text != "") || (AddPO.ScrRef.Text != null && AddPO.ScrRef.Text != ""))
                {


                    //var EditItem = (from a in ListGrid1 where a.BAR_CODE == Select_BarCode || a.ITEM_NAME == AddPO.ItemRef.Text || a.SEARCH_CODE == AddPO.ScrRef.Text select a).FirstOrDefault();
                    //var  EditItem = App.Current.Properties["EditItemGrid"] ;
                    if (App.Current.Properties["EditItemGrid"] != null)
                    {
                        var EditItem = (from a in ListGrid1 where a.BAR_CODE == Select_BarCode || a.ITEM_NAME == AddPO.ItemRef.Text || a.SEARCH_CODE == AddPO.ScrRef.Text select a).FirstOrDefault();
                        SelectedItem = EditItem;
                        //SelectedItem = (((IEnumerable)App.Current.Properties["EditItemGrid"]).Cast<POrderModel>()).ToList();
                    }
                    else
                    {


                        var itemToRemove = (from m in _ListGrid_Temp1 where m.BAR_CODE == Select_BarCode || m.ITEM_NAME == AddPO.ItemRef.Text || m.SEARCH_CODE == AddPO.ScrRef.Text select m).ToList();
                        ObservableCollection<POrderModel> myCollection1 = new ObservableCollection<POrderModel>(itemToRemove);
                        var Item = (from a in AddListGrid1 where a.BAR_CODE == Select_BarCode || a.ITEM_NAME == AddPO.ItemRef.Text || a.SEARCH_CODE == AddPO.ScrRef.Text select a).FirstOrDefault();
                        var REMOVEI = (from a in AddListGrid1 where a.BAR_CODE == Select_BarCode || a.ITEM_NAME == AddPO.ItemRef.Text || a.SEARCH_CODE == AddPO.ScrRef.Text select a).FirstOrDefault();



                        foreach (var item in myCollection1)
                        {

                            AddListGrid1.Remove(REMOVEI);

                            x = x + 1;
                            AddListGrid1.Add(new POrderModel
                            {

                                SLNO = x,
                                ITEM_NAME = item.ITEM_NAME,
                                ITEM_ID = item.ITEM_ID,
                                BAR_CODE = item.BAR_CODE,
                                TOTAL_QTY = item.Current_Qty,
                                PURCHASE_UNIT_PRICE = item.PURCHASE_UNIT_PRICE,
                                MRP = item.MRP,
                                SEARCH_CODE = item.SEARCH_CODE,
                                //TAX_COLLECTED = data1[i].TAX_COLLECTED,
                                TAX_PAID = item.TAX_PAID,
                                TaxName = item.TaxName,
                                PURCHASE_PRICE_BEFORE_TAX = ((decimal)(item.PURCHASE_UNIT_PRICE * 100) / ((item.TAX_PAID) + 100)),
                                SUB_TOTAL_AFTER_TAX = 0,

                                SUB_TOTAL_BEFORE_TAX = 0,

                            });


                        }

                        App.Current.Properties["DataGridLPO"] = AddListGrid1;
                        App.Current.Properties["DataGridPO"] = AddListGrid1;

                        ListGrid1 = AddListGrid1;
                        AddPO.ListGridRef.ItemsSource = AddListGrid1;
                        TotalBottom();


                    }

                }
            }

        }

        public void TotalBottom()
        {
            TOTAL_AMOUNT = 0;
            TOTAL_TAX = 0;
            for (int i = 0; i < ListGrid1.Count; i++)
            {
                TOTAL_AMOUNT = Convert.ToDecimal(ListGrid1[i].SUB_TOTAL_AFTER_TAX + TOTAL_AMOUNT);
                App.Current.Properties["CurrentGrosAmount1"] = TOTAL_AMOUNT;
                TOTAL_TAX = Convert.ToDecimal((ListGrid1[i].SUB_TOTAL_AFTER_TAX - ListGrid1[i].SUB_TOTAL_BEFORE_TAX) + TOTAL_TAX);
            }
            TOTAL_TAX = Math.Round(TOTAL_TAX, 2);
            AddPO.TotTaxRef.Text = TOTAL_TAX.ToString();
            AddPO.TotRef.Text = TOTAL_AMOUNT.ToString();
        }

        public async void GetPOList(long POId)
        {
            ReportPO.ListGridRef = new DataGrid();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            response = client.GetAsync("api/POAPI/PurchaseOrdList?id=" + POId + "").Result;
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<POrderModel[]>(await response.Content.ReadAsStringAsync());
                POData = new List<POrderModel>();
                int x = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    x++;
                    _ListGrid_Temp.Add(new POrderModel
                    {
                        SLNO = x,
                        BAR_CODE = data[i].BAR_CODE,
                        BUSINESS_LOCATION = data[i].BUSINESS_LOCATION,
                        BUSINESS_LOCATION_ID = data[i].BUSINESS_LOCATION_ID,
                        COMPANY_ID = data[i].COMPANY_ID,
                        DELIVER = data[i].DELIVER,
                        DELIVER_ID = data[i].DELIVER_ID,
                        DELIVER_TO = data[i].DELIVER_TO,
                        DELIVERY_DATE = data[i].DELIVERY_DATE,
                        IS_SEND_MAIL = data[i].IS_SEND_MAIL,
                        ITEM_NAME = data[i].ITEM_NAME,
                        PO_ID = data[i].PO_ID,
                        PO_NUMBER1 = data[i].PO_NUMBER1,
                        SEARCH_CODE = data[i].SEARCH_CODE,
                        SUPPLIER = data[i].SUPPLIER,
                        SUPPLIER_EMAIL = data[i].SUPPLIER_EMAIL,
                        SUPPLIER_ID = data[i].SUPPLIER_ID,
                        TERMS = data[i].TERMS,
                        TOTAL_AMOUNT = data[i].TOTAL_AMOUNT,
                        TOTAL_TAX = data[i].TOTAL_TAX,
                        PO_DATE = data[i].PO_DATE,
                        PO_STATUS=data[i].PO_STATUS

                    });
                }


                if (SEARCH_PO != "" && SEARCH_PO != null)
                {
                    var item1 = (from m in _ListGrid_Temp where m.PO_NUMBER1.Contains(SEARCH_PO) select m).ToList();

                    _ListGrid_Temp = item1;
                }
                App.Current.Properties["DataGrid1"] = _ListGrid_Temp;
                ReportPO.ListGridRef.ItemsSource = _ListGrid_Temp.ToList();

            }
            ListGrid = _ListGrid_Temp;
        }

        public List<POrderModel> _ListGrid { get; set; }
        public List<POrderModel> ListGrid
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


        private string _CreatVisible { get; set; }
        public string CreatVisible
        {

            get { return _CreatVisible; }
            set
            {
                if (value != _CreatVisible)
                {
                    _CreatVisible = value;
                    OnPropertyChanged("CreatVisible");
                }
            }
        }

        private string _UpdVisible { get; set; }
        public string UpdVisible
        {

            get { return _UpdVisible; }
            set
            {
                if (value != _UpdVisible)
                {
                    _UpdVisible = value;
                    OnPropertyChanged("UpdVisible");
                }
            }
        }

        public POViewModel()
        {

            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());

            if (App.Current.Properties["Action"].ToString() == "Edit")
            {
                CreatVisible = "Collapsed";
                UpdVisible = "Visible";
                SelectedPO = App.Current.Properties["POEdit"] as POrderModel;
                //App.Current.Properties["EditItemGrid"] = (from a in ListGrid1 where a.BAR_CODE == Select_BarCode || a.ITEM_NAME == AddPO.ItemRef.Text || a.SEARCH_CODE == AddPO.ScrRef.Text select a).FirstOrDefault();
                App.Current.Properties["Action"] = "";
                GetPoItem(comp);

            }
            else if (App.Current.Properties["Action"].ToString() == "View")
            {
                SelectedPO = App.Current.Properties["POView"] as POrderModel;
                App.Current.Properties["Action"] = "";
                GetPoItem(comp);
            }
            else
            {
                UpdVisible = "Collapsed";
                CreatVisible = "Visible";
                SelectedPO = new POrderModel();
                PO_DATE = System.DateTime.Now;
                SelectedPO.DELIVERY_DATE = System.DateTime.Now;
                GetPOList(comp);
                App.Current.Properties["Total"] = "null";



            }
            GetSpplierNo();
            isenable = true;
            _isenable = true;
            //ButtonText = "iuhiuh";
        }
        private DateTime _Current_Date;
        public DateTime Current_Date
        {
            get
            {
                return _Current_Date;
            }
            set
            {
                _Current_Date = value;
                OnPropertyChanged("TOTAL_TAX");
            }

        }
        public ICommand _ItemListClick;
        public ICommand ItemListClick
        {
            get
            {
                if (_ItemListClick == null)
                {
                    _ItemListClick = new DelegateCommand(ItemList_Click);
                }
                return _ItemListClick;
            }
        }

        public void ItemList_Click()
        {
            App.Current.Properties["ItemList"] = "1";
            AddPO.ScrRef.Text = "";
            Window_ItemList IA = new Window_ItemList();
            IA.Show();
            //ITEM_NAME = App.Current.Properties["AddPOItem"].ToString();
            //var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            //GetPOList1(comp);

        }

        public ICommand _SearchClick;
        public ICommand SearchClick
        {
            get
            {
                if (_SearchClick == null)
                {
                    _SearchClick = new DelegateCommand(Search_Click);
                }
                return _SearchClick;
            }
        }

        public void Search_Click()
        {
            App.Current.Properties["Search"] = "1";
            AddPO.ItemRef.Text = "";
            Window_ItemList IA = new Window_ItemList();
            IA.Show();
            //ITEM_NAME = App.Current.Properties["AddPOItem"].ToString();
            //var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            //GetPOList1(comp);

        }

        public ICommand _SupplierListClick;
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
            Window_SupplierList IA = new Window_SupplierList();
            IA.Show();

        }
        public ICommand _SupMyCode;

        public ICommand SupMyCode
        {
            get
            {
                if (_SupMyCode == null)
                {
                    _SupMyCode = new DelegateCommand(SupMyCode_Click);
                }
                return _SupMyCode;
            }

        }
        public string _ButtonText;
        public String ButtonText
        {
            get { return _ButtonText ?? (_ButtonText = "Auto Generate"); }
            set
            {
                _ButtonText = value;
                NotifyPropertyChanged("ButtonText");
            }
        }
        public void SupMyCode_Click()
        {
            //Supplier_Enable = false;
            //VisibleMyCode = "Collapsed";
            //VisibleAutoCode = "Visible";

            if (ButtonText == "Auto Generate")
            {
                ButtonText = "Define My Own";
                GetSpplierNo();
                _isviewmode = true;
                IsViewMode = true;

            }
            else if (ButtonText == "Define My Own")
            {
                pomodel_code = "";
                ButtonText = "Auto Generate";
                _isviewmode = false;
                IsViewMode = false;
            }
            SelectedPO.PO_NUMBER1 = pomodel_code;


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
            App.Current.Properties["BusinessLocation"] = "1";
            Window_BusinessLocationList IA = new Window_BusinessLocationList();
            IA.Show();

        }

        public ICommand _DeliveryLocation;
        public ICommand DeliveryLocation
        {
            get
            {
                if (_DeliveryLocation == null)
                {
                    _DeliveryLocation = new DelegateCommand(DeliveryLocation_Click);
                }
                return _DeliveryLocation;
            }
        }

        public void DeliveryLocation_Click()
        {
            App.Current.Properties["DeliveryLocation"] = "1";
            Window_BusinessLocationList IA = new Window_BusinessLocationList();
            IA.Show();

        }

        public ICommand _SelectOk { get; set; }
        public ICommand SelectOk
        {
            get
            {
                if (_SelectOk == null)
                {
                    _SelectOk = new DelegateCommand(Select_Ok);
                }
                return _SelectOk;
            }
        }


        public void Select_Ok()
        {
            if (App.Current.Properties["PoRecItem"] != null)
            {
                ReceiveAddItem.PoReff.Text = null;
                ReceiveAddItem.PoReff.Text = SelectedPO.PO_NUMBER1;
                App.Current.Properties["RecItemPo"] = SelectedPO.PO_NUMBER1;
                App.Current.Properties["RecItemPoId"] = SelectedPO.PO_ID;
                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                ReceiveItemViewModel RV = new ReceiveItemViewModel();
                RV.GetRcvItemList(comp);
                App.Current.Properties["PoRecItem"] = null;
            }
            Cancel_PO();
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

        private Stack<BackNavigationEventHandler> _backFunctions
           = new Stack<BackNavigationEventHandler>();

        void IModalService.NavigateTo(UserControl uc, BackNavigationEventHandler backFromDialog)
        {

        }

        void IModalService.GoBackward(bool dialogReturnValue)
        {
        }

        public bool _isviewmode;
        public bool _isenable;
        public bool IsViewMode
        {
            get { return _isviewmode; }
            set
            {
                _isviewmode = value;
                // Call NotifyPropertyChanged when the source property is updated.
                NotifyPropertyChanged("IsViewMode");
            }
        }
        public bool isenable
        {
            get { return _isenable; }
            set
            {
                _isenable = value;
                // Call NotifyPropertyChanged when the source property is updated.
                NotifyPropertyChanged("isenable");
            }
        }
        public static IModalService ModalService
        {
            get { return (IModalService)Application.Current.MainWindow; }
        }
        public async Task<string> GetSpplierNo()
        {

            string uhy = "";
            try
            {
                string nnnn = "";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/POAPI/GetPoNo/").Result;
                if (response.IsSuccessStatusCode)
                {
                    //hy = Json.SerializeObject<int[]>(await response.Content.ReadAsStringAsync());
                    uhy = await response.Content.ReadAsStringAsync();
                    string dd = Convert.ToString(uhy);
                    string aaa = dd.Substring(3, 5);
                    int xx = Convert.ToInt32(aaa);
                    int noo = Convert.ToInt32(xx + 1);
                    nnnn = "P" + noo.ToString("D6");
                    pomodel_code = nnnn;

                }
                else
                {
                    pomodel_code = "P000001";
                }
            }
            catch (Exception ex)
            { }

            return uhy;
        }
        public string pomodel_code
        {
            get
            {
                return SelectedPO.PO_NUMBER1;
            }
            set
            {
                SelectedPO.PO_NUMBER1 = value;
                OnPropertyChanged("pomodel_code");

            }
        }
        private POList _selectedpo;
        public POList selectedpo
        {
            get { return _selectedpo; }
            set
            {
                if (_selectedpo != value)
                {
                    _selectedpo = value;
                    OnPropertyChanged("selectedpo");
                }

            }
        }
    }
}