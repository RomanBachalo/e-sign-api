using DocuSign.eSign.Model;
using e_sign_api.Models;

namespace e_sign_api.Services
{
    public interface ITemplateService
    {
        Task<Models.TemplateSummary[]> GetAll(string accessToken, string accountId);
        Task<Template> GetById(string accessToken, string accountId, string templateId);
        Task<Models.TemplateSummary> Create(string accessToken, string accountId, PostTemplateModel template);
        Task<TemplateUpdateSummary> Update(string accessToken, string accountId, string templateId, PostTemplateModel template);
        Task<Models.TemplateSummary> Delete(string accessToken, string accountId, string templateId);
    }
}
