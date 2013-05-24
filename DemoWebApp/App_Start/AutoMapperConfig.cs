using System;
using Abstracts.PersistenceDtos;
using AutoMapper;
using DemoWebApp.Controllers;

namespace DemoWebApp
{
    public static class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.CreateMap<Address, AddressDisplayModel>();

            Mapper.CreateMap<Customer, CustomerDisplayModel>()
                .ForMember(t => t.Id, opt => opt.MapFrom(c => MapIdFromRaven(c.Id)))
                ;
            Mapper.CreateMap<Customer, CustomerUpdateModel>()
                .ForMember(t => t.Id, opt => opt.MapFrom(c => MapIdFromRaven(c.Id)))
                ;


            Mapper.AssertConfigurationIsValid();
        }

        private static int? MapIdFromRaven(string source)
        {
            if (source == null)
                return null;

            return Int32.Parse(source.Split('/')[1]);
        }
    }
}