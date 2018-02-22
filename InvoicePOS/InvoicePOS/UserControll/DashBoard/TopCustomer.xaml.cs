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
    /// Interaction logic for TopCustomer.xaml
    /// </summary>
    public partial class TopCustomer : Window
    {
        //DashboardViewModel _DashboardViewModel = new DashboardViewModel();
        public static Chart PieChart;
        public static Chart LineChart;
        public static Chart Barchart;
        public TopCustomer()
        {
            InitializeComponent();
            //_DashboardViewModel = new DashboardViewModel();
            //this.DataContext = _DashboardViewModel;

            PieChart = AreaChart1;
            LineChart = LineChart1;
            Barchart = BarChart1;
        }
    }
}
