using AutoMapper;
using e_sign_api.Models;

namespace e_sign_api.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Template, DocuSign.eSign.Model.EnvelopeTemplate>().ReverseMap();
            CreateMap<TemplateSummary, DocuSign.eSign.Model.TemplateSummary>().ReverseMap();
            CreateMap<Envelope, DocuSign.eSign.Model.EnvelopeDefinition>().ReverseMap();
        }
    }
}
