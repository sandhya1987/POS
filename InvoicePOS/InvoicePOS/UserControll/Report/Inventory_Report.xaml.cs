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

namespace InvoicePOS.UserControll.Report
{
    /// <summary>
    /// Interaction logic for Inventory_Report.xaml
    /// </summary>
    public partial class Inventory_Report : Window
    {
        ItemViewModel _IVM;
        public Inventory_Report()
        {
            InitializeComponent();
            _IVM = new ItemViewModel();
            this.DataContext = _IVM;
        }
    }
}
