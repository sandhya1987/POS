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

namespace InvoicePOS.UserControll.SalesReturn
{
    /// <summary>
    /// Interaction logic for salesReturnList.xaml
    /// </summary>
    public partial class salesReturnList : UserControl
    {
        SalesReturnViewModel _SalesReturnModel;
        public salesReturnList()
        {
            InitializeComponent();
            _SalesReturnModel = new SalesReturnViewModel();
            this.DataContext = _SalesReturnModel;
        }

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    MyStack.Children.Clear();
        //    SalesReturnAdd _AC = new SalesReturnAdd();
        //    MyStack.Children.Add(_AC);
        //}
        private void grdSales_Return_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var str = (SalesReturnModel)dataGrid1.SelectedItem;
            _SalesReturnModel.View_SalesReturn();
        }
    }
}
