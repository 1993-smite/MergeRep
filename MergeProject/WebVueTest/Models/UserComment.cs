using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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

        public Comment()
        {

        }
    }

    public class UserComment: Comment
    {
        public User CreatedUser { get; set; }

        public UserComment()
        {

        }
    }

    public class MergeUserComment : UserComment
    {
        public static MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<UserComment, MergeUserComment>()
                    .ForMember("CreatedUser", opt => opt.MapFrom(c => c.CreatedUser))
                    .ForMember("ParentId", opt => opt.MapFrom(c => c.ParentId))
                    .ForMember("Text", opt => opt.MapFrom(c => c.Text))
                    .ForMember("CreateDt", opt => opt.MapFrom(c => c.CreateDt))
                    .ForMember("Id", opt => opt.MapFrom(src => src.Id)));

        public int CardId { get; set; }

        public MergeUserComment()
        {

        }

        public MergeUserComment(int cardId,UserComment comment): base()
        {
            this.Id = comment.Id;
            this.ParentId = comment.ParentId;
            this.Text = comment.Text;
            this.CreateDt = comment.CreateDt;
            this.CreatedUser = comment.CreatedUser;
            CardId = cardId;
        }
    }

    public static class UserCommentFactory
    {
        private static Random rand = new Random();

        public static UserComment GetUserComment(User user, int index)
        {
            int id = index;
            return new UserComment()
            {
                Id = id,
                CreatedUser = user,
                CreateDt = DateTime.Now.AddDays(rand.Next(-100, -50)),
                UpdateDt = DateTime.Now.AddDays(rand.Next(-40, -20)),
                Text = $"Comment {id}"
            };
        }

        public static MergeUserComment ConvertComment(UserComment comment, int Id)
        {
            var mapper = new Mapper(MergeUserComment.config);
            var mergeComment = mapper.Map<UserComment, MergeUserComment>(comment);
            mergeComment.CardId = Id;
            return mergeComment;
        }

        public static IEnumerable<UserComment> CreateCommnets(List<User> users, int count)
        {
            List<UserComment> comments = new List<UserComment>();
            int userCount = users.Count;
            for(int i = 1; i <= count; i++)
            {
                var user = users[rand.Next(0, userCount)];
                comments.Add(GetUserComment(user, i));
            }
            int minId = comments.Min(x=>x.Id);
            int maxId = comments.Max(x => x.Id);
            foreach (var comment in comments)
            {
                comment.ParentId = rand.Next(minId,maxId);
                comment.ParentId = comment.ParentId >= comment.Id ? 0 : comment.ParentId;
            }
            return comments;
        }
    }
}
