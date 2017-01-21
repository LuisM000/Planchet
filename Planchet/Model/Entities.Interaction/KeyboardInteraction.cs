using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class KeyboardInteraction : EntityTime
    {
        public string Key { get; set; }
        public string Type { get; set; }
    }
}
