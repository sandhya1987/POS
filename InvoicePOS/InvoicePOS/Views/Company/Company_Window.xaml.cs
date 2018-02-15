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

namespace InvoicePOS.Views.Company
{
    /// <summary>
    /// Interaction logic for Company.xaml
    /// </summary>
    public partial class Company_Window : Window
    {
        CompanyViewModel CMPViewModel;
        public Company_Window()
        {
           // App.Current.Properties["Prev_Page"] = "Company";
            InitializeComponent();
            CMPViewModel = new CompanyViewModel();
            this.DataContext = CMPViewModel;
        }
    }
}
