using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Models;
using webapi.ViewModel;

namespace webapi.Utility.Profile
{
    public class ApplicationProfile : AutoMapper.Profile
    {
        public ApplicationProfile()
        {
            CreateMap<User, LoginVM>();
            CreateMap<LoginVM, User>();
            CreateMap<User, RegisterVM>();
            CreateMap<RegisterVM, User>();
            CreateMap<ReviewVM, Review>();
            CreateMap<Review, ReviewVM>();
        }
    }
}
