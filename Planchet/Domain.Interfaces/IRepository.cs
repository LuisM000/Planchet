using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Interfaces
{
    /// <summary>
    /// Represents the base for interaction with DAOs
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> 
    {
        /// <summary>
        /// Retrieves the item in the repository, given its Id
        /// </summary>
        /// <param name="id">Id of the item that have to be retrieved</param>
        /// <returns>Item in the repository</returns>
        T GetById(int id);

        /// <summary>
        /// Retrieves all items in the repository
        /// </summary>
        /// <returns>List with all items in the repository</returns>
        IList<T> GetAll();

        /// <summary>
        /// Deletes a single item in the repository
        /// </summary>
        /// <param name="item">Item that is going to be deleted</param>
        /// <returns>true if the item was successfully deleted. False otherwise</returns>
        bool Delete(T item);

        /// <summary>
        /// Inserts an item in the repository
        /// </summary>
        /// <param name="item">item that needs to be inserted</param>
        /// <returns>number of inserted Items (0 or 1)</returns>
        int Insert(T item);

        /// <summary>
        /// Updates an item. 
        /// </summary>
        /// <param name="item">Item that needs to be updated</param>
        /// <returns>number of updated items (0 or 1)</returns>
        int Update(T item);

        /// <summary>
        /// Makes persistent all the changes made from the last Save or Rollback
        /// </summary>
        /// <returns>number of items afected (-1 means it can´t be count)</returns>
        int SaveChanges();

        String Connection { set; }


    }

}
