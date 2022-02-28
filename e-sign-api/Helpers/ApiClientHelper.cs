using DocuSign.eSign.Api;
using DocuSign.eSign.Client;

namespace e_sign_api.Helpers
{
    public class ApiClientHelper
    {
        public static ApiClient CreateApiClient(string basePath, string accessToken)
        {
            var apiClient = new ApiClient(basePath);
            apiClient.Configuration.DefaultHeader.Add("Authorization", $"Bearer {accessToken}");
            apiClient.Configuration.DefaultHeader.Add("Content-Type", "application/json");

            return apiClient;
        }

        public static TemplatesApi CreateTemplatesApiClient(string basePath, string accessToken)
        {
            var apiClient = CreateApiClient(basePath, accessToken);
            return new TemplatesApi(apiClient);
        }

        public static EnvelopesApi CreateEnvelopesApiClient(string basePath, string accessToken)
        {
            var apiClient = CreateApiClient(basePath, accessToken);
            return new EnvelopesApi(apiClient);
        }
    }
}
