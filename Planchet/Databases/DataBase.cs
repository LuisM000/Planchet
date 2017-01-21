using Model.Entities.Image;
using Model.Settings;
using Model.Socket;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases
{
    /// <summary>
    /// Represents a database context
    /// </summary>
    public class DataBase : DbContext
    {
        public DataBase(DbConnection conection)
            : base(conection, true)
        {
            this.Database.CommandTimeout = Int32.MaxValue;
        }

        public virtual DbSet<Screenshot> Screenshots { get; set; }
        public virtual DbSet<Webcam> Webcams { get; set; }
        public virtual DbSet<TransferTime> TransferTimes { get; set; }
        public virtual DbSet<Time> Times { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<Interface> Interfaces { get; set; }
        public virtual DbSet<BusNotification> BusNotifications { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
