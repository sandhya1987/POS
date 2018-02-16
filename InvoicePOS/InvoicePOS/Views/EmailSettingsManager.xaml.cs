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
    /// Interaction logic for EmailSettingsManager.xaml
    /// </summary>
    public partial class EmailSettingsManager : Window
    {
        public EmailSettingsManager()
        {
            EmailSettingsViewModel _ESVM = new EmailSettingsViewModel();
            this.DataContext = _ESVM;
            InitializeComponent();
        }
    }
}
