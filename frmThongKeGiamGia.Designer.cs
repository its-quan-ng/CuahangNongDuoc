namespace CuahangNongduoc
{
    partial class frmThongKeGiamGia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThongKeGiamGia));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnInBaoCao = new System.Windows.Forms.Button();
            this.btnXemBaoCao = new System.Windows.Forms.Button();
            this.cmbNhanVien = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtDenNgay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabChietKhau = new System.Windows.Forms.TabPage();
            this.dgvChietKhau = new System.Windows.Forms.DataGridView();
            this.colSTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaPhieu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKhachHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChietKhau = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoTienGiam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNguoiTao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTongTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabGiamGia = new System.Windows.Forms.TabPage();
            this.dgvGiamGia = new System.Windows.Forms.DataGridView();
            this.lblTongKet = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabChietKhau.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChietKhau)).BeginInit();
            this.tabGiamGia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiamGia)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDong);
            this.groupBox1.Controls.Add(this.btnInBaoCao);
            this.groupBox1.Controls.Add(this.btnXemBaoCao);
            this.groupBox1.Controls.Add(this.cmbNhanVien);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtDenNgay);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtTuNgay);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(89, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(873, 147);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bộ lọc";
            // 
            // btnDong
            // 
            this.btnDong.Image = ((System.Drawing.Image)(resources.GetObject("btnDong.Image")));
            this.btnDong.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDong.Location = new System.Drawing.Point(678, 80);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(99, 32);
            this.btnDong.TabIndex = 1;
            this.btnDong.Text = "Đóng";
            this.btnDong.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnInBaoCao
            // 
            this.btnInBaoCao.Image = ((System.Drawing.Image)(resources.GetObject("btnInBaoCao.Image")));
            this.btnInBaoCao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInBaoCao.Location = new System.Drawing.Point(528, 79);
            this.btnInBaoCao.Name = "btnInBaoCao";
            this.btnInBaoCao.Size = new System.Drawing.Size(123, 35);
            this.btnInBaoCao.TabIndex = 1;
            this.btnInBaoCao.Text = "In báo cáo";
            this.btnInBaoCao.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInBaoCao.UseVisualStyleBackColor = true;
            this.btnInBaoCao.Click += new System.EventHandler(this.btnInBaoCao_Click);
            // 
            // btnXemBaoCao
            // 
            this.btnXemBaoCao.Image = ((System.Drawing.Image)(resources.GetObject("btnXemBaoCao.Image")));
            this.btnXemBaoCao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXemBaoCao.Location = new System.Drawing.Point(379, 79);
            this.btnXemBaoCao.Name = "btnXemBaoCao";
            this.btnXemBaoCao.Size = new System.Drawing.Size(132, 35);
            this.btnXemBaoCao.TabIndex = 6;
            this.btnXemBaoCao.Text = "Xem báo cáo";
            this.btnXemBaoCao.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnXemBaoCao.UseVisualStyleBackColor = true;
            this.btnXemBaoCao.Click += new System.EventHandler(this.btnXemBaoCao_Click);
            // 
            // cmbNhanVien
            // 
            this.cmbNhanVien.FormattingEnabled = true;
            this.cmbNhanVien.Location = new System.Drawing.Point(109, 74);
            this.cmbNhanVien.Name = "cmbNhanVien";
            this.cmbNhanVien.Size = new System.Drawing.Size(200, 24);
            this.cmbNhanVien.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nhân viên :";
            // 
            // dtDenNgay
            // 
            this.dtDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDenNgay.Location = new System.Drawing.Point(465, 35);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.Size = new System.Drawing.Size(110, 22);
            this.dtDenNgay.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(389, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến ngày :";
            // 
            // dtTuNgay
            // 
            this.dtTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTuNgay.Location = new System.Drawing.Point(109, 34);
            this.dtTuNgay.Name = "dtTuNgay";
            this.dtTuNgay.Size = new System.Drawing.Size(114, 22);
            this.dtTuNgay.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ ngày :";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl);
            this.panel1.Location = new System.Drawing.Point(3, 201);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1076, 312);
            this.panel1.TabIndex = 1;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabChietKhau);
            this.tabControl.Controls.Add(this.tabGiamGia);
            this.tabControl.Location = new System.Drawing.Point(9, 19);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1058, 368);
            this.tabControl.TabIndex = 0;
            // 
            // tabChietKhau
            // 
            this.tabChietKhau.Controls.Add(this.dgvChietKhau);
            this.tabChietKhau.ImeMode = System.Windows.Forms.ImeMode.On;
            this.tabChietKhau.Location = new System.Drawing.Point(4, 25);
            this.tabChietKhau.Name = "tabChietKhau";
            this.tabChietKhau.Padding = new System.Windows.Forms.Padding(3);
            this.tabChietKhau.Size = new System.Drawing.Size(1050, 339);
            this.tabChietKhau.TabIndex = 0;
            this.tabChietKhau.Text = "Chiết khấu";
            this.tabChietKhau.UseVisualStyleBackColor = true;
            // 
            // dgvChietKhau
            // 
            this.dgvChietKhau.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChietKhau.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSTT,
            this.colNgayBan,
            this.colMaPhieu,
            this.colKhachHang,
            this.colChietKhau,
            this.colSoTienGiam,
            this.colNguoiTao,
            this.colTongTien});
            this.dgvChietKhau.Location = new System.Drawing.Point(6, 6);
            this.dgvChietKhau.Name = "dgvChietKhau";
            this.dgvChietKhau.RowHeadersWidth = 51;
            this.dgvChietKhau.RowTemplate.Height = 24;
            this.dgvChietKhau.Size = new System.Drawing.Size(1038, 233);
            this.dgvChietKhau.TabIndex = 0;
            // 
            // colSTT
            // 
            this.colSTT.DataPropertyName = "STT";
            this.colSTT.HeaderText = "STT";
            this.colSTT.MinimumWidth = 6;
            this.colSTT.Name = "colSTT";
            this.colSTT.ReadOnly = true;
            this.colSTT.Width = 125;
            // 
            // colNgayBan
            // 
            this.colNgayBan.DataPropertyName = "Ngay_Ban";
            this.colNgayBan.HeaderText = "Ngày bán";
            this.colNgayBan.MinimumWidth = 6;
            this.colNgayBan.Name = "colNgayBan";
            this.colNgayBan.ReadOnly = true;
            this.colNgayBan.Width = 125;
            // 
            // colMaPhieu
            // 
            this.colMaPhieu.DataPropertyName = "Ma_Phieu";
            this.colMaPhieu.HeaderText = "Mã phiếu";
            this.colMaPhieu.MinimumWidth = 6;
            this.colMaPhieu.Name = "colMaPhieu";
            this.colMaPhieu.ReadOnly = true;
            this.colMaPhieu.Width = 125;
            // 
            // colKhachHang
            // 
            this.colKhachHang.DataPropertyName = "Khach_Hang";
            this.colKhachHang.HeaderText = "Khách hàng";
            this.colKhachHang.MinimumWidth = 6;
            this.colKhachHang.Name = "colKhachHang";
            this.colKhachHang.ReadOnly = true;
            this.colKhachHang.Width = 125;
            // 
            // colChietKhau
            // 
            this.colChietKhau.DataPropertyName = "Chiet_Khau_Percent";
            this.colChietKhau.HeaderText = "Chiết khấu (%)";
            this.colChietKhau.MinimumWidth = 6;
            this.colChietKhau.Name = "colChietKhau";
            this.colChietKhau.Width = 125;
            // 
            // colSoTienGiam
            // 
            this.colSoTienGiam.DataPropertyName = "So_Tien_Giam";
            this.colSoTienGiam.HeaderText = "Số tiền giảm (VND)";
            this.colSoTienGiam.MinimumWidth = 6;
            this.colSoTienGiam.Name = "colSoTienGiam";
            this.colSoTienGiam.Width = 125;
            // 
            // colNguoiTao
            // 
            this.colNguoiTao.DataPropertyName = "Nguoi_Tao";
            this.colNguoiTao.HeaderText = "Người tạo";
            this.colNguoiTao.MinimumWidth = 6;
            this.colNguoiTao.Name = "colNguoiTao";
            this.colNguoiTao.Width = 125;
            // 
            // colTongTien
            // 
            this.colTongTien.DataPropertyName = "Tong_Tien";
            this.colTongTien.HeaderText = "Tổng tiền";
            this.colTongTien.MinimumWidth = 6;
            this.colTongTien.Name = "colTongTien";
            this.colTongTien.Width = 125;
            // 
            // tabGiamGia
            // 
            this.tabGiamGia.Controls.Add(this.dgvGiamGia);
            this.tabGiamGia.Location = new System.Drawing.Point(4, 25);
            this.tabGiamGia.Name = "tabGiamGia";
            this.tabGiamGia.Padding = new System.Windows.Forms.Padding(3);
            this.tabGiamGia.Size = new System.Drawing.Size(1050, 339);
            this.tabGiamGia.TabIndex = 1;
            this.tabGiamGia.Text = "Giảm giá";
            this.tabGiamGia.UseVisualStyleBackColor = true;
            // 
            // dgvGiamGia
            // 
            this.dgvGiamGia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGiamGia.Location = new System.Drawing.Point(6, 6);
            this.dgvGiamGia.Name = "dgvGiamGia";
            this.dgvGiamGia.RowHeadersWidth = 51;
            this.dgvGiamGia.RowTemplate.Height = 24;
            this.dgvGiamGia.Size = new System.Drawing.Size(1030, 327);
            this.dgvGiamGia.TabIndex = 0;
            // 
            // lblTongKet
            // 
            this.lblTongKet.Location = new System.Drawing.Point(78, 529);
            this.lblTongKet.Name = "lblTongKet";
            this.lblTongKet.Size = new System.Drawing.Size(937, 22);
            this.lblTongKet.TabIndex = 7;
            // 
            // frmThongKeGiamGia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 603);
            this.Controls.Add(this.lblTongKet);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmThongKeGiamGia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê giảm giá";
            this.Load += new System.EventHandler(this.frmThongKeGiamGia_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabChietKhau.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChietKhau)).EndInit();
            this.tabGiamGia.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiamGia)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbNhanVien;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtDenNgay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtTuNgay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnXemBaoCao;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnInBaoCao;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabChietKhau;
        private System.Windows.Forms.DataGridView dgvChietKhau;
        private System.Windows.Forms.TabPage tabGiamGia;
        private System.Windows.Forms.DataGridView dgvGiamGia;
        private System.Windows.Forms.TextBox lblTongKet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayBan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaPhieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKhachHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChietKhau;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoTienGiam;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNguoiTao;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTongTien;
    }
}