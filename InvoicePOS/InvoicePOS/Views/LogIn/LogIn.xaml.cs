using InvoicePOS.ViewModels;
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
using System.Configuration;

namespace InvoicePOS.Views.LogIn
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        LogInViewModel _LogInViewModel;
        public LogIn()
        {
            string language = ConfigurationManager.AppSettings["Language"];
            if (language.ToUpper() == "ENGLISH")
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            }
            else if(language.ToUpper() == "GERMAN")
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-DE");
            }
            InitializeComponent();
            _LogInViewModel = new LogInViewModel();
            this.DataContext = _LogInViewModel;
        }
    }
}
