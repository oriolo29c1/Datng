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
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace BTL.LOC
{
    public partial class DiemHocSinh : Form
    {
        sql SQL = new sql();
        public DiemHocSinh()
        {
            InitializeComponent();
        }

        private void LoadComboBoxData()
        {

            SQL.Connect();

            DataTable dtLop = SQL.datatable("SELECT * FROM Lop");
            SQL.FillComboBox(cbbLop, dtLop, "TenLop", "MaLop");

            DataTable dtMonHoc = SQL.datatable("SELECT * FROM MonHoc");
            SQL.FillComboBox(cbbMonHoc, dtMonHoc, "TenMonHoc", "MaMonHoc");

            SQL.Close();
        }

        private void LoadDiemMonHoc(string maLop, string maMonHoc)
        {
            string connectionString = "Server=localhost;Database=QUANLYHOCSINH;Integrated Security=True;";
            string query = @"
            SELECT 
                hs.HoTenHocSinh,
                mh.TenMonHoc,
                dm.DiemGiuaKi,
                dm.DiemCuoiKi,
                dm.MaHocKi,
                ROUND((dm.DiemGiuaKi * dm.HeSoDiemGiuaKi + dm.DiemCuoiKi * dm.HeSoDiemCuoiKi), 2) AS DiemTrungBinh,
                l.TenLop
            FROM DiemMonHoc dm
            JOIN HoSoHocSinh hs ON dm.MaHocSinh = hs.MaHocSinh
            JOIN MonHoc mh ON dm.MaMonHoc = mh.MaMonHoc
            JOIN Lop l ON hs.MaLop = l.MaLop
            WHERE dm.MaLop = @MaLop AND dm.MaMonHoc = @MaMonHoc
            ORDER BY hs.HoTenHocSinh;";

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLop", maLop);
                        cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void LoadCrystalReport()
        {
            string connectionString = "Server=localhost;Database=QUANLYHOCSINH;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDiemHocSinh", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaLop", cbbLop.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@MaMonHoc", cbbMonHoc.SelectedValue.ToString());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    ReportDocument rptDoc = new ReportDocument();
                    rptDoc.Load(@"D:\Hướng Sự Kiện\BaiTapLon\BaiTapLon\WindowsFormsApp1\rptDiemHocSinh.rpt");
                    rptDoc.SetDataSource(dt);

                    crystalReportDiemMonHoc.ReportSource = rptDoc;
                    crystalReportDiemMonHoc.Refresh();
                }
            }
        }

        private void FormDiemMon_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            LoadDiemMonHoc(cbbLop.SelectedValue.ToString(), cbbMonHoc.SelectedValue.ToString());
            LoadCrystalReport();
        }
    }
}

