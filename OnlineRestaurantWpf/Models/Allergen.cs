using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRestaurantWpf.Models
{
    public class Allergen
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<DishAllergen> DishAllergens { get; set; }

        public Allergen()
        {
            DishAllergens = new HashSet<DishAllergen>();
        }
    }
}
