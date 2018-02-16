using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Resources;
using System.ComponentModel;
using InvoicePOS.Helpers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Drawing;
using InvoicePOS.Views;
using InvoicePOS.Models;
using InvoicePOS.Helpers;

namespace InvoicePOS.ViewModels
{
    class CurrencySettingsViewModel : DependencyObject, INotifyPropertyChanged
    {
        int num = 123456789;
        public CurrencySettingsViewModel()
        {
            //NumberFormatRadio1 = true;
            //NorrmalFontRightRadio = true;
            //SpecialFontRightRadio = true;
            //CurrencySpecialFontSymbolRight = "Visible";
            //CurrencySpecialFontSymbolRight = "Collapsed";
            SelectedCurrencySettings = new CurrencySettingsModel();
            SelectedCurrencySettings.LoadSettings();
            NumberFormatDecimalPlace = Convert.ToString(SelectedCurrencySettings.NUMBER_DECIMAL_PLACES);
            SpecialFontLeftRadio = SelectedCurrencySettings.SPECIAL_CURRENCY_SYMBOL_LEFT;
            NorrmalFontLeftRadio = SelectedCurrencySettings.NORMAL_CURRENCY_SYMBOL_LEFT;
            NorrmalFontRightRadio = !SelectedCurrencySettings.NORMAL_CURRENCY_SYMBOL_LEFT;
            SpecialFontRightRadio = !SelectedCurrencySettings.SPECIAL_CURRENCY_SYMBOL_LEFT;
            NumberFormatRadio2 = SelectedCurrencySettings.NUMBER_FORMAT_CHOICE_2;
            NumberFormatRadio1 = !SelectedCurrencySettings.NUMBER_FORMAT_CHOICE_2;
            UnitPriceDecimalPlaces = Convert.ToString(SelectedCurrencySettings.UNIT_PRICE_DECIMAL_PLACES);
            QuantityDecimalPlaces = Convert.ToString(SelectedCurrencySettings.QUANTITY_DECIMAL_PLACES);
            CurrencyNormalFontSymbol = SelectedCurrencySettings.NORMAL_CURRENCY_SYMBOL;
            CurrencySpecialFontSymbol = SelectedCurrencySettings.SPECIAL_CURRENCY_SYMBOL;
            if (SelectedCurrencySettings.FONT_FAMILY != null)
            {
                fontFamily = new System.Windows.Media.FontFamily(SelectedCurrencySettings.FONT_FAMILY);
            }
            else
            {
                fontFamily = System.Windows.SystemFonts.MessageFontFamily;
            }
            if(SelectedCurrencySettings.FONT_SIZE!=0)
            {
                fontSize = SelectedCurrencySettings.FONT_SIZE;
            }
            else
            {
                fontSize = System.Windows.SystemFonts.MessageFontSize;
            }

            fontStretch = FontHelper.getFontStretch(SelectedCurrencySettings.FONT_STRETCH);
            fontWeight = FontHelper.getFontWeight(SelectedCurrencySettings.FONT_WEIGHT);

            if (SelectedCurrencySettings.FONT_STYLE != null)
            {
                fontStyle = FontHelper.getFontStyle(SelectedCurrencySettings.FONT_STYLE);
            }
            else
            {
                fontStyle = System.Windows.SystemFonts.MessageFontStyle;
            }
            txtDecorations = FontHelper.getTextDecorations(SelectedCurrencySettings.TEXT_DECOR_UNDER_LINE,
                SelectedCurrencySettings.TEXT_DECOR_BASE_LINE,
                SelectedCurrencySettings.TEXT_DECOR_STRIKE_THROUGH,
                SelectedCurrencySettings.TEXT_DECOR_OVER_LINE);
        }

        #region 
        private string _FontDesc;
        public string FontDesc
        {
            get
            {
                string desc = "";
                desc += FontFamilyListItem.GetDisplayName(fontFamily);
                desc += ", ";
                desc += Convert.ToString(this.fontSize);
                desc += ", ";
                desc += fontStyle.ToString();
                _FontDesc = desc;
                return _FontDesc;
            }

            set
            {
                _FontDesc = value;
                OnPropertyChanged("FontDesc");
            }
        }

