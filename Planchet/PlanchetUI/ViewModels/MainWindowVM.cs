using Domain.Interfaces;
using Model.Entities.Image;
using PlanchetUI.IServices;
using PlanchetUI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PlanchetUI.ViewModels
{
    public class MainWindowVM : MainViewModel
    {
        #region Attributes
        IBaseViewModelFactory baseViewModelFactory;
        #endregion

        #region Properties

        public MainViewModel CurrentViewModel { get; set; }

        #endregion

        #region Builder
        public MainWindowVM(IBaseViewModelFactory baseViewModelFactory)
        {
            this.baseViewModelFactory = baseViewModelFactory;
            this.Initialize();
        }
        #endregion

        #region Commands

        public ICommand ChangeView
        {
            get
            {
                return new BaseCommand<Type>((viewModelType) =>
                {
                    CurrentViewModel = this.baseViewModelFactory.GetBaseViewModel(viewModelType.Name);
                    OnPropertyChanged(() => CurrentViewModel);
                    CurrentViewModel.Initialize();
                });
            }
        }
  
        #endregion

        #region Public methods
        #endregion
    }
}
