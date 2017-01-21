using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Infrastructure
{
    /// <summary>
    /// Represents a entity of db context
    /// </summary>
    /// </summary>
    public interface IEntity
    {
        [Key]
        int Id { get; set; }
         
    }
}
