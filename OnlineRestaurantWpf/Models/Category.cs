using System.Collections.Generic;

namespace OnlineRestaurantWpf.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Dish> Dishes { get; set; }
        public ICollection<Menu> Menus { get; set; }

        public Category()
        {
            Dishes = new HashSet<Dish>();
            Menus = new HashSet<Menu>();
        }
    }
}