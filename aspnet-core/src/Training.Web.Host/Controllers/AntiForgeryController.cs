using System.Threading.Tasks;
using Abp.Web.Security.AntiForgery;
using Microsoft.AspNetCore.Antiforgery;
using Training.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Training.Web.Host.Controllers
{
    public class AntiForgeryController : TrainingControllerBase
    {
        private readonly IAntiforgery _antiforgery;
        private readonly IAbpAntiForgeryManager _antiForgeryManager;

        public AntiForgeryController(IAntiforgery antiforgery, IAbpAntiForgeryManager antiForgeryManager)
        {
            _antiforgery = antiforgery;
            _antiForgeryManager = antiForgeryManager;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }

        public void SetCookie()
        {
            _antiForgeryManager.SetCookie(HttpContext);
        }
    }
}