        private System.Windows.Media.FontFamily _fontFamily;
        public System.Windows.Media.FontFamily fontFamily
        {
            get
            {
                return _fontFamily;
            }

            set
            {
                _fontFamily = value;
                OnPropertyChanged("fontFamily");
            }
        }

        private FontStretch _fontStretch;
        public FontStretch fontStretch
        {
            get
            {
                return _fontStretch;
            }

            set
            {
                _fontStretch = value;
                OnPropertyChanged("fontStretch");
            }
        }

        private System.Windows.FontStyle _fontStyle;
        public System.Windows.FontStyle fontStyle
        {
            get
            {
                return _fontStyle;
            }

            set
            {
                _fontStyle = value;
                OnPropertyChanged("fontStyle");
            }
        }

        private FontWeight _fontWeight;
        public FontWeight fontWeight
        {
            get
            {
                return _fontWeight;
            }

            set
            {
                _fontWeight = value;
                OnPropertyChanged("fontWeight");
            }
        }

        private TextDecorationCollection _txtDecorations;
        public TextDecorationCollection txtDecorations
        {
            get
            {
                return _txtDecorations;
            }

            set
            {
                _txtDecorations = value;
                OnPropertyChanged("txtDecorations");
            }
        }

        private double _fontSize;
        public double fontSize
        {
            get
            {
                return _fontSize;
            }

            set
            {
                _fontSize = value;
                OnPropertyChanged("fontSize");
            }
        }

        private CurrencySettingsModel _SelectedCurrencySettings;
        public CurrencySettingsModel SelectedCurrencySettings
        {
            get
            {
                return _SelectedCurrencySettings;
            }

            set
            {
                _SelectedCurrencySettings = value;
                OnPropertyChanged("SelectedCurrencySettings");
            }
        }

        private string _NumberFormatRadio1Txt;
        public string NumberFormatRadio1Txt
        {
            get
            {
                if (String.IsNullOrEmpty(NumberFormatDecimalPlace))
                {
                    _NumberFormatRadio1Txt = "0.00";
                }
                else
                {
                    long decPlace;
                    long.TryParse( NumberFormatDecimalPlace,out decPlace);
                    if (decPlace == 0)
                    {
                        _NumberFormatRadio1Txt = "0";
                    }
                    else if (decPlace < 100)
                    {
                        _NumberFormatRadio1Txt = "0.";
                        for(int i=0; i<decPlace;i++)
                        {
                            _NumberFormatRadio1Txt += "0";
                        }
                    }
                    else if (decPlace >= 100 && decPlace < 10000000000)
                    {
                        _NumberFormatRadio1Txt = "0.";
                        _NumberFormatRadio1Txt += "D" + Convert.ToInt64(decPlace);
                    }
                    else if (decPlace >= 10000000000)
                    {
                        _NumberFormatRadio1Txt = "0.";
                        _NumberFormatRadio1Txt += "D-2147483648";
                    }
                }

                return _NumberFormatRadio1Txt;
            }

            set
            {
                _NumberFormatRadio1Txt = value;
                NotifyPropertyChanged("NumberFormatRadio1Txt");
            }
        }

        private string _NumberFormatRadio2Txt;
        public string NumberFormatRadio2Txt
        {
            get
            {
                _NumberFormatRadio2Txt = _NumberFormatRadio1Txt;
                return _NumberFormatRadio2Txt;
            }

            set
            {
                _NumberFormatRadio2Txt = value;
                OnPropertyChanged("NumberFormatRadio2Txt");
            }
        }

        private string _CurrencySpecialFontSymbolRight;
        public string CurrencySpecialFontSymbolRight
        {
            get
            {
                if (SpecialFontRightRadio)
                {
                    _CurrencySpecialFontSymbolRight = "Visible";
                }
                else
                {
                    _CurrencySpecialFontSymbolRight = "Collapsed";
                }
                return _CurrencySpecialFontSymbolRight;
            }

            set
            {
                _CurrencySpecialFontSymbolRight = value;
                OnPropertyChanged("CurrencySpecialFontSymbolRight");
            }
        }

