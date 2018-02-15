using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.AccessRide;
using InvoicePOS.UserControll.Customer;
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

namespace InvoicePOS.ViewModels
{
    public class AccessRideViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        AccessRideModel[] data = null;
        List<DeptModel> data1 = null;
        List<DesgModel> datadeg = null;
        UserAccessModel[] data3 = null;
        List<AccessRideModel> _ListGrid_Temp = new List<AccessRideModel>();
        public AccessRideViewModel()
        {
            //var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            var comp = 1;
            SelectAccessRide = new AccessRideModel();
            SelectDeptModel = new List<DeptModel>();
            SelectDesgModel = new List<DesgModel>();
            if (App.Current.Properties["RightsAdd"] != null)
            {
                GetAccessRide(comp);
                App.Current.Properties["RightsAdd"] = null;
            }
            else
            {
                GetEmployee(comp);
            }
            

        }

        public List<AccessRideModel> _ListGrid { get; set; }
        public List<AccessRideModel> ListGrid
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
        public async Task<ObservableCollection<AccessRideModel>> GetEmployee(long comp)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("api/EmployeeAPI/EmployeeList?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<AccessRideModel[]>(await response.Content.ReadAsStringAsync());
                    // EmployeeData = new List<EmployeeModel>();
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;
                        _ListGrid_Temp.Add(new AccessRideModel
                        {
                            SLNO = x,
                            ADDRESS_1 = data[i].ADDRESS_1,
                            ADDRESS_2 = data[i].ADDRESS_2,
                            FIRST_NAME = data[i].FIRST_NAME,
                            CITY = data[i].CITY,
                            BUSINESS_LOCATION = data[i].BUSINESS_LOCATION,
                            COMMISSION_QUICK_POSITION = data[i].COMMISSION_QUICK_POSITION,
                            COMPANY_ID = data[i].COMPANY_ID,
                            DATE_OF_JOIN = data[i].DATE_OF_JOIN,
                            DEPARTMENT = data[i].DEPARTMENT,
                            DOB = data[i].DOB,
                            EMAIL = data[i].EMAIL,
                            EMPLOYEE_CODE = data[i].EMPLOYEE_CODE,
                            EMPLOYEE_DESIGNATION = data[i].EMPLOYEE_DESIGNATION,
                            EMPLOYEE_ID = data[i].EMPLOYEE_ID,
                            FAX_NO = data[i].IMAGE_PATH,
                            IS_APPOINTMENT = data[i].IS_APPOINTMENT,
                            IS_APPROVE_ACCESS_VAI_SMS = data[i].IS_APPROVE_ACCESS_VAI_SMS,
                            IS_ATTACHED_INVOICE = data[i].IS_ATTACHED_INVOICE,
                            GENDER = data[i].GENDER,
                            IMAGE_PATH = data[i].IMAGE_PATH,
                            IS_NOT_AN_EMPLOYEE = data[i].IS_NOT_AN_EMPLOYEE,
                            IS_REQUEST_VAI_SMS = data[i].IS_REQUEST_VAI_SMS,
                            LAST_NAME = data[i].LAST_NAME,
                            MARITAL_STATUS = data[i].MARITAL_STATUS,
                            MAX_SPOT_DISCOUNT = data[i].MAX_SPOT_DISCOUNT,
                            MIDDLE_NAME = data[i].MIDDLE_NAME,
                            MOBILE_NO = data[i].MOBILE_NO,
                            PIN_NO = data[i].PIN_NO,
                            SALES_PERCENT = data[i].SALES_PERCENT,
                            SEARCH_CODE = data[i].SEARCH_CODE,
                            STATE = data[i].STATE,
                            TELEPHONE_NO = data[i].TELEPHONE_NO,
                            WEBSITE = data[i].WEBSITE,
                            WORKING_SHIFT = data[i].WORKING_SHIFT,

                        });
                    }
                }
                ListGrid = _ListGrid_Temp;
               
                return new ObservableCollection<AccessRideModel>(_ListGrid_Temp);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        ObservableCollection<UserAccessModel> _ListGrid_Temp1 = new ObservableCollection<UserAccessModel>();
        public async Task<ObservableCollection<UserAccessModel>> GetAccessRide(long comp)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("api/AccessRightAPI/GetDepartment?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data1 = JsonConvert.DeserializeObject<List<DeptModel>>(await response.Content.ReadAsStringAsync());
                    for (int i = 0; i < data1.Count; i++)
                    {
                        SelectDeptModel.Add(new DeptModel
                        {

                            DEPARTMENT_ID = data1[i].DEPARTMENT_ID,
                            DEPARTMENT_NAME = data1[i].DEPARTMENT_NAME,

                        });

                    }
                }


                HttpResponseMessage response1 = client.GetAsync("api/AccessRightAPI/GetDesignation?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    datadeg = JsonConvert.DeserializeObject<List<DesgModel>>(await response1.Content.ReadAsStringAsync());
                    for (int i = 0; i < datadeg.Count; i++)
                    {
                        SelectDesgModel.Add(new DesgModel
                        {

                            DESIGNATION_ID = datadeg[i].DESIGNATION_ID,
                            DESIGNATION_NAME = datadeg[i].DESIGNATION_NAME,


                        });

                    }
                }

                long EId = Convert.ToInt64( App.Current.Properties["EmpId"].ToString());
                HttpResponseMessage response3 = client.GetAsync("api/AccessRightAPI/GetAccessRights?EId=" + EId + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data3 = JsonConvert.DeserializeObject<UserAccessModel[]>(await response3.Content.ReadAsStringAsync());
                    for (int i = 0; i < data3.Length; i++)
                    {
                        _ListGrid_Temp1.Add(new UserAccessModel
                        {
                            ACTION_CREATE = data3[i].ACTION_CREATE,
                            ACTION_DELETE = data3[i].ACTION_DELETE,
                            ACTION_VIEW = data3[i].ACTION_VIEW,
                            APPROVE = data3[i].APPROVE,
                            Company_Id = data3[i].Company_Id,
                            EDIT = data3[i].EDIT,
                            EXPT_TO_EXCEL = data3[i].EXPT_TO_EXCEL,
                            ID = data3[i].ID,
                            IMORT_TO_EXCEL = data3[i].IMORT_TO_EXCEL,
                            MAILBACK = data3[i].MAILBACK,
                            MESSAGE = data3[i].MESSAGE,
                            MODULE_ID = data3[i].MODULE_ID,
                            MODULE_NAME = data3[i].MODULE_NAME,
                            NOTIFICATION = data3[i].NOTIFICATION,
                            ROLE_ID = data3[i].ROLE_ID,
                            User_Id = data3[i].User_Id,
                            VERIFICATION = data3[i].VERIFICATION,

                        });

                    }
                    ListGrid1 = _ListGrid_Temp1;
                }
                App.Current.Properties["DataGrid"] = _ListGrid_Temp1;
                return new ObservableCollection<UserAccessModel>(_ListGrid_Temp1);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public ObservableCollection<UserAccessModel> _ListGrid1 { get; set; }
        public ObservableCollection<UserAccessModel> ListGrid1
        {
            get
            {
                return _ListGrid1;
            }
            set
            {
                this._ListGrid1 = value;
                OnPropertyChanged("ListGrid1");
            }
        }

        

            public ICommand _AccessUser;
        public ICommand AccessUser
        {
            get
            {
                if (_AccessUser == null)
                {
                    _AccessUser = new DelegateCommand(AccessUser_Save);
                }
                return _AccessUser;
            }
        }
        public void AccessUser_Save()
        {


        }


        public ICommand _AddAccessRightsClick;
        public ICommand AddAccessRightsClick
        {
            get
            {
                if (_AddAccessRightsClick == null)
                {
                    _AddAccessRightsClick = new DelegateCommand(AddAccess_RightsClick);
                }
                return _AddAccessRightsClick;
            }
        }

        public void AddAccess_RightsClick()
        {
            if (SelectAccessRide.EMPLOYEE_ID != null && SelectAccessRide.EMPLOYEE_ID != 0)
            {
                App.Current.Properties["RightsAdd"] = 1;
                App.Current.Properties["EmpId"] = SelectAccessRide.EMPLOYEE_ID;
                AccessRightList IA = new AccessRightList();
                IA.Show();
                AccessRightList.EmpNameReff.Text = "";
                AccessRightList.EmpNameReff.Text =Convert.ToString(SelectAccessRide.FIRST_NAME);
            }
            else
            {
                MessageBox.Show("Select Employee");
            }
            

        }


        private bool _ACTION_CREATE;

        public bool ACTION_CREATE
        {
            get
            {
                return _ACTION_CREATE;
            }
            set
            {
                _ACTION_CREATE = value;
                OnPropertyChanged("ACTION_CREATE"); 
            }
        }

        private long _COMAPNY_ID;
        public long COMPANY_ID
        {
            get
            {
                return SelectAccessRide.COMPANY_ID;
            }
            set
            {
                SelectAccessRide.COMPANY_ID = value;


                if (SelectAccessRide.COMPANY_ID != value)
                {
                    SelectAccessRide.COMPANY_ID = value;
                    OnPropertyChanged("COMPANY_ID");
                }
            }
        }


        private long _EMPLOYEE_ID;
        public long EMPLOYEE_ID
        {
            get
            {
                return SelectAccessRide.EMPLOYEE_ID;
            }
            set
            {
                SelectAccessRide.EMPLOYEE_ID = value;


                if (SelectAccessRide.EMPLOYEE_ID != value)
                {
                    SelectAccessRide.EMPLOYEE_ID = value;
                    OnPropertyChanged("EMPLOYEE_ID");
                }
            }
        }
        private string _EMPLOYEE_CODE;
        public string EMPLOYEE_CODE
        {
            get
            {
                return SelectAccessRide.EMPLOYEE_CODE;
            }
            set
            {
                SelectAccessRide.EMPLOYEE_CODE = value;


                if (SelectAccessRide.EMPLOYEE_CODE != value)
                {
                    SelectAccessRide.EMPLOYEE_CODE = value;
                    OnPropertyChanged("EMPLOYEE_CODE");
                }
            }
        }
        private string _FIRST_NAME;
        public string FIRST_NAME
        {
            get
            {
                return SelectAccessRide.FIRST_NAME;
            }
            set
            {
                SelectAccessRide.FIRST_NAME = value;


                if (SelectAccessRide.FIRST_NAME != value)
                {
                    SelectAccessRide.FIRST_NAME = value;
                    OnPropertyChanged("FIRST_NAME");
                }
            }
        }
        private decimal _SALES_PERCENT;
        public decimal SALES_PERCENT
        {
            get
            {
                return SelectAccessRide.SALES_PERCENT;
            }
            set
            {
                SelectAccessRide.SALES_PERCENT = value;


                if (SelectAccessRide.SALES_PERCENT != value)
                {
                    SelectAccessRide.SALES_PERCENT = value;
                    OnPropertyChanged("SALES_PERCENT");
                }
            }
        }

        private string _MIDDLE_NAME;
        public string MIDDLE_NAME
        {
            get
            {
                return SelectAccessRide.MIDDLE_NAME;
            }
            set
            {
                SelectAccessRide.MIDDLE_NAME = value;


                if (SelectAccessRide.MIDDLE_NAME != value)
                {
                    SelectAccessRide.MIDDLE_NAME = value;
                    OnPropertyChanged("MIDDLE_NAME");
                }
            }
        }
        private string _LAST_NAME;
        public string LAST_NAME
        {
            get
            {
                return SelectAccessRide.LAST_NAME;
            }
            set
            {
                SelectAccessRide.LAST_NAME = value;


                if (SelectAccessRide.LAST_NAME != value)
                {
                    SelectAccessRide.LAST_NAME = value;
                    OnPropertyChanged("LAST_NAME");
                }
            }
        }
        private string _SEARCH_CODE;
        public string SEARCH_CODE
        {
            get
            {
                return SelectAccessRide.EMPLOYEE_CODE;
            }
            set
            {
                SelectAccessRide.EMPLOYEE_CODE = value;


                if (SelectAccessRide.EMPLOYEE_CODE != value)
                {
                    SelectAccessRide.EMPLOYEE_CODE = value;
                    OnPropertyChanged("EMPLOYEE_CODE");
                }
            }
        }

        private DateTime _DOB;
        public DateTime DOB
        {
            get
            {
                return SelectAccessRide.DOB;
            }
            set
            {
                SelectAccessRide.DOB = value;


                if (SelectAccessRide.DOB != value)
                {
                    SelectAccessRide.DOB = value;
                    OnPropertyChanged("DOB");
                }
            }
        }
        private string _MARITAL_STATUS;
        public string MARITAL_STATUS
        {
            get
            {
                return SelectAccessRide.MARITAL_STATUS;
            }
            set
            {
                SelectAccessRide.MARITAL_STATUS = value;


                if (SelectAccessRide.MARITAL_STATUS != value)
                {
                    SelectAccessRide.MARITAL_STATUS = value;
                    OnPropertyChanged("MARITAL_STATUS");
                }
            }
        }
        private string _GENDER;
        public string GENDER
        {
            get
            {
                return SelectAccessRide.GENDER;
            }
            set
            {
                SelectAccessRide.GENDER = value;


                if (SelectAccessRide.GENDER != value)
                {
                    SelectAccessRide.GENDER = value;
                    OnPropertyChanged("GENDER");
                }
            }
        }
        private DateTime _DATE_OF_JOIN;
        public DateTime DATE_OF_JOIN
        {
            get
            {
                return SelectAccessRide.DATE_OF_JOIN;
            }
            set
            {
                SelectAccessRide.DATE_OF_JOIN = value;


                if (SelectAccessRide.DATE_OF_JOIN != value)
                {
                    SelectAccessRide.DATE_OF_JOIN = value;
                    OnPropertyChanged("DATE_OF_JOIN");
                }
            }
        }
        private string _BUSINESS_LOCATION;
        public string BUSINESS_LOCATION
        {
            get
            {
                return SelectAccessRide.BUSINESS_LOCATION;
            }
            set
            {
                SelectAccessRide.BUSINESS_LOCATION = value;


                if (SelectAccessRide.BUSINESS_LOCATION != value)
                {
                    SelectAccessRide.BUSINESS_LOCATION = value;
                    OnPropertyChanged("BUSINESS_LOCATION");
                }
            }
        }
        private string _DEPARTMENT;
        public string DEPARTMENT
        {
            get
            {
                return SelectAccessRide.DEPARTMENT;
            }
            set
            {
                SelectAccessRide.DEPARTMENT = value;


                if (SelectAccessRide.DEPARTMENT != value)
                {
                    SelectAccessRide.DEPARTMENT = value;
                    OnPropertyChanged("DEPARTMENT");
                }
            }
        }
        private string _EMPLOYEE_DESIGNATION;
        public string EMPLOYEE_DESIGNATION
        {
            get
            {
                return SelectAccessRide.EMPLOYEE_DESIGNATION;
            }
            set
            {
                SelectAccessRide.EMPLOYEE_DESIGNATION = value;


                if (SelectAccessRide.EMPLOYEE_DESIGNATION != value)
                {
                    SelectAccessRide.EMPLOYEE_DESIGNATION = value;
                    OnPropertyChanged("EMPLOYEE_DESIGNATION");
                }
            }
        }
        private bool _IS_REQUEST_VAI_SMS;
        public bool IS_REQUEST_VAI_SMS
        {
            get
            {
                return SelectAccessRide.IS_REQUEST_VAI_SMS;
            }
            set
            {
                SelectAccessRide.IS_REQUEST_VAI_SMS = value;


                if (SelectAccessRide.IS_REQUEST_VAI_SMS != value)
                {
                    SelectAccessRide.IS_REQUEST_VAI_SMS = value;
                    OnPropertyChanged("IS_REQUEST_VAI_SMS");
                }
            }
        }
        private bool _IS_APPROVE_ACCESS_VAI_SMS;
        public bool IS_APPROVE_ACCESS_VAI_SMS
        {
            get
            {
                return SelectAccessRide.IS_APPROVE_ACCESS_VAI_SMS;
            }
            set
            {
                SelectAccessRide.IS_APPROVE_ACCESS_VAI_SMS = value;


                if (SelectAccessRide.IS_APPROVE_ACCESS_VAI_SMS != value)
                {
                    SelectAccessRide.IS_APPROVE_ACCESS_VAI_SMS = value;
                    OnPropertyChanged("IS_APPROVE_ACCESS_VAI_SMS");
                }
            }
        }
        private bool _IS_NOT_AN_EMPLOYEE;
        public bool IS_NOT_AN_EMPLOYEE
        {
            get
            {
                return SelectAccessRide.IS_NOT_AN_EMPLOYEE;
            }
            set
            {
                SelectAccessRide.IS_NOT_AN_EMPLOYEE = value;


                if (SelectAccessRide.IS_NOT_AN_EMPLOYEE != value)
                {
                    SelectAccessRide.IS_NOT_AN_EMPLOYEE = value;
                    OnPropertyChanged("IS_NOT_AN_EMPLOYEE");
                }
            }
        }
        private bool _IS_APPOINTMENT;
        public bool IS_APPOINTMENT
        {
            get
            {
                return SelectAccessRide.IS_APPOINTMENT;
            }
            set
            {
                SelectAccessRide.IS_APPOINTMENT = value;


                if (SelectAccessRide.IS_APPOINTMENT != value)
                {
                    SelectAccessRide.IS_APPOINTMENT = value;
                    OnPropertyChanged("IS_APPOINTMENT");
                }
            }
        }
        private bool _IS_ATTACHED_INVOICE;
        public bool IS_ATTACHED_INVOICE
        {
            get
            {
                return SelectAccessRide.IS_ATTACHED_INVOICE;
            }
            set
            {
                SelectAccessRide.IS_ATTACHED_INVOICE = value;


                if (SelectAccessRide.IS_ATTACHED_INVOICE != value)
                {
                    SelectAccessRide.IS_ATTACHED_INVOICE = value;
                    OnPropertyChanged("IS_ATTACHED_INVOICE");
                }
            }
        }
        private string _WORKING_SHIFT;
        public string WORKING_SHIFT
        {
            get
            {
                return SelectAccessRide.WORKING_SHIFT;
            }
            set
            {
                SelectAccessRide.WORKING_SHIFT = value;


                if (SelectAccessRide.WORKING_SHIFT != value)
                {
                    SelectAccessRide.WORKING_SHIFT = value;
                    OnPropertyChanged("WORKING_SHIFT");
                }
            }
        }
        private string _ADDRESS_1;
        public string ADDRESS_1
        {
            get
            {
                return SelectAccessRide.ADDRESS_1;
            }
            set
            {
                SelectAccessRide.ADDRESS_1 = value;


                if (SelectAccessRide.ADDRESS_1 != value)
                {
                    SelectAccessRide.ADDRESS_1 = value;
                    OnPropertyChanged("ADDRESS_1");
                }
            }
        }
        private string _ADDRESS_2;
        public string ADDRESS_2
        {
            get
            {
                return SelectAccessRide.ADDRESS_2;
            }
            set
            {
                SelectAccessRide.ADDRESS_2 = value;


                if (SelectAccessRide.ADDRESS_2 != value)
                {
                    SelectAccessRide.ADDRESS_2 = value;
                    OnPropertyChanged("ADDRESS_2");
                }
            }
        }
        private string _CITY;
        public string CITY
        {
            get
            {
                return SelectAccessRide.CITY;
            }
            set
            {
                SelectAccessRide.CITY = value;


                if (SelectAccessRide.CITY != value)
                {
                    SelectAccessRide.CITY = value;
                    OnPropertyChanged("CITY");
                }
            }
        }
        private string _STATE;
        public string STATE
        {
            get
            {
                return SelectAccessRide.STATE;
            }
            set
            {
                SelectAccessRide.STATE = value;


                if (SelectAccessRide.STATE != value)
                {
                    SelectAccessRide.STATE = value;
                    OnPropertyChanged("STATE");
                }
            }
        }
        private decimal _MAX_SPOT_DISCOUNT;
        public decimal MAX_SPOT_DISCOUNT
        {
            get
            {
                return SelectAccessRide.MAX_SPOT_DISCOUNT;
            }
            set
            {
                SelectAccessRide.MAX_SPOT_DISCOUNT = value;
                if (SelectAccessRide.MAX_SPOT_DISCOUNT != value)
                {
                    SelectAccessRide.MAX_SPOT_DISCOUNT = value;
                    OnPropertyChanged("MAX_SPOT_DISCOUNT");
                }
            }
        }
        private string _PIN_NO;
        public string PIN_NO
        {
            get
            {
                return SelectAccessRide.PIN_NO;
            }
            set
            {
                SelectAccessRide.PIN_NO = value;


                if (SelectAccessRide.PIN_NO != value)
                {
                    SelectAccessRide.PIN_NO = value;
                    OnPropertyChanged("PIN_NO");
                }
            }
        }
        private string _TELEPHONE_NO;
        public string TELEPHONE_NO
        {
            get
            {
                return SelectAccessRide.TELEPHONE_NO;
            }
            set
            {
                SelectAccessRide.TELEPHONE_NO = value;


                if (SelectAccessRide.TELEPHONE_NO != value)
                {
                    SelectAccessRide.TELEPHONE_NO = value;
                    OnPropertyChanged("TELEPHONE_NO");
                }
            }
        }
        private string _FAX_NO;
        public string FAX_NO
        {
            get
            {
                return SelectAccessRide.FAX_NO;
            }
            set
            {
                SelectAccessRide.FAX_NO = value;


                if (SelectAccessRide.FAX_NO != value)
                {
                    SelectAccessRide.FAX_NO = value;
                    OnPropertyChanged("FAX_NO");
                }
            }
        }
        private string _MOBILE_NO;
        public string MOBILE_NO
        {
            get
            {
                return SelectAccessRide.MOBILE_NO;
            }
            set
            {
                SelectAccessRide.MOBILE_NO = value;


                if (SelectAccessRide.MOBILE_NO != value)
                {
                    SelectAccessRide.MOBILE_NO = value;
                    OnPropertyChanged("MOBILE_NO");
                }
            }
        }
        private string _EMAIL;
        public string EMAIL
        {
            get
            {
                return SelectAccessRide.EMAIL;
            }
            set
            {
                SelectAccessRide.EMAIL = value;


                if (SelectAccessRide.EMAIL != value)
                {
                    SelectAccessRide.EMAIL = value;
                    OnPropertyChanged("EMAIL");
                }
            }
        }
        private string _WEBSITE;
        public string WEBSITE
        {
            get
            {
                return SelectAccessRide.WEBSITE;
            }
            set
            {
                SelectAccessRide.WEBSITE = value;


                if (SelectAccessRide.WEBSITE != value)
                {
                    SelectAccessRide.WEBSITE = value;
                    OnPropertyChanged("WEBSITE");
                }
            }
        }
        private string _IMAGE_PATH;
        public string IMAGE_PATH
        {
            get
            {
                return SelectAccessRide.IMAGE_PATH;
            }
            set
            {
                SelectAccessRide.IMAGE_PATH = value;


                if (SelectAccessRide.IMAGE_PATH != value)
                {
                    SelectAccessRide.IMAGE_PATH = value;
                    OnPropertyChanged("IMAGE_PATH");
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
                    _Cancel = new DelegateCommand(Cancel_AccessRide);
                }
                return _Cancel;
            }
        }



        public void Cancel_AccessRide()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }

        private AccessRideModel _SelectAccessRide;
        public AccessRideModel SelectAccessRide
        {
            get { return _SelectAccessRide; }
            set
            {
                if (_SelectAccessRide != value)
                {
                    _SelectAccessRide = value;
                    OnPropertyChanged("SelectAccessRide");
                }

            }
        }

        private List<DeptModel> _SelectDeptModel;
        public List<DeptModel> SelectDeptModel
        {
            get { return _SelectDeptModel; }
            set
            {
                if (_SelectDeptModel != value)
                {
                    _SelectDeptModel = value;
                    OnPropertyChanged("SelectDeptModel");
                }

            }
        }

        private List<DesgModel> _SelectDesgModel;
        public List<DesgModel> SelectDesgModel
        {
            get { return _SelectDesgModel; }
            set
            {
                if (_SelectDesgModel != value)
                {
                    _SelectDesgModel = value;
                    OnPropertyChanged("SelectDesgModel");
                }

            }
        }
        #region 29.03.2017

        public ICommand _InsertAccess;
        public ICommand InsertAccess
        {
            get
            {
                if (_InsertAccess == null)
                {
                    _InsertAccess = new DelegateCommand(Insert_Access);
                }
                return _InsertAccess;
            }
        }
        public async void Insert_Access()
        {
            App.Current.Properties["Action"] = "Add";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsJsonAsync("api/AccessRightAPI/CreateAcessRight/", _Access_Temp);
            if (response.StatusCode.ToString() == "OK")
            {
                MessageBox.Show("Customer Insert Successfuly");
                Cancel_AccessRide();
                ModalService.NavigateTo(new AccessRide(), delegate(bool returnValue) { });
            }

        }

        ObservableCollection<UserAccessModel> _Access_Temp = new ObservableCollection<UserAccessModel>();



        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }
        
        private void Check(object sender)
        {
            CheckBox chk = sender as CheckBox;
            if (chk != null)
            {
                object item = chk.DataContext;
                UserAccessModel obj = new UserAccessModel();
                obj = (UserAccessModel)item;
                ObservableCollection<UserAccessModel> _ListGrid_Module = new ObservableCollection<UserAccessModel>();
                var Load_Item = (((IEnumerable)App.Current.Properties["DataGrid"]).Cast<UserAccessModel>()).ToList();

                foreach (var item2 in Load_Item)
                {


                    if (item2.MODULE_ID ==obj.MODULE_ID)
                    {
                        _ListGrid_Module.Add(new UserAccessModel
                        {
                            ACTION_CREATE =obj.ACTION_CREATE,
                            EDIT = obj.EDIT,
                            ACTION_VIEW = obj.ACTION_VIEW,
                            ACTION_DELETE = obj.ACTION_DELETE,
                            MODULE_ID = obj.MODULE_ID,
                            Company_Id = item2.Company_Id,
                            User_Id =Convert.ToInt64(App.Current.Properties["EmpId"].ToString())

                        });


                    }
                    else
                    {
                        _ListGrid_Module.Add(new UserAccessModel
                        {
                            ACTION_CREATE = item2.ACTION_CREATE,
                            EDIT = item2.EDIT,
                            ACTION_VIEW = item2.ACTION_VIEW,
                            ACTION_DELETE = item2.ACTION_DELETE,
                            MODULE_ID = item2.MODULE_ID,
                            Company_Id = item2.Company_Id,
                            User_Id = Convert.ToInt64(App.Current.Properties["EmpId"].ToString())
                        });

                    }


                    _Access_Temp = _ListGrid_Module;

                    App.Current.Properties["DataGrid"] = _ListGrid_Module;
                }
            }

        }
        private void UnCheck(object sender)
        {
            CheckBox chk = sender as CheckBox;
            if (chk != null)
            {
                object item = chk.DataContext;
                UserAccessModel obj = new UserAccessModel();
                obj = (UserAccessModel)item;

                ObservableCollection<UserAccessModel> _ListGrid_Module = new ObservableCollection<UserAccessModel>();
                // ObservableCollection<UserAccessModel> _ListGrid_Temp = new ObservableCollection<UserAccessModel>();
                var Load_Item = (((IEnumerable)App.Current.Properties["DataGrid"]).Cast<UserAccessModel>()).ToList();

                foreach (var item2 in Load_Item)
                {


                    if (item2.MODULE_ID == obj.MODULE_ID)
                    {
                        _ListGrid_Module.Add(new UserAccessModel
                        {
                            ACTION_CREATE = obj.ACTION_CREATE,
                            EDIT= obj.EDIT,
                            ACTION_VIEW = obj.ACTION_VIEW,
                            ACTION_DELETE = obj.ACTION_DELETE,
                            MODULE_ID= obj.MODULE_ID,
                            Company_Id=item2.Company_Id,
                            User_Id = Convert.ToInt64(App.Current.Properties["EmpId"].ToString())

                        });


                    }
                    else
                    {
                        _ListGrid_Module.Add(new UserAccessModel
                        {
                            ACTION_CREATE = item2.ACTION_CREATE,
                            EDIT = item2.EDIT,
                            ACTION_VIEW = item2.ACTION_VIEW,
                            ACTION_DELETE = item2.ACTION_DELETE,
                            MODULE_ID = item2.MODULE_ID,
                            Company_Id = item2.Company_Id,
                            User_Id = Convert.ToInt64(App.Current.Properties["EmpId"].ToString())
                        });

                    }


                    _Access_Temp = _ListGrid_Module;

                    App.Current.Properties["DataGrid"] = _ListGrid_Module;
                }
            }

        }
        private RelayCommand _CheckedCommand;
        public ICommand CheckedCommand
        {
            get
            {
                if (_CheckedCommand == null)
                {
                    _CheckedCommand = new RelayCommand(param => this.Check(param));
                }
                return _CheckedCommand;
            }

        }

        private RelayCommand _UnCheckedCommand;
        public ICommand UnCheckedCommand
        {
            get
            {
                if (_UnCheckedCommand == null)
                {
                    _UnCheckedCommand = new RelayCommand(param => this.UnCheck(param));
                }
                return _UnCheckedCommand;
            }

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
