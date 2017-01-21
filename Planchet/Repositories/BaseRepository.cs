using Databases;
using Databases.Factories;
using Domain.Interfaces;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    /// <summary>
    /// Main repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T>:IRepository<T> where T:Entity
    {
        protected IFactoryDB factoryDB;
        protected DataBase database;
        public virtual String Connection { protected get; set; }

        public BaseRepository(IFactoryDB factoryDB)
        {
            this.factoryDB = factoryDB;
        }

        /// <summary>
        /// Retrieves the item in the repository, given its Id
        /// </summary>
        /// <param name="id">Id of the item that have to be retrieved</param>
        /// <returns>Item in the repository</returns>
        public T GetById(int id)
        {
            var v = factoryDB.CreateDataBase(Connection);
            return v.Set<T>().FirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// Retrieves all items in the repository
        /// </summary>
        /// <returns>List with all items in the repository</returns>
        public IList<T> GetAll()
        {
            var v = factoryDB.CreateNewDataBase(Connection);
            return v.Set<T>().ToList();
        }

        /// <summary>
        /// Deletes a single item in the repository
        /// </summary>
        /// <param name="item">Item that is going to be deleted</param>
        /// <returns>true if the item was successfully deleted. False otherwise</returns>
        public bool Delete(T item)
        {
            factoryDB.CreateDataBase(Connection).Set<T>().Remove(item);
            return true;
        }

        /// <summary>
        /// Inserts an item in the repository
        /// </summary>
        /// <param name="item">item that needs to be inserted</param>
        /// <returns>number of inserted Items (0 or 1)</returns>
        public int Insert(T item)
        {
            factoryDB.CreateDataBase(Connection).Set<T>().Add(item);
            return 1;
        }

        /// <summary>
        /// Updates an item. 
        /// </summary>
        /// <param name="item">Item that needs to be updated</param>
        /// <returns>number of updated items (0 or 1)</returns>
        public int Update(T item)
        {
            factoryDB.CreateDataBase(Connection).Entry(item).State = EntityState.Modified;
            return 1;
        }

        /// <summary>
        /// Makes persistent all the changes made from the last Save or Rollback
        /// </summary>
        /// <returns>number of items afected (-1 means it can´t be count)</returns>
        public int SaveChanges()
        {
            factoryDB.CreateDataBase(Connection).SaveChanges();
            return 1;
        }

    }
}
