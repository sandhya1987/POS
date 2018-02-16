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
    /// Interaction logic for InvoiceSettings.xaml
    /// </summary>
    public partial class InvoiceSettings : Window
    {
        public InvoiceSettings()
        {
            InvoiceSettingsViewModel _ISVM = new InvoiceSettingsViewModel();
            this.DataContext = _ISVM;
            InitializeComponent();
        }

        private void OnNumberFormatDecimalPlaceKeyDown(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }

        public static bool IsValid(string str)
        {
            long l;
            return long.TryParse(str, out l);
        }

        private void Operation2Checked(object sender, RoutedEventArgs e)
        {
            Operation3.IsEnabled = true;
        }
        private void Operation2UnChecked(object sender, RoutedEventArgs e)
        {
            Operation3.IsChecked = false;
            Operation3.IsEnabled = false;
        }

        private void SalesEx3_Checked(object sender, RoutedEventArgs e)
        {
            SalesEx4.IsEnabled = true;

        }
        private void SalesEx3_UnChecked(object sender, RoutedEventArgs e)
        {
            SalesEx4.IsChecked = false;
            SalesEx4.IsEnabled = false;
        }

        private void SalesEx4_Checked(object sender, RoutedEventArgs e)
        {
            SalesEx5.IsEnabled = true;
            SalesEx5.IsChecked = true;
            SalesEx6.IsEnabled = true;

        }
        private void SalesEx4_UnChecked(object sender, RoutedEventArgs e)
        {
            SalesEx5.IsChecked = false;
            SalesEx5.IsEnabled = false;
            SalesEx6.IsChecked = false;
            SalesEx6.IsEnabled = false;
        }

        private void SalesEx5_Checked(object sender, RoutedEventArgs e)
        {
            
            SalesEx6.IsChecked = false;

        }
        private void SalesEx5_UnChecked(object sender, RoutedEventArgs e)
        {
            if (SalesEx4.IsChecked == true)
            {
                SalesEx6.IsChecked = true;
            }
        }

        private void SalesEx6_Checked(object sender, RoutedEventArgs e)
        {

            SalesEx5.IsChecked = false;

        }
        private void SalesEx6_UnChecked(object sender, RoutedEventArgs e)
        {
            if (SalesEx4.IsChecked == true)
            {
                SalesEx5.IsChecked = true;
            }
        }
    }
}
