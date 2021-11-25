using PostgresApp;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB.Repositories
{
    /// <summary>
    /// common repository by TdbModel
    /// </summary>
    /// <typeparam name="TdbModel"> db model </typeparam>
    public abstract class CommonRepository<TdbModel, TFilter>
    {
        /// <summary>
        /// get list by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual IEnumerable<TdbModel> GetList(TFilter filter = default)
        {
            throw new NotImplementedException("This method is not implemented!");
        }

        /// <summary>
        /// get item by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual TdbModel Get(TFilter filter = default)
        {
            throw new NotImplementedException("This method is not implemented!");
        }

        /// <summary>
        /// save model with params
        /// </summary>
        /// <typeparam name="TOutParam"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual TOutParam Save<TOutParam>(TdbModel model)
        {
            throw new NotImplementedException("This method is not implemented!");
        }

        public virtual TdbModel Save(ApplicationContext db, TdbModel model)
        {
            throw new NotImplementedException("This method is not implemented!");
        }

        public virtual TdbModel SaveTransaction(TdbModel model)
        {
            TdbModel result;
            using (ApplicationContext db = new ApplicationContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        result = Save(db, model);
                        db.SaveChanges();
                        transaction.Commit();
                        return result;    
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        /// save model void
        /// </summary>
        /// <param name="model"></param>
        public virtual void Save(TdbModel model)
        {
            throw new NotImplementedException("This method is not implemented!");
        }
    }
}
