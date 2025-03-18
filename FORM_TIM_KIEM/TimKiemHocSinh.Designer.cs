namespace BTL.FORM_TIM_KIEM
{
    partial class TimKiemHocSinh
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
            this.clbTimKiem = new System.Windows.Forms.CheckedListBox();
            this.btnComfirm = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaHS = new System.Windows.Forms.TextBox();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.cbbLop = new System.Windows.Forms.ComboBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // clbTimKiem
            // 
            this.clbTimKiem.FormattingEnabled = true;
            this.clbTimKiem.Items.AddRange(new object[] {
            "Mã học sinh",
            "Tên Học Sinh",
            "Lớp"});
            this.clbTimKiem.Location = new System.Drawing.Point(14, 15);
            this.clbTimKiem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.clbTimKiem.Name = "clbTimKiem";
            this.clbTimKiem.Size = new System.Drawing.Size(241, 188);
            this.clbTimKiem.TabIndex = 1;
            // 
            // btnComfirm
            // 
            this.btnComfirm.Location = new System.Drawing.Point(111, 170);
            this.btnComfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnComfirm.Name = "btnComfirm";
            this.btnComfirm.Size = new System.Drawing.Size(128, 29);
            this.btnComfirm.TabIndex = 2;
            this.btnComfirm.Text = "Xác Nhận";
            this.btnComfirm.UseVisualStyleBackColor = true;
            this.btnComfirm.Click += new System.EventHandler(this.click_button_choi);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(325, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mã học sinh";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(325, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tên Học Sinh";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(325, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Lớp";
            // 
            // txtMaHS
            // 
            this.txtMaHS.Location = new System.Drawing.Point(516, 20);
            this.txtMaHS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMaHS.Name = "txtMaHS";
            this.txtMaHS.Size = new System.Drawing.Size(112, 26);
            this.txtMaHS.TabIndex = 6;
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(516, 82);
            this.txtHoTen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(112, 26);
            this.txtHoTen.TabIndex = 7;
            // 
            // cbbLop
            // 
            this.cbbLop.FormattingEnabled = true;
            this.cbbLop.Location = new System.Drawing.Point(516, 148);
            this.cbbLop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbbLop.Name = "cbbLop";
            this.cbbLop.Size = new System.Drawing.Size(136, 28);
            this.cbbLop.TabIndex = 8;
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(756, 151);
            this.btnFind.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(84, 29);
            this.btnFind.TabIndex = 9;
            this.btnFind.Text = "Tìm Kiếm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.cLICK_to_SEND_TOfORM2);
            // 
            // TimKiemHocSinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 562);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.cbbLop);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.txtMaHS);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnComfirm);
            this.Controls.Add(this.clbTimKiem);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TimKiemHocSinh";
            this.Text = "Tìm Kiếm Học Sinh";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckedListBox clbTimKiem;
        private System.Windows.Forms.Button btnComfirm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMaHS;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.ComboBox cbbLop;
        private System.Windows.Forms.Button btnFind;
    }
}