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

namespace InvoicePOS.UserControll.GoDown
{
    /// <summary>
    /// Interaction logic for GodownList.xaml
    /// </summary>
    public partial class GodownList : UserControl
    {
        GodownViewModel CMPViewModel; 
        public GodownList()
        {
            InitializeComponent();
            CMPViewModel = new GodownViewModel();
            this.DataContext = CMPViewModel;
        }

        private void grdGodown_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var str = (GodownModel)dataGrid1.SelectedItem;
            CMPViewModel.ViewGOdown();
        }
    }
}
