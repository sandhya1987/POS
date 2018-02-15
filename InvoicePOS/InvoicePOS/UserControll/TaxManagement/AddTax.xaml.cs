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

namespace InvoicePOS.UserControll.TaxManagement
{
    /// <summary>
    /// Interaction logic for AddTax.xaml
    /// </summary>
    public partial class AddTax : Window
    {
        public AddTax()
        {
            TaxViewModel ViewModel;
            InitializeComponent();
            ViewModel = new TaxViewModel();
            this.DataContext = ViewModel;
        }
    }
}
