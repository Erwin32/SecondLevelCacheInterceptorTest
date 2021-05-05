using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L2CacheTest
{
    [ApiController]
    [Route("/")]
    public class TestController : ControllerBase
    {
        public TestContext Context { get; }

        public TestController(TestContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = Context.Entries.Single(x => x.Id == 1);

            return Ok(data);
        }
    }
}