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

namespace InvoicePOS.UserControll.PO
{
    /// <summary>
    /// Interaction logic for Window_POList.xaml
    /// </summary>
    public partial class Window_POList : Window
    {
        POViewModel _POViewModel;
        public Window_POList()
        {
            InitializeComponent();
            _POViewModel = new POViewModel();
            this.DataContext = _POViewModel;
        }
    }
}
