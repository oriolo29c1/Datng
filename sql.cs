using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BTL
{
    public class sql
    {
        // tham số dùng tạo select
        public string start, end, name;
        // Chuỗi kết nối đến SQL Server (Dùng static vì c# không cho 1 trường non-static làm tham số để khởi tạo giá trị 1 trường khác)
        static string connectionString = "Server=localhost;Database=QUANLYHOCSINH;Integrated Security=True;";
        // Đối tượng connect
        public SqlConnection conn = new SqlConnection(connectionString);

        // hàm mở connect
        public void Connect()
        {
            try
            {
                if(conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }
        // hàm đóng connect
        public void Close()
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        // hàm trả về dataTable từ câu lệnh select
        public DataTable datatable(string x)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(x, conn);
                adapter.Fill(dt);

            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            return dt;
        }
        // hàm kiểm tra vi phạm pk của 1 bảng (Với việc ta đã biết tên bảng cần kiểm tra và khóa chính của bảng là gì)
        public bool CheckPK (string table, string[] pk) // pk[0] = "column = '1'" hoặc pk[2] = "colunm = 'Hello'"
        {
            string x = $"select * from {table} where ";
            for (int i = 0; i<pk.Length; i++)
            {
                if(i < pk.Length - 1)
                {
                    x += pk[i] + " and ";
                }
                else
                {
                    x += pk[i];
                }
            }
            DataTable dt = new DataTable();

            try
            {
                // MessageBox.Show(x);
                SqlDataAdapter adapter = new SqlDataAdapter(x, conn);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (dt.Rows.Count > 0) { return false; }
            return true;
        }

        // Hàm lấy các giá trị của 1 cột nào đó của 1 table (thường dùng cho droplist)
        public List<string> GetValues(string tableName, string column_name)
        {

            List<string> values = new List<string>();
            string SQL_statement = $"select {column_name} from {tableName}";
            SqlCommand cmd = new SqlCommand(SQL_statement, conn);
            SqlDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();
            }
            catch(Exception ex)
            {
                MessageBox.Show (ex.Message);   
            }
            // Lấy số lượng cột trong bảng
            int columnCount = reader.FieldCount;
            // Đọc dữ liệu bằng index
            while (reader.Read())
            {
                for (int i = 0; i < columnCount; i++)
                {
                    values.Add(reader[column_name].ToString());
                    Console.WriteLine($"Getvalues {tableName} {column_name} : " + reader[column_name].ToString());
                }

            }
            reader.Close(); // Đóng reader sau khi đọc xong
            return values;
        }
        public int updateD(string q)
        {
            int check = 1;
            SqlCommand cmd = new SqlCommand(q, conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Kiểm tra mã lỗi
                {
                    MessageBox.Show("Lỗi ràng buộc khóa ngoại! Vui lòng kiểm tra dữ liệu liên quan.");
                    check = 547;
                }
                else
                {
                    MessageBox.Show($"Lỗi SQL: {ex.Message}");
                }
            }
            return check;
        }

        public int DeleteD(string q)
        {
            int check = 1;
            SqlCommand smd = new SqlCommand(q, conn);
            try
            {
                smd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return check;
        }
        public int Insert(string q)
        {
            int check = 1;
            SqlCommand smd = new SqlCommand(q, conn);
            try
            {
                smd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return check;
        }

        public void FillComboBox(ComboBox cbb, DataTable dt, string displayMember, string valueMember)
        {
            if (cbb == null || dt == null || dt.Rows.Count == 0) return;

            cbb.DataSource = dt;
            cbb.DisplayMember = displayMember;
            cbb.ValueMember = valueMember;
        }
        public void FillDataGridView(DataGridView dgv, DataTable dt)
        {
            if (dgv == null || dt == null) return;

            dgv.DataSource = null;
            dgv.DataSource = dt;
        }

    }
}
