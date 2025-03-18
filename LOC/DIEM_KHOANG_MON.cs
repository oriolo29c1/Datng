using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BTL.LOC
{
    public partial class DIEM_KHOANG_MON : Form
    {
        sql ff = new sql();
        public DIEM_KHOANG_MON()
        {
            InitializeComponent();
            reload();
            numericUpDown1.DecimalPlaces = 2;  // Hiển thị 2 số sau dấu thập phân
            numericUpDown1.Increment = 0.1M;   // Tăng/Giảm 0.1 khi bấm nút
            numericUpDown1.Minimum = 0;        // Giá trị tối thiểu
            numericUpDown1.Maximum = 10;      // Giá trị tối đa

            numericUpDown2.DecimalPlaces = 2;  // Hiển thị 2 số sau dấu thập phân
            numericUpDown2.Increment = 0.1M;   // Tăng/Giảm 0.1 khi bấm nút
            numericUpDown2.Minimum = 0;        // Giá trị tối thiểu
            numericUpDown2.Maximum = 10;      // Giá trị tối đa

            //Nhan dau phay khong phai cham
        }

        public void reload()
        {
            comboBox1.Enabled = false;
            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            ff.Connect();
            string q = $"select TenMonHoc, HoTenHocSinh, TenLop, TenGiaoVien, MaHocKi, DiemGiuaKi, DiemCuoiKi, (DiemMonHoc.DiemGiuaKi* DiemMonHoc.HeSoDiemGiuaKi + DiemMonHoc.DiemCuoiKi * DiemMonHoc.HeSoDiemCuoiKi) AS DiemTrungBinh, NamHoc, MaHocKi " +
                $"from MonHoc join DiemMonHoc on MonHoc.MaMonHoc = DiemMonHoc.MaMonHoc " +
                $"join HoSoHocSinh on DiemMonHoc.MaHocSinh = HoSoHocSinh.MaHocSinh " +
                $"join Lop on Lop.MaLop = DiemMonHoc.MaLop join GiaoVien on Lop.MaGiaoVien = GiaoVien.MaGiaoVien";
            DataTable dt = ff.datatable(q);
            dataGridView1.DataSource = dt;
            //- comboBox
            List<string> list = ff.GetValues("MonHoc", "TenMonHoc");
            comboBox1.DataSource = list;
            ff.Close();
        }

        private void click_to_chon(object sender, EventArgs e)
        {
            List<string> list2 = checkedListBox1.CheckedItems.Cast<string>().ToList(); // Lấy danh sách các ô đã được checked
            if (list2.Contains("Mon"))
            {
                comboBox1.Enabled = true;
            }
            else
            {
                comboBox1.Enabled = false;
            }
            if (list2.Contains("Khoang"))
            {
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
            }
            else
            {
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
            }
        }

        private void Click_to_find(object sender, EventArgs e)
        {
            List<string> tmp = new List<string>();
            if(comboBox1.Enabled == true)
            {
                tmp.Add($" TenMonHoc = N'{comboBox1.SelectedValue.ToString().Trim()}' ");
            }

            if (numericUpDown1.Enabled)
            {
                tmp.Add($" ((DiemMonHoc.DiemGiuaKi* DiemMonHoc.HeSoDiemGiuaKi + DiemMonHoc.DiemCuoiKi * DiemMonHoc.HeSoDiemCuoiKi) >= {numericUpDown1.Value} and (DiemMonHoc.DiemGiuaKi* DiemMonHoc.HeSoDiemGiuaKi + DiemMonHoc.DiemCuoiKi * DiemMonHoc.HeSoDiemCuoiKi) <={numericUpDown2.Value}) ");
            }

            string q = $"select TenMonHoc, HoTenHocSinh, TenLop, TenGiaoVien, MaHocKi, DiemGiuaKi, DiemCuoiKi, (DiemMonHoc.DiemGiuaKi* DiemMonHoc.HeSoDiemGiuaKi + DiemMonHoc.DiemCuoiKi * DiemMonHoc.HeSoDiemCuoiKi) AS DiemTrungBinh, NamHoc, MaHocKi " +
                $"from MonHoc join DiemMonHoc on MonHoc.MaMonHoc = DiemMonHoc.MaMonHoc " +
                $"join HoSoHocSinh on DiemMonHoc.MaHocSinh = HoSoHocSinh.MaHocSinh " +
                $"join Lop on Lop.MaLop = DiemMonHoc.MaLop join GiaoVien on Lop.MaGiaoVien = GiaoVien.MaGiaoVien";
            if (tmp.Count > 0) q += " where ";

            for(int i = 0; i < tmp.Count; i++)
            {
                if(i == tmp.Count - 1)
                {
                    q += tmp[i];
                }
                else
                {
                    q +=  tmp[i] + " AND ";
                }
            }

            //-
            ff.Connect();
            DataTable dt = ff.datatable(q);
            dataGridView1.DataSource = dt;
            ff.Close();
        }


        Bao_cao form_BC = null;
        private void Click_to_report(object sender, EventArgs e)
        {
            List<string> tmp = new List<string>();
            if (comboBox1.Enabled == true)
            {
                tmp.Add(" {MonHoc.TenMonHoc} = " +  $" '{comboBox1.SelectedValue.ToString().Trim()}' "); // Bỏ N khi dùng crystal
            }

            if (numericUpDown1.Enabled)
            {
                tmp.Add(" (({DiemMonHoc.DiemGiuaKi}* {DiemMonHoc.HeSoDiemGiuaKi} + {DiemMonHoc.DiemCuoiKi} * {DiemMonHoc.HeSoDiemCuoiKi}) >="
                    + $" {numericUpDown1.Value} and " +
                    " ({DiemMonHoc.DiemGiuaKi}* {DiemMonHoc.HeSoDiemGiuaKi} + {DiemMonHoc.DiemCuoiKi} * {DiemMonHoc.HeSoDiemCuoiKi}) <= " 
                    + $" {numericUpDown2.Value}) ");
            }
            string cond = "";
            
            for (int i = 0;i < tmp.Count;i++)
            {
                if( i == tmp.Count - 1)
                {
                    cond += tmp[i];
                }
                else
                {
                    cond += tmp[i] + " AND ";
                }
            }
            MessageBox.Show(cond);
            if (form_BC == null || form_BC.IsDisposed) // Hoặc chưa khởi tạo hoặc bị đóng
            {
                form_BC = new Bao_cao(); // Tạo form mới với data mới lần 1
                if (cond.Length > 0) form_BC.Condition(cond);
                form_BC.Show();
            }
            else
            {
                //cập nhập lại form_BC -- Chọn cách tắt form khởi tạo lại
                form_BC.Close();
                form_BC = new Bao_cao(); // Tạo form mới với data mới lần sau 1
                if (cond.Length > 0) form_BC.Condition(cond);
                form_BC.Show();
            }
        }
    }
}
