using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace RedisCacheProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisCacheController :  ControllerBase
    {
        private readonly IDatabase _database;

        // inject the redis database
        // through constructor injection
        public RedisCacheController(IDatabase database)
        {
            _database = database;
        }
        [HttpGet]
        public async Task<string> GetvalueFromCache([FromQuery] string key)
        {
            var value = await _database.StringGetAsync(key);
            return value;
        }
        [HttpPost]
        public async Task<IActionResult> SetValueToRedis(string key,string value){

            await _database.StringSetAsync(key,value);
            return Ok("Data is added in the cache");
        }
        
        [HttpPost("another")]
        public async Task<IActionResult> SetValueToRedissecond([FromBody] KeyValuePair<string,string> data ){
         // using Key value pair will save you from 
         // adding a viewmodel;

        await _database.StringSetAsync(data.Key,data.Value);
        return Ok("Data is added in the cache");
        }


        }
    }
