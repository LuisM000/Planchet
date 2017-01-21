using Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IFactories
{
    /// <summary>
    /// Commands factory
    /// </summary>
    public interface ICommandFactory
    {
        /// <summary>
        /// Creates ICommand based on actionType
        /// </summary>
        /// <param name="actionType"></param>
        /// <returns></returns>
        ICommand GetCommand(string actionType);
    }
}