        private string _CurrencySpecialFontSymbolLeft;
        public string CurrencySpecialFontSymbolLeft
        {
            get
            {
                if (SpecialFontLeftRadio)
                {
                    _CurrencySpecialFontSymbolLeft = "Visible";
                }
                else
                {
                    _CurrencySpecialFontSymbolLeft = "Collapsed";
                }
                return _CurrencySpecialFontSymbolLeft;
            }

            set
            {
                _CurrencySpecialFontSymbolLeft = value;
                OnPropertyChanged("CurrencySpecialFontSymbolLeft");
            }
        }
        private bool _NumberFormatRadio1;
        public bool NumberFormatRadio1
        {
            get
            {
                return _NumberFormatRadio1;
            }

            set
            {
                _NumberFormatRadio1 = value;
                OnPropertyChanged("NumberFormatRadio1");
                NotifyPropertyChanged("NumberFormatTxt");
                NotifyPropertyChanged("UnitPriceTxt");
                NotifyPropertyChanged("QuantityTxt");
                NotifyPropertyChanged("CurrencyNormalFontFormatTxt");
                NotifyPropertyChanged("CurrencySpecialFontFormatTxt");
            }
        }

        private bool _NumberFormatRadio2;
        public bool NumberFormatRadio2
        {
            get
            {
                return _NumberFormatRadio2;
            }

            set
            {
                _NumberFormatRadio2 = value;
                OnPropertyChanged("NumberFormatRadio2");
                NotifyPropertyChanged("NumberFormatTxt");
                NotifyPropertyChanged("UnitPriceTxt");
                NotifyPropertyChanged("QuantityTxt");
                NotifyPropertyChanged("CurrencyNormalFontFormatTxt");
                NotifyPropertyChanged("CurrencySpecialFontFormatTxt");
            }
        }

        private bool _NorrmalFontLeftRadio;
        public bool NorrmalFontLeftRadio
        {
            get
            {
                return _NorrmalFontLeftRadio;
            }

            set
            {
                _NorrmalFontLeftRadio = value;
                OnPropertyChanged("NorrmalFontLeftRadio");
                NotifyPropertyChanged("CurrencyNormalFontFormatTxt");
            }
        }

        private bool _NorrmalFontRightRadio;
        public bool NorrmalFontRightRadio
        {
            get
            {
                return _NorrmalFontRightRadio;
            }

            set
            {
                _NorrmalFontRightRadio = value;
                OnPropertyChanged("NorrmalFontRightRadio");
                NotifyPropertyChanged("CurrencyNormalFontFormatTxt");
            }
        }

        private bool _SpecialFontLeftRadio;
        public bool SpecialFontLeftRadio
        {
            get
            {
                return _SpecialFontLeftRadio;
            }

            set
            {
                _SpecialFontLeftRadio = value;
                OnPropertyChanged("SpecialFontLeftRadio");
                //NotifyPropertyChanged("CurrencySpecialFontFormatTxt");
                NotifyPropertyChanged("CurrencySpecialFontSymbolLeft");
            }
        }

        private bool _SpecialFontRightRadio;
        public bool SpecialFontRightRadio
        {
            get
            {
                return _SpecialFontRightRadio;
            }

            set
            {
                _SpecialFontRightRadio = value;
                OnPropertyChanged("SpecialFontRightRadio");
                //NotifyPropertyChanged("CurrencySpecialFontFormatTxt");
                NotifyPropertyChanged("CurrencySpecialFontSymbolRight");
            }
        }


