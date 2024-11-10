using CaseStudy.WebAPI.Controllers.BasesController;
using CaseStudy.Business.Abstract;
using CaseStudy.Core.Utilities.Results;
using CaseStudy.Entities.Model.IdentityToken;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudy.WebApi.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly ITokenService tokenService;

        public UserController(ITokenService iTokenService)
        {
            tokenService = iTokenService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ResultApi<IToken>>> Login([FromBody] IUserLogin login)
        {
            return CreateActionResultApi(await tokenService.Login(login));
        }

        [HttpPost("RefreshLogin")]
        public ActionResult<ResultApi<IToken>> RefreshLogin([FromBody] IToken tokens)
        {
            return CreateActionResultApi(tokenService.RefreshToken(tokens));
        }
    }
}
