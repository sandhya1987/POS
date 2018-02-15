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

namespace InvoicePOS.UserControll.Designation
{
    /// <summary>
    /// Interaction logic for DesignationView.xaml
    /// </summary>
    public partial class DesignationView : Window
    {
        DesignationViewModel _DesignationModel;
        public DesignationView()
        {
            InitializeComponent();
            _DesignationModel = new DesignationViewModel();
            this.DataContext = _DesignationModel;
        }
    }
}
