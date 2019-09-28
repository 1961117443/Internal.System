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
            #region Demand 需求录入
            CreateMap<Demand, DemandViewModel>()
                    .ForMember(t => t.CustomerIDCode, m => m.MapFrom(s =>s.Customer.Code))
                    .ForMember(t => t.CustomerIDName, m => m.MapFrom(s =>s.Customer.Name));

            CreateMap<Demand, DemandCardModel>()
                .ForMember(t => t.CustomerName, m => m.MapFrom(s => s.Customer.Name))
                .ForMember(t => t.InputDate, m => m.MapFrom(s => s.InputDate));

            CreateMap<Demand, DemandEditModel>()
                .ForMember(t => t.CustomerName, m => m.MapFrom(s => s.Customer.Name));

            CreateMap<Demand, DemandView>()
                .ForMember(t => t.ClientNameName, m => m.MapFrom(s => s.Customer.Name))
                //.ForMember(t => t.customerName, m => m.MapFrom(s => s.Customer.Name))
                .ReverseMap()
               .ForPath(s => s.ClientFile.ClientName, m => m.MapFrom(t => t.ClientNameName));
            CreateMap<Demand, ViewModelDemand>()
               .ForMember(t => t.ClientFileName, m => m.MapFrom(s => s.Customer.Name))
               .ReverseMap()
               .ForPath(s => s.ClientFile.ClientName, m => m.MapFrom(t => t.ClientFileName));
            CreateMap<Demand, Demand>();
            #endregion

            #region Comment 评论管理
            CreateMap<Comment, CommentViewModel>();
            #endregion

            #region UserInfo
            CreateMap<UserInfo, UserViewModel>(); 
            #endregion
        }
    }
}
