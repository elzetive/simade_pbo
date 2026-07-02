namespace simade_pbo
{
    partial class FormBarangKades
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
            this.dgvBarang = new System.Windows.Forms.DataGridView();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnRiwayat = new System.Windows.Forms.Button();
            this.btnBarang = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.panelPinjam = new System.Windows.Forms.Panel();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lbStatus = new System.Windows.Forms.Label();
            this.txtKondisiBagus = new System.Windows.Forms.TextBox();
            this.lbKondisi = new System.Windows.Forms.Label();
            this.txtKategori = new System.Windows.Forms.TextBox();
            this.lbKategori = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.lbNama = new System.Windows.Forms.Label();
            this.lblDetailData = new System.Windows.Forms.Label();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKondisiRusak = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.colNama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKategori = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colJumlah = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKondisiBagus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKondisiRusak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarang)).BeginInit();
            this.panelSidebar.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelPinjam.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvBarang
            // 
            this.dgvBarang.AllowUserToAddRows = false;
            this.dgvBarang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBarang.BackgroundColor = System.Drawing.Color.White;
            this.dgvBarang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBarang.ColumnHeadersHeight = 35;
            this.dgvBarang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBarang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNama,
            this.colKategori,
            this.colJumlah,
            this.colKondisiBagus,
            this.colKondisiRusak,
            this.colStatus});
            this.dgvBarang.Location = new System.Drawing.Point(216, 97);
            this.dgvBarang.Name = "dgvBarang";
            this.dgvBarang.ReadOnly = true;
            this.dgvBarang.RowHeadersWidth = 51;
            this.dgvBarang.Size = new System.Drawing.Size(501, 415);
            this.dgvBarang.TabIndex = 7;
            this.dgvBarang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBarang_CellClick);
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.panelSidebar.Controls.Add(this.btnLogout);
            this.panelSidebar.Controls.Add(this.btnRiwayat);
            this.panelSidebar.Controls.Add(this.btnBarang);
            this.panelSidebar.Controls.Add(this.btnDashboard);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(192, 589);
            this.panelSidebar.TabIndex = 12;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(22, 475);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(158, 37);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Log Out";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click_1);
            // 
            // btnRiwayat
            // 
            this.btnRiwayat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnRiwayat.FlatAppearance.BorderSize = 0;
            this.btnRiwayat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRiwayat.ForeColor = System.Drawing.Color.White;
            this.btnRiwayat.Location = new System.Drawing.Point(22, 242);
            this.btnRiwayat.Name = "btnRiwayat";
            this.btnRiwayat.Size = new System.Drawing.Size(158, 37);
            this.btnRiwayat.TabIndex = 3;
            this.btnRiwayat.Text = "Riwayat Peminjaman";
            this.btnRiwayat.UseVisualStyleBackColor = false;
            this.btnRiwayat.Click += new System.EventHandler(this.btnRiwayat_Click_1);
            // 
            // btnBarang
            // 
            this.btnBarang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnBarang.FlatAppearance.BorderSize = 0;
            this.btnBarang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBarang.ForeColor = System.Drawing.Color.White;
            this.btnBarang.Location = new System.Drawing.Point(22, 169);
            this.btnBarang.Name = "btnBarang";
            this.btnBarang.Size = new System.Drawing.Size(158, 37);
            this.btnBarang.TabIndex = 2;
            this.btnBarang.Text = "Lihat Barang";
            this.btnBarang.UseVisualStyleBackColor = false;
            this.btnBarang.Click += new System.EventHandler(this.btnBarang_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(22, 97);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(158, 37);
            this.btnDashboard.TabIndex = 1;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.UseVisualStyleBackColor = false;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click_1);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.btnExit);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(192, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(2);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(854, 57);
            this.panelHeader.TabIndex = 15;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblTitle.Location = new System.Drawing.Point(19, 15);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(285, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "SIMADE - Data Barang Desa";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Red;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(808, 10);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(34, 24);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panelPinjam
            // 
            this.panelPinjam.BackColor = System.Drawing.Color.White;
            this.panelPinjam.Controls.Add(this.txtKondisiRusak);
            this.panelPinjam.Controls.Add(this.label2);
            this.panelPinjam.Controls.Add(this.txtJumlah);
            this.panelPinjam.Controls.Add(this.label1);
            this.panelPinjam.Controls.Add(this.txtStatus);
            this.panelPinjam.Controls.Add(this.lbStatus);
            this.panelPinjam.Controls.Add(this.txtKondisiBagus);
            this.panelPinjam.Controls.Add(this.lbKondisi);
            this.panelPinjam.Controls.Add(this.txtKategori);
            this.panelPinjam.Controls.Add(this.lbKategori);
            this.panelPinjam.Controls.Add(this.txtNama);
            this.panelPinjam.Controls.Add(this.lbNama);
            this.panelPinjam.Controls.Add(this.lblDetailData);
            this.panelPinjam.Location = new System.Drawing.Point(730, 97);
            this.panelPinjam.Margin = new System.Windows.Forms.Padding(2);
            this.panelPinjam.Name = "panelPinjam";
            this.panelPinjam.Size = new System.Drawing.Size(304, 415);
            this.panelPinjam.TabIndex = 16;
            // 
            // txtStatus
            // 
            this.txtStatus.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtStatus.Location = new System.Drawing.Point(21, 349);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(2);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(267, 27);
            this.txtStatus.TabIndex = 10;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lbStatus.Location = new System.Drawing.Point(17, 327);
            this.lbStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(49, 19);
            this.lbStatus.TabIndex = 9;
            this.lbStatus.Text = "Status";
            // 
            // txtKondisiBagus
            // 
            this.txtKondisiBagus.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtKondisiBagus.Location = new System.Drawing.Point(21, 234);
            this.txtKondisiBagus.Margin = new System.Windows.Forms.Padding(2);
            this.txtKondisiBagus.Name = "txtKondisiBagus";
            this.txtKondisiBagus.ReadOnly = true;
            this.txtKondisiBagus.Size = new System.Drawing.Size(267, 27);
            this.txtKondisiBagus.TabIndex = 8;
            // 
            // lbKondisi
            // 
            this.lbKondisi.AutoSize = true;
            this.lbKondisi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lbKondisi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lbKondisi.Location = new System.Drawing.Point(17, 213);
            this.lbKondisi.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbKondisi.Name = "lbKondisi";
            this.lbKondisi.Size = new System.Drawing.Size(102, 19);
            this.lbKondisi.TabIndex = 7;
            this.lbKondisi.Text = "Kondisi Bagus";
            // 
            // txtKategori
            // 
            this.txtKategori.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtKategori.Location = new System.Drawing.Point(21, 118);
            this.txtKategori.Margin = new System.Windows.Forms.Padding(2);
            this.txtKategori.Name = "txtKategori";
            this.txtKategori.ReadOnly = true;
            this.txtKategori.Size = new System.Drawing.Size(267, 27);
            this.txtKategori.TabIndex = 6;
            // 
            // lbKategori
            // 
            this.lbKategori.AutoSize = true;
            this.lbKategori.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lbKategori.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lbKategori.Location = new System.Drawing.Point(17, 95);
            this.lbKategori.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbKategori.Name = "lbKategori";
            this.lbKategori.Size = new System.Drawing.Size(67, 19);
            this.lbKategori.TabIndex = 5;
            this.lbKategori.Text = "Kategori";
            // 
            // txtNama
            // 
            this.txtNama.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNama.Location = new System.Drawing.Point(19, 61);
            this.txtNama.Margin = new System.Windows.Forms.Padding(2);
            this.txtNama.Name = "txtNama";
            this.txtNama.ReadOnly = true;
            this.txtNama.Size = new System.Drawing.Size(267, 27);
            this.txtNama.TabIndex = 4;
            // 
            // lbNama
            // 
            this.lbNama.AutoSize = true;
            this.lbNama.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lbNama.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lbNama.Location = new System.Drawing.Point(15, 37);
            this.lbNama.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbNama.Name = "lbNama";
            this.lbNama.Size = new System.Drawing.Size(101, 19);
            this.lbNama.TabIndex = 3;
            this.lbNama.Text = "Nama Barang";
            // 
            // lblDetailData
            // 
            this.lblDetailData.AutoSize = true;
            this.lblDetailData.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDetailData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblDetailData.Location = new System.Drawing.Point(15, 8);
            this.lblDetailData.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDetailData.Name = "lblDetailData";
            this.lblDetailData.Size = new System.Drawing.Size(154, 21);
            this.lblDetailData.TabIndex = 0;
            this.lblDetailData.Text = "Detail Data Barang";
            // 
            // txtJumlah
            // 
            this.txtJumlah.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtJumlah.Location = new System.Drawing.Point(21, 176);
            this.txtJumlah.Margin = new System.Windows.Forms.Padding(2);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.ReadOnly = true;
            this.txtJumlah.Size = new System.Drawing.Size(267, 27);
            this.txtJumlah.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.label1.Location = new System.Drawing.Point(17, 153);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 19);
            this.label1.TabIndex = 11;
            this.label1.Text = "Jumlah Barang";
            // 
            // txtKondisiRusak
            // 
            this.txtKondisiRusak.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtKondisiRusak.Location = new System.Drawing.Point(21, 290);
            this.txtKondisiRusak.Margin = new System.Windows.Forms.Padding(2);
            this.txtKondisiRusak.Name = "txtKondisiRusak";
            this.txtKondisiRusak.ReadOnly = true;
            this.txtKondisiRusak.Size = new System.Drawing.Size(267, 27);
            this.txtKondisiRusak.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.label2.Location = new System.Drawing.Point(17, 269);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 19);
            this.label2.TabIndex = 13;
            this.label2.Text = "Kondisi Rusak";
            // 
            // colNama
            // 
            this.colNama.HeaderText = "Nama Barang";
            this.colNama.MinimumWidth = 6;
            this.colNama.Name = "colNama";
            this.colNama.ReadOnly = true;
            // 
            // colKategori
            // 
            this.colKategori.HeaderText = "Kategori";
            this.colKategori.Name = "colKategori";
            this.colKategori.ReadOnly = true;
            this.colKategori.Visible = false;
            // 
            // colJumlah
            // 
            this.colJumlah.HeaderText = "Jumlah Barang";
            this.colJumlah.Name = "colJumlah";
            this.colJumlah.ReadOnly = true;
            // 
            // colKondisiBagus
            // 
            this.colKondisiBagus.HeaderText = "Kondisi Bagus";
            this.colKondisiBagus.Name = "colKondisiBagus";
            this.colKondisiBagus.ReadOnly = true;
            // 
            // colKondisiRusak
            // 
            this.colKondisiRusak.HeaderText = "Kondisi Rusak";
            this.colKondisiRusak.Name = "colKondisiRusak";
            this.colKondisiRusak.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Visible = false;
            // 
            // FormBarangKades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1046, 589);
            this.Controls.Add(this.panelPinjam);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.dgvBarang);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormBarangKades";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Barang Kades";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarang)).EndInit();
            this.panelSidebar.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelPinjam.ResumeLayout(false);
            this.panelPinjam.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvBarang;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnRiwayat;
        private System.Windows.Forms.Button btnBarang;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panelPinjam;
        private System.Windows.Forms.Label lblDetailData;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.TextBox txtKondisiBagus;
        private System.Windows.Forms.Label lbKondisi;
        private System.Windows.Forms.TextBox txtKategori;
        private System.Windows.Forms.Label lbKategori;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.Label lbNama;
        private System.Windows.Forms.TextBox txtKondisiRusak;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNama;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKategori;
        private System.Windows.Forms.DataGridViewTextBoxColumn colJumlah;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKondisiBagus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKondisiRusak;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
    }
}