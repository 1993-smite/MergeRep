using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class User
    {
        public int Id { private set; get; }
        public string LastName { private set; get; }
        public string Name { private set; get; }
        public string MiddleName { private set; get; }

        public User(int id)
        {
            Id = id;
            LastName = $"Last Name {Id}";
            Name = $"Name {Id}";
            MiddleName = $"Middle Name {Id}";
        }

        /// <summary>
        /// ФИО для юзера
        /// </summary>
        public string FIO
        {
            get
            {
                return $"{LastName} {Name} {MiddleName}";
            }
        }
    }
}