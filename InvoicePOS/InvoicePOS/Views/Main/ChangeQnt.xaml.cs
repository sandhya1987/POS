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

namespace InvoicePOS.Views.Main
{
    /// <summary>
    /// Interaction logic for ChangeQnt.xaml
    /// </summary>
    public partial class ChangeQnt : Window
    {
        ChangQntViewModel _ItemQntViewModel;
        public ChangeQnt()
        {
            InitializeComponent();
            _ItemQntViewModel = new ChangQntViewModel();
            this.DataContext = _ItemQntViewModel;
        }
    }
}
