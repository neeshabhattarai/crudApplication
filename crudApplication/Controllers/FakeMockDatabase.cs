using crudApplication.DatabaseRelated;
using Microsoft.AspNetCore.Mvc;

namespace crudApplication.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class FakeMockDatabase : ControllerBase
    {
     
       
            private readonly CloudDatabase _database;

            public FakeMockDatabase(CloudDatabase database)
            {
                _database = database;
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> Get(string id)
            {
                var result = await _database.GetProductName(id);
                return Ok(result);
            }
        }

    }

