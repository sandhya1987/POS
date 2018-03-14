using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using InvoicePOS.UserControll.Company;
using InvoicePOS.Helpers;
using InvoicePOS.Models;
using Newtonsoft.Json;

namespace InvoicePOS.ViewModels.Company
{
    class CompaniesViewModel:DependencyObject, INotifyPropertyChanged, IModalService
    {
        CompanyModel[] data = null;
        List<CompanyModel> _ListGrid_Temp = new List<CompanyModel>();
         public CompaniesViewModel()
        {

                SelectedCompany = new CompanyModel();
                GetCompanies();
                SelectedCompany.CREATED_DATE = System.DateTime.Now;
              
         }

         # region Declaration
         private CompanyModel _SelectedCompany;
         public CompanyModel SelectedCompany
         {
             get { return _SelectedCompany; }
             set
             {
                 if (_SelectedCompany != value)
                 {
                     _SelectedCompany = value;
                     OnPropertyChanged("SelectedCompany");
                 }
             }
         }

        
         private string _COMPANY_NAME;
         public string COMPANY_NAME
         {
             get
             {
                 return SelectedCompany.COMPANY_NAME;
             }
             set
             {
                 SelectedCompany.COMPANY_NAME = value;
                 OnPropertyChanged("COMPANY_NAME");

             }
         }


         private string _COUNTRY;
         public string COUNTRY
         {
             get
             {
                 return SelectedCompany.COUNTRY;
             }
             set
             {
                 SelectedCompany.COUNTRY = value;
                 OnPropertyChanged("COUNTRY");

             }
         }


         private string _ADDRESS;
         public string ADDRESS
         {
             get
             {
                 return SelectedCompany.ADDRESS;
             }
             set
             {
                 SelectedCompany.ADDRESS = value;
                 OnPropertyChanged("ADDRESS");

             }
         }

         private string _POSTCODE;
         public string POSTCODE
         {
             get
             {
                 return SelectedCompany.POSTCODE;
             }
             set
             {
                 SelectedCompany.POSTCODE = value;
                 OnPropertyChanged("POSTCODE");

             }
         }

         private string _TELEPHONE;
         public string TELEPHONE
         {
             get
             {
                 return SelectedCompany.TELEPHONE;
             }
             set
             {
                 SelectedCompany.TELEPHONE = value;
                 OnPropertyChanged("TELEPHONE");

             }
         }

         private string _FAX;
         public string FAX
         {
             get
             {
                 return SelectedCompany.FAX;
             }
             set
             {
                 SelectedCompany.FAX = value;
                 OnPropertyChanged("FAX");

             }
         }

         private string _WEBSITE;
         public string WEBSITE
         {
             get
             {
                 return SelectedCompany.WEBSITE;
             }
             set
             {
                 SelectedCompany.WEBSITE = value;
                 OnPropertyChanged("WEBSITE");

             }
         }


         public List<CompanyModel> _ListGrid { get; set; }
         public List<CompanyModel> ListGrid
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
         #endregion Declaration

         #region GetCompany
         public async Task<ObservableCollection<CompanyModel>> GetCompanies()
         {
             try
             {
                 HttpClient client = new HttpClient();
                 client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                 client.DefaultRequestHeaders.Accept.Add(
                     new MediaTypeWithQualityHeaderValue("application/json"));
                 var response = client.GetAsync("api/CompanyAPI/GetCompanies").Result;
                 _ListGrid_Temp.Clear();
                 if (response.IsSuccessStatusCode)
                 {
                     data = JsonConvert.DeserializeObject<CompanyModel[]>(await response.Content.ReadAsStringAsync());
                     // EmployeeData = new List<EmployeeModel>();
                     int x = 0;

                     for (int i = 0; i < data.Length; i++)
                     {
                         x++;
                         _ListGrid_Temp.Add(new CompanyModel
                         {
                             SLNO = x,
                                                         
                             COMPANY_NAME = data[i].COMPANY_NAME,
                             ADDRESS = data[i].ADDRESS,
                             COUNTRY = data[i].COUNTRY,
                             FAX = data[i].FAX,
                             TELEPHONE = data[i].TELEPHONE,
                             POSTCODE = data[i].POSTCODE,
                             COMPANY_ID = data[i].COMPANY_ID,


                         });
                     }

                 }
                 // ListGrid.Clear();

                 ListGrid = _ListGrid_Temp;
                 //DeliveryAddress.ListGridRef.ItemsSource = ListGrid.ToString();
                 return new ObservableCollection<CompanyModel>(_ListGrid_Temp);
             }
             catch (Exception ex)
             {
                 throw;
             }
         }

         # endregion

         private ICommand _AddNewCompanyClick { get; set; }
         public ICommand AddNewCompanyAsistantClick
         {
             get
             {
                 if (_AddNewCompanyClick == null)
                 {
                     _AddNewCompanyClick = new DelegateCommand(AddNewCompanyAsistant_Click);
                 }
                 return _AddNewCompanyClick;
             }

         }

         public void AddNewCompanyAsistant_Click()
         {

             AddCompany _CA = new AddCompany();
             _CA.ShowDialog();
             foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
             {
                 if (window.DataContext == this)
                 {
                     window.Close();
                 }
             }

         }


         private ICommand _RegisterClick { get; set; }
         public ICommand RegisterClick
         {
             get
             {
                 if (_RegisterClick == null)
                 {
                     _RegisterClick = new DelegateCommand(Register_Click);
                 }
                 return _RegisterClick;
             }

         }

