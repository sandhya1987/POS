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
using InvoicePOS.ViewModels;
using InvoicePOS.Models;

namespace InvoicePOS.UserControll.Category
{
    /// <summary>
    /// Interaction logic for CategoryList.xaml
    /// </summary>
    public partial class CategoryList : UserControl
    {
        CategoryViewModel _CVM;
        public CategoryList()
        {
            //CategoryViewModel _CVM=new CategoryViewModel();
            InitializeComponent();
            _CVM = new CategoryViewModel();
            this.DataContext = _CVM;
        }

        private void grdCategory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var str = (CategoryModel)dataGrid1.SelectedItem;
            _CVM.View_Catagory();
        }
    }
}
