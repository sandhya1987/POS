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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InvoicePOS.UserControll.Company
{
    /// <summary>
    /// Interaction logic for BusinessLocationAdd.xaml
    /// </summary>
    public partial class BusinessLocationAdd : Window
    {
        BussinessLocationViewModel _BussinessLocationViewModel;
        public BusinessLocationAdd()
        {
            InitializeComponent();
            _BussinessLocationViewModel = new BussinessLocationViewModel();
            this.DataContext = _BussinessLocationViewModel;

        }
    }
}
