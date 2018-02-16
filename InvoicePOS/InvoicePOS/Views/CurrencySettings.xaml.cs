using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InvoicePOS.ViewModels;

namespace InvoicePOS.Views
{
    /// <summary>
    /// Interaction logic for CurrencySettings.xaml
    /// </summary>
    public partial class CurrencySettings : Window
    {
        public CurrencySettings()
        {
            InitializeComponent();
            CurrencySettingsViewModel _CSVM = new CurrencySettingsViewModel();
            this.DataContext = _CSVM;
        }

        private void OnNumberFormatDecimalPlaceKeyDown(object sender, TextCompositionEventArgs e )
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }

        public static bool IsValid(string str)
        {
            long l;
            return long.TryParse(str, out l);
        }

        private void OnFontChange(object sender, RoutedEventArgs e)
        {
            FontChooser fontChooser = new FontChooser();
            fontChooser.Owner = this;

            fontChooser.SetPropertiesFromObject(FontName);
            fontChooser.PreviewSampleText = FontName.Text;

            if (fontChooser.ShowDialog().Value)
            {
                fontChooser.ApplyPropertiesToObject(FontName);
                fontChooser.ApplyPropertiesToObject(SpecialFontTextBox);
                fontChooser.ApplyPropertiesToObject(CurrencySpecialFontSymbolLeft);
                fontChooser.ApplyPropertiesToObject(CurrencySpecialFontSymbol);
            }

        }

        
    }
}
