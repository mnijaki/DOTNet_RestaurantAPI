using RestaurantAPI.Models;

namespace RestaurantAPI.Services;

public interface IDishService
{
    int CreateDish(CreateDishDTO createDishDTO, int restaurantId);
}