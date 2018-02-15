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
using InvoicePOS.Models;

namespace InvoicePOS.UserControll.TaxManagement
{
    /// <summary>
    /// Interaction logic for Window_TaxList.xaml
    /// </summary>
    public partial class Window_TaxList : Window
    {
        TaxViewModel ViewModel;
        public Window_TaxList()
        {
            InitializeComponent();
            ViewModel = new TaxViewModel();
            this.DataContext = ViewModel;
        }
        private void Win_grdTex_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //var str = (TaxManagement)dataGrid1.SelectedItem;
            ViewModel.Select_Ok();
        }
    }
}
