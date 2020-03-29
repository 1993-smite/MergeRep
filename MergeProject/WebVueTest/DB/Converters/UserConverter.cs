﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using DB;
using System.Threading.Tasks;
using PostgresApp;
using WebVueTest.Models;

namespace WebVueTest.DB.Converters
{
    public static class UserConverter
    {
        public static MapperConfiguration configToMdl = new MapperConfiguration(cfg => cfg.CreateMap<DBUser, User>()
                    .ForMember("Id", opt => opt.MapFrom(src => src.Id))
                    .ForMember("LastName", opt => opt.MapFrom(c => c.LastName))
                    .ForMember("FirstName", opt => opt.MapFrom(c => c.Name))
                    .ForMember("MiddleName", opt => opt.MapFrom(c => c.MiddleName))
                    .ForMember("Email", opt => opt.MapFrom(c => c.Email))
                    );

        public static MapperConfiguration configToDB = new MapperConfiguration(cfg => cfg.CreateMap<User, DBUser>()
                    .ForMember("Id", opt => opt.MapFrom(src => src.Id))
                    .ForMember("LastName", opt => opt.MapFrom(c => c.LastName))
                    .ForMember("Name", opt => opt.MapFrom(c => c.FirstName))
                    .ForMember("MiddleName", opt => opt.MapFrom(c => c.MiddleName))
                    .ForMember("Email", opt => opt.MapFrom(c => c.Email))
                    );

        public static User Convert(DBUser dBUser)
        {
            var mapper = new Mapper(UserConverter.configToMdl);
            return mapper.Map<DBUser, User>(dBUser);
        }

        public static DBUser Convert(User user)
        {
            var mapper = new Mapper(UserConverter.configToDB);
            return mapper.Map<User, DBUser>(user);
        }

    }

    public static class UserCommentConverter
    {
        public static MapperConfiguration configToMdl = new MapperConfiguration(cfg => cfg.CreateMap<DBUserComment, MergeUserComment>()
                    .ForMember("Id", opt => opt.MapFrom(src => src.Id))
                    .ForMember("UserId", opt => opt.MapFrom(c => c.UserId))
                    .ForMember("ParentId", opt => opt.MapFrom(c => c.ParentId))
                    .ForMember("CreateDt", opt => opt.MapFrom(c => c.CreateDT))
                    //.ForMember("UpdateDT", opt => opt.MapFrom(c => c.UpdateDT))
                    .ForMember("Text", opt => opt.MapFrom(c => c.Text))
               );

        public static MapperConfiguration configToDB = new MapperConfiguration(cfg => cfg.CreateMap<MergeUserComment, DBUserComment>()
                    .ForMember(f => f.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(f => f.ParentId, opt => opt.MapFrom(c => c.ParentId))
                    .ForMember(f => f.UserId, opt => opt.MapFrom(c => c.UserId))
                    .ForMember(f => f.CreateDT, opt => opt.MapFrom(c => c.CreateDt))
                    //.ForMember(f => f.UpdateDT, opt => opt.MapFrom(c => c.UpdateDt))
                    .ForMember(f => f.Text, opt => opt.MapFrom(c => c.Text))
               );

        public static MergeUserComment Convert(DBUserComment dBComment)
        {
            var mapper = new Mapper(UserCommentConverter.configToMdl);
            return mapper.Map<DBUserComment, MergeUserComment>(dBComment);
        }

        public static DBUserComment Convert(MergeUserComment comment)
        {
            var mapper = new Mapper(UserCommentConverter.configToDB);
            return mapper.Map<MergeUserComment, DBUserComment>(comment);
        }

    }
}
