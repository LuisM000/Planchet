using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Input.IServices
{
    /// <summary>
    /// The service in charge of capture the interactions of keyboard.
    /// </summary>
    public interface IKeyboardService
    {
        /// <summary>
        /// Initializes the service and starts capture of keyboard
        /// </summary>
        void Initialize();

        /// <summary>
        /// Returns all interactions since initialize method was called
        /// </summary>
        /// <returns></returns>
        IList<KeyboardInteraction> GetInteractions();
    }
}
