using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.Core.Resource;
using Library_management.Models;
using Library_management.ViewModel;

namespace Library_management.Middleware
{
    public class MapperConfig : Profile
    {
        public static Mapper InitializeAutomapper()
        {
            //Provide all the Mapping Configuration
            var config = new MapperConfiguration(cfg =>
            {
                //Configuring Customers and CustomerDTO
                cfg.CreateMap<Users, UsersDTO>().ReverseMap();
                cfg.CreateMap<Books, BooksDTO>().ReverseMap();
                cfg.CreateMap<Category, CategoryDTO>().ReverseMap();

                //  cfg.CreateMap<CustomerDTO, Customers>();
                //Any Other Mapping Configuration ....
            });
            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}