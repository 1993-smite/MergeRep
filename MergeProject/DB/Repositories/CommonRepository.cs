using System;
using System.Collections.Generic;
using System.Text;

namespace DB.Repositories
{
    public abstract class BaseFilter
    {

    }

    /// <summary>
    /// common repository by TdbModel
    /// </summary>
    /// <typeparam name="TdbModel"> db model </typeparam>
    public abstract class CommonRepository<TdbModel>
    {
        /// <summary>
        /// get list by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<TdbModel> GetList(BaseFilter filter = default(BaseFilter))
        {
            throw new NotImplementedException("This method is not implemented!");
        }

        /// <summary>
        /// get item by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<TdbModel> Get(BaseFilter filter = default(BaseFilter))
        {
            throw new NotImplementedException("This method is not implemented!");
        }

        /// <summary>
        /// save model with params
        /// </summary>
        /// <typeparam name="TOutParam"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public TOutParam Save<TOutParam>(TdbModel model)
        {
            throw new NotImplementedException("This method is not implemented!");
        }

        /// <summary>
        /// save model void
        /// </summary>
        /// <param name="model"></param>
        public void Save(TdbModel model)
        {
            throw new NotImplementedException("This method is not implemented!");
        }
    }
}
