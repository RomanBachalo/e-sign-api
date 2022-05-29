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

        public async Task<Models.EnvelopeSummary> Create(string accessToken, string accountId, PostEnvelopeModel envelope)
        {
            var envelopesApi = ApiClientHelper.CreateEnvelopesApiClient(_configuration["DocuSign:BasePath"], accessToken);

            EnvelopeDefinition env = new EnvelopeDefinition();
            env.TemplateId = envelope.TemplateId;

            TemplateRole signer = new TemplateRole();
            signer.Email = envelope.SignerEmail;
            signer.Name = envelope.SignerName;
            signer.RoleName = "Signer";

            env.TemplateRoles = new List<TemplateRole> { signer };
            env.Status = "sent";
            env.EmailSubject = "Sign this document set";

            var envelopeCreateSummary = await envelopesApi.CreateEnvelopeAsync(accountId, env);

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
    }
}
