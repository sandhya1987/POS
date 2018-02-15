using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Department;
using InvoicePOS.UserControll.Employee;
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
    public class DepartmentViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        DepartmentModel[] data = null;
        List<DepartmentModel> _ListGrid_Temp = new List<DepartmentModel>();
        public DepartmentViewModel()
        {
            var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            if (App.Current.Properties["Action"].ToString() == "Edit")
            {
                CreatVisible = "Collapsed";
                UpdVisible = "Visible";
                SelectedDepartment = App.Current.Properties["DepartmentEdit"] as DepartmentModel;
                App.Current.Properties["Action"] = "";
            }
            else if (App.Current.Properties["Action"].ToString() == "View")
            {
                SelectedDepartment = App.Current.Properties["DepartmentView"] as DepartmentModel;
                App.Current.Properties["Action"] = "";
            }
            else
            {
                UpdVisible = "Collapsed";
                CreatVisible = "Visible";
                SelectedDepartment = new DepartmentModel();
                GetDepartment(comp);
            }
        }


        public ICommand _AddDepartmentClick;
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
            Add_department IA = new Add_department();
            IA.Show();

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
        public async Task<ObservableCollection<DepartmentModel>> GetDepartment(long comp)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/DepartmentAPI/GetDepartment?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<DepartmentModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;

                        _ListGrid_Temp.Add(new DepartmentModel
                        {
                            SLNO = x,
                            COMPANY_ID = data[i].COMPANY_ID,
                            DEPARTMENT_ID = data[i].DEPARTMENT_ID,
                            IS_DELETE = data[i].IS_DELETE,
                            DEPARTMENT_NAME = data[i].DEPARTMENT_NAME,
                            SORT_INDEX = data[i].SORT_INDEX,
                            CREATED_DATE = data[i].CREATED_DATE,
                            CREATED_BY = data[i].CREATED_BY,
                            STATUS = data[i].STATUS,

                        });
                    }
                }
                ListGrid = _ListGrid_Temp;
                App.Current.Properties["DepartmentList"] = ListGrid;
                return new ObservableCollection<DepartmentModel>(_ListGrid_Temp);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<DepartmentModel> _ListGrid { get; set; }
        public List<DepartmentModel> ListGrid
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

        private DepartmentModel _SelectedDepartment;
        public DepartmentModel SelectedDepartment
        {
            get { return _SelectedDepartment; }
            set
            {
                if (_SelectedDepartment != value)
                {
                    _SelectedDepartment = value;
                    OnPropertyChanged("SelectedDepartment");
                }
            }
        }
        private string _Department_Name;
        public string  Department_Name
        {
            get
            {
                return SelectedDepartment.DEPARTMENT_NAME;
            }
            set
            {
                SelectedDepartment.DEPARTMENT_NAME = value;

                if (SelectedDepartment.DEPARTMENT_NAME != value)
                {
                    SelectedDepartment.DEPARTMENT_NAME = value;
                    OnPropertyChanged("DEPARTMENT_NAME");
                }
            }
        }
        private int _SortIndex;
        public  int SortIndex
        {
            get
            {
                return SelectedDepartment.SORT_INDEX;
            }
            set
            {
                SelectedDepartment.SORT_INDEX = value;

                if (SelectedDepartment.SORT_INDEX != value)
                {
                    SelectedDepartment.SORT_INDEX = value;
                    OnPropertyChanged("SORT_INDEX");
                }
            }
        }
        private bool _IS_DELETE;
        public bool IS_DELETE
        {
            get
            {
                return SelectedDepartment.IS_DELETE;
            }
            set
            {
                SelectedDepartment.IS_DELETE = value;

                if (SelectedDepartment.IS_DELETE != value)
                {
                    SelectedDepartment.IS_DELETE = value;
                    OnPropertyChanged("IS_DELETE");
                }
            }
        }

        private long _COMPANY_ID;
        public  long  COMPANY_ID
        {
            get
            {
                return _COMPANY_ID;
            }
            set
            {
                _COMPANY_ID = value;
                OnPropertyChanged("COMPANY_ID");

            }
        }

        private long _DEPARTMENT_ID;
        public long DEPARTMENT_ID
        {
            get
            {
                return _DEPARTMENT_ID;
            }
            set
            {
                _DEPARTMENT_ID = value;
                OnPropertyChanged("DEPARTMENT_ID");

            }
        }

        private long _CREATED_DATE;
        public long CREATED_DATE
        {
            get
            {
                return _CREATED_DATE;
            }
            set
            {
                _CREATED_DATE = value;
                OnPropertyChanged("CREATED_DATE");

            }
        }

        private long _CREATED_BY;
        public long CREATED_BY
        {
            get
            {
                return _CREATED_BY;
            }
            set
            {
                _CREATED_BY = value;
                OnPropertyChanged("CREATED_BY");

            }
        }

        private string _DEPARTMENT_NAME;
        public string DEPARTMENT_NAME
        {
            get
            {
                return SelectedDepartment.DEPARTMENT_NAME;
            }
            set
            {
                SelectedDepartment.DEPARTMENT_NAME = value;
                OnPropertyChanged("DEPARTMENT_ID");

            }
        }

        private bool _STATUS;
        public bool STATUS
        {
            get
            {
                return _STATUS;
            }
            set
            {
                _STATUS = value;
                OnPropertyChanged("STATUS");

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
            if (App.Current.Properties["DepartmentEmpReff"] != null)
            {
                EmployeeAdd.EmpDepartmentReff.Text = null;
                EmployeeAdd.EmpDepartmentReff.Text = SelectedDepartment.DEPARTMENT_NAME;
                App.Current.Properties["DepartmentEmpReff"] = null;
            }
            Cancel_Department();
        }

        public ICommand _DepartmentView { get; set; }
        public ICommand ViewDepartment
        {
            get
            {
                if (_DepartmentView == null)
                {
                    _DepartmentView = new DelegateCommand(Department_View);
                }
                return _DepartmentView;
            }
        }
        public async void Department_View()
        {
            if (SelectedDepartment.DEPARTMENT_ID != null && SelectedDepartment.DEPARTMENT_ID != 0)
            {
                App.Current.Properties["Action"] = "View";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/DepartmentAPI/DepartmentEdit?id=" + SelectedDepartment.DEPARTMENT_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<DepartmentModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedDepartment.COMPANY_ID = data[i].COMPANY_ID;  
                            SelectedDepartment.IS_DELETE = data[i].IS_DELETE;
                            SelectedDepartment.DEPARTMENT_NAME = data[i].DEPARTMENT_NAME;
                            SelectedDepartment.SORT_INDEX = data[i].SORT_INDEX;
                            SelectedDepartment.DEPARTMENT_ID = data[i].DEPARTMENT_ID;
                        }
                        App.Current.Properties["DepartmentView"] = SelectedDepartment;
                        DepartmentView IA = new DepartmentView();
                        IA.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Select Department first", "Department Department", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }


        

        public ICommand _InsertDepartment;
        public ICommand InsertDepartment
        {
            get
            {
                if (_InsertDepartment == null)
                {
                    _InsertDepartment = new DelegateCommand(Insert_Department);
                }
                return _InsertDepartment;
            }
        }
        public async void Insert_Department()
        {
            SelectedDepartment.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            var response = await client.PostAsJsonAsync("api/DepartmentAPI/DepartmentAdd/", SelectedDepartment);
            if (response.StatusCode.ToString() == "OK")
            {

                MessageBox.Show("Department Added Successfully");
                Cancel_Department();
                ModalService.NavigateTo(new Department_list(), delegate(bool returnValue) { });
            }
        }
        public ICommand _EditDepartment { get; set; }
        public ICommand EditDepartment
        {
            get
            {
                if (_EditDepartment == null)
                {
                    _EditDepartment = new DelegateCommand(Edit_Department);
                }
                return _EditDepartment;
            }
        }
        public async void Edit_Department()
        {
            if (SelectedDepartment.DEPARTMENT_ID != null && SelectedDepartment.DEPARTMENT_ID != 0)
            {
                App.Current.Properties["Action"] = "Edit";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/DepartmentAPI/DepartmentEdit?id=" + SelectedDepartment.DEPARTMENT_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<DepartmentModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            SelectedDepartment.COMPANY_ID = data[i].COMPANY_ID;
                            //SelectedDepartment.IS_DELETE = data[i].IS_DELETE;
                            SelectedDepartment.DEPARTMENT_NAME = data[i].DEPARTMENT_NAME;
                            SelectedDepartment.SORT_INDEX = data[i].SORT_INDEX;
                            SelectedDepartment.DEPARTMENT_ID = data[i].DEPARTMENT_ID;
                        }
                        App.Current.Properties["DepartmentEdit"] = SelectedDepartment;
                        Add_department IA = new Add_department();
                        IA.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Select Department first", "Department Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        public ICommand _UpdateDepartment;
        public ICommand UpdateDepartment
        {
            get
            {
                if (_UpdateDepartment == null)
                {
                    _UpdateDepartment = new DelegateCommand(Update_Department);
                }
                return _UpdateDepartment;
            }
        }


        public async void Update_Department()
        {
            HttpClient client = new HttpClient();
            //UnitData = new List<UnitModel>();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            var response = await client.PostAsJsonAsync("api/DepartmentAPI/DepartmentUpdate/", SelectedDepartment);
            if (response.StatusCode.ToString() == "OK")
            {
                MessageBox.Show("Department Update Successfully");
                Cancel_Department();
                ModalService.NavigateTo(new Department_list(), delegate(bool returnValue) { });
            }

        }

        public ICommand _DeleteDepartment;
        public ICommand DeleteDepartment
        {
            get
            {
                if (_DeleteDepartment == null)
                {
                    _DeleteDepartment = new DelegateCommand(Delete_Department);
                }
                return _DeleteDepartment;
            }
        }
        public async void Delete_Department()
        {
            if (SelectedDepartment.DEPARTMENT_ID != null && SelectedDepartment.DEPARTMENT_ID != 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure Delete this Department " + SelectedDepartment.DEPARTMENT_NAME + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {

                    var id = SelectedDepartment.DEPARTMENT_ID;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    HttpResponseMessage response = client.GetAsync("api/DepartmentAPI/DeleteDepartment?id=" + id + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {
                        MessageBox.Show("Department Delete Successfully");
                        ModalService.NavigateTo(new Department_list(), delegate(bool returnValue) { });

                    }
                }
                else
                {
                    Cancel_Department();
                }
            }
            else
            {
                MessageBox.Show("Select Department");
            }

        }

        

        public ICommand _Cancel;
        public ICommand Cancel
        {
            get
            {
                if (_Cancel == null)
                {
                    _Cancel = new DelegateCommand(Cancel_Department);
                }
                return _Cancel;
            }
        }

        public void Cancel_Department()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
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
