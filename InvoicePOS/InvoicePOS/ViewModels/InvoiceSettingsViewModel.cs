using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using InvoicePOS.Helpers;
using InvoicePOS.Views;
using InvoicePOS.Models;
using System.Windows;
using System.Windows.Input;
using System.Resources;
using System.IO;
using System.Collections;

namespace InvoicePOS.ViewModels
{
    class InvoiceSettingsViewModel : DependencyObject, INotifyPropertyChanged
    {
        public InvoiceSettingsViewModel()
        {
            string resxFile = "..\\..\\Resources\\Settings.resx";

            if (File.Exists(resxFile))
            {
                using (ResXResourceReader resxReader = new ResXResourceReader(resxFile))
                {
                    foreach (DictionaryEntry entry in resxReader)
                    {
                        string s = (string)entry.Key;

                        switch (s)
                        {
                            case "Warning1":
                                Warning1 = (bool)entry.Value;
                                break;
                            case "Warning2":
                                Warning2 = (bool)entry.Value;
                                break;
                            case "Warning3":
                                Warning3 = (bool)entry.Value;
                                break;
                            case "Operations1":
                                Operations1 = (bool)entry.Value;
                                break;
                            case "Operations2":
                                Operations2 = (bool)entry.Value;
                                break;
                            case "Operations3":
                                Operations3 = (bool)entry.Value;
                                break;
                            case "Operations4":
                                Operations4 = (bool)entry.Value;
                                break;
                            case "Operations5":
                                Operations5 = (bool)entry.Value;
                                break;
                            case "Operations6":
                                Operations6 = (bool)entry.Value;
                                break;
                            case "Operations7":
                                Operations7 = (bool)entry.Value;
                                break;
                            case "Operations8":
                                Operations8 = (bool)entry.Value;
                                break;
                            case "Operations9":
                                Operations9 = (string)entry.Value;
                                break;
                            case "Operations10":
                                Operations10 = (string)entry.Value;
                                break;
                            case "Customer1":
                                Customer1 = (bool)entry.Value;
                                break;
                            case "Customer2":
                                Customer2 = (bool)entry.Value;
                                break;
                            case "Customer3":
                                Customer3 = (bool)entry.Value;
                                break;
                            case "Customer4":
                                Customer4 = (bool)entry.Value;
                                break;
                            case "Customer5":
                                Customer5 = (bool)entry.Value;
                                break;
                            case "Customer6":
                                Customer6 = (bool)entry.Value;
                                break;
                            case "BottomPanels1":
                                BottomPanels1 = (bool)entry.Value;
                                break;
                            case "BottomPanels2":
                                BottomPanels2 = (bool)entry.Value;
                                break;
                            case "BottomPanels3":
                                BottomPanels3 = (bool)entry.Value;
                                break;
                            case "BottomPanels4":
                                BottomPanels4 = (bool)entry.Value;
                                break;
                            case "InvoiceScreenGrid1":
                                InvoiceScreenGrid1 = (bool)entry.Value;
                                break;
                            case "InvoiceScreenGrid2":
                                InvoiceScreenGrid2 = (bool)entry.Value;
                                break;
                            case "InvoiceScreenGrid3":
                                InvoiceScreenGrid3 = (bool)entry.Value;
                                break;
                            case "InvoiceScreenGrid4":
                                InvoiceScreenGrid4 = (bool)entry.Value;
                                break;
                            case "Totals1":
                                Totals1 = (bool)entry.Value;
                                break;
                            case "Totals2":
                                Totals2 = (bool)entry.Value;
                                break;
                            case "Totals3":
                                Totals3 = (bool)entry.Value;
                                break;
                            case "Totals4":
                                Totals4 = (bool)entry.Value;
                                break;
                            case "Totals5":
                                Totals5 = (bool)entry.Value;
                                break;
                            case "PaymentSettings1":
                                PaymentSettings1 = (string)entry.Value;
                                break;
                            case "PaymentSettings2":
                                PaymentSettings2 = (bool)entry.Value;
                                break;
                            case "PaymentSettings3":
                                PaymentSettings3 = (string)entry.Value;
                                break;
                            case "PaymentSettings4":
                                Customer4 = (bool)entry.Value;
                                break;
                            case "PaymentSettings5":
                                PaymentSettings5 = (string)entry.Value;
                                break;
                            case "PaymentSettings6":
                                PaymentSettings6 = (bool)entry.Value;
                                break;
                            case "PaymentSettings7":
                                PaymentSettings7 = (bool)entry.Value;
                                break;
                            case "ExpressPaymentSettings1":
                                ExpressPaymentSettings1 = (bool)entry.Value;
                                break;
                            case "ExpressPaymentSettings2":
                                ExpressPaymentSettings2 = (bool)entry.Value;
                                break;
                            case "ExpressPaymentSettings3":
                                ExpressPaymentSettings3 = (bool)entry.Value;
                                break;
                            case "ExpressPaymentSettings4":
                                ExpressPaymentSettings4 = (string)entry.Value;
                                break;
                            case "ExpressPaymentSettings5":
                                ExpressPaymentSettings5 = (bool)entry.Value;
                                break;
                            case "ExpressPaymentSettings6":
                                ExpressPaymentSettings6 = (bool)entry.Value;
                                break;
                            case "SalesExecutive1":
                                SalesExecutive1 = (bool)entry.Value;
                                break;
                            case "SalesExecutive2":
                                SalesExecutive2 = (bool)entry.Value;
                                break;
                            case "SalesExecutive3":
                                SalesExecutive3 = (bool)entry.Value;
                                break;
                            case "SalesExecutive4":
                                SalesExecutive4 = (bool)entry.Value;
                                break;
                            case "SalesExecutive5":
                                SalesExecutive5 = (bool)entry.Value;
                                break;
                            case "SalesExecutive6":
                                SalesExecutive6 = (bool)entry.Value;
                                break;
                            case "InvoiceDelivery1":
                                InvoiceDelivery1 = (bool)entry.Value;
                                break;
                            case "LabelSettings1":
                                LabelSettings1 = (bool)entry.Value;
                                break;
                            case "Subsidy1":
                                Subsidy1 = (bool)entry.Value;
                                break;
                            case "OfferSettings1":
                                OfferSettings1 = (bool)entry.Value;
                                break;
                            case "OfferSettings2":
                                OfferSettings2 = (bool)entry.Value;
                                break;
                            case "OtherSettings1":
                                OtherSettings1 = (string)entry.Value;
                                break;
                            case "OtherSettings2":
                                OtherSettings2 = (string)entry.Value;
                                break;
                            case "InvoiceTokenSettings1":
                                InvoiceTokenSettings1 = (bool)entry.Value;
                                break;
                            case "InvoiceTokenSettings2":
                                InvoiceTokenSettings2 = (bool)entry.Value;
                                break;
                            case "InvoiceTokenSettings3":
                                InvoiceTokenSettings3 = (bool)entry.Value;
                                break;
                            case "PictorialBillingSettings1":
                                PictorialBillingSettings1 = (bool)entry.Value;
                                break;
                            case "ManualDiscount1":
                                ManualDiscount1 = (bool)entry.Value;
                                break;
                            case "ManualDiscount2":
                                ManualDiscount2 = (bool)entry.Value;
                                break;
                            default:
                                break;
                        }
                        resxReader.Close();
                    }
                }
            }
        
        }
        #region dataBinding

