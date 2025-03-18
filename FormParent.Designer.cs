namespace BTL
{
    partial class FormParent
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.bangHocSinhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bangMônHocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bangGiaoViênToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bangLơpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bangĐiêmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timKiêmVaLocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.điêmTheoMônKhoangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.điểmChiTiếtHọcSinhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bangHocSinhToolStripMenuItem,
            this.bangMônHocToolStripMenuItem,
            this.bangGiaoViênToolStripMenuItem,
            this.bangLơpToolStripMenuItem,
            this.bangĐiêmToolStripMenuItem,
            this.timKiêmVaLocToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(900, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // bangHocSinhToolStripMenuItem
            // 
            this.bangHocSinhToolStripMenuItem.Name = "bangHocSinhToolStripMenuItem";
            this.bangHocSinhToolStripMenuItem.Size = new System.Drawing.Size(144, 29);
            this.bangHocSinhToolStripMenuItem.Text = "Bảng Học Sinh";
            this.bangHocSinhToolStripMenuItem.Click += new System.EventHandler(this.Click_toShowHocSinh);
            // 
            // bangMônHocToolStripMenuItem
            // 
            this.bangMônHocToolStripMenuItem.Name = "bangMônHocToolStripMenuItem";
            this.bangMônHocToolStripMenuItem.Size = new System.Drawing.Size(147, 29);
            this.bangMônHocToolStripMenuItem.Text = "Bảng Môn Học";
            this.bangMônHocToolStripMenuItem.Click += new System.EventHandler(this.click_to_showMonHoc);
            // 
            // bangGiaoViênToolStripMenuItem
            // 
            this.bangGiaoViênToolStripMenuItem.Name = "bangGiaoViênToolStripMenuItem";
            this.bangGiaoViênToolStripMenuItem.Size = new System.Drawing.Size(148, 29);
            this.bangGiaoViênToolStripMenuItem.Text = "Bảng Giáo Viên";
            this.bangGiaoViênToolStripMenuItem.Click += new System.EventHandler(this.Click_toShowGiaoVien);
            // 
            // bangLơpToolStripMenuItem
            // 
            this.bangLơpToolStripMenuItem.Name = "bangLơpToolStripMenuItem";
            this.bangLơpToolStripMenuItem.Size = new System.Drawing.Size(103, 29);
            this.bangLơpToolStripMenuItem.Text = "Bảng Lớp";
            this.bangLơpToolStripMenuItem.Click += new System.EventHandler(this.Click_to_showLop);
            // 
            // bangĐiêmToolStripMenuItem
            // 
            this.bangĐiêmToolStripMenuItem.Name = "bangĐiêmToolStripMenuItem";
            this.bangĐiêmToolStripMenuItem.Size = new System.Drawing.Size(115, 29);
            this.bangĐiêmToolStripMenuItem.Text = "Bảng Điểm";
            this.bangĐiêmToolStripMenuItem.Click += new System.EventHandler(this.Click_toShowDiem);
            // 
            // timKiêmVaLocToolStripMenuItem
            // 
            this.timKiêmVaLocToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.điêmTheoMônKhoangToolStripMenuItem,
            this.điểmChiTiếtHọcSinhToolStripMenuItem});
            this.timKiêmVaLocToolStripMenuItem.Name = "timKiêmVaLocToolStripMenuItem";
            this.timKiêmVaLocToolStripMenuItem.Size = new System.Drawing.Size(155, 29);
            this.timKiêmVaLocToolStripMenuItem.Text = "Tìm kiếm và Lọc";
            // 
            // điêmTheoMônKhoangToolStripMenuItem
            // 
            this.điêmTheoMônKhoangToolStripMenuItem.Name = "điêmTheoMônKhoangToolStripMenuItem";
            this.điêmTheoMônKhoangToolStripMenuItem.Size = new System.Drawing.Size(322, 34);
            this.điêmTheoMônKhoangToolStripMenuItem.Text = "Điểm theo môn + Khoảng";
            this.điêmTheoMônKhoangToolStripMenuItem.Click += new System.EventHandler(this.KHOANG_DIEM_MON);
            // 
            // điểmChiTiếtHọcSinhToolStripMenuItem
            // 
            this.điểmChiTiếtHọcSinhToolStripMenuItem.Name = "điểmChiTiếtHọcSinhToolStripMenuItem";
            this.điểmChiTiếtHọcSinhToolStripMenuItem.Size = new System.Drawing.Size(322, 34);
            this.điểmChiTiếtHọcSinhToolStripMenuItem.Text = "Điểm chi tiết học sinh";
            this.điểmChiTiếtHọcSinhToolStripMenuItem.Click += new System.EventHandler(this.điểmChiTiếtHọcSinhToolStripMenuItem_Click);
            // 
            // FormParent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 562);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormParent";
            this.Text = "Form Parent";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem bangHocSinhToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bangMônHocToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bangGiaoViênToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bangLơpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bangĐiêmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timKiêmVaLocToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem điêmTheoMônKhoangToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem điểmChiTiếtHọcSinhToolStripMenuItem;
    }
}

