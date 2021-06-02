using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using IntelligenceBattle.WebApi.Dal.Models;
using IntelligenceBattle.WebApi.Dal.Models.Out;

namespace IntelligenceBattle.WebApi.Dal
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SearchGame, OutSearchGame>();
            CreateMap<User, UserResponce>();
            CreateMap<GameType, OutGameTypeResponce>();
            CreateMap<Category, OutCategoryResponce>();
        }
    }
}
