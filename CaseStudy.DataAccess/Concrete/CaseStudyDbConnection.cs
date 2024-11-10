using CaseStudy.DataAccess.Abstract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.DataAccess.Concrete
{
    public class CaseStudyDbConnection : ICaseStudyDbConnector
    {
        private readonly IConfiguration _configuration;

        public CaseStudyDbConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetSection("ConnectionStrings:CaseStudyDb").Value);
        }
    }
}
