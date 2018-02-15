using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.Item;
using InvoicePOS.UserControll.Report;
using InvoicePOS.UserControll.Report.PO_Report;
using InvoicePOS.Views.Report;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace InvoicePOS.ViewModels
{
    public class ReportViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {

        private List<ReportGroupModel> _children;
        private List<ReportGroupModel> _childrenGroup;
        readonly ReportViewModel _parent;

        readonly ChildViewModel _rootPerson;

        public ReportViewModel Parent
        {
            get { return _parent; }
        }

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
        public List<ReportGroupModel> ChildrenGroup
        {
            get { return _childrenGroup; }
            set
            {
                this._childrenGroup = value;
                OnPropertyChanged("ChildrenGroup");
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
        ReportGroupModel[] dataH;
        ObservableCollection<ReportGroupModel> _ListHirarchy_ReportGroup = new ObservableCollection<ReportGroupModel>();

        public async void ReportGroupHirarchy()
        {
            var cid = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            HttpResponseMessage response = client.GetAsync("api/ReportGroupAPI/GetReportHirarchy?id=" + cid + "").Result;
            if (response.IsSuccessStatusCode)
            {
                dataH = JsonConvert.DeserializeObject<ReportGroupModel[]>(await response.Content.ReadAsStringAsync());
                int x = 0;
                for (int i = 0; i < dataH.Length; i++)
                {
                    x++;
                    _ListHirarchy_ReportGroup.Add(new ReportGroupModel
                    {
                        // REPORT_GRP_ID = dataH[i].REPORT_GRP_ID,
                        GROUP_NAME = dataH[i].GROUP_NAME,
                        // REPORT_ID = dataH[i].REPORT_ID,
                        REPORT_CHILD_ID = dataH[i].REPORT_CHILD_ID,
                        REPORT_PARENT_ID = dataH[i].REPORT_PARENT_ID,
                        REPORT_NAME = dataH[i].REPORT_NAME,
                    });
                }
            }
            App.Current.Properties["Report_Hirarchy"] = _ListHirarchy_ReportGroup.ToList();
        }






        public ReportViewModel()
        {
            SelectReportGroup = new ReportGroupModel();
            SelectReportAdd = new ReportAddModel();
            ReportGroupModel item1 = new ReportGroupModel();
            var cid = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
            ReportGroupModel root = new ReportGroupModel();
            ReportGroupHirarchy();
            var dfr = (App.Current.Properties["Report_Hirarchy"] as List<ReportGroupModel>);
            var group = (from m in dfr select m);

            foreach (var item in group)
            {
                if (item.REPORT_CHILD_ID == 0)
                {
                    if (root.Children != null && root.Children.Count > 0)
                    {
                        foreach (var item5 in root.Children)
                        {
                            if (item5.REPORT_PARENT_ID == item.REPORT_CHILD_ID)
                            {

                            }
                            else
                            {
                                item1 = new ReportGroupModel() { GROUP_NAME = item.GROUP_NAME, REPORT_PARENT_ID = item.REPORT_PARENT_ID };

                            }
                        }
                    }
                    else
                    {

                        item1 = new ReportGroupModel() { GROUP_NAME = item.GROUP_NAME, REPORT_PARENT_ID = item.REPORT_PARENT_ID };
                    }

                    foreach (var item3 in group)
                    {
                        if (item.REPORT_PARENT_ID == item3.REPORT_CHILD_ID)
                        {

                            item1.Children.Add(new ReportGroupModel() { GROUP_NAME = item3.GROUP_NAME, REPORT_PARENT_ID = item3.REPORT_PARENT_ID });

                        }
                    }
                    root.Children.Add(item1);
                }
            }
            ChildrenGroup = (root.Children).ToList();
            App.Current.Properties["ChildrenGroup"] = ChildrenGroup;
            Children = ChildrenGroup;

            GetReportGroup(cid);

        }


        //  PrintSetup


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

        public ICommand _PagePreview;
        public ICommand PagePreview
        {
            get
            {
                if (_PagePreview == null)
                {
                    _PagePreview = new DelegateCommand(Page_Preview);
                }
                return _PagePreview;
            }
        }

        public void Page_Preview()
        {

            //PrintPreview PP = new PrintPreview();
            //PP.Show();

            var pd = new PrintDialog();

            // calculate page size
            var sz = new Size(pd.PrintableAreaWidth, pd.PrintableAreaHeight);

            // create paginator
            //   var paginator = new FlexPaginator(_flex, ScaleMode.PageWidth, sz, new Thickness(96 / 4), 100);
            string tempFileName = System.IO.Path.GetTempFileName();

            File.Delete(tempFileName);

            using (XpsDocument xpsDocument = new XpsDocument(tempFileName, FileAccess.ReadWrite))
            {
                XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
                //   writer.Write(paginator);
                DocumentViewer previewWindow = new DocumentViewer
                {
                    Document = xpsDocument.GetFixedDocumentSequence()
                };

                ReportAddModel gft = App.Current.Properties["ReportAdd"] as ReportAddModel;
                if (gft.REPORT_NAME == "All Items")
                {
                    Window printpriview = new Window();
                    EstimateInvoice Pageinvocie = new EstimateInvoice();
                    printpriview.Content = Pageinvocie.grid11;
                    printpriview.Title = "C1FlexGrid: Print Preview";
                    printpriview.Show();
                }
                else if (gft.REPORT_NAME == "All PO")
                {
                    Window printpriview = new Window();
                    ReportPO Pageinvocie = new ReportPO();
                    
                    printpriview.Content = Pageinvocie.grid11;
                    printpriview.Title = "C1FlexGrid: Print Preview";
                    printpriview.Show();
                   
                   // Pageinvocie.Show();
                }
                else if (gft.REPORT_NAME == "Purchage Return")
                {

                }
                else if (gft.REPORT_NAME == "Manage Purchage")
                {

                }



            }

        }




        public ICommand _PagePrint;
        public ICommand PagePrint
        {
            get
            {
                if (_PagePrint == null)
                {
                    _PagePrint = new DelegateCommand(Page_Print);
                }
                return _PagePrint;
            }
        }

        public void Page_Print()
        {
            DateTime to = Convert.ToDateTime(ToDate);
            DateTime From = Convert.ToDateTime(FromDate);
            PrintDialog PD = new PrintDialog();

            PD.ShowDialog();
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
            Window_ItemList IA = new Window_ItemList();
            IA.Show();

        }


        public ICommand _PrintSetup;
        public ICommand PrintSetup
        {
            get
            {
                if (_PrintSetup == null)
                {
                    _PrintSetup = new DelegateCommand(Double_click);
                }
                return _PrintSetup;
            }
        }

        //public ICommand _Doubleclick;
        //public ICommand Doubleclick
        //{
        //    get
        //    {
        //        if (_Doubleclick == null)
        //        {
        //            _Doubleclick = new DelegateCommand(Double_click);
        //        }
        //        return _Doubleclick;
        //    }
        //}


        public void Double_click()
        {
            if (SelectReportGroup.REPORT_GRP_ID != null && SelectReportGroup.REPORT_GRP_ID == 0)
            {

                GenerateReport GR = new GenerateReport();
                GR.Show();
            }
            else
            {
                MessageBox.Show("Select Report Group");
            }


        }



        private long _ITEM_UNIQUE_NAME;
        public long ITEM_UNIQUE_NAME
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

        public List<ReportGroupModel> Children
        {
            get { return _children; }
            set
            {
                this._children = value;
                OnPropertyChanged("Children");
            }
        }

        ReportAddModel[] data;
        List<ReportAddModel> _ListGrid_ReportGroup = new List<ReportAddModel>();
        public async Task<List<ReportAddModel>> GetReportGroup(long comp)
        {
            SelectReportAdd = new ReportAddModel();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            HttpResponseMessage response = client.GetAsync("api/ReportGroupAPI/GetReportName?id=" + comp + "").Result;
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<ReportAddModel[]>(await response.Content.ReadAsStringAsync());
                int x = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    x++;
                    _ListGrid_ReportGroup.Add(new ReportAddModel
                    {

                        REPORT_NAME = data[i].REPORT_NAME,
                        SEARCH_CODE = data[i].SEARCH_CODE,
                        REPORT_GROUP_ID = data[i].REPORT_GROUP_ID,
                        REPORT_ADD_ID = data[i].REPORT_ADD_ID,
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




        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is expanded.
        /// </summary>
        private bool _isExpanded;
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
                if (_isExpanded && _parent != null)
                    _parent.IsExpanded = true;
            }
        }




        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is selected.
        /// </summary>
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
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
        public ICommand _ReportAddClick;
        public ICommand ReportAddClick
        {
            get
            {
                if (_ReportAddClick == null)
                {
                    _ReportAddClick = new DelegateCommand(ReportAdd_Click);
                }
                return _ReportAddClick;
            }
        }

        public void ReportAdd_Click()
        {
            ReportAdd Iaa = new ReportAdd();
            Iaa.Show();

        }
        public ICommand _InsertReportAdd;
        public ICommand InsertReportAdd
        {
            get
            {
                if (_InsertReportAdd == null)
                {
                    _InsertReportAdd = new DelegateCommand(Insert_ReportAdd);
                }
                return _InsertReportAdd;
            }
        }
        public async void Insert_ReportAdd()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            var response = await client.PostAsJsonAsync("api/ReportGroupAPI/ReportAdd/", SelectReportAdd);
            if (response.StatusCode.ToString() == "OK")
            {
                MessageBox.Show("Report Added Successfully");
                Cancel_ReportGroup();
                ModalService.NavigateTo(new ReportList(), delegate (bool returnValue) { });
            }
        }

        private ICommand _selectedItemChangedCommand;
        public ICommand SelectedItemChangedCommand
        {
            get
            {
                if (_selectedItemChangedCommand == null)
                    _selectedItemChangedCommand = new RelayCommand(args => SelectedItemChanged(args));
                return _selectedItemChangedCommand;
            }
        }

        private void SelectedItemChanged(object args)
        {
            var gft = REPORT_GROUP_ID;

            var gftn = REPORT_NAME;
            //Cast your object
        }



        private static object _selectedItem = null;
        // This is public get-only here but you could implement a public setter which also selects the item.
        // Also this should be moved to an instance property on a VM for the whole tree, otherwise there will be conflicts for more than one tree.
        public static object SelectedItem
        {
            get { return _selectedItem; }
            private set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnSelectedItemChanged();
                }
            }
        }


        public static void OnSelectedItemChanged()
        {
            // Raise event / do other things
        }



        private ReportAddModel _SelectReportAdd;
        public ReportAddModel SelectReportAdd
        {
            get { return _SelectReportAdd; }
            set
            {
                if (_SelectReportAdd != value)
                {
                    _SelectReportAdd = value;
                    if (SelectReportAdd.REPORT_NAME!=null)
                    {
                        App.Current.Properties["ReportAdd"] = _SelectReportAdd;
                    }
                   
                    OnPropertyChanged("SelectReportAdd");
                }
            }
        }
        private long _REPORT_ADD_ID;
        public long REPORT_ADD_ID
        {
            get
            {
                return SelectReportAdd.REPORT_ADD_ID;
            }
            set
            {
                SelectReportAdd.REPORT_ADD_ID = value;

                if (SelectReportAdd.REPORT_ADD_ID != value)
                {
                    SelectReportAdd.REPORT_ADD_ID = value;
                    OnPropertyChanged("REPORT_ADD_ID");
                }
            }
        }
        private string _DESCRIPTION_ADD;
        public string DESCRIPTION_ADD
        {
            get
            {
                return SelectReportAdd.DESCRIPTION_ADD;
            }
            set
            {
                SelectReportAdd.DESCRIPTION_ADD = value;

                if (SelectReportAdd.DESCRIPTION_ADD != value)
                {
                    SelectReportAdd.DESCRIPTION_ADD = value;
                    OnPropertyChanged("DESCRIPTION_ADD");
                }
            }
        }
        private string _SEARCH_CODE;
        public string SEARCH_CODE
        {
            get
            {
                return SelectReportAdd.SEARCH_CODE;
            }
            set
            {
                SelectReportAdd.SEARCH_CODE = value;

                if (SelectReportAdd.SEARCH_CODE != value)
                {
                    SelectReportAdd.SEARCH_CODE = value;
                    OnPropertyChanged("SEARCH_CODE");
                }
            }
        }
        private string _REPORT_NAME;
        public string REPORT_NAME
        {
            get
            {
                return SelectReportAdd.REPORT_NAME;
            }
            set
            {
                SelectReportAdd.REPORT_NAME = value;

                if (SelectReportAdd.REPORT_NAME != value)
                {
                    SelectReportAdd.REPORT_NAME = value;
                    OnPropertyChanged("REPORT_NAME");
                }
            }
        }










        private long _REPORT_GROUP_ID;
        public long REPORT_GROUP_ID
        {
            get
            {
                return SelectReportAdd.REPORT_GROUP_ID;
            }
            set
            {
                SelectReportAdd.REPORT_GROUP_ID = value;

                if (SelectReportAdd.REPORT_GROUP_ID != value)
                {
                    SelectReportAdd.REPORT_GROUP_ID = value;
                    OnPropertyChanged("REPORT_GROUP_ID");
                }
            }
        }
        private DateTime _ToDate;
        public DateTime ToDate
        {
            get
            {
                return _ToDate;
            }
            set
            {
                _ToDate = value;

                if (_ToDate != value)
                {
                    _ToDate = value;
                    OnPropertyChanged("ToDate");
                }
            }
        }
        private DateTime _FromDate;
        public DateTime FromDate
        {
            get
            {
                return _FromDate;
            }
            set
            {
                _FromDate = value;

                if (_FromDate != value)
                {
                    _FromDate = value;
                    OnPropertyChanged("FromDate");
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
