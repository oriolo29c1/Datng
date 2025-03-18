using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BTL.LOC;

namespace BTL
{
    public partial class FormParent : Form
    {
        private FormHocSinh hocSinh = null;
        private FormMonHoc monHoc = null;
<<<<<<< HEAD
        private FormGiaoVien giaoVien = null;
=======
        private Form4 giaoVien = null;
>>>>>>> c612b34a7ca0059b49eca4326002829f3d3d7e2e
        private FormDiem diem = null;
        private FormLop lop = null;
        private DIEM_KHOANG_MON DKM = null; // caanf cos using BTL.LOC; vif thuoc thuw mucj khacs
        private DiemHocSinh diemHS = null;
        public FormParent()
        {
            InitializeComponent();
            this.IsMdiContainer = true; // Biến Form này thành MDI Container
        }

        void hideOrtherForm(Form X)
        {
            foreach(Form child in this.MdiChildren)
            {
                if(X != child)
                {
                    child.Hide();
                }
            }
        }

        private void Click_toShowHocSinh(object sender, EventArgs e)
        {
            if (hocSinh == null || hocSinh.IsDisposed)
            {
                hocSinh = new FormHocSinh();
                hocSinh.MdiParent = this;
                hocSinh.FormClosed += (s, args) => hocSinh = null; // Reset biến khi form đóng
                // Trước khi show phải ẩn form khác
                hideOrtherForm(hocSinh);
                hocSinh.Show();
            }
            else if (hocSinh.Visible)
            {
                hocSinh.Hide();
            }
            else
            {
                // Trước khi show phải ẩn form khác
                hideOrtherForm(hocSinh);
                hocSinh.Show();
            }
        }

        private void click_to_showMonHoc(object sender, EventArgs e)
        {
            if (monHoc == null || monHoc.IsDisposed)
            {
                monHoc = new FormMonHoc();
                monHoc.MdiParent = this;
                monHoc.FormClosed += (s, args) => monHoc = null;
                // Trước khi show phải ẩn form khác
                hideOrtherForm(monHoc);
                monHoc.Show();
            }
            else if (monHoc.Visible)
            {
                monHoc.Hide();
            }
            else
            {
                hideOrtherForm(monHoc);
                monHoc.Show();
            }
        }

        private void Click_toShowGiaoVien(object sender, EventArgs e)
        {
            if (giaoVien == null || giaoVien.IsDisposed)
            {
<<<<<<< HEAD
                giaoVien = new FormGiaoVien();
=======
                giaoVien = new Form4();
>>>>>>> c612b34a7ca0059b49eca4326002829f3d3d7e2e
                giaoVien.MdiParent = this;
                giaoVien.FormClosed += (s, args) => giaoVien = null;
                // Trước khi show phải ẩn form khác
                hideOrtherForm(giaoVien);
                giaoVien.Show();
            }
            else if (giaoVien.Visible)
            {
                giaoVien.Hide();
            }
            else
            {
                hideOrtherForm(giaoVien);
                giaoVien.Show();
            }
        }

        private void Click_toShowDiem(object sender, EventArgs e)
        {
            if (diem == null || diem.IsDisposed)
            {
                diem = new FormDiem();
                diem.MdiParent = this;
                diem.FormClosed += (s, args) => diem = null;
                // Trước khi show phải ẩn form khác
                hideOrtherForm(diem);
                diem.Show();
            }
            else if (diem.Visible)
            {
                diem.Hide();
            }
            else
            {hideOrtherForm(diem);
                diem.Show();
            }
        }

        private void Click_to_showLop(object sender, EventArgs e)
        {
            if (lop == null || lop.IsDisposed)
            {
                lop = new FormLop();
                lop.MdiParent = this;
                lop.FormClosed += (s, args) => lop = null;
                // Trước khi show phải ẩn form khác
                hideOrtherForm(lop);
                lop.Show();
            }
            else if (lop.Visible)
            {
                lop.Hide();
            }
            else
            {
                hideOrtherForm(lop);
                lop.Show();
            }
        }

        private void KHOANG_DIEM_MON(object sender, EventArgs e)
        {
            if (DKM == null || DKM.IsDisposed)
            {
                DKM = new DIEM_KHOANG_MON();
                DKM.MdiParent = this;
                DKM.FormClosed += (s, args) => DKM = null;
                // Trước khi show phải ẩn form khác
                hideOrtherForm(DKM);
                DKM.Show();
            }
            else if (DKM.Visible)
            {
                DKM.Hide();
            }
            else
            {
                hideOrtherForm(DKM);
                DKM.Show();
            }
        }

        private void điểmChiTiếtHọcSinhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (diemHS == null || diemHS.IsDisposed)
            {
                diemHS = new DiemHocSinh();
                diemHS.MdiParent = this;
                diemHS.FormClosed += (s, args) => diemHS = null;
                // Trước khi show phải ẩn form khác
                hideOrtherForm(diemHS);
                diemHS.Show();
            }
            else if (diemHS.Visible)
            {
                diemHS.Hide();
            }
            else
            {
                hideOrtherForm(diemHS);
                diemHS.Show();
            }
        }
    }
}
