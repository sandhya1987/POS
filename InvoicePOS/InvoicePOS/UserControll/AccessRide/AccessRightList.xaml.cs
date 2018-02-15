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

namespace InvoicePOS.UserControll.AccessRide
{
    /// <summary>
    /// Interaction logic for AccessRightList.xaml
    /// </summary>
    public partial class AccessRightList : Window
    {
        AccessRideViewModel _AccessRideViewModel;
        public static TextBlock EmpNameReff;
        public AccessRightList()
        {
            InitializeComponent();
            _AccessRideViewModel = new AccessRideViewModel();
            this.DataContext = _AccessRideViewModel;

            EmpName.Text = "";
            EmpNameReff = EmpName;
        }
    }
}
