using DB.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebVueTest.DB.Converters;
using WebVueTest.Models;

namespace WebVueTest.DB.Mappers
{
    public static class UserMapper
    {
        public static User GetUser(int Id)
        {
            return UserConverter.Convert(UserRepository.GetUser(Id));
        }

        public static int SaveUser(User user)
        {
            return UserRepository.SaveUser(UserConverter.Convert(user));
        }
    }
}
