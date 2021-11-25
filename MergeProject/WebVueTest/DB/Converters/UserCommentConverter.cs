using AutoMapper;
using DB.DBModels;
using System.Collections.Generic;
using WebVueTest.Models;

namespace WebVueTest.DB.Converters
{
    public static class UserCommentConverter
    {
        public static MapperConfiguration configToMdl = new MapperConfiguration(cfg => cfg.CreateMap<DBUserComment, MergeUserComment>()
                    .ForMember(f => f.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(f => f.UserId, opt => opt.MapFrom(c => c.UserId))
                    .ForMember(f => f.CardId, opt => opt.MapFrom(c => c.UserId))
                    .ForMember(f => f.ParentId, opt => opt.MapFrom(c => c.ParentId))
                    .ForMember(f => f.CreateDt, opt => opt.MapFrom(c => c.CreateDT))
                    .ForMember(f => f.UpdateDt, opt => opt.MapFrom(c => c.UpdateDT))
                    .ForMember(f => f.Text, opt => opt.MapFrom(c => c.Text))
                    .ForMember(f => f.Invoit, opt => opt.MapFrom(c => c.InvoitCount))
               );

        public static MapperConfiguration configToDB = new MapperConfiguration(cfg => cfg.CreateMap<MergeUserComment, DBUserComment>()
                    .ForMember(f => f.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(f => f.ParentId, opt => opt.MapFrom(c => c.ParentId))
                    .ForMember(f => f.UserId, opt => opt.MapFrom(c => c.UserId))
                    .ForMember(f => f.InvoitCount, opt => opt.MapFrom(c => c.Invoit))
                    .ForMember(f => f.CreateDT, opt => opt.MapFrom(c => c.CreateDt))
                    .ForMember(f => f.UpdateDT, opt => opt.MapFrom(c => c.UpdateDt))
                    .ForMember(f => f.Text, opt => opt.MapFrom(c => c.Text))
               );

        public static MergeUserComment Convert(DBUserComment dBComment)
        {
            var mapper = new Mapper(UserCommentConverter.configToMdl);
            var comment = mapper.Map<DBUserComment, MergeUserComment>(dBComment);
            comment.CreatedUser = UserConverter.Convert(dBComment.CreateUser);
            comment.IsSelfUserComment = dBComment.Invoits?.Exists(x => x.UserId == comment.CreateUserId) ?? false;
            //comment.CreatedUser = UserConverter.Convert(dBComment.User);
            return comment;
        }

        public static IEnumerable<MergeUserComment> Convert(IEnumerable<DBUserComment> comments)
        {
            var res = new List<MergeUserComment>();
            foreach (var comment in comments)
                res.Add(Convert(comment));

            return res;
        }

        public static DBUserComment Convert(MergeUserComment comment)
        {
            var mapper = new Mapper(UserCommentConverter.configToDB);
            return mapper.Map<MergeUserComment, DBUserComment>(comment);
        }

        public static IEnumerable<DBUserComment> Convert(IEnumerable<MergeUserComment> comments)
        {
            var res = new List<DBUserComment>();
            foreach (var comment in comments)
                res.Add(Convert(comment));

            return res;
        }

    }
}
