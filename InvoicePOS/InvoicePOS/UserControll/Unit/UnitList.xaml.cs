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

namespace InvoicePOS.UserControll.Unit
{
    /// <summary>
    /// Interaction logic for UnitList.xaml
    /// </summary>
    public partial class UnitList : UserControl
    {
        UnitViewModel _UnitModel;
        public UnitList()
        {
            InitializeComponent();
            _UnitModel = new UnitViewModel();
            this.DataContext = _UnitModel;
        }

        private void grdUnit_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var str = (UnitModel)dataGrid.SelectedItem;
            _UnitModel.View_Unit();
        }
    }
}