        private bool _Warning1;
        public bool Warning1
        {
            get
            {
                return _Warning1;
            }

            set
            {
                _Warning1 = value;
                OnPropertyChanged("_Warning1");
            }
        }

        private bool _Warning2;
        public bool Warning2
        {
            get
            {
                return _Warning2;
            }

            set
            {
                _Warning2 = value;
                OnPropertyChanged("_Warning2");
            }
        }


        private bool _Warning3;
        public bool Warning3
        {
            get
            {
                return _Warning3;
            }

            set
            {
                _Warning3 = value;
                OnPropertyChanged("Warning3");
            }
        }


        private bool _Operations1;
        public bool Operations1
        {
            get
            {
                return _Operations1;
            }

            set
            {
                _Operations1 = value;
                OnPropertyChanged("Operations1");
            }
        }

        private bool _Operations2;
        public bool Operations2
        {
            get
            {
                return _Operations2;
            }

            set
            {
                _Operations2 = value;
                OnPropertyChanged("Operations2");
            }
        }

        private bool _Operations3;
        public bool Operations3
        {
            get
            {
                return _Operations3;
            }

            set
            {
                _Operations3 = value;
                OnPropertyChanged("Operations3");
            }
        }

        private bool _Operations4;
        public bool Operations4
        {
            get
            {
                return _Operations4;
            }

            set
            {
                _Operations4 = value;
                OnPropertyChanged("Operations4");
            }
        }

