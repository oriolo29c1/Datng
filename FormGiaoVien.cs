using BTL.FORM_TIM_KIEM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BTL; 

namespace BTL
{
    public partial class FormGiaoVien : Form
    {
        sql SQL = new sql();
        public FormGiaoVien()
        {
            InitializeComponent();
        }
        private void ResetForm()
        {
            txtMGV.Text = "";
            txtNameGV.Text = "";
            txtAddress.Text = "";
            dateTimePicker1.Value = dateTimePicker1.MaxDate;
            txtPhoneNumber.Text = "";
            txtCM.Text = "";
            cbGiaoVien.SelectedIndex = 0;
        }
        
        private void reloadGV()
        {
            SQL.Connect();
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            dgvGV.DataSource = SQL.datatable("SELECT * FROM GiaoVien");
            SQL.Close();
        }
        private void FormGiaoVien_Load(object sender, EventArgs e)
        {
            reloadGV();
            sql db = new sql();  
            DataTable dtGiaoVien = db.datatable("SELECT * FROM GiaoVien");
            db.FillComboBox(cbGiaoVien, dtGiaoVien, "TenGiaoVien", "MaGiaoVien");
        }


        private void btnSearching_Click(object sender, EventArgs e)
        {
            if (cbGiaoVien.SelectedValue == null) return;

            string maGiaoVien = cbGiaoVien.SelectedValue.ToString();
            string query = $"SELECT MaLop, TenLop, SiSo, NamHoc FROM Lop WHERE MaGiaoVien = '{maGiaoVien}'";

            // Sử dụng class sql thay vì mở kết nối thủ công
            sql db = new sql();
            DataTable dtLopTheoGV = db.datatable(query);

            // Gọi phương thức FillDataGridView từ class sql
            db.FillDataGridView(dgvGV, dtLopTheoGV);
        }


        private void cbGiaoVien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string ma = txtMGV.Text.Trim();
            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Không được để trống mã giáo viên!");
                return;
            }
            if (!SQL.CheckPK("GiaoVien", new string[] { $"MaGiaoVien = '{ma}'" }))
            {
                MessageBox.Show("Trùng khóa chính!");
                return;
            }
            string hoTen = txtNameGV.Text.Trim();
            string ngaySinh = dateTimePicker1.Value.ToString();
            string diaChi = txtAddress.Text.Trim(); 
            string sodienThoai = txtPhoneNumber.Text.Trim();
            string chuyenMon = txtCM.Text.Trim();
            string query = $@"
                INSERT INTO GiaoVien (MaGiaoVien, TenGiaoVien,NgaySinh, DiaChi, SoDienThoai, ChuyenMon)
                VALUES ('{ma}', N'{hoTen}', N'{ngaySinh}', '{diaChi}', {sodienThoai}, N'{chuyenMon}')
            ";
            SQL.Connect();
            SQL.Insert(query);
            SQL.Close();
            reloadGV();
            ResetForm();

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string ma = txtMGV.Text.Trim();
            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Không được để trống mã giáo viên!");
                return;
            }

            string hoTen = txtNameGV.Text.Trim().Replace("'", "''");
            string ngaySinh = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"); // Định dạng SQL chuẩn
            string diaChi = txtAddress.Text.Trim().Replace("'", "''");
            string sodienThoai = txtPhoneNumber.Text.Trim().Replace("'", "''");
            string chuyenMon = txtCM.Text.Trim().Replace("'", "''");

            List<string> updates = new List<string>();

            if (!string.IsNullOrEmpty(hoTen)) updates.Add($"TenGiaoVien = N'{hoTen}'");
            if (!string.IsNullOrEmpty(ngaySinh)) updates.Add($"NgaySinh = '{ngaySinh}'");
            if (!string.IsNullOrEmpty(diaChi)) updates.Add($"DiaChi = N'{diaChi}'");
            if (!string.IsNullOrEmpty(sodienThoai)) updates.Add($"SoDienThoai = N'{sodienThoai}'");
            if (!string.IsNullOrEmpty(chuyenMon)) updates.Add($"ChuyenMon = N'{chuyenMon}'");

            if (updates.Count == 0)
            {
                MessageBox.Show("Không có thông tin nào để cập nhật!");
                return;
            }

            string query = $"UPDATE GiaoVien SET {string.Join(", ", updates)} WHERE MaGiaoVien = '{ma.Replace("'", "''")}'";

            SQL.Connect();
            try
            {
                int result = SQL.updateD(query);
                if (result > 0)
                    MessageBox.Show("Cập nhật thành công!");
                else
                    MessageBox.Show("Không tìm thấy giáo viên để cập nhật!");

                reloadGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message);
            }
            finally
            {
                SQL.Close();
            }

            ResetForm();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMGV.Text.Trim();
            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng nhập mã giáo viên cần xóa!");
                return;
            }
            SQL.Connect();
            try
            {
                string query = $"DELETE FROM GiaoVien WHERE MaGiaoVien = '{ma}'";
                int result = SQL.DeleteD(query);

                if (result > 0)
                {
                    MessageBox.Show("Xóa thành công!");
                    reloadGV();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy giáo viên cần xóa!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }

            SQL.Close();
            ResetForm();

        }

        private void btnDSGV_Click(object sender, EventArgs e)
        {
            reloadGV();

        }
    }
}
