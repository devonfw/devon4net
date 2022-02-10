using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace OASP4Net.Business.Controller.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Microsoft.AspNetCore.Mvc.Controller
    {
        /// <summary>
        ///     This method shows the get method
        /// </summary>
        /// <returns>A string containing a pair key-Value</returns>
        /// <response code="201">Account created</response>
        /// <response code="400">Username already in use</response>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] {"value1", "value2"};
        }

        /// <summary>
        ///     Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        ///     Creates a TodoItem.
        /// </summary>
        /// <remarks>
        ///     Note that the key is a GUID and not an integer.
        ///     POST /Todo
        ///     {
        ///     "key": "0e7ad584-7788-4ab1-95a6-ca0a5b444cbb",
        ///     "name": "Item1",
        ///     "isComplete": true
        ///     }
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>New Created Todo Item</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(typeof(int), 201)]
        [ProducesResponseType(typeof(int), 400)]
        public int Post([FromBody] string value)
        {
            return 4;
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        ///     Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}