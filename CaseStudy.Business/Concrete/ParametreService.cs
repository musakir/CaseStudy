using CaseStudy.Business.Abstract;
using CaseStudy.Core.Utilities.Helper;
using CaseStudy.Core.Utilities.Results;
using CaseStudy.DataAccess.Abstract;
using CaseStudy.Entities.Model.Parametre;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Business.Concrete
{
    public class ParametreService : IParametreService
    {
        private readonly IConfiguration configuration;
        private readonly IDbConnector connectorCaseStudy;

        public ParametreService(IConfiguration iConfiguration, ICaseStudyDbConnector iConnectorCaseStudy)
        {
            configuration = iConfiguration;
            connectorCaseStudy = iConnectorCaseStudy;
        }

        public async Task<ResultApi<List<EyaletList>>> EyaletList()
        {
            ResultApi<List<EyaletList>> _result = new ResultApi<List<EyaletList>>();

            try
            {
                string sql = $@"exec SpParametre @pIslem=1";

                var returnData = await Orm.QueryAsync<EyaletList>(connectorCaseStudy, sql);

                _result = ResultApi<List<EyaletList>>.Success(returnData.AsList(), 200, "Başarılı.");
            }
            catch (Exception ex)
            {
                _result = ResultApi<List<EyaletList>>.Fail(null, 400, $@"Hata: {ex.Message}");
            }

            return _result;
        }

        public async Task<ResultApi<List<TatilTurList>>> TatilTurList()
        {
            ResultApi<List<TatilTurList>> _result = new ResultApi<List<TatilTurList>>();

            try
            {
                string sql = $@"exec SpParametre @pIslem=2";

                var returnData = await Orm.QueryAsync<TatilTurList>(connectorCaseStudy, sql);

                _result = ResultApi<List<TatilTurList>>.Success(returnData.AsList(), 200, "Başarılı.");
            }
            catch (Exception ex)
            {
                _result = ResultApi<List<TatilTurList>>.Fail(null, 400, $@"Hata: {ex.Message}");
            }

            return _result;
        }

    }
}
