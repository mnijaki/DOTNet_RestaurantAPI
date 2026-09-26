using RestaurantAPI.Models;

namespace RestaurantAPI.Services;

public interface IDishService
{
    IEnumerable<DishDTO> GetAllDishes(int restaurantId);
    DishDTO GetDishById(int restaurantId, int dishId);
    int CreateDish(int restaurantId, CreateDishDTO createDishDTO);
    void UpdateDish(int restaurantId, int dishId, UpdateDishDTO updateDishDTO);
    void DeleteAllDishesByRestaurantId(int restaurantId);
    void DeleteDish(int restaurantId, int dishId);
}