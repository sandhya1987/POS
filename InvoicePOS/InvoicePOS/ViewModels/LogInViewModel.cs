using InvoicePOS.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using InvoicePOS.Views.Company;
using InvoicePOS.Views.Main;
using System.Net.Http;
using InvoicePOS.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using InvoicePOS.Views.LogIn;
using System.Collections.ObjectModel;

namespace InvoicePOS.ViewModels
{
    public class LogInViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        LogInModel data = null;
        UserAccessModel[] data3 = null;
        public LogInViewModel()
        {


        }
        public ICommand _CancelLogIn;
        public ICommand CancelLogIn
        {
            get
            {
                if (_CancelLogIn == null)
                {
                    _CancelLogIn = new DelegateCommand(Cancel_LogIn);
                }
                return _CancelLogIn;
            }
        }



        public void Cancel_LogIn()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }


        public ICommand _LogInClick { get; set; }
        public ICommand LogInClick
        {
            get
            {
                if (_LogInClick == null)
                {

                    _LogInClick = new DelegateCommand(LogIn_Click);
                }
                return _LogInClick;
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

        ObservableCollection<UserAccessModel> _ListGrid_Temp1 = new ObservableCollection<UserAccessModel>();
        public async void LogIn_Click()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            HttpResponseMessage response = client.GetAsync("api/LogInAPI/GetUser?id=" + USERNAME + "&password=" + PASSWORD + "").Result;
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<LogInModel>(await response.Content.ReadAsStringAsync());
                if (data != null)
                {
                    //var comp = Convert.ToInt32(App.Current.Properties["Company_Id"].ToString());
                    App.Current.Properties["Company_Id"] = data.COMPANY_ID;
                    App.Current.Properties["User_Id"] = data.USER_ID;

                    
                    HttpResponseMessage response3 = client.GetAsync("api/AccessRightAPI/GetAccessRights?EId=" + data.USER_ID + "").Result;
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
                                // Company_Id = data3[i].Company_Id,
                             
                                EDIT = data3[i].EDIT,
                                EXPT_TO_EXCEL = data3[i].EXPT_TO_EXCEL,
                               // ID = data3[i].ID,
                                IMORT_TO_EXCEL = data3[i].IMORT_TO_EXCEL,
                                MAILBACK = data3[i].MAILBACK,
                                MESSAGE = data3[i].MESSAGE,
                                MODULE_ID = data3[i].MODULE_ID,
                                MODULE_NAME = data3[i].MODULE_NAME,
                                NOTIFICATION = data3[i].NOTIFICATION,
                               // ROLE_ID = data3[i].ROLE_ID,
                               // User_Id = data3[i].User_Id,
                                VERIFICATION = data3[i].VERIFICATION,
                            });
                        }
                    }
                    App.Current.Properties["AccessModuleByUser"] = _ListGrid_Temp1;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Your Credentials is wrong");
                }
            }            
        }
        private string _USERNAME;
        public string USERNAME
        {
            get
            {
                return _USERNAME;
            }
            set
            {
                _USERNAME = value;
                OnPropertyChanged("USER_NAME");
            }
        }
        private string _PASSWORD;
        public string PASSWORD
        {
            get
            {
                return _PASSWORD;
            }
            set
            {
                _PASSWORD = value;
                OnPropertyChanged("PASSWORD");
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
