using InvoicePOS.Models;
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

namespace InvoicePOS.UserControll.Report
{
    /// <summary>
    /// Interaction logic for ReportAdd.xaml
    /// </summary>
    public partial class ReportAdd : Window
    {
        ReportViewModel _ReportGroupModel;
        public static TreeView TreeViewRef;
        public ReportAdd()
        {
            InitializeComponent();
          //  ReportGroupModel rootPerson = new ReportGroupModel();
           _ReportGroupModel = new ReportViewModel();
            this.DataContext = _ReportGroupModel;
            List<ReportGroupModel> Children = new List<ReportGroupModel>();

            Children = App.Current.Properties["ChildrenGroup"] as List<ReportGroupModel>;
            tv.ItemsSource = Children;
        }
    }
}
