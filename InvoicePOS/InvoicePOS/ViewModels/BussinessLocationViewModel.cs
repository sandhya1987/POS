using InvoicePOS.Helpers;
using InvoicePOS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Runtime.Caching;
using System.Windows.Media;
using System.Web;
using System.Drawing;
using System.Collections;
using InvoicePOS.ViewModels.Company;
using InvoicePOS.Helper;
using InvoicePOS.Views.Main;
using InvoicePOS.ViewModels.WelCome;
using InvoicePOS.Views.WelCome;
using System.Net.Http.Formatting;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Imaging;
using InvoicePOS.UserControll.Company;
using InvoicePOS.UserControll.Item;
using InvoicePOS.Views.Customer;
using InvoicePOS.UserControll.PO;
using InvoicePOS.UserControll.StockTransfer;
using InvoicePOS.UserControll.SuppPayment;
using InvoicePOS.UserControll.SalesReturn;
using InvoicePOS.UserControll.CashReg;
using InvoicePOS.UserControll.Payment;
using InvoicePOS.UserControll.DailySales;
using InvoicePOS.UserControll.Employee;
using InvoicePOS.UserControll.StockLedger;
using InvoicePOS.Views.CashRegister;

namespace InvoicePOS.ViewModels
{
    public class BussinessLocationViewModel : BaseViewModel, INotifyPropertyChanged, IModalService
    {

        public HttpResponseMessage response;
        // private readonly BusinessLocationModel OprModel;
        // private Dictionary<string, bool> validProperties;
        BusinessLocationModel _BusinessLocationModel = new BusinessLocationModel();
        public ICommand select { get; set; }
        BusinessLocationModel[] data = null;

        List<BusinessLocationModel> _ListGrid_Temp = new List<BusinessLocationModel>();

