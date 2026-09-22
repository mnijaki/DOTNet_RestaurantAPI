using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services;

public interface IRestaurantService
{
    IEnumerable<RestaurantDTO> GetAllRestaurants();
    RestaurantDTO? GetRestaurantById(int id);
    Restaurant CreateRestaurant(CreateRestaurantDTO createRestaurantDTO);
    public void UpdateRestaurant(int id, UpdateRestaurantDTO updateRestaurantDTO);
    void DeleteRestaurant(int id);
}