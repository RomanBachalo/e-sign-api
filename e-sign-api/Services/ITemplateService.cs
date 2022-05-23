using e_sign_api.Models;

namespace e_sign_api.Services
{
    public interface ITemplateService
    {
        Task<TemplateSummary[]> GetAll(string accessToken, string accountId);
        Task<Template> GetById(string accessToken, string accountId, string templateId);
        Task<TemplateSummary> Create(string accessToken, string accountId, PostTemplateModel template);
        Task<TemplateSummary> Update(string accessToken, string accountId, string templateId, Template template);
        Task<TemplateSummary> Delete(string accessToken, string accountId, string templateId);
    }
}
