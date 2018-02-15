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
using InvoicePOS.Views;
using InvoicePOS.Models;

namespace InvoicePOS.UserControll.Item
{
    /// <summary>
    /// Interaction logic for Window_Opening_stock.xaml
    /// </summary>
    public partial class Window_Opening_stock : Window
    {
        ItemViewModel _ItemViewModel;
        public static AutoCompleteBusinessLoc BussRef;
        public static AutoCompleteGodown GodownRef;
        List<string> lsState = new List<string>();
        public Window_Opening_stock()
        {
            InitializeComponent();
            _ItemViewModel = new ItemViewModel();
            this.DataContext = _ItemViewModel;


            var GoDown = App.Current.Properties["GoDownList"] as List<AutoGodownModel>;
            var BussLoc = App.Current.Properties["BussLocList"] as List<AutoBussinessModel>;

            foreach (var item in BussLoc)
            {
                BussLocation.AddItem(new AutoBussinessModel
                {

                    DisplayId = item.DisplayId,
                    DisplayName = item.DisplayName

                });
            }

            foreach (var item in GoDown)
            {
                Godown.AddItem(new AutoGodownModel
                {

                    DisplayId = item.DisplayId,
                    DisplayName = item.DisplayName

                });
             }
           
            BussRef = BussLocation;



            //Godown.Text = "";
            GodownRef = Godown;

        }

        
    }
}
