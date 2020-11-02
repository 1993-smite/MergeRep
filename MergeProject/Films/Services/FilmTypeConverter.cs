using Films.Models;
using PostgresApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Films.Services
{
    public class FilmTypeConverter : Converter<DBFilmType, FilmType>
    {
        public DBFilmType toDB(FilmType from)
        {
            var to = new DBFilmType()
            {
                Id = from.Id,
                Name = from.Name
            };
            return to;
        }

        public FilmType toView(DBFilmType from)
        {
            var to = new FilmType()
            {
                Id = (int)from.Id,
                Name = from.Name,
            };
            return to;
        }
    }
}
