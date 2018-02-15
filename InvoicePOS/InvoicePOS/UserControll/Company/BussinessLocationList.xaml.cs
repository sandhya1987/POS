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

namespace InvoicePOS.UserControll.Company
{
    /// <summary>
    /// Interaction logic for BussinessLocationList.xaml
    /// </summary>
    public partial class BussinessLocationList : UserControl
    {
        BussinessLocationViewModel CMPViewModel; 
        public BussinessLocationList()
        {
            InitializeComponent();
            CMPViewModel = new BussinessLocationViewModel();
            this.DataContext = CMPViewModel;
        }

        private void grdBusiness_Loc_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var str = (BusinessLocationModel)dataGrid1.SelectedItem;
            CMPViewModel.View_BussLoc();
        }
    }
}
