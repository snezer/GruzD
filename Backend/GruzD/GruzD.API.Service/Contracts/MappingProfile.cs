using AutoMapper;
using Microsoft.AspNetCore.Identity;
using GruzD.DataModel;
using GruzD.Web.Contracts.User;
using GruzD.Web.Models;

namespace GruzD.Web.Contracts
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IdentityRole, RoleContract>();

            CreateMap<ApplicationUser, UserShortContract>()
                .ForMember(e => e.Id, e => e.MapFrom(c => c.Id))
                .ForMember(e => e.Email, e => e.MapFrom(c => c.Email))
                .ForMember(e => e.Name, e => e.MapFrom(c => c.FullName))
                .ForMember(e => e.Account, e => e.MapFrom(c => c.UserName));
        }
    }
}
