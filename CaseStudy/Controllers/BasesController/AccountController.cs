using CaseStudy.WebAPI.Controllers.BasesController;
using CaseStudy.Business.Abstract;
using CaseStudy.Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CaseStudy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : CustomBaseController
    {
        private readonly ITokenService tokenService;

        //public AccountController(ITokenService iTokenService)
        //{
        //    tokenService = iTokenService;
        //}

        //[HttpPost("login")]
        //public async Task<ActionResult<ResultApi<IToken>>> Login([FromBody] IUserLogin login)
        //{
        //    return CreateActionResultApi(await tokenService.Login(login));
        //}

        //[HttpPost("refresh")]
        //public ActionResult<ResultApi<IToken>> RefreshLogin([FromBody] IToken tokens)
        //{
        //    return CreateActionResultApi(tokenService.RefreshToken(tokens));
        //}
    }
}
