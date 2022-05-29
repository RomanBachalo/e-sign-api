using e_sign_api.Helpers;
using e_sign_api.Models;
using e_sign_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace e_sign_api.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class TemplateController : Controller
    {
        private readonly ITemplateService _templateService;
        private readonly AuthHelper _authHelper;

        public TemplateController(ITemplateService templateService, AuthHelper authHelper)
        {
            _templateService = templateService;
            _authHelper = authHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTemplates()
        {
            if (!_authHelper.CheckToken())
            {
                _authHelper.LoginUser();
            }

            var userInfo = _authHelper.User;
            var templates = await _templateService.GetAll(userInfo.AccessToken, userInfo.AccountId);

            return Ok(templates);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTemplateById(string id)
        {
            if (!_authHelper.CheckToken())
            {
                _authHelper.LoginUser();
            }

            var userInfo = _authHelper.User;
            var template = await _templateService.GetById(userInfo.AccessToken, userInfo.AccountId, id);

            return Ok(template);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTemplate([FromBody] PostTemplateModel template)
        {
            if (!_authHelper.CheckToken())
            {
                _authHelper.LoginUser();
            }

            var userInfo = _authHelper.User;
            var createResponse = await _templateService.Create(userInfo.AccessToken, userInfo.AccountId, template);

            return Ok(createResponse);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTemplate(string id, [FromBody] PostTemplateModel template)
        {
            if (!_authHelper.CheckToken())
            {
                _authHelper.LoginUser();
            }

            var userInfo = _authHelper.User;
            var updateResponse = await _templateService.Update(userInfo.AccessToken, userInfo.AccountId, id, template);

            return Ok(updateResponse);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTemplate(string id)
        {
            if (!_authHelper.CheckToken())
            {
                _authHelper.LoginUser();
            }

            var userInfo = _authHelper.User;
            var deletedTemplate = _templateService.Delete(userInfo.AccessToken, userInfo.AccountId, id);

            return Ok(deletedTemplate);
        }
    }
}
