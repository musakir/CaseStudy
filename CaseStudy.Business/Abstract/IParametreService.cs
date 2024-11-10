using CaseStudy.Core.Utilities.Results;
using CaseStudy.Entities.Model.Parametre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Business.Abstract
{
    public interface IParametreService
    {
        Task<ResultApi<List<EyaletList>>> EyaletList();
        Task<ResultApi<List<TatilTurList>>> TatilTurList();
    }
}
