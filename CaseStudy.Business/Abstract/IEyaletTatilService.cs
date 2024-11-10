using CaseStudy.Core.Utilities.Results;
using CaseStudy.Entities.Filter.EyaletTatil;
using CaseStudy.Entities.Model.EyaletTatil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Business.Abstract
{
    public interface IEyaletTatilService
    {
        Task<ResultApi<int>> EyaletTatilSave(EyaletTatilSave iu);
        Task<ResultApi<List<EyaletTatilList>>> EyaletTatilList(EyaletTatilFilter filter);
        Task<ResultApi<bool>> EyaletTatilDelete(EyaletTatilFilter filter);
    }
}
