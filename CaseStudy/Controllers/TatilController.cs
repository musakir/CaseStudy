using CaseStudy.WebAPI.Controllers.BasesController;
using CaseStudy.Business.Abstract;
using CaseStudy.Core.Utilities.Results;
using CaseStudy.Entities.Filter;
using CaseStudy.Entities.Filter.Tatil;
using CaseStudy.Entities.Model.Tatil;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudy.WebApi.Controllers
{
    [Route("api/Tatil")]
    [ApiController]
    public class TatilController : CustomBaseController
    {
        private readonly ITatilService tatilService;
        public TatilController(ITatilService iTatilService)
        {
            tatilService = iTatilService;
        }

        [Route("TatilSave")]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ResultApi<int>>> TatilSave([FromBody] TatilSave iu)
        {
            return CreateActionResultApi(await tatilService.TatilSave(iu));
        }

        [Route("TatilList")]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ResultApi<List<TatilList>>>> TatilList([FromQuery] TatilFilter filter)
        {
            return CreateActionResultApi(await tatilService.TatilList(filter));
        }

        [Route("TatilDelete")]
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult<ResultApi<bool>>> TatilDelete([FromQuery] TatilFilter filter)
        {
            return CreateActionResultApi(await tatilService.TatilDelete(filter));
        }

        [Route("EyaletTatilSecimList")]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ResultApi<List<EyaletTatilSecimList>>>> EyaletTatilSecimList([FromQuery] int TatilId)
        {
            return CreateActionResultApi(await tatilService.EyaletTatilSecimList(TatilId));
        }
    }
}
