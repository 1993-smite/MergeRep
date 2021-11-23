using DB.DBModels;
using DB.Repositories;
using PostgresApp;
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

        public static User GetUser(string login)
        {
            return UserConverter.Convert(UserRepository.GetUser(login));
        }

        public static List<User> GetUsers()
        {
            var dbUsers = UserRepository.GetUsers();
            var users = new List<User>();
            foreach(var dbuser in dbUsers)
            {
                users.Add(UserConverter.Convert(dbuser));
            }
            return users;
        }

        public static int SaveUser(User user)
        {
            return UserRepository.SaveUser(UserConverter.Convert(user));
        }

        public static List<MergeUserComment> GetUserComments(int userId)
        {
            List<DBUserComment> dbComments = UserRepository.GetUserComments(userId);
            List<MergeUserComment> mergeUserComments = new List<MergeUserComment>(); 
            for(int index = 0;index < dbComments.Count;index++)
            {
                var comment = UserCommentConverter.Convert(dbComments[index]);
                mergeUserComments.Add(comment);
            }
            return mergeUserComments;
        }

        public static MergeUserComment GetUserComment(int id)
        {
            return UserCommentConverter.Convert(UserRepository.GetUserComment(id));
        }

        public static int SaveUserComment(MergeUserComment comment)
        {
            return UserRepository.SaveUserComment(UserCommentConverter.Convert(comment));
        }
    }
}
