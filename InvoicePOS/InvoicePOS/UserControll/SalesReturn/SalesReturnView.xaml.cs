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

namespace InvoicePOS.UserControll.SalesReturn
{
    /// <summary>
    /// Interaction logic for SalesReturnView.xaml
    /// </summary>
    public partial class SalesReturnView : Window
    {
        SalesReturnViewModel _SalesReturnModel;
        public SalesReturnView()
        {
            InitializeComponent();
            _SalesReturnModel = new SalesReturnViewModel();
            this.DataContext = _SalesReturnModel;
        }
    }
}
