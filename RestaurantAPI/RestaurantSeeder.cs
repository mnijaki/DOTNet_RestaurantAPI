using RestaurantAPI.Entities;

namespace RestaurantAPI;

public class RestaurantSeeder(RestaurantDbContext dbContext)
{
    public void Seed()
    {
        if (dbContext.Database.CanConnect())
        {
            if (IsRestaurantDatabaseNotSeeded())
            {
                dbContext.Restaurants.AddRange(GetRestaurants());
                dbContext.SaveChanges();
            }
        }
    }

    private bool IsRestaurantDatabaseNotSeeded()
    {
        return !dbContext.Restaurants.Any();
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
         var restaurants = new List<Restaurant>
         {
             new()
             {
                 Name = "KFC", 
                 Category = "Fast Food", 
                 Description = "Best pizza in town!", 
                 Email = "asd@gmail.com", 
                 PhoneNumber = "123456789",
                 HasDelivery = true,
                 Dishes =
                 [
                     new Dish
                     {
                         Name = "Nashville Hot Chicken",
                         Price = 10.30M,
                         Description = "Very good chicken"
                     },

                     new Dish
                     {
                         Name = "Chicken Nuggets",
                         Price = 5.30M,
                         Description = "Very good chicken nuggets"
                     }

                 ],
                 Address = new Address()
                 {
                     City = "Kraków", 
                     Street = "Long Street 5",
                     ZipCode = "30-001"
                 }
             },
             new()
             {
                 Name = "Pizza Hut", 
                 Category = "Fast Food", 
                 Description = "Best pizza in town!", 
                 Email = "pizzahut@gmail.com", 
                 PhoneNumber = "987654321",
                 HasDelivery = true,
                 Dishes =
                 [
                     new Dish
                     {
                         Name = "Soup",
                         Price = 10.00M,
                         Description = "Very good soup"
                     },

                     new Dish
                     {
                         Name = "Potatoes",
                         Price = 13.00M,
                         Description = "Very good potatoes"
                     }

                 ],
                 Address = new Address()
                 {
                     City = "Kraków", 
                     Street = "Short Street 3",
                     ZipCode = "20-001"
                 }
             }
         };
         return restaurants;
    }
}