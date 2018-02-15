using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Report;
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
    public class ReportGroupViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {

        
        ReportGroupModel[] data;  

    
       
       private bool _isExpanded { get; set; }
       private bool _isSelected { get; set; }

       
        public ReportGroupViewModel()
        {
            SelectReportGroup = new ReportGroupModel();
            var cid =Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            // GetReportGroup(cid);


        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }

                // Expand all the way up to the root.
               
               
            }
        }

        /// <summary>
        /// Returns true if this object's Children have not yet been populated.
        /// </summary>


        public List<ReportAddModel> _ListGrid { get; set; }

        public List<ReportAddModel> ListGrid
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




        private ReportGroupModel _SelectReportGroup;
        public ReportGroupModel SelectReportGroup
        {
            get { return _SelectReportGroup; }
            set
            {
                if (_SelectReportGroup != value)
                {
                    _SelectReportGroup = value;
                    OnPropertyChanged("SelectReportGroup");
                }
            }
        }
        private string _REPORT_NAME;
        public string REPORT_NAME
        {
            get
            {
                return _REPORT_NAME;
            }
            set
            {
                _REPORT_NAME = value;

               
                    OnPropertyChanged("REPORT_NAME");
               
            }
        }

        private long _REPORT_GRP_ID;
        public long REPORT_GRP_ID
        {
            get
            {
                return SelectReportGroup.REPORT_GRP_ID;
            }
            set
            {
                SelectReportGroup.REPORT_GRP_ID = value;

                if (SelectReportGroup.REPORT_GRP_ID != value)
                {
                    SelectReportGroup.REPORT_GRP_ID = value;
                    OnPropertyChanged("REPORT_GRP_ID");
                }
            }
        }
        private long _REPORT_PARENT_ID;
        public long REPORT_PARENT_ID
        {
            get
            {
                return _REPORT_PARENT_ID;
            }
            set
            {
                _REPORT_PARENT_ID = value;

               
                    OnPropertyChanged("REPORT_PARENT_ID");
              
            }
        }
        private long _REPORT_CHILD_ID;
        public long REPORT_CHILD_ID
        {
            get
            {
                return _REPORT_CHILD_ID;
            }
            set
            {
                _REPORT_CHILD_ID = value;


                OnPropertyChanged("REPORT_CHILD_ID");

            }
        }
        private string _GROUP_NAME;
        public string GROUP_NAME
        {
            get
            {
                return SelectReportGroup.GROUP_NAME;
            }
            set
            {
                SelectReportGroup.GROUP_NAME = value;

                if (SelectReportGroup.GROUP_NAME != value)
                {
                    SelectReportGroup.GROUP_NAME = value;
                    OnPropertyChanged("GROUP_NAME");
                }
            }
        }
        private string _DESCRIPTION;
        public string DESCRIPTION
        {
            get
            {
                return SelectReportGroup.DESCRIPTION;
            }
            set
            {
                SelectReportGroup.DESCRIPTION = value;

                if (SelectReportGroup.DESCRIPTION != value)
                {
                    SelectReportGroup.DESCRIPTION = value;
                    OnPropertyChanged("DESCRIPTION");
                }
            }
        }

        private DateTime _DATE;
        public DateTime DATE
        {
            get
            {
                return SelectReportGroup.DATE;
            }
            set
            {
                SelectReportGroup.DATE = value;

                if (SelectReportGroup.DATE != value)
                {
                    SelectReportGroup.DATE = value;
                    OnPropertyChanged("DATE");
                }
            }
        }

        private long _REPORT_ID;
        public long REPORT_ID
        {
            get
            {
                return SelectReportGroup.REPORT_ID;
            }
            set
            {
                SelectReportGroup.REPORT_ID = value;

                if (SelectReportGroup.REPORT_ID != value)
                {
                    SelectReportGroup.REPORT_ID = value;
                    OnPropertyChanged("REPORT_ID");
                }
            }
        }

        private bool _IS_DELETE;
        public bool IS_DELETE
        {
            get
            {
                return SelectReportGroup.IS_DELETE;
            }
            set
            {
                SelectReportGroup.IS_DELETE = value;

                if (SelectReportGroup.IS_DELETE != value)
                {
                    SelectReportGroup.IS_DELETE = value;
                    OnPropertyChanged("IS_DELETE");
                }
            }
        }
        private long _COMPANY_ID;
        public long COMPANY_ID
        {
            get
            {
                return SelectReportGroup.COMPANY_ID;
            }
            set
            {
                SelectReportGroup.COMPANY_ID = value;

                if (SelectReportGroup.COMPANY_ID != value)
                {
                    SelectReportGroup.COMPANY_ID = value;
                    OnPropertyChanged("COMPANY_ID");
                }
            }
        }
        private long _CREATED_BY;
        public long CREATED_BY
        {
            get
            {
                return SelectReportGroup.CREATED_BY;
            }
            set
            {
                SelectReportGroup.CREATED_BY = value;

                if (SelectReportGroup.CREATED_BY != value)
                {
                    SelectReportGroup.CREATED_BY = value;
                    OnPropertyChanged("CREATED_BY");
                }
            }
        }

        public ICommand _InsertReportGroup;
        public ICommand InsertReportGroup
        {
            get
            {
                if (_InsertReportGroup == null)
                {
                    _InsertReportGroup = new DelegateCommand(Insert_ReportGroup);
                }
                return _InsertReportGroup;
            }
        }
        public async void Insert_ReportGroup()
        {
            SelectReportGroup.COMPANY_ID = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            //_opr.ITEM_NAME = ITEM_NAME;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            var response = await client.PostAsJsonAsync("api/ReportGroupAPI/ReportGroupAdd/", SelectReportGroup);
            if (response.StatusCode.ToString() == "OK")
            {
                MessageBox.Show("Report Group Added Successfully");
                Cancel_ReportGroup();
                ModalService.NavigateTo(new ReportList(), delegate(bool returnValue) { });
            }
        }
       

        ReportAddModel[] datah;
        List<ReportAddModel> _ListGrid_ReportGroup = new List<ReportAddModel>();
        public async Task<List<ReportAddModel>> GetReportGroup(long comp)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            HttpResponseMessage response = client.GetAsync("api/ReportGroupAPI/GetReportName?id=" + comp + "").Result;
            if (response.IsSuccessStatusCode)
            {
                datah = JsonConvert.DeserializeObject<ReportAddModel[]>(await response.Content.ReadAsStringAsync());
                int x = 0;
                for (int i = 0; i < datah.Length; i++)
                {
                    x++;
                    _ListGrid_ReportGroup.Add(new ReportAddModel
                    {

                        REPORT_NAME = datah[i].REPORT_NAME,
                        SEARCH_CODE = datah[i].SEARCH_CODE,
                        REPORT_GROUP_ID = datah[i].REPORT_GROUP_ID,
                        REPORT_ADD_ID = datah[i].REPORT_ADD_ID,
                    });
                }
            }
            ListGrid = _ListGrid_ReportGroup;

            return new List<ReportAddModel>(_ListGrid_ReportGroup);
        }
        public ICommand _Cancel;
        public ICommand Cancel
        {
            get
            {
                if (_Cancel == null)
                {
                    _Cancel = new DelegateCommand(Cancel_ReportGroup);
                }
                return _Cancel;
            }
        }



        public void Cancel_ReportGroup()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }

        public ICommand _ReportGroupClick;
        public ICommand ReportGroupClick
        {
            get
            {
                if (_ReportGroupClick == null)
                {
                    _ReportGroupClick = new DelegateCommand(ReportGroup_Click);
                }
                return _ReportGroupClick;
            }
        }

        public void ReportGroup_Click()
        {
            AddReportGroup IA = new AddReportGroup();
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
