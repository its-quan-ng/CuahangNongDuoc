namespace CuahangNongduoc
{
    partial class frmCauHinh
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.grpPhuongPhapTinhGia = new System.Windows.Forms.GroupBox();
            this.radAverage = new System.Windows.Forms.RadioButton();
            this.radFIFOGia = new System.Windows.Forms.RadioButton();
            this.grpPhuongPhapXuatKho = new System.Windows.Forms.GroupBox();
            this.radChiDinh = new System.Windows.Forms.RadioButton();
            this.radFIFO = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.grpPhuongPhapTinhGia.SuspendLayout();
            this.grpPhuongPhapXuatKho.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnHuy);
            this.panel1.Controls.Add(this.btnLuu);
            this.panel1.Controls.Add(this.grpPhuongPhapTinhGia);
            this.panel1.Controls.Add(this.grpPhuongPhapXuatKho);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(432, 240);
            this.panel1.TabIndex = 0;
            // 
            // btnHuy
            // 
            this.btnHuy.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnHuy.BackColor = System.Drawing.Color.OrangeRed;
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Location = new System.Drawing.Point(253, 195);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(80, 30);
            this.btnHuy.TabIndex = 6;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            //
            // btnLuu
            // 
            this.btnLuu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLuu.BackColor = System.Drawing.Color.LightGreen;
            this.btnLuu.Location = new System.Drawing.Point(93, 195);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(80, 30);
            this.btnLuu.TabIndex = 7;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            //
            // grpPhuongPhapTinhGia
            // 
            this.grpPhuongPhapTinhGia.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grpPhuongPhapTinhGia.Controls.Add(this.radAverage);
            this.grpPhuongPhapTinhGia.Controls.Add(this.radFIFOGia);
            this.grpPhuongPhapTinhGia.Location = new System.Drawing.Point(16, 105);
            this.grpPhuongPhapTinhGia.Name = "grpPhuongPhapTinhGia";
            this.grpPhuongPhapTinhGia.Size = new System.Drawing.Size(400, 80);
            this.grpPhuongPhapTinhGia.TabIndex = 5;
            this.grpPhuongPhapTinhGia.TabStop = false;
            this.grpPhuongPhapTinhGia.Text = "Phương pháp tính giá xuất";
            // 
            // radAverage
            // 
            this.radAverage.AutoSize = true;
            this.radAverage.Checked = true;
            this.radAverage.Location = new System.Drawing.Point(15, 25);
            this.radAverage.Name = "radAverage";
            this.radAverage.Size = new System.Drawing.Size(225, 20);
            this.radAverage.TabIndex = 0;
            this.radAverage.TabStop = true;
            this.radAverage.Tag = "AVERAGE";
            this.radAverage.Text = "AVERAGE (Bình quân gia quyền)";
            this.radAverage.UseVisualStyleBackColor = true;
            // 
            // radFIFOGia
            // 
            this.radFIFOGia.AutoSize = true;
            this.radFIFOGia.Location = new System.Drawing.Point(15, 50);
            this.radFIFOGia.Name = "radFIFOGia";
            this.radFIFOGia.Size = new System.Drawing.Size(194, 20);
            this.radFIFOGia.TabIndex = 0;
            this.radFIFOGia.Tag = "FIFO";
            this.radFIFOGia.Text = "FIFO (Theo giá nhập của lô)";
            this.radFIFOGia.UseVisualStyleBackColor = true;
            // 
            // grpPhuongPhapXuatKho
            // 
            this.grpPhuongPhapXuatKho.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grpPhuongPhapXuatKho.Controls.Add(this.radChiDinh);
            this.grpPhuongPhapXuatKho.Controls.Add(this.radFIFO);
            this.grpPhuongPhapXuatKho.Location = new System.Drawing.Point(16, 15);
            this.grpPhuongPhapXuatKho.Name = "grpPhuongPhapXuatKho";
            this.grpPhuongPhapXuatKho.Size = new System.Drawing.Size(400, 80);
            this.grpPhuongPhapXuatKho.TabIndex = 4;
            this.grpPhuongPhapXuatKho.TabStop = false;
            this.grpPhuongPhapXuatKho.Text = "Phương pháp xuất kho";
            // 
            // radChiDinh
            // 
            this.radChiDinh.AutoSize = true;
            this.radChiDinh.Location = new System.Drawing.Point(15, 50);
            this.radChiDinh.Name = "radChiDinh";
            this.radChiDinh.Size = new System.Drawing.Size(142, 20);
            this.radChiDinh.TabIndex = 0;
            this.radChiDinh.Tag = "CHI_DINH";
            this.radChiDinh.Text = "Chỉ định lô thủ công";
            this.radChiDinh.UseVisualStyleBackColor = true;
            // 
            // radFIFO
            // 
            this.radFIFO.AutoSize = true;
            this.radFIFO.Checked = true;
            this.radFIFO.Location = new System.Drawing.Point(15, 25);
            this.radFIFO.Name = "radFIFO";
            this.radFIFO.Size = new System.Drawing.Size(189, 20);
            this.radFIFO.TabIndex = 0;
            this.radFIFO.TabStop = true;
            this.radFIFO.Tag = "FIFO";
            this.radFIFO.Text = "FIFO(Nhập trước xuất trước)";
            this.radFIFO.UseVisualStyleBackColor = true;
            // 
            // frmCauHinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 240);
            this.Controls.Add(this.panel1);
            this.Name = "frmCauHinh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu hình";
            this.panel1.ResumeLayout(false);
            this.grpPhuongPhapTinhGia.ResumeLayout(false);
            this.grpPhuongPhapTinhGia.PerformLayout();
            this.grpPhuongPhapXuatKho.ResumeLayout(false);
            this.grpPhuongPhapXuatKho.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.GroupBox grpPhuongPhapTinhGia;
        private System.Windows.Forms.RadioButton radAverage;
        private System.Windows.Forms.RadioButton radFIFOGia;
        private System.Windows.Forms.GroupBox grpPhuongPhapXuatKho;
        private System.Windows.Forms.RadioButton radChiDinh;
        private System.Windows.Forms.RadioButton radFIFO;
    }
}