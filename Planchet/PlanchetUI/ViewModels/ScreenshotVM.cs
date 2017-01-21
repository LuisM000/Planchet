using Domain.Interfaces;
using System.Windows.Input;
using Model.Entities.Image;
using PlanchetUI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanchetUI.IServices;
using System.Windows.Media.Imaging;

namespace PlanchetUI.ViewModels
{
    public class ScreenshotVM : MainViewModel
    {
        #region Attributes
        IRepository<Screenshot> screenshotRepository;
        IImageViewer imageViewer;
        #endregion

        #region Properties

        ObservableCollection<Screenshot> screenshots;

        public ObservableCollection<Screenshot> Screenshots
        {
            get
            {
                return new ObservableCollection<Screenshot>(this.screenshotRepository.GetAll().
                    Where(s => s.Time.Date >= this.StartDateTime.Date && s.Time.Date<=this.EndDateTime.Date).
                    OrderBy(s=>s.Time));
            }
            set
            {
                screenshots = value;
                OnPropertyChanged(() => Screenshots);
                OnPropertyChanged(() => CurrentScreenshot);
            }
        }

        private Screenshot currentScreenshot;

        public Screenshot CurrentScreenshot
        {
            get
            {
                if (currentScreenshot == null && this.Screenshots.Count > 0)
                    currentScreenshot = this.Screenshots.FirstOrDefault();
                return currentScreenshot; 
            }
            set { currentScreenshot = value; OnPropertyChanged(() => CurrentScreenshot); }
        }

        private DateTime startDateTime;

        public DateTime StartDateTime
        {
            get { return startDateTime; }
            set { startDateTime = value;
                OnPropertyChanged(() => StartDateTime);
                OnPropertyChanged(() => Screenshots);
            }
        }


        private DateTime endDateTime;

        public DateTime EndDateTime
        {
            get { return endDateTime; }
            set
            {
                endDateTime = value;
                OnPropertyChanged(() => EndDateTime);
                OnPropertyChanged(() => Screenshots);
            }
        }
        #endregion

        #region Builder
        public ScreenshotVM(IRepository<Screenshot> screenshotRepository,IImageViewer imageViewer)
        {
            this.screenshotRepository = screenshotRepository;
            this.imageViewer = imageViewer;
            this.Initialize();
        }
        #endregion

        #region Commands

        public ICommand OpenImageCommand
        {
            get
            {
                return new BaseCommand<BitmapImage>((image) =>
                {
                    this.imageViewer.OpenImage(image);
                });
            }
        }
        #endregion


        #region Public methods

        public override void Initialize()
        {
            this.StartDateTime = DateTime.Now;
            this.EndDateTime = DateTime.Now;
        }
        #endregion
    }
}
