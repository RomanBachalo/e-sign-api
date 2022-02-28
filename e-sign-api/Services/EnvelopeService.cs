using AutoMapper;
using DocuSign.eSign.Model;
using e_sign_api.Helpers;
using e_sign_api.Models;

namespace e_sign_api.Services
{
    public class EnvelopeService : IEnvelopeService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public EnvelopeService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Models.TemplateSummary> Create(string accessToken, string accountId, Models.Envelope envelope)
        {
            var envelopesApi = ApiClientHelper.CreateEnvelopesApiClient(_configuration["DocuSign:BasePath"], accessToken);

            var envelopeDefinition = _mapper.Map<EnvelopeDefinition>(envelope);
            var envelopeCreateSummary = await envelopesApi.CreateEnvelopeAsync(accountId, envelopeDefinition);

            return _mapper.Map<Models.TemplateSummary>(envelopeCreateSummary);
        }

        public async Task<Models.TemplateSummary> Delete(string accessToken, string accountId, string templateId)
        {
            var templatesApi = ApiClientHelper.CreateTemplatesApiClient(_configuration["DocuSign:BasePath"], accessToken);

            // update template status to "deleted"
            var template = new EnvelopeTemplate
            {
                Status = "deleted"
            };

            var summary = await templatesApi.UpdateAsync(accountId, templateId, template);
            return _mapper.Map<Models.TemplateSummary>(summary);
        }

        public async Task<Template> GetById(string accessToken, string accountId, string templateId)
        {
            var templatesApi = ApiClientHelper.CreateTemplatesApiClient(_configuration["DocuSign:BasePath"], accessToken);

            var template = await templatesApi.GetAsync(accountId, templateId);
            if (template == null) throw new InvalidOperationException("Template was not retrieved");

            return _mapper.Map<Template>(template);
        }

        public async Task<Models.TemplateSummary[]> GetAll(string accessToken, string accountId)
        {
            var templatesApi = ApiClientHelper.CreateTemplatesApiClient(_configuration["DocuSign:BasePath"], accessToken);

            var templatesResultResponse = await templatesApi.ListTemplatesAsync(accountId);
            if (templatesResultResponse == null) throw new InvalidOperationException("Templates were not retrieved");

            var templates = templatesResultResponse.EnvelopeTemplates.ToArray();
            if (templates == null) throw new InvalidOperationException("Templates were not retrieved");

            return _mapper.Map<Models.TemplateSummary[]>(templates);
        }

        public async Task<Models.TemplateSummary> Update(string accessToken, string accountId, string templateId, Models.Envelope envelope)
        {
            var templatesApi = ApiClientHelper.CreateTemplatesApiClient(_configuration["DocuSign:BasePath"], accessToken);

            var envelopeTemplate = _mapper.Map<EnvelopeTemplate>(template);
            var templateUpdateSummary = await templatesApi.UpdateAsync(accountId, templateId, envelopeTemplate);

            return _mapper.Map<Models.TemplateSummary>(templateUpdateSummary);
        }
    }
}
