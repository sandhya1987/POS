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
using System.Windows.Shapes;

namespace InvoicePOS.UserControll.Report
{
    /// <summary>
    /// Interaction logic for GenerateReport.xaml
    /// </summary>
    public partial class GenerateReport : Window
    {

      //  readonly FamilyTreeViewModel _familyTree;
        readonly ReportViewModel _ReportViewModel;
        public GenerateReport()
        {
            InitializeComponent();
           // ReportGroupModel rootPerson = new ReportGroupModel();
            _ReportViewModel = new ReportViewModel();
            this.DataContext = _ReportViewModel;

            //ReportGroupModel rootPerson = _ReportViewModel.GetFamilyTree();

            // Create UI-friendly wrappers around the 
            // raw data objects (i.e. the view-model).
            // _ReportViewModel = new ReportViewModel(rootPerson);

            // Let the UI bind to the view-model.
            //base.DataContext = _ReportViewModel;






            //this.DataContext = _ReportViewModel;
        }
    }
}
