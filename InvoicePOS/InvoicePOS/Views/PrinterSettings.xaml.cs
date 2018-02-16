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
using System.Management;

namespace InvoicePOS.Views
{
    /// <summary>
    /// Interaction logic for PrinterSettings.xaml
    /// </summary>
    public partial class PrinterSettings : Window
    {
        public PrinterSettings()
        {
            InitializeComponent();
            System.Drawing.Printing.PrinterSettings settings = new System.Drawing.Printing.PrinterSettings();
            string DefultPrinterName = settings.PrinterName;


            foreach (string item in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                com_Printers.Items.Add(item);
                if (item == DefultPrinterName)
                {
                    com_Printers.SelectedItem = item;
                }
            }
        }

        void OnSelectionChange(Object sender, RoutedEventArgs e)
        {
            var query = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");
            var printers = query.Get();
            foreach (ManagementObject printer in printers)
            {
                if (printer["name"].ToString() == com_Printers.SelectedItem.ToString())
                {
                    printer.InvokeMethod("SetDefaultPrinter", new object[] { com_Printers.SelectedItem.ToString() });
                }
            }
        }
    }
}