        private BusinessLocationModel _SelectedBusinessLoca;
        public BusinessLocationModel SelectedBusinessLoca
        {
            get { return _SelectedBusinessLoca; }
            set
            {
                if (_SelectedBusinessLoca != value)
                {
                    _SelectedBusinessLoca = value;
                    App.Current.Properties["LOC_ID"] = SelectedBusinessLoca.BUSINESS_LOCATION_ID;
                    App.Current.Properties["BUSSINESS_LOC"] = SelectedBusinessLoca.BUSINESS_LOCATION;
                    App.Current.Properties["BussLocList"] = SelectedBusinessLoca.BUSINESS_LOCATION;
                    App.Current.Properties["BussLocName"] = SelectedBusinessLoca.BUSS_ADDRESS_1;
                    OnPropertyChanged("_SelectedBusinessLoca");
                }

            }
        }
        private string _COMPANY;
        public string COMPANY
        {
            get
            {
                return SelectedBusinessLoca.COMPANY;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new Exception("Name can not be empty.");
                }
                if (SelectedBusinessLoca.COMPANY != value)
                {
                    SelectedBusinessLoca.COMPANY = value;
                    OnPropertyChanged("COMPANY");

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
        private string _BUSINESS_LOCATION;
        public string BUSINESS_LOCATION
        {
            get
            {
                return SelectedBusinessLoca.BUSINESS_LOCATION;
            }
            set
            {

                if (SelectedBusinessLoca.BUSINESS_LOCATION != value)
                {
                    SelectedBusinessLoca.BUSINESS_LOCATION = value;
                    OnPropertyChanged("BUSINESS_LOCATION");
                }
            }
        }

        private string _SHOP_NAME;
        public string SHOP_NAME
        {
            get
            {
                return SelectedBusinessLoca.SHOP_NAME;
            }
            set
            {
                SelectedBusinessLoca.SHOP_NAME = value;
                if (string.IsNullOrEmpty(value))
                {
                    //ValidationError
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedBusinessLoca.SHOP_NAME != value)
                {
                    SelectedBusinessLoca.SHOP_NAME = value;
                    OnPropertyChanged("SHOP_NAME");
                }
            }
        }
        private string _PREFIX_DOCUMENT;
        public string PREFIX_DOCUMENT
        {
            get
            {
                return SelectedBusinessLoca.PREFIX_DOCUMENT;
            }
            set
            {
                SelectedBusinessLoca.PREFIX_DOCUMENT = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedBusinessLoca.PREFIX_DOCUMENT != value)
                {
                    SelectedBusinessLoca.PREFIX_DOCUMENT = value;
                    OnPropertyChanged("PREFIX_DOCUMENT");
                }
            }
        }
        private string _DOCUMENT_NO;
        public string DOCUMENT_NO
        {
            get
            {
                return SelectedBusinessLoca.DOCUMENT_NO;
            }
            set
            {
                SelectedBusinessLoca.DOCUMENT_NO = value;

                if (SelectedBusinessLoca.DOCUMENT_NO != value)
                {
                    SelectedBusinessLoca.DOCUMENT_NO = value;
                    OnPropertyChanged("DOCUMENT_NO");
                }
            }
        }

        private string _TIN_NUMBER;
        public string TIN_NUMBER
        {
            get
            {
                return SelectedBusinessLoca.TIN_NUMBER;
            }
            set
            {
                SelectedBusinessLoca.TIN_NUMBER = value;

                if (SelectedBusinessLoca.TIN_NUMBER != value)
                {
                    SelectedBusinessLoca.TIN_NUMBER = value;
                    OnPropertyChanged("TIN_NUMBER");
                }
            }
        }
        private string _BUSS_ADDRESS_1;
        public string BUSS_ADDRESS_1
        {
            get
            {
                return SelectedBusinessLoca.BUSS_ADDRESS_1;
            }
            set
            {
                SelectedBusinessLoca.BUSS_ADDRESS_1 = value;

                if (SelectedBusinessLoca.BUSS_ADDRESS_1 != value)
                {
                    SelectedBusinessLoca.BUSS_ADDRESS_1 = value;
                    OnPropertyChanged("BUSS_ADDRESS_1");
                }
            }
        }
        private string _BUSS_ADDRESS_2;
        public string BUSS_ADDRESS_2
        {
            get
            {
                return SelectedBusinessLoca.BUSS_ADDRESS_2;
            }
            set
            {
                SelectedBusinessLoca.BUSS_ADDRESS_2 = value;
                if (SelectedBusinessLoca.BUSS_ADDRESS_2 != value)
                {
                    SelectedBusinessLoca.BUSS_ADDRESS_2 = value;
                    OnPropertyChanged("BUSS_ADDRESS_2");
                }
            }
        }

        private string _CITY;
        public string CITY
        {
            get
            {
                return SelectedBusinessLoca.CITY;
            }
            set
            {
                SelectedBusinessLoca.CITY = value;

                if (SelectedBusinessLoca.CITY != value)
                {
                    SelectedBusinessLoca.CITY = value;
                    OnPropertyChanged("CITY");
                }
            }
        }
        private string _STATE;
        public string STATE
        {
            get
            {
                return SelectedBusinessLoca.STATE;
            }
            set
            {
                SelectedBusinessLoca.STATE = value;

                if (SelectedBusinessLoca.STATE != value)
                {
                    SelectedBusinessLoca.STATE = value;
                    OnPropertyChanged("STATE");
                }
            }
        }

        private string _COUNTRY;
        public string COUNTRY
        {
            get
            {
                return SelectedBusinessLoca.COUNTRY;
            }
            set
            {
                SelectedBusinessLoca.COUNTRY = value;

                if (SelectedBusinessLoca.COUNTRY != value)
                {
                    SelectedBusinessLoca.COUNTRY = value;
                    OnPropertyChanged("COUNTRY");
                }
            }
        }
        private string _PIN;
        public string PIN
        {
            get
            {
                return SelectedBusinessLoca.PIN;
            }
            set
            {
                SelectedBusinessLoca.PIN = value;

                if (SelectedBusinessLoca.PIN != value)
                {
                    SelectedBusinessLoca.PIN = value;
                    OnPropertyChanged("PIN");
                }
            }
        }


        private string _PHONE_NO;
        public string PHONE_NO
        {
            get
            {
                return SelectedBusinessLoca.PHONE_NO;
            }
            set
            {
                SelectedBusinessLoca.PHONE_NO = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedBusinessLoca.PHONE_NO != value)
                {
                    SelectedBusinessLoca.PHONE_NO = value;
                    OnPropertyChanged("PHONE_NO");
                }
            }
        }
        private string _EMAIL;
        public string EMAIL
        {
            get
            {
                return SelectedBusinessLoca.EMAIL;
            }
            set
            {
                SelectedBusinessLoca.EMAIL = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedBusinessLoca.EMAIL != value)
                {
                    SelectedBusinessLoca.EMAIL = value;
                    OnPropertyChanged("EMAIL");
                }
            }
        }
        private string _WEBSITE;
        public string WEBSITE
        {
            get
            {
                return SelectedBusinessLoca.WEBSITE;
            }
            set
            {
                SelectedBusinessLoca.WEBSITE = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedBusinessLoca.WEBSITE != value)
                {
                    SelectedBusinessLoca.WEBSITE = value;
                    OnPropertyChanged("WEBSITE");
                }
            }
        }


        private string _SMS_SETTING;
        public string SMS_SETTING
        {
            get
            {
                return SelectedBusinessLoca.SMS_SETTING;
            }
            set
            {
                SelectedBusinessLoca.SMS_SETTING = value;


                if (SelectedBusinessLoca.SMS_SETTING != value)
                {
                    SelectedBusinessLoca.SMS_SETTING = value;
                    OnPropertyChanged("SMS_SETTING");
                }
            }
        }
        private string _EMAIL_SETTING;
        public string EMAIL_SETTING
        {
            get
            {
                return SelectedBusinessLoca.EMAIL_SETTING;
            }
            set
            {
                SelectedBusinessLoca.EMAIL_SETTING = value;


                if (SelectedBusinessLoca.EMAIL_SETTING != value)
                {
                    SelectedBusinessLoca.EMAIL_SETTING = value;
                    OnPropertyChanged("EMAIL_SETTING");
                }
            }
        }

        private string _EMAIL_TEMPLATE_SETTING;
        public string EMAIL_TEMPLATE_SETTING
        {
            get
            {
                return SelectedBusinessLoca.EMAIL_TEMPLATE_SETTING;
            }
            set
            {
                SelectedBusinessLoca.EMAIL_TEMPLATE_SETTING = value;


                if (SelectedBusinessLoca.EMAIL_TEMPLATE_SETTING != value)
                {
                    SelectedBusinessLoca.EMAIL_TEMPLATE_SETTING = value;
                    OnPropertyChanged("EMAIL_TEMPLATE_SETTING");
                }
            }
        }
        private string _SCEOND_RECEIPT_PRINTER;
        public string SCEOND_RECEIPT_PRINTER
        {
            get
            {
                return SelectedBusinessLoca.SCEOND_RECEIPT_PRINTER;
            }
            set
            {
                SelectedBusinessLoca.SCEOND_RECEIPT_PRINTER = value;
                if (SelectedBusinessLoca.SCEOND_RECEIPT_PRINTER != value)
                {
                    SelectedBusinessLoca.SCEOND_RECEIPT_PRINTER = value;
                    OnPropertyChanged("SCEOND_RECEIPT_PRINTER");
                }
            }
        }
        private string _SCEOND_RECEIPT_PRINT_FORMATE;
        public string SCEOND_RECEIPT_PRINT_FORMATE
        {
            get
            {
                return SelectedBusinessLoca.SCEOND_RECEIPT_PRINT_FORMATE;
            }
            set
            {
                SelectedBusinessLoca.SCEOND_RECEIPT_PRINT_FORMATE = value;
                if (SelectedBusinessLoca.SCEOND_RECEIPT_PRINT_FORMATE != value)
                {
                    SelectedBusinessLoca.SCEOND_RECEIPT_PRINT_FORMATE = value;
                    OnPropertyChanged("SCEOND_RECEIPT_PRINT_FORMATE");
                }
            }
        }

        private string _NUMBER_OF_SECOND_RECEIPT_PRINT;
        public string NUMBER_OF_SECOND_RECEIPT_PRINT
        {
            get
            {
                return SelectedBusinessLoca.NUMBER_OF_SECOND_RECEIPT_PRINT;
            }
            set
            {
                SelectedBusinessLoca.NUMBER_OF_SECOND_RECEIPT_PRINT = value;
                if (SelectedBusinessLoca.NUMBER_OF_SECOND_RECEIPT_PRINT != value)
                {
                    SelectedBusinessLoca.NUMBER_OF_SECOND_RECEIPT_PRINT = value;
                    OnPropertyChanged("NUMBER_OF_SECOND_RECEIPT_PRINT");
                }
            }
        }
        private string _GODOWN_TO_KEEP;
        public string GODOWN_TO_KEEP
        {
            get
            {
                return SelectedBusinessLoca.GODOWN_TO_KEEP;
            }
            set
            {
                SelectedBusinessLoca.GODOWN_TO_KEEP = value;
                if (SelectedBusinessLoca.GODOWN_TO_KEEP != value)
                {
                    SelectedBusinessLoca.GODOWN_TO_KEEP = value;
                    OnPropertyChanged("GODOWN_TO_KEEP");
                }
            }
        }
        private bool _IS_ENABLE_EMAIL;
        public bool IS_ENABLE_EMAIL
        {
            get
            {
                return SelectedBusinessLoca.IS_ENABLE_EMAIL;
            }
            set
            {
                SelectedBusinessLoca.IS_ENABLE_EMAIL = value;
                if (SelectedBusinessLoca.IS_ENABLE_EMAIL != value)
                {
                    SelectedBusinessLoca.IS_ENABLE_EMAIL = value;
                    OnPropertyChanged("IS_ENABLE_EMAIL");
                }
            }
        }
        private bool _IS_SCEOND_RECEIPT_PRINTER;
        public bool IS_SCEOND_RECEIPT_PRINTER
        {
            get
            {
                return SelectedBusinessLoca.IS_SCEOND_RECEIPT_PRINTER;
            }
            set
            {
                SelectedBusinessLoca.IS_SCEOND_RECEIPT_PRINTER = value;
                if (SelectedBusinessLoca.IS_SCEOND_RECEIPT_PRINTER != value)
                {
                    SelectedBusinessLoca.IS_SCEOND_RECEIPT_PRINTER = value;
                    OnPropertyChanged("IS_SCEOND_RECEIPT_PRINTER");
                }
            }
        }
        private bool _IS_SECOND_DIFF_PRINT;
        public bool IS_SECOND_DIFF_PRINT
        {
            get
            {
                return SelectedBusinessLoca.IS_SECOND_DIFF_PRINT;
            }
            set
            {
                SelectedBusinessLoca.IS_SECOND_DIFF_PRINT = value;
                if (SelectedBusinessLoca.IS_SECOND_DIFF_PRINT != value)
                {
                    SelectedBusinessLoca.IS_SECOND_DIFF_PRINT = value;
                    OnPropertyChanged("IS_SECOND_DIFF_PRINT");
                }
            }
        }
        private bool _IS_ASK_TO_PRIENT_EVERYTIME;
        public bool IS_ASK_TO_PRIENT_EVERYTIME
        {
            get
            {
                return SelectedBusinessLoca.IS_ASK_TO_PRIENT_EVERYTIME;
            }
            set
            {
                SelectedBusinessLoca.IS_ASK_TO_PRIENT_EVERYTIME = value;
                if (SelectedBusinessLoca.IS_ASK_TO_PRIENT_EVERYTIME != value)
                {
                    SelectedBusinessLoca.IS_ASK_TO_PRIENT_EVERYTIME = value;
                    OnPropertyChanged("IS_ASK_TO_PRIENT_EVERYTIME");
                }
            }
        }
        private bool _IS_DELETE_INVOICE_SPECIFIED_GODOWN;
        public bool IS_DELETE_INVOICE_SPECIFIED_GODOWN
        {
            get
            {
                return SelectedBusinessLoca.IS_DELETE_INVOICE_SPECIFIED_GODOWN;
            }
            set
            {
                SelectedBusinessLoca.IS_DELETE_INVOICE_SPECIFIED_GODOWN = value;
                if (SelectedBusinessLoca.IS_DELETE_INVOICE_SPECIFIED_GODOWN != value)
                {
                    SelectedBusinessLoca.IS_DELETE_INVOICE_SPECIFIED_GODOWN = value;
                    OnPropertyChanged("IS_DELETE_INVOICE_SPECIFIED_GODOWN");
                }
            }
        }
        private string _IMAGE;
        public string IMAGE
        {
            get
            {
                return SelectedBusinessLoca.IMAGE;
            }
            set
            {
                SelectedBusinessLoca.IMAGE = value;
                if (SelectedBusinessLoca.IMAGE != value)
                {
                    SelectedBusinessLoca.IMAGE = value;
                    OnPropertyChanged("IMAGE");
                }
            }
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
                SelectedBusinessLoca.BUSINESS_LOCATION_ID = value;
                if (SelectedBusinessLoca.BUSINESS_LOCATION_ID != value)
                {
                    SelectedBusinessLoca.BUSINESS_LOCATION_ID = value;
                    OnPropertyChanged("BUSINESS_LOCATION_ID");
                }
            }
        }




        public ICommand _ImgLoad { get; set; }
        public ICommand ImgLoad
        {
            get
            {
                if (_ImgLoad == null)
                {
                    _ImgLoad = new DelegateCommand(Img_Load);
                }
                return _ImgLoad;
            }
        }

        public void Img_Load()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                //foreach (string filename in openFileDialog.FileNames)
                //    IMAGE_PATH = Path.GetFileName(filename);
            }
            if (File.Exists(openFileDialog.FileName))
            {
                IMAGE_PATH1 = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));

            }

        }
        private ImageSource _IMAGE_PATH1;
        public ImageSource IMAGE_PATH1
        {
            get { return _IMAGE_PATH1; }
            set
            {
                if (Equals(value, _IMAGE_PATH1)) return;
                _IMAGE_PATH1 = value;
                OnPropertyChanged("IMAGE_PATH1");
            }
        }
        public ICommand _RemovedImg { get; set; }
        public ICommand RemovedImg
        {
            get
            {
                if (_RemovedImg == null)
                {
                    _RemovedImg = new DelegateCommand(Removed_Img);
                }
                return _RemovedImg;
            }
        }

        public void Removed_Img()
        {
            IMAGE_PATH1 = null;
        }
        public ImageSource GetImageSource(string filename)
        {
            string _fileName = filename;

            BitmapImage glowIcon = new BitmapImage();

            glowIcon.BeginInit();
            glowIcon.UriSource = new Uri(_fileName);
            glowIcon.EndInit();

            return glowIcon;
        }
        public ICommand _EditBussLoc { get; set; }
        public ICommand EditBussLoc
        {
            get
            {
                if (_EditBussLoc == null)
                {
                    _EditBussLoc = new DelegateCommand(Edit_BussLoc);
                }
                return _EditBussLoc;
            }
        }
        public async void Edit_BussLoc()
        {
            if (SelectedBusinessLoca.BUSINESS_LOCATION_ID != null && SelectedBusinessLoca.BUSINESS_LOCATION_ID != 0)
            {
                App.Current.Properties["Action"] = "Edit";
                BussLocadata = new List<BussinessLocationList>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/BussLocationAPI/GetEditBussLocation?id=" + SelectedBusinessLoca.BUSINESS_LOCATION_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    var objResponse1 = JsonConvert.DeserializeObject<List<BusinessLocationModel>>(await response.Content.ReadAsStringAsync());

                    // data = JsonConvert.DeserializeObject<BusinessLocationModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedBusinessLoca.BUSINESS_LOCATION_ID = data[i].BUSINESS_LOCATION_ID;
                            SelectedBusinessLoca.BUSS_ADDRESS_1 = data[i].BUSS_ADDRESS_1;
                            SelectedBusinessLoca.BUSS_ADDRESS_2 = data[i].BUSS_ADDRESS_2;
                            SelectedBusinessLoca.CITY = data[i].CITY;
                            SelectedBusinessLoca.COMPANY = data[i].COMPANY;
                            SelectedBusinessLoca.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedBusinessLoca.COUNTRY = data[i].COUNTRY;
                            SelectedBusinessLoca.DOCUMENT_NO = data[i].DOCUMENT_NO;
                            SelectedBusinessLoca.EMAIL = data[i].EMAIL;
                            SelectedBusinessLoca.EMAIL_SETTING = data[i].EMAIL_SETTING;
                            SelectedBusinessLoca.EMAIL_TEMPLATE_SETTING = data[i].EMAIL_TEMPLATE_SETTING;
                            SelectedBusinessLoca.GODOWN_TO_KEEP = data[i].GODOWN_TO_KEEP;
                            SelectedBusinessLoca.IMAGE = data[i].IMAGE;
                            SelectedBusinessLoca.IS_ASK_TO_PRIENT_EVERYTIME = data[i].IS_ASK_TO_PRIENT_EVERYTIME;
                            SelectedBusinessLoca.IS_DELETE_INVOICE_SPECIFIED_GODOWN = data[i].IS_DELETE_INVOICE_SPECIFIED_GODOWN;
                            SelectedBusinessLoca.IS_ENABLE_EMAIL = data[i].IS_ENABLE_EMAIL;
                            SelectedBusinessLoca.IS_SCEOND_RECEIPT_PRINTER = data[i].IS_SCEOND_RECEIPT_PRINTER;
                            SelectedBusinessLoca.IS_SECOND_DIFF_PRINT = data[i].IS_SECOND_DIFF_PRINT;
                            SelectedBusinessLoca.MOBILE_NO = data[i].MOBILE_NO;
                            SelectedBusinessLoca.NUMBER_OF_SECOND_RECEIPT_PRINT = data[i].NUMBER_OF_SECOND_RECEIPT_PRINT;
                            SelectedBusinessLoca.PHONE_NO = data[i].PHONE_NO;
                            SelectedBusinessLoca.PIN = data[i].PIN;
                            SelectedBusinessLoca.PREFIX_DOCUMENT = data[i].PREFIX_DOCUMENT;
                            SelectedBusinessLoca.PRINTER_SETTING = data[i].PRINTER_SETTING;
                            SelectedBusinessLoca.SCEOND_RECEIPT_PRINT_FORMATE = data[i].SCEOND_RECEIPT_PRINT_FORMATE;
                            SelectedBusinessLoca.SCEOND_RECEIPT_PRINTER = data[i].SCEOND_RECEIPT_PRINTER;
                            SelectedBusinessLoca.SHOP_NAME = data[i].SHOP_NAME;
                            SelectedBusinessLoca.SMS_SETTING = data[i].SMS_SETTING;
                            SelectedBusinessLoca.STATE = data[i].STATE;
                            SelectedBusinessLoca.TIN_NUMBER = data[i].TIN_NUMBER;
                            SelectedBusinessLoca.WEBSITE = data[i].WEBSITE;
                        }

                        App.Current.Properties["BussLocEdit"] = SelectedBusinessLoca;
                        BusinessLocationAdd BLA = new BusinessLocationAdd();
                        BLA.Show();
                        //CreatVisible="Visible";
                        //UpdateVisible = "Collapsed";
                        //Close();
                        // ModalService.NavigateTo(new BusinessLocationAdd(), delegate(bool returnValue) { });
                    }
                }

            }
            else
            {

                MessageBox.Show("Select BusinessLocation first", "BusinessLocation Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }

        public ICommand _ViewBussLoc { get; set; }
        public ICommand ViewBussLoc
        {
            get
            {
                if (_ViewBussLoc == null)
                {
                    _ViewBussLoc = new DelegateCommand(View_BussLoc);
                }
                return _ViewBussLoc;
            }
        }
        public async void View_BussLoc()
        {
            if (SelectedBusinessLoca.BUSINESS_LOCATION_ID != null && SelectedBusinessLoca.BUSINESS_LOCATION_ID != 0)
            {
                App.Current.Properties["Action"] = "View";
                BussLocadata = new List<BussinessLocationList>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/BussLocationAPI/GetEditBussLocation?id=" + SelectedBusinessLoca.BUSINESS_LOCATION_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    var objResponse1 = JsonConvert.DeserializeObject<List<BusinessLocationModel>>(await response.Content.ReadAsStringAsync());

                    // data = JsonConvert.DeserializeObject<BusinessLocationModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedBusinessLoca.BUSINESS_LOCATION_ID = data[i].BUSINESS_LOCATION_ID;
                            SelectedBusinessLoca.BUSS_ADDRESS_1 = data[i].BUSS_ADDRESS_1;
                            SelectedBusinessLoca.BUSS_ADDRESS_2 = data[i].BUSS_ADDRESS_2;
                            SelectedBusinessLoca.CITY = data[i].CITY;
                            SelectedBusinessLoca.COMPANY = data[i].COMPANY;
                            SelectedBusinessLoca.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedBusinessLoca.COUNTRY = data[i].COUNTRY;
                            SelectedBusinessLoca.DOCUMENT_NO = data[i].DOCUMENT_NO;
                            SelectedBusinessLoca.EMAIL = data[i].EMAIL;
                            SelectedBusinessLoca.EMAIL_SETTING = data[i].EMAIL_SETTING;
                            SelectedBusinessLoca.EMAIL_TEMPLATE_SETTING = data[i].EMAIL_TEMPLATE_SETTING;
                            SelectedBusinessLoca.GODOWN_TO_KEEP = data[i].GODOWN_TO_KEEP;
                            SelectedBusinessLoca.IMAGE = data[i].IMAGE;
                            SelectedBusinessLoca.IS_ASK_TO_PRIENT_EVERYTIME = data[i].IS_ASK_TO_PRIENT_EVERYTIME;
                            SelectedBusinessLoca.IS_DELETE_INVOICE_SPECIFIED_GODOWN = data[i].IS_DELETE_INVOICE_SPECIFIED_GODOWN;
                            SelectedBusinessLoca.IS_ENABLE_EMAIL = data[i].IS_ENABLE_EMAIL;
                            SelectedBusinessLoca.IS_SCEOND_RECEIPT_PRINTER = data[i].IS_SCEOND_RECEIPT_PRINTER;
                            SelectedBusinessLoca.IS_SECOND_DIFF_PRINT = data[i].IS_SECOND_DIFF_PRINT;
                            SelectedBusinessLoca.MOBILE_NO = data[i].MOBILE_NO;
                            SelectedBusinessLoca.NUMBER_OF_SECOND_RECEIPT_PRINT = data[i].NUMBER_OF_SECOND_RECEIPT_PRINT;
                            SelectedBusinessLoca.PHONE_NO = data[i].PHONE_NO;
                            SelectedBusinessLoca.PIN = data[i].PIN;
                            SelectedBusinessLoca.PREFIX_DOCUMENT = data[i].PREFIX_DOCUMENT;
                            SelectedBusinessLoca.PRINTER_SETTING = data[i].PRINTER_SETTING;
                            SelectedBusinessLoca.SCEOND_RECEIPT_PRINT_FORMATE = data[i].SCEOND_RECEIPT_PRINT_FORMATE;
                            SelectedBusinessLoca.SCEOND_RECEIPT_PRINTER = data[i].SCEOND_RECEIPT_PRINTER;
                            SelectedBusinessLoca.SHOP_NAME = data[i].SHOP_NAME;
                            SelectedBusinessLoca.SMS_SETTING = data[i].SMS_SETTING;
                            SelectedBusinessLoca.STATE = data[i].STATE;
                            SelectedBusinessLoca.TIN_NUMBER = data[i].TIN_NUMBER;
                            SelectedBusinessLoca.WEBSITE = data[i].WEBSITE;
                        }
                        App.Current.Properties["BussLocView"] = SelectedBusinessLoca;
                        BusinessLocationView BLA = new BusinessLocationView();
                        BLA.Show();
                    }
                }

            }
            else
            {
                MessageBox.Show("Select BusinessLocation first", "BusinessLocation Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private string _CreateVisible { get; set; }
        public string CreateVisible
        {

            get { return _CreateVisible; }
            set
            {
                if (value != _CreateVisible)
                {
                    _CreateVisible = value;
                    OnPropertyChanged("CreateVisible");
                }
            }
        }
        private string _UpdateVisible { get; set; }
        public string UpdateVisible
        {

            get { return _UpdateVisible; }
            set
            {
                if (value != _UpdateVisible)
                {
                    _UpdateVisible = value;
                    OnPropertyChanged("UpdateVisible");
                }
            }
        }

        public ICommand _InsertBusinessLoca { get; set; }
        public ICommand InsertBusinessLoca
        {
            get
            {
                if (_InsertBusinessLoca == null)
                {

                    _InsertBusinessLoca = new DelegateCommand(Add_bussLoc);
                }
                return _InsertBusinessLoca;
            }
        }

        public async void Add_bussLoc()
        {
            if (SelectedBusinessLoca.COMPANY == "" || SelectedBusinessLoca.COMPANY == null)
            {
                MessageBox.Show("Company Name is missing");
            }
            else if (SelectedBusinessLoca.SHOP_NAME == "" || SelectedBusinessLoca.SHOP_NAME == null)
            {
                MessageBox.Show("Shop Name is missing");
            }
            else if (SelectedBusinessLoca.TIN_NUMBER == "" || SelectedBusinessLoca.TIN_NUMBER == null)
            {
                MessageBox.Show("Tin Number is missing");

            }
            else if (SelectedBusinessLoca.BUSS_ADDRESS_1 == "" || SelectedBusinessLoca.BUSS_ADDRESS_1 == null)
            {

                MessageBox.Show("BUSSINESS ADDRESS 1 is missing");
            }
            //else if (SelectedBusinessLoca.BUSS_ADDRESS_2 == "" || SelectedBusinessLoca.BUSS_ADDRESS_2 == null)
            //{
            //    MessageBox.Show("BUSS_ADDRESS_2 Does Not Exist");
            //}
            else if (SelectedBusinessLoca.PIN == "" || SelectedBusinessLoca.PIN == null)
            {
                MessageBox.Show("BusinessLoca.PIN is missing");
            }
            //else if (SelectedBusinessLoca.PHONE_NO == "" || SelectedBusinessLoca.PHONE_NO == null)
            //{
            //    MessageBox.Show("PHONE_NO Does Not Exist");
            //}
            //else if (SelectedBusinessLoca.EMAIL == "" || SelectedBusinessLoca.EMAIL == null)
            //{
            //    MessageBox.Show("EMAIL Does Not Exist");
            //}
            else if (SelectedBusinessLoca.PRINTER_SETTING == "" || SelectedBusinessLoca.PRINTER_SETTING == null)
            {
                MessageBox.Show("PRINTER SETTING is missing");
            }
            else if (SelectedBusinessLoca.GODOWN_TO_KEEP == "" || SelectedBusinessLoca.GODOWN_TO_KEEP == null)
            {
                MessageBox.Show("GODOWN TO KEEP is missing");
            }
            else
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                SelectedBusinessLoca.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                var response = await client.PostAsJsonAsync("api/BussLocationAPI/BussLocationAdd/", SelectedBusinessLoca);
                if (response.StatusCode.ToString() == "OK")
                {
                    App.Current.Properties["Action"] = "";
                    MessageBox.Show("Business Location insert Successfully");


                    //Window_BusinessLocationList.GridRef.ItemsSource = null;
                    // Window_BusinessLocationList.GridRef.ItemsSource = _ListGrid_Temp.ToList();
                    Cancel_BusinessLoca();
                    ModalService.NavigateTo(new BussinessLocationList(), delegate (bool returnValue) { });
                }
            }
        }

        private List<BussinessLocationList> _BussLocadata;
        public List<BussinessLocationList> BussLocadata
        {
            get { return _BussLocadata; }
            set
            {
                if (_BussLocadata != value)
                {
                    _BussLocadata = value;
                }
            }
        }




        List<AutoBussinessModel> bussList = new List<AutoBussinessModel>();
        List<BusinessLocationModel> _ListGrid_Busniss = new List<BusinessLocationModel>();
        public async Task<ObservableCollection<BusinessLocationModel>> GetBussList(long copm)
        {
            try
            {
                bussList = new List<AutoBussinessModel>();
                _ListGrid_Busniss = new List<BusinessLocationModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/BussLocationAPI/GetAllBusinessLo?id=" + copm + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<BusinessLocationModel[]>(await response.Content.ReadAsStringAsync());
                    for (int i = 0; i < data.Length; i++)
                    {
                        _ListGrid_Busniss.Add(new BusinessLocationModel
                        {
                            BUSINESS_LOCATION_ID = data[i].BUSINESS_LOCATION_ID,
                            COMPANY = data[i].COMPANY,
                        });
                        bussList.Add(new AutoBussinessModel
                        {
                            DisplayName = data[i].SHOP_NAME,
                            DisplayId = data[i].BUSINESS_LOCATION_ID
                        });


                    }
                    ListGrid = _ListGrid_Busniss;


                    App.Current.Properties["BussLocList"] = bussList;

                }
                return new ObservableCollection<BusinessLocationModel>(_ListGrid_Busniss);
            }
            catch (Exception ex)
            {
                throw;
            }
        }




        public async Task<ObservableCollection<BusinessLocationModel>> GetBussinessList(long copm)
        {
            try
            {
                bussList = new List<AutoBussinessModel>();
                //BussLocadata = new List<BusinessLocationModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/BussLocationAPI/GetAllBusinessLo?id=" + copm + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<BusinessLocationModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {

                        x++;
                        _ListGrid_Temp.Add(new BusinessLocationModel
                        {
                            SLNO = x,
                            BUSINESS_LOCATION_ID = data[i].BUSINESS_LOCATION_ID,
                            BUSS_ADDRESS_1 = data[i].BUSS_ADDRESS_1,
                            BUSS_ADDRESS_2 = data[i].BUSS_ADDRESS_2,
                            CITY = data[i].CITY,
                            COMPANY = data[i].COMPANY,
                            COMPANY_ID = data[i].COMPANY_ID,
                            COUNTRY = data[i].COUNTRY,
                            DOCUMENT_NO = data[i].DOCUMENT_NO,
                            EMAIL = data[i].EMAIL,
                            EMAIL_SETTING = data[i].EMAIL_SETTING,
                            EMAIL_TEMPLATE_SETTING = data[i].EMAIL_TEMPLATE_SETTING,
                            GODOWN_TO_KEEP = data[i].GODOWN_TO_KEEP,
                            IMAGE = data[i].IMAGE,
                            IS_ASK_TO_PRIENT_EVERYTIME = data[i].IS_ASK_TO_PRIENT_EVERYTIME,
                            IS_DELETE_INVOICE_SPECIFIED_GODOWN = data[i].IS_DELETE_INVOICE_SPECIFIED_GODOWN,
                            IS_ENABLE_EMAIL = data[i].IS_ENABLE_EMAIL,
                            IS_SCEOND_RECEIPT_PRINTER = data[i].IS_SCEOND_RECEIPT_PRINTER,
                            IS_SECOND_DIFF_PRINT = data[i].IS_SECOND_DIFF_PRINT,
                            MOBILE_NO = data[i].MOBILE_NO,
                            NUMBER_OF_SECOND_RECEIPT_PRINT = data[i].NUMBER_OF_SECOND_RECEIPT_PRINT,
                            PHONE_NO = data[i].PHONE_NO,
                            PIN = data[i].PIN,
                            PREFIX_DOCUMENT = data[i].PREFIX_DOCUMENT,
                            PRINTER_SETTING = data[i].PRINTER_SETTING,
                            SCEOND_RECEIPT_PRINT_FORMATE = data[i].SCEOND_RECEIPT_PRINT_FORMATE,
                            SCEOND_RECEIPT_PRINTER = data[i].SCEOND_RECEIPT_PRINTER,
                            SHOP_NAME = data[i].SHOP_NAME,
                            SMS_SETTING = data[i].SMS_SETTING,
                            STATE = data[i].STATE,
                            TIN_NUMBER = data[i].TIN_NUMBER,
                            WEBSITE = data[i].WEBSITE,
                        });
                        bussList.Add(new AutoBussinessModel
                        {
                            DisplayName = data[i].SHOP_NAME,
                            DisplayId = data[i].BUSINESS_LOCATION_ID
                        });
                    }
                    if (SEARCH_BUSS != "" && SEARCH_BUSS != null)
                    {
                        var item1 = (from m in _ListGrid_Temp where m.SHOP_NAME.Contains(SEARCH_BUSS) select m).ToList();
                        _ListGrid_Temp = item1;
                    }
                }
                App.Current.Properties["BussLocList"] = bussList;
                ListGrid = _ListGrid_Temp;
                return new ObservableCollection<BusinessLocationModel>(_ListGrid_Temp);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private string _SEARCH_BUSS;
        public string SEARCH_BUSS
        {
            get
            {
                return _SEARCH_BUSS;
            }
            set
            {


                if (_SEARCH_BUSS != value)
                {
                    _SEARCH_BUSS = value;

                    if (_SEARCH_BUSS != "" && _SEARCH_BUSS != null)
                    {

                        GetBussinessList(COMPANY_ID);
                    }
                    else
                    {
                        GetBussinessList(COMPANY_ID);

                    }
                    OnPropertyChanged("SEARCH_BUSS");
                }
            }
        }

        public ICommand _UpdateBusinessLoca { get; set; }
        public ICommand UpdateBusinessLoca
        {
            get
            {
                if (_UpdateBusinessLoca == null)
                {
                    _UpdateBusinessLoca = new DelegateCommand(Update_BusinessLoca);
                }
                return _UpdateBusinessLoca;
            }
        }
        public async void Update_BusinessLoca()
        {
            if (SelectedBusinessLoca.COMPANY == "" || SelectedBusinessLoca.COMPANY == null)
            {
                MessageBox.Show("Company Name is missing");
            }
            else if (SelectedBusinessLoca.SHOP_NAME == "" || SelectedBusinessLoca.SHOP_NAME == null)
            {
                MessageBox.Show("Shop Name is missing");
            }
            else if (SelectedBusinessLoca.TIN_NUMBER == "" || SelectedBusinessLoca.TIN_NUMBER == null)
            {
                MessageBox.Show("Tin Number is missing");

            }
            else if (SelectedBusinessLoca.BUSS_ADDRESS_1 == "" || SelectedBusinessLoca.BUSS_ADDRESS_1 == null)
            {

                MessageBox.Show("BUSSINESS ADDRESS 1 is missing");
            }
            else if (SelectedBusinessLoca.PIN == "" || SelectedBusinessLoca.PIN == null)
            {
                MessageBox.Show("BusinessLoca.PIN is missing");
            }

            else if (SelectedBusinessLoca.PRINTER_SETTING == "" || SelectedBusinessLoca.PRINTER_SETTING == null)
            {
                MessageBox.Show("PRINTER SETTING is missing");
            }
            else if (SelectedBusinessLoca.GODOWN_TO_KEEP == "" || SelectedBusinessLoca.GODOWN_TO_KEEP == null)
            {
                MessageBox.Show("GODOWN TO KEEP is missing");
            }
            else
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                SelectedBusinessLoca.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                var response = await client.PostAsJsonAsync("api/BussLocationAPI/BussLocationUpdate/", SelectedBusinessLoca);
                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Business Location Updated Successfully");
                    Cancel_BusinessLoca();
                    ModalService.NavigateTo(new BussinessLocationList(), delegate (bool returnValue) { });
                }
            }
        }

        public ICommand _CancelBusinessLoca;
        public ICommand CancelBusinessLoca
        {
            get
            {
                if (_CancelBusinessLoca == null)
                {
                    _CancelBusinessLoca = new DelegateCommand(Cancel_BusinessLoca);
                }
                return _CancelBusinessLoca;
            }
        }



        public void Cancel_BusinessLoca()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }
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
        private OpeningStockModel _SelectedOpeningStock;
        public OpeningStockModel SelectedOpeningStock
        {
            get { return _SelectedOpeningStock; }
            set
            {
                if (_SelectedOpeningStock != value)
                {
                    _SelectedOpeningStock = value;

                    OnPropertyChanged("SelectedOpeningStock");
                }
            }
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
            var fdrt = App.Current.Properties["OppingDiffProperties"] as OpeningStockModel;
            var SelectedBusinessLocation = App.Current.Properties["BussLocList"] as BusinessLocationModel;

            SelectedOpeningStock.BUSINESS_LOC = SelectedBusinessLoca.SHOP_NAME;
            if (SelectedBusinessLoca.BUSINESS_LOCATION !=null)
            {
                InvoicePOS.UserControll.Customer.AddCustomer.BusinessLoc.Text = null;
                InvoicePOS.UserControll.Customer.AddCustomer.BusinessLoc.Text = SelectedBusinessLoca.BUSINESS_LOCATION;
            }
            if (fdrt != null)
            {
                SelectedOpeningStock.GODOWN = fdrt.GODOWN;
                SelectedOpeningStock.COMPANY_NAME = fdrt.COMPANY_NAME;

            }
            App.Current.Properties["ChangeBusinessLocation"] = SelectedBusinessLoca;
            App.Current.Properties["OppingDiffProperties"] = SelectedOpeningStock;
            if (Openingbalance.BussRef != null)
            {
                Openingbalance.BussRef.Text = null;
                Openingbalance.BussRef.Text = SelectedOpeningStock.BUSINESS_LOC;
            }
            //if (AddPO.BussRef != null)
            //{
            //    AddPO.BussRef.Text = null;
            //    AddPO.BussRef.Text = SelectedOpeningStock.BUSINESS_LOC;
            //    App.Current.Properties["AddPOBussLocation"] = SelectedOpeningStock.BUSINESS_LOC;
            //    AddPO.DeliveryRef.Text = null;
            //    AddPO.DeliveryRef.Text = SelectedOpeningStock.BUSINESS_LOC;
            //}

            if (App.Current.Properties["BusinessLocation"] != null)
            {
                AddPO.BussRef.Text = null;
                AddPO.BussRef.Text = SelectedBusinessLoca.SHOP_NAME;
                SelectedPO.BUSINESS_LOCATION = SelectedBusinessLoca.SHOP_NAME;
                App.Current.Properties["AddPOBussLocation"] = SelectedOpeningStock.BUSINESS_LOC;
                App.Current.Properties["BusinessLocation"] = null;
            }
            if(App.Current.Properties["BussLocName"] != null && InvoicePOS.Views.CashRegister.CashReg.BusLocationName != null)
            {
                //App.Current.Properties["BussLocList"] = SelectedBusinessLoca.BUSINESS_LOCATION;
                //CashReg.BusLocationName.Text = SelectedBusinessLoca.SHOP_NAME;
                //CashReg.BussAddress.Text = SelectedBusinessLoca.BUSS_ADDRESS_1;
                //ChangeBussinessLocation.BussinessLocationName.Text = null;
                
                App.Current.Properties["ShowBussinessLocation"] = SelectedBusinessLoca.BUSS_ADDRESS_1;
                //ChangeBussinessLocation.BussinessLocationName.Text = SelectedBusinessLoca.SHOP_NAME;
                //ChangeBussinessLocation.Address.Text = SelectedBusinessLoca.BUSS_ADDRESS_1;
                InvoicePOS.Views.CashRegister.CashReg.BusLocationName.Text = SelectedBusinessLoca.SHOP_NAME;
                InvoicePOS.Views.CashRegister.CashReg.BussAddress.Text = SelectedBusinessLoca.BUSS_ADDRESS_1;
            }
            if(App.Current.Properties["ChangeBusinessLocation"] != null && ChangeBussinessLocation.BussinessLocationName != null)
            {
                ChangeBussinessLocation.BussinessLocationName.Text = SelectedBusinessLoca.SHOP_NAME;
                ChangeBussinessLocation.Address.Text = SelectedBusinessLoca.BUSS_ADDRESS_1;
            }
            if (App.Current.Properties["BussLocListforCashAmount"] != null && CashRegisterAmountDetails.CashRegNo != null && CashRegisterAmountDetails.BusLocationName != null)
            {
                //App.Current.Properties["BussLocList"] = SelectedBusinessLoca.BUSINESS_LOCATION;
                
                CashRegisterAmountDetails.CashRegNo.Text = SelectedBusinessLoca.BUSS_ADDRESS_1;
                CashRegisterAmountDetails.BusLocationName.Text = SelectedBusinessLoca.SHOP_NAME;
            }

            if (App.Current.Properties["DeliveryLocation"] != null)
            {
                AddPO.DeliveryRef.Text = null;
                AddPO.DeliveryRef.Text = SelectedBusinessLoca.SHOP_NAME;
                SelectedPO.DELIVER_TO = SelectedBusinessLoca.SHOP_NAME;
                App.Current.Properties["AddPODeliveryTo"] = SelectedBusinessLoca.SHOP_NAME;
                App.Current.Properties["DeliveryLocation"] = null;
            }

            if (EmployeeAdd.BussRef != null)
            {
                EmployeeAdd.BussRef.Text = null;
                EmployeeAdd.BussRef.Text = SelectedOpeningStock.BUSINESS_LOC;
                App.Current.Properties["AddEmpBussLocation"] = SelectedOpeningStock.BUSINESS_LOC;

            }
           
            if (App.Current.Properties["BussMainReff"] != null)
            {
                Main.BussLocationMainReff.Text = null;
                Main.BussLocationMainReff.Text = SelectedBusinessLoca.SHOP_NAME;
                App.Current.Properties["BussMainName"] = SelectedBusinessLoca.SHOP_NAME;
                App.Current.Properties["BussMainReff"] = SelectedBusinessLoca.BUSINESS_LOCATION_ID;
            }
            if (AddStockTransfer.BussRef != null)
            {
                AddStockTransfer.BussRef.Text = null;
                AddStockTransfer.BussRef.Text = SelectedOpeningStock.BUSINESS_LOC;
                App.Current.Properties["BussStockName"] = SelectedOpeningStock.BUSINESS_LOC;
            }
            if (SuppPayAdd.BussRef != null)
            {
                SuppPayAdd.BussRef.Text = null;
                SuppPayAdd.BussRef.Text = SelectedOpeningStock.BUSINESS_LOC;
            }
            if (App.Current.Properties["BussRecivedItem"] != null)
            {
                ReceiveAddItem.BussRef.Text = null;
                ReceiveAddItem.BussRef.Text = SelectedOpeningStock.BUSINESS_LOC;
                App.Current.Properties["ReceiveAddBuss"] = SelectedOpeningStock.BUSINESS_LOC;
                App.Current.Properties["BussRecivedItem"] = null;
            }

            //if (Window_Opening_stock.GodownRef != null)
            //{
            //    Window_Opening_stock.GodownRef.Text = null;
            //    Window_Opening_stock.GodownRef.Text = Convert.ToString(SelectedOpeningStock.GODOWN);
            //}
            //if (Window_Opening_stock.CompanyRef != null)
            //{
            //    Window_Opening_stock.CompanyRef.Text = null;
            //    Window_Opening_stock.CompanyRef.Text = Convert.ToString(SelectedOpeningStock.COMPANY_NAME);
            //}
            //if (App.Current.Properties["BussLocList"] != null)
            //{
            //    CashReg.BusLocationName = SelectedBusinessLocation ;

            //}
            if (App.Current.Properties["BussSalesReturn"] != null)
            {
                SalesReturnAdd.BussReff.Text = null;
                SalesReturnAdd.BussReff.Text = SelectedBusinessLoca.SHOP_NAME;
                App.Current.Properties["SalesReturnBuss"] = SelectedBusinessLoca.SHOP_NAME;
                App.Current.Properties["BussSalesReturn"] = null;
            }
            if (App.Current.Properties["BussLocList"] != null)
            {
                AddCashReg.BussReff.Text = null;
                AddCashReg.BussReff.Text = SelectedBusinessLoca.SHOP_NAME;
                App.Current.Properties["BussCashReg"] = null;
            }
            if (App.Current.Properties["ReceivePaymentBussReff"] != null)
            {
                AddReceivePayment.ReceivePaymentBussReff.Text = null;
                AddReceivePayment.ReceivePaymentBussReff.Text = SelectedBusinessLoca.SHOP_NAME;
                App.Current.Properties["ReceivePaymentBussReff"] = null;
            }
            if (App.Current.Properties["DailySalesBussLocation"] != null)
            {
                DailySales.DailySalesBussreff.Text = null;
                DailySales.DailySalesBussreff.Text = SelectedBusinessLoca.SHOP_NAME;
                App.Current.Properties["DailySalesBussLocation"] = null;
            }
            if (App.Current.Properties["BussLocationRef"] != null)
            {
                Window_Opening_stock.BussRef.Text = null;
                Window_Opening_stock.BussRef.Text = SelectedBusinessLoca.SHOP_NAME;
                App.Current.Properties["BussLocidRef"] = SelectedBusinessLoca.BUSINESS_LOCATION_ID;
                App.Current.Properties["BussLocationRef"] = null;
            }
            if (App.Current.Properties["getcompany"] != null)
            {
                App.Current.Properties["getcompanyid"] = SelectedBusinessLoca.COMPANY_ID;
                App.Current.Properties["getcompanyname"] = SelectedBusinessLoca.COMPANY;
                StockLedgerList.getCompanyName.Text = SelectedBusinessLoca.COMPANY;
                App.Current.Properties["getcompany"] = null;
            }
            Cancel_BusinessLoca();
        }
        //public ICommand _SelectOK;
        //public ICommand SelectOK
        //{
        //    get
        //    {
        //        if (_SelectOK == null)
        //        {
        //            _SelectOK = new DelegateCommand(Select_BusinessLoca);
        //        }
        //        return _SelectOK;
        //    }
        //}

        //public void Select_BusinessLoca()
        //{
        //    OpeningStockModel _SelectedOpeningStock = new OpeningStockModel();

        //    _SelectedOpeningStock.BUSINESS_LOC_ID= SelectedBusinessLoca.BUSINESS_LOCATION_ID;
        //    _SelectedOpeningStock.BUSINESS_LOC = "Tamluk";
        //    App.Current.Properties["Value"] = _SelectedOpeningStock;
        //    SelectedBusinessLoca.BUSINESS_LOCATION = "Tamluk";
        //    Cancel_OppStock();
        //   // Window_Opening_stock OSVW = new Window_Opening_stock();


        //}

        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }
        public ICommand _DeleteBussLoc;
        public ICommand DeleteBussLoc
        {
            get
            {
                if (_DeleteBussLoc == null)
                {
                    _DeleteBussLoc = new DelegateCommand(Delete_BussLoca);
                }
                return _DeleteBussLoc;
            }
        }





        public async void Delete_BussLoca()
        {
            if (SelectedBusinessLoca.BUSINESS_LOCATION_ID != null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure Delete this  business Location " + SelectedBusinessLoca.SHOP_NAME + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var id = SelectedBusinessLoca.BUSINESS_LOCATION_ID;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    HttpResponseMessage response = client.GetAsync("api/BussLocationAPI/DeleteBussLocation?id=" + id + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {
                        MessageBox.Show("Business Location Delete Successfully");
                        ModalService.NavigateTo(new BussinessLocationList(), delegate (bool returnValue) { });
                    }
                }
                else
                {
                    Cancel_BusinessLoca();
                }
            }
            else
            {
                MessageBox.Show("Select Business Location");
            }

        }
        public List<BusinessLocationModel> _ListGrid { get; set; }
        public List<BusinessLocationModel> ListGrid
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

        public BussinessLocationViewModel()
        {
            SelectedPO = new POrderModel();
            var COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            SelectedOpeningStock = new OpeningStockModel();
           
            if (App.Current.Properties["Action"] == "Edit")
            {
                CreateVisible = "Collapsed";
                UpdateVisible = "Visible";
                SelectedBusinessLoca = App.Current.Properties["BussLocEdit"] as BusinessLocationModel;
                // GetBussinessList(COMPANY_ID);
                App.Current.Properties["Action"] = "";
            }
            else if (App.Current.Properties["Action"] == "View")
            {
                SelectedBusinessLoca = App.Current.Properties["BussLocView"] as BusinessLocationModel;
                // GetBussinessList(COMPANY_ID);
                App.Current.Properties["BussLocView"] = "";
                // App.Current.Properties["Action"] = "";
            }
            else if (App.Current.Properties["Action"] == "Add")
            {
                CreateVisible = "Visible";
                UpdateVisible = "Collapsed";
                SelectedBusinessLoca = new BusinessLocationModel();
                GetBussinessList(COMPANY_ID);
                // GetBussinessList(COMPANY_ID);
                // App.Current.Properties["Action"] = "";
            }
            else
            {
                SelectedBusinessLoca = new BusinessLocationModel { };
                GetBussinessList(COMPANY_ID);
            }

        }
        public ICommand _LocationClick;
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
            App.Current.Properties["Action"] = "Add";
            BusinessLocationAdd BLA = new BusinessLocationAdd();

            BLA.Show();

            // ModalService.NavigateTo(new BusinessLocationAdd(), delegate(bool returnValue) { });

        }
        private void OnClosingCommandExecuted(CancelEventArgs cancelEventArgs)
        {
            // logic to check if view model has updated since it is loaded

            cancelEventArgs.Cancel = true;

        }
        public void Close()
        {
            try
            {
                //BussinessLocationList _BLA = new BussinessLocationList();
                //_BLA.Show();
                //Dispatcher.BeginInvoke(new Action(delegate { _BLA.Activate(); }), System.Windows.Threading.DispatcherPriority.ContextIdle, null);
                Application.Current.MainWindow.Close();
            }
            catch (Exception EX)
            {

                throw;
            }
        }
        public void Cancel_OppStock()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
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
