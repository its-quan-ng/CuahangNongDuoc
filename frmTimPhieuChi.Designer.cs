namespace CuahangNongduoc
{
    partial class frmTimPhieuChi
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLyDo = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.dtTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtDenNgay = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lý do chi";
            // 
            // cmbLyDo
            // 
            this.cmbLyDo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbLyDo.FormattingEnabled = true;
            this.cmbLyDo.Location = new System.Drawing.Point(112, 31);
            this.cmbLyDo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbLyDo.Name = "cmbLyDo";
            this.cmbLyDo.Size = new System.Drawing.Size(236, 24);
            this.cmbLyDo.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::CuahangNongduoc.Properties.Resources.stop;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(192, 112);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 43);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Hủy bỏ";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::CuahangNongduoc.Properties.Resources.Ok;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(84, 112);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 43);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "Đồng ý";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // dtTuNgay
            // 
            this.dtNgayChi.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtNgayChi.CustomFormat = "dd/MM/yyyy";
            this.dtNgayChi.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtNgayChi.Location = new System.Drawing.Point(112, 63);
            this.dtNgayChi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtNgayChi.Name = "dtNgayChi";
            this.dtNgayChi.Size = new System.Drawing.Size(139, 22);
            this.dtNgayChi.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 68);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Từ Ngày";
            // 
            // dtDenNgay
            // 
            this.dtDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDenNgay.Location = new System.Drawing.Point(128, 104);
            this.dtDenNgay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.Size = new System.Drawing.Size(156, 26);
            this.dtDenNgay.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 111);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Đến ngày";
            // 
            // frmTimPhieuChi
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(371, 170);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dtTuNgay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbLyDo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmTimPhieuChi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tìm phiếu chi";
            this.Load += new System.EventHandler(this.frmTimPhieuChi_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.DateTimePicker dtTuNgay;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox cmbLyDo;
        public System.Windows.Forms.DateTimePicker dtDenNgay;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.DateTimePicker dtNgayChi;
    }
}