using DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebVueTest.DB.Mappers
{
    public abstract class CommonMapper<TModel>
    {
        public virtual IEnumerable<TModel> GetList(Filter filter)
        {
            throw new NotImplementedException("This method is not implemented!");
        }

        public virtual TModel Get(Filter filter)
        {
            throw new NotImplementedException("This method is not implemented!");
        }

        public virtual int Save(TModel model)
        {
            throw new NotImplementedException("This method is not implemented!");
        }
    }
}
