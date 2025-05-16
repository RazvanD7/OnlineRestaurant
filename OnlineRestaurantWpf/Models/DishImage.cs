using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRestaurantWpf.Models
{
    public class DishImage
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public bool IsMain { get; set; }

        public int DishId { get; set; }
        public Dish Dish { get; set; }

        public DishImage()
        {
            IsMain = false;
        }
    }
}
