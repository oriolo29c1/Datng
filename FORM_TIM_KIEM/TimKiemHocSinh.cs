using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;

namespace BTL.FORM_TIM_KIEM
{
    public partial class TimKiemHocSinh : Form
    {
        sql SQL = new sql();

        public TimKiemHocSinh()
        {
            InitializeComponent();
            reload();
        }

        private void reload()
        {
            // Tắt tất cả các input ban đầu
            txtMaHS.Enabled = false;
            txtHoTen.Enabled = false;
            cbbLop.Enabled = false;
            btnFind.Enabled = false;

            // Load danh sách lớp
            SQL.Connect();
            DataTable hocSinh = SQL.datatable("SELECT * FROM Lop");
            SQL.FillComboBox(cbbLop, hocSinh, "TenLop", "MaLop");
            SQL.Close();
        }

        private void click_button_choi(object sender, EventArgs e)
        {
            var checkedItems = clbTimKiem.CheckedItems.Cast<string>().ToList();

            // Kích hoạt các input dựa vào checkbox đã chọn
            txtMaHS.Enabled = checkedItems.Contains("Mã học sinh");
            txtHoTen.Enabled = checkedItems.Contains("Tên Học Sinh");
            cbbLop.Enabled = checkedItems.Contains("Lớp");

            // Bật nút tìm kiếm nếu có ít nhất một lựa chọn
            btnFind.Enabled = checkedItems.Count > 0;
        }

        public string[] x = new string[3];

        public void cLICK_to_SEND_TOfORM2(object sender, EventArgs e)
        {
            x[0] = txtMaHS.Enabled ? $" MaHocSinh = '{txtMaHS.Text.Trim()}' " : "";
            x[1] = txtHoTen.Enabled ? $" HoTenHocSinh LIKE N'%{txtHoTen.Text.Trim()}%' " : "";
            x[2] = cbbLop.Enabled ? GetClassCondition() : "";

            if (x[2] == null) return;  // Nếu có lỗi khi lấy mã lớp thì dừng lại

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private string GetClassCondition()
        {

            string selectedClass = cbbLop.SelectedValue.ToString();

            return $" MaLop = '{selectedClass}' ";
        }
    }
}