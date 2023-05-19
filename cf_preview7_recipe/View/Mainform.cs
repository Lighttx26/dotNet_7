using cf_preview7_recipe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations.Model;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cf_preview7_recipe
{

    public partial class Mainform : Form
    {
        private List<ItemDGV> datasource;
        public Mainform()
        {
            InitializeComponent();

            dgv.Columns.Add("#", "STT");
        }

        private void Mainform_Load(object sender, EventArgs e)
        {
            ReloadDishCB();
            ReloadSortCB();
            //ReloadDGV();
        }

        #region Event Handler
        private void cbbDish_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadDGV();
            RenameColumn();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            dgv.DataSource = BLL.BLL.Instance.GetMaterialsInDishBySearch(datasource, tbSearch.Text);
            RenameColumn();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int dishid = ((ItemCBB)cbbDish.SelectedItem).Value;
            Detailsform df = new Detailsform(dishid);
            df.ReloadMainform = new Detailsform.MyDelegate(this.ReloadDGV);
            df.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count != 1)
            {
                MessageBox.Show("Chon 1 hang de thuc hien");
            }

            else
            {
                string id = dgv.SelectedRows[0].Cells["Id"].Value.ToString();
                int dishid = ((ItemCBB)cbbDish.SelectedItem).Value;
                //int materialid = Convert.ToInt32(dgv.SelectedRows[0].Cells["MaterialId"].Value.ToString());
                Detailsform dform = new Detailsform(dishid, id);
                dform.ReloadMainform = new Detailsform.MyDelegate(this.ReloadDGV); // Ham this.Reload() se duoc thuc thi khi MyDelegate DetailsForm duoc goi
                dform.Show();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count != 1)
            {
                MessageBox.Show("Chon 1 hang de thuc hien");
            }

            else
            {
                if (Convert.ToBoolean(dgv.SelectedRows[0].Cells["Status"].Value) == true)
                {
                    MessageBox.Show("Khong the xoa dong nay: nguyen lieu con ton tai");
                    return;
                }

                if (MessageBox.Show("Bạn muốn xóa dòng này?", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {                   
                        string id = dgv.SelectedRows[0].Cells["Id"].Value.ToString();
                        BLL.BLL.Instance.DeleteDishMaterial(id);  
                }
            }

            ReloadDGV();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {

        }

        #endregion

        private void ReloadDGV()
        {
            this.datasource = BLL.BLL.Instance.GetMaterialsInDish(((ItemCBB)cbbDish.SelectedItem).Value);
            dgv.DataSource = datasource;
            RenameColumn();
        }

        private void ReloadDishCB()
        {
            cbbDish.Items.Clear();
            cbbDish.Items.AddRange(BLL.BLL.Instance.GetAllDishes().ToArray());
            cbbDish.SelectedIndex = 0;
        }

        private void ReloadSortCB()
        {

        }

        private void RenameColumn()
        {
            dgv.Columns["Id"].Visible = false;
            dgv.Columns["MaterialId"].Visible = false;
            // Doi ten hien thi cac cot
            dgv.Columns["MaterialName"].HeaderText = "Tên nguyên liệu";
            dgv.Columns["Number"].HeaderText = "Số lượng";
            dgv.Columns["Unit"].HeaderText = "Đơn vị tính";
            dgv.Columns["Status"].HeaderText = "Tình trạng";
        }

        private void dgv_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dgv.Rows[e.RowIndex].Cells["#"].Value = (e.RowIndex + 1).ToString();
        }
    }
}
