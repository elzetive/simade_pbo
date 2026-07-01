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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDataPinjam));
            this.dgvTabelList = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lblTersedia = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblDipinjam = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnBatalKategori = new System.Windows.Forms.Button();
            this.txtNo_Hp = new System.Windows.Forms.TextBox();
            this.txtAlamat = new System.Windows.Forms.TextBox();
            this.txtNama_Lengkap = new System.Windows.Forms.TextBox();
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
            this.kode_peminjaman = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Peminjam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Waktu_Pinjam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Waktu_Kembali = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.nama_barang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kategori_barang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jumlah_barang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status_pengajuan = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.keterangan_pengajuan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnTambah = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabelList)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel4.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTabelList
            // 
            this.dgvTabelList.AllowUserToAddRows = false;
            this.dgvTabelList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTabelList.BackgroundColor = System.Drawing.Color.White;
            this.dgvTabelList.ColumnHeadersHeight = 35;
            this.dgvTabelList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTabelList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.kode_peminjaman,
            this.Peminjam,
            this.Waktu_Pinjam,
            this.Waktu_Kembali,
            this.status});
            this.dgvTabelList.Location = new System.Drawing.Point(253, 203);
            this.dgvTabelList.Name = "dgvTabelList";
            this.dgvTabelList.ReadOnly = true;
            this.dgvTabelList.RowHeadersWidth = 51;
            this.dgvTabelList.Size = new System.Drawing.Size(728, 132);
            this.dgvTabelList.TabIndex = 18;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.pictureBox3);
            this.panel3.Controls.Add(this.lblTersedia);
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
            this.pictureBox3.Location = new System.Drawing.Point(48, 12);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(29, 25);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 11;
            this.pictureBox3.TabStop = false;
            // 
            // lblTersedia
            // 
            this.lblTersedia.AutoSize = true;
            this.lblTersedia.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTersedia.Location = new System.Drawing.Point(89, 37);
            this.lblTersedia.Name = "lblTersedia";
            this.lblTersedia.Size = new System.Drawing.Size(38, 45);
            this.lblTersedia.TabIndex = 3;
            this.lblTersedia.Text = "0";
            this.lblTersedia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(83, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 25);
            this.label6.TabIndex = 3;
            this.label6.Text = "Ditolak";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.lblDipinjam);
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
            this.pictureBox2.Location = new System.Drawing.Point(40, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 25);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // lblDipinjam
            // 
            this.lblDipinjam.AutoSize = true;
            this.lblDipinjam.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDipinjam.Location = new System.Drawing.Point(88, 37);
            this.lblDipinjam.Name = "lblDipinjam";
            this.lblDipinjam.Size = new System.Drawing.Size(38, 45);
            this.lblDipinjam.TabIndex = 2;
            this.lblDipinjam.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(78, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 25);
            this.label4.TabIndex = 2;
            this.label4.Text = "Diterima";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lblTotal);
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
            this.pictureBox1.Location = new System.Drawing.Point(28, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(86, 37);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(38, 45);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(66, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Pengajuan";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btnTambah);
            this.panel4.Controls.Add(this.dataGridView1);
            this.panel4.Controls.Add(this.btnBatalKategori);
            this.panel4.Controls.Add(this.txtNo_Hp);
            this.panel4.Controls.Add(this.txtAlamat);
            this.panel4.Controls.Add(this.txtNama_Lengkap);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Location = new System.Drawing.Point(253, 346);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(728, 192);
            this.panel4.TabIndex = 22;
            // 
            // btnBatalKategori
            // 
            this.btnBatalKategori.BackColor = System.Drawing.Color.DimGray;
            this.btnBatalKategori.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBatalKategori.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBatalKategori.ForeColor = System.Drawing.Color.White;
            this.btnBatalKategori.Location = new System.Drawing.Point(513, 152);
            this.btnBatalKategori.Name = "btnBatalKategori";
            this.btnBatalKategori.Size = new System.Drawing.Size(99, 28);
            this.btnBatalKategori.TabIndex = 25;
            this.btnBatalKategori.Text = "BATAL";
            this.btnBatalKategori.UseVisualStyleBackColor = false;
            this.btnBatalKategori.Click += new System.EventHandler(this.btnBatalKategori_Click);
            // 
            // txtNo_Hp
            // 
            this.txtNo_Hp.Location = new System.Drawing.Point(574, 31);
            this.txtNo_Hp.Name = "txtNo_Hp";
            this.txtNo_Hp.Size = new System.Drawing.Size(144, 20);
            this.txtNo_Hp.TabIndex = 18;
            // 
            // txtAlamat
            // 
            this.txtAlamat.Location = new System.Drawing.Point(332, 31);
            this.txtAlamat.Name = "txtAlamat";
            this.txtAlamat.Size = new System.Drawing.Size(144, 20);
            this.txtAlamat.TabIndex = 17;
            // 
            // txtNama_Lengkap
            // 
            this.txtNama_Lengkap.Location = new System.Drawing.Point(97, 31);
            this.txtNama_Lengkap.Name = "txtNama_Lengkap";
            this.txtNama_Lengkap.Size = new System.Drawing.Size(144, 20);
            this.txtNama_Lengkap.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(482, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Nomor Telepon";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(247, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Alamat";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Nama Peminjam";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.lblCari.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.btnData_Kembali.Click += new System.EventHandler(this.btnData_Kembali_Click);
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
            this.btnData_Pinjam.Click += new System.EventHandler(this.btnData_Pinjam_Click);
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
            this.btnData_Barang.Click += new System.EventHandler(this.btnData_Barang_Click);
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
            this.btnData_Ambil.Click += new System.EventHandler(this.btnData_Ambil_Click);
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
            this.btnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogOut.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogOut.ForeColor = System.Drawing.Color.White;
            this.btnLogOut.Location = new System.Drawing.Point(19, 501);
            this.btnLogOut.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(158, 37);
            this.btnLogOut.TabIndex = 2;
            this.btnLogOut.Text = "Log Out";
            this.btnLogOut.UseVisualStyleBackColor = false;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
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
            this.panelSidebar.Margin = new System.Windows.Forms.Padding(2);
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
            this.btnTambah_Admin.Click += new System.EventHandler(this.btnTambah_Admin_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Red;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(803, 16);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(34, 24);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
            // kode_peminjaman
            // 
            this.kode_peminjaman.HeaderText = "Kode Peminjaman";
            this.kode_peminjaman.Name = "kode_peminjaman";
            this.kode_peminjaman.ReadOnly = true;
            // 
            // Peminjam
            // 
            this.Peminjam.HeaderText = "Nama Peminjam";
            this.Peminjam.MinimumWidth = 6;
            this.Peminjam.Name = "Peminjam";
            this.Peminjam.ReadOnly = true;
            // 
            // Waktu_Pinjam
            // 
            this.Waktu_Pinjam.HeaderText = "Tanggal Pinjam";
            this.Waktu_Pinjam.MinimumWidth = 6;
            this.Waktu_Pinjam.Name = "Waktu_Pinjam";
            this.Waktu_Pinjam.ReadOnly = true;
            // 
            // Waktu_Kembali
            // 
            this.Waktu_Kembali.HeaderText = "Tanggal Kembali";
            this.Waktu_Kembali.MinimumWidth = 6;
            this.Waktu_Kembali.Name = "Waktu_Kembali";
            this.Waktu_Kembali.ReadOnly = true;
            // 
            // status
            // 
            this.status.HeaderText = "Status";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nama_barang,
            this.kategori_barang,
            this.jumlah_barang,
            this.status_pengajuan,
            this.keterangan_pengajuan});
            this.dataGridView1.Location = new System.Drawing.Point(8, 61);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(709, 81);
            this.dataGridView1.TabIndex = 26;
            // 
            // nama_barang
            // 
            this.nama_barang.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nama_barang.HeaderText = "Nama Barang";
            this.nama_barang.Name = "nama_barang";
            this.nama_barang.ReadOnly = true;
            this.nama_barang.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // kategori_barang
            // 
            this.kategori_barang.HeaderText = "Kategori Barang";
            this.kategori_barang.Name = "kategori_barang";
            this.kategori_barang.ReadOnly = true;
            // 
            // jumlah_barang
            // 
            this.jumlah_barang.HeaderText = "Jumlah Barang";
            this.jumlah_barang.Name = "jumlah_barang";
            this.jumlah_barang.ReadOnly = true;
            // 
            // status_pengajuan
            // 
            this.status_pengajuan.HeaderText = "Status";
            this.status_pengajuan.Items.AddRange(new object[] {
            "Disetujui",
            "Ditolak"});
            this.status_pengajuan.Name = "status_pengajuan";
            this.status_pengajuan.ReadOnly = true;
            this.status_pengajuan.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.status_pengajuan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // keterangan_pengajuan
            // 
            this.keterangan_pengajuan.HeaderText = "Keterangan";
            this.keterangan_pengajuan.Name = "keterangan_pengajuan";
            this.keterangan_pengajuan.ReadOnly = true;
            // 
            // btnTambah
            // 
            this.btnTambah.BackColor = System.Drawing.Color.SeaGreen;
            this.btnTambah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambah.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTambah.ForeColor = System.Drawing.Color.White;
            this.btnTambah.Location = new System.Drawing.Point(618, 151);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(99, 29);
            this.btnTambah.TabIndex = 47;
            this.btnTambah.Text = "SIMPAN";
            this.btnTambah.UseVisualStyleBackColor = false;
            // 
            // FormDataPinjam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 589);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.txtCari);
            this.Controls.Add(this.lblCari);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvTabelList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormDataPinjam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = resources.GetString("$this.Text");
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabelList)).EndInit();
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
            this.panelSidebar.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvTabelList;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblTersedia;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblDipinjam;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTotal;
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
        private System.Windows.Forms.TextBox txtNo_Hp;
        private System.Windows.Forms.TextBox txtAlamat;
        private System.Windows.Forms.TextBox txtNama_Lengkap;
        private System.Windows.Forms.Button btnBatalKategori;
        private System.Windows.Forms.DataGridViewTextBoxColumn kode_peminjaman;
        private System.Windows.Forms.DataGridViewTextBoxColumn Peminjam;
        private System.Windows.Forms.DataGridViewTextBoxColumn Waktu_Pinjam;
        private System.Windows.Forms.DataGridViewTextBoxColumn Waktu_Kembali;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nama_barang;
        private System.Windows.Forms.DataGridViewTextBoxColumn kategori_barang;
        private System.Windows.Forms.DataGridViewTextBoxColumn jumlah_barang;
        private System.Windows.Forms.DataGridViewComboBoxColumn status_pengajuan;
        private System.Windows.Forms.DataGridViewTextBoxColumn keterangan_pengajuan;
        private System.Windows.Forms.Button btnTambah;
    }
}