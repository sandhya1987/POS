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

namespace InvoicePOS.UserControll.DashBoard
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();
            DashboardViewModel _DBVM = new DashboardViewModel();
            this.DataContext = _DBVM;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem selectedTab = e.AddedItems[0] as TabItem;  // Gets selected tab

            string tabTitle = selectedTab.Header.ToString();
            switch (tabTitle)
            {
                case "Pending Invoices":
                    break;
                case "Pending Payment":
                    break;
                case "Top Product":
                    break;
                case "Top Customer":
                    break;
                case "Recent Invoices":
                    break;
                case "Top Vendor":
                    break;
                case "Pending Purchase Order":
                    break;
                case "Stock List":
                    break;
                default:
                    break;
            }
        }
    }
}
