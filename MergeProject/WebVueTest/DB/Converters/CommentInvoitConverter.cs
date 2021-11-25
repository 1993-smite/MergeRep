using AutoMapper;
using DB.DBModels;
using WebVueTest.Models;

namespace WebVueTest.DB.Converters
{
    public static class CommentInvoitConverter
    {
        public static MapperConfiguration configToMdl = new MapperConfiguration(cfg => cfg.CreateMap<DBUserCommentInvoit, CommentInvoit>()
                    .ForMember(f => f.Id, opt => opt.MapFrom(src => src.Id))
               );

        public static MapperConfiguration configToDB = new MapperConfiguration(cfg => cfg.CreateMap<CommentInvoit, DBUserCommentInvoit>()
                    .ForMember(f => f.Id, opt => opt.MapFrom(src => src.Id))
               );

        public static CommentInvoit Convert(DBUserCommentInvoit dBComment)
        {
            var mapper = new Mapper(configToMdl);
            var invoit = mapper.Map<DBUserCommentInvoit, CommentInvoit>(dBComment);
            return invoit;
        }

        public static DBUserCommentInvoit Convert(CommentInvoit comment)
        {
            var mapper = new Mapper(configToDB);
            return mapper.Map<CommentInvoit, DBUserCommentInvoit>(comment);
        }

    }
}