        private string _NumberFormatDecimalPlace;
        public string NumberFormatDecimalPlace
        {
            get
            {
                return _NumberFormatDecimalPlace;
            }

            set
            {
                _NumberFormatDecimalPlace = value;
                NotifyPropertyChanged("NumberFormatDecimalPlace");
                NotifyPropertyChanged("NumberFormatRadio1Txt");
                NotifyPropertyChanged("NumberFormatRadio2Txt");
                NotifyPropertyChanged("NumberFormatTxt");
                NotifyPropertyChanged("CurrencyNormalFontFormatTxt");
                NotifyPropertyChanged("CurrencySpecialFontFormatTxt");
            }
        }

        private string _UnitPriceDecimalPlaces;
        public string UnitPriceDecimalPlaces
        {
            get
            {
                return _UnitPriceDecimalPlaces;
            }

            set
            {
                _UnitPriceDecimalPlaces = value;
                OnPropertyChanged("UnitPriceDecimalPlaces");
                NotifyPropertyChanged("UnitPriceTxt");
            }
        }

        private string _QuantityDecimalPlaces;
        public string QuantityDecimalPlaces
        {
            get
            {
                return _QuantityDecimalPlaces;
            }

            set
            {
                _QuantityDecimalPlaces = value;
                OnPropertyChanged("QuantityDecimalPlaces");
                NotifyPropertyChanged("QuantityTxt");
            }
        }


        private string _CurrencyNormalFontSymbol;
        public string CurrencyNormalFontSymbol
        {
            get
            {
                return _CurrencyNormalFontSymbol;
            }

            set
            {
                _CurrencyNormalFontSymbol = value;
                OnPropertyChanged("CurrencyNormalFontSymbol");
                NotifyPropertyChanged("CurrencyNormalFontFormatTxt");
            }
        }

        private string _CurrencySpecialFontSymbol;
        public string CurrencySpecialFontSymbol
        {
            get
            {
                return _CurrencySpecialFontSymbol;
            }

            set
            {
                _CurrencySpecialFontSymbol = value;
                OnPropertyChanged("CurrencySpecialFontSymbol");
                NotifyPropertyChanged("CurrencySpecialFontFormatTxt");
            }
        }


        private string _NumberFormatTxt;
        public string NumberFormatTxt
        {
            get
            {
                if (NumberFormatRadio1)
                {
                    _NumberFormatTxt = "12,34,56,789";

                }
                else
                {
                    _NumberFormatTxt = "123,456,789";
                }

                string decimalTxt = "";
                if (String.IsNullOrEmpty(NumberFormatDecimalPlace))
                {
                    decimalTxt = ".00";
                }
                else
                {
                    long decPlace;
                    long.TryParse(NumberFormatDecimalPlace, out decPlace);
                    if (decPlace == 0)
                    {
                        decimalTxt = "";
                    }
                    else if (decPlace < 100)
                    {
                        decimalTxt = ".";
                        for (int i = 0; i < decPlace; i++)
                        {
                            decimalTxt += "0";
                        }
                    }
                    else if (decPlace >= 100 && decPlace < 10000000000)
                    {
                        decimalTxt = ".";
                        decimalTxt += "D" + Convert.ToInt64(decPlace);
                    }
                    else if (decPlace >= 10000000000)
                    {
                        decimalTxt = ".";
                        decimalTxt += "D-2147483648";
                    }
                }

                _NumberFormatTxt = _NumberFormatTxt + decimalTxt;
                return _NumberFormatTxt;
            }

            set
            {
                _NumberFormatTxt = value;
                OnPropertyChanged("NumberFormatTxt");
            }
        }

        private string _UnitPriceTxt;
        public string UnitPriceTxt
        {
            get
            {
                if (NumberFormatRadio1)
                {
                    _UnitPriceTxt = "12,34,56,789";

                }
                else
                {
                    _UnitPriceTxt = "123,456,789";
                }

                string decimalTxt = "";
                if (String.IsNullOrEmpty(UnitPriceDecimalPlaces))
                {
                    decimalTxt = ".00";
                }
                else
                {
                    long decPlace;
                    long.TryParse(UnitPriceDecimalPlaces, out decPlace);
                    if (decPlace == 0)
                    {
                        decimalTxt = "";
                    }
                    else if (decPlace < 100)
                    {
                        decimalTxt = ".";
                        for (int i = 0; i < decPlace; i++)
                        {
                            decimalTxt += "0";
                        }
                    }
                    else if (decPlace >= 100 )
                    {
                        decimalTxt = ".";
                        decimalTxt += "D" + Convert.ToInt64(decPlace);
                    }
                    
                }

                _UnitPriceTxt = _UnitPriceTxt + decimalTxt;
                return _UnitPriceTxt;
            }

            set
            {
                _UnitPriceTxt = value;
                OnPropertyChanged("UnitPriceTxt");
            }
        }

