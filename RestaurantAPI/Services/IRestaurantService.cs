using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services;

public interface IRestaurantService
{
    IEnumerable<RestaurantDTO> GetAllRestaurants();
    RestaurantDTO? GetRestaurantById(int id);
    Restaurant CreateRestaurant(CreateRestaurantDTO createRestaurantDTO);
    public bool UpdateRestaurant(int id, UpdateRestaurantDTO updateRestaurantDTO);
    bool DeleteRestaurant(int id);
}