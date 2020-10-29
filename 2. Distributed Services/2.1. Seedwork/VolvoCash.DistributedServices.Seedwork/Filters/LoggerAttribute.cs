using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using VolvoCash.CrossCutting.Localization;

namespace VolvoCash.DistributedServices.Seedwork.Filters
{
    public class LoggerAttribute : ActionFilterAttribute
    {
        #region Members
        private Stopwatch watch;
        private readonly ILogger<LoggerAttribute> logger;
        private readonly ILocalization resources;
        #endregion

        #region Constructor
        public LoggerAttribute(ILogger<LoggerAttribute> _logger)
        {
            logger = _logger;
            resources = LocalizationFactory.CreateLocalResources();
        }
        #endregion

        #region Public Methods
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            watch = new Stopwatch();
            watch.Start();

            var parameter = context.ActionArguments.FirstOrDefault();
            var logMessage = string.Format(resources.GetStringResource(LocalizationKeys.DistributedServices.info_OnExecuting), context.ActionDescriptor.DisplayName, watch.ElapsedMilliseconds);
            var parameterValue = parameter.Value;
            var values = parameterValue != null ? Newtonsoft.Json.JsonConvert.SerializeObject(parameterValue) : null;
            logMessage += string.Format(resources.GetStringResource(LocalizationKeys.DistributedServices.info_Parameter), values ?? "/null/");
            logger.LogInformation(logMessage);
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            string logMessage = string.Format(resources.GetStringResource(LocalizationKeys.DistributedServices.info_OnExecuted), context.ActionDescriptor.DisplayName, watch.ElapsedMilliseconds);
            logger.LogInformation(logMessage);
            watch.Stop();
        }
        #endregion
    }
}
