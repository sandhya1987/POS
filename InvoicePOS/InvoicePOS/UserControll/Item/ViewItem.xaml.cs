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
    /// Interaction logic for ViewItem.xaml
    /// </summary>
    public partial class ViewItem : Window
    {
        public static Image ImgCtrl ;
        ItemViewModel _ItemViewModel;
        public ViewItem()
        {
            InitializeComponent();
            _ItemViewModel = new ItemViewModel();
            this.DataContext = _ItemViewModel;


            ImgSource.Source = App.Current.Properties["ItemViewImg"] as ImageSource;
            App.Current.Properties["ItemViewImg"] = null;
            //ImgCtrl = ImgSource;
        }
    }
}
