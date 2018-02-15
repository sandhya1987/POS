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

namespace InvoicePOS.UserControll.PO
{
    /// <summary>
    /// Interaction logic for POList.xaml
    /// </summary>
    public partial class POList : UserControl
    {
        POViewModel _POViewModel;
        public POList()
        {
            InitializeComponent();
            _POViewModel = new POViewModel();
            this.DataContext = _POViewModel;
        }

        private void grdPO_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var str = (POrderModel)dataGrid1.SelectedItem;
            _POViewModel.View_PO();
        }
    }
}
