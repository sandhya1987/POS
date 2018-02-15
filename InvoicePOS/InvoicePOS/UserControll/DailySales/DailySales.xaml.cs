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

namespace InvoicePOS.UserControll.DailySales
{
    /// <summary>
    /// Interaction logic for DailySales.xaml
    /// </summary>
    public partial class DailySales : UserControl
    {
        DailySalesViewModel _WCVM;
        public static TextBox DailySalesBussreff;
        public DailySales()
        {
            InitializeComponent();
            _WCVM = new DailySalesViewModel();
            this.DataContext = _WCVM;

            BussLocation.Text = "";
            DailySalesBussreff=BussLocation;
        }

       
    }
}
