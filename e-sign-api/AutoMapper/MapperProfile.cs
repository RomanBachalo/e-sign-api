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
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.Parse(src.Created ?? DateTime.Now.ToString()).ToString("dd.MM.yyyy HH:mm:ss")))
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.Parse(src.LastModified ?? DateTime.Now.ToString()).ToString("dd.MM.yyyy HH:mm:ss")))
                .ReverseMap();
            CreateMap<TemplateSummary, DocuSign.eSign.Model.TemplateSummary>().ReverseMap();
            CreateMap<Envelope, DocuSign.eSign.Model.EnvelopeDefinition>().ReverseMap();
            CreateMap<EnvelopeSummary, DocuSign.eSign.Model.EnvelopeSummary>()
                .ForMember(dest => dest.EnvelopeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap();
            CreateMap<DocuSign.eSign.Model.Envelope, EnvelopeSummary>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EnvelopeId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.EmailSubject))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.Parse(src.CreatedDateTime ?? DateTime.Now.ToString()).ToString("dd.MM.yyyy HH:mm:ss")))
                .ReverseMap();
        }
    }
}
