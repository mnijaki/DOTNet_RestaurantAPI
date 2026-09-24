using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers;

[ApiController]
[Route("api/restaurant/{restaurantId:int}/[controller]")]
public class DishController(IDishService dishService) : ControllerBase
{
    [HttpPost]
    public ActionResult CreateDish([FromBody]CreateDishDTO createDishDTO, [FromRoute]int restaurantId)
    {
        var dishId = dishService.CreateDish(createDishDTO, restaurantId);
        // TODO:Fix path to newly created dish.
        return CreatedAtAction(nameof(CreateDish), new { restaurantId = restaurantId, dishId = dishId }, null);
    }
}