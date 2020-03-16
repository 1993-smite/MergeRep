using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebVueTest.Models
{
    public class UserFile
    {
        public int UserId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string FullFileName { get; set; }

        public UserFile()
        {

        }
    }

    public class User
    {
        public int Id { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        public string FullName => $"{LastName} {FirstName} {MiddleName}";

        public string Login => ($"{FirstName?.ToLower().FirstOrDefault() ?? ' '}{MiddleName?.ToLower().FirstOrDefault() ?? ' '}{LastName?.ToLower().Replace(" ","") ?? ""}").Trim();

        [Display(Name = "Почта")]
        public string Email => $"{LastName?.ToLower() ?? ""}@app.ru";
    }

    public class UserView: User
    {
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

        public List<UserFile> Files { get; set; }

        public UserView()
        {

        }
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
                HomeAddress = $"homeAddress {i}",
                Files = new List<UserFile>()
            };
        }

        public static User CreateUser(int i)
        {
            return new User()
            {
                Id = i,
                LastName = $"lastName {i}",
                FirstName = $"firstName {i}",
                MiddleName = $"middleName {i}"
            };
        }

        public static List<User> CreateUsers(int count)
        {
            List<User> users = new List<User>();
            for(int i = 1; i <= count; i++)
            {
                users.Add(CreateUser(i));
            }
            return users;
        }

        public static int SaveUserView(UserView userView)
        {
            if (userView.Id < 3)
                userView.Id = 10001;
            return userView.Id;
        }

        public static UserViewValidate ConvertToValidate(UserView userView)
        {
            return (UserViewValidate)userView;
        }
    }
}