        private bool _Operations5;
        public bool Operations5
        {
            get
            {
                return _Operations5;
            }

            set
            {
                _Operations5 = value;
                OnPropertyChanged("Operations5");
            }
        }

        private bool _Operations6;
        public bool Operations6
        {
            get
            {
                return _Operations6;
            }

            set
            {
                _Operations6 = value;
                OnPropertyChanged("Operations6");
            }
        }


        private bool _Operations7;
        public bool Operations7
        {
            get
            {
                return _Operations7;
            }

            set
            {
                _Operations7 = value;
                OnPropertyChanged("Operations7");
            }
        }

        private bool _Operations8;
        public bool Operations8
        {
            get
            {
                return _Operations8;
            }

            set
            {
                _Operations8 = value;
                OnPropertyChanged("Operations8");
            }
        }


        private string _Operations9;
        public string Operations9
        {
            get
            {
                return _Operations9;
            }

            set
            {
                _Operations9 = value;
                OnPropertyChanged("Operations8");
            }
        }

        private string _Operations10;
        public string Operations10
        {
            get
            {
                return _Operations10;
            }

            set
            {
                _Operations10 = value;
                OnPropertyChanged("Operations10");
            }
        }

        private bool _Customer1;
        public bool Customer1
        {
            get
            {
                return _Customer1;
            }

            set
            {
                _Customer1 = value;
                OnPropertyChanged("Customer1");
            }
        }


        private bool _Customer2;
        public bool Customer2
        {
            get
            {
                return _Customer2;
            }

            set
            {
                _Customer2 = value;
                OnPropertyChanged("Customer2");
            }
        }

        private bool _Customer3;
        public bool Customer3
        {
            get
            {
                return _Customer3;
            }

            set
            {
                _Customer3 = value;
                OnPropertyChanged("Customer3");
            }
        }


        private bool _Customer4;
        public bool Customer4
        {
            get
            {
                return _Customer4;
            }

            set
            {
                _Customer4 = value;
                OnPropertyChanged("Customer4");
            }
        }

        private bool _Customer5;
        public bool Customer5
        {
            get
            {
                return _Customer5;
            }

            set
            {
                _Customer5 = value;
                OnPropertyChanged("Customer5");
            }
        }

        private bool _Customer6;
        public bool Customer6
        {
            get
            {
                return _Customer6;
            }

            set
            {
                _Customer6 = value;
                OnPropertyChanged("Customer6");
            }
        }

        private bool _BottomPanels1;
        public bool BottomPanels1
        {
            get
            {
                return _BottomPanels1;
            }

            set
            {
                _BottomPanels1 = value;
                OnPropertyChanged("BottomPanels1");
            }
        }

        private bool _BottomPanels2;
        public bool BottomPanels2
        {
            get
            {
                return _BottomPanels2;
            }

            set
            {
                _BottomPanels2 = value;
                OnPropertyChanged("BottomPanels2");
            }
        }


        private bool _BottomPanels3;
        public bool BottomPanels3
        {
            get
            {
                return _BottomPanels3;
            }

            set
            {
                _BottomPanels3 = value;
                OnPropertyChanged("BottomPanels3");
            }
        }

