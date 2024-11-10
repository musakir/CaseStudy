using CaseStudy.Business.Abstract;
using CaseStudy.Core.Utilities.Helper;
using CaseStudy.Entities.Concrete.DataModels;
using CaseStudy.Entities.Model.IdentityToken;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudy.WebApi.Controllers.Filters
{
    public class ActionFilterMiddleware : IActionFilter
    {
        private readonly ISessionProvider sessionService;

        public ActionFilterMiddleware(ISessionProvider iSessionService)
        {
            sessionService = iSessionService;
        }

        public ObjectResult ReturnErrorContext(ActionExecutingContext context)
        {
            ObjectResult result = new ObjectResult(context.ModelState)
            {
                Value = "You are not authorized for this page",
                StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status405MethodNotAllowed
            };

            return result;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                if (!context.ModelState.IsValid)
                {
                    context.Result = new UnprocessableEntityObjectResult(context.ModelState);
                    return;
                }

                var routeName = ((ControllerActionDescriptor)context.ActionDescriptor).AttributeRouteInfo.Template;
                string FormCode = context.HttpContext.Request.Headers["Form"].FirstOrDefault()?.Split(" ").Last();
                string ProjectCode = context.HttpContext.Request.Headers["Project"].FirstOrDefault()?.Split(" ").Last();

                var splits = routeName.Split('/');

                if (splits.Length != 3)
                {
                    context.Result = ReturnErrorContext(context);
                    return;
                }

                string iFormCode = splits[1];
                string iActionCode = splits[2];

                //ITokenUser user = sessionService.GetUser();

                //if (user.UserId <= 0 || string.IsNullOrWhiteSpace(FormCode) || string.IsNullOrWhiteSpace(ProjectCode))
                //{
                //    context.Result = ReturnErrorContext(context);
                //    return;
                //}

                //IUserAuthControlFilter filter = new IUserAuthControlFilter();
                //filter.FormCode = FormCode;
                //filter.ActionCode = iActionCode;

                //bool AuthResultValue = false;
                //AuthResultValue = userService.IUserAuthControl(filter).Result.R;

                //if (AuthResultValue == false)
                //{
                //    context.Result = ReturnErrorContext(context);
                //    return;
                //}

                return;
            }
            catch (Exception ex)
            {
                context.Result = ReturnErrorContext(context);
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // our code after action executes
        }

    }
}
