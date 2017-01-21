using Infrastructure.IFactories;
using Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Services.External.WindowsForms
{
    public partial class MessageForm : Form
    {
        ICommandFactory commandFactory;
        Azure.Entities.Message message;

        public MessageForm(ICommandFactory commandFactory, Azure.Entities.Message message)
        {
            this.commandFactory = commandFactory;
            this.message = message;
            InitializeComponent();
            this.Text = message.Caption;
            this.txtText.Text = message.Text;
        }

        /// <summary>
        /// Accept button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.ExecuteAction(Azure.Entities.Action.ActivationType.Accept);
        }

        /// <summary>
        /// Cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Close form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ExecuteAction(Azure.Entities.Action.ActivationType.Cancel);
        }

        /// <summary>
        /// Executes all actions of message based on ActivationType
        /// </summary>
        /// <param name="activationType"></param>
        private void ExecuteAction(Azure.Entities.Action.ActivationType activationType)
        {
            if (message.Actions != null)
                foreach (var action in this.message.Actions.Where(a => a.Activation.Equals(activationType)))
                {
                    ICommand command = this.commandFactory.GetCommand(action.ActionType);
                    command.Execute();
                }
        }

     

    }
}