        private bool _BottomPanels4;
        public bool BottomPanels4
        {
            get
            {
                return _BottomPanels4;
            }

            set
            {
                _BottomPanels4 = value;
                OnPropertyChanged("BottomPanels4");
            }
        }

        private bool _InvoiceScreenGrid1;
        public bool InvoiceScreenGrid1
        {
            get
            {
                return _InvoiceScreenGrid1;
            }

            set
            {
                _InvoiceScreenGrid1 = value;
                OnPropertyChanged("InvoiceScreenGrid1");
            }
        }

        private bool _InvoiceScreenGrid2;
        public bool InvoiceScreenGrid2
        {
            get
            {
                return _InvoiceScreenGrid2;
            }

            set
            {
                _InvoiceScreenGrid2 = value;
                OnPropertyChanged("InvoiceScreenGrid2");
            }
        }


        private bool _InvoiceScreenGrid3;
        public bool InvoiceScreenGrid3
        {
            get
            {
                return _InvoiceScreenGrid3;
            }

            set
            {
                _InvoiceScreenGrid3 = value;
                OnPropertyChanged("InvoiceScreenGrid3");
            }
        }

        private bool _InvoiceScreenGrid4;
        public bool InvoiceScreenGrid4
        {
            get
            {
                return _InvoiceScreenGrid4;
            }

            set
            {
                _InvoiceScreenGrid4 = value;
                OnPropertyChanged("InvoiceScreenGrid4");
            }
        }

        private bool _Totals1;
        public bool Totals1
        {
            get
            {
                return _Totals1;
            }

            set
            {
                _Totals1 = value;
                OnPropertyChanged("_Totals1");
            }
        }

        private bool _Totals2;
        public bool Totals2
        {
            get
            {
                return _Totals2;
            }

            set
            {
                _Totals2 = value;
                OnPropertyChanged("_Totals2");
            }
        }

        private bool _Totals3;
        public bool Totals3
        {
            get
            {
                return _Totals3;
            }

            set
            {
                _Totals3 = value;
                OnPropertyChanged("_Totals3");
            }
        }

        private bool _Totals4;
        public bool Totals4
        {
            get
            {
                return _Totals4;
            }

            set
            {
                _Totals4 = value;
                OnPropertyChanged("_Totals4");
            }
        }

        private bool _Totals5;
        public bool Totals5
        {
            get
            {
                return _Totals5;
            }

            set
            {
                _Totals5 = value;
                OnPropertyChanged("_Totals5");
            }
        }

        private string _PaymentSettings1;
        public string PaymentSettings1
        {
            get
            {
                return _PaymentSettings1;
            }

            set
            {
                _PaymentSettings1 = value;
                OnPropertyChanged("PaymentSettings1");
            }
        }

        private bool _PaymentSettings2;
        public bool PaymentSettings2
        {
            get
            {
                return _PaymentSettings2;
            }

            set
            {
                _PaymentSettings2 = value;
                OnPropertyChanged("PaymentSettings2");
            }
        }

        private string _PaymentSettings3;
        public string PaymentSettings3
        {
            get
            {
                return _PaymentSettings3;
            }

            set
            {
                _PaymentSettings3 = value;
                OnPropertyChanged("PaymentSettings1");
            }
        }


        private bool _PaymentSettings4;
        public bool PaymentSettings4
        {
            get
            {
                return _PaymentSettings4;
            }

            set
            {
                _PaymentSettings4 = value;
                OnPropertyChanged("PaymentSettings4");
            }
        }

        private string _PaymentSettings5;
        public string PaymentSettings5
        {
            get
            {
                return _PaymentSettings5;
            }

            set
            {
                _PaymentSettings5 = value;
                OnPropertyChanged("PaymentSettings5");
            }
        }


        private bool _PaymentSettings6;
        public bool PaymentSettings6
        {
            get
            {
                return _PaymentSettings6;
            }

            set
            {
                _PaymentSettings6 = value;
                OnPropertyChanged("PaymentSettings6");
            }
        }


