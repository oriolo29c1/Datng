using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BTL.FORM_TIM_KIEM;
using System.Text.RegularExpressions;
namespace BTL
{
    public partial class FormHocSinh : Form
    {
        sql SQL = new sql();
        public FormHocSinh()
        {
            InitializeComponent();
            reloadDTHS();
            loadDropList();
        }

        private void ResetForm()
        {
            txtMaHS.Text = "";
            txtHoTen.Text = "";
            txtDiaChi.Text = "";
            dtNgaySinh.Value = dtNgaySinh.MaxDate; 
            radioNam.Checked = true; 
            radioNu.Checked = false;
            txtDanToc.Text = "";
            txtQuocTich.Text = "";
            txtSoDienThoai.Text = "";
            cbbLop.SelectedIndex = 0; 
        }

        private void reloadDTHS()
        {
            SQL.Connect();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            dgvHocSinh.DataSource = SQL.datatable("SELECT * FROM HoSoHocSinh");
            SQL.Close();
        }

        private void loadDropList()
        {
            SQL.Connect();
            DataTable hocSinh = SQL.datatable("SELECT * FROM Lop");
            SQL.FillComboBox(cbbLop, hocSinh, "TenLop", "MaLop");
            SQL.Close();
        }

        private void Click_to_addHS(object sender, EventArgs e)
        {
            string ma = txtMaHS.Text.Trim();
            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Không được để trống mã học sinh!");
                return;
            }

            // Kiểm tra trùng khóa chính
            if (!SQL.CheckPK("HoSoHocSinh", new string[] { $"MaHocSinh = '{ma}'" }))
            {
                MessageBox.Show("Trùng khóa chính!");
                return;
            }

            string hoTen = txtHoTen.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string ngaySinh = dtNgaySinh.Value.ToString("yyyy-MM-dd");
            int gioiTinh = radioNam.Checked ? 1 : 0;
            string danToc = txtDanToc.Text.Trim();
            string quocTich = txtQuocTich.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string maLop = cbbLop.SelectedValue?.ToString() ?? "";

            string query = $@"
                INSERT INTO HoSoHocSinh (MaHocSinh, HoTenHocSinh, DiaChi, NgaySinh, GioiTinh, DanToc, QuocTich, SoDienThoai, MaLop)
                VALUES ('{ma}', N'{hoTen}', N'{diaChi}', '{ngaySinh}', {gioiTinh}, N'{danToc}', N'{quocTich}', '{soDienThoai}', '{maLop}')
            ";

            SQL.Connect();
            SQL.Insert(query);
            SQL.Close();
            reloadDTHS();
            ResetForm();
        }

        private void Click_to_edit_HS(object sender, EventArgs e)
        {
            string maHocSinh = txtMaHS.Text.Trim();
            if (string.IsNullOrEmpty(maHocSinh))
            {
                MessageBox.Show("Mã học sinh không được để trống!");
                return;
            }

            string hoTen = txtHoTen.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string ngaySinh = dtNgaySinh.Value.ToString("yyyy-MM-dd");
            int gioiTinh = radioNam.Checked ? 1 : 0;
            string danToc = txtDanToc.Text.Trim();
            string quocTich = txtQuocTich.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string maLop = cbbLop.SelectedValue?.ToString() ?? "";

            List<string> updates = new List<string>();
            if (!string.IsNullOrEmpty(hoTen)) updates.Add($"HoTenHocSinh = N'{hoTen}'");
            if (!string.IsNullOrEmpty(diaChi)) updates.Add($"DiaChi = N'{diaChi}'");
            if (!string.IsNullOrEmpty(ngaySinh)) updates.Add($"NgaySinh = '{ngaySinh}'");
            updates.Add($"GioiTinh = {gioiTinh}");
            if (!string.IsNullOrEmpty(danToc)) updates.Add($"DanToc = N'{danToc}'");
            if (!string.IsNullOrEmpty(quocTich)) updates.Add($"QuocTich = N'{quocTich}'");
            if (!string.IsNullOrEmpty(soDienThoai)) updates.Add($"SoDienThoai = '{soDienThoai}'");
            if (!string.IsNullOrEmpty(maLop)) updates.Add($"MaLop = '{maLop}'");

            if (updates.Count == 0)
            {
                MessageBox.Show("Không có thông tin nào để cập nhật!");
                return;
            }

            string query = $"UPDATE HoSoHocSinh SET {string.Join(", ", updates)} WHERE MaHocSinh = '{maHocSinh}'";

            SQL.Connect();

            try
            {
                int result = SQL.updateD(query);
                MessageBox.Show($"Cập Nhật Thành Công.");
                reloadDTHS();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message);
            }

