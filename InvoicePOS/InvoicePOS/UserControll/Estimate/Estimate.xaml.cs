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

namespace InvoicePOS.UserControll.Estimate
{
    /// <summary>
    /// Interaction logic for Estimate.xaml
    /// </summary>
    public partial class Estimate : UserControl
    {
        EstimateInvoiceViewModel _EstimateInvoiceViewModel;
        public static DataGrid ListGridRef;
        public Estimate()
        {
            InitializeComponent();
            _EstimateInvoiceViewModel = new EstimateInvoiceViewModel();
            this.DataContext = _EstimateInvoiceViewModel;
        }
    }
}