         public async void Register_Click()
            {
             
                try
                {

                    //if (selectCompany.START_YEAR == "" || selectCompany.START_YEAR == null)
                    //{
                    //    MessageBox.Show("Start_Year Should not be blank..");
                    //}
                    //else
                    {

                        HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                        var response = await client.PostAsJsonAsync("api/CompanyAPI/CreateCompanies/", SelectedCompany);
                        if (response.StatusCode.ToString() == "OK")
                        {

                            RegisterCompany _RC = new RegisterCompany();
                            _RC.ShowDialog();
                            Cancel_Company();
                        }
                    }
                }
                catch
                {

                }

         }
         # region Register Declaration


         public ICommand _MyUserId;
         public ICommand MyUserId
         {
             get
             {
                 if (_MyUserId == null)
                 {
                     _MyUserId = new DelegateCommand(MyUser_Id);
                 }
                 return _MyUserId;
             }

         }
         public string _ButtonText;
         public String ButtonText
         {
             get { return _ButtonText ?? (_ButtonText = "Define My Own"); }
             set
             {
                 _ButtonText = value;
                 NotifyPropertyChanged("ButtonText");
             }
         }

         public void MyUser_Id()
         {
             if (ButtonText == "Auto Generate")
             {
                 ButtonText = "Define My Own";
                 //LoginCredential.UserName.Text = App.Current.Properties["Company_Name"].ToString();
                 //_isviewmode = true;
                 //IsViewMode = true;
             }
             else if (ButtonText == "Define My Own")
             {
                 ButtonText = "Auto Generate";
                 //_isviewmode = false;
                 //IsViewMode = false;
                 //LoginCredential.UserName.Text = "";

             }


         }

         private string _PASSWORD;
         public string PASSWORD
         {
             get
             {
                 return SelectedCompany.PASSWORD;
             }
             set
             {
                 SelectedCompany.PASSWORD = value;
                 OnPropertyChanged("PASSWORD");

             }
         }

         private string _CONFIRM_PASSWORD;
         public string CONFIRM_PASSWORD
         {
             get
             {
                 return SelectedCompany.CONFIRM_PASSWORD;
             }
             set
             {
                 SelectedCompany.CONFIRM_PASSWORD = value;
                 OnPropertyChanged("CONFIRM_PASSWORD");

             }
         }


         private string _USER_NAME;
         public string USER_NAME
         {
             get
             {
                 return SelectedCompany.USER_NAME;
             }
             set
             {
                 SelectedCompany.USER_NAME = value;
                 OnPropertyChanged("USER_NAME");

             }
         }


         //private DateTime _LAST_LOGIN;
         //public DateTime LAST_LOGIN
         //{
         //    get
         //    {
         //        return selectCompany.LAST_LOGIN;
         //    }
         //    set
         //    {
         //        selectCompany.LAST_LOGIN = value;
         //        OnPropertyChanged("LAST_LOGIN");

         //    }
         //}



         private DateTime _CREATED_DATE;
         public DateTime CREATED_DATE
         {
             get
             {
                 return SelectedCompany.CREATED_DATE;
             }
             set
             {
                 SelectedCompany.CREATED_DATE = System.DateTime.Now;
                 OnPropertyChanged("CREATED_DATE");

             }
         }



         private ICommand _LoginClick { get; set; }
         public ICommand LoginClick
         {
             get
             {
                 if (_LoginClick == null)
                 {
                     _LoginClick = new DelegateCommand(Login_Click);
                 }
                 return _LoginClick;
             }

         }

         public async void Login_Click()
            {

                try
                {
                    if (SelectedCompany.USER_NAME == "" || SelectedCompany.USER_NAME == null)
                    {
                        MessageBox.Show("Mobile No is not blank");
                    }
                    else if (SelectedCompany.PASSWORD == "" || SelectedCompany.PASSWORD == null)
                    {
                        MessageBox.Show("Email is not blank");
                    }
                    else if (SelectedCompany.CONFIRM_PASSWORD == "" || SelectedCompany.CONFIRM_PASSWORD == null)
                    {
                        MessageBox.Show("Customer Group is not blank");
                    }

                    else if (!Regex.IsMatch(SelectedCompany.PASSWORD,

                    "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",

                   RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                    {
                        MessageBox.Show("Please check Password format");
                        return;
                    }

                    else
                    {

                        if (PASSWORD == CONFIRM_PASSWORD)
                        {
                            HttpClient client = new HttpClient();
                            client.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json"));
                            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                            var response = await client.PostAsJsonAsync("api/CompanyAPI/RegisterCompany/", SelectedCompany);
                            if (response.StatusCode.ToString() == "OK")
                            {


                                CompanyLogin _CL = new CompanyLogin();
                                _CL.ShowDialog();
                                Cancel_Company();
                            }
                        }

                        else
                        {
                            MessageBox.Show("Your Password Doesnot Matches to Confirm Password..");
                        }
                    }
                }
                catch
                {

                }
         }

         # endregion
         public ICommand _Cancel;
            public ICommand Cancel
            {
                get
                {
                    if (_Cancel == null)
                    {
                        _Cancel = new DelegateCommand(Cancel_Company);
                    }
                    return _Cancel;
                }
            }



            public void Cancel_Company()
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