        private string _QuantityTxt;
        public string QuantityTxt
        {
            get
            {
                if (NumberFormatRadio1)
                {
                    _QuantityTxt = "12,34,56,789";

                }
                else
                {
                    _QuantityTxt = "123,456,789";
                }

                string decimalTxt = "";
                if (String.IsNullOrEmpty(QuantityDecimalPlaces))
                {
                    decimalTxt = ".00";
                }
                else
                {
                    long decPlace;
                    long.TryParse(QuantityDecimalPlaces, out decPlace);
                    if (decPlace == 0)
                    {
                        decimalTxt = "";
                    }
                    else if (decPlace < 100)
                    {
                        decimalTxt = ".";
                        for (int i = 0; i < decPlace; i++)
                        {
                            decimalTxt += "0";
                        }
                    }
                    else if (decPlace >= 100)
                    {
                        decimalTxt = ".";
                        decimalTxt += "D" + Convert.ToInt64(decPlace);
                    }

                }

                _QuantityTxt = _QuantityTxt + decimalTxt;
                return _QuantityTxt;
            }

            set
            {
                _QuantityTxt = value;
                OnPropertyChanged("QuantityTxt");
            }
        }

        private string _CurrencySpecialFontFormatTxt;
        public string CurrencySpecialFontFormatTxt
        {
            get
            {
                if (NumberFormatRadio1)
                {
                    _CurrencySpecialFontFormatTxt = "12,34,56,789";

                }
                else
                {
                    _CurrencySpecialFontFormatTxt = "123,456,789";
                }
                string decimalTxt = "";
                if (String.IsNullOrEmpty(NumberFormatDecimalPlace))
                {
                    decimalTxt = ".00";
                }
                else
                {
                    long decPlace;
                    long.TryParse(NumberFormatDecimalPlace, out decPlace);
                    if (decPlace == 0)
                    {
                        decimalTxt = "";
                    }
                    else if (decPlace < 100)
                    {
                        decimalTxt = ".";
                        for (int i = 0; i < decPlace; i++)
                        {
                            decimalTxt += "0";
                        }
                    }
                    else if (decPlace >= 100 && decPlace < 10000000000)
                    {
                        decimalTxt = ".";
                        decimalTxt += "D" + Convert.ToInt64(decPlace);
                    }
                    else if (decPlace >= 10000000000)
                    {
                        decimalTxt = ".";
                        decimalTxt += "D-2147483648";
                    }
                }
                _CurrencySpecialFontFormatTxt = _CurrencySpecialFontFormatTxt + decimalTxt;
                /*if (SpecialFontLeftRadio)
                {
                    _CurrencySpecialFontFormatTxt = CurrencySpecialFontSymbol + _CurrencySpecialFontFormatTxt;
                }
                else
                {
                    _CurrencySpecialFontFormatTxt = _CurrencySpecialFontFormatTxt + CurrencySpecialFontSymbol;
                }*/

                return _CurrencySpecialFontFormatTxt;
            }

            set
            {
                _CurrencySpecialFontFormatTxt = value;
                OnPropertyChanged("CurrencySpecialFontFormatTxt");
            }
        }


