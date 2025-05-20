using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineRestaurantWpf.Models
{
    public class MenuDish
    {
        public int MenuId { get; set; }
        public Menu Menu { get; set; } = null!;

        public int DishId { get; set; }
        public Dish Dish { get; set; } = null!;

        [Column(TypeName = "decimal(10,2)")]
        public decimal QuantityInMenu { get; set; }
    }
}