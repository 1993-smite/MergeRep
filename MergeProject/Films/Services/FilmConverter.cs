using Films.Models;
using PostgresApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Films.Services
{
    public class FilmConverter: Converter<DBFilm, Film>
    {
        private List<FilmType> _types = new List<FilmType>();

        public FilmConverter()
        {
        }

        public FilmConverter SetTypes(IEnumerable<FilmType> types = null)
        {
            if (_types != null)
                _types.AddRange(types);
            return this;
        }

        public FilmConverter SetType(FilmType type)
        {
            if (type != null)
                _types.Add(type);
            return this;
        }

        //public IEnumerable<Film> toViews()
        //{

        //}

        public DBFilm toDB(Film from)
        {
            var to = new DBFilm()
            {
                Id = from.Id,
                Name = from.Name,
                TypeId = from.Type?.Id ?? 0,
                Year = from.Year,
                Descriptions = from.Description,
                Country = from.Country,
                Budget = from.Budget,
                Timing = from.Timing,
            };
            return to;
        }

        public Film toView(DBFilm from)
        {
            var to = new Film()
            {
                Id = (int)from.Id,
                Name = from.Name,
                Type = _types?.FirstOrDefault(x=>x.Id == from.TypeId) ?? new FilmType() { Id = from.TypeId },
                Year = from.Year,
                Description = from.Descriptions,
                Country = from.Country,
                Budget = from.Budget.HasValue ? from.Budget.Value : 0,
                Timing = from.Timing.HasValue ? from.Timing.Value : 0,
            };
            return to;
        }
    }
}
