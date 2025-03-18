using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace BTL
{
    public partial class FormMonHoc : Form
    {
        private readonly sql SQL = new sql();
        private string oldMaMH = "", oldTenMon = "";

        public FormMonHoc()
        {
            InitializeComponent();
            reload();
        }

        private void reload()
        {
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            txtMaMH.Clear();
            txtTenMon.Clear();

            SQL.Connect();
            dgvMonHoc.DataSource = SQL.datatable("SELECT * FROM MonHoc");
            SQL.Close();
        }

        private void find(object sender, EventArgs e)
        {
            StringBuilder query = new StringBuilder("SELECT * FROM MonHoc");
            List<string> conditions = new List<string>();

            if (!string.IsNullOrWhiteSpace(txtMaMH.Text))
                conditions.Add($"MaMonHoc LIKE '%{txtMaMH.Text.Trim()}%'");

            if (!string.IsNullOrWhiteSpace(txtTenMon.Text))
                conditions.Add($"TenMonHoc LIKE N'%{txtTenMon.Text.Trim()}%'");

            if (conditions.Count > 0)
                query.Append(" WHERE " + string.Join(" AND ", conditions));

            SQL.Connect();
            dgvMonHoc.DataSource = SQL.datatable(query.ToString());
            SQL.Close();
        }

        private void cell_click_monHoc(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvMonHoc.Rows[e.RowIndex];
            txtMaMH.Text = row.Cells["MaMonHoc"].Value.ToString();
            txtTenMon.Text = row.Cells["TenMonHoc"].Value.ToString();

            oldMaMH = txtMaMH.Text;
            oldTenMon = txtTenMon.Text;

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void click_edit_monhoc(object sender, EventArgs e)
        {
            string newMaMH = txtMaMH.Text.Trim();
            string newTenMon = txtTenMon.Text.Trim();

            if (string.IsNullOrWhiteSpace(newMaMH) || string.IsNullOrWhiteSpace(newTenMon))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            SQL.Connect();
            if (newMaMH != oldMaMH)
            {
                string updateMaMH = $"UPDATE MonHoc SET MaMonHoc = '{newMaMH}' WHERE MaMonHoc = '{oldMaMH}'";
                int result = SQL.updateD(updateMaMH);
                if (result == 547)
                {
                    MessageBox.Show($"Lỗi ràng buộc: Hãy cập nhật dữ liệu liên quan trước khi đổi {oldMaMH} -> {newMaMH}");
                    SQL.Close();
                    return;
                }
            }

            if (newTenMon != oldTenMon)
            {
                string updateTenMon = $"UPDATE MonHoc SET TenMonHoc = N'{newTenMon}' WHERE MaMonHoc = '{oldMaMH}'";
                SQL.updateD(updateTenMon);
            }
            SQL.Close();
        }

        private void click_toDeleteMH(object sender, EventArgs e)
        {
            string maMH = txtMaMH.Text.Trim();
            if (string.IsNullOrEmpty(maMH))
            {
                MessageBox.Show("Vui lòng chọn môn học cần xóa!");
                return;
            }

            SQL.Connect();
            string query = $"DELETE FROM MonHoc WHERE MaMonHoc = '{maMH}'";
            SQL.DeleteD(query);
            SQL.Close();

            reload();
        }

        private void click_toAddMH(object sender, EventArgs e)
        {
            string maMH = txtMaMH.Text.Trim();
            string tenMon = txtTenMon.Text.Trim();

            if (string.IsNullOrEmpty(maMH) || string.IsNullOrEmpty(tenMon))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            SQL.Connect();
            if (SQL.CheckPK("MonHoc", new[] { $"MaMonHoc = '{maMH}'" }))
            {
                string query = $"INSERT INTO MonHoc (MaMonHoc, TenMonHoc) VALUES ('{maMH}', N'{tenMon}')";
                SQL.Insert(query);
                reload();
            }
            else
            {
                MessageBox.Show($"Mã môn học '{maMH}' đã tồn tại!");
            }
            SQL.Close();
        }
    }
}