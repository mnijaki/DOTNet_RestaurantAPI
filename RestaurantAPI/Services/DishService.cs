using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services;

public class DishService(RestaurantDbContext context, IMapper mapper) : IDishService
{
    public int CreateDish(CreateDishDTO createDishDTO, int restaurantId)
    {
        Restaurant? restaurant = context.Restaurants.FirstOrDefault(restaurant => restaurant.Id == restaurantId);
        if (restaurant == null)
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