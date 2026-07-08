namespace simade_pbo
{
    partial class FormDataPinjam
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
            this.dgvUtama = new System.Windows.Forms.DataGridView();
            this.kode_peminjaman = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nama_lengkap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tgl_pinjam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tgl_kembali = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status_peminjaman = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lblTotalDitolak = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblTotalDisetujui = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTotalPengajuan = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtTglKembali = new System.Windows.Forms.TextBox();
            this.txtTglPinjam = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.nama_barang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_kategori = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jumlah_pinjam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jumlah_tersedia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.keterangan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnBatalDetail = new System.Windows.Forms.Button();
            this.txtNoHp = new System.Windows.Forms.TextBox();
            this.txtAlamat = new System.Windows.Forms.TextBox();
            this.txtNamaLengkap = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCari = new System.Windows.Forms.TextBox();
            this.lblCari = new System.Windows.Forms.Label();
            this.btnData_Kembali = new System.Windows.Forms.Button();
            this.btnData_Pinjam = new System.Windows.Forms.Button();
            this.btnData_Barang = new System.Windows.Forms.Button();
            this.btnData_Ambil = new System.Windows.Forms.Button();
            this.lblNamaWarga = new System.Windows.Forms.Label();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnTambah_Admin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUtama)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.panelSidebar.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvUtama
            // 
            this.dgvUtama.AllowUserToAddRows = false;
            this.dgvUtama.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUtama.BackgroundColor = System.Drawing.Color.White;
            this.dgvUtama.ColumnHeadersHeight = 35;
            this.dgvUtama.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvUtama.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.kode_peminjaman,
            this.nama_lengkap,
            this.tgl_pinjam,
            this.tgl_kembali,
            this.status_peminjaman});
            this.dgvUtama.Location = new System.Drawing.Point(253, 203);
            this.dgvUtama.Name = "dgvUtama";
            this.dgvUtama.ReadOnly = true;
            this.dgvUtama.RowHeadersWidth = 51;
            this.dgvUtama.Size = new System.Drawing.Size(728, 132);
            this.dgvUtama.TabIndex = 18;
            // 
            // kode_peminjaman
            // 
            this.kode_peminjaman.HeaderText = "Kode Peminjaman";
            this.kode_peminjaman.MinimumWidth = 6;
            this.kode_peminjaman.Name = "kode_peminjaman";
            this.kode_peminjaman.ReadOnly = true;
            // 
            // nama_lengkap
            // 
            this.nama_lengkap.HeaderText = "Nama Peminjam";
            this.nama_lengkap.MinimumWidth = 6;
            this.nama_lengkap.Name = "nama_lengkap";
            this.nama_lengkap.ReadOnly = true;
            // 
            // tgl_pinjam
            // 
            this.tgl_pinjam.HeaderText = "Tanggal Pinjam";
            this.tgl_pinjam.MinimumWidth = 6;
            this.tgl_pinjam.Name = "tgl_pinjam";
            this.tgl_pinjam.ReadOnly = true;
            // 
            // tgl_kembali
            // 
            this.tgl_kembali.HeaderText = "Tanggal Kembali";
            this.tgl_kembali.MinimumWidth = 6;
            this.tgl_kembali.Name = "tgl_kembali";
            this.tgl_kembali.ReadOnly = true;
            // 
            // status_peminjaman
            // 
            this.status_peminjaman.HeaderText = "Status";
            this.status_peminjaman.MinimumWidth = 6;
            this.status_peminjaman.Name = "status_peminjaman";
            this.status_peminjaman.ReadOnly = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.pictureBox3);
            this.panel3.Controls.Add(this.lblTotalDitolak);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Location = new System.Drawing.Point(767, 69);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(214, 92);
            this.panel3.TabIndex = 21;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.White;
            this.pictureBox3.Image = global::simade_pbo.Properties.Resources.tersedia;
            this.pictureBox3.Location = new System.Drawing.Point(53, 12);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(29, 25);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 11;
            this.pictureBox3.TabStop = false;
            // 
            // lblTotalDitolak
            // 
            this.lblTotalDitolak.AutoSize = true;
            this.lblTotalDitolak.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalDitolak.Location = new System.Drawing.Point(90, 35);
            this.lblTotalDitolak.Name = "lblTotalDitolak";
            this.lblTotalDitolak.Size = new System.Drawing.Size(38, 45);
            this.lblTotalDitolak.TabIndex = 3;
            this.lblTotalDitolak.Text = "0";
            this.lblTotalDitolak.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(88, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 21);
            this.label6.TabIndex = 3;
            this.label6.Text = "Ditolak";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.lblTotalDisetujui);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(510, 69);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(214, 92);
            this.panel2.TabIndex = 20;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.Image = global::simade_pbo.Properties.Resources.dipinjam;
            this.pictureBox2.Location = new System.Drawing.Point(47, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 25);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // lblTotalDisetujui
            // 
            this.lblTotalDisetujui.AutoSize = true;
            this.lblTotalDisetujui.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalDisetujui.Location = new System.Drawing.Point(90, 35);
            this.lblTotalDisetujui.Name = "lblTotalDisetujui";
            this.lblTotalDisetujui.Size = new System.Drawing.Size(38, 45);
            this.lblTotalDisetujui.TabIndex = 2;
            this.lblTotalDisetujui.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(82, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 21);
            this.label4.TabIndex = 2;
            this.label4.Text = "Disetujui";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lblTotalPengajuan);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(253, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(214, 92);
            this.panel1.TabIndex = 19;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::simade_pbo.Properties.Resources.totalbarang;
            this.pictureBox1.Location = new System.Drawing.Point(44, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // lblTotalPengajuan
            // 
            this.lblTotalPengajuan.AutoSize = true;
            this.lblTotalPengajuan.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalPengajuan.Location = new System.Drawing.Point(90, 35);
            this.lblTotalPengajuan.Name = "lblTotalPengajuan";
            this.lblTotalPengajuan.Size = new System.Drawing.Size(38, 45);
            this.lblTotalPengajuan.TabIndex = 1;
            this.lblTotalPengajuan.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(79, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Pengajuan";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.txtTglKembali);
            this.panel4.Controls.Add(this.txtTglPinjam);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.btnSimpan);
            this.panel4.Controls.Add(this.dgvDetail);
            this.panel4.Controls.Add(this.btnBatalDetail);
            this.panel4.Controls.Add(this.txtNoHp);
            this.panel4.Controls.Add(this.txtAlamat);
            this.panel4.Controls.Add(this.txtNamaLengkap);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Location = new System.Drawing.Point(253, 346);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(728, 192);
            this.panel4.TabIndex = 22;
            // 
            // txtTglKembali
            // 
            this.txtTglKembali.Location = new System.Drawing.Point(334, 154);
            this.txtTglKembali.Name = "txtTglKembali";
            this.txtTglKembali.Size = new System.Drawing.Size(144, 20);
            this.txtTglKembali.TabIndex = 55;
            // 
            // txtTglPinjam
            // 
            this.txtTglPinjam.Location = new System.Drawing.Point(97, 154);
            this.txtTglPinjam.Name = "txtTglPinjam";
            this.txtTglPinjam.Size = new System.Drawing.Size(144, 20);
            this.txtTglPinjam.TabIndex = 54;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(247, 157);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 13);
            this.label10.TabIndex = 53;
            this.label10.Text = "Tanggal Kembali";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 157);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 13);
            this.label11.TabIndex = 52;
            this.label11.Text = "Tanggal Pinjam";
            // 
            // btnSimpan
            // 
            this.btnSimpan.BackColor = System.Drawing.Color.SeaGreen;
            this.btnSimpan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSimpan.ForeColor = System.Drawing.Color.White;
            this.btnSimpan.Location = new System.Drawing.Point(618, 151);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(99, 29);
            this.btnSimpan.TabIndex = 47;
            this.btnSimpan.Text = "SIMPAN";
            this.btnSimpan.UseVisualStyleBackColor = false;
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nama_barang,
            this.id_kategori,
            this.jumlah_pinjam,
            this.jumlah_tersedia,
            this.status,
            this.keterangan});
            this.dgvDetail.Location = new System.Drawing.Point(-1, 63);
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.ReadOnly = true;
            this.dgvDetail.RowHeadersWidth = 51;
            this.dgvDetail.Size = new System.Drawing.Size(728, 81);
            this.dgvDetail.TabIndex = 26;
            // 
            // nama_barang
            // 
            this.nama_barang.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nama_barang.HeaderText = "Nama Barang";
            this.nama_barang.MinimumWidth = 6;
            this.nama_barang.Name = "nama_barang";
            this.nama_barang.ReadOnly = true;
            // 
            // id_kategori
            // 
            this.id_kategori.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.id_kategori.HeaderText = "Kategori Barang";
            this.id_kategori.MinimumWidth = 6;
            this.id_kategori.Name = "id_kategori";
            this.id_kategori.ReadOnly = true;
            // 
            // jumlah_pinjam
            // 
            this.jumlah_pinjam.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.jumlah_pinjam.HeaderText = "Jumlah Pinjam";
            this.jumlah_pinjam.MinimumWidth = 6;
            this.jumlah_pinjam.Name = "jumlah_pinjam";
            this.jumlah_pinjam.ReadOnly = true;
            // 
            // jumlah_tersedia
            // 
            this.jumlah_tersedia.HeaderText = "Jumlah Tersedia";
            this.jumlah_tersedia.MinimumWidth = 6;
            this.jumlah_tersedia.Name = "jumlah_tersedia";
            this.jumlah_tersedia.ReadOnly = true;
            this.jumlah_tersedia.Width = 125;
            // 
            // status
            // 
            this.status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.status.HeaderText = "Status";
            this.status.Items.AddRange(new object[] {
            "Pending",
            "Disetujui",
            "Ditolak"});
            this.status.MinimumWidth = 6;
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // keterangan
            // 
            this.keterangan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.keterangan.HeaderText = "Keterangan";
            this.keterangan.MinimumWidth = 6;
            this.keterangan.Name = "keterangan";
            this.keterangan.ReadOnly = true;
            // 
            // btnBatalDetail
            // 
            this.btnBatalDetail.BackColor = System.Drawing.Color.DimGray;
            this.btnBatalDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBatalDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnBatalDetail.ForeColor = System.Drawing.Color.White;
            this.btnBatalDetail.Location = new System.Drawing.Point(513, 152);
            this.btnBatalDetail.Name = "btnBatalDetail";
            this.btnBatalDetail.Size = new System.Drawing.Size(99, 28);
            this.btnBatalDetail.TabIndex = 25;
            this.btnBatalDetail.Text = "BATAL";
            this.btnBatalDetail.UseVisualStyleBackColor = false;
            // 
            // txtNoHp
            // 
            this.txtNoHp.Location = new System.Drawing.Point(574, 33);
            this.txtNoHp.Name = "txtNoHp";
            this.txtNoHp.Size = new System.Drawing.Size(144, 20);
            this.txtNoHp.TabIndex = 18;
            // 
            // txtAlamat
            // 
            this.txtAlamat.Location = new System.Drawing.Point(332, 33);
            this.txtAlamat.Name = "txtAlamat";
            this.txtAlamat.Size = new System.Drawing.Size(144, 20);
            this.txtAlamat.TabIndex = 17;
            // 
            // txtNamaLengkap
            // 
            this.txtNamaLengkap.Location = new System.Drawing.Point(97, 33);
            this.txtNamaLengkap.Name = "txtNamaLengkap";
            this.txtNamaLengkap.Size = new System.Drawing.Size(144, 20);
            this.txtNamaLengkap.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(482, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Nomor Telepon";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(247, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Alamat";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Nama Peminjam";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(2, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(177, 25);
            this.label7.TabIndex = 6;
            this.label7.Text = "Detail Peminjaman";
            // 
            // txtCari
            // 
            this.txtCari.Location = new System.Drawing.Point(303, 173);
            this.txtCari.Name = "txtCari";
            this.txtCari.Size = new System.Drawing.Size(678, 20);
            this.txtCari.TabIndex = 24;
            // 
            // lblCari
            // 
            this.lblCari.AutoSize = true;
            this.lblCari.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCari.Location = new System.Drawing.Point(249, 170);
            this.lblCari.Name = "lblCari";
            this.lblCari.Size = new System.Drawing.Size(40, 21);
            this.lblCari.TabIndex = 23;
            this.lblCari.Text = "Cari";
            // 
            // btnData_Kembali
            // 
            this.btnData_Kembali.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnData_Kembali.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnData_Kembali.FlatAppearance.BorderSize = 0;
            this.btnData_Kembali.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnData_Kembali.ForeColor = System.Drawing.Color.White;
            this.btnData_Kembali.Location = new System.Drawing.Point(19, 223);
            this.btnData_Kembali.Name = "btnData_Kembali";
            this.btnData_Kembali.Size = new System.Drawing.Size(158, 37);
            this.btnData_Kembali.TabIndex = 7;
            this.btnData_Kembali.Text = "Data Pengembalian";
            this.btnData_Kembali.UseVisualStyleBackColor = false;
            // 
            // btnData_Pinjam
            // 
            this.btnData_Pinjam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnData_Pinjam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnData_Pinjam.FlatAppearance.BorderSize = 0;
            this.btnData_Pinjam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnData_Pinjam.ForeColor = System.Drawing.Color.White;
            this.btnData_Pinjam.Location = new System.Drawing.Point(19, 120);
            this.btnData_Pinjam.Name = "btnData_Pinjam";
            this.btnData_Pinjam.Size = new System.Drawing.Size(158, 37);
            this.btnData_Pinjam.TabIndex = 6;
            this.btnData_Pinjam.Text = "Data Pinjam";
            this.btnData_Pinjam.UseVisualStyleBackColor = false;
            // 
            // btnData_Barang
            // 
            this.btnData_Barang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnData_Barang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnData_Barang.FlatAppearance.BorderSize = 0;
            this.btnData_Barang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnData_Barang.ForeColor = System.Drawing.Color.White;
            this.btnData_Barang.Location = new System.Drawing.Point(19, 69);
            this.btnData_Barang.Name = "btnData_Barang";
            this.btnData_Barang.Size = new System.Drawing.Size(158, 37);
            this.btnData_Barang.TabIndex = 5;
            this.btnData_Barang.Text = "Data Barang";
            this.btnData_Barang.UseVisualStyleBackColor = false;
            // 
            // btnData_Ambil
            // 
            this.btnData_Ambil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnData_Ambil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnData_Ambil.FlatAppearance.BorderSize = 0;
            this.btnData_Ambil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnData_Ambil.ForeColor = System.Drawing.Color.White;
            this.btnData_Ambil.Location = new System.Drawing.Point(19, 172);
            this.btnData_Ambil.Name = "btnData_Ambil";
            this.btnData_Ambil.Size = new System.Drawing.Size(158, 37);
            this.btnData_Ambil.TabIndex = 4;
            this.btnData_Ambil.Text = "Data Pengambilan";
            this.btnData_Ambil.UseVisualStyleBackColor = false;
            // 
            // lblNamaWarga
            // 
            this.lblNamaWarga.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblNamaWarga.ForeColor = System.Drawing.Color.White;
            this.lblNamaWarga.Location = new System.Drawing.Point(8, 18);
            this.lblNamaWarga.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNamaWarga.Name = "lblNamaWarga";
            this.lblNamaWarga.Size = new System.Drawing.Size(177, 23);
            this.lblNamaWarga.TabIndex = 0;
            this.lblNamaWarga.Text = "Halo, Admin!";
            this.lblNamaWarga.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnLogOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogOut.FlatAppearance.BorderSize = 0;
            this.btnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogOut.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogOut.ForeColor = System.Drawing.Color.White;
            this.btnLogOut.Location = new System.Drawing.Point(19, 501);
            this.btnLogOut.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(158, 37);
            this.btnLogOut.TabIndex = 2;
            this.btnLogOut.Text = "Log Out";
            this.btnLogOut.UseVisualStyleBackColor = false;
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.panelSidebar.Controls.Add(this.btnTambah_Admin);
            this.panelSidebar.Controls.Add(this.btnData_Kembali);
            this.panelSidebar.Controls.Add(this.btnData_Pinjam);
            this.panelSidebar.Controls.Add(this.btnData_Barang);
            this.panelSidebar.Controls.Add(this.btnData_Ambil);
            this.panelSidebar.Controls.Add(this.lblNamaWarga);
            this.panelSidebar.Controls.Add(this.btnLogOut);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(192, 589);
            this.panelSidebar.TabIndex = 25;
            // 
            // btnTambah_Admin
            // 
            this.btnTambah_Admin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnTambah_Admin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTambah_Admin.FlatAppearance.BorderSize = 0;
            this.btnTambah_Admin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambah_Admin.ForeColor = System.Drawing.Color.White;
            this.btnTambah_Admin.Location = new System.Drawing.Point(19, 276);
            this.btnTambah_Admin.Name = "btnTambah_Admin";
            this.btnTambah_Admin.Size = new System.Drawing.Size(158, 37);
            this.btnTambah_Admin.TabIndex = 9;
            this.btnTambah_Admin.Text = "Tambah Admin";
            this.btnTambah_Admin.UseVisualStyleBackColor = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Red;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(803, 16);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(34, 24);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.btnExit);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(192, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(848, 57);
            this.panelHeader.TabIndex = 26;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblTitle.Location = new System.Drawing.Point(19, 13);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(232, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "SIMADE - Data Pinjam";
            // 
            // FormDataPinjam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 589);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.txtCari);
            this.Controls.Add(this.lblCari);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvUtama);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormDataPinjam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form Data Pinjam Admin";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUtama)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.panelSidebar.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvUtama;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblTotalDitolak;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblTotalDisetujui;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTotalPengajuan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Label lblCari;
        private System.Windows.Forms.Button btnData_Kembali;
        private System.Windows.Forms.Button btnData_Pinjam;
        private System.Windows.Forms.Button btnData_Barang;
        private System.Windows.Forms.Button btnData_Ambil;
        private System.Windows.Forms.Label lblNamaWarga;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnTambah_Admin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNoHp;
        private System.Windows.Forms.TextBox txtAlamat;
        private System.Windows.Forms.TextBox txtNamaLengkap;
        private System.Windows.Forms.Button btnBatalDetail;
        private System.Windows.Forms.DataGridView dgvDetail;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.TextBox txtTglKembali;
        private System.Windows.Forms.TextBox txtTglPinjam;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridViewTextBoxColumn kode_peminjaman;
        private System.Windows.Forms.DataGridViewTextBoxColumn nama_lengkap;
        private System.Windows.Forms.DataGridViewTextBoxColumn tgl_pinjam;
        private System.Windows.Forms.DataGridViewTextBoxColumn tgl_kembali;
        private System.Windows.Forms.DataGridViewTextBoxColumn status_peminjaman;
        private System.Windows.Forms.DataGridViewTextBoxColumn nama_barang;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_kategori;
        private System.Windows.Forms.DataGridViewTextBoxColumn jumlah_pinjam;
        private System.Windows.Forms.DataGridViewTextBoxColumn jumlah_tersedia;
        private System.Windows.Forms.DataGridViewComboBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn keterangan;
    }
}