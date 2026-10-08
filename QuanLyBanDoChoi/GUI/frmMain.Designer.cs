namespace QuanLyBanDoChoi.GUI
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.pct5 = new System.Windows.Forms.PictureBox();
            this.ptb1 = new System.Windows.Forms.PictureBox();
            this.ptb2 = new System.Windows.Forms.PictureBox();
            this.ptb4 = new System.Windows.Forms.PictureBox();
            this.ptb3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.uC_BanHang1 = new QuanLyBanDoChoi.GUI.UC_BanHang();
            this.ucKhachHang1 = new QuanLyBanDoChoi.GUI.ucKhachHang();
            this.ucThongKe1 = new QuanLyBanDoChoi.GUI.ucThongKe();
            this.uC_SanPham1 = new QuanLyBanDoChoi.GUI.UC_SanPham();
            this.pnlMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pct5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptb1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptb2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptb4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptb3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.pnlMenu.Controls.Add(this.pct5);
            this.pnlMenu.Controls.Add(this.ptb1);
            this.pnlMenu.Controls.Add(this.ptb2);
            this.pnlMenu.Controls.Add(this.ptb4);
            this.pnlMenu.Controls.Add(this.ptb3);
            this.pnlMenu.Controls.Add(this.pictureBox1);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlMenu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(127, 718);
            this.pnlMenu.TabIndex = 0;
            this.pnlMenu.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMenu_Paint);
            // 
            // pct5
            // 
            this.pct5.Image = ((System.Drawing.Image)(resources.GetObject("pct5.Image")));
            this.pct5.Location = new System.Drawing.Point(19, 505);
            this.pct5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pct5.Name = "pct5";
            this.pct5.Size = new System.Drawing.Size(77, 54);
            this.pct5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pct5.TabIndex = 5;
            this.pct5.TabStop = false;
            this.pct5.Click += new System.EventHandler(this.pct5_Click);
            // 
            // ptb1
            // 
            this.ptb1.Image = ((System.Drawing.Image)(resources.GetObject("ptb1.Image")));
            this.ptb1.Location = new System.Drawing.Point(19, 110);
            this.ptb1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ptb1.Name = "ptb1";
            this.ptb1.Size = new System.Drawing.Size(77, 62);
            this.ptb1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptb1.TabIndex = 4;
            this.ptb1.TabStop = false;
            this.ptb1.Click += new System.EventHandler(this.ptb1_Click);
            // 
            // ptb2
            // 
            this.ptb2.Image = ((System.Drawing.Image)(resources.GetObject("ptb2.Image")));
            this.ptb2.Location = new System.Drawing.Point(19, 208);
            this.ptb2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ptb2.Name = "ptb2";
            this.ptb2.Size = new System.Drawing.Size(77, 54);
            this.ptb2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptb2.TabIndex = 3;
            this.ptb2.TabStop = false;
            this.ptb2.Click += new System.EventHandler(this.ptb2_Click);
            // 
            // ptb4
            // 
            this.ptb4.Image = ((System.Drawing.Image)(resources.GetObject("ptb4.Image")));
            this.ptb4.Location = new System.Drawing.Point(19, 407);
            this.ptb4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ptb4.Name = "ptb4";
            this.ptb4.Size = new System.Drawing.Size(77, 54);
            this.ptb4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptb4.TabIndex = 2;
            this.ptb4.TabStop = false;
            this.ptb4.Click += new System.EventHandler(this.ptb4_Click);
            // 
            // ptb3
            // 
            this.ptb3.Image = ((System.Drawing.Image)(resources.GetObject("ptb3.Image")));
            this.ptb3.Location = new System.Drawing.Point(19, 306);
            this.ptb3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ptb3.Name = "ptb3";
            this.ptb3.Size = new System.Drawing.Size(77, 54);
            this.ptb3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptb3.TabIndex = 1;
            this.ptb3.TabStop = false;
            this.ptb3.Click += new System.EventHandler(this.ptb3_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(127, 105);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.uC_SanPham1);
            this.pnlContent.Controls.Add(this.uC_BanHang1);
            this.pnlContent.Controls.Add(this.ucKhachHang1);
            this.pnlContent.Controls.Add(this.ucThongKe1);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlContent.Location = new System.Drawing.Point(127, 0);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1344, 718);
            this.pnlContent.TabIndex = 1;
            this.pnlContent.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlContent_Paint);
            // 
            // uC_BanHang1
            // 
            this.uC_BanHang1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_BanHang1.Location = new System.Drawing.Point(0, 0);
            this.uC_BanHang1.Margin = new System.Windows.Forms.Padding(5);
            this.uC_BanHang1.Name = "uC_BanHang1";
            this.uC_BanHang1.Size = new System.Drawing.Size(1344, 718);
            this.uC_BanHang1.TabIndex = 0;
            this.uC_BanHang1.Load += new System.EventHandler(this.uC_BanHang1_Load);
            // 
            // ucKhachHang1
            // 
            this.ucKhachHang1.Location = new System.Drawing.Point(1, 0);
            this.ucKhachHang1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ucKhachHang1.Name = "ucKhachHang1";
            this.ucKhachHang1.Size = new System.Drawing.Size(1344, 721);
            this.ucKhachHang1.TabIndex = 2;
            // 
            // ucThongKe1
            // 
            this.ucThongKe1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucThongKe1.Location = new System.Drawing.Point(0, 0);
            this.ucThongKe1.Name = "ucThongKe1";
            this.ucThongKe1.Size = new System.Drawing.Size(1344, 718);
            this.ucThongKe1.TabIndex = 3;
            this.ucThongKe1.Visible = false;
            // 
            // uC_SanPham1
            // 
            this.uC_SanPham1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_SanPham1.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.uC_SanPham1.Location = new System.Drawing.Point(0, 0);
            this.uC_SanPham1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.uC_SanPham1.Name = "uC_SanPham1";
            this.uC_SanPham1.Size = new System.Drawing.Size(1344, 718);
            this.uC_SanPham1.TabIndex = 1;
            this.uC_SanPham1.Load += new System.EventHandler(this.uC_SanPham1_Load);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1471, 718);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlMenu);
            this.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TOYSHOP-Quản lý đồ chơi";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.pnlMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pct5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptb1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptb2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptb4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptb3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.PictureBox ptb3;
        private System.Windows.Forms.PictureBox ptb2;
        private System.Windows.Forms.PictureBox ptb4;
        private System.Windows.Forms.PictureBox ptb1;
        private System.Windows.Forms.PictureBox pct5;
        private UC_BanHang uC_BanHang1;
        private UC_SanPham uC_SanPham1;
        private ucKhachHang ucKhachHang1;
        private ucThongKe ucThongKe1;
    }
}

