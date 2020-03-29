using AutoMapper;
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
}
