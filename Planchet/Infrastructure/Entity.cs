using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    /// <summary>
    /// Represents a entity of db context
    /// </summary>
    public class Entity:IEntity
    {
        public int Id { get; set; }
    }
}
