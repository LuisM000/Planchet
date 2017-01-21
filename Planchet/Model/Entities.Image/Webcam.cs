using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities.Image
{
    /// <summary>
    /// Entity that represents a imagen of webcam
    /// </summary>
    public class Webcam : EntityTime
    {
        public byte[] Image { get; set; }
    }
}
