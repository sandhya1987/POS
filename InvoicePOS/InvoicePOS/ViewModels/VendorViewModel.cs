using InvoicePOS.Helpers;
using InvoicePOS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    public class VendorViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {



        public HttpResponseMessage response;
        private readonly VendorModel VNDModel;
        VendorModel _VendorModel = new VendorModel();
        public ICommand select { get; set; }
        VendorModel[] data = null;


        private VendorModel _SelectedVendor;
        public VendorModel SelectedVendor
        {
            get { return _SelectedVendor; }
            set
            {
                if (_SelectedVendor != value)
                {
                    _SelectedVendor = value;
                    OnPropertyChanged("SelectedVendor");
                }

            }
        }




        public ICommand _InsertVendor { get; set; }
        public ICommand InsertVendor
        {
            get
            {
                if (_InsertVendor == null)
                {

                    _InsertVendor = new DelegateCommand(Add_Vendor);
                }
                return _InsertVendor;
            }
        }

        public async void Add_Vendor()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(500000000000);
            var response = await client.PostAsJsonAsync("api/VendorAPI/VendorAdd/", SelectedVendor);



        }
        private List<VendorModel> _VendorData;
        public List<VendorModel> VendorData
        {
            get { return _VendorData; }
            set
            {
                if (_VendorData != value)
                {
                    _VendorData = value;
                }
            }
        }
        public async void GetVendor(int VendorId)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GlobalData.gblApiAdress);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            response = client.GetAsync("api/VendorAPI/SelectVendor?id=" + VendorId + "").Result;
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<VendorModel[]>(await response.Content.ReadAsStringAsync());
                VendorData = new List<VendorModel>();
                for (int i = 0; i < data.Length; i++)
                {
                    //int Vendor_Id = Convert.ToInt32(data[i].EMPLOYEE_ID);
                    //string VendorName = data[i].FIRST_NAME;
                    //string Address1 = data[i].ADDRESS_1;
                    //string Address2 = data[i].ADDRESS_2;
                    //string City = data[i].CITY;
                    VendorData.Add(new VendorModel
                    {
                       

                    });
                }
            }


        }






















        public VendorViewModel()
        { 
        
        
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