        private bool _PaymentSettings7;
        public bool PaymentSettings7
        {
            get
            {
                return _PaymentSettings7;
            }

            set
            {
                _PaymentSettings7 = value;
                OnPropertyChanged("PaymentSettings7");
            }
        }


        private bool _ExpressPaymentSettings1;
        public bool ExpressPaymentSettings1
        {
            get
            {
                return _ExpressPaymentSettings1;
            }

            set
            {
                _ExpressPaymentSettings1 = value;
                OnPropertyChanged("ExpressPaymentSettings1");
            }
        }

        private bool _ExpressPaymentSettings2;
        public bool ExpressPaymentSettings2
        {
            get
            {
                return _ExpressPaymentSettings2;
            }

            set
            {
                _ExpressPaymentSettings2 = value;
                OnPropertyChanged("ExpressPaymentSettings2");
            }
        }


        private bool _ExpressPaymentSettings3;
        public bool ExpressPaymentSettings3
        {
            get
            {
                return _ExpressPaymentSettings3;
            }

            set
            {
                _ExpressPaymentSettings3 = value;
                OnPropertyChanged("ExpressPaymentSettings3");
            }
        }

        private string _ExpressPaymentSettings4;
        public string ExpressPaymentSettings4
        {
            get
            {
                return _ExpressPaymentSettings4;
            }

            set
            {
                _ExpressPaymentSettings4 = value;
                OnPropertyChanged("ExpressPaymentSettings3");
            }
        }

        private bool _ExpressPaymentSettings5;
        public bool ExpressPaymentSettings5
        {
            get
            {
                return _ExpressPaymentSettings5;
            }

            set
            {
                _ExpressPaymentSettings5 = value;
                OnPropertyChanged("ExpressPaymentSettings5");
            }
        }

        private bool _ExpressPaymentSettings6;
        public bool ExpressPaymentSettings6
        {
            get
            {
                return _ExpressPaymentSettings6;
            }

            set
            {
                _ExpressPaymentSettings6 = value;
                OnPropertyChanged("ExpressPaymentSettings6");
            }
        }

        private bool _SalesExecutive1;
        public bool SalesExecutive1
        {
            get
            {
                return _SalesExecutive1;
            }

            set
            {
                _SalesExecutive1 = value;
                OnPropertyChanged("SalesExecutive1");
            }
        }

        private bool _SalesExecutive2;
        public bool SalesExecutive2
        {
            get
            {
                return _SalesExecutive2;
            }

            set
            {
                _SalesExecutive2 = value;
                OnPropertyChanged("SalesExecutive2");
            }
        }

        private bool _SalesExecutive3;
        public bool SalesExecutive3
        {
            get
            {
                return _SalesExecutive3;
            }

            set
            {
                _SalesExecutive3 = value;
                OnPropertyChanged("SalesExecutive3");
            }
        }



        private bool _SalesExecutive4;
        public bool SalesExecutive4
        {
            get
            {
                return _SalesExecutive4;
            }

            set
            {
                _SalesExecutive4 = value;
                OnPropertyChanged("SalesExecutive4");
            }
        }

        private bool _SalesExecutive5;
        public bool SalesExecutive5
        {
            get
            {
                return _SalesExecutive5;
            }

            set
            {
                _SalesExecutive5 = value;
                OnPropertyChanged("SalesExecutive5");
            }
        }

        private bool _SalesExecutive6;
        public bool SalesExecutive6
        {
            get
            {
                return _SalesExecutive6;
            }

            set
            {
                _SalesExecutive6 = value;
                OnPropertyChanged("SalesExecutive6");
            }
        }


        private bool _InvoiceDelivery1;
        public bool InvoiceDelivery1
        {
            get
            {
                return _InvoiceDelivery1;
            }

            set
            {
                _InvoiceDelivery1 = value;
                OnPropertyChanged("InvoiceDelivery1");
            }
        }

        private bool _LabelSettings1;
        public bool LabelSettings1
        {
            get
            {
                return _LabelSettings1;
            }

            set
            {
                _LabelSettings1 = value;
                OnPropertyChanged("LabelSettings1");
            }
        }

