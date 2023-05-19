using cf_preview7_recipe.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace cf_preview7_recipe.DAL
{
    internal class DAL
    {
        private static DAL _instance;
        public static DAL Instance
        {
            get
            {
                if (_instance == null) _instance = new DAL();
                return _instance;
            }

            private set { }
        }

        public List<Dish> GetAllDishes()
        {
            List<Dish> dishes = new List<Dish>();
            using (Model model = new Model())
            {
                dishes = model.Dishes.ToList();
            }
            return dishes;
        }

        public List<ItemDGV> GetMaterialsInDish(int dishid) 
        {
            List<ItemDGV> list = new List<ItemDGV>();
            using (Model model = new Model())
            {
                var tlist = model.DishesMaterials.Include(dm => dm.Material)
                    .Where(dm => dm.DishId == dishid)
                    .Select(dm => new
                    {
                        Id = dm.Id,
                        MaterialId = dm.MaterialId,
                        MaterialName = dm.Material.MaterialName,
                        Number = dm.Number,
                        Unit = dm.Unit,
                        Status = dm.Material.Status,
                    })
                    .ToList();
                
                foreach (var item in tlist)
                {
                    list.Add(new ItemDGV
                    {
                        Id = item.Id,
                        MaterialId = item.MaterialId,
                        MaterialName = item.MaterialName,
                        Number = item.Number,
                        Unit = item.Unit,
                        Status = item.Status,
                    });
                }
            }
            return list;
        }

        public ItemDGV GetDishMaterial(string id)
        {
            ItemDGV _dm;
            
            using (Model model = new Model())
            {
                var item = model.DishesMaterials.Include(dm => dm.Material)
                    .Where(dm => dm.Id == id)
                    .Select(dm => new
                    {
                        Id = dm.Id,
                        MaterialId = dm.MaterialId,
                        MaterialName = dm.Material.MaterialName,
                        Number = dm.Number,
                        Unit = dm.Unit,
                        Status = dm.Material.Status,
                    }).First();

                _dm = new ItemDGV
                {
                    Id = item.Id,
                    MaterialId = item.MaterialId,
                    MaterialName = item.MaterialName,
                    Number = item.Number,
                    Unit = item.Unit,
                    Status = item.Status,
                };
            }

            return _dm;
        }

        public List<Material> GetAllMaterials()
        {
            List<Material> materials = new List<Material>();
            using (Model model = new Model())
            {
                materials = model.Materials.ToList();
            }
            return materials;
        }

        public bool IsExistDishMaterial(int dishid, int materialid)
        {
            using (Model model = new Model())
            {
                return model.DishesMaterials.Any(dm => dm.DishId == dishid && dm.MaterialId == materialid);
            }
        }

        public Material GetMaterial(int materialid)
        {
            using (Model model = new Model())
            {
                return model.Materials.FirstOrDefault(dm => dm.MaterialId == materialid);
            }
        }

        public void AddDishMaterial(DishMaterial dishmaterial)
        {
            using (Model model = new Model())
            {
                model.DishesMaterials.Add(dishmaterial);
                model.SaveChanges();
            }
        }

        public void UpdateDishMaterial(DishMaterial dishmaterial)
        {
            using (Model model = new Model())
            {
                DishMaterial _dm = model.DishesMaterials.First(dm => dm.Id == dishmaterial.Id);
                _dm.MaterialId = dishmaterial.MaterialId;
                _dm.Number = dishmaterial.Number;
                _dm.Unit = dishmaterial.Unit;
                    
                model.SaveChanges();
            }
        }

        public void ChangeStatusMaterial(int materialid, bool status)
        {
            using (Model model = new Model())
            {
                Material _m = model.Materials.First(m => m.MaterialId == materialid);
                _m.Status = status;

                model.SaveChanges();
            }
        }

        public void DeleteDishMaterial(string id)
        {
            using (Model model = new Model())
            {
                model.DishesMaterials.Remove(model.DishesMaterials.First(dm => dm.Id == id));
                model.SaveChanges();
            }
        }
    }
}
