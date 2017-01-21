using Infrastructure.IFactories;
using Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Factories
{
    /// <summary>
    /// Commands factory
    /// </summary>
    public class CommandFactory : ICommandFactory
    {
        ICommandFactory commandFactory;
        public CommandFactory(ICommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }
        /// <summary>
        /// Creates ICommand based on actionType
        /// </summary>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public ICommand GetCommand(string actionType)
        {
            ICommand command = commandFactory.GetCommand(actionType);
            return command;
        }
    }
}
