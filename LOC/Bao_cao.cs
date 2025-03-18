using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL.LOC
{
    public partial class Bao_cao : Form
    {
        crytals cry = new crytals();
        public Bao_cao()
        {
            InitializeComponent();
            crystalReportViewer1.ReportSource = cry.Load_crytal(@"D:\BTL\LOC\KHOANG_DIEM_MON.rpt");
            crystalReportViewer1.RefreshReport(); // Nếu có thay đổi về source sẽ tự cập nhật lại 
        }
        public void Condition(string cond)
        {
            cry.addCondition(cond);
            crystalReportViewer1.RefreshReport();// Đảm bảo Crystal Report cập nhật lại điều kiện lọc (bắt buộc mỗi khi có thêm condition)
        }
    }
}
