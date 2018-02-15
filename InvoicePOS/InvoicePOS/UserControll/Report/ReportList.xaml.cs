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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InvoicePOS.UserControll.Report
{
    /// <summary>
    /// Interaction logic for ReportList.xaml
    /// </summary>
    public partial class ReportList : UserControl
    {
        
        ReportViewModel _ReportViewModel;
       public static TreeView TreeViewRef;
        public ReportList()
        {
            InitializeComponent();
        _ReportViewModel = new ReportViewModel();
            this.DataContext = _ReportViewModel;
            List<ReportGroupModel> Children = new List<ReportGroupModel>();

            Children = App.Current.Properties["ChildrenGroup"] as List<ReportGroupModel>;
            tv.ItemsSource = Children;
        }

        private void dataGrid1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
