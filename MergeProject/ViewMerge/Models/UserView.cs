using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViewMerge.Models
{
    public class UserView
    {
        public int Id { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Display(Name = "День рождения")]
        public DateTime Birthday { get; set; }

        [Display(Name = "День смерти")]
        public DateTime Death { get; set; }

        [Display(Name = "Место работы")]
        public string WorkPlace { get; set; }

        [Display(Name = "Должность")]
        public string WorkPosition { get; set; }

        [Display(Name = "Адресс")]
        public string HomeAddress { get; set; }
    }

    public class UserViewValidate: UserView
    {
        [Display(Name = nameof(UserView.Birthday))]
        public string BirthdayStr => Birthday.ToString("dd.MM.yyyy");

        [Display(Name = nameof(UserView.Death))]
        public string DeathStr => Death.ToString("dd.MM.yyyy");
    }

    public static class FactoryUserView
    {
        public static UserViewValidate Create(int i)
        {
            return new UserViewValidate()
            {
                Id = i,
                LastName = $"lastName {i}",
                FirstName = $"firstName {i}",
                MiddleName = $"middleName {i}",
                Birthday = DateTime.Today.AddDays(-i * 30),
                Death = DateTime.Today.AddDays(i * 30),
                WorkPosition = $"workPosition {i}",
                WorkPlace = $"workPlace {i}",
                HomeAddress = $"homeAddress {i}"
            };
        }

        public static UserViewValidate ConvertToValidate(UserView userView)
        {
            return (UserViewValidate)userView;
        }
    }
}
