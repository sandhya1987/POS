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

namespace InvoicePOS.UserControll.AccessRide
{
    /// <summary>
    /// Interaction logic for AccessRide.xaml
    /// </summary>
    public partial class AccessRide : UserControl
    {
        AccessRideViewModel _AccessRideViewModel;
        public AccessRide()
        {
            InitializeComponent();
            _AccessRideViewModel = new AccessRideViewModel();
            this.DataContext = _AccessRideViewModel;
        }
    }
}
