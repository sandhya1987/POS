using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace InvoicePOS.ViewModels.Customer
{
    class CustomerCommand : ICommand
    {
        private readonly CustomerViewModel _CM;
        public CustomerCommand(CustomerViewModel vm)
        {
            _CM = vm;
        }
        public void Execute(object parameter)
        {
            _CM.Add();
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged;
    }
}