        private bool _Subsidy1;
        public bool Subsidy1
        {
            get
            {
                return _Subsidy1;
            }

            set
            {
                _Subsidy1 = value;
                OnPropertyChanged("Subsidy1");
            }
        }

        private bool _OfferSettings1;
        public bool OfferSettings1
        {
            get
            {
                return _OfferSettings1;
            }

            set
            {
                _OfferSettings1 = value;
                OnPropertyChanged("OfferSettings1");
            }
        }


        private bool _OfferSettings2;
        public bool OfferSettings2
        {
            get
            {
                return _OfferSettings2;
            }

            set
            {
                _OfferSettings2 = value;
                OnPropertyChanged("OfferSettings2");
            }
        }


        private string _OtherSettings1;
        public string OtherSettings1
        {
            get
            {
                return _OtherSettings1;
            }

            set
            {
                _OtherSettings1 = value;
                OnPropertyChanged("OtherSettings1");
            }
        }

        private string _OtherSettings2;
        public string OtherSettings2
        {
            get
            {
                return _OtherSettings2;
            }

            set
            {
                _OtherSettings2 = value;
                OnPropertyChanged("OtherSettings2");
            }
        }

        private string _OtherSettings3;
        public string OtherSettings3
        {
            get
            {
                return _OtherSettings3;
            }

            set
            {
                _OtherSettings3 = value;
                OnPropertyChanged("OtherSettings3");
            }
        }

        private bool _InvoiceTokenSettings1;
        public bool InvoiceTokenSettings1
        {
            get
            {
                return _InvoiceTokenSettings1;
            }

            set
            {
                _InvoiceTokenSettings1 = value;
                OnPropertyChanged("InvoiceTokenSettings1");
            }
        }

        private bool _InvoiceTokenSettings2;
        public bool InvoiceTokenSettings2
        {
            get
            {
                return _InvoiceTokenSettings2;
            }

            set
            {
                _InvoiceTokenSettings2 = value;
                OnPropertyChanged("InvoiceTokenSettings2");
            }
        }

        private bool _InvoiceTokenSettings3;
        public bool InvoiceTokenSettings3
        {
            get
            {
                return _InvoiceTokenSettings3;
            }

            set
            {
                _InvoiceTokenSettings3 = value;
                OnPropertyChanged("InvoiceTokenSettings3");
            }
        }

        private bool _PictorialBillingSettings1;
        public bool PictorialBillingSettings1
        {
            get
            {
                return _PictorialBillingSettings1;
            }

            set
            {
                _PictorialBillingSettings1 = value;
                OnPropertyChanged("PictorialBillingSettings1");
            }
        }

        private bool _ManualDiscount1;
        public bool ManualDiscount1
        {
            get
            {
                return _ManualDiscount1;
            }

            set
            {
                _ManualDiscount1 = value;
                OnPropertyChanged("ManualDiscount1");
            }
        }

        private bool _ManualDiscount2;
        public bool ManualDiscount2
        {
            get
            {
                return _ManualDiscount2;
            }

            set
            {
                _ManualDiscount2 = value;
                OnPropertyChanged("ManualDiscount2");
            }
        }

        #endregion 


        #region Interface Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            ;
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

        #endregion

        #region CommandBinding
        private ICommand _Apply;
        public ICommand Apply
        {
            get
            {
                if (_Apply == null)
                {
                    _Apply = new DelegateCommand(Apply_Click);
                }
                return _Apply;
            }
        }

