using Services.Input.IServices;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyboardAndMouse.Keyboard;
using Model.Entities;
using System.Windows.Forms;

namespace Services.Input.Services
{
    /// <summary>
    /// The service in charge of capture the interactions of keyboard.
    /// The initialize method must be called after the form is initialized 
    /// (in the builder, for example) and before Application.Run(form)
    /// Only is compatibility with Windows.Forms
    /// </summary>
    public class KeyboardService:IKeyboardService
    {
        KeyboardActions keyboard;
        IList<KeyboardInteraction> keyboardInteractions;

        public KeyboardService()
        {
            KeyboardActions.initialize();
        }

        /// <summary>
        /// Initializes the service and starts capture of keyboard
        /// </summary>
        public void Initialize()
        {
            //Initialize service, properties and start capture
            this.keyboard = new KeyboardAndMouse.Keyboard.KeyboardActions();
            this.keyboardInteractions = new List<KeyboardInteraction>();
            this.keyboard.KeyboardChanged += keyboard_KeyboardChanged;
            this.keyboard.start();
        }

        /// <summary>
        /// Returns all interactions since initialize method was called
        /// </summary>
        /// <returns></returns>
        public IList<KeyboardInteraction> GetInteractions()
        {
            return keyboardInteractions;
        }

        /// <summary>
        /// Received keyboard data
        /// </summary>
        /// <param name="value"></param>
        private void keyboard_KeyboardChanged(KeyboardData value)
        {
            var interaction = this.ConvertToKeyboardInteraction(value);
            this.keyboardInteractions.Add(interaction);
        }

        /// <summary>
        /// Converts KeyboardData to KeyboardInteraction
        /// ToDo: use automapper
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private KeyboardInteraction ConvertToKeyboardInteraction(KeyboardData value)
        {
            KeyboardInteraction keyboardInteraction = new KeyboardInteraction();
            if(value!=null)
            {
                keyboardInteraction.Key=value.KeyAction.ToString();
                keyboardInteraction.Type = value.Type.ToString();
            }
            return keyboardInteraction;
        }
         
    }
}
