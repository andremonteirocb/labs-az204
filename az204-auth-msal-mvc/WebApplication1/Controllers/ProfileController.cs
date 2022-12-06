using Microsoft.AspNetCore.Mvc;
using Services;

namespace az204_msal.mvc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IMeService _meService;
        private readonly ILogger<ProfileController> _logger;
        public ProfileController(ILogger<ProfileController> logger,
            IAuthenticateService authenticateService,
            IMeService meService)
        {
            _authenticateService = authenticateService;
            _meService = meService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<dynamic> Get()
        {
            var accessToken = await _authenticateService.Authenticate();
            return await _meService.ReadMe(accessToken);
        }
    }
}