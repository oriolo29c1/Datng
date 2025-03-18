using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//-
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace BTL
{
    internal class crytals
    {
        
        ReportDocument cryRpt; 
       
        public ReportDocument Load_crytal(string url)
        {
            cryRpt = new ReportDocument();
            // Đường dẫn đến file Crystal Report (.rpt)
            string reportPath = $"{url}";
            cryRpt.Load(reportPath);

            // Tạo đối tượng kết nối CSDL
            ConnectionInfo connInfo = new ConnectionInfo();
            connInfo.ServerName = "LAPTOP-NQEL46NE\\MAY1"; // Ví dụ: "localhost"
            connInfo.DatabaseName = "QUANLYHOCSINH"; // Ví dụ: "QLSV"
            connInfo.UserID = "sa"; // Ví dụ: "sa"
            connInfo.Password = "12345678"; // Ví dụ: "123456"

            // Duyệt tất cả Table trong báo cáo và thiết lập kết nối cho mỗi bảng
            Tables tables = cryRpt.Database.Tables;
            foreach (Table table in tables)
            {
                TableLogOnInfo tableLogOnInfo = table.LogOnInfo;
                tableLogOnInfo.ConnectionInfo = connInfo;
                table.ApplyLogOnInfo(tableLogOnInfo);
            }

            return cryRpt;
        }
        public void addCondition(string condition)
        {
            cryRpt.RecordSelectionFormula = condition;
        }
        
    }
}