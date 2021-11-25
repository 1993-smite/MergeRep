using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using DB;
using System.Threading.Tasks;
using PostgresApp;
using WebVueTest.Models;
using DB.DBModels;

namespace WebVueTest.DB.Converters
{
    public static class UserConverter
    {
        public static MapperConfiguration configToMdl = new MapperConfiguration(cfg => cfg.CreateMap<DBUser, User>()
                    .ForMember(tgt=>tgt.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(tgt => tgt.LastName, opt => opt.MapFrom(c => c.LastName))
                    .ForMember(tgt => tgt.FirstName, opt => opt.MapFrom(c => c.Name))
                    .ForMember(tgt => tgt.MiddleName, opt => opt.MapFrom(c => c.MiddleName))
                    .ForMember(tgt => tgt.Email, opt => opt.MapFrom(c => c.Email))
                    .ForMember(tgt => tgt.City, opt => opt.MapFrom(c => (c.City == null ? "" : c.City.Name )))
                    );

        public static MapperConfiguration configToDB = new MapperConfiguration(cfg => cfg.CreateMap<User, DBUser>()
                    .ForMember(tgt => tgt.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(tgt => tgt.LastName, opt => opt.MapFrom(c => c.LastName))
                    .ForMember(tgt => tgt.Name, opt => opt.MapFrom(c => c.FirstName))
                    .ForMember(tgt => tgt.MiddleName, opt => opt.MapFrom(c => c.MiddleName))
                    .ForMember(tgt => tgt.Email, opt => opt.MapFrom(c => c.Email ?? ""))
                    .ForMember(tgt => tgt.CityId , opt => opt.MapFrom(c => c.CityId))
                    .ForMember(tgt => tgt.City, opt => opt.MapFrom(c => new DBCity() { Id = c.CityId }))
                    );

        public static User Convert(DBUser dBUser)
        {
            if (dBUser == null)
                return new User() { Id = 0 };
            var mapper = new Mapper(UserConverter.configToMdl);
            var user = mapper.Map<DBUser, User>(dBUser);
            var userLogin = dBUser.Logins.FirstOrDefault();
            if (userLogin != null)
            {
                user.Login = userLogin.Login;
                user.DBPassword = userLogin.Password;
            }
            return user;
        }

        public static IEnumerable<User> Convert(IEnumerable<DBUser> list)
        {
            var users = new List<User>();
            foreach(var dbMdl in list)
            {
                users.Add(Convert(dbMdl));
            }

            return users;
        }

        public static DBUser Convert(User user)
        {
            var mapper = new Mapper(UserConverter.configToDB);
            return mapper.Map<User, DBUser>(user);
        }

        public static IEnumerable<DBUser> Convert(IEnumerable<User> list)
        {
            var users = new List<DBUser>();
            foreach (var dbMdl in list)
            {
                users.Add(Convert(dbMdl));
            }

            return users;
        }

    }
}
