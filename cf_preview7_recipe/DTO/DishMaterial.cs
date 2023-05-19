using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cf_preview7_recipe.DTO
{
    public class DishMaterial
    {
        [Key]
        [Required]
        [StringLength(5)]
        public string Id { get; set; }
        public int Number { get; set; }
        public string Unit { get; set; }
        public int DishId { get; set; }
        [ForeignKey("DishId")]
        public virtual Dish Dish { get; set; }
        public int MaterialId { get; set; }
        [ForeignKey("MaterialId")]
        public virtual Material Material { get; set; }
    }
}
