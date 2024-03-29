﻿using AutoMapper;
using Internal.Data.Entity;
using Internal.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Internal.Data
{
    public class ViewToEntityProfile: Profile
    {
        public ViewToEntityProfile()
        {
            CreateMap<DemandViewModel, Demand>();
            CreateMap<DemandEditModel, Demand>();

            #region Comment 评论管理
            CreateMap<CommentViewModel, Comment>();
            #endregion
        }
    }
}
