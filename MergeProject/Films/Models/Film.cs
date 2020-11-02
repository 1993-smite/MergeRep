using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Films.Models
{
    /// <summary>
    /// film
    /// </summary>
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FilmType Type { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
    }
}
