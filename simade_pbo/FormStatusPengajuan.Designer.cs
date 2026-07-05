namespace simade_pbo
{
    partial class FormStatusPengajuan
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.lblNamaWarga = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.btnMenuDashboard = new System.Windows.Forms.Button();
            this.btnMenuStatus = new System.Windows.Forms.Button();
            this.btnMenuRiwayat = new System.Windows.Forms.Button();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.dgvPengajuan = new System.Windows.Forms.DataGridView();
            this.colNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIdPinjam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTglPinjam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatusUtama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTabel = new System.Windows.Forms.Label();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.lblStatusBadge = new System.Windows.Forms.Label();
            this.lblTxtStatus = new System.Windows.Forms.Label();
            this.dgvDetailBarang = new System.Windows.Forms.DataGridView();
            this.colDetailNama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetailJumlah = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblFormDetail = new System.Windows.Forms.Label();
            this.panelSidebar.SuspendLayout();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPengajuan)).BeginInit();
            this.panelDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetailBarang)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.panelSidebar.Controls.Add(this.lblNamaWarga);
            this.panelSidebar.Controls.Add(this.lblRole);
            this.panelSidebar.Controls.Add(this.btnMenuDashboard);
            this.panelSidebar.Controls.Add(this.btnMenuStatus);
            this.panelSidebar.Controls.Add(this.btnMenuRiwayat);
            this.panelSidebar.Controls.Add(this.btnLogOut);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(192, 589);
            this.panelSidebar.TabIndex = 0;
            // 
            // lblNamaWarga
            // 
            this.lblNamaWarga.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblNamaWarga.ForeColor = System.Drawing.Color.White;
            this.lblNamaWarga.Location = new System.Drawing.Point(9, 25);
            this.lblNamaWarga.Name = "lblNamaWarga";
            this.lblNamaWarga.Size = new System.Drawing.Size(177, 23);
            this.lblNamaWarga.TabIndex = 0;
            this.lblNamaWarga.Text = "Halo, Warga!";
            this.lblNamaWarga.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRole
            // 
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblRole.Location = new System.Drawing.Point(9, 48);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(177, 19);
            this.lblRole.TabIndex = 1;
            this.lblRole.Text = "User";
            this.lblRole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnMenuDashboard
            // 
            this.btnMenuDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnMenuDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuDashboard.FlatAppearance.BorderSize = 0; // PERBAIKAN: Menghapus border putih
            this.btnMenuDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuDashboard.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnMenuDashboard.ForeColor = System.Drawing.Color.White;
            this.btnMenuDashboard.Location = new System.Drawing.Point(16, 95);
            this.btnMenuDashboard.Name = "btnMenuDashboard";
            this.btnMenuDashboard.Size = new System.Drawing.Size(160, 38);
            this.btnMenuDashboard.TabIndex = 2;
            this.btnMenuDashboard.Text = "Peminjaman Aset";
            this.btnMenuDashboard.UseVisualStyleBackColor = false;
            // 
            // btnMenuStatus
            // 
            this.btnMenuStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnMenuStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuStatus.FlatAppearance.BorderSize = 0; // PERBAIKAN: Menghapus border putih
            this.btnMenuStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnMenuStatus.ForeColor = System.Drawing.Color.White;
            this.btnMenuStatus.Location = new System.Drawing.Point(16, 143);
            this.btnMenuStatus.Name = "btnMenuStatus";
            this.btnMenuStatus.Size = new System.Drawing.Size(160, 38);
            this.btnMenuStatus.TabIndex = 3;
            this.btnMenuStatus.Text = "Status Pengajuan";
            this.btnMenuStatus.UseVisualStyleBackColor = false;
            // 
            // btnMenuRiwayat
            // 
            this.btnMenuRiwayat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnMenuRiwayat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuRiwayat.FlatAppearance.BorderSize = 0; // PERBAIKAN: Menghapus border putih
            this.btnMenuRiwayat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuRiwayat.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnMenuRiwayat.ForeColor = System.Drawing.Color.White;
            this.btnMenuRiwayat.Location = new System.Drawing.Point(16, 191);
            this.btnMenuRiwayat.Name = "btnMenuRiwayat";
            this.btnMenuRiwayat.Size = new System.Drawing.Size(160, 38);
            this.btnMenuRiwayat.TabIndex = 4;
            this.btnMenuRiwayat.Text = "Riwayat Pinjam";
            this.btnMenuRiwayat.UseVisualStyleBackColor = false;
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnLogOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogOut.FlatAppearance.BorderSize = 0; // PERBAIKAN: Menghapus border putih
            this.btnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogOut.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogOut.ForeColor = System.Drawing.Color.White;
            this.btnLogOut.Location = new System.Drawing.Point(16, 520);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(160, 37);
            this.btnLogOut.TabIndex = 5;
            this.btnLogOut.Text = "Log Out";
            this.btnLogOut.UseVisualStyleBackColor = false;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.btnExit);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(192, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(854, 57);
            this.panelHeader.TabIndex = 6;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblTitle.Location = new System.Drawing.Point(19, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(305, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "SIMADE - Status Permohonan";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Red;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(808, 10);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(34, 24);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            // 
            // dgvPengajuan
            // 
            this.dgvPengajuan.AllowUserToAddRows = false;
            this.dgvPengajuan.AllowUserToDeleteRows = false;
            this.dgvPengajuan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPengajuan.BackgroundColor = System.Drawing.Color.White;
            this.dgvPengajuan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPengajuan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPengajuan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNo,
            this.colIdPinjam,
            this.colTglPinjam,
            this.colStatusUtama});
            this.dgvPengajuan.Location = new System.Drawing.Point(212, 114);
            this.dgvPengajuan.Name = "dgvPengajuan";
            this.dgvPengajuan.ReadOnly = true;
            this.dgvPengajuan.RowHeadersVisible = false;
            this.dgvPengajuan.Size = new System.Drawing.Size(397, 447);
            this.dgvPengajuan.TabIndex = 2;
            // 
            // colNo
            // 
            this.colNo.FillWeight = 40F;
            this.colNo.HeaderText = "No";
            this.colNo.Name = "colNo";
            this.colNo.ReadOnly = true;
            // 
            // colIdPinjam
            // 
            this.colIdPinjam.HeaderText = "Kode Nota";
            this.colIdPinjam.Name = "colIdPinjam";
            this.colIdPinjam.ReadOnly = true;
            // 
            // colTglPinjam
            // 
            this.colTglPinjam.HeaderText = "Tgl Pinjam";
            this.colTglPinjam.Name = "colTglPinjam";
            this.colTglPinjam.ReadOnly = true;
            // 
            // colStatusUtama
            // 
            this.colStatusUtama.HeaderText = "Status";
            this.colStatusUtama.Name = "colStatusUtama";
            this.colStatusUtama.ReadOnly = true;
            // 
            // lblTabel
            // 
            this.lblTabel.AutoSize = true;
            this.lblTabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblTabel.Location = new System.Drawing.Point(212, 81);
            this.lblTabel.Name = "lblTabel";
            this.lblTabel.Size = new System.Drawing.Size(176, 21);
            this.lblTabel.TabIndex = 5;
            this.lblTabel.Text = "Permohonan Berjalan";
            // 
            // panelDetail
            // 
            this.panelDetail.BackColor = System.Drawing.Color.White;
            this.panelDetail.Controls.Add(this.lblStatusBadge);
            this.panelDetail.Controls.Add(this.lblTxtStatus);
            this.panelDetail.Controls.Add(this.dgvDetailBarang);
            this.panelDetail.Controls.Add(this.lblFormDetail);
            this.panelDetail.Location = new System.Drawing.Point(629, 114);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(397, 447);
            this.panelDetail.TabIndex = 4;
            // 
            // lblStatusBadge
            // 
            this.lblStatusBadge.BackColor = System.Drawing.Color.Orange;
            this.lblStatusBadge.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblStatusBadge.ForeColor = System.Drawing.Color.White;
            this.lblStatusBadge.Location = new System.Drawing.Point(145, 342);
            this.lblStatusBadge.Name = "lblStatusBadge";
            this.lblStatusBadge.Size = new System.Drawing.Size(237, 26);
            this.lblStatusBadge.TabIndex = 0;
            this.lblStatusBadge.Text = "PENDING";
            this.lblStatusBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTxtStatus
            // 
            this.lblTxtStatus.AutoSize = true;
            this.lblTxtStatus.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTxtStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblTxtStatus.Location = new System.Drawing.Point(15, 345);
            this.lblTxtStatus.Name = "lblTxtStatus";
            this.lblTxtStatus.Size = new System.Drawing.Size(116, 20);
            this.lblTxtStatus.TabIndex = 1;
            this.lblTxtStatus.Text = "Status Berkas : ";
            // 
            // dgvDetailBarang
            // 
            this.dgvDetailBarang.AllowUserToAddRows = false;
            this.dgvDetailBarang.AllowUserToDeleteRows = false;
            this.dgvDetailBarang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetailBarang.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetailBarang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetailBarang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDetailNama,
            this.colDetailJumlah});
            this.dgvDetailBarang.Location = new System.Drawing.Point(15, 60);
            this.dgvDetailBarang.Name = "dgvDetailBarang";
            this.dgvDetailBarang.ReadOnly = true;
            this.dgvDetailBarang.RowHeadersVisible = false;
            this.dgvDetailBarang.Size = new System.Drawing.Size(367, 260);
            this.dgvDetailBarang.TabIndex = 1;
            // 
            // colDetailNama
            // 
            this.colDetailNama.HeaderText = "Nama Aset";
            this.colDetailNama.Name = "colDetailNama";
            this.colDetailNama.ReadOnly = true;
            // 
            // colDetailJumlah
            // 
            this.colDetailJumlah.HeaderText = "Jumlah";
            this.colDetailJumlah.Name = "colDetailJumlah";
            this.colDetailJumlah.ReadOnly = true;
            // 
            // lblFormDetail
            // 
            this.lblFormDetail.AutoSize = true;
            this.lblFormDetail.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblFormDetail.Location = new System.Drawing.Point(15, 20);
            this.lblFormDetail.Name = "lblFormDetail";
            this.lblFormDetail.Size = new System.Drawing.Size(153, 21);
            this.lblFormDetail.TabIndex = 2;
            this.lblFormDetail.Text = "Detail Item Pinjam";
            // 
            // FormStatusPengajuan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1046, 589);
            this.Controls.Add(this.panelDetail);
            this.Controls.Add(this.lblTabel);
            this.Controls.Add(this.dgvPengajuan);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormStatusPengajuan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Status Pengajuan SIMADE";
            this.panelSidebar.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPengajuan)).EndInit();
            this.panelDetail.ResumeLayout(false);
            this.panelDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetailBarang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Label lblNamaWarga;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Button btnMenuDashboard;
        private System.Windows.Forms.Button btnMenuStatus;
        private System.Windows.Forms.Button btnMenuRiwayat;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dgvPengajuan;
        private System.Windows.Forms.Label lblTabel;
        private System.Windows.Forms.Panel panelDetail;
        private System.Windows.Forms.Label lblFormDetail;
        private System.Windows.Forms.DataGridView dgvDetailBarang;
        private System.Windows.Forms.Label lblStatusBadge;
        private System.Windows.Forms.Label lblTxtStatus;

        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdPinjam;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTglPinjam;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatusUtama;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetailNama;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetailJumlah;
    }
}