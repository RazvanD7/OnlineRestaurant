using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRestaurantWpf.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public DateTime? EstimatedDeliveryTime { get; set; }

        public decimal ProductsCost { get; set; }
        public decimal AppliedDiscount { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalCost { get; set; }

        public string DeliveryAddressForOrder { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<OrderItem> Items { get; set; }

        public Order()
        {
            Items = new HashSet<OrderItem>();
            OrderDate = DateTime.Now;
            AppliedDiscount = 0;
            ShippingCost = 0;
        }
    }
}
