using CaseStudy.Core.Utilities.Results;
using CaseStudy.Entities.Filter;
using CaseStudy.Entities.Filter.Tatil;
using CaseStudy.Entities.Model.Tatil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Business.Abstract
{
    public interface ITatilService
    {
        Task<ResultApi<int>> TatilSave(TatilSave iu);
        Task<ResultApi<List<TatilList>>> TatilList(TatilFilter filter);
        Task<ResultApi<bool>> TatilDelete(TatilFilter filter);
        Task<ResultApi<List<EyaletTatilSecimList>>> EyaletTatilSecimList(int TatilId);
    }
}
