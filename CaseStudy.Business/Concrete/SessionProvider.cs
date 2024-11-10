using CaseStudy.Business.Abstract;
using CaseStudy.Entities.Concrete.DataModels;
using CaseStudy.Entities.Model.IdentityToken;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace CaseStudy.Business.Concrete
{
    public class SessionProvider : ISessionProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionProvider(IHttpContextAccessor httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public ITokenUser GetUser()
        {
            var user = new ITokenUser();

            var UserIdText = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var UserName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;

            var isUserIdCaseted = Int32.TryParse(UserIdText, out int UserId);

            if (isUserIdCaseted == false || UserName is null)
                return null;

            user.UserId = UserId;
            user.UserName = UserName;

            return user;
        }
    }
}
