using CaseStudy.Core.Utilities.Helper;
using CaseStudy.DataAccess.Abstract;
using CaseStudy.Business.Abstract;
using CaseStudy.Core.Utilities.Helper;
using CaseStudy.Core.Utilities.Results;
using CaseStudy.Core.Utilities.Security;
using CaseStudy.DataAccess.Abstract;
using CaseStudy.Entities.Model;
using CaseStudy.Entities.Model.IdentityToken;
using CaseStudy.Entities.Model.User;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Business.Concrete
{
    public class TokenService : ITokenService
    {
        private readonly IDbConnector connector;
        private readonly IConfiguration _configuration;

        public TokenService(ICaseStudyDbConnector iConnector, IConfiguration configuration)
        {
            connector = iConnector;
            _configuration = configuration;
        }

        private IToken CreateToken(ITokenUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenOptions = _configuration.GetSection("TokenOptions").Get<ITokenOptions>();
            var key = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey);

            var accessTokenExpires = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration);
            var refreshTokenExpires = DateTime.Now.AddDays(tokenOptions.RefreshTokenExpiration);

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var accessToken = new JwtSecurityToken(
                      issuer: tokenOptions.Issuer,
                      audience: tokenOptions.Audience,
                      expires: accessTokenExpires,
                      notBefore: DateTime.Now,
                      claims: SetClaims(user),
                      signingCredentials: signingCredentials
                  );

            var refreshToken = new JwtSecurityToken(
                      issuer: tokenOptions.Issuer,
                      audience: tokenOptions.Audience,
                      expires: refreshTokenExpires,
                      claims: SetClaims(user),
                      signingCredentials: signingCredentials
                  );

            var accessTokenRaw = tokenHandler.WriteToken(accessToken);
            var refreshTokenRaw = tokenHandler.WriteToken(refreshToken);

            var AccessTokenEncryption = Func.AesEncryptString(tokenOptions.AesSecurityKey, accessTokenRaw);
            var RefreshTokenEncrption = Func.AesEncryptString(tokenOptions.AesSecurityKey, refreshTokenRaw);

            return new IToken
            {
                AccessToken = AccessTokenEncryption,
                RefreshToken = RefreshTokenEncrption
            };
        }

        public ResultApi<IToken> RefreshToken(IToken tokens)
        {
            var handler = new JwtSecurityTokenHandler();

            try
            {
                var tokenOptions = _configuration.GetSection("TokenOptions").Get<ITokenOptions>();

                string RefreshToken = Func.AesDecryptString(tokenOptions.AesSecurityKey, tokens.RefreshToken);

                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(RefreshToken);
                long Expire = jwt.Claims.First(c => c.Type == "exp").Value.ackToLong();

                var jwtSecurityToken = handler.ReadJwtToken(RefreshToken);

                if (jwtSecurityToken.ValidTo <= DateTime.UtcNow)
                    return ResultApi<IToken>.Fail(null, 401, "Oturum yenilenemiyor");

                var user = new ITokenUser();

                string UserName = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Name).Value.ackToString();

                string sql = $@"exec SpIUser @pIslem=1, @UserName='{UserName}'";
                var resultUser = Orm.Query<IUser>(connector, sql);

                foreach (var item in resultUser)
                {
                    user.UserId = item.Id;
                    user.UserName = item.UserName;
                }

                return ResultApi<IToken>.Success(CreateToken(user), 200, "Success");
            }
            catch (Exception ex)
            {
                return ResultApi<IToken>.Fail(null, 500, ex.Message);
            }
        }

        public async Task<ResultApi<IToken>> Login(IUserLogin login)
        {
            if (login == null)
                return ResultApi<IToken>.Fail(null, 401, "Kullanıcı adı veya şifre hatalı .!");

            string sql = $@"exec SpIUser @pIslem=1, @UserName='{login.UserName}'";
            var resultUser = await Orm.QueryAsync<IUser>(connector, sql);

            if (resultUser == null || resultUser.AsList().Count == 0)
                return ResultApi<IToken>.Fail(null, 401, "Kullanıcı adı veya şifre hatalı .!");

            IUser user = new IUser();
            foreach (var item in resultUser)
            {
                user.Id = item.Id;
                user.UserName = item.UserName;
                user.Password = item.Password;
            }

            string sqlPassword = Func.ackTextDecryption(user.Password);

            if (sqlPassword != login.Password)
                return ResultApi<IToken>.Fail(null, 401, "Kullanıcı adı veya şifre hatalı .!");

            ITokenUser tUser = new ITokenUser();
            tUser.UserId = user.Id;
            tUser.UserName = user.UserName;

            IToken token = new IToken();
            token = CreateToken(tUser);

            if (token == null)
                return ResultApi<IToken>.Fail(null, 401, "Kullanıcı adı veya şifre hatalı .!");

            return ResultApi<IToken>.Success(token, 200, "Başarılı.");
        }

        private IEnumerable<Claim> SetClaims(ITokenUser user)
        {
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            return claims;
        }

        public ResultApi<ITokenControlResult> TokenControl(ITokenControl t)
        {
            var handler = new JwtSecurityTokenHandler();

            try
            {
                var tokenOptions = _configuration.GetSection("TokenOptions").Get<ITokenOptions>();

                t.Token = Func.AesDecryptString(tokenOptions.AesSecurityKey, t.Token);

                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(t.Token);
                long Expire = jwt.Claims.First(c => c.Type == "exp").Value.ackToLong();

                var jwtSecurityToken = handler.ReadJwtToken(t.Token);

                if (jwtSecurityToken.ValidTo <= DateTime.UtcNow)
                    return ResultApi<ITokenControlResult>.Fail(null, 401, "Oturum yenilenemiyor");

                var user = new ITokenUser();

                user.UserId = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value.ackToInt();
                user.UserName = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Name).Value.ackToString();

                ITokenControlResult result = new ITokenControlResult();
                result.Token = t.Token;

                return ResultApi<ITokenControlResult>.Success(result, 200, "Success");
            }
            catch (Exception ex)
            {
                return ResultApi<ITokenControlResult>.Fail(null, 500, ex.Message);
            }
        }

    }
}
