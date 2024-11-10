using CaseStudy.Business.Abstract;
using CaseStudy.Core.Utilities.Results;
using CaseStudy.Entities.Model.IdentityToken;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudy.WebApi.Controllers.Filters
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ITokenService tokenService, IConfiguration configuration /*, IUserService userService, IJwtUtils jwtUtils*/)
        {
            try
            {
                string rToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (rToken != null && !context.Request.Path.Value.Contains("/api/Token/"))
                {
                    ResultApi<ITokenControlResult> TokenResult = new ResultApi<ITokenControlResult>();

                    ITokenControl tokenFilter = new ITokenControl();
                    tokenFilter.Token = rToken;

                    TokenResult = tokenService.TokenControl(tokenFilter);

                    if (TokenResult == null || TokenResult.OperationStatusCode != 200)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return;
                    }
                    else
                    {
                        context.Request.Headers["Authorization"] = "Bearer " + TokenResult.R.Token;
                        context.Response.StatusCode = StatusCodes.Status200OK;
                    }
                }

                await _next(context);
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
