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

namespace InvoicePOS.UserControll.Designation
{
    /// <summary>
    /// Interaction logic for Window_Designation.xaml
    /// </summary>
    public partial class Window_Designation : Window
    {
        DesignationViewModel _DesignationModel;
        public Window_Designation()
        {
            InitializeComponent();
            _DesignationModel = new DesignationViewModel();
            this.DataContext = _DesignationModel;
        }
        private void Win_grdDesignation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //var str = (TaxManagement)dataGrid1.SelectedItem;
            _DesignationModel.Select_Ok();
        }
    }
}
