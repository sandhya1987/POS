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
using InvoicePOS.ViewModels.Item;
using InvoicePOS.ViewModels;
using InvoicePOS.Models;

namespace InvoicePOS.UserControll.Item
{
    /// <summary>
    /// Interaction logic for ReceiveItems.xaml
    /// </summary>
    public partial class ReceiveItems : UserControl
    {

        ReceiveItemViewModel _WCVM;
        public ReceiveItems()
        {
            InitializeComponent();
            _WCVM = new ReceiveItemViewModel();
            this.DataContext = _WCVM;
        }

        private void grdReceive_Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var str = (ReceiveItem)dataGrid1.SelectedItem;
            _WCVM.ViewRecvItm_Click();
        }
    }
}
