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
    public static class ContactConverter
    {
        public static MapperConfiguration configToMdl = new MapperConfiguration(cfg => cfg.CreateMap<DBContact, Contact>()
                    .ForMember(tgt=>tgt.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(tgt => tgt.Name, opt => opt.MapFrom(c => c.Name))
                    .ForMember(tgt => tgt.Phone, opt => opt.MapFrom(c => c.Phone))
                    .ForMember(tgt => tgt.Status, opt => opt.MapFrom(c => c.Status))
                    );

        public static MapperConfiguration configToDB = new MapperConfiguration(cfg => cfg.CreateMap<Contact, DBContact>()
                    .ForMember(tgt => tgt.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(tgt => tgt.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(tgt => tgt.Phone, opt => opt.MapFrom(src => src.Phone))
                    .ForMember(tgt => tgt.Status, opt => opt.MapFrom(c => c.Status))
                    );

        public static Contact Convert(DBContact dBContact)
        {
            var mapper = new Mapper(ContactConverter.configToMdl);
            return mapper.Map<DBContact, Contact>(dBContact);
        }

        public static DBContact Convert(Contact contact)
        {
            var mapper = new Mapper(ContactConverter.configToDB);
            return mapper.Map<Contact, DBContact>(contact);
        }

    }
}
