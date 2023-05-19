using cf_preview7_recipe.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cf_preview7_recipe.BLL
{
    internal class BLL
    {
        private static BLL _instance;
        public static BLL Instance
        {
            get
            {
                if (_instance == null) _instance = new BLL();
                return _instance;
            }

            private set { }
        }

        public List<ItemCBB> GetAllDishes()
        {
            try
            {
                List<ItemCBB> list = new List<ItemCBB>();

                foreach (Dish dish in DAL.DAL.Instance.GetAllDishes())
                {
                    list.Add(new ItemCBB { Value = dish.DishId, Text = dish.DishName });
                }

                return list;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in BLL.GetAllDishes: " + ex.Message);
                return null;
            }
        }

        public List<ItemDGV> GetMaterialsInDish(int dishid) 
        {
            try
            {
                return DAL.DAL.Instance.GetMaterialsInDish(dishid);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in BLL.GetMaterialsInDish: " + ex.Message);
                return null;
            }
        }

        public List<ItemDGV> GetMaterialsInDishBySearch(List<ItemDGV> list, string searchtxt)
        {
            try
            {
                return list.Where(dm => dm.MaterialName.Contains(searchtxt)
                                     || dm.Unit.Contains(searchtxt)
                                     || dm.Number.ToString().Contains(searchtxt))
                           .ToList();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in BLL.GetMaterialsInDishBySearch: " + ex.Message);
                return null;
            }
        }

        public ItemDGV GetDishMaterial(string Id)
        {
            try
            {
                return DAL.DAL.Instance.GetDishMaterial(Id);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in BLL.GetMaterialsInDishBySearch: " + ex.Message);
                return null;
            }
        }

        public List<ItemCBB> GetAllMaterials()
        {
            try
            {
                List<ItemCBB> list = new List<ItemCBB>();

                foreach (Material m in DAL.DAL.Instance.GetAllMaterials())
                {
                    list.Add(new ItemCBB
                    {
                        Value = m.MaterialId,
                        Text = m.MaterialName,
                    });
                }

                return list;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in BLL.GetAllMaterials: " + ex.Message);
                return null;
            }
        }

        public bool IsExistDishMaterial(int dishid, int materialid)
        {
            try
            {
                return DAL.DAL.Instance.IsExistDishMaterial(dishid, materialid);
            }

            catch (Exception ex)
            {
                throw new Exception("Errorr in BLL.IsExistDishMaterial: " + ex.Message);
            }
        }

        public Material GetMaterial(int materialid)
        {
            try
            {
                return DAL.DAL.Instance.GetMaterial(materialid);
            }

            catch (Exception ex)
            {
                throw new Exception("Errorr in BLL.GetMaterial: " + ex.Message);
            }
        }

        public void AddDishMaterial(DishMaterial dm)
        {
            try
            {
                DAL.DAL.Instance.AddDishMaterial(dm);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in BLL.AddDishMaterial: " + ex.Message);
            }
        }

        public void ChangeStatusMaterial(int materialid, bool status)
        {
            try
            {
                DAL.DAL.Instance.ChangeStatusMaterial(materialid, status);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in BLL.ChangeStatusMaterial: " + ex.Message);

            }
        }

        public void UpdateDishMaterial(DishMaterial dishmaterial)
        {
            try
            {
                DAL.DAL.Instance.UpdateDishMaterial(dishmaterial);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in BLL.UpdateDishMaterial: " + ex.Message);

            }
        }

        public void DeleteDishMaterial(string id)
        {
            try
            {
                DAL.DAL.Instance.DeleteDishMaterial(id);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in BLL.DeleteDishMaterial: " + ex.Message);

            }
        }
    }
}
