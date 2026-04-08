using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers;

[Route("api/[controller]")]
public class RestaurantController(IRestaurantService restaurantService) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<RestaurantDTO>> GetAllRestaurants()
    {
        var restaurantsDTO = restaurantService.GetAllRestaurants();
        return Ok(restaurantsDTO);
    }
    
    [HttpGet("{id:int}")]
    public ActionResult<RestaurantDTO> GetRestaurantByIdRoute([FromRoute] int id)
    {
        var restaurantDTO = restaurantService.GetRestaurantById(id);
        if (restaurantDTO is null)
        {
            return NotFound();
        }
        
        return Ok(restaurantDTO);
    }
    
    [HttpGet("GetRestaurantById")]
    public ActionResult<RestaurantDTO> GetRestaurantByIdQuery([FromQuery] int id)
    {
        var restaurantDTO = restaurantService.GetRestaurantById(id);
        if (restaurantDTO is null)
        {
            return NotFound();
        }
        
        return Ok(restaurantDTO);
    }

    [HttpPost]
    public IActionResult CreateRestaurant([FromBody] CreateRestaurantDTO createRestaurantDTO)
    {
        // ModelState is a property of the ControllerBase class. It contains all validation errors.
        if (!ModelState.IsValid)
        {
            // Return BadRequest with all validation errors.
            return BadRequest(ModelState);
        }
        
        var restaurant = restaurantService.CreateRestaurant(createRestaurantDTO);
        
        // Return 201 Created with a location header pointing to a newly created restaurant.
        // CreatedAtAction(name_of_the_action_to_redirect_to, params_needed_for_that_action, created_object).
        return CreatedAtAction(nameof(GetRestaurantByIdRoute), new { id = restaurant.Id }, restaurant);
        // Alternative syntax with direct URI pointing to a created object.
        // return Created($"api/restaurant/{restaurant.Id}", restaurant); 
    }

    [HttpPut("{id:int}")]
    public ActionResult UpdateRestaurant([FromRoute] int id, [FromBody] UpdateRestaurantDTO updateRestaurantDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var wasUpdated = restaurantService.UpdateRestaurant(id, updateRestaurantDTO);
        if (wasUpdated)
        {
            return Ok(); 
        }
        
        return NotFound(); 
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteRestaurant([FromRoute] int id)
    {
        var wasDeleted = restaurantService.DeleteRestaurant(id);
        if (wasDeleted)
        {
            return NoContent();
        }
        
        return NotFound();
    }
}