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

namespace InvoicePOS.Views.Unit
{
    /// <summary>
    /// Interaction logic for AddUnit.xaml
    /// </summary>
    public partial class AddUnit : Window
    {
        UnitViewModel _UnitViewModel;
        public AddUnit()
        {
            InitializeComponent();
            _UnitViewModel = new UnitViewModel();
            this.DataContext = _UnitViewModel;
        }
    }
}
