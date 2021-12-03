using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using GruzD.DataModel;
using GruzD.DataModel.BL;
using GruzD.DataModel.Events;
using GruzD.Web.Contracts.Provider;
using GruzD.Web.Contracts.Supply;
using GruzD.Web.Contracts.User;
using GruzD.Web.Contracts.Zone;
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

            CreateMap<ZoneModel, UnloadingZone>()
                .ForMember(e => e.Sources, c => c.Ignore());

            CreateMap<UnloadingZone, ZoneModel>()
                .ForSourceMember(e => e.Sources, o => o.DoNotValidate());

            CreateMap<ProcessEventModel, ProcessEvent>()
                .ForMember(t => t.Sources, e => e.Ignore())
                .ForMember(t => t.ProcessTime, e => e.Ignore())
                .ForMember(t => t.Processed, e => e.Ignore())
                .ForMember(t => t.Registered, e => e.MapFrom(c => DateTime.Now));

            CreateMap<ProcessEvent, ProcessEventModel>();

            CreateMap<DataModel.BL.Provider, ProviderModel>()
                .ForSourceMember(s => s.Supplies, o => o.DoNotValidate());

            CreateMap<ProviderModel, DataModel.BL.Provider>()
                .ForMember(d => d.Supplies, o => o.Ignore());

            CreateMap<DataModel.BL.Supply, SupplyModel>()
                .ForMember(m => m.ProviderName, c => c.MapFrom(e => e.Provider.Name))
                .ForMember(m => m.RawMaterialName, c => c.MapFrom(e => e.RawMaterialType.Name));

            CreateMap<SupplyModel, DataModel.BL.Supply>()
                .ForSourceMember(s => s.ProviderName, e => e.DoNotValidate())
                .ForSourceMember(s => s.RawMaterialName, e => e.DoNotValidate());
        }
    }
}
