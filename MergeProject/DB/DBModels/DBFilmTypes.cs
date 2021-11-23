using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace DB.DBModels
{
    [Table("film-types")]
    public class DBFilmType
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
    }

    public enum FilmTypes
    {
        [Display(Name = "Триллер")]
        Thriller = 1,
        [Display(Name = "Комедия")]
        Comedy,
        [Display(Name = "Ужас")]
        Horror,
        [Display(Name = "Драмма")]
        Drama,
        [Display(Name = "Детектив")]
        Detective,
        [Display(Name = "Трагедия")]
        Tragedy,
        [Display(Name = "Исторический фильм")]
        HistoricalFilm,
        [Display(Name = "Сказка")]
        FairyTale,
        [Display(Name = "Приключение")]
        Adventure
    }

    public static class EnumExtension
    {
        public static string GetDisplay(this FilmTypes type)
        {
            var tp = type.GetType();
            var member = tp.GetMember(type.ToString());
            var attr = member.FirstOrDefault() ?? null;

            if (attr == null)
                return string.Empty;

            var displayAttr = attr.GetCustomAttribute<DisplayAttribute>();
            return displayAttr?.Name ?? string.Empty;
        }
    }
}
