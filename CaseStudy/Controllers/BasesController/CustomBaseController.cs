using CaseStudy.Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CaseStudy.WebAPI.Controllers.BasesController
{
    public class CustomBaseController
    {
        public ActionResult<Response<T>> CreateActionResultInstance<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

        public ActionResult<T> CreateActionResultInstanceList<T>(T response)
        {
            return new ObjectResult(response);
        }

        public ActionResult CreateActionResultInstanceListWithStatusCode<T>(ResultApi<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.OperationStatusCode
            };
        }

        public ActionResult<ResultApi<T>> CreateActionResultApi<T>(ResultApi<T> result)
        {
            return new ObjectResult(result);
        }
    }
}
