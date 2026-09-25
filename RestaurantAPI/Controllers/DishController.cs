using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers;

[ApiController]
[Route("api/restaurant/{restaurantId:int}/[controller]")]
public class DishController(IDishService dishService) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<DishDTO>> GetAllDishes([FromRoute]int restaurantId)
    {
        IEnumerable<DishDTO> dishesDTO = dishService.GetAllDishes(restaurantId);
        return Ok(dishesDTO);
    }
    
    [HttpGet("{dishId:int}")]
    public ActionResult<DishDTO> GetDishByIdRoute([FromRoute]int dishId)
    {
        DishDTO dishDTO = dishService.GetDishById(dishId);
        return Ok(dishDTO);
    }
    
    [HttpGet("GetDishById")]
    public ActionResult<DishDTO> GetDishByIdQuery([FromQuery] int dishId)
    {
        DishDTO dishDTO = dishService.GetDishById(dishId);
        return Ok(dishDTO);
    }
    
    [HttpPost]
    public ActionResult CreateDish([FromRoute] int restaurantId, [FromBody] CreateDishDTO createDishDTO)
    {
        var dishId = dishService.CreateDish(restaurantId, createDishDTO);
        return CreatedAtAction(nameof(GetDishByIdRoute), new {restaurantId = restaurantId, dishId = dishId }, null);
    }
}