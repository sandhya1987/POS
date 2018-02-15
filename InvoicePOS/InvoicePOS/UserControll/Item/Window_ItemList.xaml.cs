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

namespace InvoicePOS.UserControll.Item
{
    /// <summary>
    /// Interaction logic for Window_ItemList.xaml
    /// </summary>
    public partial class Window_ItemList : Window
    {
        ItemViewModel _ItemViewModel;
        //BussinessLocationViewModel _BussinessLocationViewModel;
        public Window_ItemList()
        {
            InitializeComponent();
            _ItemViewModel = new ItemViewModel();

            //_BussinessLocationViewModel = new BussinessLocationViewModel();

            this.DataContext = _ItemViewModel;
        }
        private void grdwinItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           // var str = (ItemModel)dataGrid1.SelectedItem;
            _ItemViewModel.Select_Ok();
        }
        
    }
}
