using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI;

public class RestaurantMappingProfile : Profile
{
    public RestaurantMappingProfile()
    {
        // Need to explicitly define mappings for address fields since it is flattened from Address entity in RestaurantDTO.
        CreateMap<Restaurant, RestaurantDTO>()
            .ForMember(restaurantDTO => restaurantDTO.City, 
                memberConfigurationExpression => memberConfigurationExpression.MapFrom(restaurant => restaurant.Address.City))
            .ForMember(restaurantDTO => restaurantDTO.Street, 
                memberConfigurationExpression => memberConfigurationExpression.MapFrom(restaurant => restaurant.Address.Street))
            .ForMember(restaurantDTO => restaurantDTO.ZipCode, 
                memberConfigurationExpression => memberConfigurationExpression.MapFrom(restaurant => restaurant.Address.ZipCode));
        
        CreateMap<Dish, DishDTO>();

        CreateMap<CreateRestaurantDTO, Restaurant>()
            .ForMember(restaurant => restaurant.Address, 
                memberConfigurationExpression => memberConfigurationExpression.MapFrom(
                    createRestaurantDTO => new Address { City = createRestaurantDTO.City, Street = createRestaurantDTO.Street, ZipCode = createRestaurantDTO.ZipCode }));
    }
}