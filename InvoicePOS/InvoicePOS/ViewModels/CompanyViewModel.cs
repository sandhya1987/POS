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
using System.Net;


namespace InvoicePOS.ViewModels
{
    public class CompanyViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        public HttpResponseMessage response;
        private readonly CompanyModel OprModel;
        private Dictionary<string, bool> validProperties;
        CompanyModel _CompanyModel = new CompanyModel();
        public ICommand select { get; set; }
        CompanyModel[] data = null;

        private CompanyModel _SelectedCompany;
        public CompanyModel SelectedCompany
        {
            get { return _SelectedCompany; }
            set
            {
                if (_SelectedCompany != value)
                {
                    _SelectedCompany = value;
                    OnPropertyChanged("SelectedCompany");
                }

            }
        }
        private string _NAME;
        public string NAME
        {
            get
            {
                return SelectedCompany.NAME;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new Exception("Name can not be empty.");
                }
                if (SelectedCompany.NAME != value)
                {
                    SelectedCompany.NAME = value;
                    OnPropertyChanged("NAME");
                }
            }
        }

        public async Task<ObservableCollection<CompanyModel>> EditComapny(int cID)
        {

            try
            {

                string ftpServerIP = "115.115.196.30";
                string ftpUserID = "suvendu";
                string ftpPassword = "Passw0rd";

                string filename = "ftp://" + ftpServerIP + "//Upload//";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.GetAsync("api/CompanyAPI/EditCompany?id=" + cID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<CompanyModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedCompany.EMAIL = data[i].EMAIL;
                            SelectedCompany.NAME = data[i].NAME;
                            SelectedCompany.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedCompany.SHOPNAME = data[i].SHOPNAME;
                            SelectedCompany.PREFIX = data[i].PREFIX;
                            SelectedCompany.PREFIX_NUM = data[i].PREFIX_NUM;
                            SelectedCompany.TIN_NUMBER = data[i].TIN_NUMBER;
                            SelectedCompany.ADDRESS_1 = data[i].ADDRESS_1;
                            SelectedCompany.ADDRESS_2 = data[i].ADDRESS_2;
                            SelectedCompany.CITY = data[i].CITY;
                            SelectedCompany.STATE = data[i].STATE;
                            SelectedCompany.COUNTRY = data[i].COUNTRY;
                            SelectedCompany.PIN = data[i].PIN;
                            SelectedCompany.PHONE_NUMBER = data[i].PHONE_NUMBER;
                            SelectedCompany.MOBILE_NUMBER = data[i].MOBILE_NUMBER;
                            SelectedCompany.IMAGE_PATH = data[i].IMAGE_PATH;
                            SelectedCompany.WEBSITE = data[i].WEBSITE;
                            SelectedCompany.DEFAULT_TAX_RATE = data[i].DEFAULT_TAX_RATE;
                            SelectedCompany.IS_WARNED_FOR_NEGATIVE_STOCK = data[i].IS_WARNED_FOR_NEGATIVE_STOCK;
                            SelectedCompany.IS_WARNED_FOR_LESS_SALES_PRICE = data[i].IS_WARNED_FOR_LESS_SALES_PRICE;
                            SelectedCompany.TAX_PRINTED_DESCRIPTION = data[i].TAX_PRINTED_DESCRIPTION;
                            SelectedCompany.FRIST_DAY_OF_FINANCIAL_YEAR = data[i].FRIST_DAY_OF_FINANCIAL_YEAR;
                            SelectedCompany.FIRST_MONTH_OF_FINANCIAL_YEAR = data[i].FIRST_MONTH_OF_FINANCIAL_YEAR;
                            SelectedCompany.BANK_NAME = data[i].BANK_NAME;
                            //SelectedCompany.BANK_ID = data[i].BANK_ID;
                            //SelectedCompany.BANK_ACCOUNT_ID = data[i].BANK_ACCOUNT_ID;
                            SelectedCompany.IFSC_CODE = data[i].IFSC_CODE;
                            SelectedCompany.BRANCH_NAME = data[i].BRANCH_NAME;
                            SelectedCompany.ACCOUNT_NUMBER = data[i].ACCOUNT_NUMBER;
                            SelectedCompany.BANK_ADDRESS_1 = data[i].BANK_ADDRESS_1;
                            SelectedCompany.BANK_ADDRESS_2 = data[i].BANK_ADDRESS_2;

                            SelectedCompany.BANK_CITY = data[i].BANK_CITY;
                            SelectedCompany.BANK_STATE = data[i].BANK_STATE;
                            SelectedCompany.BANK_PIN_CODE = data[i].BANK_PIN_CODE;
                            SelectedCompany.BANK_PHONE_NUMBER = data[i].BANK_PHONE_NUMBER;
                            SelectedCompany.BANK_CODE = data[i].BANK_CODE;








                            var fr = filename + data[i].IMAGE_PATH;

                            var imageFile = new System.IO.FileInfo(data[i].IMAGE_PATH);
                            string file = imageFile.Name;
                            var applicationPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);


                            // get your 'Uploaded' folder
                            var dir = new System.IO.DirectoryInfo(System.IO.Path.Combine(applicationPath, "uploaded"));
                            //if (!dir.Exists)
                            //    dir.Create();
                            // Copy file to your folder
                            //No-Image


                            // imageFile.CopyTo(System.IO.Path.Combine(dir.FullName, file),false);
                            string path1 = System.IO.Path.Combine(dir.FullName + "\\", file);

                            //  FtpDown(path1, file);

                            IMAGE_PATH1 = new BitmapImage(new Uri(path1));
                            App.Current.Properties["ItemViewImg"] = IMAGE_PATH1;



                            SelectedCompany.IMAGE_PATH = file;
                        }



                        App.Current.Properties["EditComapnyS"] = SelectedCompany;
                    }
                }
            }
            catch (Exception ex)
            {


            }

            return new ObservableCollection<CompanyModel>(_ListGrid_Temp);
        }

        private string _SHOP_NAME;
        public string SHOP_NAME
        {
            get
            {
                return SelectedCompany.SHOPNAME;
            }
            set
            {
                SelectedCompany.SHOPNAME = value;
                if (string.IsNullOrEmpty(value))
                {
                    //ValidationError
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCompany.SHOPNAME != value)
                {
                    SelectedCompany.SHOPNAME = value;
                    OnPropertyChanged("SHOPNAME");
                }
            }
        }
        private string _PREFIX;
        public string PREFIX
        {
            get
            {
                return SelectedCompany.PREFIX;
            }
            set
            {
                SelectedCompany.PREFIX = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCompany.PREFIX != value)
                {
                    SelectedCompany.PREFIX = value;
                    OnPropertyChanged("PREFIX");
                }
            }
        }
        private string _PREFIX_NUM;
        public string PREFIX_NUM
        {
            get
            {
                return SelectedCompany.PREFIX_NUM;
            }
            set
            {
                SelectedCompany.PREFIX_NUM = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCompany.PREFIX_NUM != value)
                {
                    SelectedCompany.PREFIX_NUM = value;
                    OnPropertyChanged("PREFIX_NUM");
                }
            }
        }

        private string _TIN_NUMBER;
        public string TIN_NUMBER
        {
            get
            {
                return SelectedCompany.TIN_NUMBER;
            }
            set
            {
                SelectedCompany.TIN_NUMBER = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCompany.TIN_NUMBER != value)
                {
                    SelectedCompany.TIN_NUMBER = value;
                    OnPropertyChanged("TIN_NUMBER");
                }
            }
        }
        private string _ADDRESS_1;
        public string ADDRESS_1
        {
            get
            {
                return SelectedCompany.ADDRESS_1;
            }
            set
            {
                SelectedCompany.ADDRESS_1 = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCompany.ADDRESS_1 != value)
                {
                    SelectedCompany.ADDRESS_1 = value;
                    OnPropertyChanged("ADDRESS_1");
                }
            }
        }
        private string _ADDRESS_2;
        public string ADDRESS_2
        {
            get
            {
                return SelectedCompany.ADDRESS_2;
            }
            set
            {
                SelectedCompany.ADDRESS_2 = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCompany.ADDRESS_2 != value)
                {
                    SelectedCompany.ADDRESS_2 = value;
                    OnPropertyChanged("ADDRESS_1");
                }
            }
        }

        private string _CITY;
        public string CITY
        {
            get
            {
                return SelectedCompany.CITY;
            }
            set
            {
                SelectedCompany.CITY = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCompany.CITY != value)
                {
                    SelectedCompany.CITY = value;
                    OnPropertyChanged("CITY");
                }
            }
        }
        private string _STATE;
        public string STATE
        {
            get
            {
                return SelectedCompany.STATE;
            }
            set
            {
                SelectedCompany.STATE = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCompany.STATE != value)
                {
                    SelectedCompany.STATE = value;
                    OnPropertyChanged("STATE");
                }
            }
        }

        private string _COUNTRY;
        public string COUNTRY
        {
            get
            {
                return SelectedCompany.COUNTRY;
            }
            set
            {
                SelectedCompany.COUNTRY = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCompany.COUNTRY != value)
                {
                    SelectedCompany.COUNTRY = value;
                    OnPropertyChanged("COUNTRY");
                }
            }
        }
        private string _PIN;
        public string PIN
        {
            get
            {
                return SelectedCompany.PIN;
            }
            set
            {
                SelectedCompany.PIN = value;

                if (SelectedCompany.PIN != value)
                {
                    SelectedCompany.PIN = value;
                    OnPropertyChanged("PIN");
                }
            }
        }


        private decimal _DEFAULT_TAX_RATE { get; set; }
        public decimal DEFAULT_TAX_RATE
        {
            get
            {
                return _DEFAULT_TAX_RATE;
            }
            set
            {
                _DEFAULT_TAX_RATE = value;

                if (_DEFAULT_TAX_RATE != value)
                {
                    _DEFAULT_TAX_RATE = value;
                    SelectedCompany.DEFAULT_TAX_RATE = _DEFAULT_TAX_RATE;
                    OnPropertyChanged("DEFAULT_TAX_RATE");
                }
            }
        }

        private string _PHONE_NUMBER;
        public string PHONE_NUMBER
        {
            get
            {
                return SelectedCompany.PHONE_NUMBER;
            }
            set
            {
                SelectedCompany.PHONE_NUMBER = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCompany.PHONE_NUMBER != value)
                {
                    SelectedCompany.PHONE_NUMBER = value;
                    OnPropertyChanged("PHONE_NUMBER");
                }
            }
        }
        private string _EMAIL;
        public string EMAIL
        {
            get
            {
                return SelectedCompany.EMAIL;
            }
            set
            {
                SelectedCompany.EMAIL = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCompany.EMAIL != value)
                {
                    SelectedCompany.EMAIL = value;
                    OnPropertyChanged("EMAIL");
                }
            }
        }
        private string _WEBSITE;
        public string WEBSITE
        {
            get
            {
                return SelectedCompany.WEBSITE;
            }
            set
            {
                SelectedCompany.WEBSITE = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Field not be blank");
                }

                if (SelectedCompany.WEBSITE != value)
                {
                    SelectedCompany.WEBSITE = value;
                    OnPropertyChanged("WEBSITE");
                }
            }
        }


        private string _BANK_CODE;
        public string BANK_CODE
        {
            get
            {
                return SelectedCompany.BANK_CODE;
            }
            set
            {
                SelectedCompany.BANK_CODE = value;


                if (SelectedCompany.BANK_CODE != value)
                {
                    SelectedCompany.BANK_CODE = value;
                    OnPropertyChanged("BANK_CODE");
                }
            }
        }
        private string _BANK_NAME;
        public string BANK_NAME
        {
            get
            {
                return SelectedCompany.BANK_NAME;
            }
            set
            {
                SelectedCompany.BANK_NAME = value;


                if (SelectedCompany.BANK_NAME != value)
                {
                    SelectedCompany.BANK_NAME = value;
                    OnPropertyChanged("BANK_NAME");
                }
            }
        }

        private string _BRANCH_NAME;
        public string BRANCH_NAME
        {
            get
            {
                return SelectedCompany.BRANCH_NAME;
            }
            set
            {
                SelectedCompany.BRANCH_NAME = value;


                if (SelectedCompany.BRANCH_NAME != value)
                {
                    SelectedCompany.BRANCH_NAME = value;
                    OnPropertyChanged("BANK_NAME");
                }
            }
        }
        private string _ACCOUNT_NUMBER;
        public string ACCOUNT_NUMBER
        {
            get
            {
                return SelectedCompany.ACCOUNT_NUMBER;
            }
            set
            {
                SelectedCompany.ACCOUNT_NUMBER = value;
                if (SelectedCompany.ACCOUNT_NUMBER != value)
                {
                    SelectedCompany.ACCOUNT_NUMBER = value;
                    OnPropertyChanged("ACCOUNT_NUMBER");
                }
            }
        }
        private string _BANK_ADDRESS_1;
        public string BANK_ADDRESS_1
        {
            get
            {
                return SelectedCompany.BANK_ADDRESS_1;
            }
            set
            {
                SelectedCompany.BANK_ADDRESS_1 = value;
                if (SelectedCompany.BANK_ADDRESS_1 != value)
                {
                    SelectedCompany.BANK_ADDRESS_1 = value;
                    OnPropertyChanged("BANK_ADDRESS_1");
                }
            }
        }

        private string _BANK_ADDRESS_2;
        public string BANK_ADDRESS_2
        {
            get
            {
                return SelectedCompany.BANK_ADDRESS_2;
            }
            set
            {
                SelectedCompany.BANK_ADDRESS_2 = value;
                if (SelectedCompany.BANK_ADDRESS_2 != value)
                {
                    SelectedCompany.BANK_ADDRESS_2 = value;
                    OnPropertyChanged("BANK_ADDRESS_2");
                }
            }
        }

        private string _BANK_CITY;
        public string BANK_CITY
        {
            get
            {
                return SelectedCompany.BANK_CITY;
            }
            set
            {
                SelectedCompany.BANK_CITY = value;
                if (SelectedCompany.BANK_CITY != value)
                {
                    SelectedCompany.BANK_CITY = value;
                    OnPropertyChanged("BANK_CITY");
                }
            }
        }


        private string _BANK_STATE;
        public string BANK_STATE
        {
            get
            {
                return SelectedCompany.BANK_STATE;
            }
            set
            {
                SelectedCompany.BANK_STATE = value;
                if (SelectedCompany.BANK_STATE != value)
                {
                    SelectedCompany.BANK_STATE = value;
                    OnPropertyChanged("BANK_STATE");
                }
            }
        }

        private string _BANK_PIN_CODE;
        public string BANK_PIN_CODE
        {
            get
            {
                return SelectedCompany.BANK_PIN_CODE;
            }
            set
            {
                SelectedCompany.BANK_PIN_CODE = value;


                if (SelectedCompany.BANK_PIN_CODE != value)
                {
                    SelectedCompany.BANK_PIN_CODE = value;
                    OnPropertyChanged("BANK_PIN_CODE");
                }
            }
        }

        private string _BANK_PHONE_NUMBER;
        public string BANK_PHONE_NUMBER
        {
            get
            {
                return SelectedCompany.BANK_PHONE_NUMBER;
            }
            set
            {
                SelectedCompany.BANK_PHONE_NUMBER = value;
                if (SelectedCompany.BANK_PHONE_NUMBER != value)
                {
                    SelectedCompany.BANK_PHONE_NUMBER = value;
                    OnPropertyChanged("BANK_PHONE_NUMBER");
                }
            }
        }
        private bool _IS_WARNED_FOR_NEGATIVE_STOCK;
        public bool IS_WARNED_FOR_NEGATIVE_STOCK
        {
            get
            {
                return SelectedCompany.IS_WARNED_FOR_NEGATIVE_STOCK;
            }
            set
            {
                SelectedCompany.IS_WARNED_FOR_NEGATIVE_STOCK = value;
                if (SelectedCompany.IS_WARNED_FOR_NEGATIVE_STOCK != value)
                {
                    SelectedCompany.IS_WARNED_FOR_NEGATIVE_STOCK = value;
                    OnPropertyChanged("IS_WARNED_FOR_NEGATIVE_STOCK");
                }
            }
        }
        private bool _IS_WARNED_FOR_LESS_SALES_PRICE;
        public bool IS_WARNED_FOR_LESS_SALES_PRICE
        {
            get
            {
                return SelectedCompany.IS_WARNED_FOR_LESS_SALES_PRICE;
            }
            set
            {
                SelectedCompany.IS_WARNED_FOR_LESS_SALES_PRICE = value;
                if (SelectedCompany.IS_WARNED_FOR_LESS_SALES_PRICE != value)
                {
                    SelectedCompany.IS_WARNED_FOR_LESS_SALES_PRICE = value;
                    OnPropertyChanged("IS_WARNED_FOR_LESS_SALES_PRICE");
                }
            }
        }


        private Int16 _FRIST_DAY_OF_FINANCIAL_YEAR;
        public Int16 FRIST_DAY_OF_FINANCIAL_YEAR
        {
            get
            {
                return _FRIST_DAY_OF_FINANCIAL_YEAR;
            }
            set
            {
                if (_FRIST_DAY_OF_FINANCIAL_YEAR != value)
                {
                    _FRIST_DAY_OF_FINANCIAL_YEAR = value;
                    if (null != PropertyChanged)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("FRIST_DAY_OF_FINANCIAL_YEAR"));
                        SelectedCompany.FRIST_DAY_OF_FINANCIAL_YEAR = _FRIST_DAY_OF_FINANCIAL_YEAR;
                    }
                }
            }
        }

        private Int16 _FRIST_MONTH_OF_FINANCIAL_YEAR;
        public Int16 FRIST_MONTH_OF_FINANCIAL_YEAR
        {
            get
            {
                return _FRIST_MONTH_OF_FINANCIAL_YEAR;
            }
            set
            {
                if (_FRIST_MONTH_OF_FINANCIAL_YEAR != value)
                {
                    _FRIST_MONTH_OF_FINANCIAL_YEAR = value;
                    if (null != PropertyChanged)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("FRIST_MONTH_OF_FINANCIAL_YEAR"));
                        SelectedCompany.FIRST_MONTH_OF_FINANCIAL_YEAR = _FRIST_MONTH_OF_FINANCIAL_YEAR;
                    }
                }
            }
        }

        private ImageSource _IMAGE_PATH;
        public ImageSource IMAGE_PATH
        {
            get { return _IMAGE_PATH; }
            set
            {
                if (Equals(value, _IMAGE_PATH)) return;
                _IMAGE_PATH = value;
                OnPropertyChanged("IMAGE_PATH");
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

            openFileDialog.Filter = "Image Files(*.jpg; .jpeg; .gif; .bmp)|*.jpg; .jpeg; .gif; .bmp";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {

                if (File.Exists(openFileDialog.FileName))
                {
                    IMAGE_PATH1 = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
                    // IMAGE_PATH = Convert.ToString( new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute)));
                    string imagepath = openFileDialog.FileName.ToString();
                    var imageFile = new System.IO.FileInfo(imagepath);
                    string file = imageFile.Name;
                    var applicationPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);


                    // get your 'Uploaded' folder
                    var dir = new System.IO.DirectoryInfo(System.IO.Path.Combine(applicationPath, "uploaded"));
                    if (!dir.Exists)
                        dir.Create();
                    // Copy file to your folder
                    imageFile.CopyTo(System.IO.Path.Combine(dir.FullName + "\\", file), true);
                    string path1 = System.IO.Path.Combine(dir.FullName + "\\", file);

                    Ftpup(path1, openFileDialog.SafeFileName);
                    SelectedCompany.IMAGE_PATH = openFileDialog.SafeFileName;
                }


            }


        }
        public static void Ftpup(string sourceFile, string targetFile)
        {
            try
            {
                string ftpServerIP = "115.115.196.30";
                string ftpUserID = "suvendu";
                string ftpPassword = "Passw0rd";

                string filename = "ftp://" + ftpServerIP + "//Upload//" + targetFile;
                FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(filename);
                ftpReq.UseBinary = true;

                ftpReq.Method = WebRequestMethods.Ftp.UploadFile;
                ftpReq.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                byte[] b = System.IO.File.ReadAllBytes(sourceFile);

                ftpReq.ContentLength = b.Length;
                int d = b.Length;
                using (Stream s = ftpReq.GetRequestStream())
                {
                    s.Write(b, 0, b.Length);
                }

                FtpWebResponse ftpResp = (FtpWebResponse)ftpReq.GetResponse();

                if (ftpResp != null)
                {
                    string responseDesc = ftpResp.StatusDescription;
                }
            }
            catch (Exception ex)
            {
                string responseDesc = ex.ToString();
            }
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

        // public ICommand _InsertCompany { get; set; }
        public ICommand InsertCompany { get; set; }


        public async void Insert_Company()
        {


            if (SelectedCompany.NAME == "" || SelectedCompany.NAME == null)
            {

            }
            else if (SelectedCompany.SHOPNAME == "" || SelectedCompany.SHOPNAME == null)
            {

            }
            else if (SelectedCompany.PREFIX_NUM == "" || SelectedCompany.PREFIX_NUM == null)
            {

            }
            else if (SelectedCompany.TIN_NUMBER == "" || SelectedCompany.TIN_NUMBER == null)
            {

            }
            else if (SelectedCompany.ADDRESS_1 == "" || SelectedCompany.ADDRESS_1 == null)
            {

            }
            else if (SelectedCompany.STATE == "" || SelectedCompany.STATE == null)
            {

            }
            else if (SelectedCompany.CITY == "" || SelectedCompany.CITY == null)
            {

            }
            else if (SelectedCompany.PIN == "" || SelectedCompany.PIN == null)
            {

            }
            else if (SelectedCompany.EMAIL == "" || SelectedCompany.EMAIL == null)
            {

            }
            else if (SelectedCompany.BANK_NAME == "" || SelectedCompany.EMAIL == null)
            {

            }
            else if (SelectedCompany.BANK_CODE == "" || SelectedCompany.EMAIL == null)
            {

            }
            else if (SelectedCompany.BRANCH_NAME == "" || SelectedCompany.EMAIL == null)
            {

            }
            //else if (SelectedCompany.ACCOUNT_NUMBER.ToString() == "" || SelectedCompany.ACCOUNT_NUMBER == null)
            //{

            //}
            else
            {


                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
                //client.Timeout = new TimeSpan(500000000000);
                HttpContent content = new ObjectContent<CompanyModel>(SelectedCompany, jsonFormatter);
                var response = await client.PostAsync("api/CompanyAPI/CreateCompany/", content);

                if (response.StatusCode.ToString() == "OK")
                {
                    //GetCompany();
                    Close();
                }
                else
                {
                    SelectedCompany = new CompanyModel();
                }


            }
        }
        ObservableCollection<CompanyModel> _ListGrid_Temp = new ObservableCollection<CompanyModel>();
        private List<CompanyModel> _CompanyData;
        public List<CompanyModel> CompanyData
        {
            get { return _CompanyData; }
            set
            {
                if (_CompanyData != value)
                {
                    _CompanyData = value;
                }
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
        public async Task<ObservableCollection<CompanyModel>> GetCompany()
        {
            try
            {

                //string ftpServerIP = "115.115.196.30";
                //string ftpUserID = "suvendu";
                //string ftpPassword = "Passw0rd";

                //string filename = "ftp://" + ftpServerIP + "//Upload//";



                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                //response = client.GetAsync("api/CompanyAPI/GetCompany?id=" + Id + "").Result;
                response = client.GetAsync("api/CompanyAPI/GetCompany").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<CompanyModel[]>(await response.Content.ReadAsStringAsync());
                    CompanyData = new List<CompanyModel>();
                    for (int i = 0; i < data.Length; i++)
                    {
                        int croId = Convert.ToInt32(data[i].COMPANY_ID);
                        string NAME = data[i].NAME;
                        string ADDRESS_1 = data[i].ADDRESS_1;
                        string ADDRESS_2 = data[i].ADDRESS_2;
                        string CITY = data[i].CITY;
                        SelectedCompany.NAME = data[i].NAME; ;
                        _ListGrid_Temp.Add(new CompanyModel
                        {
                            ADDRESS_1 = ADDRESS_1,
                            NAME = NAME,
                            CITY = CITY,
                            ADDRESS_2 = ADDRESS_2,
                        });
                        App.Current.Properties["Company_Id"] = Convert.ToInt32(data[i].COMPANY_ID);
                        App.Current.Properties["Company_Name"] = data[i].NAME;
                        App.Current.Properties["Company_Email"] = data[i].EMAIL;
                        App.Current.Properties["Company_Address1"] = data[i].ADDRESS_1;
                        App.Current.Properties["Company_Address2"] = data[i].ADDRESS_2;
                        App.Current.Properties["Company_Mobile"] = data[i].MOBILE_NUMBER;
                        App.Current.Properties["Company_Phone"] = data[i].PHONE_NUMBER;
                        App.Current.Properties["Company_Pin"] = data[i].PIN;


                        if (data[i].IMAGE_PATH != null)
                        {


                            //var fr = filename + data[i].IMAGE_PATH;

                            //var imageFile = new System.IO.FileInfo(data[i].IMAGE_PATH);
                            //string file = imageFile.Name;
                            var applicationPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);


                            // get your 'Uploaded' folder
                            var dir = new System.IO.DirectoryInfo(System.IO.Path.Combine(applicationPath, "uploaded"));
                            //if (!dir.Exists)
                            //    dir.Create();
                            // Copy file to your folder
                            //imageFile.CopyTo(System.IO.Path.Combine(dir.FullName, file), true);
                            string path1 = System.IO.Path.Combine(dir.FullName + "\\", data[i].IMAGE_PATH);

                           // FtpDown(path1, file);

                            IMAGE_PATH1 = new BitmapImage(new Uri(path1));
                            //App.Current.Properties["ItemViewImg"] = IMAGE_PATH1;
                        }
                    }

                }
                //ListGrid = _ListGrid_Temp;
                //return new ObservableCollection<CompanyModel>(_ListGrid_Temp);
            }
            catch (Exception ex)
            {


            }
            ListGrid = _ListGrid_Temp;
            return new ObservableCollection<CompanyModel>(_ListGrid_Temp);
        }


        public ObservableCollection<CompanyModel> _ListGrid { get; set; }
        public ObservableCollection<CompanyModel> ListGrid
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

        public CompanyViewModel()
        {
            try
            {
                SelectedCompany = new CompanyModel();
                if (App.Current.Properties["EditCompany"] == "CompanyEdit")
                {
                    CreatVisible = "Collapsed";
                    UpdVisible = "Visible";
                    int cID = Convert.ToInt32(App.Current.Properties["Company_Id"]);
                    EditComapny(cID);
                    SelectedCompany = App.Current.Properties["EditComapnyS"] as CompanyModel;
                }
                else
                {
                    CreatVisible = "Visible";
                    UpdVisible = "Collapsed";
                    OprModel = _CompanyModel;
                    GetCompany();
                    if (data.Length > 0)
                    {
                        // Main mn = new Main();
                        //mn.Show();
                        // this.Close();
                        // Close();
                    }
                    else
                    {
                        InsertCompany = new DelegateCommand(Insert_Company);
                    }
                }
            }
            catch (Exception ex)
            {


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

        public void Close()
        {
            try
            {
                Main _Main = new Main();
                _Main.Show();
                Dispatcher.BeginInvoke(new Action(delegate { _Main.Activate(); }), System.Windows.Threading.DispatcherPriority.ContextIdle, null);
                Application.Current.MainWindow.Close();
            }
            catch (Exception EX)
            {

                throw;
            }
        }
        public ICommand _UpdateCompany { get; set; }
        public ICommand UpdateCompany
        {
            get
            {
                if (_UpdateCompany == null)
                {

                    _UpdateCompany = new DelegateCommand(Update_Company);
                }
                return _UpdateCompany;
            }
        }


        public async void Update_Company()
        {


            if (SelectedCompany.NAME == "" || SelectedCompany.NAME == null)
            {

            }
            else if (SelectedCompany.SHOPNAME == "" || SelectedCompany.SHOPNAME == null)
            {

            }
            else if (SelectedCompany.PREFIX_NUM == "" || SelectedCompany.PREFIX_NUM == null)
            {

            }
            else if (SelectedCompany.TIN_NUMBER == "" || SelectedCompany.TIN_NUMBER == null)
            {

            }
            else if (SelectedCompany.ADDRESS_1 == "" || SelectedCompany.ADDRESS_1 == null)
            {

            }
            else if (SelectedCompany.STATE == "" || SelectedCompany.STATE == null)
            {

            }
            else if (SelectedCompany.CITY == "" || SelectedCompany.CITY == null)
            {

            }
            else if (SelectedCompany.PIN == "" || SelectedCompany.PIN == null)
            {

            }
            else if (SelectedCompany.EMAIL == "" || SelectedCompany.EMAIL == null)
            {

            }
            else if (SelectedCompany.BANK_NAME == "" || SelectedCompany.EMAIL == null)
            {

            }
            else if (SelectedCompany.BANK_CODE == "" || SelectedCompany.EMAIL == null)
            {

            }
            else if (SelectedCompany.BRANCH_NAME == "" || SelectedCompany.EMAIL == null)
            {

            }
            else
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
                HttpContent content = new ObjectContent<CompanyModel>(SelectedCompany, jsonFormatter);
                var response = await client.PostAsync("api/CompanyAPI/UpdateCompany/", content);

                if (response.StatusCode.ToString() == "OK")
                {
                    MessageBox.Show("Company Update Successfully");
                    Cancel_Company();
                }


            }
        }
        public ICommand _Cancel;
        public ICommand Cancel
        {
            get
            {
                if (_Cancel == null)
                {
                    _Cancel = new DelegateCommand(Cancel_Company);
                }
                return _Cancel;
            }
        }



        public void Cancel_Company()
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
