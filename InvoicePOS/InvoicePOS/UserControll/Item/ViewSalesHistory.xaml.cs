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
using System.Windows.Shapes;

namespace InvoicePOS.UserControll.Item
{
    /// <summary>
    /// Interaction logic for ViewSalesHistory.xaml
    /// </summary>
    public partial class ViewSalesHistory : Window
    {
        ItemViewModel _ItemViewModel;
        public static DataGrid datagridref;
        public ObservableCollection<ItemModel> ListGrid1 = new ObservableCollection<ItemModel>();
        public ViewSalesHistory()
        {

            InitializeComponent();
            _ItemViewModel = new ItemViewModel();
            this.DataContext = _ItemViewModel;

            //dataGrid.ItemsSource = ListGrid1;
            //datagridref = dataGrid;
        }
    }
}
