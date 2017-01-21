using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    /// <summary>
    /// Represents a entity of db context that contains DateTime
    /// </summary>
    public class EntityTime : Entity
    {
        public EntityTime() { this.Time = DateTime.Now; }

        public virtual DateTime Time { get; set; }
    }
}
