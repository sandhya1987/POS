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

namespace InvoicePOS.UserControll.SuppPayment
{
    /// <summary>
    /// Interaction logic for SupplierPayView.xaml
    /// </summary>
    public partial class SupplierPayView : Window
    {

        SuppPayViewModel _WCVM;
        public SupplierPayView()
        {
            InitializeComponent();
            _WCVM = new SuppPayViewModel();
            this.DataContext = _WCVM;
        }
    }
}
