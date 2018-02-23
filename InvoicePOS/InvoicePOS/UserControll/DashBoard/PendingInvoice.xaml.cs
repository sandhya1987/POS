using InvoicePOS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InvoicePOS.UserControll.Dashboard
{
    /// <summary>
    /// Interaction logic for PendingInvoice.xaml
    /// </summary>
    public partial class PendingInvoice : Window
    {
        DashboardViewModel  _DashboardViewModel = new DashboardViewModel();
        public static Chart AreaChart;
        public static Chart BarChart;
        public static Chart LineChart;
        public PendingInvoice()
        {
            InitializeComponent();
            _DashboardViewModel = new DashboardViewModel();
            this.DataContext = _DashboardViewModel;
            AreaChart1 = AreaChart;
            BarChart1 = BarChart;
            LineChart1 = LineChart;
        }
    }
}
