using DB.DBModels;
using PostgresApp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DB.Repositories
{
    public static class AddressRepository
    {
        #region Get
        /// <summary>
        /// get addresses
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<DBAddress> Get(string filter = "", int count = 10)
        {
            var addresses = new List<DBAddress>();
            using (ApplicationContext db = new ApplicationContext())
            {
                addresses = db.DBAddress
                    .Where(x=>x.Address.Contains(filter))
                    .Take(count)
                    .ToList();
            }
            return addresses;
        }
        #endregion

        /// <summary>
        /// save address
        /// </summary>
        /// <param name="address"></param>
        public static void Save(DBAddress address)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var exist = db.DBAddress
                            .FirstOrDefault(x => x.Address == address.Address
                                            || (x.Lat == address.Lat && x.Lon == address.Lon));

                        if (exist == null)
                        {
                            db.DBAddress.Add(address);
                        }
                        else
                        {
                            db.DBAddress.Remove(exist);
                            db.DBAddress.Add(address);
                        }

                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }
    }
}
