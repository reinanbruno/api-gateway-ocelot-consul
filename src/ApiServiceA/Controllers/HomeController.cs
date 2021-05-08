using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace ApiServiceA.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class HomeController : Controller
    {
        /// <summary>
        /// Carregar informação do Service A
        /// </summary>
        /// <response code="200">Retornado com sucesso.</response>
        /// 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult Get()
        {
            return Ok("Service A");
        }
    }
}
