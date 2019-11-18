using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewMerge.Models
{
    public class UserView
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime Birthday { get; set; }
        public string BirthdayStr {
            get
            {
                return Birthday.ToString("dd.MM.yyyy");
            }
        }
        public string WorkPlace { get; set; }
        public string WorkPosition { get; set; }
        public string HomeAddress { get; set; }
    }

    public static class FactoryUserView
    {
        public static UserView Create(int i)
        {
            return new UserView()
            {
                Id = i,
                LastName = $"lastName {i}",
                FirstName = $"firstName {i}",
                MiddleName = $"middleName {i}",
                Birthday = DateTime.Today.AddDays(-i * 30),
                WorkPosition = $"workPosition {i}",
                WorkPlace = $"workPlace {i}",
                HomeAddress = $"homeAddress {i}"
            };
        }
    }
}
