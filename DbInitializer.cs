using PizzaStore.Models;

namespace PizzaStore.DB
{
    public static class DbInitializer
    {
        public static void Initializer(PizzaContext context)
        {
            if (context.Pizzas.Any() && context.Sauces.Any() && context.Toppings.Any())
            {
                // DB has been seeded
                return;
            }

            var pepperoniTopping = new Topping{Name = "Pepperoni", Calories = 100};
            var sausageTopping = new Topping{Name = "Sausage", Calories = 200};
            var cheeseTopping = new Topping{Name = "Cheese", Calories = 300};
            var chickenTopping = new Topping{Name = "Chicken", Calories = 400};
            var baconTopping = new Topping{Name = "Bacon", Calories = 500};
            var tomatoSauce = new Sauce{Name = "Tomato", IsVegan = true};
            var alfredoSauce = new Sauce{Name = "Alfredo", IsVegan = false};

            var pizzas = new Pizza[]
            {
                new Pizza{Name = "Pepperoni", Sauce = tomatoSauce, Toppings = new Topping[]{pepperoniTopping}},
                new Pizza{Name = "Sausage", Sauce = tomatoSauce, Toppings = new Topping[]{sausageTopping}},
                new Pizza{Name = "Cheese", Sauce = tomatoSauce, Toppings = new Topping[]{cheeseTopping}},
                new Pizza{Name = "Chicken", Sauce = alfredoSauce, Toppings = new Topping[]{chickenTopping}},
                new Pizza{Name = "Bacon", Sauce = alfredoSauce, Toppings = new Topping[]{baconTopping}}
            };

            context.Pizzas.AddRange(pizzas);
            context.SaveChanges();
        }
    }

    public static class Extensions
    {
        public static void CreateDbIfNotExists(this IHost host) {
            {
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<PizzaContext>();
                                        context.Database.EnsureCreated();
                    DbInitializer.Initializer(context);
                }
            }
        }
    }
}