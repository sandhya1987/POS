using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;



namespace InvoicePOS.ViewModels.Company
{
    class CompanyCommand:ICommand
    {

         private readonly CompanyViewModel _CM;
         public CompanyCommand(CompanyViewModel vm)
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
