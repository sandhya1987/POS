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

namespace InvoicePOS.UserControll.SuppPayment
{
    /// <summary>
    /// Interaction logic for SuppPayList.xaml
    /// </summary>
    public partial class SuppPayList : UserControl
    {
        SuppPayViewModel _SuppVwModel;
        
        public SuppPayList()
        {
            InitializeComponent();
            _SuppVwModel = new SuppPayViewModel();
            this.DataContext = _SuppVwModel;
        }

        private void Win_grdSupppay_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //var str = (TaxManagement)dataGrid1.SelectedItem;
            _SuppVwModel.view_SuppPay();
        }

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    MyStack.Children.Clear();
        //    SuppPayAdd _AC = new SuppPayAdd();
        //    MyStack.Children.Add(_AC);
        //}
    }
}
