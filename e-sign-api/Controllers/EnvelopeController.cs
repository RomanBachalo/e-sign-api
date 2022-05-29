using e_sign_api.Helpers;
using e_sign_api.Models;
using e_sign_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace e_sign_api.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class EnvelopeController: Controller
    {
        private readonly IEnvelopeService _envelopeService;
        private readonly AuthHelper _authHelper;

        public EnvelopeController(IEnvelopeService templateService, AuthHelper authHelper)
        {
            _envelopeService = templateService;
            _authHelper = authHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEnvelopes()
        {
            if (!_authHelper.CheckToken())
            {
                _authHelper.LoginUser();
            }

            var userInfo = _authHelper.User;
            var templates = await _envelopeService.GetAll(userInfo.AccessToken, userInfo.AccountId);

            return Ok(templates);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEnvelopeById(string id)
        {
            if (!_authHelper.CheckToken())
            {
                _authHelper.LoginUser();
            }

            var userInfo = _authHelper.User;
            var template = await _envelopeService.GetById(userInfo.AccessToken, userInfo.AccountId, id);

            return Ok(template);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnvelope([FromBody] PostEnvelopeModel envelope)
        {
            if (!_authHelper.CheckToken())
            {
                _authHelper.LoginUser();
            }

            var userInfo = _authHelper.User;
            var createResponse = await _envelopeService.Create(userInfo.AccessToken, userInfo.AccountId, envelope);

            return Ok(createResponse);
        }
    }
}
