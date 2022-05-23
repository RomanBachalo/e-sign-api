using AutoMapper;
using e_sign_api.Models;

namespace e_sign_api.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Template, DocuSign.eSign.Model.EnvelopeTemplate>().ReverseMap();
            CreateMap<DocuSign.eSign.Model.EnvelopeTemplate, TemplateSummary>()
                .ForMember(dest => dest.TemplateId, opt => opt.MapFrom(src => src.TemplateId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreatedDateTime))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.LastModifiedDateTime))
                .ReverseMap();
            CreateMap<TemplateSummary, DocuSign.eSign.Model.TemplateSummary>().ReverseMap();
            CreateMap<Envelope, DocuSign.eSign.Model.EnvelopeDefinition>().ReverseMap();
        }
    }
}
