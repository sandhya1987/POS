using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InvoicePOS.Helper
{
    class Simple
    {
        public class SimpleCommand<T> : ICommand
        {
            public Predicate<T> CanExecuteDelegate { get; set; }
            public Action<T> ExecuteDelegate { get; set; }

            #region ICommand Members

            public bool CanExecute(object parameter)
            {
                if (CanExecuteDelegate != null)
                    return CanExecuteDelegate((T)parameter);
                return true;// if there is no can execute default to true
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public void Execute(object parameter)
            {
                if (ExecuteDelegate != null)
                    ExecuteDelegate((T)parameter);
            }

            #endregion
        }
    }
}
