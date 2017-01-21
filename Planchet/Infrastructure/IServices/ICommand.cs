using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IServices
{
    /// <summary>
    /// Represents base of commands
    /// </summary>
    public class ICommand
    {
        public virtual void Execute(){}
    }
}
