using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DB
{
    public static class QueryBuild
    {
        public static IQueryable<T> GetPage<T>(this IQueryable<T> list,int page, int pageCount)
        {
            int skipCount = (page - 1) * pageCount;
            return list.Skip(skipCount).Take(pageCount);
        }
    }
}