        public void Apply_Click()
        {
            using (ResXResourceWriter resx = new ResXResourceWriter("..\\..\\Resources\\Settings.resx"))
            {
                resx.AddResource("SalesExecutive1", SalesExecutive1);
                resx.AddResource("SalesExecutive2", SalesExecutive2);
                resx.AddResource("SalesExecutive3", SalesExecutive3);
                resx.AddResource("SalesExecutive4", SalesExecutive4);
                resx.AddResource("SalesExecutive5", SalesExecutive5);
                resx.AddResource("SalesExecutive6", SalesExecutive6);
                resx.AddResource("InvoiceDelivery1", InvoiceDelivery1);
                resx.AddResource("LabelSettings1", LabelSettings1);
                resx.AddResource("Subsidy1", Subsidy1);
                resx.AddResource("OfferSettings1", OfferSettings1);
                resx.AddResource("OfferSettings2", OfferSettings2);
                resx.AddResource("OtherSettings1", OtherSettings1);
                resx.AddResource("OtherSettings2", OtherSettings2);
                resx.AddResource("OtherSettings3", OtherSettings3);
                resx.AddResource("InvoiceTokenSettings1", InvoiceTokenSettings1);
                resx.AddResource("InvoiceTokenSettings2", InvoiceTokenSettings2);
                resx.AddResource("InvoiceTokenSettings3", InvoiceTokenSettings3);
                resx.AddResource("PictorialBillingSettings1", PictorialBillingSettings1);
                resx.AddResource("ManualDiscount1", ManualDiscount1);
                resx.AddResource("ManualDiscount2", ManualDiscount2);
                resx.AddResource("Warning1", Warning1);
                resx.AddResource("Warning2", Warning2);
                resx.AddResource("Warning3", Warning3);
                resx.AddResource("Operations1", Operations1);
                resx.AddResource("Operations2", Operations2);
                resx.AddResource("Operations3", Operations3);
                resx.AddResource("Operations4", Operations4);
                resx.AddResource("Operations5", Operations5);
                resx.AddResource("Operations6", Operations6);
                resx.AddResource("Operations7", Operations7);
                resx.AddResource("Operations8", Operations8);
                resx.AddResource("Operations9", Operations9);
                resx.AddResource("Operations10", Operations10);
                resx.AddResource("Customer1", Customer1);
                resx.AddResource("Customer2", Customer2);
                resx.AddResource("Customer3", Customer3);
                resx.AddResource("Customer4", Customer4);
                resx.AddResource("Customer5", Customer5);
                resx.AddResource("Customer6", Customer6);
                resx.AddResource("BottomPanels1", BottomPanels1);
                resx.AddResource("BottomPanels2", BottomPanels2);
                resx.AddResource("BottomPanels3", BottomPanels3);
                resx.AddResource("InvoiceScreenGrid1", InvoiceScreenGrid1);
                resx.AddResource("InvoiceScreenGrid2", InvoiceScreenGrid2);
                resx.AddResource("InvoiceScreenGrid3", InvoiceScreenGrid3);
                resx.AddResource("InvoiceScreenGrid4", InvoiceScreenGrid4);
                resx.AddResource("Totals1", Totals1);
                resx.AddResource("Totals2", Totals2);
                resx.AddResource("Totals3", Totals3);
                resx.AddResource("Totals4", Totals4);
                resx.AddResource("Totals5", Totals5);
                resx.AddResource("PaymentSettings1", PaymentSettings1);
                resx.AddResource("PaymentSettings2", PaymentSettings2);
                resx.AddResource("PaymentSettings3", PaymentSettings3);
                resx.AddResource("PaymentSettings4", PaymentSettings4);
                resx.AddResource("PaymentSettings5", PaymentSettings5);
                resx.AddResource("PaymentSettings6", PaymentSettings6);
                resx.AddResource("PaymentSettings7", PaymentSettings7);
                resx.AddResource("ExpressPaymentSettings1", ExpressPaymentSettings1);
                resx.AddResource("ExpressPaymentSettings2", ExpressPaymentSettings2);
                resx.AddResource("ExpressPaymentSettings3", ExpressPaymentSettings3);
                resx.AddResource("ExpressPaymentSettings4", ExpressPaymentSettings4);
                resx.AddResource("ExpressPaymentSettings5", ExpressPaymentSettings5);
                resx.AddResource("ExpressPaymentSettings6", ExpressPaymentSettings6);
                resx.Close();
            }

            Cancel_Click();

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
                if (App.Current.Windows[intCounter].Title == "InvoiceSettings")
                {
                    App.Current.Windows[intCounter].Close();
                }
            }
        }
        #endregion

    }
}
