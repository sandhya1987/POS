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

namespace InvoicePOS.UserControll.Unit
{
    /// <summary>
    /// Interaction logic for Window_UnitList.xaml
    /// </summary>
    public partial class Window_UnitList : Window
    {
        UnitViewModel _UnitModel;
        public Window_UnitList()
        {
            InitializeComponent();
            _UnitModel = new UnitViewModel();
            this.DataContext = _UnitModel;
        }
        private void Win_grdUnit_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //var str = (TaxManagement)dataGrid1.SelectedItem;
            _UnitModel.Select_Ok();
        }
    }
}
