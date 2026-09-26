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
    public ActionResult<DishDTO> GetDishByIdRoute([FromRoute]int restaurantId, [FromRoute]int dishId)
    {
        DishDTO dishDTO = dishService.GetDishById(restaurantId, dishId);
        return Ok(dishDTO);
    }
    
    [HttpGet("GetDishById")]
    public ActionResult<DishDTO> GetDishByIdQuery([FromRoute]int restaurantId, [FromQuery] int dishId)
    {
        DishDTO dishDTO = dishService.GetDishById(restaurantId, dishId);
        return Ok(dishDTO);
    }
    
    [HttpPost]
    public ActionResult<DishDTO> CreateDish([FromRoute] int restaurantId, [FromBody] CreateDishDTO createDishDTO)
    {
        var dishId = dishService.CreateDish(restaurantId, createDishDTO);
        return CreatedAtAction(nameof(GetDishByIdRoute), new {restaurantId = restaurantId, dishId = dishId }, null);
    }
    
    [HttpPut("{dishId:int}")]
    public IActionResult UpdateDish([FromRoute] int restaurantId, [FromRoute] int dishId, [FromBody] UpdateDishDTO updateDishDTO)
    {
        dishService.UpdateDish(restaurantId, dishId, updateDishDTO);
        return NoContent();
    }
    
    [HttpDelete("DeleteAllDishesByRestaurantId")]
    public IActionResult DeleteAllDishesByRestaurantId([FromRoute] int restaurantId)
    {
        dishService.DeleteAllDishesByRestaurantId(restaurantId);
        return NoContent();
    }
    
    [HttpDelete("{dishId:int}")]
    public IActionResult DeleteDish([FromRoute]int restaurantId, [FromRoute] int dishId)
    {
        dishService.DeleteDish(restaurantId, dishId);
        return NoContent();
    }
}