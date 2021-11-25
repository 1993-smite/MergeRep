using DB.DBModels;
using DB.Repositories;
using DB.Repositories.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebVueTest.DB.Converters;
using WebVueTest.Models;

namespace WebVueTest.DB.Mappers.Comment
{
    public class CommentMapper: CommonMapper<MergeUserComment>
    {
        Lazy<CommonRepository<DBUserComment, CommentFilter>> _lazyCommentRepository 
            = new Lazy<CommonRepository<DBUserComment, CommentFilter>>(() => new CommentRepository());
        CommonRepository<DBUserComment, CommentFilter> CommentRepository => _lazyCommentRepository.Value;

        public override IEnumerable<MergeUserComment> GetList(Filter filter)
        {
            var db = CommentRepository.GetList(filter as CommentFilter);
            return UserCommentConverter.Convert(db);
        }

        public override MergeUserComment Get(Filter filter)
        {
            var db = CommentRepository.Get(filter as CommentFilter);
            return UserCommentConverter.Convert(db);
        }

        public override int Save(MergeUserComment comment)
        {
            var db = UserCommentConverter.Convert(comment);

            var saved = CommentRepository.SaveTransaction(db);

            return saved.Id;
        }
    }
}
