using System.Threading.Tasks;
using Automated.Testing.System.ApplicationServices.Interfaces;
using Automated.Testing.System.Common.Dictionary.Dto;
using Automated.Testing.System.Common.Dictionary.Dto.Request;
using Automated.Testing.System.Core.Execute;
using Automated.Testing.System.Core.Execute.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Automated.Testing.System.Web.Controllers
{
    /// <summary>
    /// Api для работы со справочниками
    /// </summary>
    // [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class TestController : Controller
    {

        public TestController()
        {
        }

    }
}