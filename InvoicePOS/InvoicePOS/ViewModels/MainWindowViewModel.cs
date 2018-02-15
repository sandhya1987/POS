using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using InvoicePOS.Helpers;
using InvoicePOS.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace InvoicePOS.ViewModels
{
    public class MainWindowViewModel : BaseViewModel, INotifyPropertyChanged
    {
        #region Properties

        #region MyDateTime

        private DateTime _myDateTime;
        public DateTime MyDateTime
        {
            get { return _myDateTime; }
            set
            {
                if (_myDateTime != value)
                {
                    _myDateTime = value;
                    OnPropertyChanged("MyDateTime");
                }
            }
        }
        private string _Select_BarCode;
        public string Select_BarCode
        {
            get { return _Select_BarCode; }
            set
            {
                _Select_BarCode = value;

              //  GetItem(_Select_BarCode);
                // call your required
                // method here
                this.NotifyPropertyChanged("Select_BarCode");
            }
        }
        
       
        #endregion

        #region PersonsCollection

        private ObservableCollection<Person> _personsCollection;
        public ObservableCollection<Person> PersonsCollection
        {
            get { return _personsCollection; }
            set
            {
                if (_personsCollection != value)
                {
                    _personsCollection = value;
                    OnPropertyChanged("PersonsCollection");
                }
            }
        }

        #endregion

        #endregion

        #region Commands

        public ICommand RefreshDateCommand { get { return new DelegateCommand(OnRefreshDate); } }
        public ICommand RefreshPersonsCommand { get { return new DelegateCommand(OnRefreshPersons); } }
        public ICommand DoNothingCommand { get { return new DelegateCommand(OnDoNothing, CanExecuteDoNothing); } }

        #endregion

        #region Ctor
        public MainWindowViewModel()
        {
            
            RandomizeData();
        }
        #endregion

        #region Command Handlers

        private void OnRefreshDate()
        {
            MyDateTime = DateTime.Now;
        }

        private void OnRefreshPersons()
        {
            RandomizeData();
        }

        private void OnDoNothing()
        {
        }

        private bool CanExecuteDoNothing()
        {
            return false;
        }

        #endregion

        private void RandomizeData()
        {
            PersonsCollection = new ObservableCollection<Person>();

            for (var i = 0; i < 10; i++)
            {
                PersonsCollection.Add(new Person(
                    RandomHelper.RandomString(10, true),
                    RandomHelper.RandomInt(1, 43),
                    RandomHelper.RandomBool(),
                    RandomHelper.RandomNumber(50, 180, 1),
                    RandomHelper.RandomDate(new DateTime(1980, 1, 1), DateTime.Now),
                    RandomHelper.RandomColor()
                    ));
            }
        }
        public bool Add()
        {
            return true;
        }
        public bool Select()
        {
            return true;
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

        //void IModalService.NavigateTo(UserControl uc, BackNavigationEventHandler backFromDialog)
        //{

        //}

        //void IModalService.GoBackward(bool dialogReturnValue)
        //{
        //}


        public static IModalService ModalService
        {
            get { return (IModalService)Application.Current.MainWindow; }
        }
    }
}