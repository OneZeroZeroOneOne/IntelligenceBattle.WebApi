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
            CreateMap<GameType, OutGameType>().ForMember(x => x.Translations, x=> x.MapFrom(x => x.GameTypeTranslations));
            CreateMap<Category, OutCategory>().ForMember(x => x.Translations, x => x.MapFrom(x => x.CategoryTranslations));
            CreateMap<CategoryTranslation, OutCategoryTranslation>().ForMember(x => x.LangCode, x => x.MapFrom(x => x.Lang.Code));
            CreateMap<GameTypeTranslation, OutGameTypeTranslation>().ForMember(x => x.LangCode, x => x.MapFrom(x => x.Lang.Code));
        }
    }
}
