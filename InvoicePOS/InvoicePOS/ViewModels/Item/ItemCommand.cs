using InvoicePOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
namespace InvoicePOS.ViewModels.Item
{
    class ItemCommand : ICommand
    {
        private readonly ItemViewModel _IM;
        public ItemCommand(ItemViewModel im)
        {
            _IM = im;
        }
        public void Execute(object parameter)
        {
           // _IM.Add();
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged;
       
    }
}
