using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Films.Services
{
    interface Converter<TDB,TView>
    {
        /// <summary>
        /// convert view model to db model
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        TDB toDB(TView from);
        /// <summary>
        /// convert db model to view model
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        TView toView(TDB from);
    }
}
