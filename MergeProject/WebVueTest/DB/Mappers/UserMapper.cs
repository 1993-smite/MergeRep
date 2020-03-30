using DB.Users;
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

        public static int SaveUserComment(MergeUserComment comment)
        {
            return UserRepository.SaveUserComment(UserCommentConverter.Convert(comment));
        }
    }
}