        private string _CurrencyNormalFontFormatTxt;
        public string CurrencyNormalFontFormatTxt
        {
            get
            {
                if (NumberFormatRadio1)
                {
                    _CurrencyNormalFontFormatTxt = "12,34,56,789";

                }
                else
                {
                    _CurrencyNormalFontFormatTxt = "123,456,789";
                }

                string decimalTxt = "";
                if (String.IsNullOrEmpty(NumberFormatDecimalPlace))
                {
                    decimalTxt = ".00";
                }
                else
                {
                    long decPlace;
                    long.TryParse(NumberFormatDecimalPlace, out decPlace);
                    if (decPlace == 0)
                    {
                        decimalTxt = "";
                    }
                    else if (decPlace < 100)
                    {
                        decimalTxt = ".";
                        for (int i = 0; i < decPlace; i++)
                        {
                            decimalTxt += "0";
                        }
                    }
                    else if (decPlace >= 100 && decPlace < 10000000000)
                    {
                        decimalTxt = ".";
                        decimalTxt += "D" + Convert.ToInt64(decPlace);
                    }
                    else if (decPlace >= 10000000000)
                    {
                        decimalTxt = ".";
                        decimalTxt += "D-2147483648";
                    }
                }
                _CurrencyNormalFontFormatTxt = _CurrencyNormalFontFormatTxt + decimalTxt;
                if (NorrmalFontLeftRadio)
                {
                    _CurrencyNormalFontFormatTxt = CurrencyNormalFontSymbol + _CurrencyNormalFontFormatTxt;
                }
                else
                {
                    _CurrencyNormalFontFormatTxt = _CurrencyNormalFontFormatTxt + CurrencyNormalFontSymbol;
                }
                return _CurrencyNormalFontFormatTxt;
            }

            set
            {
                _CurrencyNormalFontFormatTxt = value;
                OnPropertyChanged("CurrencyNormalFontFormatTxt");
            }
        }


        private string _CurrencySymbolFont;
        public string CurrencySymbolFont
        {
            get
            {
                return _CurrencySymbolFont;
            }

            set
            {
                _CurrencySymbolFont = value;
                OnPropertyChanged("CurrencySymbolFont");
            }
        }

        

        #endregion
       
        #region CommandBinding
        private ICommand _CurrencyClick;
        public ICommand CurrencyClick
        {
            get
            {
                if (_CurrencyClick == null)
                {
                    _CurrencyClick = new DelegateCommand(Currency_Click);
                }
                return _CurrencyClick;
            }
        }

        public void Currency_Click()
        {
            CurrencySettings currencySettingsView = new CurrencySettings();
            currencySettingsView.Show();

        }


        private ICommand _ApplyCurrencySettings;
        public ICommand ApplyCurrencySettings
        {
            get
            {
                if (_ApplyCurrencySettings == null)
                {
                    _ApplyCurrencySettings = new DelegateCommand(Apply_CurrencySettings);
                }
                return _ApplyCurrencySettings;
            }
        }

