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
using System.Windows.Media.Imaging;

namespace PlanchetUI.ViewModels
{
    public class WebcamVM: MainViewModel
    {
        #region Attributes
        IRepository<Webcam> webcamRepository;
        IImageViewer imageViewer;
        #endregion

        #region Properties

        ObservableCollection<Webcam> webcams;

        public ObservableCollection<Webcam> Webcams
        {
            get
            {
                return new ObservableCollection<Webcam>(this.webcamRepository.GetAll().
                    Where(s => s.Time.Date >= this.StartDateTime.Date && s.Time.Date<=this.EndDateTime.Date).
                    OrderBy(s=>s.Time));
            }
            set
            {
                webcams = value;
                OnPropertyChanged(() => Webcams);
                OnPropertyChanged(() => CurrentWebcam);
            }
        }

        private Webcam currentWebcam;

        public Webcam CurrentWebcam
        {
            get
            {
                if (currentWebcam == null && this.Webcams.Count > 0)
                    currentWebcam = this.Webcams.FirstOrDefault();
                return currentWebcam; 
            }
            set { currentWebcam = value; OnPropertyChanged(() => CurrentWebcam); }
        }

        private DateTime startDateTime;

        public DateTime StartDateTime
        {
            get { return startDateTime; }
            set { startDateTime = value;
                OnPropertyChanged(() => StartDateTime);
                OnPropertyChanged(() => Webcams);
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
                OnPropertyChanged(() => Webcams);
            }
        }
        #endregion

        #region Builder
        public WebcamVM(IRepository<Webcam> webcamRepository, IImageViewer imageViewer)
        {
            this.webcamRepository = webcamRepository;
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
