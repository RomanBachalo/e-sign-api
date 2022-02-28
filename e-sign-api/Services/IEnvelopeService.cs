using e_sign_api.Models;

namespace e_sign_api.Services
{
    public interface IEnvelopeService
    {
        Task<TemplateSummary[]> GetAll(string accessToken, string accountId);
        Task<Envelope> GetById(string accessToken, string accountId, string id);
        Task<TemplateSummary> Create(string accessToken, string accountId, Envelope template);
        Task<TemplateSummary> Update(string accessToken, string accountId, string templateId, Envelope template);
        Task<TemplateSummary> Delete(string accessToken, string accountId, string id);
    }
}