        public void Apply_CurrencySettings()
        {

            SelectedCurrencySettings.NORMAL_CURRENCY_SYMBOL = CurrencyNormalFontSymbol;
            SelectedCurrencySettings.NORMAL_CURRENCY_SYMBOL_LEFT = NorrmalFontLeftRadio;
            if (!String.IsNullOrEmpty(NumberFormatDecimalPlace))
            {
                SelectedCurrencySettings.NUMBER_DECIMAL_PLACES = Convert.ToInt32(NumberFormatDecimalPlace);
            }
            else
            {
                SelectedCurrencySettings.NUMBER_DECIMAL_PLACES = 2;
            }
            SelectedCurrencySettings.NUMBER_FORMAT_CHOICE_2 = NumberFormatRadio2;

            if (!String.IsNullOrEmpty(QuantityDecimalPlaces))
            {

                SelectedCurrencySettings.QUANTITY_DECIMAL_PLACES = Convert.ToInt32(QuantityDecimalPlaces);
            }
            else
            {
                SelectedCurrencySettings.QUANTITY_DECIMAL_PLACES = 2;
            }
            SelectedCurrencySettings.SPECIAL_CURRENCY_SYMBOL = CurrencySpecialFontSymbol;
            SelectedCurrencySettings.SPECIAL_CURRENCY_SYMBOL_LEFT = SpecialFontLeftRadio;
            if (!String.IsNullOrEmpty(UnitPriceDecimalPlaces))
            {

                SelectedCurrencySettings.UNIT_PRICE_DECIMAL_PLACES = Convert.ToInt32(UnitPriceDecimalPlaces);
            }
            else
            {
                SelectedCurrencySettings.UNIT_PRICE_DECIMAL_PLACES = 2;
            }

            if ((SelectedCurrencySettings.NUMBER_DECIMAL_PLACES > 4)
                || (SelectedCurrencySettings.QUANTITY_DECIMAL_PLACES > 4)
                || (SelectedCurrencySettings.UNIT_PRICE_DECIMAL_PLACES > 4))
            {
                MessageBox.Show("You can only specify number of decimal places in the range of 1 to 4. If you specify 0 default currency format will be used", "Error", MessageBoxButton.OK);
            }
            else
            {

                using (ResXResourceWriter resx = new ResXResourceWriter("..\\..\\Resources\\Settings.resx"))
                {
                    resx.AddResource("FONT_FAMILY", SelectedCurrencySettings.FONT_FAMILY);
                    resx.AddResource("FONT_WEIGHT", SelectedCurrencySettings.FONT_WEIGHT);
                    resx.AddResource("FONT_STYLE", SelectedCurrencySettings.FONT_STYLE);
                    resx.AddResource("FONT_STRETCH", SelectedCurrencySettings.FONT_STRETCH);
                    resx.AddResource("FONT_SIZE", SelectedCurrencySettings.FONT_SIZE);
                    resx.AddResource("TEXT_DECOR_UNDER_LINE", SelectedCurrencySettings.TEXT_DECOR_UNDER_LINE);
                    resx.AddResource("TEXT_DECOR_STRIKE_THROUGH", SelectedCurrencySettings.TEXT_DECOR_STRIKE_THROUGH);
                    resx.AddResource("TEXT_DECOR_OVER_LINE", SelectedCurrencySettings.TEXT_DECOR_OVER_LINE);
                    resx.AddResource("TEXT_DECOR_BASE_LINE", SelectedCurrencySettings.TEXT_DECOR_BASE_LINE);
                    resx.AddResource("NORMAL_CURRENCY_SYMBOL", SelectedCurrencySettings.NORMAL_CURRENCY_SYMBOL);
                    resx.AddResource("NORMAL_CURRENCY_SYMBOL_LEFT", SelectedCurrencySettings.NORMAL_CURRENCY_SYMBOL_LEFT);
                    resx.AddResource("SPECIAL_CURRENCY_SYMBOL", SelectedCurrencySettings.SPECIAL_CURRENCY_SYMBOL);
                    resx.AddResource("SPECIAL_CURRENCY_SYMBOL_LEFT", SelectedCurrencySettings.SPECIAL_CURRENCY_SYMBOL_LEFT);
                    resx.AddResource("NUMBER_FORMAT_CHOICE_2", SelectedCurrencySettings.NUMBER_FORMAT_CHOICE_2);
                    resx.AddResource("NUMBER_DECIMAL_PLACES", SelectedCurrencySettings.NUMBER_DECIMAL_PLACES);
                    resx.AddResource("UNIT_PRICE_DECIMAL_PLACES", SelectedCurrencySettings.UNIT_PRICE_DECIMAL_PLACES);
                    resx.AddResource("QUANTITY_DECIMAL_PLACES", SelectedCurrencySettings.QUANTITY_DECIMAL_PLACES);
                    resx.AddResource("CODE_PAGE", SelectedCurrencySettings.CODE_PAGE);
                }

                if (MessageBox.Show("Changes to currency settings will not be applied will not be applied to main Invoice Screen until you restart application. Do you want to restart application now?", "Restart Application", MessageBoxButton.OKCancel) == System.Windows.MessageBoxResult.OK)
                {
                    System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                };
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
                if (App.Current.Windows[intCounter].Title == "Currency Settings")
                {
                    App.Current.Windows[intCounter].Close();
                }
            }
        }
        #endregion

        #region Interface Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {;
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
    }
}
