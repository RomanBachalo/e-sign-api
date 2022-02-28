using DocuSign.eSign.Client;
using e_sign_api.Models;
using Microsoft.Extensions.Caching.Memory;
using static DocuSign.eSign.Client.Auth.OAuth;

namespace e_sign_api.Helpers
{
    public class AuthHelper
    {
        public User User
        {
            get => _cache.Get<User>("User");
            set => _cache.Set("User", value);
        }

        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;
        private readonly ApiClient _apiClient;

        private OAuthToken authToken = null;

        public AuthHelper(IMemoryCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _configuration = configuration;

            _apiClient = new ApiClient();
        }

        public void LoginUser()
        {
            string integrationKey = _configuration["DocuSign:IntegrationKey"];
            string userId = _configuration["DocuSign:UserId"];
            string authServerUrl = _configuration["DocuSign:AuthServerUrl"];

            string privateKeyFileName = _configuration["DocuSign:PrivateKeyPath"];

            var fileNameOnly = Path.GetFileName(privateKeyFileName);
            if (string.IsNullOrEmpty(fileNameOnly))
            {
                fileNameOnly = "private.key";
            }

            var filePath = Path.GetDirectoryName(privateKeyFileName);
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = Directory.GetCurrentDirectory();
            }

            string privateKeyPath = Path.Combine(filePath, fileNameOnly);
            byte[] privateKeyBytes = File.ReadAllBytes(privateKeyPath);

            int expiresInHours = _configuration.GetValue<int>("DocuSign:ExpireTime");

            var scopes = new List<string>
            {
                "signature",
                "impersonation"
            };


            _apiClient.SetOAuthBasePath(authServerUrl);
            authToken = _apiClient.RequestJWTUserToken(integrationKey, userId, authServerUrl, privateKeyBytes,
                expiresInHours, scopes);

            this.User = GetUserInfo(authToken);
        }

        public void Logout()
        {
            this.authToken = null;
            this.User = null;
        }

        public bool CheckToken()
        {
            return User != null && User?.AccessToken != null && DateTime.Now < this.User.ExpireIn.Value;
        }

        private User GetUserInfo(OAuthToken token)
        {
            var userInfo = _apiClient.GetUserInfo(authToken.access_token);
            var acct = userInfo.Accounts.FirstOrDefault();

            if (acct == null)
            {
                throw new InvalidOperationException("The user does not have access to account");
            }

            var user = new User
            {
                Name = acct.AccountName,
                AccountId = acct.AccountId,
                AccessToken = token.access_token,
                RefreshToken = token.refresh_token,
                ExpireIn = DateTime.Now.AddSeconds(token.expires_in.Value),
            };

            return user;
        }
    }
}
