using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services;

public class DishService(RestaurantDbContext context, IMapper mapper) : IDishService
{
    public IEnumerable<DishDTO> GetAllDishes(int restaurantId)
    {
        var dishes = context.Dishes.Where(dish => dish.RestaurantId == restaurantId);
            
        var dishesDTO = mapper.Map<IEnumerable<DishDTO>>(dishes);
        return dishesDTO;
    }

    public DishDTO GetDishById(int dishId)
    {
        Dish? dish = context.Dishes.FirstOrDefault(dish => dish.Id == dishId);
        if (dish is null)
        {
            throw new NotFoundException($"Dish with id [{dishId}] not found");
        }

        return mapper.Map<DishDTO>(dish);
    }
    
    public int CreateDish(int restaurantId, CreateDishDTO createDishDTO)
    {
        Restaurant? restaurant = context.Restaurants.FirstOrDefault(restaurant => restaurant.Id == restaurantId);
        if (restaurant is null)
        {
            throw new NotFoundException($"Restaurant with id [{restaurantId}] not found");
        }

        var dish = mapper.Map<Dish>(createDishDTO);
        dish.RestaurantId = restaurantId;
        context.Dishes.Add(dish);
        context.SaveChanges();
        
        return dish.Id;
    }
}