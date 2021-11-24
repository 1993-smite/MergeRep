using DB.DBModels;
using DB.Repositories.User;
using PostgresApp;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using WebVueTest.DB.Converters;
using WebVueTest.Models;

namespace WebVueTest.DB.Mappers
{
    public class UserMapper
    {
        Lazy<UserRepository> _repository = new Lazy<UserRepository>(() => new UserRepository());
        UserRepository Repository => _repository.Value;

        public User GetUser([NotNull] UserFilter filter)
        {
            return UserConverter.Convert(Repository.Get(filter));
        }

        public IEnumerable<User> GetUsers(UserFilter filter = default)
        {
            return UserConverter.Convert(Repository.GetList(filter));
        }

        public int SaveUser(User user)
        {
            var dbUser = UserConverter.Convert(user);
            var saved = Repository.SaveTransaction(dbUser);
            return saved?.Id ?? 0;
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
