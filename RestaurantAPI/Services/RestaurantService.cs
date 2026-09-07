using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services;

public class RestaurantService(RestaurantDbContext context, IMapper mapper) : IRestaurantService
{
    public IEnumerable<RestaurantDTO> GetAllRestaurants()
    {
        // Entity Framework will create under the hood a proper SQL query to get all Restaurants and return it under 'Restaurants' property.
        // Add the necessary other tables to the result of the SQL query.
        var restaurants = context.Restaurants.
            Include(restaurant => restaurant.Address)
            .Include(restaurant => restaurant.Dishes)
            .ToList();    
        
        // Map Restaurant entity to RestaurantDTO.
        var restaurantsDTO = mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
        return restaurantsDTO;
    }
    
    public RestaurantDTO? GetRestaurantById(int id)
    {
        var foundRestaurant = context.Restaurants
            .Include(restaurant => restaurant.Address)
            .Include(restaurant => restaurant.Dishes)
            .FirstOrDefault(restaurant => restaurant.Id == id);
        if (foundRestaurant is null)
        {
            return null;
        }
        
        var restaurantDTO = mapper.Map<RestaurantDTO>(foundRestaurant);
        return restaurantDTO;
    }
    
    public Restaurant CreateRestaurant(CreateRestaurantDTO createRestaurantDTO)
    {
        var restaurant = mapper.Map<Restaurant>(createRestaurantDTO);
        context.Restaurants.Add(restaurant);
        context.SaveChanges();

        return restaurant;
    }

    public bool UpdateRestaurant(int id, UpdateRestaurantDTO updateRestaurantDTO)
    {
        var restaurant = context.Restaurants.Find(id);
        if (restaurant is null)
        {
            return false;
        }
        
        restaurant.Name = updateRestaurantDTO.Name;
        restaurant.Description = updateRestaurantDTO.Description;
        restaurant.HasDelivery = updateRestaurantDTO.HasDelivery;
        context.SaveChanges();
        
        return true;
    }

    public bool DeleteRestaurant(int id)
    {
        var restaurant = context.Restaurants.Find(id);
        if (restaurant is null)
        {
            return false;
        }
        
        context.Restaurants.Remove(restaurant);
        context.SaveChanges();
        
        return true;
    }
}