using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;

namespace PizzaStore.DB;

public class PizzaContext : DbContext
{
    public DbSet<Pizza> Pizzas => Set<Pizza>();
    public DbSet<Sauce> Sauces => Set<Sauce>();
    public DbSet<Topping> Toppings => Set<Topping>();

    public PizzaContext(DbContextOptions<PizzaContext> options) : base(options)
    {
    }
}