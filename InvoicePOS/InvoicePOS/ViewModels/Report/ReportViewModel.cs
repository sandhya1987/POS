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
using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Customer;
using InvoicePOS.UserControll.Report;
using Newtonsoft.Json;

namespace InvoicePOS.ViewModels
{
    class ReportViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {

        public HttpResponseMessage response;
        private readonly ReportModel RModel;
        ReportModel _POModel = new ReportModel();
        public ICommand select { get; set; }
        // POrderModel[] data = null;
        //POrderModel[] data1 = null;
        // List<ReportModel> _ListGrid_Temp = new List<ReportModel>();
        //List<POrderModel> _ListGrid_Temp1 = new List<POrderModel>();
        int x = 0;
        ReportModel[] data = null;
        ObservableCollection<ReportModel> _ListGrid_Temp = new ObservableCollection<ReportModel>();

        public ReportViewModel()
        {
            //var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            var comp = 1;
            //if (App.Current.Properties["Action"].ToString() == "View")
            //{
            //    //SelectedPO = App.Current.Properties["POView"] as POrderModel;
            //    //App.Current.Properties["Action"] = "";
            //    //GetPoItem(comp);
            //}
            //else
            //{
                //UpdVisible = "Collapsed";
                //CreatVisible = "Visible";
                SelectedReport = new ReportModel();
                //PO_DATE = System.DateTime.Now;
                // SelectedPO.DELIVERY_DATE = System.DateTime.Now;
                GetReportList(comp);


            //}
        }


        private ReportModel _SelectedReport;
        public ReportModel SelectedReport
        {
            get { return _SelectedReport; }
            set
            {
                if (_SelectedReport != value)
                {
                    _SelectedReport = value;
                    OnPropertyChanged("SelectedReport");
                }

            }
        }

        private long _PO_ID;
        public long PO_ID
        {
            get
            {
                return SelectedReport.REPORT_ID;
            }
            set
            {
                SelectedReport.REPORT_ID = value;


                if (SelectedReport.REPORT_ID != value)
                {
                    SelectedReport.REPORT_ID = value;
                    OnPropertyChanged("REPORT_ID");
                }
            }
        }
        private string _REPORT_NAME;
        public string REPORT_NAME
        {
            get
            {
                return SelectedReport.REPORT_NAME;
            }
            set
            {
                SelectedReport.REPORT_NAME = value;


                if (SelectedReport.REPORT_NAME != value)
                {
                    SelectedReport.REPORT_NAME = value;
                    OnPropertyChanged("REPORT_NAME");
                }
            }
        }

        private DateTime _CREATION_DATE;
        public DateTime CREATION_DATE
        {
            get
            {
                return SelectedReport.CREATION_DATE;
            }
            set
            {
                SelectedReport.CREATION_DATE = DateTime.Now;


                if (SelectedReport.CREATION_DATE != value)
                {
                    SelectedReport.CREATION_DATE = DateTime.Now;
                    OnPropertyChanged("CREATION_DATE");
                }
            }
        }
        public ObservableCollection<ReportModel> _ListGrid { get; set; }
        public ObservableCollection<ReportModel> ListGrid
        {
            get
            {
                return _ListGrid;

            }
            set
            {
                if (value != _ListGrid)
                {
                    this._ListGrid = value;
                    OnPropertyChanged("ListGrid");
                }
            }
        }

        public async Task<ObservableCollection<ReportModel>> GetReportList(long comp)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/ReportAPI/GetReports?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<ReportModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        x++;

                        _ListGrid_Temp.Add(new ReportModel
                        {
                            SLNO = x,
                            REPORT_ID = data[i].REPORT_ID,
                            REPORT_NAME = data[i].REPORT_NAME,
                            IS_DELETE = data[i].IS_DELETE,
                            CREATION_DATE = data[i].CREATION_DATE,
                            CREATED_BY = data[i].CREATED_BY,
                            STATUS = data[i].STATUS,

                        });
                    }
                }
                ListGrid = _ListGrid_Temp;
                //App.Current.Properties["DepartmentList"] = ListGrid;
                return new ObservableCollection<ReportModel>(_ListGrid_Temp);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ICommand _ViewReport { get; set; }
        public ICommand ViewReport
        {
            get
            {
                if (_ViewReport == null)
                {
                    _ViewReport = new DelegateCommand(View_Report);
                }
                return _ViewReport;
            }
        }
        public async void View_Report()
        {
            if (SelectedReport.REPORT_ID != null && SelectedReport.REPORT_ID != 0)
            {
                //App.Current.Properties["Action"] = "View";
                if (SelectedReport.REPORT_NAME == "Customer Account summary")
                {
                    ViewLedger _VL = new ViewLedger();
                    _VL.Show();
                }

                if (SelectedReport.REPORT_NAME == "Purchase order summary")
                {
                    PO_Summery_Report _PS = new PO_Summery_Report();
                    _PS.Show();
                }

                if (SelectedReport.REPORT_NAME == "Sales Return report")
                {
                    Sales_Return_Report _SR = new Sales_Return_Report();
                    _SR.Show();
                }

                if (SelectedReport.REPORT_NAME == " Item weight multiply with item sold report")
                {
                    ItemWeight_SoldReport _WS = new ItemWeight_SoldReport();
                    _WS.Show();
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
