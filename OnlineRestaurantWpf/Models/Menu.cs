using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineRestaurantWpf.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<MenuDish> MenuDishes { get; set; }

        [NotMapped]
        public string FirstDishImagePath { get; set; }

        public Menu()
        {
            MenuDishes = new HashSet<MenuDish>();
            IsAvailable = true;
        }
    }
}
