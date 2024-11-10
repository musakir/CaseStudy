using CaseStudy.Business.Abstract;
using CaseStudy.Core.Utilities.Helper;
using CaseStudy.DataAccess.Abstract;
using CaseStudy.Business.Abstract;
using CaseStudy.Core.Utilities.Helper;
using CaseStudy.Core.Utilities.Results;
using CaseStudy.DataAccess.Abstract;
using CaseStudy.Entities.Filter.EyaletTatil;
using CaseStudy.Entities.Model.EyaletTatil;
using CaseStudy.Entities.Model.IdentityToken;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Business.Concrete
{
    public class EyaletTatilService : IEyaletTatilService
    {
        private readonly IConfiguration configuration;
        private readonly IDbConnector connectorCaseStudy;
        private readonly ISessionProvider session;

        public EyaletTatilService(IConfiguration iConfiguration, ICaseStudyDbConnector iConnectorCaseStudy, ISessionProvider iSession)
        {
            configuration = iConfiguration;
            connectorCaseStudy = iConnectorCaseStudy;
            session = iSession;
        }

        public async Task<ResultApi<int>> EyaletTatilSave(EyaletTatilSave iu)
        {
            ResultApi<int> _result = new ResultApi<int>();

            try
            {
                if (iu == null)
                    return ResultApi<int>.Fail(0, 400, $@"Hata: Kayıt bulunamadı .!");

                ITokenUser user = session.GetUser();

                string sql = $@"exec SpEyaletTatil @pIslem=@pIslem, @Id=@Id, @EyaletId=@EyaletId, @TatilId=@TatilId, @EyaletTatilSecim=@EyaletTatilSecim, @UserId=@UserId";

                var parameters = new DynamicParameters();
                parameters.Add("pIslem", 1);
                parameters.Add("Id", iu.Id);
                parameters.Add("EyaletId", iu.EyaletId);
                parameters.Add("TatilId", iu.TatilId);
                parameters.Add("EyaletTatilSecim", iu.EyaletTatilSecim);
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

        public async Task<ResultApi<List<EyaletTatilList>>> EyaletTatilList(EyaletTatilFilter filter)
        {
            ResultApi<List<EyaletTatilList>> _result = new ResultApi<List<EyaletTatilList>>();

            try
            {
                string where = "";

                var parameters = new DynamicParameters();

                if (filter.Id > 0)
                {
                    where += $@", @Id=@Id";
                    parameters.Add("Id", filter.Id);
                }

                if (!string.IsNullOrWhiteSpace(filter.Tarih?.ackToString()))
                {
                    where += $@", @Tarih=@Tarih";
                    parameters.Add("Tarih", filter.Tarih?.ToString("yyyy-MM-dd"));
                }

                if (filter.EyaletId > 0)
                {
                    where += $@", @EyaletId=@EyaletId";
                    parameters.Add("EyaletId", filter.EyaletId);
                }

                if (filter.UlkeId > 0)
                {
                    where += $@", @UlkeId=@UlkeId";
                    parameters.Add("UlkeId", filter.UlkeId);
                }

                if (filter.Yil > 0)
                {
                    where += $@", @Yil=@Yil";
                    parameters.Add("Yil", filter.Yil);
                }

                if (filter.Ay > 0)
                {
                    where += $@", @Ay=@Ay";
                    parameters.Add("Ay", filter.Ay);
                }

                if (filter.TatilTurId > 0)
                {
                    where += $@", @TatilTurId=@TatilTurId";
                    parameters.Add("TatilTurId", filter.TatilTurId);
                }

                string sql = $@"exec SpEyaletTatil @pIslem=5 {where}";

                var returnData = await Orm.QueryAsync<EyaletTatilList>(connectorCaseStudy, sql, parameters);

                _result = ResultApi<List<EyaletTatilList>>.Success(returnData.AsList(), 200, "Başarılı.");
            }
            catch (Exception ex)
            {
                _result = ResultApi<List<EyaletTatilList>>.Fail(null, 400, $@"Hata: {ex.Message}");
            }

            return _result;
        }

        public async Task<ResultApi<bool>> EyaletTatilDelete(EyaletTatilFilter filter)
        {
            ResultApi<bool> _result = new ResultApi<bool>();

            try
            {
                ITokenUser user = session.GetUser();

                var parameters = new DynamicParameters();
                parameters.Add("pIslem", 2);
                parameters.Add("Id", filter.Id);
                parameters.Add("UserId", user.UserId);

                string sql = $@"exec SpEyaletTatil @pIslem=@pIslem, @Id=@Id, @UserId=@UserId";

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

    }
}
