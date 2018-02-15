using InvoicePOS.Helpers;
using InvoicePOS.Models;
using InvoicePOS.UserControll.CashReg;
using InvoicePOS.UserControll.Company;
using InvoicePOS.Views.CashRegister;
using InvoicePOS.Views.Main;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InvoicePOS.ViewModels
{
    class ChangeBussinessLocationViewModel : DependencyObject, INotifyPropertyChanged, IModalService
    {
        private BusinessLocationModel _selectedItem;
        ItemModel[] data = null;
        public ICommand _ShowBussinessLocationList { get; set; }
        public ICommand ShowBussinessLocationList
        {
            get
            {
                if (_ShowBussinessLocationList == null)
                {
                    _ShowBussinessLocationList = new DelegateCommand(ShowBussinessLocation_List);
                }

                return _ShowBussinessLocationList;
            }
        }

        public void ShowBussinessLocation_List()
        {
            if (App.Current.Properties["SelectedItem"] != null)
            {
                Window_BusinessLocationList IA = new Window_BusinessLocationList();
                IA.Show();
                if (App.Current.Properties["BussLocName"] != null)
                {
                    //InvoicePOS.Views.CashRegister.CashReg.CashRegName.Text = _selectedItem.CASH_REG_NAME;
                    //textBox5.Text = "";
                    var data = App.Current.Properties["BussLocName"] as BusinessLocationModel;
                    //Main.CashRegisterName.Text = CashReg.CashRegName.Text;
                    //CashRegisterAmountDetails.BusLocationName.Text = CashReg.CashRegName.Text;
                    InvoicePOS.Views.CashRegister.ChangeBussinessLocation.BussinessLocationName.Text = data.BUSS_ADDRESS_1;
                }
            }
        }
        public ICommand _ShowCashReg { get; set; }
        public ICommand ShowCashReg
        {
            get
            {
                if (_ShowCashReg == null)
                {
                    _ShowCashReg = new DelegateCommand(ShowCashReg_Ok);
                }

                return _ShowCashReg;
            }
        }
        public void ShowCashReg_Ok()
        {
            Window_CashRegList IA = new Window_CashRegList();
            IA.Show();
        }
        public ICommand _ClickOk { get; set; }
        public ICommand ClickOk
        {
            get
            {
                if (_ClickOk == null)
                {
                    _ClickOk = new DelegateCommand(Click_Ok);
                }

                return _ClickOk;
            }
        }
        public void Click_Ok()
        {
            //MainViewModel view = new MainViewModel();
            InvoicePOS.Views.Main.Main.BusinessLocName.Text = ChangeBussinessLocation.BussinessLocationName.Text;
            InvoicePOS.Views.Main.Main.BusinessAddress.Text = ChangeBussinessLocation.Address.Text;
            //view.BusinessLocName = ChangeBussinessLocation.BussinessLocationName.Text;
            //view.BusinessLocAddress = ChangeBussinessLocation.BussinessLocationName.Text;
            App.Current.Properties["BusinessLocName"] = ChangeBussinessLocation.BussinessLocationName.Text;
            App.Current.Properties["BusinessLocAddress"] = ChangeBussinessLocation.Address.Text;
            
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void GoBackward(bool dialogReturnValue)
        {
            throw new NotImplementedException();
        }

        public void NavigateTo(UserControl uc, BackNavigationEventHandler backFromDialog)
        {
            throw new NotImplementedException();
        }
    }
}
