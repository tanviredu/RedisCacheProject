using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace RedisCacheProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly IDatabase _database;

        public CacheController(IDatabase database)
        {
            _database = database; 
            
        }
        [HttpGet]
        public string GetValueFromCache([FromQuery] string key)
        {
            // we take the data from the query string
            return _database.StringGet(key);

        }
        [HttpPost]
        
        // added a key value paor data structure 
        // for setting data in redis database
        public void PostCahce([FromBody] KeyValuePair<string,string>data){
            // you can use two variable
            // but this is the good 
            // practice
            _database.StringSet(data.Key,data.Value);
        }


    }
}