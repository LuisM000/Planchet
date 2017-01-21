using Infrastructure.IServices;
using Ninject;
using Services.IServices;
using SharedComposer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planchet
{
    public static class Program
    {
        static IKernel kernel;
        static MainCapturer mainCapturer;

        public static void Main()
        {
            kernel = new StandardKernel(new Composer());
            mainCapturer = kernel.Get<MainCapturer>();
            mainCapturer.Initialize();
        }
    }
}
