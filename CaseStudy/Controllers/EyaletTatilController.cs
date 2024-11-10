using CaseStudy.WebAPI.Controllers.BasesController;
using CaseStudy.Business.Abstract;
using CaseStudy.Core.Utilities.Results;
using CaseStudy.Entities.Filter.EyaletTatil;
using CaseStudy.Entities.Model.EyaletTatil;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudy.WebApi.Controllers
{
    [Route("api/EyaletTatil")]
    //[Authorize]
    [ApiController]
    public class EyaletTatilController : CustomBaseController
    {
        private readonly IEyaletTatilService eyaletTatilService;
        public EyaletTatilController(IEyaletTatilService iEyaletTatilService)
        {
            eyaletTatilService = iEyaletTatilService;
        }

        [Route("EyaletTatilSave")]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ResultApi<int>>> EyaletTatilSave([FromBody] EyaletTatilSave iu)
        {
            return CreateActionResultApi(await eyaletTatilService.EyaletTatilSave(iu));
        }

        [Route("EyaletTatilList")]
        [HttpGet]
        public async Task<ActionResult<ResultApi<List<EyaletTatilList>>>> EyaletTatilList([FromQuery] EyaletTatilFilter filter)
        {
            return CreateActionResultApi(await eyaletTatilService.EyaletTatilList(filter));
        }

        [Route("EyaletTatilDelete")]
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult<ResultApi<bool>>> EyaletTatilDelete([FromQuery] EyaletTatilFilter filter)
        {
            return CreateActionResultApi(await eyaletTatilService.EyaletTatilDelete(filter));
        }
    }
}
