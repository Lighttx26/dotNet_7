using cf_preview7_recipe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cf_preview7_recipe
{
    public partial class Detailsform : Form
    {
        public delegate void MyDelegate();
        public MyDelegate ReloadMainform { get; set; }

        private readonly string _Id;
        private readonly int _dishid;
        //private readonly int _materialid;
        public Detailsform(int dishid, string id = "")
        {
            InitializeComponent();

            this._Id = id;
            this._dishid = dishid;
            //this._materialid = materialid;
        }

        private void Detailsform_Load(object sender, EventArgs e)
        {
            cbbMaterialName.Items.Clear();
            cbbMaterialName.Items.AddRange(BLL.BLL.Instance.GetAllMaterials().ToArray());

            cbbStatus.Items.Clear();
            cbbStatus.Items.AddRange(new ItemCBB[]
            {
                new ItemCBB {Value = 1, Text = "Da nhap hang"},
                new ItemCBB {Value = 0, Text = "Chua nhap hang"},
            });

            cbbUnit.Items.Clear();
            cbbUnit.Items.AddRange(new String[] { "g", "kg", "ml", "l", "qua", "cu" });
            // Edit
            if (_Id.Length > 0)
            {
                var dm = BLL.BLL.Instance.GetDishMaterial(_Id);

                cbbMaterialName.SelectedIndex = cbbMaterialName.FindStringExact(dm.MaterialName);
                tbNumber.Text = dm.Number.ToString();
                cbbUnit.Text = dm.Unit;
                cbbStatus.Text = dm.Status == true ? "Da nhap hang" : "Chua nhap hang";
            }

            // Add
            else if (_Id.Length == 0)
            {
                cbbStatus.Enabled = false;
            }


        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_Id.Length > 0)
            {
                EditHandler();
            }

            else if (_Id.Length == 0)
            {
                AddHandler();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void EditHandler()
        {
            try
            {
                var dm = BLL.BLL.Instance.GetDishMaterial(this._Id);
                //
                int materialid = ((ItemCBB)cbbMaterialName.SelectedItem).Value;

                if (dm.MaterialId != materialid)
                    if (BLL.BLL.Instance.IsExistDishMaterial(_dishid, materialid))
                        throw new Exception("Mon nay da su dung nguyen lieu nay");
                    
                BLL.BLL.Instance.ChangeStatusMaterial(materialid, Convert.ToBoolean(((ItemCBB)cbbStatus.SelectedItem).Value));

                //
                DishMaterial newdm = new DishMaterial
                {
                    Id = _Id,
                    Number = Convert.ToInt32(tbNumber.Text),
                    Unit = cbbUnit.Text,
                    MaterialId = ((ItemCBB)cbbMaterialName.SelectedItem).Value,
                    DishId = _dishid,
                };
                BLL.BLL.Instance.UpdateDishMaterial(newdm);

                ReloadMainform();
                this.Dispose();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddHandler()
        {
            try
            {
                int materialid = ((ItemCBB)cbbMaterialName.SelectedItem).Value;
                //
                if (!BLL.BLL.Instance.IsExistDishMaterial(_dishid, materialid))
                {
                    DishMaterial dm = new DishMaterial
                    {
                        Id = _dishid.ToString() + materialid.ToString(),
                        DishId = _dishid,
                        MaterialId = materialid,
                        Number = Convert.ToInt32(tbNumber.Text),
                        Unit = cbbUnit.Text,
                    };
                    BLL.BLL.Instance.AddDishMaterial(dm);
                }

                else throw new Exception("Mon nay da su dung nguyen lieu nay");

                ReloadMainform();
                this.Dispose();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckInput()
        {

        }

        private void cbbMaterialName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbStatus.Text = BLL.BLL.Instance.GetMaterial(((ItemCBB)cbbMaterialName.SelectedItem).Value).Status == true ? "Da nhap hang" : "Chua nhap hang";
        }
    }
}
