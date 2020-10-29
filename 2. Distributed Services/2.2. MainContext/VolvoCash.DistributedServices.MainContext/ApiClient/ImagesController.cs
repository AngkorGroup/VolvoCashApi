using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using VolvoCash.Application.MainContext.Charges.Services;
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
