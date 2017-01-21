using Infrastructure;
using Infrastructure.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Settings
{
    /// <summary>
    /// Describes the credentials of program
    /// </summary>
    public class Credential:Entity
    {
        public string User { get; set; }
        public string Hash { get; set; }

        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public bool IsChecked { get; set; }
    }
}
