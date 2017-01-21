using PlanchetUI.ViewModels;
using PlanchetUI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanchetUI.IServices
{
    public interface IBaseViewModelFactory
    {
        MainViewModel GetBaseViewModel(string name);

    }
}
