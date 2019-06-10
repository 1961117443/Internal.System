using AutoMapper;
using Internal.Data.Entity;
using Internal.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Data
{
    public class EntityToViewProfile : Profile
    {
        public EntityToViewProfile()
        {
            CreateMap<Demand, DemandViewModel>()
                .ForMember(t => t.CustomerIDCode, m => m.MapFrom(s => s.Customer.Code));
        }
    }
}
