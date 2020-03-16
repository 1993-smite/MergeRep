using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebVueTest.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public DateTime CreateDt { get; set; }
        public DateTime UpdateDt { get; set; }
        public string Text { get; set; }
    }

    public class UserComment: Comment
    {
        public User CreatedUser { get; set; }
    }

    public static class UserCommentFactory
    {
        private static int index = 0;
        private static Random rand = new Random();

        private static int getNextId()
        {
            return ++index;
        }

        public static UserComment GetUserComment(User user)
        {
            int id = getNextId();
            return new UserComment()
            {
                Id = id,
                CreatedUser = user,
                CreateDt = DateTime.Now.AddDays(rand.Next(-100, -50)),
                UpdateDt = DateTime.Now.AddDays(rand.Next(-40, -20)),
                Text = $"Comment {id}"
            };
        }

        public static IEnumerable<UserComment> CreateCommnets(List<User> users, int count)
        {
            List<UserComment> comments = new List<UserComment>();
            int userCount = users.Count;
            for(int i = 0; i < count; i++)
            {
                var user = users[rand.Next(0, userCount)];
                comments.Add(GetUserComment(user));
            }
            int minId = comments.Min(x=>x.Id);
            int maxId = comments.Min(x => x.Id);
            foreach (var comment in comments)
            {
                comment.ParentId = rand.Next(0, 3) < 2 ? 0 : rand.Next(minId,maxId);
                comment.ParentId = comment.ParentId == comment.Id ? 0 : comment.ParentId;
            }
            return comments;
        }
    }
}
