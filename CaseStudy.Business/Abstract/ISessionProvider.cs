using CaseStudy.Entities.Concrete.DataModels;
using CaseStudy.Entities.Model.IdentityToken;

namespace CaseStudy.Business.Abstract
{
    public interface ISessionProvider
    {
        ITokenUser GetUser();
    }
}
