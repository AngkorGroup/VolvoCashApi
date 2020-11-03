using System;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using VolvoCash.Application.Seedwork;

namespace VolvoCash.DistributedServices.Seedwork.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        #region Members
        private readonly ILogger _logger;
        #endregion

        #region Constructor
        public CustomExceptionFilterAttribute(IHostingEnvironment hostingEnvironment,
                                              IModelMetadataProvider modelMetadataProvider,
                                              ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CustomExceptionFilterAttribute>();
        }
        #endregion

        #region Public Methods
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var message = new StringBuilder(exception.Message);

            if (exception is ApplicationValidationErrorsException)
            {
                var applicationValidationErrorsException = exception as ApplicationValidationErrorsException;                
                var validationErrors = applicationValidationErrorsException?.ValidationErrors;

                if (validationErrors != null)
                {
                    foreach (var error in validationErrors)
                    {
                        message.Append(Environment.NewLine + error);
                    }
                }
            }

            var errorMessage = message.ToString();
            var controller = context.ActionDescriptor.RouteValues["controller"];
            var action = context.ActionDescriptor.RouteValues["action"];
            var result = new JsonResult(new { controller, action, message });

            _logger.LogError(null, exception, result.Value.ToString());

            result.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new { errorMessage });
        }
        #endregion
    }
}