            SQL.Close();
            ResetForm();
        }

        private void Click_to_deleteHS(object sender, EventArgs e)
        {
            string maHocSinh = txtMaHS.Text.Trim();
            if (string.IsNullOrEmpty(maHocSinh))
            {
                MessageBox.Show("Vui lòng nhập mã học sinh cần xóa!");
                return;
            }

            SQL.Connect();

            try
            {
                string query = $"DELETE FROM HoSoHocSinh WHERE MaHocSinh = '{maHocSinh}'";
                int result = SQL.DeleteD(query);

                if (result > 0)
                {
                    MessageBox.Show("Xóa thành công!");
                    reloadDTHS();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy học sinh cần xóa!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }

            SQL.Close();
            ResetForm();
        }

        // SỬ dụng form từ thư mục khác thêm đoạn using BTL.FORM_TIM_KIEM; Vào trên

        private void Click_to_open_form_Find(object sender, EventArgs e)
        {
            string query = "SELECT * FROM HoSoHocSinh";
            TimKiemHocSinh fhs = new TimKiemHocSinh();

            if (fhs.ShowDialog() == DialogResult.OK) // Chỉ chạy khi người dùng nhập và đóng form
            {
                string[] result = fhs.x;
                List<string> filters = result.Where(r => !string.IsNullOrEmpty(r)).ToList(); // Lọc giá trị rỗng

                if (filters.Count > 0)
                {
                    query += " WHERE " + string.Join(" AND ", filters); // Ghép điều kiện SQL
                }
            }

            MessageBox.Show(query);
            dgvHocSinh.DataSource = SQL.datatable(query);
        }

        private void showHOCSINH(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;

            if (e.RowIndex < 0) return; // Đảm bảo không click vào tiêu đề

            DataGridViewRow row = dgvHocSinh.Rows[e.RowIndex];

            // Lấy dữ liệu từ DataGridView
            string id = row.Cells["MaHocSinh"].Value?.ToString();
            string name = row.Cells["HoTenHocSinh"].Value?.ToString();
            string dt = row.Cells["DiaChi"].Value?.ToString();
            DateTime timeOld = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
            bool GioiTinh = Convert.ToBoolean(row.Cells["GioiTinh"].Value);
            string QuocTich = row.Cells["QuocTich"].Value?.ToString();
            string DanToc = row.Cells["DanToc"].Value?.ToString();
            string SDT = row.Cells["SoDienThoai"].Value?.ToString();
            string MaLop = row.Cells["MaLop"].Value?.ToString();

            // Gán giá trị vào các control
            txtMaHS.Text = id;
            txtHoTen.Text = name;
            txtDiaChi.Text = dt;
            dtNgaySinh.Value = timeOld;
            radioNam.Checked = GioiTinh;
            radioNu.Checked = !GioiTinh;
            txtDanToc.Text = DanToc;
            txtQuocTich.Text = QuocTich;
            txtSoDienThoai.Text = SDT;
            cbbLop.SelectedValue = MaLop;
        }

        private void txtMaHS_TextChanged(object sender, EventArgs e)
        {
            string ma = txtMaHS.Text.Trim();

            // Kiểm tra nếu để trống
            if (string.IsNullOrEmpty(ma))
            {
                err.SetError(txtMaHS, "Không được để trống!");
            }
            else
            {
                err.SetError(txtMaHS, ""); // Xóa lỗi nếu hợp lệ
            }
        }

        private void txtDiaChi_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                err.SetError(txtDiaChi, "Địa chỉ không được để trống!");
            }
            else
            {
                err.SetError(txtDiaChi, "");
            }
        }


        private void txtDanToc_TextChanged(object sender, EventArgs e)
        {
            string dantoc = txtDanToc.Text.Trim();
            string pattern = "^[^0-9]+$";

            if (string.IsNullOrWhiteSpace(dantoc))
            {
                err.SetError(txtDanToc, "Dân Tộc không được để trống!");
            }
            else if (!Regex.IsMatch(txtDanToc.Text, pattern))
            {
                err.SetError(txtDanToc, "Dân Tộc không được chứa ký tự đặc biệt!");
            }
            else
            {
                err.SetError(txtDanToc, "");
            }
        }

        private void txtQuocTich_TextChanged(object sender, EventArgs e)
        {
            string quocTich = txtQuocTich.Text.Trim();
            string pattern = "^[^0-9]+$";

            if (string.IsNullOrWhiteSpace(quocTich))
            {
                err.SetError(txtQuocTich, "Quốc tịch không được để trống!");
            }
            else if (!Regex.IsMatch(txtQuocTich.Text, pattern))
            {
                err.SetError(txtQuocTich, "Quốc tịch không được chứa số!");
            }
            else
            {
                err.SetError(txtQuocTich, ""); 
            }

        }

        private void txtSoDienThoai_TextChanged(object sender, EventArgs e)
        {
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string pattern = @"^\d{9,11}$"; // Chỉ chấp nhận số, độ dài 9-11 số

            if (string.IsNullOrWhiteSpace(soDienThoai))
            {
                err.SetError(txtSoDienThoai, "Số điện thoại không được để trống!");
            }
            else if (!Regex.IsMatch(soDienThoai, pattern))
            {
                err.SetError(txtSoDienThoai, "Số điện thoại không hợp lệ! (chỉ chứa 9-11 chữ số)");
            }
            else
            {
                err.SetError(txtSoDienThoai, ""); 
            }
        }

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            string pattern = "^[^0-9]+$";

            if (string.IsNullOrWhiteSpace(hoTen))
            {
                err.SetError(txtHoTen, "Họ tên không được để trống!");
            }
            else if (!Regex.IsMatch(hoTen, pattern))
            {
                err.SetError(txtHoTen, "Họ tên không được chứa ký tự đặc biệt!");
            }
            else
            {
                err.SetError(txtHoTen, "");
            }
        }
    }
}
