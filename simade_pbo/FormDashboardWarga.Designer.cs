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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.lblTabel = new System.Windows.Forms.Label();
            this.panelPinjam = new System.Windows.Forms.Panel();
            this.dtpTanggalKembali = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTambahKeList = new System.Windows.Forms.Button();
            this.dtpTanggalPinjam = new System.Windows.Forms.DateTimePicker();
            this.lblTglPinjam = new System.Windows.Forms.Label();
            this.txtJumlahPinjam = new System.Windows.Forms.TextBox();
            this.lblJumlah = new System.Windows.Forms.Label();
            this.txtNamaBarang = new System.Windows.Forms.TextBox();
            this.lblNamaBarang = new System.Windows.Forms.Label();
            this.lblFormPinjam = new System.Windows.Forms.Label();
            this.dgvAntrean = new System.Windows.Forms.DataGridView();
            this.colCartNama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCartQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnKirimPengajuan = new System.Windows.Forms.Button();
            this.lblCartTitle = new System.Windows.Forms.Label();
            this.dgvBarang = new System.Windows.Forms.DataGridView();
            this.nama_barang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_kategori = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jumlah_tersedia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelSidebar.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelPinjam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAntrean)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarang)).BeginInit();
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
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 9.75F);
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
            this.btnMenuDashboard.Click += new System.EventHandler(this.btnMenuDashboard_Click);
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
            this.btnMenuStatus.Click += new System.EventHandler(this.btnMenuStatus_Click);
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
            this.btnMenuRiwayat.Click += new System.EventHandler(this.btnMenuRiwayat_Click);
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
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
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
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
            this.panelPinjam.Controls.Add(this.dtpTanggalKembali);
            this.panelPinjam.Controls.Add(this.label1);
            this.panelPinjam.Controls.Add(this.btnTambahKeList);
            this.panelPinjam.Controls.Add(this.dtpTanggalPinjam);
            this.panelPinjam.Controls.Add(this.lblTglPinjam);
            this.panelPinjam.Controls.Add(this.txtJumlahPinjam);
            this.panelPinjam.Controls.Add(this.lblJumlah);
            this.panelPinjam.Controls.Add(this.txtNamaBarang);
            this.panelPinjam.Controls.Add(this.lblNamaBarang);
            this.panelPinjam.Controls.Add(this.lblFormPinjam);
            this.panelPinjam.Location = new System.Drawing.Point(724, 114);
            this.panelPinjam.Margin = new System.Windows.Forms.Padding(2);
            this.panelPinjam.Name = "panelPinjam";
            this.panelPinjam.Size = new System.Drawing.Size(304, 289);
            this.panelPinjam.TabIndex = 4;
            // 
            // dtpTanggalKembali
            // 
            this.dtpTanggalKembali.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpTanggalKembali.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTanggalKembali.Location = new System.Drawing.Point(18, 217);
            this.dtpTanggalKembali.Margin = new System.Windows.Forms.Padding(2);
            this.dtpTanggalKembali.Name = "dtpTanggalKembali";
            this.dtpTanggalKembali.Size = new System.Drawing.Size(267, 25);
            this.dtpTanggalKembali.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.label1.Location = new System.Drawing.Point(15, 197);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Tanggal Kembali";
            // 
            // btnTambahKeList
            // 
            this.btnTambahKeList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnTambahKeList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTambahKeList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambahKeList.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnTambahKeList.ForeColor = System.Drawing.Color.White;
            this.btnTambahKeList.Location = new System.Drawing.Point(19, 249);
            this.btnTambahKeList.Margin = new System.Windows.Forms.Padding(2);
            this.btnTambahKeList.Name = "btnTambahKeList";
            this.btnTambahKeList.Size = new System.Drawing.Size(266, 32);
            this.btnTambahKeList.TabIndex = 9;
            this.btnTambahKeList.Text = "Tambah ke List";
            this.btnTambahKeList.UseVisualStyleBackColor = false;
            this.btnTambahKeList.Click += new System.EventHandler(this.btnTambahKeList_Click);
            // 
            // dtpTanggalPinjam
            // 
            this.dtpTanggalPinjam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpTanggalPinjam.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTanggalPinjam.Location = new System.Drawing.Point(19, 166);
            this.dtpTanggalPinjam.Margin = new System.Windows.Forms.Padding(2);
            this.dtpTanggalPinjam.Name = "dtpTanggalPinjam";
            this.dtpTanggalPinjam.Size = new System.Drawing.Size(267, 25);
            this.dtpTanggalPinjam.TabIndex = 6;
            // 
            // lblTglPinjam
            // 
            this.lblTglPinjam.AutoSize = true;
            this.lblTglPinjam.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTglPinjam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblTglPinjam.Location = new System.Drawing.Point(16, 146);
            this.lblTglPinjam.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTglPinjam.Name = "lblTglPinjam";
            this.lblTglPinjam.Size = new System.Drawing.Size(104, 17);
            this.lblTglPinjam.TabIndex = 5;
            this.lblTglPinjam.Text = "Tanggal Pinjam";
            // 
            // txtJumlahPinjam
            // 
            this.txtJumlahPinjam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtJumlahPinjam.Location = new System.Drawing.Point(19, 114);
            this.txtJumlahPinjam.Margin = new System.Windows.Forms.Padding(2);
            this.txtJumlahPinjam.Name = "txtJumlahPinjam";
            this.txtJumlahPinjam.Size = new System.Drawing.Size(267, 25);
            this.txtJumlahPinjam.TabIndex = 4;
            // 
            // lblJumlah
            // 
            this.lblJumlah.AutoSize = true;
            this.lblJumlah.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblJumlah.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblJumlah.Location = new System.Drawing.Point(16, 94);
            this.lblJumlah.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblJumlah.Name = "lblJumlah";
            this.lblJumlah.Size = new System.Drawing.Size(100, 17);
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
            this.lblFormPinjam.Size = new System.Drawing.Size(137, 20);
            this.lblFormPinjam.TabIndex = 0;
            this.lblFormPinjam.Text = "Form Peminjaman";
            // 
            // dgvAntrean
            // 
            this.dgvAntrean.AllowUserToAddRows = false;
            this.dgvAntrean.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAntrean.BackgroundColor = System.Drawing.Color.White;
            this.dgvAntrean.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAntrean.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAntrean.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCartNama,
            this.colCartQty});
            this.dgvAntrean.Location = new System.Drawing.Point(724, 427);
            this.dgvAntrean.Name = "dgvAntrean";
            this.dgvAntrean.ReadOnly = true;
            this.dgvAntrean.RowHeadersVisible = false;
            this.dgvAntrean.Size = new System.Drawing.Size(304, 86);
            this.dgvAntrean.TabIndex = 5;
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
            // btnKirimPengajuan
            // 
            this.btnKirimPengajuan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnKirimPengajuan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKirimPengajuan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKirimPengajuan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnKirimPengajuan.ForeColor = System.Drawing.Color.White;
            this.btnKirimPengajuan.Location = new System.Drawing.Point(724, 519);
            this.btnKirimPengajuan.Name = "btnKirimPengajuan";
            this.btnKirimPengajuan.Size = new System.Drawing.Size(304, 37);
            this.btnKirimPengajuan.TabIndex = 6;
            this.btnKirimPengajuan.Text = "Kirim Pengajuan";
            this.btnKirimPengajuan.UseVisualStyleBackColor = false;
            this.btnKirimPengajuan.Click += new System.EventHandler(this.btnKirimPengajuan_Click);
            // 
            // lblCartTitle
            // 
            this.lblCartTitle.AutoSize = true;
            this.lblCartTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCartTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblCartTitle.Location = new System.Drawing.Point(721, 405);
            this.lblCartTitle.Name = "lblCartTitle";
            this.lblCartTitle.Size = new System.Drawing.Size(179, 19);
            this.lblCartTitle.TabIndex = 7;
            this.lblCartTitle.Text = "List Antrean Peminjaman:";
            // 
            // dgvBarang
            // 
            this.dgvBarang.AllowUserToAddRows = false;
            this.dgvBarang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBarang.BackgroundColor = System.Drawing.Color.White;
            this.dgvBarang.ColumnHeadersHeight = 35;
            this.dgvBarang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBarang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nama_barang,
            this.id_kategori,
            this.jumlah_tersedia});
            this.dgvBarang.Location = new System.Drawing.Point(216, 114);
            this.dgvBarang.Name = "dgvBarang";
            this.dgvBarang.ReadOnly = true;
            this.dgvBarang.RowHeadersWidth = 51;
            this.dgvBarang.Size = new System.Drawing.Size(491, 443);
            this.dgvBarang.TabIndex = 13;
            this.dgvBarang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBarang_CellClick);
            // 
            // nama_barang
            // 
            this.nama_barang.DataPropertyName = "nama_barang";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nama_barang.DefaultCellStyle = dataGridViewCellStyle3;
            this.nama_barang.HeaderText = "Nama Barang";
            this.nama_barang.MinimumWidth = 6;
            this.nama_barang.Name = "nama_barang";
            this.nama_barang.ReadOnly = true;
            // 
            // id_kategori
            // 
            this.id_kategori.DataPropertyName = "nama_kategori";
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.id_kategori.DefaultCellStyle = dataGridViewCellStyle4;
            this.id_kategori.HeaderText = "Kategori Barang";
            this.id_kategori.MinimumWidth = 6;
            this.id_kategori.Name = "id_kategori";
            this.id_kategori.ReadOnly = true;
            // 
            // jumlah_tersedia
            // 
            this.jumlah_tersedia.DataPropertyName = "jumlah_tersedia";
            this.jumlah_tersedia.HeaderText = "Jumlah Tersedia";
            this.jumlah_tersedia.Name = "jumlah_tersedia";
            this.jumlah_tersedia.ReadOnly = true;
            // 
            // FormDashboardWarga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1046, 589);
            this.Controls.Add(this.dgvBarang);
            this.Controls.Add(this.lblCartTitle);
            this.Controls.Add(this.btnKirimPengajuan);
            this.Controls.Add(this.dgvAntrean);
            this.Controls.Add(this.panelPinjam);
            this.Controls.Add(this.lblTabel);
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
            this.panelPinjam.ResumeLayout(false);
            this.panelPinjam.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAntrean)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarang)).EndInit();
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
        private System.Windows.Forms.Label lblTabel;
        private System.Windows.Forms.Panel panelPinjam;
        private System.Windows.Forms.Label lblFormPinjam;
        private System.Windows.Forms.TextBox txtNamaBarang;
        private System.Windows.Forms.Label lblNamaBarang;
        private System.Windows.Forms.TextBox txtJumlahPinjam;
        private System.Windows.Forms.Label lblJumlah;
        private System.Windows.Forms.DateTimePicker dtpTanggalPinjam;
        private System.Windows.Forms.Label lblTglPinjam;
        private System.Windows.Forms.Button btnTambahKeList;
        private System.Windows.Forms.DataGridView dgvAntrean;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCartNama;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCartQty;
        private System.Windows.Forms.Button btnKirimPengajuan;
        private System.Windows.Forms.Label lblCartTitle;
        private System.Windows.Forms.DateTimePicker dtpTanggalKembali;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvBarang;
        private System.Windows.Forms.DataGridViewTextBoxColumn nama_barang;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_kategori;
        private System.Windows.Forms.DataGridViewTextBoxColumn jumlah_tersedia;
    }
}