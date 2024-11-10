using AutoMapper;
using CaseStudy.Entities.Concrete.DataModels;
using CaseStudy.Entities.Concrete.Dtos;

namespace BasicCRUD.Business.Utilities.Mapping.AutoMapper
{
    public class CustomMapping : Profile
    {
        public CustomMapping()
        {
            CreateMap<User, UserLoginDto>().ReverseMap();
        }
    }
}
