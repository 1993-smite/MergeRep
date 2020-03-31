using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebVueTest.DB.Mappers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;

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

    public class appUser : IdentityUser
    {
        public static string sessionKey = "active-user-login";
        public string Login { get; set; }

        public string PasswordEnter { get; set; }
        private string Password { get; set; }

        public string DBPassword { set
            {
                Password = value;
            }
        }
        public appUser()
        {

        }

        public appUser(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public bool CheckPassword(string password) => 
            string.Equals(Password, password, StringComparison.CurrentCultureIgnoreCase);
    }

    public class User: appUser
    {
        public int Id { get; set; }

        [Display(Name = "pLastName")]
        public string LastName { get; set; }

        [Display(Name = "pName")]
        public string FirstName { get; set; }

        [Display(Name = "pMiddleName")]
        public string MiddleName { get; set; }

        [Display(Name = "pEmail")]
        public string Email => $"{LastName?.ToLower() ?? ""}@app.ru";
    }

    public class UserView: User
    {
        public string FullName => $"{LastName} {FirstName} {MiddleName}";

        [Display(Name = "pBirthday")]
        public DateTime Birthday { get; set; }

        [Display(Name = "pDeath")]
        public DateTime Death { get; set; }

        [Display(Name = "pWorkPlace")]
        public string WorkPlace { get; set; }

        [Display(Name = "pWorkPosition")]
        public string WorkPosition { get; set; }

        [Display(Name = "pHomeAddress")]
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


    /// <summary>
    /// альтернативная вью-модель модель валидации 
    /// </summary>
    /// <typeparam name="TUserView"></typeparam>
    public class UserViewValidate<TUserView> where TUserView : UserView
    {
        public TUserView source { get; set; }

        [Display(Name = nameof(UserView.Birthday))]
        public string BirthdayStr => source.Birthday.ToString("dd.MM.yyyy");

        [Display(Name = nameof(UserView.Death))]
        public string DeathStr => source.Death.ToString("dd.MM.yyyy");

        public UserViewValidate(TUserView _source)
        {
            source = _source;
        }
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

        public static User GetUser(int Id) => UserMapper.GetUser(Id);

        public static int SaveUser(User user) => UserMapper.SaveUser(user);

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

        public static RType Convert<T,RType>(T model)
        {
            string output = JsonConvert.SerializeObject(model);
            return JsonConvert.DeserializeObject<RType>(output);
        }

        public static UserViewValidate ConvertToValidate(UserView userView)
        {
            return (UserViewValidate)userView;
        }
    }
}
