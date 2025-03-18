using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;

namespace BTL
{
    public partial class FormLop : Form
    {
        private sql SQL = new sql();

        public FormLop()
        {
            InitializeComponent();
            reloadDTHS();
            loadDropList();
            SetupNumericFields();
        }

        private void reloadDTHS()
        {
            SQL.Connect();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            DataTable dt = SQL.datatable("SELECT * FROM Lop");
            dgvLop.DataSource = dt;

            SQL.Close();
        }

        private void resetControls()
        {
            txtMaLop.Clear();
            txtTenLop.Clear();

            numSiSo.Value = 0;
            numNamHoc.Value = DateTime.Now.Year;

            cbbGiaoVien.SelectedIndex = 0;

            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void loadDropList()
        {
            SQL.Connect();
            DataTable hocSinh = SQL.datatable("SELECT * FROM GiaoVien");
            SQL.FillComboBox(cbbGiaoVien, hocSinh, "TenGiaoVien", "MaGiaoVien");
            SQL.Close();
        }

        private void SetupNumericFields()
        {
            numNamHoc.Minimum = 1900;
            numNamHoc.Maximum = DateTime.Now.Year + 1;
        }

        private void fill_input(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Tránh lỗi khi click vào tiêu đề cột

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;

            DataGridViewRow row = dgvLop.Rows[e.RowIndex];

            txtMaLop.Text = row.Cells["MaLop"].Value?.ToString().Trim();
            txtTenLop.Text = row.Cells["TenLop"].Value?.ToString().Trim();
            numSiSo.Value = Convert.ToInt32(row.Cells["SiSo"].Value);
            numNamHoc.Value = Convert.ToInt32(row.Cells["NamHoc"].Value);
            cbbGiaoVien.SelectedValue = row.Cells["MaGiaoVien"].Value?.ToString().Trim();
        }

        private void ADD(object sender, EventArgs e)
        {
            string ma = txtMaLop.Text.Trim();
            string name = txtTenLop.Text.Trim();
            int siSo = (int)numSiSo.Value;
            int year = (int)numNamHoc.Value;
            string maGV = cbbGiaoVien.SelectedValue?.ToString().Trim();

            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(maGV))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin lớp học.");
                return;
            }

            SQL.Connect();

            // Kiểm tra khóa chính có trùng không
            if (!SQL.CheckPK("Lop", new string[] { $"MaLop = '{ma}'" }))
            {
                MessageBox.Show("Trùng khóa chính! Vui lòng nhập mã lớp khác.");
                SQL.Close();
                return;
            }

            string query = $"INSERT INTO Lop (MaLop, TenLop, SiSo, NamHoc, MaGiaoVien) " +
                           $"VALUES ('{ma}', '{name}', {siSo}, {year}, '{maGV}')";

            SQL.Insert(query);
            SQL.Close();

            reloadDTHS();
            MessageBox.Show("Thêm lớp thành công!");
            resetControls();
        }

        // Khai báo biến toàn cục
        private string oldMaLop, oldTenLop, oldMaGV;
        private int oldSiSo, oldNamHoc;

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string maLop = txtMaLop.Text.Trim();
            if (string.IsNullOrEmpty(maLop))
            {
                MessageBox.Show("Vui lòng chọn lớp học cần xóa!");
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa lớp {maLop} không?",
                                                   "Xác nhận xóa",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            SQL.Connect();

            try
            {
                string query = $"DELETE FROM Lop WHERE MaLop = '{maLop}'";
                int result = SQL.DeleteD(query);

                if (result == 547)  // Mã lỗi SQL nếu có ràng buộc khóa ngoại
                {
                    MessageBox.Show($"Không thể xóa lớp {maLop} do có dữ liệu liên quan!",
                                    "Lỗi ràng buộc",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
                else
                {
                    reloadDTHS();
                    MessageBox.Show($"Đã xóa lớp {maLop} thành công!");
                    resetControls();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SQL.Close();
            }
        }

        private void dgvLop_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvLop.Rows[e.RowIndex];

            // Lấy dữ liệu từ DataGridView và hiển thị lên form
            txtMaLop.Text = row.Cells["MaLop"].Value?.ToString().Trim() ?? "";
            txtTenLop.Text = row.Cells["TenLop"].Value?.ToString().Trim() ?? "";

            numSiSo.Value = int.TryParse(row.Cells["SiSo"].Value?.ToString(), out int siSo) ? siSo : 0;
            numNamHoc.Value = int.TryParse(row.Cells["NamHoc"].Value?.ToString(), out int namHoc) ? namHoc : DateTime.Now.Year;

            cbbGiaoVien.SelectedValue = row.Cells["MaGiaoVien"].Value?.ToString().Trim() ?? "";

            // Lưu lại dữ liệu cũ để kiểm tra thay đổi
            oldMaLop = txtMaLop.Text;
            oldTenLop = txtTenLop.Text;
            oldSiSo = (int)numSiSo.Value;
            oldNamHoc = (int)numNamHoc.Value;
            oldMaGV = cbbGiaoVien.SelectedValue?.ToString();

            // Bật các nút sửa, xóa
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLop.Text) ||
                string.IsNullOrWhiteSpace(txtTenLop.Text) ||
                string.IsNullOrWhiteSpace(cbbGiaoVien.SelectedValue?.ToString()))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            string newMaLop = txtMaLop.Text.Trim();
            string newTenLop = txtTenLop.Text.Trim();
            int newSiSo = (int)numSiSo.Value;
            int newNamHoc = (int)numNamHoc.Value;
            string newMaGV = cbbGiaoVien.SelectedValue?.ToString().Trim();

            List<string> updates = new List<string>();

            SQL.Connect();

            // Nếu mã lớp thay đổi, kiểm tra ràng buộc và cập nhật riêng
            if (newMaLop != oldMaLop)
            {
                string updateMaLop = $"UPDATE Lop SET MaLop = '{newMaLop}' WHERE MaLop = '{oldMaLop}'";
                int result = SQL.updateD(updateMaLop);
                if (result == 547)
                {
                    MessageBox.Show($"Lỗi ràng buộc: Hãy cập nhật dữ liệu liên quan trước khi đổi {oldMaLop} -> {newMaLop}");
                    SQL.Close();
                    return;
                }
            }

            // Kiểm tra các trường còn lại
            if (newTenLop != oldTenLop)
                updates.Add($"TenLop = N'{newTenLop}'");

            if (newSiSo != oldSiSo)
                updates.Add($"SiSo = {newSiSo}");

            if (newNamHoc != oldNamHoc)
                updates.Add($"NamHoc = {newNamHoc}");

            if (newMaGV != oldMaGV)
                updates.Add($"MaGiaoVien = '{newMaGV}'");

            // Nếu có thay đổi, thực hiện UPDATE
            if (updates.Count > 0)
            {
                string updateQuery = $"UPDATE Lop SET {string.Join(", ", updates)} WHERE MaLop = '{newMaLop}'";
                SQL.updateD(updateQuery);
            }

            SQL.Close();
            reloadDTHS();
            MessageBox.Show("Cập nhật lớp học thành công!");
            resetControls();
        }
    }
}