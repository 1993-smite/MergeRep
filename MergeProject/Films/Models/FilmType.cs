using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Films.Models
{
    /// <summary>
    /// type of film (comedy, dramma, triller)
    /// </summary>
    public class FilmType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
