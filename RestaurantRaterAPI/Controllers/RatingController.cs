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
    public class RatingController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        public object RestaurantId { get; private set; }

        //Create new ratings
        // POST api/rating
        [HttpPost]
        public async Task<IHttpActionResult> CreateRating([FromBody] Rating model)
        {
            // Check if is null
            if (model is null)
                return BadRequest("Your request body cannot be empry.");

            // Check if ModelState is invalid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Find the restaurant by the model.RestaurantId and see that it exists
            var restaurantEntity = await _context.Restaurants.FindAsync(model.RestaurantId);
            if (restaurantEntity is null)
                return BadRequest($"The target restaurant with the ID of {model.RestaurantId} does not exist");

            //Create the Rating

            //Add to the Rating Table
            //_context.Ratings.Add(model);

            //Add to the Restaurant Entity
            restaurantEntity.Ratings.Add(model);
            if (await _context.SaveChangesAsync() == 1)
                return Ok($"Your rated restaurant {restaurantEntity.Name} successfully");

            return InternalServerError();
        }

        //Get a rating by its Id

        //Get all Rating

        //Getr all Rating for specific restaurant by the Restaurant Id

        //Update a Rating

        //Delete a Rating
    }
}
