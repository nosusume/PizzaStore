using Microsoft.EntityFrameworkCore;
using PizzaStore.DB;
using PizzaStore.Models;

namespace PizzaStore.Services;

public class PizzaService
{
    private readonly PizzaContext _context;
    
    public PizzaService(PizzaContext context)
    {
        _context = context;
    }

    public IEnumerable<Pizza> GetAll() { 
        return _context.Pizzas.AsNoTracking().ToList();
    }

    public Pizza? GetById(int id) {
        return _context.Pizzas
        .Include(p => p.Sauce)
        .Include(p => p.Toppings)
        .AsNoTracking()
        .SingleOrDefault(p => p.Id == id);
    }

    public Pizza Create(Pizza pizza) {
        _context.Pizzas.Add(pizza);
        _context.SaveChanges();
        return pizza;
    }

    public void AddTopping(int pizzaId, int toppingId)
    {
        var pizzaToUpdate = _context.Pizzas.Find(pizzaId);
        var toppingToAdd = _context.Toppings.Find(toppingId);

        if (pizzaToUpdate is null || toppingToAdd is null)
        {
            throw new InvalidOperationException("Pizza or Topping not found");
        }

        if (pizzaToUpdate.Toppings is null)
        {
            pizzaToUpdate.Toppings = new List<Topping>();
        }

        pizzaToUpdate.Toppings.Add(toppingToAdd);
        _context.SaveChanges();
    }

    public void UpdateSauce(int pizzaId, int sauceId)
    {
        var pizzaToUpdate = _context.Pizzas.Find(pizzaId);
        var sauceToUpdate = _context.Sauces.Find(sauceId);

        if (pizzaToUpdate is null || sauceToUpdate is null)
        {
            throw new InvalidOperationException("Pizza or Sauce not found");
        }

        pizzaToUpdate.Sauce = sauceToUpdate;
        _context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var pizza = _context.Pizzas.Find(id);
        if (pizza is not null)
        {
            _context.Pizzas.Remove(pizza);
            _context.SaveChanges();
        }
    }
}