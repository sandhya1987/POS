using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Company;
using InvoicePOS.UserControll.Department;
using InvoicePOS.UserControll.Designation;
using InvoicePOS.UserControll.Employee;
using InvoicePOS.UserControll.Item;
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
using InvoicePOS.UserControll.Item;

namespace InvoicePOS.ViewModels
{
    public class EmployeeViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {


        public HttpResponseMessage response;
        private readonly EmployeeModel EmpModel;
        EmployeeModel _EmpModel = new EmployeeModel();
        public ICommand select { get; set; }
        EmployeeModel[] data = null;

        List<EmployeeModel> _ListGrid_Temp = new List<EmployeeModel>();
        private EmployeeModel _SelectedEmployee;
        public EmployeeModel SelectedEmployee
        {
            get { return _SelectedEmployee; }
            set
            {
                if (_SelectedEmployee != value)
                {
                    _SelectedEmployee = value;
                    OnPropertyChanged("SelectedEmployee");
                }

            }
        }
        public ICommand _ListOfEmployee { get; set; }
        public ICommand ListOfEmployee
        {
            get
            {
                if (_ListOfEmployee == null)
                {
                    _ListOfEmployee = new DelegateCommand(ListOfEmployee_Click);
                }
                return _ListOfEmployee;
            }
        }
        public async void ListOfEmployee_Click()
        {
           
                App.Current.Properties["Action"] = "ListOfEmployee";
                ObservableCollection<EmployeeModel> _ListGrid_Temp = new ObservableCollection<EmployeeModel>();
                //var ItemName = SelectedItem.ITEM_NAME;
                //ListGridSales = new List<ItemModel>();
                ItemModel get = App.Current.Properties["GetItemDetails"] as ItemModel;
                int itemid = get.ITEM_ID;
                int companyid = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                //int invoiceid = SelectedItem.INVOICE_ID.Value;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/EmployeeAPI/EmployeeList?id=" + companyid + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<EmployeeModel[]>(await response.Content.ReadAsStringAsync());
                    //for (int i = 0; i < data.Length; i++)
                    //{
                    int total = 0;
                    int totalItem = 0;
                    int totalQty = 0;
                    foreach (var item in data)
                    {
                       
                        _ListGrid_Temp.Add(new EmployeeModel
                        {
                            EMPLOYEE_CODE = item.EMPLOYEE_CODE,
                            FIRST_NAME = item.FIRST_NAME,
                            SEARCH_CODE = item.SEARCH_CODE,
                            DATE_OF_JOIN = item.DATE_OF_JOIN,
                            DEPARTMENT = item.DEPARTMENT,
                            EMPLOYEE_DESIGNATION = item.EMPLOYEE_DESIGNATION,
                            BUSINESS_LOCATION = item.BUSINESS_LOCATION,
                            COMMISSION_QUICK_POSITION = item.COMMISSION_QUICK_POSITION,
                            IS_APPOINTMENT = item.IS_APPOINTMENT,
                            WORKING_SHIFT = item.WORKING_SHIFT
                        });

                    }

                        ListGridEmployee = _ListGrid_Temp;
                        App.Current.Properties["EmloyeeName"] = ListGridEmployee;
                    App.Current.Properties["ListGridEmloyee"] = _ListGrid_Temp;
                    SelectEmployee sh = new SelectEmployee();
                    sh.DataContext = this;
                    sh.Show();
                }

            
        }
        public ObservableCollection<EmployeeModel> _ListGridEmployee { get; set; }
        public ObservableCollection<EmployeeModel> ListGridEmployee
        {
            get
            {
                return _ListGridEmployee;
            }
            set
            {
                this._ListGridEmployee = value;
                OnPropertyChanged("ListGridEmployee");
            }
        }

