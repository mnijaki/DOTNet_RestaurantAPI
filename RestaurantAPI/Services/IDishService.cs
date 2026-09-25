using RestaurantAPI.Models;

namespace RestaurantAPI.Services;

public interface IDishService
{
    IEnumerable<DishDTO> GetAllDishes(int restaurantId);
    DishDTO GetDishById(int dishId);
    int CreateDish(int restaurantId, CreateDishDTO createDishDTO);
}