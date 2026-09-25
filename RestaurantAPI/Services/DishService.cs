using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services;

public class DishService(RestaurantDbContext context, IMapper mapper) : IDishService
{
    public IEnumerable<DishDTO> GetAllDishes(int restaurantId)
    {
        Restaurant restaurant = GetRestaurantById(restaurantId);
        var dishesDTO = mapper.Map<IEnumerable<DishDTO>>(restaurant.Dishes);
        return dishesDTO;
    }

    public DishDTO GetDishById(int restaurantId, int dishId)
    {
        Restaurant restaurant = GetRestaurantById(restaurantId);
        if (restaurant.Dishes is null)
        {
            throw new NotFoundException($"Restaurant with id [{restaurantId}] has no dishes");
        }

        Dish? dish = restaurant.Dishes.FirstOrDefault(dish => dish.Id == dishId);
        if (dish is null)
        {
            throw new NotFoundException($"Dish with id [{dishId}] not found for restaurant with id [{restaurantId}]");
        }

        return mapper.Map<DishDTO>(dish);
    }
    
    public int CreateDish(int restaurantId, CreateDishDTO createDishDTO)
    {
        CheckIfRestaurantExists(restaurantId);

        var dish = mapper.Map<Dish>(createDishDTO);
        dish.RestaurantId = restaurantId;
        context.Dishes.Add(dish);
        context.SaveChanges();
        
        return dish.Id;
    }

    public void DeleteAllDishesByRestaurantId(int restaurantId)
    {
        Restaurant restaurant = GetRestaurantById(restaurantId);
        if (restaurant.Dishes is null)
        {
            throw new NotFoundException($"Restaurant with id [{restaurantId}] has no dishes");
        }
        
        context.Dishes.RemoveRange(restaurant.Dishes);
        context.SaveChanges();
    }

    public void DeleteDish(int restaurantId, int dishId)
    {
        Restaurant restaurant = GetRestaurantById(restaurantId);
        if (restaurant.Dishes is null)
        {
            throw new NotFoundException($"Restaurant with id [{restaurantId}] has no dishes");
        }
        
        var dish = restaurant.Dishes.FirstOrDefault(dish => dish.Id == dishId);
        if (dish is null)
        {
            throw new NotFoundException($"Dish with id [{dishId}] not found for restaurant with id [{restaurantId}]");
        }

        context.Dishes.Remove(dish);
        context.SaveChanges();
    }
    
    private Restaurant GetRestaurantById(int restaurantId)
    {
        Restaurant? restaurant = context.Restaurants
            .Include(restaurant => restaurant.Dishes)
            .FirstOrDefault(restaurant => restaurant.Id == restaurantId);
        if (restaurant is null)
        {
            throw new NotFoundException($"Restaurant with id [{restaurantId}] not found");
        }

        return restaurant;
    }
    
    private void CheckIfRestaurantExists(int restaurantId)
    {
        GetRestaurantById(restaurantId);
    }
}