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

namespace InvoicePOS.UserControll.Company
{
    /// <summary>
    /// Interaction logic for BusinessLocationView.xaml
    /// </summary>
    public partial class BusinessLocationView : Window
    {

        public BusinessLocationView()
        {
            BussinessLocationViewModel _BussinessLocationViewModel;
            InitializeComponent();
            _BussinessLocationViewModel = new BussinessLocationViewModel();
            this.DataContext = _BussinessLocationViewModel;
        }
    }
}