        public ICommand _AddEmployeeClick { get; set; }
        public ICommand AddEmployeeClick
        {
            get
            {
                if (_AddEmployeeClick == null)
                {
                    _AddEmployeeClick = new DelegateCommand(AddEmployee_Click);
                }
                return _AddEmployeeClick;
            }
        }
        public void AddEmployee_Click()
        {
           
            EmployeeAdd _emp = new EmployeeAdd();
            _emp.Show();
           // ModalService.NavigateTo(new EmployeeAdd(), delegate(bool returnValue) { });

        }
        public ICommand _DeleteEmployeeClick { get; set; }
        public ICommand DeleteEmployeeClick
        {
            get
            {
                if (_DeleteEmployeeClick == null)
                {
                    _DeleteEmployeeClick = new DelegateCommand(DeleteEmployee_Click);
                }
                return _DeleteEmployeeClick;
            }
        }
        public void DeleteEmployee_Click()
        {
            if (SelectedEmployee.EMPLOYEE_ID != null && SelectedEmployee.EMPLOYEE_ID != 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure Delete this Employee " + SelectedEmployee.EMPLOYEE_CODE + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    response = client.GetAsync("api/EmployeeAPI/EmployeeDelete?id=" + SelectedEmployee.EMPLOYEE_ID + "").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Delete Employee ", "Employee Delete", MessageBoxButton.OK, MessageBoxImage.Error);
                        ModalService.NavigateTo(new Employee(), delegate(bool returnValue) { });
                    }
                }
                else
                {
                    Cancel_Emp();
                }
            }
            else
            {
                MessageBox.Show("Select Employee first", "Employee Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }

        public ICommand _UpdateInvoiceEmployee { get; set; }
        public ICommand UpdateInvoiceEmployee
        {
            get
            {
                if (_UpdateInvoiceEmployee == null)
                {
                    _UpdateInvoiceEmployee = new DelegateCommand(UpdateInvoiceEmployee_Click);
                }
                return _UpdateInvoiceEmployee;
            }
        }
        public async void UpdateInvoiceEmployee_Click() 
        {
            if (SelectedEmployee.EMPLOYEE_ID != null && SelectedEmployee.EMPLOYEE_ID != 0)
            {
                App.Current.Properties["Action"] = "Edit";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.GetAsync("api/EmployeeAPI/EmployeeEdit?id=" + SelectedEmployee.EMPLOYEE_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<EmployeeModel[]>(await response.Content.ReadAsStringAsync());
                    // EmployeeData = new List<EmployeeModel>();
                    for (int i = 0; i < data.Length; i++)
                    {
                        SelectedEmployee.ADDRESS_1 = data[i].ADDRESS_1;
                        SelectedEmployee.ADDRESS_2 = data[i].ADDRESS_2;
                        SelectedEmployee.FIRST_NAME = data[i].FIRST_NAME;
                        SelectedEmployee.CITY = data[i].CITY;
                        SelectedEmployee.BUSINESS_LOCATION = data[i].BUSINESS_LOCATION;
                        SelectedEmployee.COMMISSION_QUICK_POSITION = data[i].COMMISSION_QUICK_POSITION;
                        SelectedEmployee.COMPANY_ID = data[i].COMPANY_ID;
                        SelectedEmployee.DATE_OF_JOIN = data[i].DATE_OF_JOIN;
                        SelectedEmployee.DEPARTMENT = data[i].DEPARTMENT;
                        SelectedEmployee.DOB = data[i].DOB;
                        SelectedEmployee.EMAIL = data[i].EMAIL;
                        SelectedEmployee.EMPLOYEE_CODE = data[i].EMPLOYEE_CODE;
                        SelectedEmployee.EMPLOYEE_DESIGNATION = data[i].EMPLOYEE_DESIGNATION;
                        SelectedEmployee.EMPLOYEE_ID = data[i].EMPLOYEE_ID;
                        SelectedEmployee.FAX_NO = data[i].FAX_NO;
                        SelectedEmployee.IS_APPOINTMENT = data[i].IS_APPOINTMENT;
                        SelectedEmployee.IS_APPROVE_ACCESS_VAI_SMS = data[i].IS_APPROVE_ACCESS_VAI_SMS;
                        SelectedEmployee.IS_ATTACHED_INVOICE = data[i].IS_ATTACHED_INVOICE;
                        SelectedEmployee.GENDER = data[i].GENDER;
                        SelectedEmployee.IMAGE_PATH = data[i].IMAGE_PATH;
                        SelectedEmployee.IS_NOT_AN_EMPLOYEE = data[i].IS_NOT_AN_EMPLOYEE;
                        SelectedEmployee.IS_REQUEST_VAI_SMS = data[i].IS_REQUEST_VAI_SMS;
                        SelectedEmployee.LAST_NAME = data[i].LAST_NAME;
                        SelectedEmployee.MARITAL_STATUS = data[i].MARITAL_STATUS;
                        SelectedEmployee.MAX_SPOT_DISCOUNT = data[i].MAX_SPOT_DISCOUNT;
                        SelectedEmployee.MIDDLE_NAME = data[i].MIDDLE_NAME;
                        SelectedEmployee.MOBILE_NO = data[i].MOBILE_NO;
                        SelectedEmployee.PIN_NO = data[i].PIN_NO;
                        SelectedEmployee.SALES_PERCENT = data[i].SALES_PERCENT;
                        SelectedEmployee.SEARCH_CODE = data[i].SEARCH_CODE;
                        SelectedEmployee.STATE = data[i].STATE;
                        SelectedEmployee.TELEPHONE_NO = data[i].TELEPHONE_NO;
                        SelectedEmployee.WEBSITE = data[i].WEBSITE;
                        SelectedEmployee.WORKING_SHIFT = data[i].WORKING_SHIFT;
                    }
                    App.Current.Properties["EmpEdit"] = SelectedEmployee;
                    EmployeeAdd _EmployeeAdd = new EmployeeAdd();
                    _EmployeeAdd.Show();
                    //ModalService.NavigateTo(new EmployeeAdd(), delegate(bool returnValue) { });
                }
            }

            else
            {
                MessageBox.Show("Select Employee first", "Employee Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        public ICommand _EditEmployee { get; set; }
        public ICommand EditEmployee
        {
            get
            {
                if (_EditEmployee == null)
                {
                    _EditEmployee = new DelegateCommand(Edit_Employee);
                }
                return _EditEmployee;
            }
        }
        public async void Edit_Employee()
        {

            try
            {
                if (SelectedEmployee.EMPLOYEE_ID != null && SelectedEmployee.EMPLOYEE_ID != 0)
                {
                    App.Current.Properties["Action"] = "Edit";
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    response = client.GetAsync("api/EmployeeAPI/EmployeeEdit?id=" + SelectedEmployee.EMPLOYEE_ID + "").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        data = JsonConvert.DeserializeObject<EmployeeModel[]>(await response.Content.ReadAsStringAsync());
                        // EmployeeData = new List<EmployeeModel>();
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedEmployee.ADDRESS_1 = data[i].ADDRESS_1;
                            SelectedEmployee.ADDRESS_2 = data[i].ADDRESS_2;
                            SelectedEmployee.FIRST_NAME = data[i].FIRST_NAME;
                            SelectedEmployee.CITY = data[i].CITY;
                            SelectedEmployee.BUSINESS_LOCATION = data[i].BUSINESS_LOCATION;
                            SelectedEmployee.COMMISSION_QUICK_POSITION = data[i].COMMISSION_QUICK_POSITION;
                            SelectedEmployee.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedEmployee.DATE_OF_JOIN = data[i].DATE_OF_JOIN;
                            SelectedEmployee.DEPARTMENT = data[i].DEPARTMENT;
                            SelectedEmployee.DOB = data[i].DOB;
                            SelectedEmployee.EMAIL = data[i].EMAIL;
                            SelectedEmployee.EMPLOYEE_CODE = data[i].EMPLOYEE_CODE;
                            SelectedEmployee.EMPLOYEE_DESIGNATION = data[i].EMPLOYEE_DESIGNATION;
                            SelectedEmployee.EMPLOYEE_ID = data[i].EMPLOYEE_ID;
                            SelectedEmployee.FAX_NO = data[i].FAX_NO;
                            SelectedEmployee.IS_APPOINTMENT = data[i].IS_APPOINTMENT;
                            SelectedEmployee.IS_APPROVE_ACCESS_VAI_SMS = data[i].IS_APPROVE_ACCESS_VAI_SMS;
                            SelectedEmployee.IS_ATTACHED_INVOICE = data[i].IS_ATTACHED_INVOICE;
                            SelectedEmployee.GENDER = data[i].GENDER;
                            SelectedEmployee.IMAGE_PATH = data[i].IMAGE_PATH;
                            SelectedEmployee.IS_NOT_AN_EMPLOYEE = data[i].IS_NOT_AN_EMPLOYEE;
                            SelectedEmployee.IS_REQUEST_VAI_SMS = data[i].IS_REQUEST_VAI_SMS;
                            SelectedEmployee.LAST_NAME = data[i].LAST_NAME;
                            SelectedEmployee.MARITAL_STATUS = data[i].MARITAL_STATUS;
                            SelectedEmployee.MAX_SPOT_DISCOUNT = data[i].MAX_SPOT_DISCOUNT;
                            SelectedEmployee.MIDDLE_NAME = data[i].MIDDLE_NAME;
                            SelectedEmployee.MOBILE_NO = data[i].MOBILE_NO;
                            SelectedEmployee.PIN_NO = data[i].PIN_NO;
                            SelectedEmployee.SALES_PERCENT = data[i].SALES_PERCENT;
                            SelectedEmployee.SEARCH_CODE = data[i].SEARCH_CODE;
                            SelectedEmployee.STATE = data[i].STATE;
                            SelectedEmployee.TELEPHONE_NO = data[i].TELEPHONE_NO;
                            SelectedEmployee.WEBSITE = data[i].WEBSITE;
                            SelectedEmployee.WORKING_SHIFT = data[i].WORKING_SHIFT;
                        }
                        App.Current.Properties["EmpEdit"] = SelectedEmployee;
                        EmployeeAdd _EmployeeAdd = new EmployeeAdd();
                        _EmployeeAdd.Show();
                        //ModalService.NavigateTo(new EmployeeAdd(), delegate(bool returnValue) { });
                    }
                }

                else
                {
                    MessageBox.Show("Select Employee first", "Employee Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ICommand _ViewEmployee { get; set; }
        public ICommand ViewEmployee
        {
            get
            {
                if (_ViewEmployee == null)
                {
                    _ViewEmployee = new DelegateCommand(View_Employee);
                }
                return _ViewEmployee;
            }
        }
        public async void View_Employee()
        {

            try
            {
                if (SelectedEmployee.EMPLOYEE_ID != null && SelectedEmployee.EMPLOYEE_ID != 0)
                {
                    App.Current.Properties["Action"] = "View";
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    response = client.GetAsync("api/EmployeeAPI/EmployeeEdit?id=" + SelectedEmployee.EMPLOYEE_ID + "").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        data = JsonConvert.DeserializeObject<EmployeeModel[]>(await response.Content.ReadAsStringAsync());
                        // EmployeeData = new List<EmployeeModel>();
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedEmployee.ADDRESS_1 = data[i].ADDRESS_1;
                            SelectedEmployee.ADDRESS_2 = data[i].ADDRESS_2;
                            SelectedEmployee.FIRST_NAME = data[i].FIRST_NAME;
                            SelectedEmployee.CITY = data[i].CITY;
                            SelectedEmployee.BUSINESS_LOCATION = data[i].BUSINESS_LOCATION;
                            SelectedEmployee.COMMISSION_QUICK_POSITION = data[i].COMMISSION_QUICK_POSITION;
                            SelectedEmployee.COMPANY_ID = data[i].COMPANY_ID;
                            SelectedEmployee.DATE_OF_JOIN = data[i].DATE_OF_JOIN;
                            SelectedEmployee.DEPARTMENT = data[i].DEPARTMENT;
                            SelectedEmployee.DOB = data[i].DOB;
                            SelectedEmployee.EMAIL = data[i].EMAIL;
                            SelectedEmployee.EMPLOYEE_CODE = data[i].EMPLOYEE_CODE;
                            SelectedEmployee.EMPLOYEE_DESIGNATION = data[i].EMPLOYEE_DESIGNATION;
                            SelectedEmployee.EMPLOYEE_ID = data[i].EMPLOYEE_ID;
                            SelectedEmployee.FAX_NO = data[i].FAX_NO;
                            SelectedEmployee.IS_APPOINTMENT = data[i].IS_APPOINTMENT;
                            SelectedEmployee.IS_APPROVE_ACCESS_VAI_SMS = data[i].IS_APPROVE_ACCESS_VAI_SMS;
                            SelectedEmployee.IS_ATTACHED_INVOICE = data[i].IS_ATTACHED_INVOICE;
                            SelectedEmployee.GENDER = data[i].GENDER;
                            SelectedEmployee.IMAGE_PATH = data[i].IMAGE_PATH;
                            SelectedEmployee.IS_NOT_AN_EMPLOYEE = data[i].IS_NOT_AN_EMPLOYEE;
                            SelectedEmployee.IS_REQUEST_VAI_SMS = data[i].IS_REQUEST_VAI_SMS;
                            SelectedEmployee.LAST_NAME = data[i].LAST_NAME;
                            SelectedEmployee.MARITAL_STATUS = data[i].MARITAL_STATUS;
                            SelectedEmployee.MAX_SPOT_DISCOUNT = data[i].MAX_SPOT_DISCOUNT;
                            SelectedEmployee.MIDDLE_NAME = data[i].MIDDLE_NAME;
                            SelectedEmployee.MOBILE_NO = data[i].MOBILE_NO;
                            SelectedEmployee.PIN_NO = data[i].PIN_NO;
                            SelectedEmployee.SALES_PERCENT = data[i].SALES_PERCENT;
                            SelectedEmployee.SEARCH_CODE = data[i].SEARCH_CODE;
                            SelectedEmployee.STATE = data[i].STATE;
                            SelectedEmployee.TELEPHONE_NO = data[i].TELEPHONE_NO;
                            SelectedEmployee.WEBSITE = data[i].WEBSITE;
                            SelectedEmployee.WORKING_SHIFT = data[i].WORKING_SHIFT;
                        }
                        App.Current.Properties["EmpView"] = SelectedEmployee;
                        EmployeeView _view = new EmployeeView();
                        _view.Show();
                    }
                }

                else
                {
                    MessageBox.Show("Select Employee first", "Employee Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }


        public ICommand _InsertEmployee { get; set; }
        public ICommand InsertEmployee
        {
            get
            {
                if (_InsertEmployee == null)
                {

                    _InsertEmployee = new DelegateCommand(Add_Employee);
                }
                return _InsertEmployee;
            }
        }

        public async void Add_Employee()
        {
            if (SelectedEmployee.FIRST_NAME == "" || SelectedEmployee.FIRST_NAME == null)
            {
                MessageBox.Show("FIRST NAME Should Not be Blank");
            }
            else if (SelectedEmployee.SEARCH_CODE == "" || SelectedEmployee.SEARCH_CODE == null)
            {
                MessageBox.Show("SEARCH CODE Should Not be Blank");
            }
                            
            else if (SelectedEmployee.DEPARTMENT == "" || SelectedEmployee.DEPARTMENT == null)
            {
                MessageBox.Show("DEPARTMENT Should not be Blank");
            }
            else if (SelectedEmployee.EMPLOYEE_DESIGNATION == "" || SelectedEmployee.EMPLOYEE_DESIGNATION == null)
            {
                MessageBox.Show("EMPLOYEE DESIGNATION Should not be Blank");
            }

            else if (SelectedEmployee.ADDRESS_1 == "" || SelectedEmployee.ADDRESS_1 == null)
            {
                MessageBox.Show("ADDRESS 1  Should Not be Blank");
            }
            else if (SelectedEmployee.SALES_PERCENT == 0 || SelectedEmployee.SALES_PERCENT == null)
            {
                MessageBox.Show("SALES PERCENT Should Not be Blank");
            }
            else if (SelectedEmployee.MAX_SPOT_DISCOUNT == 0 || SelectedEmployee.MAX_SPOT_DISCOUNT == null)
            {
                MessageBox.Show("MAX SPOT DISCOUNT Hierarchy Should not be Blank");
            }
            
            else
            {
                if (App.Current.Properties["AddEmpBussLocation"] != null)
                {
                    SelectedEmployee.BUSINESS_LOCATION = App.Current.Properties["AddEmpBussLocation"].ToString();
                }
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                SelectedEmployee.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                var response = await client.PostAsJsonAsync("api/EmployeeAPI/EmployeeAdd/", SelectedEmployee);
                if (response.StatusCode.ToString() == "OK")
                {

                    MessageBox.Show("Employee insert Successfully");
                    Cancel_Emp();
                    ModalService.NavigateTo(new Employee(), delegate(bool returnValue) { });
                }
            }

        }
        public ICommand _UpdateEmployee { get; set; }
        public ICommand UpdateEmployee
        {
            get
            {
                if (_UpdateEmployee == null)
                {

                    _UpdateEmployee = new DelegateCommand(Update_Employee);
                }
                return _UpdateEmployee;
            }
        }

        public async void Update_Employee()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            SelectedEmployee.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            var response = await client.PostAsJsonAsync("api/EmployeeAPI/EmployeeUpdate/", SelectedEmployee);
            if (response.StatusCode.ToString() == "OK")
            {

                MessageBox.Show("Employee Update Successfully");
                Cancel_Emp();
                ModalService.NavigateTo(new Employee(), delegate(bool returnValue) { });
            }


        }
        private List<EmployeeModel> _EmployeeData;
        public List<EmployeeModel> EmployeeData
        {
            get { return _EmployeeData; }
            set
            {
                if (_EmployeeData != value)
                {
                    _EmployeeData = value;
                }
            }
        }
        public async Task<ObservableCollection<EmployeeModel>> GetEmployee(long EmpId)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.GetAsync("api/EmployeeAPI/EmployeeList?id=" + EmpId + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<EmployeeModel[]>(await response.Content.ReadAsStringAsync());
                    // EmployeeData = new List<EmployeeModel>();
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;
                        _ListGrid_Temp.Add(new EmployeeModel
                        {
                            SLNO=x,
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
                    if (SEARCH_EMP != "" && SEARCH_EMP != null)
                    {
                        var item1 = (from m in _ListGrid_Temp where m.FIRST_NAME.Contains(SEARCH_EMP) select m).ToList();
                        _ListGrid_Temp = item1;
                    }
                }
                ListGrid = _ListGrid_Temp; 
                return new ObservableCollection<EmployeeModel>(_ListGrid_Temp);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private string _SEARCH_EMP;
        public string SEARCH_EMP
        {
            get
            {
                return _SEARCH_EMP;
            }
            set
            {


                if (_SEARCH_EMP != value)
                {
                    _SEARCH_EMP = value;

                    if (_SEARCH_EMP != "" && _SEARCH_EMP != null)
                    {

                        GetEmployee(COMPANY_ID);
                    }
                    else
                    {
                        GetEmployee(COMPANY_ID);

                    }
                    OnPropertyChanged("SEARCH_EMP");
                }
            }
        }
        public List<EmployeeModel> _ListGrid { get; set; }
        public List<EmployeeModel> ListGrid
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
        public EmployeeViewModel()
        {
            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            if (App.Current.Properties["Action"].ToString() == "Edit")
            {
                CreatVisible = "Collapsed";
                UpdVisible = "Visible";
                SelectedEmployee = App.Current.Properties["EmpEdit"] as EmployeeModel;
                App.Current.Properties["Action"] = "";
                //GetEmployee(comp);
            }
            else if (App.Current.Properties["Action"].ToString() == "View")
            {
                SelectedEmployee = App.Current.Properties["EmpView"] as EmployeeModel;
                App.Current.Properties["Action"] = "";
                App.Current.Properties["EmpView"] = "";
            }
            else
            {
                var fdr = GetEmpNo();
                string dd = Convert.ToString(fdr.Result);
                UpdVisible = "Collapsed";
                CreatVisible = "Visible";
                GetEmployee(comp);
                SelectedEmployee = new EmployeeModel();
                SelectedEmployee.DATE_OF_JOIN = System.DateTime.Now;
                SelectedEmployee.DOB = System.DateTime.Now;
                string aaa = dd.Substring(3, 5);
                int xx = Convert.ToInt32(aaa);
                int noo = Convert.ToInt32(xx + 1);
                string nnnn = "E-" + noo.ToString("D5");
                SelectedEmployee.EMPLOYEE_CODE = nnnn;

            }
        }

        public async Task<string> GetEmpNo()
        {

            string uhy = "";
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/EmployeeAPI/GetEmpNo/").Result;
                if (response.IsSuccessStatusCode)
                {
                    //hy = Json.SerializeObject<int[]>(await response.Content.ReadAsStringAsync());
                    uhy = await response.Content.ReadAsStringAsync();
                }

            }
            catch (Exception ex)
            { }

            return uhy;
        }








        private long _COMAPNY_ID;
        public long COMPANY_ID
        {
            get
            {
                return SelectedEmployee.COMPANY_ID;
            }
            set
            {
                SelectedEmployee.COMPANY_ID = value;


                if (SelectedEmployee.COMPANY_ID != value)
                {
                    SelectedEmployee.COMPANY_ID = value;
                    OnPropertyChanged("COMPANY_ID");
                }
            }
        }


        private long _EMPLOYEE_ID;
        public long EMPLOYEE_ID
        {
            get
            {
                return SelectedEmployee.EMPLOYEE_ID;
            }
            set
            {
                SelectedEmployee.EMPLOYEE_ID = value;


                if (SelectedEmployee.EMPLOYEE_ID != value)
                {
                    SelectedEmployee.EMPLOYEE_ID = value;
                    OnPropertyChanged("EMPLOYEE_ID");
                }
            }
        }
        private string _EMPLOYEE_CODE;
        public string EMPLOYEE_CODE
        {
            get
            {
                return SelectedEmployee.EMPLOYEE_CODE;
            }
            set
            {
                SelectedEmployee.EMPLOYEE_CODE = value;


                if (SelectedEmployee.EMPLOYEE_CODE != value)
                {
                    SelectedEmployee.EMPLOYEE_CODE = value;
                    OnPropertyChanged("EMPLOYEE_CODE");
                }
            }
        }
        private string _FIRST_NAME;
        public string FIRST_NAME
        {
            get
            {
                return SelectedEmployee.FIRST_NAME;
            }
            set
            {
                SelectedEmployee.FIRST_NAME = value;


                if (SelectedEmployee.FIRST_NAME != value)
                {
                    SelectedEmployee.FIRST_NAME = value;
                    OnPropertyChanged("FIRST_NAME");
                }
            }
        }
        private decimal _SALES_PERCENT;
        public decimal SALES_PERCENT
        {
            get
            {
                return SelectedEmployee.SALES_PERCENT;
            }
            set
            {
                SelectedEmployee.SALES_PERCENT = value;


                if (SelectedEmployee.SALES_PERCENT != value)
                {
                    SelectedEmployee.SALES_PERCENT = value;
                    OnPropertyChanged("SALES_PERCENT");
                }
            }
        }
        private string _COMMISSION_QUICK_POSITION;
        public string COMMISSION_QUICK_POSITION
        {
            get
            {
                return SelectedEmployee.COMMISSION_QUICK_POSITION;
            }
            set
            {
                SelectedEmployee.COMMISSION_QUICK_POSITION = value;


                if (SelectedEmployee.COMMISSION_QUICK_POSITION != value)
                {
                    SelectedEmployee.COMMISSION_QUICK_POSITION = value;
                    OnPropertyChanged("COMMISSION_QUICK_POSITION");
                }
            }
        }

        private string _MIDDLE_NAME;
        public string MIDDLE_NAME
        {
            get
            {
                return SelectedEmployee.MIDDLE_NAME;
            }
            set
            {
                SelectedEmployee.MIDDLE_NAME = value;


                if (SelectedEmployee.MIDDLE_NAME != value)
                {
                    SelectedEmployee.MIDDLE_NAME = value;
                    OnPropertyChanged("MIDDLE_NAME");
                }
            }
        }
        private string _LAST_NAME;
        public string LAST_NAME
        {
            get
            {
                return SelectedEmployee.LAST_NAME;
            }
            set
            {
                SelectedEmployee.LAST_NAME = value;


                if (SelectedEmployee.LAST_NAME != value)
                {
                    SelectedEmployee.LAST_NAME = value;
                    OnPropertyChanged("LAST_NAME");
                }
            }
        }
        private string _SEARCH_CODE;
        public string SEARCH_CODE
        {
            get
            {
                return SelectedEmployee.EMPLOYEE_CODE;
            }
            set
            {
                SelectedEmployee.EMPLOYEE_CODE = value;


                if (SelectedEmployee.EMPLOYEE_CODE != value)
                {
                    SelectedEmployee.EMPLOYEE_CODE = value;
                    OnPropertyChanged("EMPLOYEE_CODE");
                }
            }
        }

        private DateTime _DOB;
        public DateTime DOB
        {
            get
            {
                return SelectedEmployee.DOB;
            }
            set
            {
                SelectedEmployee.DOB = value;


                if (SelectedEmployee.DOB != value)
                {
                    SelectedEmployee.DOB = value;
                    OnPropertyChanged("DOB");
                }
            }
        }
        private string _MARITAL_STATUS;
        public string MARITAL_STATUS
        {
            get
            {
                return SelectedEmployee.MARITAL_STATUS;
            }
            set
            {
                SelectedEmployee.MARITAL_STATUS = value;


                if (SelectedEmployee.MARITAL_STATUS != value)
                {
                    SelectedEmployee.MARITAL_STATUS = value;
                    OnPropertyChanged("MARITAL_STATUS");
                }
            }
        }
        private string _GENDER;
        public string GENDER
        {
            get
            {
                return SelectedEmployee.GENDER;
            }
            set
            {
                SelectedEmployee.GENDER = value;


                if (SelectedEmployee.GENDER != value)
                {
                    SelectedEmployee.GENDER = value;
                    OnPropertyChanged("GENDER");
                }
            }
        }
        private DateTime _DATE_OF_JOIN;
        public DateTime DATE_OF_JOIN
        {
            get
            {
                return SelectedEmployee.DATE_OF_JOIN;
            }
            set
            {
                SelectedEmployee.DATE_OF_JOIN = value;


                if (SelectedEmployee.DATE_OF_JOIN != value)
                {
                    SelectedEmployee.DATE_OF_JOIN = value;
                    OnPropertyChanged("DATE_OF_JOIN");
                }
            }
        }
        private string _BUSINESS_LOCATION;
        public string BUSINESS_LOCATION
        {
            get
            {
                return SelectedEmployee.BUSINESS_LOCATION;
            }
            set
            {
                SelectedEmployee.BUSINESS_LOCATION = value;


                if (SelectedEmployee.BUSINESS_LOCATION != value)
                {
                    SelectedEmployee.BUSINESS_LOCATION = value;
                    OnPropertyChanged("BUSINESS_LOCATION");
                }
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
            Window_BusinessLocationList IA = new Window_BusinessLocationList();
            IA.Show();

        }
        private string _DEPARTMENT;
        public string DEPARTMENT
        {
            get
            {
                return SelectedEmployee.DEPARTMENT;
            }
            set
            {
                SelectedEmployee.DEPARTMENT = value;


                if (SelectedEmployee.DEPARTMENT != value)
                {
                    SelectedEmployee.DEPARTMENT = value;
                    OnPropertyChanged("DEPARTMENT");
                }
            }
        }
        private string _EMPLOYEE_DESIGNATION;
        public string EMPLOYEE_DESIGNATION
        {
            get
            {
                return SelectedEmployee.EMPLOYEE_DESIGNATION;
            }
            set
            {
                SelectedEmployee.EMPLOYEE_DESIGNATION = value;


                if (SelectedEmployee.EMPLOYEE_DESIGNATION != value)
                {
                    SelectedEmployee.EMPLOYEE_DESIGNATION = value;
                    OnPropertyChanged("EMPLOYEE_DESIGNATION");
                }
            }
        }
        private bool _IS_REQUEST_VAI_SMS;
        public bool IS_REQUEST_VAI_SMS
        {
            get
            {
                return SelectedEmployee.IS_REQUEST_VAI_SMS;
            }
            set
            {
                SelectedEmployee.IS_REQUEST_VAI_SMS = value;


                if (SelectedEmployee.IS_REQUEST_VAI_SMS != value)
                {
                    SelectedEmployee.IS_REQUEST_VAI_SMS = value;
                    OnPropertyChanged("IS_REQUEST_VAI_SMS");
                }
            }
        }
        private bool _IS_APPROVE_ACCESS_VAI_SMS;
        public bool IS_APPROVE_ACCESS_VAI_SMS
        {
            get
            {
                return SelectedEmployee.IS_APPROVE_ACCESS_VAI_SMS;
            }
            set
            {
                SelectedEmployee.IS_APPROVE_ACCESS_VAI_SMS = value;


                if (SelectedEmployee.IS_APPROVE_ACCESS_VAI_SMS != value)
                {
                    SelectedEmployee.IS_APPROVE_ACCESS_VAI_SMS = value;
                    OnPropertyChanged("IS_APPROVE_ACCESS_VAI_SMS");
                }
            }
        }
        private bool _IS_NOT_AN_EMPLOYEE;
        public bool IS_NOT_AN_EMPLOYEE
        {
            get
            {
                return SelectedEmployee.IS_NOT_AN_EMPLOYEE;
            }
            set
            {
                SelectedEmployee.IS_NOT_AN_EMPLOYEE = value;


                if (SelectedEmployee.IS_NOT_AN_EMPLOYEE != value)
                {
                    SelectedEmployee.IS_NOT_AN_EMPLOYEE = value;
                    OnPropertyChanged("IS_NOT_AN_EMPLOYEE");
                }
            }
        }
        private bool _IS_APPOINTMENT;
        public bool IS_APPOINTMENT
        {
            get
            {
                return SelectedEmployee.IS_APPOINTMENT;
            }
            set
            {
                SelectedEmployee.IS_APPOINTMENT = value;


                if (SelectedEmployee.IS_APPOINTMENT != value)
                {
                    SelectedEmployee.IS_APPOINTMENT = value;
                    OnPropertyChanged("IS_APPOINTMENT");
                }
            }
        }
        private bool _IS_ATTACHED_INVOICE;
        public bool IS_ATTACHED_INVOICE
        {
            get
            {
                return SelectedEmployee.IS_ATTACHED_INVOICE;
            }
            set
            {
                SelectedEmployee.IS_ATTACHED_INVOICE = value;


                if (SelectedEmployee.IS_ATTACHED_INVOICE != value)
                {
                    SelectedEmployee.IS_ATTACHED_INVOICE = value;
                    OnPropertyChanged("IS_ATTACHED_INVOICE");
                }
            }
        }
        private string _WORKING_SHIFT;
        public string WORKING_SHIFT
        {
            get
            {
                return SelectedEmployee.WORKING_SHIFT;
            }
            set
            {
                SelectedEmployee.WORKING_SHIFT = value;


                if (SelectedEmployee.WORKING_SHIFT != value)
                {
                    SelectedEmployee.WORKING_SHIFT = value;
                    OnPropertyChanged("WORKING_SHIFT");
                }
            }
        }
        private string _ADDRESS_1;
        public string ADDRESS_1
        {
            get
            {
                return SelectedEmployee.ADDRESS_1;
            }
            set
            {
                SelectedEmployee.ADDRESS_1 = value;


                if (SelectedEmployee.ADDRESS_1 != value)
                {
                    SelectedEmployee.ADDRESS_1 = value;
                    OnPropertyChanged("ADDRESS_1");
                }
            }
        }
        private string _ADDRESS_2;
        public string ADDRESS_2
        {
            get
            {
                return SelectedEmployee.ADDRESS_2;
            }
            set
            {
                SelectedEmployee.ADDRESS_2 = value;


                if (SelectedEmployee.ADDRESS_2 != value)
                {
                    SelectedEmployee.ADDRESS_2 = value;
                    OnPropertyChanged("ADDRESS_2");
                }
            }
        }
        private string _CITY;
        public string CITY
        {
            get
            {
                return SelectedEmployee.CITY;
            }
            set
            {
                SelectedEmployee.CITY = value;


                if (SelectedEmployee.CITY != value)
                {
                    SelectedEmployee.CITY = value;
                    OnPropertyChanged("CITY");
                }
            }
        }
        private string _STATE;
        public string STATE
        {
            get
            {
                return SelectedEmployee.STATE;
            }
            set
            {
                SelectedEmployee.STATE = value;


                if (SelectedEmployee.STATE != value)
                {
                    SelectedEmployee.STATE = value;
                    OnPropertyChanged("STATE");
                }
            }
        }
        private decimal _MAX_SPOT_DISCOUNT;
        public decimal MAX_SPOT_DISCOUNT
        {
            get
            {
                return SelectedEmployee.MAX_SPOT_DISCOUNT;
            }
            set
            {
                SelectedEmployee.MAX_SPOT_DISCOUNT = value;
                if (SelectedEmployee.MAX_SPOT_DISCOUNT != value)
                {
                    SelectedEmployee.MAX_SPOT_DISCOUNT = value;
                    OnPropertyChanged("MAX_SPOT_DISCOUNT");
                }
            }
        }
        private string _PIN_NO;
        public string PIN_NO
        {
            get
            {
                return SelectedEmployee.PIN_NO;
            }
            set
            {
                SelectedEmployee.PIN_NO = value;


                if (SelectedEmployee.PIN_NO != value)
                {
                    SelectedEmployee.PIN_NO = value;
                    OnPropertyChanged("PIN_NO");
                }
            }
        }
        private string _TELEPHONE_NO;
        public string TELEPHONE_NO
        {
            get
            {
                return SelectedEmployee.TELEPHONE_NO;
            }
            set
            {
                SelectedEmployee.TELEPHONE_NO = value;


                if (SelectedEmployee.TELEPHONE_NO != value)
                {
                    SelectedEmployee.TELEPHONE_NO = value;
                    OnPropertyChanged("TELEPHONE_NO");
                }
            }
        }
        private string _FAX_NO;
        public string FAX_NO
        {
            get
            {
                return SelectedEmployee.FAX_NO;
            }
            set
            {
                SelectedEmployee.FAX_NO = value;


                if (SelectedEmployee.FAX_NO != value)
                {
                    SelectedEmployee.FAX_NO = value;
                    OnPropertyChanged("FAX_NO");
                }
            }
        }
        private string _MOBILE_NO;
        public string MOBILE_NO
        {
            get
            {
                return SelectedEmployee.MOBILE_NO;
            }
            set
            {
                SelectedEmployee.MOBILE_NO = value;


                if (SelectedEmployee.MOBILE_NO != value)
                {
                    SelectedEmployee.MOBILE_NO = value;
                    OnPropertyChanged("MOBILE_NO");
                }
            }
        }
        private string _EMAIL;
        public string EMAIL
        {
            get
            {
                return SelectedEmployee.EMAIL;
            }
            set
            {
                SelectedEmployee.EMAIL = value;


                if (SelectedEmployee.EMAIL != value)
                {
                    SelectedEmployee.EMAIL = value;
                    OnPropertyChanged("EMAIL");
                }
            }
        }
        private string _WEBSITE;
        public string WEBSITE
        {
            get
            {
                return SelectedEmployee.WEBSITE;
            }
            set
            {
                SelectedEmployee.WEBSITE = value;


                if (SelectedEmployee.WEBSITE != value)
                {
                    SelectedEmployee.WEBSITE = value;
                    OnPropertyChanged("WEBSITE");
                }
            }
        }
        private string _IMAGE_PATH;
        public string IMAGE_PATH
        {
            get
            {
                return SelectedEmployee.IMAGE_PATH;
            }
            set
            {
                SelectedEmployee.IMAGE_PATH = value;


                if (SelectedEmployee.IMAGE_PATH != value)
                {
                    SelectedEmployee.IMAGE_PATH = value;
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
                    _Cancel = new DelegateCommand(Cancel_Emp);
                }
                return _Cancel;
            }
        }



        public void Cancel_Emp()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
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
            if (App.Current.Properties["EMPRecItem"] != null)
            {
                ReceiveAddItem.EmpReff.Text = null;
                ReceiveAddItem.EmpReff.Text = SelectedEmployee.FIRST_NAME;
                App.Current.Properties["EMPRecItem"] = null;
            }
            if (App.Current.Properties["EmloyeeName"] != null)
            {
                InvoiceDeliveryDetails.EmployeeName.Text = null;
                InvoiceDeliveryDetails.EmployeeName.Text = SelectedEmployee.FIRST_NAME;
                Cancel_Emp();
            }
            //Cancel_Emp();
        }

        public ICommand _AddDesignationClick { get; set; }
        public ICommand AddDesignationClick
        {
            get
            {
                if (_AddDesignationClick == null)
                {
                    _AddDesignationClick = new DelegateCommand(AddDesignation_Click);
                }
                return _AddDesignationClick;
            }
        }
        public void AddDesignation_Click()
        {
            App.Current.Properties["DesignationEmpReff"] = 1;
            Window_Designation _emp = new Window_Designation();
            _emp.Show();
            // ModalService.NavigateTo(new EmployeeAdd(), delegate(bool returnValue) { });

        }

        public ICommand _AddDepartmentClick { get; set; }
        public ICommand AddDepartmentClick
        {
            get
            {
                if (_AddDepartmentClick == null)
                {
                    _AddDepartmentClick = new DelegateCommand(AddDepartment_Click);
                }
                return _AddDepartmentClick;
            }
        }
        public void AddDepartment_Click()
        {
            App.Current.Properties["DepartmentEmpReff"] = 1;
            Window_DepartmentList _emp = new Window_DepartmentList();
            _emp.Show();
            // ModalService.NavigateTo(new EmployeeAdd(), delegate(bool returnValue) { });

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
