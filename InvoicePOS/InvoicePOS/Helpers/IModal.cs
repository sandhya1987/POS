using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InvoicePOS.Helpers
{
    public delegate void BackNavigationEventHandler(bool dialogReturn);

    public interface IModalService
    {
        void NavigateTo(UserControl uc, BackNavigationEventHandler backFromDialog);
        void GoBackward(bool dialogReturnValue);

    }
}
