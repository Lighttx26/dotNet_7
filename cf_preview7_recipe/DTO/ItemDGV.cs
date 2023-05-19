using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cf_preview7_recipe.DTO
{
    internal class ItemDGV
    {
        public string Id { get; set; }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public int Number { get; set; }
        public string Unit { get; set; }
        public bool Status { get; set; }
    }
}
