using InvoicePOS.Models;
using InvoicePOS.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace InvoicePOS.UserControll.Report.PO_Report
{
    /// <summary>
    /// Interaction logic for ReportPO.xaml
    /// </summary>
    public partial class ReportPO : Window
    {
        POViewModel _POViewModel;
        public static DataGrid ListGridRef;
        public ReportPO()
        {
            InitializeComponent();
            _POViewModel = new POViewModel();
            this.DataContext = _POViewModel;
            List<POrderModel> ListGrid = new List<POrderModel>();


            ListGrid = App.Current.Properties["DataGrid1"] as List<POrderModel>;

            dataGrid1.ItemsSource = ListGrid;
            ListGridRef = dataGrid1;
        }
    }
}
