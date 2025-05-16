using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRestaurantWpf.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public decimal UnitPriceAtOrder { get; set; }
        public decimal Subtotal { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int? DishId { get; set; }
        public Dish Dish { get; set; }

        public int? MenuId { get; set; }
        public Menu Menu { get; set; }
    }
}
