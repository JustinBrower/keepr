using keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultController : ControllerBase
    {
        private readonly VaultsService _vs;

        public VaultController(VaultsService vs)
        {
            _vs = vs;
        }
    }
}