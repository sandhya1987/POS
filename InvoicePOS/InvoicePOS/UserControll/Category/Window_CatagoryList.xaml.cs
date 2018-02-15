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

namespace InvoicePOS.UserControll.Category
{
    /// <summary>
    /// Interaction logic for Window_CatagoryList.xaml
    /// </summary>
    public partial class Window_CatagoryList : Window
    {
        CategoryViewModel _CVM;
        public static DataGrid CatGridRef;
        public Window_CatagoryList()
        {
            ObservableCollection<CategoryModel> ListGrid = new ObservableCollection<CategoryModel>();
            InitializeComponent();
            _CVM = new CategoryViewModel();
            this.DataContext = _CVM;


            dataGrid1.ItemsSource = ListGrid;
            CatGridRef = dataGrid1;
        }
        private void Win_grdCatagory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _CVM.Select_Ok();
        }
    }
}
