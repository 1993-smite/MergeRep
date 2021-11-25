using DB.DBModels;
using DB.Repositories;
using DB.Repositories.CommentInvoit;
using WebVueTest.Models;
using System;
using WebVueTest.DB.Converters;

namespace WebVueTest.DB.Mappers.CommentInvoitNS
{
    public class CommentInvoitMapper: CommonMapper<CommentInvoit>
    {
        Lazy<CommonRepository<DBUserCommentInvoit, CommentInvoitFilter>> _lazyCommentInvoitRepository
            = new Lazy<CommonRepository<DBUserCommentInvoit, CommentInvoitFilter>>(() => new CommentInvoitRepository());
        CommonRepository<DBUserCommentInvoit, CommentInvoitFilter> CommentInvoitRepository => _lazyCommentInvoitRepository.Value;

        public override int Save(CommentInvoit model)
        {
            var db = CommentInvoitConverter.Convert(model);
            return CommentInvoitRepository.SaveTransaction(db)?.Id ?? 0;
        }
    }
}
