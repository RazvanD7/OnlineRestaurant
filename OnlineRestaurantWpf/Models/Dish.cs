using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineRestaurantWpf.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PortionQuantity { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalQuantity { get; set; }

        public string Unit { get; set; } = string.Empty;

        public string? Description { get; set; }
        public bool IsAvailable { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<DishAllergen> DishAllergens { get; set; }
        public ICollection<DishImage> Images { get; set; }
        public ICollection<MenuDish> MenuDishes { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

        public Dish()
        {
            DishAllergens = new HashSet<DishAllergen>();
            Images = new List<DishImage>();
            MenuDishes = new HashSet<MenuDish>();
            OrderItems = new HashSet<OrderItem>();
            IsAvailable = true;
            Unit = "g"; // Default unit, can be changed
        }
    }
}