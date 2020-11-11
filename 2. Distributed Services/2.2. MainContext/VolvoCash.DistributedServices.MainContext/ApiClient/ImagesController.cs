using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolvoCash.DistributedServices.Seedwork.Filters;
using VolvoCash.DistributedServices.Seedwork.Utils;

namespace VolvoCash.DistributedServices.MainContext.ApiClient
{
    [ApiController]
    [Route("api_client")]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class ImagesController : Controller
    {
        #region Constructor
        public ImagesController()
        {
        }
        #endregion

        #region Public Methods
        [AllowAnonymous]
        [HttpGet("qrcode")]
        public ActionResult GetQrCode([FromQuery] string content)
        {
            var qr = QrGenerator.GenerateQrCode(content);
            return File(qr, "image/jpeg");
        }
        #endregion
    }
}
