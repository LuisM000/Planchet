using Domain.Interfaces;
using Infrastructure.IServices;
using Model.Entities.Image;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Actions
{
    /// <summary>
    /// Gets and saves a screenshot
    /// </summary>
    public class ScreenshotCommand:ICommand
    {
        IRepository<Screenshot> repoScreenshot;
        IScreenshotService screenshotService;
       
        public ScreenshotCommand() { }
        public ScreenshotCommand(IRepository<Screenshot> repoScreenshot, IScreenshotService screenshotService)
        {
            this.repoScreenshot = repoScreenshot;
            this.screenshotService = screenshotService;
        }
        public override void Execute()
        {
            byte[] screenShot = screenshotService.GetScreenshot();
            repoScreenshot.Insert(new Screenshot() { Image = screenShot });
            repoScreenshot.SaveChanges();
        }
    }
}
