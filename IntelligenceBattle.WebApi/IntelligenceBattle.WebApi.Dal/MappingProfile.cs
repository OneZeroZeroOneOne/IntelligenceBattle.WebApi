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
            CreateMap<User, OutUser>().ForMember(x => x.LangCode, x => x.MapFrom(x => x.Lang.Code));
            CreateMap<GameType, OutGameType>()
                .ForMember(x => x.Translations, x=> x.MapFrom(x => x.GameTypeTranslations));
            CreateMap<Category, OutCategory>()
                .ForMember(x => x.Translations, x => x.MapFrom(x => x.CategoryTranslations));
            CreateMap<CategoryTranslation, OutCategoryTranslation>()
                .ForMember(x => x.LangCode, x => x.MapFrom(x => x.Lang.Code));
            CreateMap<GameTypeTranslation, OutGameTypeTranslation>()
                .ForMember(x => x.LangCode, x => x.MapFrom(x => x.Lang.Code));
            CreateMap<UserAnswer, OutUserAnswer>();

            CreateMap<Answer, OutAnswer>()
                .ForMember(x => x.Translations, x => x.MapFrom(x => x.AnswerTranslations));
            CreateMap<AnswerTranslation, OutAnswerTranslation>()
                .ForMember(x => x.LangCode, x => x.MapFrom(x => x.Lang.Code));


            CreateMap<Question, OutQuestion>()
                .ForMember(x => x.Translations, x => x.MapFrom(x => x.QuestionTranslations));
            CreateMap<QuestionTranslation, OutQuestionTranslation>()
                .ForMember(x => x.LangCode, x => x.MapFrom(x => x.Lang.Code));


            CreateMap<SendQuestion, OutSendQuestion>();

            CreateMap<UserAnswer, OutUserAnswer>().ForMember(x => x.IsTrue, x => x.MapFrom(x => x.Answer.IsTrue));
            CreateMap<Notification, OutNotification>().ForMember(x => x.TypeTittle, x => x.MapFrom(x => x.Type.Tittle));
            CreateMap<Lang, OutLang>();
        }
    }
}
