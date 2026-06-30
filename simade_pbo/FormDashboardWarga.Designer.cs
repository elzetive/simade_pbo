namespace simade_pbo
{
    partial class FormDashboardWarga
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
            this.dgvBarang = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNamaAset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKategori = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStokTersedia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTabel = new System.Windows.Forms.Label();
            this.panelPinjam = new System.Windows.Forms.Panel();
            this.btnPinjam = new System.Windows.Forms.Button();
            this.dtpPinjam = new System.Windows.Forms.DateTimePicker();
            this.lblTglPinjam = new System.Windows.Forms.Label();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.lblJumlah = new System.Windows.Forms.Label();
            this.txtNamaBarang = new System.Windows.Forms.TextBox();
            this.lblNamaBarang = new System.Windows.Forms.Label();
            this.lblFormPinjam = new System.Windows.Forms.Label();
            this.dgvKeranjang = new System.Windows.Forms.DataGridView();
            this.colCartNama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCartQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAjukanFinal = new System.Windows.Forms.Button();
            this.lblCartTitle = new System.Windows.Forms.Label();
            this.panelSidebar.SuspendLayout();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarang)).BeginInit();
            this.panelPinjam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeranjang)).BeginInit();
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
            this.panelSidebar.Margin = new System.Windows.Forms.Padding(2);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(192, 589);
            this.panelSidebar.TabIndex = 0;
            // 
            // lblNamaWarga
            // 
            this.lblNamaWarga.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblNamaWarga.ForeColor = System.Drawing.Color.White;
            this.lblNamaWarga.Location = new System.Drawing.Point(9, 25);
            this.lblNamaWarga.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNamaWarga.Name = "lblNamaWarga";
            this.lblNamaWarga.Size = new System.Drawing.Size(177, 23);
            this.lblNamaWarga.TabIndex = 0;
            this.lblNamaWarga.Text = "Halo, Warga!";
            this.lblNamaWarga.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRole
            // 
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular);
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblRole.Location = new System.Drawing.Point(9, 48);
            this.lblRole.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(177, 19);
            this.lblRole.TabIndex = 1;
            this.lblRole.Text = "User";
            this.lblRole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnMenuDashboard
            // 
            this.btnMenuDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnMenuDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuDashboard.FlatAppearance.BorderSize = 0;
            this.btnMenuDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuDashboard.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnMenuDashboard.ForeColor = System.Drawing.Color.White;
            this.btnMenuDashboard.Location = new System.Drawing.Point(16, 95);
            this.btnMenuDashboard.Margin = new System.Windows.Forms.Padding(2);
            this.btnMenuDashboard.Name = "btnMenuDashboard";
            this.btnMenuDashboard.Size = new System.Drawing.Size(160, 38);
            this.btnMenuDashboard.TabIndex = 3;
            this.btnMenuDashboard.Text = "Peminjaman Aset";
            this.btnMenuDashboard.UseVisualStyleBackColor = false;
            // 
            // btnMenuStatus
            // 
            this.btnMenuStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnMenuStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuStatus.FlatAppearance.BorderSize = 0;
            this.btnMenuStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnMenuStatus.ForeColor = System.Drawing.Color.White;
            this.btnMenuStatus.Location = new System.Drawing.Point(16, 143);
            this.btnMenuStatus.Margin = new System.Windows.Forms.Padding(2);
            this.btnMenuStatus.Name = "btnMenuStatus";
            this.btnMenuStatus.Size = new System.Drawing.Size(160, 38);
            this.btnMenuStatus.TabIndex = 4;
            this.btnMenuStatus.Text = "Status Pengajuan";
            this.btnMenuStatus.UseVisualStyleBackColor = false;
            // 
            // btnMenuRiwayat
            // 
            this.btnMenuRiwayat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnMenuRiwayat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuRiwayat.FlatAppearance.BorderSize = 0;
            this.btnMenuRiwayat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuRiwayat.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnMenuRiwayat.ForeColor = System.Drawing.Color.White;
            this.btnMenuRiwayat.Location = new System.Drawing.Point(16, 191);
            this.btnMenuRiwayat.Margin = new System.Windows.Forms.Padding(2);
            this.btnMenuRiwayat.Name = "btnMenuRiwayat";
            this.btnMenuRiwayat.Size = new System.Drawing.Size(160, 38);
            this.btnMenuRiwayat.TabIndex = 5;
            this.btnMenuRiwayat.Text = "Riwayat Pinjam";
            this.btnMenuRiwayat.UseVisualStyleBackColor = false;
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnLogOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogOut.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogOut.ForeColor = System.Drawing.Color.White;
            this.btnLogOut.Location = new System.Drawing.Point(16, 520);
            this.btnLogOut.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(160, 37);
            this.btnLogOut.TabIndex = 2;
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
            this.panelHeader.Margin = new System.Windows.Forms.Padding(2);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(854, 57);
            this.panelHeader.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblTitle.Location = new System.Drawing.Point(19, 15);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(346, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "SIMADE - Dashboard Peminjaman";
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
            // 
            // dgvBarang
            // 
            this.dgvBarang.AllowUserToAddRows = false;
            this.dgvBarang.AllowUserToDeleteRows = false;
            this.dgvBarang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBarang.BackgroundColor = System.Drawing.Color.White;
            this.dgvBarang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBarang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBarang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colNamaAset,
            this.colKategori,
            this.colStokTersedia});
            this.dgvBarang.Location = new System.Drawing.Point(216, 114);
            this.dgvBarang.Margin = new System.Windows.Forms.Padding(2);
            this.dgvBarang.Name = "dgvBarang";
            this.dgvBarang.ReadOnly = true;
            this.dgvBarang.RowHeadersWidth = 51;
            this.dgvBarang.RowTemplate.Height = 24;
            this.dgvBarang.Size = new System.Drawing.Size(491, 447);
            this.dgvBarang.TabIndex = 2;
            // 
            // colId
            // 
            this.colId.FillWeight = 40F;
            this.colId.HeaderText = "No";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            // 
            // colNamaAset
            // 
            this.colNamaAset.HeaderText = "Nama Aset";
            this.colNamaAset.Name = "colNamaAset";
            this.colNamaAset.ReadOnly = true;
            // 
            // colKategori
            // 
            this.colKategori.HeaderText = "Kategori";
            this.colKategori.Name = "colKategori";
            this.colKategori.ReadOnly = true;
            // 
            // colStokTersedia
            // 
            this.colStokTersedia.HeaderText = "Jumlah Tersedia";
            this.colStokTersedia.Name = "colStokTersedia";
            this.colStokTersedia.ReadOnly = true;
            // 
            // lblTabel
            // 
            this.lblTabel.AutoSize = true;
            this.lblTabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblTabel.Location = new System.Drawing.Point(218, 81);
            this.lblTabel.Margin = new System.Windows.Forms.Padding(2);
            this.lblTabel.Name = "lblTabel";
            this.lblTabel.Size = new System.Drawing.Size(136, 21);
            this.lblTabel.TabIndex = 3;
            this.lblTabel.Text = "Daftar Aset Desa";
            // 
            // panelPinjam
            // 
            this.panelPinjam.BackColor = System.Drawing.Color.White;
            this.panelPinjam.Controls.Add(this.btnPinjam);
            this.panelPinjam.Controls.Add(this.dtpPinjam);
            this.panelPinjam.Controls.Add(this.lblTglPinjam);
            this.panelPinjam.Controls.Add(this.txtJumlah);
            this.panelPinjam.Controls.Add(this.lblJumlah);
            this.panelPinjam.Controls.Add(this.txtNamaBarang);
            this.panelPinjam.Controls.Add(this.lblNamaBarang);
            this.panelPinjam.Controls.Add(this.lblFormPinjam);
            this.panelPinjam.Location = new System.Drawing.Point(724, 114);
            this.panelPinjam.Margin = new System.Windows.Forms.Padding(2);
            this.panelPinjam.Name = "panelPinjam";
            this.panelPinjam.Size = new System.Drawing.Size(304, 252);
            this.panelPinjam.TabIndex = 4;
            // 
            // btnPinjam
            // 
            this.btnPinjam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnPinjam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPinjam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPinjam.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnPinjam.ForeColor = System.Drawing.Color.White;
            this.btnPinjam.Location = new System.Drawing.Point(19, 206);
            this.btnPinjam.Margin = new System.Windows.Forms.Padding(2);
            this.btnPinjam.Name = "btnPinjam";
            this.btnPinjam.Size = new System.Drawing.Size(266, 32);
            this.btnPinjam.TabIndex = 9;
            this.btnPinjam.Text = "Tambah ke List";
            this.btnPinjam.UseVisualStyleBackColor = false;
            // 
            // dtpPinjam
            // 
            this.dtpPinjam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpPinjam.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPinjam.Location = new System.Drawing.Point(19, 166);
            this.dtpPinjam.Margin = new System.Windows.Forms.Padding(2);
            this.dtpPinjam.Name = "dtpPinjam";
            this.dtpPinjam.Size = new System.Drawing.Size(267, 25);
            this.dtpPinjam.TabIndex = 6;
            // 
            // lblTglPinjam
            // 
            this.lblTglPinjam.AutoSize = true;
            this.lblTglPinjam.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTglPinjam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblTglPinjam.Location = new System.Drawing.Point(16, 146);
            this.lblTglPinjam.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTglPinjam.Name = "lblTglPinjam";
            this.lblTglPinjam.Size = new System.Drawing.Size(103, 17);
            this.lblTglPinjam.TabIndex = 5;
            this.lblTglPinjam.Text = "Tanggal Pinjam";
            // 
            // txtJumlah
            // 
            this.txtJumlah.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtJumlah.Location = new System.Drawing.Point(19, 114);
            this.txtJumlah.Margin = new System.Windows.Forms.Padding(2);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(267, 25);
            this.txtJumlah.TabIndex = 4;
            // 
            // lblJumlah
            // 
            this.lblJumlah.AutoSize = true;
            this.lblJumlah.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblJumlah.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblJumlah.Location = new System.Drawing.Point(16, 94);
            this.lblJumlah.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblJumlah.Name = "lblJumlah";
            this.lblJumlah.Size = new System.Drawing.Size(98, 17);
            this.lblJumlah.TabIndex = 3;
            this.lblJumlah.Text = "Jumlah Pinjam";
            // 
            // txtNamaBarang
            // 
            this.txtNamaBarang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNamaBarang.Location = new System.Drawing.Point(19, 64);
            this.txtNamaBarang.Margin = new System.Windows.Forms.Padding(2);
            this.txtNamaBarang.Name = "txtNamaBarang";
            this.txtNamaBarang.ReadOnly = true;
            this.txtNamaBarang.Size = new System.Drawing.Size(267, 25);
            this.txtNamaBarang.TabIndex = 2;
            // 
            // lblNamaBarang
            // 
            this.lblNamaBarang.AutoSize = true;
            this.lblNamaBarang.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblNamaBarang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblNamaBarang.Location = new System.Drawing.Point(16, 44);
            this.lblNamaBarang.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNamaBarang.Name = "lblNamaBarang";
            this.lblNamaBarang.Size = new System.Drawing.Size(91, 17);
            this.lblNamaBarang.TabIndex = 1;
            this.lblNamaBarang.Text = "Nama Barang";
            // 
            // lblFormPinjam
            // 
            this.lblFormPinjam.AutoSize = true;
            this.lblFormPinjam.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblFormPinjam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblFormPinjam.Location = new System.Drawing.Point(15, 12);
            this.lblFormPinjam.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFormPinjam.Name = "lblFormPinjam";
            this.lblFormPinjam.Size = new System.Drawing.Size(133, 20);
            this.lblFormPinjam.TabIndex = 0;
            this.lblFormPinjam.Text = "Form Peminjaman";
            // 
            // dgvKeranjang
            // 
            this.dgvKeranjang.AllowUserToAddRows = false;
            this.dgvKeranjang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKeranjang.BackgroundColor = System.Drawing.Color.White;
            this.dgvKeranjang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvKeranjang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKeranjang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCartNama,
            this.colCartQty});
            this.dgvKeranjang.Location = new System.Drawing.Point(724, 400);
            this.dgvKeranjang.Name = "dgvKeranjang";
            this.dgvKeranjang.ReadOnly = true;
            this.dgvKeranjang.RowHeadersVisible = false;
            this.dgvKeranjang.Size = new System.Drawing.Size(304, 114);
            this.dgvKeranjang.TabIndex = 5;
            // 
            // colCartNama
            // 
            this.colCartNama.HeaderText = "Nama Barang";
            this.colCartNama.Name = "colCartNama";
            this.colCartNama.ReadOnly = true;
            // 
            // colCartQty
            // 
            this.colCartQty.FillWeight = 50F;
            this.colCartQty.HeaderText = "Jumlah";
            this.colCartQty.Name = "colCartQty";
            this.colCartQty.ReadOnly = true;
            // 
            // btnAjukanFinal
            // 
            this.btnAjukanFinal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnAjukanFinal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAjukanFinal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAjukanFinal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnAjukanFinal.ForeColor = System.Drawing.Color.White;
            this.btnAjukanFinal.Location = new System.Drawing.Point(724, 524);
            this.btnAjukanFinal.Name = "btnAjukanFinal";
            this.btnAjukanFinal.Size = new System.Drawing.Size(304, 37);
            this.btnAjukanFinal.TabIndex = 6;
            this.btnAjukanFinal.Text = "Kirim Pengajuan";
            this.btnAjukanFinal.UseVisualStyleBackColor = false;
            // 
            // lblCartTitle
            // 
            this.lblCartTitle.AutoSize = true;
            this.lblCartTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCartTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblCartTitle.Location = new System.Drawing.Point(721, 378);
            this.lblCartTitle.Name = "lblCartTitle";
            this.lblCartTitle.Size = new System.Drawing.Size(176, 19);
            this.lblCartTitle.TabIndex = 7;
            this.lblCartTitle.Text = "List Antrean Peminjaman:";
            // 
            // FormDashboardWarga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1046, 589);
            this.Controls.Add(this.lblCartTitle);
            this.Controls.Add(this.btnAjukanFinal);
            this.Controls.Add(this.dgvKeranjang);
            this.Controls.Add(this.panelPinjam);
            this.Controls.Add(this.lblTabel);
            this.Controls.Add(this.dgvBarang);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormDashboardWarga";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard Warga";
            this.panelSidebar.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarang)).EndInit();
            this.panelPinjam.ResumeLayout(false);
            this.panelPinjam.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeranjang)).EndInit();
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
        private System.Windows.Forms.DataGridView dgvBarang;
        private System.Windows.Forms.Label lblTabel;
        private System.Windows.Forms.Panel panelPinjam;
        private System.Windows.Forms.Label lblFormPinjam;
        private System.Windows.Forms.TextBox txtNamaBarang;
        private System.Windows.Forms.Label lblNamaBarang;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.Label lblJumlah;
        private System.Windows.Forms.DateTimePicker dtpPinjam;
        private System.Windows.Forms.Label lblTglPinjam;
        private System.Windows.Forms.Button btnPinjam;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNamaAset;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKategori;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStokTersedia;
        private System.Windows.Forms.DataGridView dgvKeranjang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCartNama;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCartQty;
        private System.Windows.Forms.Button btnAjukanFinal;
        private System.Windows.Forms.Label lblCartTitle;
    }
}