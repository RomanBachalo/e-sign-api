using e_sign_api.Models;

namespace e_sign_api.Services
{
    public interface IEnvelopeService
    {
        Task<EnvelopeSummary[]> GetAll(string accessToken, string accountId);
        Task<Envelope> GetById(string accessToken, string accountId, string id);
        Task<EnvelopeSummary> Create(string accessToken, string accountId, Envelope template);
        Task<EnvelopeSummary> Update(string accessToken, string accountId, string templateId, Envelope template);
        Task<EnvelopeSummary> Delete(string accessToken, string accountId, string id);
    }
}
