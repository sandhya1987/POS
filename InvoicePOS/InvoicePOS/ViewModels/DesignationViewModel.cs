using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Designation;
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
using InvoicePOS.UserControll.Employee;

namespace InvoicePOS.ViewModels
{
    public class DesignationViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        DesignationModel _DesignationModel = new DesignationModel();
        DesignationModel[] data = null;
        List<DesignationModel> _ListGrid_Temp = new List<DesignationModel>();
        public DesignationViewModel()
        {
            int comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            if (App.Current.Properties["Action"].ToString() == "Edit")
            {
                CreatVisible = "Collapsed";
                UpdVisible = "Visible";
                selectDesignation = App.Current.Properties["DesignationEdit"] as DesignationModel;
                App.Current.Properties["Action"] = "";
            }
            else if (App.Current.Properties["Action"].ToString() == "View")
            {
                selectDesignation = App.Current.Properties["DesignationView"] as DesignationModel;
                App.Current.Properties["Action"] = "";
            }
            else
            {
                UpdVisible = "Collapsed";
                CreatVisible = "Visible";
                selectDesignation = new DesignationModel();
                GetDesignation(comp);
            }
        }
        private long _DESIGNATION_ID;
        public long DESIGNATION_ID
        {
            get
            {
                return selectDesignation.DESIGNATION_ID;
            }
            set
            {
                selectDesignation.DESIGNATION_ID = value;
                OnPropertyChanged("DESIGNATION_ID");

            }
        }
        private string _DESIGNATION_NAME;
        public string DESIGNATION_NAME
        {
            get
            {
                return selectDesignation.DESIGNATION_NAME;
            }
            set
            {
                selectDesignation.DESIGNATION_NAME = value;
                OnPropertyChanged("DESIGNATION_NAME");

            }
        }
        private long _COMPANY_ID;
        public long COMPANY_ID
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
        private DateTime _CREATED_DATE;
        public DateTime CREATED_DATE
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
        private long _SORT_INDEX;
        public long SORT_INDEX
        {
            get
            {
                return selectDesignation.SORT_INDEX;
            }
            set
            {
                selectDesignation.SORT_INDEX = value;
                OnPropertyChanged("SORT_INDEX");

            }
        }
        public ICommand _InsertDesignation;
        public ICommand InsertDesignation
        {
            get
            {
                if (_InsertDesignation == null)
                {
                    _InsertDesignation = new DelegateCommand(Insert_Designation);
                }
                return _InsertDesignation;
            }
        }
        public async void Insert_Designation()
        {
            selectDesignation.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            var response = await client.PostAsJsonAsync("api/DesignationListAPI/DesignationAdd/", selectDesignation);
            if (response.StatusCode.ToString() == "OK")
            {

                System.Windows.MessageBox.Show("Designation Added Successfully");
                Cancel_Designation();
                ModalService.NavigateTo(new DesignationList(), delegate(bool returnValue) { });
            }
        }
        public ICommand _EditDesignation { get; set; }
        public ICommand EditDesignation
        {
            get
            {
                if (_EditDesignation == null)
                {
                    _EditDesignation = new DelegateCommand(Edit_Designation);
                }
                return _EditDesignation;
            }
        }
        public async void Edit_Designation()
        {
            if (selectDesignation.DESIGNATION_ID != null && selectDesignation.DESIGNATION_ID != 0)
            {
                long id = selectDesignation.DESIGNATION_ID;
                App.Current.Properties["Action"] = "Edit";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/DesignationListAPI/DesignationEdit?id=" + id + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<DesignationModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            selectDesignation.DESIGNATION_ID = data[i].DESIGNATION_ID;
                            selectDesignation.DESIGNATION_NAME = data[i].DESIGNATION_NAME;
                            selectDesignation.CREATED_BY = data[i].CREATED_BY;
                            selectDesignation.CREATED_DATE = data[i].CREATED_DATE;
                            selectDesignation.IS_DELETE = data[i].IS_DELETE;
                            selectDesignation.COMPANY_ID = data[i].COMPANY_ID;
                            selectDesignation.SORT_INDEX = data[i].SORT_INDEX;
                            selectDesignation.STATUS = data[i].STATUS;
                        }
                        App.Current.Properties["DesignationEdit"] = selectDesignation;
                        DesignationAdd IA = new DesignationAdd();
                        IA.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Select Designation first", "Designation Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        public ICommand _UpdateDesignation;
        public ICommand UpdateDesignation
        {
            get
            {
                if (_UpdateDesignation == null)
                {
                    _UpdateDesignation = new DelegateCommand(Update_Designation);
                }
                return _UpdateDesignation;
            }
        }


        public async void Update_Designation()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            var response = await client.PostAsJsonAsync("api/DesignationListAPI/DesignationUpdate/", selectDesignation);
            if (response.StatusCode.ToString() == "OK")
            {
                MessageBox.Show("Designation Update Successfully");
                Cancel_Designation();
                ModalService.NavigateTo(new DesignationList(), delegate(bool returnValue) { });
            }

        }
        public ICommand _DeleteDesignation;
        public ICommand DeleteDesignation
        {
            get
            {
                if (_DeleteDesignation == null)
                {
                    _DeleteDesignation = new DelegateCommand(Delete_Designation);
                }
                return _DeleteDesignation;
            }
        }
        public async void Delete_Designation()
        {
            if (selectDesignation.DESIGNATION_ID != null && selectDesignation.DESIGNATION_ID != 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure Delete this Designation " + selectDesignation.DESIGNATION_NAME + "?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {

                    var id = selectDesignation.DESIGNATION_ID;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    HttpResponseMessage response = client.GetAsync("api/DesignationListAPI/DeleteDesignation?id=" + id + "").Result;
                    if (response.StatusCode.ToString() == "OK")
                    {
                        MessageBox.Show("Designation Delete Successfully");
                        ModalService.NavigateTo(new DesignationList(), delegate(bool returnValue) { });

                    }
                }
                else
                {
                    Cancel_Designation();
                }
            }
            else
            {
                MessageBox.Show("Select Designation");
            }

        }
        public async Task<ObservableCollection<DesignationModel>> GetDesignation(long comp)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/DesignationListAPI/GetDesignation?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<DesignationModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;

