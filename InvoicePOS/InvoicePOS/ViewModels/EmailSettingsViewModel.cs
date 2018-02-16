using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using InvoicePOS.Models;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using InvoicePOS.Helpers;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json;
using InvoicePOS.Views;

namespace InvoicePOS.ViewModels
{
    class EmailSettingsViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        #region constructor and methods
        EmailSettingsModel[] data = null;
        ObservableCollection<EmailSettingsModel> tmpListGrid = new ObservableCollection<EmailSettingsModel>();
        EmailSettingsModel copiedEmailSettingsModel = null;

        public EmailSettingsViewModel()
        {
            GetEmailsForUser();
        }

        public async Task<ObservableCollection<EmailSettingsModel>> GetEmailsForUser()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            //long userId = 1;
            long userId = Convert.ToInt32(App.Current.Properties["User_Id"].ToString());
            HttpResponseMessage response = client.GetAsync("api/EmailSettingsAPI/GetEmailSettings?userId=" + userId + "").Result;
            if (response.IsSuccessStatusCode)
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                string str = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<EmailSettingsModel[]>(str, settings);
                tmpListGrid.Clear();
                for (int i = 0; i < data.Length; i++)
                    {
                        tmpListGrid.Add(new EmailSettingsModel
                        {

                            BCC = data[i].BCC,
                            CC = data[i].CC,
                            EMAIL = data[i].EMAIL,
                            ID = data[i].ID,
                            IS_GMAIL = data[i].IS_GMAIL,
                            IS_REQ_ENCRYPT_CONN = data[i].IS_REQ_ENCRYPT_CONN,
                            NAME = data[i].NAME,
                            PASSWORD = data[i].PASSWORD,
                            SMTP_SERVER = data[i].SMTP_SERVER,
                            SMTP_SERVER_PORT = data[i].SMTP_SERVER_PORT,
                            USER_ID = data[i].USER_ID,
                            USER_NAME = data[i].USER_NAME,
                            MAIL_TYPE = data[i].MAIL_TYPE,

                        });
                    }
                }
            EmailSettingsModel tmpEmailSettingsModel;
            if (tmpListGrid.Count == 0)
            {
                tmpEmailSettingsModel = new EmailSettingsModel();
                tmpEmailSettingsModel.MAIL_TYPE = "System Email";
                tmpListGrid.Add(tmpEmailSettingsModel);
                tmpEmailSettingsModel = new EmailSettingsModel();
                tmpEmailSettingsModel.MAIL_TYPE = "Online Product Enquiry Email";
                tmpListGrid.Add(tmpEmailSettingsModel);
            }
            else if (tmpListGrid.Count == 1)
            {
                if (tmpListGrid[0].MAIL_TYPE == "System Email")
                {
                    tmpEmailSettingsModel = new EmailSettingsModel();
                    tmpEmailSettingsModel.MAIL_TYPE = "Online Product Enquiry Email";
                    tmpListGrid.Add(tmpEmailSettingsModel);
                }
                else if (tmpListGrid[0].MAIL_TYPE == "Online Product Enquiry Email")
                {
                    tmpEmailSettingsModel = new EmailSettingsModel();
                    tmpEmailSettingsModel.MAIL_TYPE = "System Email";
                    tmpListGrid.Add(tmpEmailSettingsModel);
                }
            }

            ListGrid = tmpListGrid;
            return new ObservableCollection<EmailSettingsModel>(tmpListGrid);
        }


        #endregion

        #region DataBinding
        public ObservableCollection<EmailSettingsModel> _ListGrid { get; set; }
        public ObservableCollection<EmailSettingsModel> ListGrid
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

        private EmailSettingsModel _SelectedEmailSettingsModel;
        public EmailSettingsModel SelectedEmailSettingsModel
        {
             get
            {
                return _SelectedEmailSettingsModel;
            }
            set
            {
                this._SelectedEmailSettingsModel = value;
                OnPropertyChanged("SelectedEmailSettingsModel");
            }
        }


        #endregion

        #region command binding

        private ICommand _EditButtonClick;
        public ICommand EditButtonClick
        {
            get
            {
                if (_EditButtonClick == null)
                {
                    _EditButtonClick = new DelegateCommand(_EditButton_Click);
                }
                return _EditButtonClick;
            }
        }

        public void _EditButton_Click()
        {
            if (SelectedEmailSettingsModel == null)
            {
                MessageBox.Show("Select a item to edit");
            }
            else
            {
                EditEmailSettings editES = new EditEmailSettings();
                editES.DataContext = this;

                if (SelectedEmailSettingsModel.IS_GMAIL == false)
                {
                    editES.UsingAnotherAccount.IsChecked = true;
                }
                editES.Show();
            }

        }

        private ICommand _ViewButtonClick;
        public ICommand ViewButtonClick
        {
            get
            {
                if (_ViewButtonClick == null)
                {
                    _ViewButtonClick = new DelegateCommand(_ViewButton_Click);
                }
                return _ViewButtonClick;
            }
        }

        public void _ViewButton_Click()
        {
            if (SelectedEmailSettingsModel == null)
            {
                MessageBox.Show("Select a item to view");
            }
            else
            {
                EditEmailSettings editES = new EditEmailSettings();
                editES.DataContext = this;

                if (SelectedEmailSettingsModel.IS_GMAIL == false)
                {
                    editES.UsingAnotherAccount.IsChecked = true;
                }

                editES.IsEnabled = false;
                editES.Show();
            }

        }

        private ICommand _DeleteButtonClick;
        public ICommand DeleteButtonClick
        {
            get
            {
                if (_DeleteButtonClick == null)
                {
                    _DeleteButtonClick = new DelegateCommand(_DeleteButton_Click);
                }
                return _DeleteButtonClick;
            }
        }

        public async void _DeleteButton_Click()
        {
            if (SelectedEmailSettingsModel == null)
            {
                MessageBox.Show("Select a item to delete");
            }
            else
            {
                MessageBoxResult dialogResult = MessageBox.Show("Are you sure you want to delete selected Email Settings", "Confirm", System.Windows.MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    SelectedEmailSettingsModel.USER_ID = Convert.ToInt32(App.Current.Properties["User_Id"].ToString());
                    //SelectedEmailSettingsModel.USER_ID = 1;
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = new TimeSpan(500000000000);
                    var response = await client.PostAsJsonAsync("api/EmailSettingsAPI/DeleteEmailSettings/", SelectedEmailSettingsModel);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        MessageBox.Show("Email Settings Deleted Successfully");
                    }
                    GetEmailsForUser();
                    NotifyPropertyChanged("ListGrid");
                }
                
            }

        }

        private ICommand _Cancel;
        public ICommand Cancel
        {
            get
            {
                if (_Cancel == null)
                {
                    _Cancel = new DelegateCommand(Cancel_Click);
                }
                return _Cancel;
            }
        }

        public void Cancel_Click()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
            {
                if (App.Current.Windows[intCounter].Title == "EditEmailSettings")
                {
                    App.Current.Windows[intCounter].Close();
                }
            }
        }


        private ICommand _Update;
        public ICommand Update
        {
            get
            {
                if (_Update == null)
                {
                    _Update = new DelegateCommand(Update_Click);
                }
                return _Update;
            }
        }

        public async void Update_Click()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            SelectedEmailSettingsModel.USER_ID = Convert.ToInt32(App.Current.Properties["User_Id"].ToString());
            //SelectedEmailSettingsModel.USER_ID = 1;
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            if (SelectedEmailSettingsModel.IS_GMAIL == true)
            {
                SelectedEmailSettingsModel.IS_REQ_ENCRYPT_CONN = null;
                SelectedEmailSettingsModel.SMTP_SERVER = null;
                SelectedEmailSettingsModel.SMTP_SERVER_PORT = null;
            }
            var response = await client.PostAsJsonAsync("api/EmailSettingsAPI/UpdateEmailSettings/", SelectedEmailSettingsModel);
            if (response.StatusCode.ToString() == "OK")
            {
                MessageBox.Show("Email Settings Updated Successfully");
            }
            GetEmailsForUser();
            NotifyPropertyChanged("ListGrid");
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
            {
                if (App.Current.Windows[intCounter].Title == "EditEmailSettings")
                {
                    App.Current.Windows[intCounter].Close();
                }
            }
        }


        private ICommand _CopyButtonClick;
        public ICommand CopyButtonClick
        {
            get
            {
                if (_CopyButtonClick == null)
                {
                    _CopyButtonClick = new DelegateCommand(_CopyButton_Click);
                }
                return _CopyButtonClick;
            }
        }

        public  void _CopyButton_Click()
        {
            if (SelectedEmailSettingsModel == null)
            {
                MessageBox.Show("Select a item to copy");
            }
            else
            {
                copiedEmailSettingsModel = SelectedEmailSettingsModel;

            }

        }

        private ICommand _PasteButtonClick;
        public ICommand PasteButtonClick
        {
            get
            {
                if (_PasteButtonClick == null)
                {
                    _PasteButtonClick = new DelegateCommand(_PasteButton_Click);
                }
                return _PasteButtonClick;
            }
        }

        public async void _PasteButton_Click()
        {
            if (SelectedEmailSettingsModel == null)
            {
                MessageBox.Show("Select a item to paste");
            }
            else
            {
                if (copiedEmailSettingsModel != null)
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                    SelectedEmailSettingsModel.USER_ID = Convert.ToInt32(App.Current.Properties["User_Id"].ToString());
                    //SelectedEmailSettingsModel.USER_ID = 1;
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = new TimeSpan(500000000000);
                    SelectedEmailSettingsModel.BCC = copiedEmailSettingsModel.BCC;
                    SelectedEmailSettingsModel.CC = copiedEmailSettingsModel.CC;
                    SelectedEmailSettingsModel.EMAIL = copiedEmailSettingsModel.EMAIL;
                    SelectedEmailSettingsModel.IS_GMAIL = copiedEmailSettingsModel.IS_GMAIL;
                    SelectedEmailSettingsModel.IS_REQ_ENCRYPT_CONN = copiedEmailSettingsModel.IS_REQ_ENCRYPT_CONN;
                    SelectedEmailSettingsModel.NAME = copiedEmailSettingsModel.NAME;
                    SelectedEmailSettingsModel.PASSWORD = copiedEmailSettingsModel.PASSWORD;
                    SelectedEmailSettingsModel.SMTP_SERVER = copiedEmailSettingsModel.SMTP_SERVER;
                    SelectedEmailSettingsModel.SMTP_SERVER_PORT = copiedEmailSettingsModel.SMTP_SERVER;
                    SelectedEmailSettingsModel.USER_NAME = copiedEmailSettingsModel.USER_NAME;

                    var response = await client.PostAsJsonAsync("api/EmailSettingsAPI/UpdateEmailSettings/", SelectedEmailSettingsModel);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        MessageBox.Show("Email Settings Updated Successfully");
                    }
                    GetEmailsForUser();
                    NotifyPropertyChanged("ListGrid");
                }

            }

        }
        #endregion 


        #region interface implementation
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
        #endregion
    }
}
