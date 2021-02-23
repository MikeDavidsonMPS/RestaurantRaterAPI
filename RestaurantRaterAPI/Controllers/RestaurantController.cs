using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        //Post (create)
        // api/Restaurant
        [HttpPost]
        public async Task<IHttpActionResult> CreateRestaurant([FromBody] Restaurant model)
        {
            if (model is null)
            {
                return BadRequest("Your request body cannot be empty.");
            }
            //If the model is valid
            if (ModelState.IsValid)
            {
                //Store the model in the database
                _context.Restaurants.Add(model);
                int changeCount = await _context.SaveChangesAsync();

                return Ok("your restaurant was created!");

            }
            // The model is not valid, go ahead and reject it
            return BadRequest(ModelState);
        }

        //Get All //2.
        // api/Restaurant
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return Ok(restaurants);
        }
        //Get By ID
        // api/Restaurant/(id)
        [HttpGet]
        public async Task<IHttpActionResult> GetById([FromUri] int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant != null)
            {
                return Ok(restaurant);
            }

            return NotFound();
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant([FromUri] int id, [FromBody] Restaurant updateRestaurant)
        {
            // Check the ids if they match
            if (id != updateRestaurant?.Id)
            {
                return BadRequest(ModelState);
            }

            // Check the ModelState

            if (!ModelState.IsValid)
                return BadRequest("Ids do not match");

            //find the restaurant in the database
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            //update the properties
            restaurant.Name = updateRestaurant.Name;
            restaurant.Address = updateRestaurant.Address;

            //Save the changes
            await _context.SaveChangesAsync();

            return Ok("The restaurant was updated");
        }
       
        // Delete (delet)
        // api/Restaurant/(id)
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRestaurant([FromUri] int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant is null)
                return NotFound();

            _context.Restaurants.Remove(restaurant);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok("The restaurant was deleted.");
            }

            return InternalServerError();
        }
    }
}
