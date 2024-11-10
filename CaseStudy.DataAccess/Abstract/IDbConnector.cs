using System.Data;

namespace CaseStudy.DataAccess.Abstract
{
    public interface IDbConnector
    {
        IDbConnection CreateConnection();
    }
}
