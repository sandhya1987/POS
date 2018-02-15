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

namespace InvoicePOS.UserControll.Item
{
    /// <summary>
    /// Interaction logic for ReceiveView.xaml
    /// </summary>
    public partial class ReceiveView : Window
    {
        ReceiveItemViewModel _WCVM;
        public ReceiveView()
        {
            InitializeComponent();
            _WCVM = new ReceiveItemViewModel();
            this.DataContext = _WCVM;
        }
    }
}
