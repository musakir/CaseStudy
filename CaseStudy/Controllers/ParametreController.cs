using CaseStudy.Business.Abstract;
using CaseStudy.Business.Concrete;
using CaseStudy.Core.Utilities.Results;
using CaseStudy.Entities.Model.Parametre;
using CaseStudy.WebAPI.Controllers.BasesController;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudy.WebApi.Controllers
{
    [Route("api/Parametre")]
    [ApiController]
    public class ParametreController : CustomBaseController
    {
        private readonly IParametreService parametreService;
        public ParametreController(IParametreService iParametreService)
        {
            parametreService = iParametreService;
        }

        [Route("EyaletList")]
        [HttpGet]
        public async Task<ActionResult<ResultApi<List<EyaletList>>>> EyaletList()
        {
            return CreateActionResultApi(await parametreService.EyaletList());
        }

        [Route("TatilTurList")]
        [HttpGet]
        public async Task<ActionResult<ResultApi<List<TatilTurList>>>> TatilTurList()
        {
            return CreateActionResultApi(await parametreService.TatilTurList());
        }
    }
}
