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

namespace InvoicePOS.UserControll.Loyalty
{
    /// <summary>
    /// Interaction logic for AddLoyalty.xaml
    /// </summary>
    public partial class AddLoyalty : Window
    {
        LoyaltyViewModel _LoyaltyViewModel;
        public static TextBox LoyaltyCoustomerGroup;
        public AddLoyalty()
        {
            InitializeComponent();
                 _LoyaltyViewModel = new LoyaltyViewModel();
            this.DataContext = _LoyaltyViewModel;

            CustomerGroup.Text = "";
            LoyaltyCoustomerGroup = CustomerGroup;
        }
    }
}
