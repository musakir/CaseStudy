using CaseStudy.Business.Abstract;
using CaseStudy.Core.Utilities.Helper;
using CaseStudy.DataAccess.Abstract;
using CaseStudy.Business.Abstract;
using CaseStudy.Core.Utilities.Helper;
using CaseStudy.Core.Utilities.Results;
using CaseStudy.DataAccess.Abstract;
using CaseStudy.Entities.Filter;
using CaseStudy.Entities.Filter.Tatil;
using CaseStudy.Entities.Model.IdentityToken;
using CaseStudy.Entities.Model.Tatil;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Business.Concrete
{
    public class TatilService : ITatilService
    {
        private readonly IConfiguration configuration;
        private readonly IDbConnector connectorCaseStudy;
        private readonly ISessionProvider session;

        public TatilService(IConfiguration iConfiguration, ICaseStudyDbConnector iConnectorCaseStudy, ISessionProvider iSession)
        {
            configuration = iConfiguration;
            connectorCaseStudy = iConnectorCaseStudy;
            session = iSession;
        }

        public async Task<ResultApi<int>> TatilSave(TatilSave iu)
        {
            ResultApi<int> _result = new ResultApi<int>();

            try
            {
                if (iu == null)
                    return ResultApi<int>.Fail(0, 400, $@"Hata: Kayıt bulunamadı .!");

                ITokenUser user = session.GetUser();

                string sql = $@"exec SpTatil @pIslem=@pIslem, @Id=@Id, @TatilTurId=@TatilTurId, @Tarih=@Tarih, @Aciklama=@Aciklama, @Aktif = @Aktif, @UserId=@UserId";

                var parameters = new DynamicParameters();
                parameters.Add("pIslem", 1);
                parameters.Add("Id", iu.Id);
                parameters.Add("TatilTurId", iu.TatilTurId);
                parameters.Add("Tarih", iu.Tarih?.ToString("yyyy-MM-dd HH:mm:ss"));
                parameters.Add("Aciklama", iu.Aciklama);
                parameters.Add("Aktif", iu.Aktif);
                parameters.Add("UserId", user.UserId);

                var returnData = await Orm.ExecuteScalarAsync(connectorCaseStudy, sql, parameters);

                if (returnData.ackToInt() <= 0)
                    return ResultApi<int>.Fail(0, 400, $@"Hata: Kayıt oluşturulamadı .!");

                _result = ResultApi<int>.Success(returnData.ackToInt(), 200, "Başarılı.");
            }
            catch (Exception ex)
            {
                _result = ResultApi<int>.Fail(0, 400, $@"Hata: {ex.Message}");
            }

            return _result;
        }

        public async Task<ResultApi<List<TatilList>>> TatilList(TatilFilter filter)
        {
            ResultApi<List<TatilList>> _result = new ResultApi<List<TatilList>>();

            try
            {
                string where = "";

                ITokenUser user = session.GetUser();
                where += $@", @UserId=@UserId";

                var parameters = new DynamicParameters();
                parameters.Add("UserId", user.UserId);

                if (filter.Id > 0)
                {
                    where += $@", @Id=@Id";
                    parameters.Add("Id", filter.Id);
                }

                string sql = $@"exec SpTatil @pIslem=5 {where}";

                var returnData = await Orm.QueryAsync<TatilList>(connectorCaseStudy, sql, parameters);

                _result = ResultApi<List<TatilList>>.Success(returnData.AsList(), 200, "Başarılı.");
            }
            catch (Exception ex)
            {
                _result = ResultApi<List<TatilList>>.Fail(null, 400, $@"Hata: {ex.Message}");
            }

            return _result;
        }

        public async Task<ResultApi<bool>> TatilDelete(TatilFilter filter)
        {
            ResultApi<bool> _result = new ResultApi<bool>();

            try
            {
                ITokenUser user = session.GetUser();

                var parameters = new DynamicParameters();
                parameters.Add("pIslem", 2);
                parameters.Add("Id", filter.Id);
                parameters.Add("UserId", user.UserId);

                string sql = $@"exec SpTatil @pIslem=@pIslem, @Id=@Id, @UserId=@UserId";

                var returnData = await Orm.ExecuteScalarAsync(connectorCaseStudy, sql, parameters);

                if (returnData.ackToInt() <= 0)
                    return ResultApi<bool>.Fail(false, 400, $@"Hata: Bir sorun oluştu .!");

                _result = ResultApi<bool>.Success(true, 200, "Başarılı.");
            }
            catch (Exception ex)
            {
                _result = ResultApi<bool>.Fail(false, 400, $@"Hata: {ex.Message}");
            }

            return _result;
        }

        public async Task<ResultApi<List<EyaletTatilSecimList>>> EyaletTatilSecimList(int TatilId)
        {
            ResultApi<List<EyaletTatilSecimList>> _result = new ResultApi<List<EyaletTatilSecimList>>();

            try
            {
                string where = "";

                ITokenUser user = session.GetUser();
                where += $@", @UserId=@UserId";

                var parameters = new DynamicParameters();
                parameters.Add("TatilId", TatilId);

                string sql = $@"exec SpTatil @pIslem=6, @TatilId=@TatilId";

                var returnData = await Orm.QueryAsync<EyaletTatilSecimList>(connectorCaseStudy, sql, parameters);

                _result = ResultApi<List<EyaletTatilSecimList>>.Success(returnData.AsList(), 200, "Başarılı.");
            }
            catch (Exception ex)
            {
                _result = ResultApi<List<EyaletTatilSecimList>>.Fail(null, 400, $@"Hata: {ex.Message}");
            }

            return _result;
        }

    }
}
