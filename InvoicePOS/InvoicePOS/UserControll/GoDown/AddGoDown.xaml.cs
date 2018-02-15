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

namespace InvoicePOS.UserControll.GoDown
{
    /// <summary>
    /// Interaction logic for AddGoDown.xaml
    /// </summary>
    public partial class AddGoDown : Window
    {
        GodownViewModel _GodownViewModel;
        public AddGoDown()
        {
            InitializeComponent();
            _GodownViewModel = new GodownViewModel();
            this.DataContext = _GodownViewModel;
        }
    }
}
