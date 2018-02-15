using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Item;
using InvoicePOS.UserControll.PO;
using InvoicePOS.UserControll.StockTransfer;
using InvoicePOS.UserControll.Supplier;
using InvoicePOS.UserControll.SuppPayment;
using InvoicePOS.Views.Customer;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InvoicePOS.ViewModels
{
    public class SupplierViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {


        public HttpResponseMessage response;
        private readonly SupplierModel SPModel;
        SupplierModel _SPModel = new SupplierModel();
        public ICommand select { get; set; }
        SupplierModel[] data = null;



        private SupplierModel _SelectedSupplier;
        public SupplierModel SelectedSupplier
        {
            get { return _SelectedSupplier; }
            set
            {
                if (_SelectedSupplier != value)
                {
                    _SelectedSupplier = value;
                    OnPropertyChanged("SelectedSupplier");
                }

            }
        }

        private long _COMPANY_ID;
        public long COMPANY_ID
        {
            get
            {
                return SelectedSupplier.COMPANY_ID;
            }
            set
            {
                SelectedSupplier.COMPANY_ID = value;


                if (SelectedSupplier.COMPANY_ID != value)
                {
                    SelectedSupplier.COMPANY_ID = value;
                    OnPropertyChanged("COMPANY_ID");
                }
            }
        }
        private string _SUPPLIER_CODE;
        public string SUPPLIER_CODE
        {
            get
            {
                return SelectedSupplier.SUPPLIER_CODE;
            }
            set
            {
                SelectedSupplier.SUPPLIER_CODE = value;
                OnPropertyChanged("SUPPLIER_CODE");

            }
        }
        //private string _SUPPLIER_NAME;//Prev Code
        //public string SUPPLIER_NAME
        //{
        //    get
        //    {
        //        return _SUPPLIER_NAME;
        //    }
        //    set
        //    {
        //        _SUPPLIER_NAME = value;
        //        if (_SUPPLIER_NAME != null)
        //        {

        //            DisplaySuppName = value;
        //        }
        //        OnPropertyChanged("SUPPLIER_NAME");

        //    }
        //}

        private string _SUPPLIER_NAME;
        public string SUPPLIER_NAME
        {
            get
            {
                return SelectedSupplier.SUPPLIER_NAME;
            }
            set
            {
                SelectedSupplier.SUPPLIER_NAME = value;
                DisplaySuppName = SelectedSupplier.SUPPLIER_NAME;
                OnPropertyChanged("SUPPLIER_NAME");

            }
        }

        //private string _DisplaySuppName;//Prev Code
        //public string DisplaySuppName
        //{
        //    get
        //    {
        //        return _SUPPLIER_NAME;
        //    }
        //    set
        //    {
        //        _DisplaySuppName = value;

        //        OnPropertyChanged("DisplaySuppName");

        //    }
        //}


        private string _DisplaySuppName;
        public string DisplaySuppName
        {
            get
            {
                return SelectedSupplier.DisplaySuppName;
            }
            set
            {
                SelectedSupplier.DisplaySuppName = SelectedSupplier.SUPPLIER_NAME;
                OnPropertyChanged("DisplaySuppName");

            }
        }


        private string _SEARCH_CODE;
        public string SEARCH_CODE
        {
            get
            {
                return SelectedSupplier.SEARCH_CODE;
            }
            set
            {
                SelectedSupplier.SEARCH_CODE = value;


                if (SelectedSupplier.SEARCH_CODE != value)
                {
                    SelectedSupplier.SEARCH_CODE = value;
                    OnPropertyChanged("SEARCH_CODE");
                }
            }
        }
        private string _VAT_NO;
        public string VAT_NO
        {
            get
            {
                return SelectedSupplier.VAT_NO;
            }
            set
            {
                SelectedSupplier.VAT_NO = value;


                if (SelectedSupplier.VAT_NO != value)
                {
                    SelectedSupplier.VAT_NO = value;
                    OnPropertyChanged("VAT_NO");
                }
            }
        }
        private string _CST_NO;
        public string CST_NO
        {
            get
            {
                return SelectedSupplier.CST_NO;
            }
            set
            {
                SelectedSupplier.CST_NO = value;


                if (SelectedSupplier.CST_NO != value)
                {
                    SelectedSupplier.CST_NO = value;
                    OnPropertyChanged("CST_NO");
                }
            }
        }
        private string _TIN_NO;
        public string TIN_NO
        {
            get
            {
                return SelectedSupplier.TIN_NO;
            }
            set
            {
                SelectedSupplier.TIN_NO = value;


                if (SelectedSupplier.TIN_NO != value)
                {
                    SelectedSupplier.TIN_NO = value;
                    OnPropertyChanged("TIN_NO");
                }
            }
        }
        private string _PAN_NO;
        public string PAN_NO
        {
            get
            {
                return SelectedSupplier.PAN_NO;
            }
            set
            {
                SelectedSupplier.PAN_NO = value;


                if (SelectedSupplier.PAN_NO != value)
                {
                    SelectedSupplier.PAN_NO = value;
                    OnPropertyChanged("PAN_NO");
                }
            }
        }

        private string _ADDRESS_1;
        public string ADDRESS_1
        {
            get
            {
                return SelectedSupplier.ADDRESS_1;
            }
            set
            {
                SelectedSupplier.ADDRESS_1 = value;


                if (SelectedSupplier.ADDRESS_1 != value)
                {
                    SelectedSupplier.ADDRESS_1 = value;
                    OnPropertyChanged("ADDRESS_1");
                }
            }
        }
        private string _ADDRESS_2;
        public string ADDRESS_2
        {
            get
            {
                return SelectedSupplier.ADDRESS_2;
            }
            set
            {
                SelectedSupplier.ADDRESS_2 = value;


                if (SelectedSupplier.ADDRESS_2 != value)
                {
                    SelectedSupplier.ADDRESS_2 = value;
                    OnPropertyChanged("ADDRESS_2");
                }
            }
        }
        private string _CITY;
        public string CITY
        {
            get
            {
                return SelectedSupplier.CITY;
            }
            set
            {
                SelectedSupplier.CITY = value;


                if (SelectedSupplier.CITY != value)
                {
                    SelectedSupplier.CITY = value;
                    OnPropertyChanged("CITY");
                }
            }
        }
        private string _STATE;
        public string STATE
        {
            get
            {
                return SelectedSupplier.STATE;
            }
            set
            {
                SelectedSupplier.STATE = value;


                if (SelectedSupplier.STATE != value)
                {
                    SelectedSupplier.STATE = value;
                    OnPropertyChanged("STATE");
                }
            }
        }
        private string _ZIP_CODE;
        public string ZIP_CODE
        {
            get
            {
                return SelectedSupplier.ZIP_CODE;
            }
            set
            {
                SelectedSupplier.ZIP_CODE = value;


                if (SelectedSupplier.ZIP_CODE != value)
                {
                    SelectedSupplier.ZIP_CODE = value;
                    OnPropertyChanged("ZIP_CODE");
                }
            }
        }
        private decimal _OPENING_BALANCE;
        public decimal OPENING_BALANCE
        {
            get
            {
                return SelectedSupplier.OPENING_BALANCE;
            }
            set
            {
                SelectedSupplier.OPENING_BALANCE = value;


                if (SelectedSupplier.OPENING_BALANCE != value)
                {
                    SelectedSupplier.OPENING_BALANCE = value;
                    OnPropertyChanged("OPENING_BALANCE");
                }
            }
        }
        private bool _IS_PREFERRED_SUPPLIER;
        public bool IS_PREFERRED_SUPPLIER
        {
            get
            {
                return SelectedSupplier.IS_PREFERRED_SUPPLIER;
            }
            set
            {
                SelectedSupplier.IS_PREFERRED_SUPPLIER = value;


                if (SelectedSupplier.IS_PREFERRED_SUPPLIER != value)
                {
                    SelectedSupplier.IS_PREFERRED_SUPPLIER = value;
                    OnPropertyChanged("IS_PREFERRED_SUPPLIER");
                }
            }
        }
        private bool _IS_ACTIVE;
        public bool IS_ACTIVE
        {
            get
            {
                return SelectedSupplier.IS_ACTIVE;
            }
            set
            {
                SelectedSupplier.IS_ACTIVE = value;


                if (SelectedSupplier.IS_ACTIVE != value)
                {
                    SelectedSupplier.IS_ACTIVE = value;
                    OnPropertyChanged("IS_ACTIVE");
                }
            }
        }
        private int _PAYMENT_SETTLED;
        public int PAYMENT_SETTLED
        {
            get
            {
                return SelectedSupplier.PAYMENT_SETTLED;
            }
            set
            {
                SelectedSupplier.PAYMENT_SETTLED = value;


                if (SelectedSupplier.PAYMENT_SETTLED != value)
                {
                    SelectedSupplier.PAYMENT_SETTLED = value;
                    OnPropertyChanged("PAYMENT_SETTLED");
                }
            }
        }
        private string _IMAGE_PATH;
        public string IMAGE_PATH
        {
            get
            {
                return SelectedSupplier.IMAGE_PATH;
            }
            set
            {
                SelectedSupplier.IMAGE_PATH = value;


                if (SelectedSupplier.IMAGE_PATH != value)
                {
                    SelectedSupplier.IMAGE_PATH = value;
                    OnPropertyChanged("IMAGE_PATH");
                }
            }
        }
        private string _CONTACT_FRIST_NAME;
        public string CONTACT_FRIST_NAME
        {
            get
            {
                return SelectedSupplier.CONTACT_FRIST_NAME;
            }
            set
            {
                SelectedSupplier.CONTACT_FRIST_NAME = value;


                if (SelectedSupplier.CONTACT_FRIST_NAME != value)
                {
                    SelectedSupplier.CONTACT_FRIST_NAME = value;
                    OnPropertyChanged("CONTACT_FRIST_NAME");
                }
            }
        }
        private string _CONTACT_LAST_NAME;
        public string CONTACT_LAST_NAME
        {
            get
            {
                return SelectedSupplier.CONTACT_LAST_NAME;
            }
            set
            {
                SelectedSupplier.CONTACT_LAST_NAME = value;


                if (SelectedSupplier.CONTACT_LAST_NAME != value)
                {
                    SelectedSupplier.CONTACT_LAST_NAME = value;
                    OnPropertyChanged("CONTACT_LAST_NAME");
                }
            }
        }
        private string _CONTACT_TELEPHONE_NO;
        public string CONTACT_TELEPHONE_NO
        {
            get
            {
                return SelectedSupplier.CONTACT_TELEPHONE_NO;
            }
            set
            {
                SelectedSupplier.CONTACT_TELEPHONE_NO = value;


                if (SelectedSupplier.CONTACT_TELEPHONE_NO != value)
                {
                    SelectedSupplier.CONTACT_TELEPHONE_NO = value;
                    OnPropertyChanged("CONTACT_TELEPHONE_NO");
                }
            }
        }
        private string _CONTACT_FAX_NO;
        public string CONTACT_FAX_NO
        {
            get
            {
                return SelectedSupplier.CONTACT_FAX_NO;
            }
            set
            {
                SelectedSupplier.CONTACT_FAX_NO = value;


                if (SelectedSupplier.CONTACT_FAX_NO != value)
                {
                    SelectedSupplier.CONTACT_FAX_NO = value;
                    OnPropertyChanged("CONTACT_FAX_NO");
                }
            }
        }
        private string _CONTACT_MOBILE_NO;
        public string CONTACT_MOBILE_NO
        {
            get
            {
                return SelectedSupplier.CONTACT_MOBILE_NO;
            }
            set
            {
                SelectedSupplier.CONTACT_MOBILE_NO = value;


                if (SelectedSupplier.CONTACT_MOBILE_NO != value)
                {
                    SelectedSupplier.CONTACT_MOBILE_NO = value;
                    OnPropertyChanged("CONTACT_MOBILE_NO");
                }
            }
        }
        private string _CONTACT_WEBSITE;
        public string CONTACT_WEBSITE
        {
            get
            {
                return SelectedSupplier.CONTACT_WEBSITE;
            }
            set
            {
                SelectedSupplier.CONTACT_WEBSITE = value;


                if (SelectedSupplier.CONTACT_WEBSITE != value)
                {
                    SelectedSupplier.CONTACT_WEBSITE = value;
                    OnPropertyChanged("CONTACT_WEBSITE");
                }
            }
        }
        private string _CONTACT_EMAIL;
        public string CONTACT_EMAIL
        {
            get
            {
                return SelectedSupplier.CONTACT_EMAIL;
            }
            set
            {
                SelectedSupplier.CONTACT_EMAIL = value;


                if (SelectedSupplier.CONTACT_EMAIL != value)
                {
                    SelectedSupplier.CONTACT_EMAIL = value;
                    OnPropertyChanged("CONTACT_EMAIL");
                }
            }
        }


        private Int32 _UserId;
        public Int32 UserId
        {
            get { return _UserId; }
            set
            {
                if (value != _UserId)
                {
                    _UserId = value;
                    OnPropertyChanged("UserId");
                }
            }
        }

        public ICommand _InsertSupplier { get; set; }
        public ICommand InsertSupplier
        {
            get
            {
                if (_InsertSupplier == null)
                {

                    _InsertSupplier = new DelegateCommand(Add_Supplier);
                }
                return _InsertSupplier;
            }
        }
        public List<SupplierModel> _ListGrid { get; set; }
        public List<SupplierModel> ListGrid
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

        public ICommand _SuppPayAdd1 { get; set; }
        public ICommand SuppPayAdd1
        {
            get
            {
                if (_SuppPayAdd1 == null)
                {

                    _SuppPayAdd1 = new DelegateCommand(SuppAdd_Click);
                }
                return _SuppPayAdd1;
            }
        }
        public void SuppAdd_Click()
        {
            SupplierAdd SA = new SupplierAdd();
            SA.Show();

            // ModalService.NavigateTo(new SupplierAdd(), delegate(bool returnValue) { });

        }

        public ICommand _Cancel;
        public ICommand Cancel
        {
            get
            {
                if (_Cancel == null)
                {
                    _Cancel = new DelegateCommand(Cancel_Supplier);
                }
                return _Cancel;
            }
        }



        public void Cancel_Supplier()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
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

        public async void Add_Supplier()
        {
            if (SelectedSupplier.SUPPLIER_NAME == "" || SelectedSupplier.SUPPLIER_NAME == null)
            {
                MessageBox.Show("SUPPLIER NAME Should not be Blank");
            }
            else if (SelectedSupplier.SEARCH_CODE == "" || SelectedSupplier.SEARCH_CODE == null)
            {
                MessageBox.Show("SEARCH CODE Should not be Blank");
            }
            //else if (SelectedSupplier.VAT_NO == "" || SelectedSupplier.VAT_NO == null)
            //{
            //    MessageBox.Show("VAT NO Should not be Blank");
            //}
            //else if (SelectedSupplier.CST_NO == "" || SelectedSupplier.CST_NO == null)
            //{
            //    MessageBox.Show("CST NO Should not be Blank");
            //}
            //else if (SelectedSupplier.TIN_NO == "" || SelectedSupplier.TIN_NO == null)
            //{

            //}
            //else if (SelectedSupplier.PAN_NO == "" || SelectedSupplier.PAN_NO == null)
            //{

            //}
            else if (SelectedSupplier.ADDRESS_1 == "" || SelectedSupplier.ADDRESS_1 == null)
            {
                MessageBox.Show("ADDRESS 1 Should not be Blank");
            }
            else if (SelectedSupplier.ADDRESS_2 == "" || SelectedSupplier.ADDRESS_2 == null)
            {
                MessageBox.Show("ADDRESS 2 Should not be Blank");
            }
            else if (SelectedSupplier.CITY == "" || SelectedSupplier.CITY == null)
            {
                MessageBox.Show("CITY Should not be Blank");
            }
            else if (SelectedSupplier.STATE == "" || SelectedSupplier.STATE == null)
            {
                MessageBox.Show("STATE Should not be Blank");
            }
            else if (SelectedSupplier.ZIP_CODE == "" || SelectedSupplier.ZIP_CODE == null)
            {
                MessageBox.Show("ZIP CODE Should not be Blank");
            }
            else if (SelectedSupplier.CONTACT_FRIST_NAME == "" || SelectedSupplier.CONTACT_FRIST_NAME == null)
            {
                MessageBox.Show("CONTACT FRIST NAME Should not be Blank");
            }
            else if (SelectedSupplier.CONTACT_FAX_NO == "" || SelectedSupplier.CONTACT_FAX_NO == null)
            {
                MessageBox.Show("CONTACT FAX NO Should not be Blank");
            }
            else if (SelectedSupplier.CONTACT_TELEPHONE_NO == "" || SelectedSupplier.CONTACT_TELEPHONE_NO == null)
            {
                MessageBox.Show("CONTACT TELEPHONE NO Should not be Blank");
            }
            else if (SelectedSupplier.CONTACT_MOBILE_NO == "" || SelectedSupplier.CONTACT_MOBILE_NO == null)
            {
                MessageBox.Show("CONTACT MOBILE NO Should not be Blank");
            }
            else if (SelectedSupplier.CONTACT_WEBSITE == "" || SelectedSupplier.CONTACT_WEBSITE == null)
            {
                MessageBox.Show("CONTACT WEBSITE Should not be Blank");
            }
            else if (SelectedSupplier.CONTACT_EMAIL == "" || SelectedSupplier.CONTACT_EMAIL == null)
            {
                MessageBox.Show("Email is not blank");
            }

            else if (!Regex.IsMatch(SelectedSupplier.CONTACT_EMAIL,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                MessageBox.Show("Please check Email format");
                return;
            }
            else
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                SelectedSupplier.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                var response = await client.PostAsJsonAsync("api/SupplierAPI/SupplierAdd/", SelectedSupplier);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Supplier Insert Successfully");
                    Cancel_Supplier();
                    ModalService.NavigateTo(new SupplierList(), delegate (bool returnValue) { });
                }
            }

        }
        public ICommand _UpdateSupplier { get; set; }
        public ICommand UpdateSupplier
        {
            get
            {
                if (_UpdateSupplier == null)
                {

                    _UpdateSupplier = new DelegateCommand(Update_Supplier);
                }
                return _UpdateSupplier;
            }
        }
        public async void Update_Supplier()
        {

            if (SelectedSupplier.CONTACT_EMAIL == "" || SelectedSupplier.CONTACT_EMAIL == null)
            {
                MessageBox.Show("Email is not blank");
            }

            else if (!Regex.IsMatch(SelectedSupplier.CONTACT_EMAIL,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                MessageBox.Show("Please check Email format");
                return;
            }
            else
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                SelectedSupplier.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                var response = await client.PostAsJsonAsync("api/SupplierAPI/SupplierUpdate/", SelectedSupplier);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Supplier Updated Successfully");
                    Cancel_Supplier();
                    ModalService.NavigateTo(new SupplierList(), delegate (bool returnValue) { });
                }
            }

        }
        //public List<SupplierModel> _SupplierData;
        //public List<SupplierModel> SupplierData
        //{
        //    get { return _SupplierData; }
        //    set
        //    {
        //        if (_SupplierData != value)
        //        {
        //            _SupplierData = value;
        //        }
        //    }

        //}
        public ICommand _EditSupplier { get; set; }
        public ICommand EditSupplier
        {
            get
            {
                if (_EditSupplier == null)
                {
                    _EditSupplier = new DelegateCommand(Edit_Supplier);
                }
                return _EditSupplier;
            }
        }
        public async void Edit_Supplier()
        {

            //string ftpServerIP = "54.70.197.231";
            //string ftpUserID = "suvendu";
            //string ftpPassword = "vpY9dNp3W9AqhjGy";

            string ftpServerIP = "115.115.196.30";
            string ftpUserID = "suvendu";
            string ftpPassword = "Passw0rd";

            string filename = "ftp://" + ftpServerIP + "//Upload//";
            if (SelectedSupplier.SUPPLIER_ID != null && SelectedSupplier.SUPPLIER_ID != 0)
            {
                App.Current.Properties["Action"] = "Edit";
                SupplierData = new List<SupplierModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/SupplierAPI/EditSupplier?id=" + SelectedSupplier.SUPPLIER_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<SupplierModel[]>(await response.Content.ReadAsStringAsync());

                    for (int i = 0; i < data.Length; i++)
                    {
                        SelectedSupplier.IMAGE_PATH = data[i].IMAGE_PATH;
                        //SelectedSupplier.SUPPLIER_CODE = data[i].SUPPLIER_CODE;
                        SelectedSupplier.SUPPLIER_CODE = data[i].SUPPLIER_CODE;
                        SelectedSupplier.SUPPLIER_NAME = data[i].SUPPLIER_NAME;
                        SelectedSupplier.SEARCH_CODE = data[i].SEARCH_CODE;
                        SelectedSupplier.VAT_NO = data[i].VAT_NO;
                        SelectedSupplier.CST_NO = data[i].CST_NO;
                        SelectedSupplier.TIN_NO = data[i].TIN_NO;
                        SelectedSupplier.PAN_NO = data[i].PAN_NO;
                        SelectedSupplier.ADDRESS_1 = data[i].ADDRESS_1;
                        SelectedSupplier.ADDRESS_2 = data[i].ADDRESS_2;
                        SelectedSupplier.CITY = data[i].CITY;
                        SelectedSupplier.STATE = data[i].STATE;
                        SelectedSupplier.ZIP_CODE = data[i].ZIP_CODE;
                        SelectedSupplier.OPENING_BALANCE = data[i].OPENING_BALANCE;
                        SelectedSupplier.IS_PREFERRED_SUPPLIER = data[i].IS_PREFERRED_SUPPLIER;
                        SelectedSupplier.IS_ACTIVE = data[i].IS_ACTIVE;
                        SelectedSupplier.PAYMENT_SETTLED = data[i].PAYMENT_SETTLED;
                        SelectedSupplier.IMAGE_PATH = data[i].IMAGE_PATH;
                        SelectedSupplier.COMPANY_ID = data[i].COMPANY_ID;
                        SelectedSupplier.SUPPLIER_ID = data[i].SUPPLIER_ID;
                        SelectedSupplier.CONTACT_FRIST_NAME = data[i].CONTACT_FRIST_NAME;
                        SelectedSupplier.CONTACT_LAST_NAME = data[i].CONTACT_LAST_NAME;
                        SelectedSupplier.CONTACT_TELEPHONE_NO = data[i].CONTACT_TELEPHONE_NO;
                        SelectedSupplier.CONTACT_FAX_NO = data[i].CONTACT_FAX_NO;
                        SelectedSupplier.CONTACT_MOBILE_NO = data[i].CONTACT_MOBILE_NO;
                        SelectedSupplier.CONTACT_WEBSITE = data[i].CONTACT_WEBSITE;
                        SelectedSupplier.CONTACT_EMAIL = data[i].CONTACT_EMAIL;

                        var applicationPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                        var dir = new System.IO.DirectoryInfo(System.IO.Path.Combine(applicationPath, "uploaded"));
                        if (data[i].IMAGE_PATH != null)
                        {
                            var fr = filename + data[i].IMAGE_PATH;

                            var imageFile = new System.IO.FileInfo(data[i].IMAGE_PATH);
                            string file = imageFile.Name;

                            if (!dir.Exists)
                                dir.Create();
                            // Copy file to your folder
                            //imageFile.CopyTo(System.IO.Path.Combine(dir.FullName, file), true);
                            string path1 = System.IO.Path.Combine(dir.FullName + "\\", file);

                            FtpDown(path1, file);

                            //if (path1 == file)
                            //{
                            IMAGE_PATH1 = new BitmapImage(new Uri(path1));
                            SelectedSupplier.IMAGE_PATH = file;
                            //}
                            //else
                            //    MessageBox.Show("No Image Found");

                        }
                        else
                        {
                            var imageFile = new System.IO.FileInfo("NoImage.jpg");
                            string file = imageFile.Name;
                            string path1 = System.IO.Path.Combine(dir.FullName + "\\", file);
                            IMAGE_PATH1 = new BitmapImage(new Uri(path1));
                        }
                        App.Current.Properties["ItemViewImg"] = IMAGE_PATH1;
                        //App.Current.Properties["TaxListsession"] = "";


                    }
                }
                App.Current.Properties["SupplierEdit"] = SelectedSupplier;

                SupplierAdd SA = new SupplierAdd();
                //SA.Show();

                //if (SupplierAdd.ImageReff.Source != null)
                //{
                SA.ShowDialog();
                //}
                // ModalService.NavigateTo(new SupplierAdd(), delegate(bool returnValue) { });
            }
            else
            {
                MessageBox.Show("Select Item first", "Item Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

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
            SelectedPO.SUPPLIER = SelectedSupplier.SUPPLIER_NAME;
            SelectedPO.SUPPLIER_EMAIL = SelectedSupplier.CONTACT_EMAIL;
            if (fdrt != null)
            {
                SelectedOpeningStock.GODOWN = fdrt.GODOWN;
                SelectedOpeningStock.COMPANY_NAME = fdrt.COMPANY_NAME;

            }
            App.Current.Properties["OppingDiffProperties"] = SelectedOpeningStock;
            if (Openingbalance.BussRef != null)
            {
                Openingbalance.BussRef.Text = null;
                Openingbalance.BussRef.Text = SelectedOpeningStock.BUSINESS_LOC;
            }


            if (App.Current.Properties["SupplierSupItem"] != null)
            {
               
                App.Current.Properties["SupplierId"] = SelectedSupplier.SUPPLIER_ID;
                App.Current.Properties["GEtSupplierName"] = SelectedSupplier.SUPPLIER_NAME;
                SuppPayAdd.SuppRef.Text = null;
                SuppPayAdd.SuppRef.Text = SelectedSupplier.SUPPLIER_NAME;
                SuppPayAdd.SuppEmail.Text = SelectedSupplier.CONTACT_EMAIL;
                SuppPayAdd.SuppSMS.Text = SelectedSupplier.CONTACT_MOBILE_NO;

                //SuppPayViewModel SP = new SuppPayViewModel();
                //SP.GetSupplierList();
                App.Current.Properties["SupplierSupItem"] = null;
            }

            //if (AddPO.BussRef != null)
            //{
            //    AddPO.BussRef.Text = null;
            //    AddPO.BussRef.Text = SelectedOpeningStock.BUSINESS_LOC;
            //}
            //if (AddPO.ItemRef != null)
            //{
            //    AddPO.ItemRef.Text = null;
            //    AddPO.ItemRef.Text = SelectedOpeningStock.ITEM_NAME;
            //}
            if (AddPO.SuppRef != null)
            {
                AddPO.SuppRef.Text = null;
                AddPO.SuppRef.Text = SelectedPO.SUPPLIER;
                SuppPayAdd.SuppEmail.Text = SelectedSupplier.CONTACT_EMAIL;
                App.Current.Properties["AddPOsupplier"] = SelectedPO.SUPPLIER;


            }

            if (AddPO.SuppEmailRef != null)
            {
                AddPO.SuppEmailRef.Text = null;
                AddPO.SuppEmailRef.Text = SelectedPO.SUPPLIER_EMAIL;
                App.Current.Properties["AddPOsuppEmail"] = SelectedPO.SUPPLIER_EMAIL;

            }
            //if (SuppPayAdd.SuppRef != null)
            //{
            //    SuppPayAdd.SuppRef.Text = null;
            //    SuppPayAdd.SuppRef.Text = SelectedPO.SUPPLIER;
            //}
            if (AddStockTransfer.SuppRef != null)
            {
                AddStockTransfer.SuppRef.Text = null;
                AddStockTransfer.SuppRef.Text = SelectedPO.SUPPLIER;
                AddStockTransfer.stockEmail.Text = SelectedPO.SUPPLIER_EMAIL;
            }
            if (Window_Opening_stock.GodownRef != null)
            {
                Window_Opening_stock.GodownRef.Text = null;
                Window_Opening_stock.GodownRef.Text = Convert.ToString(SelectedOpeningStock.GODOWN);
            }

            if (App.Current.Properties["SupplierRecItem"] != null)
            {
                ReceiveAddItem.SupplierReff.Text = null;
                ReceiveAddItem.SupplierReff.Text = SelectedSupplier.SUPPLIER_NAME;
                App.Current.Properties["RecItemSupplier"] = SelectedSupplier.SUPPLIER_NAME;
                App.Current.Properties["RecItemSupplierId"] = SelectedSupplier.SUPPLIER_ID;
                App.Current.Properties["SupplierRecItem"] = null;
            }
            Cancel_Supplier();
        }
        public ICommand _ViewSupplier { get; set; }
        public ICommand ViewSupplier
        {
            get
            {
                if (_ViewSupplier == null)
                {
                    _ViewSupplier = new DelegateCommand(View_Supplier);
                }
                return _ViewSupplier;
            }
        }
        public async void View_Supplier()
        {

            if (SelectedSupplier.SUPPLIER_ID != null && SelectedSupplier.SUPPLIER_ID != 0)
            {

                //string ftpServerIP = "115.115.196.30";
                //string ftpUserID = "suvendu";
                //string ftpPassword = "Passw0rd";


                string ftpServerIP = "54.70.197.231";
                string ftpUserID = "suvendu";
                string ftpPassword = "vpY9dNp3W9AqhjGy";

                string filename = "ftp://" + ftpServerIP + "//Upload//";


                App.Current.Properties["Action"] = "View";
                SupplierData = new List<SupplierModel>();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/SupplierAPI/EditSupplier?id=" + SelectedSupplier.SUPPLIER_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<SupplierModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {

                        for (int i = 0; i < data.Length; i++)
                        {

                            SelectedSupplier.SUPPLIER_CODE = data[i].SUPPLIER_CODE;
                            SelectedSupplier.SUPPLIER_NAME = data[i].SUPPLIER_NAME;
                            SelectedSupplier.SEARCH_CODE = data[i].SEARCH_CODE;
                            SelectedSupplier.VAT_NO = data[i].VAT_NO;
                            SelectedSupplier.CST_NO = data[i].CST_NO;
                            SelectedSupplier.TIN_NO = data[i].TIN_NO;
                            SelectedSupplier.PAN_NO = data[i].PAN_NO;
                            SelectedSupplier.ADDRESS_1 = data[i].ADDRESS_1;
                            SelectedSupplier.ADDRESS_2 = data[i].ADDRESS_2;
                            SelectedSupplier.CITY = data[i].CITY;
                            SelectedSupplier.STATE = data[i].STATE;
                            SelectedSupplier.ZIP_CODE = data[i].ZIP_CODE;
                            SelectedSupplier.OPENING_BALANCE = data[i].OPENING_BALANCE;
                            SelectedSupplier.IS_PREFERRED_SUPPLIER = data[i].IS_PREFERRED_SUPPLIER;
                            SelectedSupplier.IS_ACTIVE = data[i].IS_ACTIVE;
                            SelectedSupplier.PAYMENT_SETTLED = data[i].PAYMENT_SETTLED;
                            SelectedSupplier.IMAGE_PATH = data[i].IMAGE_PATH;
                            SelectedSupplier.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedSupplier.SUPPLIER_ID = data[i].SUPPLIER_ID;
                            SelectedSupplier.CONTACT_FRIST_NAME = data[i].CONTACT_FRIST_NAME;
                            SelectedSupplier.CONTACT_LAST_NAME = data[i].CONTACT_LAST_NAME;
                            SelectedSupplier.CONTACT_TELEPHONE_NO = data[i].CONTACT_TELEPHONE_NO;
                            SelectedSupplier.CONTACT_FAX_NO = data[i].CONTACT_FAX_NO;
                            SelectedSupplier.CONTACT_MOBILE_NO = data[i].CONTACT_MOBILE_NO;
                            SelectedSupplier.CONTACT_WEBSITE = data[i].CONTACT_WEBSITE;
                            SelectedSupplier.CONTACT_EMAIL = data[i].CONTACT_EMAIL;
                            var applicationPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                            var dir = new System.IO.DirectoryInfo(System.IO.Path.Combine(applicationPath, "uploaded"));
                            if (data[i].IMAGE_PATH != null)
                            {

                                var fr = filename + data[i].IMAGE_PATH;

                                var imageFile = new System.IO.FileInfo(data[i].IMAGE_PATH);
                                string file = imageFile.Name;

                                // get your 'Uploaded' folder

                                if (!dir.Exists)
                                    dir.Create();
                                // Copy file to your folder
                                // imageFile.CopyTo(System.IO.Path.Combine(dir.FullName, file), true);
                                string path1 = System.IO.Path.Combine(dir.FullName + "\\", file);

                                FtpDown(path1, file);

                                IMAGE_PATH1 = new BitmapImage(new Uri(path1));
                                SelectedSupplier.IMAGE_PATH = file;
                            }
                            else
                            {
                                var imageFile = new System.IO.FileInfo("NoImage.jpg");
                                string file = imageFile.Name;
                                string path1 = System.IO.Path.Combine(dir.FullName + "\\", file);
                                IMAGE_PATH1 = new BitmapImage(new Uri(path1));
                            }
                            App.Current.Properties["ItemViewImg"] = IMAGE_PATH1;


                        }
                        App.Current.Properties["SupplierView"] = SelectedSupplier;
                        SupplierView SA = new SupplierView();
                        SA.Show();
                    }
                }

            }
            else
            {
                MessageBox.Show("Select Vendo first", "Vendor Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }



        }
        public async Task<ObservableCollection<SupplierModel>> GetSupplier(long CompId)
        {
            List<SupplierModel> _ListGrid_Temp = new List<SupplierModel>();
            SupplierData = new List<SupplierModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            HttpResponseMessage response = client.GetAsync("api/SupplierAPI/GetSupplier?id=" + CompId + "").Result;
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<SupplierModel[]>(await response.Content.ReadAsStringAsync());
                int x = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    x++;
                    _ListGrid_Temp.Add(new SupplierModel
                    {
                        SLNO = x,
                        SUPPLIER_CODE = data[i].SUPPLIER_CODE,
                        SUPPLIER_NAME = data[i].SUPPLIER_NAME,
                        DisplaySuppName = data[i].SUPPLIER_NAME,
                        SEARCH_CODE = data[i].SEARCH_CODE,
                        VAT_NO = data[i].VAT_NO,
                        CST_NO = data[i].CST_NO,
                        TIN_NO = data[i].TIN_NO,
                        PAN_NO = data[i].PAN_NO,
                        ADDRESS_1 = data[i].ADDRESS_1,
                        ADDRESS_2 = data[i].ADDRESS_2,
                        CITY = data[i].CITY,
                        STATE = data[i].STATE,
                        ZIP_CODE = data[i].ZIP_CODE,
                        OPENING_BALANCE = data[i].OPENING_BALANCE,
                        IS_PREFERRED_SUPPLIER = data[i].IS_PREFERRED_SUPPLIER,
                        IS_ACTIVE = data[i].IS_ACTIVE,
                        PAYMENT_SETTLED = data[i].PAYMENT_SETTLED,
                        IMAGE_PATH = data[i].IMAGE_PATH,
                        COMPANY_ID = data[i].COMPANY_ID,
                        SUPPLIER_ID = data[i].SUPPLIER_ID,
                        CONTACT_FRIST_NAME = data[i].CONTACT_FRIST_NAME,
                        CONTACT_LAST_NAME = data[i].CONTACT_LAST_NAME,
                        CONTACT_TELEPHONE_NO = data[i].CONTACT_TELEPHONE_NO,
                        CONTACT_FAX_NO = data[i].CONTACT_FAX_NO,
                        CONTACT_MOBILE_NO = data[i].CONTACT_MOBILE_NO,
                        CONTACT_WEBSITE = data[i].CONTACT_WEBSITE,
                        CONTACT_EMAIL = data[i].CONTACT_EMAIL,
                    });
                }
                if (SEARCH_SUPPLIER != "" && SEARCH_SUPPLIER != null)
                {
                    var item1 = (from m in _ListGrid_Temp where m.SUPPLIER_NAME.Contains(SEARCH_SUPPLIER) || m.SUPPLIER_CODE.Contains(SEARCH_SUPPLIER) || m.SEARCH_CODE.Contains(SEARCH_SUPPLIER) select m).ToList();
                    _ListGrid_Temp = item1;
                }
                if (IS_InACTIVESearch == true)
                {
                    var InActiveSupp = (from m in _ListGrid_Temp where m.IS_ACTIVE == true select m).ToList();
                    _ListGrid_Temp = InActiveSupp;
                }
            }
            ListGrid = _ListGrid_Temp;
            return new ObservableCollection<SupplierModel>(_ListGrid_Temp);

        }
        private List<SupplierModel> _SupplierData;
        public List<SupplierModel> SupplierData
        {
            get { return _SupplierData; }
            set
            {
                if (_SupplierData != value)
                {
                    _SupplierData = value;
                }
            }
        }


        //public string SEARCH_SUPPLIER
        //{
        //    get
        //    {
        //        return _SEARCH_SUPPLIER;
        //    }
        //    set
        //    {


        //        if (_SEARCH_SUPPLIER != value)
        //        {
        //            _SEARCH_SUPPLIER = value;

        //            if (_SEARCH_SUPPLIER != "" && _SEARCH_SUPPLIER != null)
        //            {

        //                GetSupplier(COMPANY_ID);
        //            }
        //            else
        //            {
        //                GetSupplier(COMPANY_ID);

        //            }
        //            OnPropertyChanged("SEARCH_SUPPLIER");
        //        }
        //    }
        //}
        private string _SEARCH_SUPPLIER;
        public string SEARCH_SUPPLIER
        {
            get
            {
                return _SEARCH_SUPPLIER;
            }
            set
            {
                _SEARCH_SUPPLIER = value;
                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                if (_SEARCH_SUPPLIER != null)
                {
                    GetSupplier(comp);
                }
                OnPropertyChanged("SEARCH_SUPPLIER");
            }
        }
        public ICommand _SearchSuup;
        public ICommand SearchSupp
        {
            get
            {
                if (_SearchSuup == null)
                {
                    _SearchSuup = new DelegateCommand(Search_Supp);
                }
                return _SearchSuup;
            }
        }
        public async void Search_Supp()
        {
            try
            {
                List<SupplierModel> _ListGrid_Temp = new List<SupplierModel>();
                App.Current.Properties["Action"] = "Search";
                string comp = SelectedSupplier.SEARCH_SUPPLIER;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/SupplierAPI/SearchSupplier?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<SupplierModel[]>(await response.Content.ReadAsStringAsync());


                    for (int i = 0; i < data.Length; i++)
                    {
                        _ListGrid_Temp.Add(new SupplierModel
                        {
                            SUPPLIER_CODE = data[i].SUPPLIER_CODE,
                            SUPPLIER_NAME = data[i].SUPPLIER_NAME,
                            SEARCH_CODE = data[i].SEARCH_CODE,
                            VAT_NO = data[i].VAT_NO,
                            CST_NO = data[i].CST_NO,
                            TIN_NO = data[i].TIN_NO,
                            PAN_NO = data[i].PAN_NO,
                            ADDRESS_1 = data[i].ADDRESS_1,
                            ADDRESS_2 = data[i].ADDRESS_2,
                            CITY = data[i].CITY,
                            STATE = data[i].STATE,
                            ZIP_CODE = data[i].ZIP_CODE,
                            OPENING_BALANCE = data[i].OPENING_BALANCE,
                            IS_PREFERRED_SUPPLIER = data[i].IS_PREFERRED_SUPPLIER,
                            IS_ACTIVE = data[i].IS_ACTIVE,
                            PAYMENT_SETTLED = data[i].PAYMENT_SETTLED,
                            IMAGE_PATH = data[i].IMAGE_PATH,
                            COMPANY_ID = data[i].COMPANY_ID,
                            SUPPLIER_ID = data[i].SUPPLIER_ID,
                            CONTACT_FRIST_NAME = data[i].CONTACT_FRIST_NAME,
                            CONTACT_LAST_NAME = data[i].CONTACT_LAST_NAME,
                            CONTACT_TELEPHONE_NO = data[i].CONTACT_TELEPHONE_NO,
                            CONTACT_FAX_NO = data[i].CONTACT_FAX_NO,
                            CONTACT_MOBILE_NO = data[i].CONTACT_MOBILE_NO,
                            CONTACT_WEBSITE = data[i].CONTACT_WEBSITE,
                            CONTACT_EMAIL = data[i].CONTACT_EMAIL,
                        });
                    }
                }
                ListGrid = _ListGrid_Temp;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //public async void SelectSupplier()
        //{

        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
        //    client.DefaultRequestHeaders.Accept.Add
        //        new MediaTypeWithQualityHeaderValue("(application/json"));
        //    response = client.GetAsync("api/SupplierAPI/GetSupplierList?id=" + SupplierId + "").Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        data = JsonConvert.DeserializeObject<Supplier[]>(await response.Content.ReadAsStringAsync());
        //        SupplierData = new List<Supplier>();
        //        for (int i = 0; i < data.Length; i++)
        //        {
        //            //int Vendor_Id = Convert.ToInt32(data[i].EMPLOYEE_ID);
        //            //string VendorName = data[i].FIRST_NAME;
        //            //string Address1 = data[i].ADDRESS_1;
        //            //string Address2 = data[i].ADDRESS_2;
        //            //string City = data[i].CITY;
        //            SupplierData.Add(new Supplier
        //            {


        //            });
        //        }
        //    }


        //}


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
        public ICommand _DeleteSupp { get; set; }
        public ICommand DeleteSupp
        {
            get
            {
                if (_DeleteSupp == null)
                {
                    _DeleteSupp = new DelegateCommand(Delete_Supp);
                }
                return _DeleteSupp;
            }
        }





        public async void Delete_Supp()
        {
            if (SelectedSupplier.SUPPLIER_ID != null && SelectedSupplier.SUPPLIER_ID != 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure Delete this Supplier " + SelectedSupplier.CONTACT_FRIST_NAME + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var id = SelectedSupplier.SUPPLIER_ID;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    HttpResponseMessage response = client.GetAsync("api/SupplierAPI/DeleteSupp?id=" + id + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {
                        MessageBox.Show("Supplier Deleted Successfully");
                        ModalService.NavigateTo(new SupplierList(), delegate (bool returnValue) { });

                    }
                }
                else
                {
                    Cancel_Supplier();
                }
            }
            else
            {
                MessageBox.Show("Select Vandor");
            }

        }
        public SupplierViewModel()
        {
            App.Current.Properties["IMG_HideShow"] = false;
            SelectedOpeningStock = new OpeningStockModel();
            SelectedPO = new POrderModel();
            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            if (App.Current.Properties["Action"].ToString() == "Edit")
            {
                CreatVisible = "Collapsed";
                UpdVisible = "Visible";
                SelectedSupplier = App.Current.Properties["SupplierEdit"] as SupplierModel;
                App.Current.Properties["Action"] = "";
            }
            else if (App.Current.Properties["Action"].ToString() == "View")
            {
                SelectedSupplier = App.Current.Properties["SupplierView"] as SupplierModel;
                App.Current.Properties["Action"] = "";
            }
            else if (App.Current.Properties["Action"].ToString() == "Search")
            {
                SelectedSupplier = App.Current.Properties["SearchSupp"] as SupplierModel;
                App.Current.Properties["Action"] = "";

            }
            else
            {
                CreatVisible = "Visible";
                UpdVisible = "Collapsed";
                SelectedSupplier = new SupplierModel();
                TXTOPN_BAL = "Collapsed";
                LBLOPN_BAL = "Visible";
                VisibleMyCode = "Visible";
                VisibleAutoCode = "Collapsed";
                GetSupplier(comp);
                GetSpplierNo();
            }

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
                HttpResponseMessage response = client.GetAsync("api/SupplierAPI/GetSupplierNo/").Result;
                if (response.IsSuccessStatusCode)
                {
                    //hy = Json.SerializeObject<int[]>(await response.Content.ReadAsStringAsync());
                    uhy = await response.Content.ReadAsStringAsync();
                    string dd = Convert.ToString(uhy);
                    string aaa = dd.Substring(3, 5);
                    int xx = Convert.ToInt32(aaa);
                    int noo = Convert.ToInt32(xx + 1);
                    nnnn = "S" + noo.ToString("D6");
                    SUPPLIER_CODE = nnnn;
                }

                else
                {
                    SUPPLIER_CODE = "S000001";
                }
            }
            catch (Exception ex)
            { }

            return uhy;
        }

        private bool _Supplier_Enable;
        public bool Supplier_Enable
        {

            get
            {
                return _Supplier_Enable;
            }
            set
            {

                _Supplier_Enable = value;
                OnPropertyChanged("Supplier_Enable");
            }
        }

        public ICommand _SupAutoCode;
        public ICommand SupAutoCode
        {
            get
            {
                if (_SupAutoCode == null)
                {
                    _SupAutoCode = new DelegateCommand(SupAutoCode_Click);
                }
                return _SupAutoCode;
            }
        }

        public void SupAutoCode_Click()
        {
            Supplier_Enable = true;
            VisibleMyCode = "Visible";
            VisibleAutoCode = "Collapsed";
            SUPPLIER_CODE = "";

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

        public void SupMyCode_Click()
        {
            Supplier_Enable = false;
            VisibleMyCode = "Collapsed";
            VisibleAutoCode = "Visible";
            GetSpplierNo();

        }

        public ICommand _OPNCHNG;
        public ICommand OPNCHNG
        {
            get
            {
                if (_OPNCHNG == null)
                {
                    _OPNCHNG = new DelegateCommand(OPNCHNG_Click);
                }
                return _OPNCHNG;
            }
        }
        public void OPNCHNG_Click()
        {
            TXTOPN_BAL = "Visible";
            LBLOPN_BAL = "Collapsed";
        }


        private string _VisibleMyCode;
        public string VisibleMyCode
        {

            get
            {
                return _VisibleMyCode;
            }
            set
            {

                _VisibleMyCode = value;
                OnPropertyChanged("VisibleMyCode");
            }
        }


        private string _VisibleAutoCode;
        public string VisibleAutoCode
        {

            get
            {
                return _VisibleAutoCode;
            }
            set
            {

                _VisibleAutoCode = value;
                OnPropertyChanged("VisibleAutoCode");
            }
        }


        private string _TXTOPN_BAL;
        public string TXTOPN_BAL
        {

            get
            {
                return _TXTOPN_BAL;
            }
            set
            {

                _TXTOPN_BAL = value;
                OnPropertyChanged("TXTOPN_BAL");
            }
        }

        private string _LBLOPN_BAL;
        public string LBLOPN_BAL
        {

            get
            {
                return _LBLOPN_BAL;
            }
            set
            {

                _LBLOPN_BAL = value;
                OnPropertyChanged("LBLOPN_BAL");
            }
        }

        #region Image Load/Remove
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

                    // Ftpup(path1, openFileDialog.SafeFileName);
                    SelectedSupplier.IMAGE_PATH = openFileDialog.SafeFileName;
                }


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
            //ImageReff
            //ImageReff = null;
            SupplierAdd.ImageReff.Source = null;
            SelectedSupplier.IMAGE_PATH = null;
        }

        #endregion

        #region FileUpload /DownLoad
        public static void Ftpup(string sourceFile, string targetFile)
        {
            try
            {
                //string ftpServerIP = "115.115.196.30";
                //string ftpUserID = "suvendu";
                //string ftpPassword = "Passw0rd";

                string ftpServerIP = "54.70.197.231";
                string ftpUserID = "suvendu";
                string ftpPassword = "vpY9dNp3W9AqhjGy";

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

        public static void FtpDown(string sourceFile, string targetFile)
        {
            try
            {
                //string ftpServerIP = "115.115.196.30";
                //string ftpUserID = "suvendu";
                //string ftpPassword = "Passw0rd";

                string ftpServerIP = "54.70.197.231";
                string ftpUserID = "suvendu";
                string ftpPassword = "vpY9dNp3W9AqhjGy";

                string filename = "ftp://" + ftpServerIP + "//Upload//" + targetFile;
                FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(filename);
                ftpReq.UseBinary = true;

                ftpReq.Method = WebRequestMethods.Ftp.DownloadFile;
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
                else
                    MessageBox.Show("no data");
            }
            catch (Exception ex)
            {
                string responseDesc = ex.ToString();
            }
        }
        #endregion

        public bool _IS_InACTIVESearch;
        public bool IS_InACTIVESearch
        {
            get
            {
                return _IS_InACTIVESearch;
            }
            set
            {
                _IS_InACTIVESearch = value;
                var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                if (_IS_InACTIVESearch == true)
                {
                    GetSupplier(comp);
                }
                else
                {
                    GetSupplier(comp);
                }
                OnPropertyChanged("IS_InACTIVESearch");
            }
        }
        private string _ExcelPath;
        public string ExcelPath
        {
            get { return _ExcelPath; }
            set
            {
                if (Equals(value, _ExcelPath)) return;
                _ExcelPath = value;
                OnPropertyChanged("ExcelPath");
            }
        }
        public ICommand _AttechmentClick { get; set; }
        public ICommand AttechmentClick
        {
            get
            {
                if (_AttechmentClick == null)
                {
                    _AttechmentClick = new DelegateCommand(Attechment_Click);
                }
                return _AttechmentClick;
            }
        }
        public void Attechment_Click()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel file(*.xlsm;*.xlsx;*.xlsx;*.xlt; )|*.xlsm;*.xlsx;*.xlsx;*.xlt;";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                string xx = openFileDialog.FileName;
                ExcelPath = openFileDialog.FileName;
            }

        }
        public ICommand _ExcelImportClick { get; set; }
        public ICommand ExcelImportClick
        {
            get
            {
                if (_ExcelImportClick == null)
                {
                    _ExcelImportClick = new DelegateCommand(ExcelImport_Click);
                }
                return _ExcelImportClick;
            }
        }
        public void ExcelImport_Click()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel file(*.xlsm;*.xlsx;*.xlsx;*.xlt; *.xlk;)|*.xlsm;*.xlsx;*.xlsx;*.xlt; *xlk";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
            }
            if (File.Exists(openFileDialog.FileName))
            {
                string xx = openFileDialog.FileName;
                ExcelPath = openFileDialog.FileName;
                var excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + xx + ";Extended Properties=Excel 12.0;");
                OleDbConnection objOlecon = new OleDbConnection();
                objOlecon.ConnectionString = excelConnectionString;
                objOlecon.Open();
                OleDbDataAdapter objOleDa = new OleDbDataAdapter("Select * from [Sheet1$]", objOlecon);
                DataTable objdt = new DataTable();
                objOleDa.Fill(objdt);
                List<SupplierModel> _ListGridTemp = new List<SupplierModel>();
                if (objdt.Rows.Count > 0)
                {
                    for (int i = 0; i < objdt.Rows.Count; i++)
                    {
                        var df = objdt.Rows[0];                        

                        _ListGridTemp.Add(new SupplierModel
                        {
                            SLNO = Convert.ToInt32(objdt.Rows[i].ItemArray[0]),
                            SUPPLIER_CODE = Convert.ToString(objdt.Rows[i].ItemArray[1]),
                            SUPPLIER_NAME = Convert.ToString(objdt.Rows[i].ItemArray[2]),
                            SEARCH_CODE = Convert.ToString(objdt.Rows[i].ItemArray[3]),
                            VAT_NO = Convert.ToString(objdt.Rows[i].ItemArray[4]),
                            CST_NO = Convert.ToString(objdt.Rows[i].ItemArray[5]),
                            TIN_NO = Convert.ToString(objdt.Rows[i].ItemArray[6]),
                            PAN_NO = Convert.ToString(objdt.Rows[i].ItemArray[7]),
                            ADDRESS_1 = Convert.ToString(objdt.Rows[i].ItemArray[8]),
                            ADDRESS_2 = Convert.ToString(objdt.Rows[i].ItemArray[9]),
                            CITY = Convert.ToString(objdt.Rows[i].ItemArray[10]),
                            STATE = Convert.ToString(objdt.Rows[i].ItemArray[11]),
                            ZIP_CODE = Convert.ToString(objdt.Rows[i].ItemArray[12]),
                            OPENING_BALANCE = Convert.ToInt32(objdt.Rows[i].ItemArray[13])
                        });
                    }


                }
                ListGrid = _ListGridTemp;
                App.Current.Properties["ExcelData"] = SelectedSupplier;

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
