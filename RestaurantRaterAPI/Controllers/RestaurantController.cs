using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    public class RestaurantController : ApiController
    {
        //Post (create)
        // api/Restaurant
        [HttpPost]
        public async Task<IHttpActionResult> CreateRestaurant([FromBody] Restaurant model)
        {
            //If the model is valid
            if (ModelState.IsValid)
            {
                //Store the model in the database
                return Ok("your restaurant was created!");

            }
            // The model is not valid, go ahead and reject it
            return BadRequest(ModelState);
        }
       
       
    }
}
