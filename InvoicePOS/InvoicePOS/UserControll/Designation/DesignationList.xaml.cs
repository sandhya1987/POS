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

namespace InvoicePOS.UserControll.Designation
{
    /// <summary>
    /// Interaction logic for DesignationList.xaml
    /// </summary>
    public partial class DesignationList : UserControl
    {
        DesignationViewModel _DesignationModel;
        public DesignationList()
        {
            InitializeComponent();
            _DesignationModel = new DesignationViewModel();
            this.DataContext = _DesignationModel;
        }
        private void Win_grdDesignation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //var str = (TaxManagement)dataGrid1.SelectedItem;
            _DesignationModel.View_Designation();
        }
    }
}
