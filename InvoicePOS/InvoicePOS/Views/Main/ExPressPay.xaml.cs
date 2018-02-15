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

namespace InvoicePOS.Views.Main
{
    /// <summary>
    /// Interaction logic for ExPressPay.xaml
    /// </summary>
    public partial class ExPressPay : Window
    {
        InvoiceViewModel ViewModel;
        public ExPressPay()
        {
            InitializeComponent();
            ViewModel = new InvoiceViewModel();
            this.DataContext = ViewModel;
        }
    }
}
