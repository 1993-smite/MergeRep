using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string Country { get; set; }
        public long Timing { get; set; }
        public long Budget { get; set; }
    }

    public class FilmValid: Film
    {
        public bool isNew => Id < 1;

        [Required(ErrorMessage = "'Название' должно быть заполнено")]
        public string NameValid => Name;

        [RegularExpression("(True|true)", ErrorMessage = "'Год' должен быть в дипазоне от 1800 до 2030")]
        public bool YearValid => (Year > 1800 && Year < 2030);

        [RegularExpression("(True|true)", ErrorMessage = "'Тип' должен быть выбрано")]
        public bool TypeValid => !(Type == null || Type.Id < 1);
    }
}
