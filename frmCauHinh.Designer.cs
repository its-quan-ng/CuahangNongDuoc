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
            this.grpPhuongPhapXuatKho = new System.Windows.Forms.GroupBox();
            this.radChiDinh = new System.Windows.Forms.RadioButton();
            this.radFIFO = new System.Windows.Forms.RadioButton();
            this.grpPhuongPhapTinhGia = new System.Windows.Forms.GroupBox();
            this.radAverage = new System.Windows.Forms.RadioButton();
            this.radFIFOGia = new System.Windows.Forms.RadioButton();
            this.grpTuyChon = new System.Windows.Forms.GroupBox();
            this.chkHienThiLo = new System.Windows.Forms.CheckBox();
            this.chkTuDongPhanLo = new System.Windows.Forms.CheckBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.grpPhuongPhapXuatKho.SuspendLayout();
            this.grpPhuongPhapTinhGia.SuspendLayout();
            this.grpTuyChon.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpPhuongPhapXuatKho
            // 
            this.grpPhuongPhapXuatKho.Controls.Add(this.radChiDinh);
            this.grpPhuongPhapXuatKho.Controls.Add(this.radFIFO);
            this.grpPhuongPhapXuatKho.Location = new System.Drawing.Point(20, 20);
            this.grpPhuongPhapXuatKho.Name = "grpPhuongPhapXuatKho";
            this.grpPhuongPhapXuatKho.Size = new System.Drawing.Size(400, 80);
            this.grpPhuongPhapXuatKho.TabIndex = 0;
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
            // grpPhuongPhapTinhGia
            // 
            this.grpPhuongPhapTinhGia.Controls.Add(this.radAverage);
            this.grpPhuongPhapTinhGia.Controls.Add(this.radFIFOGia);
            this.grpPhuongPhapTinhGia.Location = new System.Drawing.Point(20, 110);
            this.grpPhuongPhapTinhGia.Name = "grpPhuongPhapTinhGia";
            this.grpPhuongPhapTinhGia.Size = new System.Drawing.Size(400, 80);
            this.grpPhuongPhapTinhGia.TabIndex = 1;
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
            // grpTuyChon
            // 
            this.grpTuyChon.Controls.Add(this.chkHienThiLo);
            this.grpTuyChon.Controls.Add(this.chkTuDongPhanLo);
            this.grpTuyChon.Location = new System.Drawing.Point(20, 200);
            this.grpTuyChon.Name = "grpTuyChon";
            this.grpTuyChon.Size = new System.Drawing.Size(400, 80);
            this.grpTuyChon.TabIndex = 2;
            this.grpTuyChon.TabStop = false;
            this.grpTuyChon.Text = "Tùy chọn ";
            // 
            // chkHienThiLo
            // 
            this.chkHienThiLo.AutoSize = true;
            this.chkHienThiLo.Checked = true;
            this.chkHienThiLo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHienThiLo.Location = new System.Drawing.Point(15, 50);
            this.chkHienThiLo.Name = "chkHienThiLo";
            this.chkHienThiLo.Size = new System.Drawing.Size(193, 20);
            this.chkHienThiLo.TabIndex = 0;
            this.chkHienThiLo.Text = "Hiển thị số lô trên phiếu xuất";
            this.chkHienThiLo.UseVisualStyleBackColor = true;
            // 
            // chkTuDongPhanLo
            // 
            this.chkTuDongPhanLo.AutoSize = true;
            this.chkTuDongPhanLo.Checked = true;
            this.chkTuDongPhanLo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTuDongPhanLo.Location = new System.Drawing.Point(15, 25);
            this.chkTuDongPhanLo.Name = "chkTuDongPhanLo";
            this.chkTuDongPhanLo.Size = new System.Drawing.Size(205, 20);
            this.chkTuDongPhanLo.TabIndex = 0;
            this.chkTuDongPhanLo.Text = "Tự động phân lô khi bán hàng";
            this.chkTuDongPhanLo.UseVisualStyleBackColor = true;
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.LightGreen;
            this.btnLuu.Location = new System.Drawing.Point(97, 300);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(80, 30);
            this.btnLuu.TabIndex = 3;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.OrangeRed;
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Location = new System.Drawing.Point(257, 300);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(80, 30);
            this.btnHuy.TabIndex = 3;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // frmCauHinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 433);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.grpTuyChon);
            this.Controls.Add(this.grpPhuongPhapTinhGia);
            this.Controls.Add(this.grpPhuongPhapXuatKho);
            this.Name = "frmCauHinh";
            this.Text = "Cấu Hình";
            this.grpPhuongPhapXuatKho.ResumeLayout(false);
            this.grpPhuongPhapXuatKho.PerformLayout();
            this.grpPhuongPhapTinhGia.ResumeLayout(false);
            this.grpPhuongPhapTinhGia.PerformLayout();
            this.grpTuyChon.ResumeLayout(false);
            this.grpTuyChon.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPhuongPhapXuatKho;
        private System.Windows.Forms.RadioButton radChiDinh;
        private System.Windows.Forms.RadioButton radFIFO;
        private System.Windows.Forms.GroupBox grpPhuongPhapTinhGia;
        private System.Windows.Forms.RadioButton radAverage;
        private System.Windows.Forms.RadioButton radFIFOGia;
        private System.Windows.Forms.GroupBox grpTuyChon;
        private System.Windows.Forms.CheckBox chkHienThiLo;
        private System.Windows.Forms.CheckBox chkTuDongPhanLo;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
    }
}