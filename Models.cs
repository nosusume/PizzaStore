namespace PizzaStore.Models;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
public record Pizza
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }

    public Sauce? Sauce { get; set; }

    public ICollection<Topping>? Toppings { get; set; }

}

public class Sauce
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }

    public bool IsVegan { get; set; }
}

public class Topping
{
    public int Id {get; set;}
    [Required]
    [MaxLength(100)]
    public string? Name {get; set;}
    public decimal Calories {get; set;}
    [JsonIgnore]
    public ICollection<Pizza>? Pizzas {get; set;}
}