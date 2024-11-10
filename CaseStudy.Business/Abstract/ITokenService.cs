using CaseStudy.Core.Utilities.Results;
using CaseStudy.Entities.Model;
using CaseStudy.Entities.Model.IdentityToken;
using CaseStudy.Entities.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Business.Abstract
{
    public interface ITokenService
    {
        Task<ResultApi<IToken>> Login(IUserLogin login);
        ResultApi<IToken> RefreshToken(IToken tokens);
        ResultApi<ITokenControlResult> TokenControl(ITokenControl tokens);
    }
}
