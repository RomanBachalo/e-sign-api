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

            var doc = new Document
            {
                HtmlDefinition = new DocumentHtmlDefinition
                {
                    Source = "<></>",
                    DocumentId = "1"
                },
                Name = "Order acknowledgement"
            };
        }

        public async Task<Models.EnvelopeSummary> Create(string accessToken, string accountId, Models.Envelope envelope)
        {
            var envelopesApi = ApiClientHelper.CreateEnvelopesApiClient(_configuration["DocuSign:BasePath"], accessToken);

            var envelopeDefinition = _mapper.Map<EnvelopeDefinition>(envelope);
            var envelopeCreateSummary = await envelopesApi.CreateEnvelopeAsync(accountId, envelopeDefinition);

            return _mapper.Map<Models.EnvelopeSummary>(envelopeCreateSummary);
        }

        public async Task<Models.EnvelopeSummary> Delete(string accessToken, string accountId, string envelopeId)
        {
            var envelopesApi = ApiClientHelper.CreateEnvelopesApiClient(_configuration["DocuSign:BasePath"], accessToken);

            var envelopeDeleteModel = new DocuSign.eSign.Model.Envelope
            {
                Status = "voided"
            };
            var envelopeDeleteSummary = await envelopesApi.UpdateAsync(accountId, envelopeId, envelopeDeleteModel);
            return _mapper.Map<Models.EnvelopeSummary>(envelopeDeleteSummary);
        }

        public async Task<Models.Envelope> GetById(string accessToken, string accountId, string envelopeId)
        {
            var envelopesApi = ApiClientHelper.CreateEnvelopesApiClient(_configuration["DocuSign:BasePath"], accessToken);

            var envelope = await envelopesApi.GetEnvelopeAsync(accountId, envelopeId);
            if (envelope == null) throw new InvalidOperationException("Envelope was not retrieved");

            return _mapper.Map<Models.Envelope>(envelope);
        }

        public async Task<Models.EnvelopeSummary[]> GetAll(string accessToken, string accountId)
        {
            var envelopesApi = ApiClientHelper.CreateEnvelopesApiClient(_configuration["DocuSign:BasePath"], accessToken);

            var envelopesInformation = await envelopesApi.ListStatusAsync(accountId);
            if (envelopesInformation == null) throw new InvalidOperationException("Envelopes were not retrieved");

            var envelopes = envelopesInformation.Envelopes;
            if (envelopes == null || envelopes.Count == 0) throw new InvalidOperationException("Envelopes were not retrieved");

            return _mapper.Map<Models.EnvelopeSummary[]>(envelopes.ToArray());
        }

        public async Task<Models.EnvelopeSummary> Update(string accessToken, string accountId, string envelopeId, Models.Envelope envelope)
        {
            var envelopesApi = ApiClientHelper.CreateEnvelopesApiClient(_configuration["DocuSign:BasePath"], accessToken);

            var envelopeData = _mapper.Map<DocuSign.eSign.Model.Envelope>(envelope);
            var envelopeUpdateSummary = await envelopesApi.UpdateAsync(accountId, envelopeId, envelopeData);

            return _mapper.Map<Models.EnvelopeSummary>(envelopeUpdateSummary);
        }
    }
}