                        _ListGrid_Temp.Add(new DesignationModel
                        {
                            SLNO = x,
                            COMPANY_ID = data[i].COMPANY_ID,
                            DESIGNATION_ID = data[i].DESIGNATION_ID,
                            CREATED_DATE = data[i].CREATED_DATE,
                            DESIGNATION_NAME = data[i].DESIGNATION_NAME,
                            IS_DELETE = data[i].IS_DELETE,
                            CREATED_BY = data[i].CREATED_BY,
                            SORT_INDEX = data[i].SORT_INDEX,
                            STATUS = data[i].STATUS,
                        });
                    }
                }
                ListGrid = _ListGrid_Temp;
                return new ObservableCollection<DesignationModel>(_ListGrid_Temp);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ICommand _ViewDesignation;
        public ICommand ViewDesignation
        {
            get
            {
                if (_ViewDesignation == null)
                {
                    _ViewDesignation = new DelegateCommand(View_Designation);
                }
                return _ViewDesignation;
            }
        }
        public async void View_Designation()
        {
            if (selectDesignation.DESIGNATION_ID != null && selectDesignation.DESIGNATION_ID != 0)
            {
                App.Current.Properties["Action"] = "View";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/DesignationListAPI/DesignationEdit?id=" + selectDesignation.DESIGNATION_ID + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<DesignationModel[]>(await response.Content.ReadAsStringAsync());
                    if (data.Length > 0)
                    {
                        for (int i = 0; i < data.Length; i++)
                        {
                            selectDesignation.DESIGNATION_ID = data[i].DESIGNATION_ID;
                            selectDesignation.DESIGNATION_NAME = data[i].DESIGNATION_NAME;
                            selectDesignation.CREATED_BY = data[i].CREATED_BY;
                            selectDesignation.CREATED_DATE = data[i].CREATED_DATE;
                            selectDesignation.IS_DELETE = data[i].IS_DELETE;
                            selectDesignation.COMPANY_ID = data[i].COMPANY_ID;
                            selectDesignation.SORT_INDEX = data[i].SORT_INDEX;
                            selectDesignation.STATUS = data[i].STATUS;
                        }
                        App.Current.Properties["DesignationView"] = selectDesignation;
                        DesignationView IA = new DesignationView();
                        IA.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Select unit first", "Unit Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        public ICommand _SelectOk;
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
        public async void Select_Ok()
        {
            if (App.Current.Properties["DesignationEmpReff"]!=null)
            {
                EmployeeAdd.EmpDesignationReff.Text = null;
                EmployeeAdd.EmpDesignationReff.Text = selectDesignation.DESIGNATION_NAME;
                App.Current.Properties["DesignationEmpReff"] = null;
            }
            Cancel_Designation();
        }
        public List<DesignationModel> _ListGrid { get; set; }
        public List<DesignationModel> ListGrid
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
        private DesignationModel _selectDesignation;
        public DesignationModel selectDesignation
        {
            get { return _selectDesignation; }
            set
            {
                if (_selectDesignation != value)
                {
                    _selectDesignation = value;
                    OnPropertyChanged("selectDesignation");
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
                    _Cancel = new DelegateCommand(Cancel_Designation);
                }
                return _Cancel;
            }
        }
        public void Cancel_Designation()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
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



        public ICommand _AddDesignationClick;
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
            DesignationAdd IA = new DesignationAdd();
            IA.Show();

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
