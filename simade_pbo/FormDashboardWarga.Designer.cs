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
            this.btnLogOut = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.dgvBarang = new System.Windows.Forms.DataGridView();
            this.lblTabel = new System.Windows.Forms.Label();
            this.panelPinjam = new System.Windows.Forms.Panel();
            this.btnPinjam = new System.Windows.Forms.Button();
            this.dtpKembali = new System.Windows.Forms.DateTimePicker();
            this.lblTglKembali = new System.Windows.Forms.Label();
            this.dtpPinjam = new System.Windows.Forms.DateTimePicker();
            this.lblTglPinjam = new System.Windows.Forms.Label();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.lblJumlah = new System.Windows.Forms.Label();
            this.txtNamaBarang = new System.Windows.Forms.TextBox();
            this.lblNamaBarang = new System.Windows.Forms.Label();
            this.lblFormPinjam = new System.Windows.Forms.Label();
            this.panelSidebar.SuspendLayout();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarang)).BeginInit();
            this.panelPinjam.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.panelSidebar.Controls.Add(this.lblNamaWarga);
            this.panelSidebar.Controls.Add(this.lblRole);
            this.panelSidebar.Controls.Add(this.btnLogOut);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(192, 589);
            this.panelSidebar.TabIndex = 0;
            // 
            // lblNamaWarga
            // 
            this.lblNamaWarga.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblNamaWarga.ForeColor = System.Drawing.Color.White;
            this.lblNamaWarga.Location = new System.Drawing.Point(9, 32);
            this.lblNamaWarga.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNamaWarga.Name = "lblNamaWarga";
            this.lblNamaWarga.Size = new System.Drawing.Size(177, 23);
            this.lblNamaWarga.TabIndex = 0;
            this.lblNamaWarga.Text = "Halo, Warga!";
            this.lblNamaWarga.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRole
            // 
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblRole.Location = new System.Drawing.Point(9, 55);
            this.lblRole.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(177, 19);
            this.lblRole.TabIndex = 1;
            this.lblRole.Text = "Masyarakat Umum";
            this.lblRole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnLogOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogOut.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogOut.ForeColor = System.Drawing.Color.White;
            this.btnLogOut.Location = new System.Drawing.Point(19, 474);
            this.btnLogOut.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(158, 37);
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
            this.panelHeader.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(854, 57);
            this.panelHeader.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.btnExit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(34, 24);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dgvBarang
            // 
            this.dgvBarang.AllowUserToAddRows = false;
            this.dgvBarang.AllowUserToDeleteRows = false;
            this.dgvBarang.BackgroundColor = System.Drawing.Color.White;
            this.dgvBarang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBarang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBarang.Location = new System.Drawing.Point(216, 114);
            this.dgvBarang.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvBarang.Name = "dgvBarang";
            this.dgvBarang.ReadOnly = true;
            this.dgvBarang.RowHeadersWidth = 51;
            this.dgvBarang.RowTemplate.Height = 24;
            this.dgvBarang.Size = new System.Drawing.Size(491, 447);
            this.dgvBarang.TabIndex = 2;
            this.dgvBarang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBarang_CellClick);
            // 
            // lblTabel
            // 
            this.lblTabel.AutoSize = true;
            this.lblTabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblTabel.Location = new System.Drawing.Point(218, 81);
            this.lblTabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTabel.Name = "lblTabel";
            this.lblTabel.Size = new System.Drawing.Size(136, 21);
            this.lblTabel.TabIndex = 3;
            this.lblTabel.Text = "Daftar Aset Desa";
            // 
            // panelPinjam
            // 
            this.panelPinjam.BackColor = System.Drawing.Color.White;
            this.panelPinjam.Controls.Add(this.btnPinjam);
            this.panelPinjam.Controls.Add(this.dtpKembali);
            this.panelPinjam.Controls.Add(this.lblTglKembali);
            this.panelPinjam.Controls.Add(this.dtpPinjam);
            this.panelPinjam.Controls.Add(this.lblTglPinjam);
            this.panelPinjam.Controls.Add(this.txtJumlah);
            this.panelPinjam.Controls.Add(this.lblJumlah);
            this.panelPinjam.Controls.Add(this.txtNamaBarang);
            this.panelPinjam.Controls.Add(this.lblNamaBarang);
            this.panelPinjam.Controls.Add(this.lblFormPinjam);
            this.panelPinjam.Location = new System.Drawing.Point(724, 114);
            this.panelPinjam.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelPinjam.Name = "panelPinjam";
            this.panelPinjam.Size = new System.Drawing.Size(304, 447);
            this.panelPinjam.TabIndex = 4;
            // 
            // btnPinjam
            // 
            this.btnPinjam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnPinjam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPinjam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPinjam.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnPinjam.ForeColor = System.Drawing.Color.White;
            this.btnPinjam.Location = new System.Drawing.Point(19, 374);
            this.btnPinjam.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnPinjam.Name = "btnPinjam";
            this.btnPinjam.Size = new System.Drawing.Size(266, 37);
            this.btnPinjam.TabIndex = 9;
            this.btnPinjam.Text = "Ajukan Peminjaman";
            this.btnPinjam.UseVisualStyleBackColor = false;
            this.btnPinjam.Click += new System.EventHandler(this.btnPinjam_Click);
            // 
            // dtpKembali
            // 
            this.dtpKembali.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpKembali.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpKembali.Location = new System.Drawing.Point(19, 309);
            this.dtpKembali.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpKembali.Name = "dtpKembali";
            this.dtpKembali.Size = new System.Drawing.Size(267, 27);
            this.dtpKembali.TabIndex = 8;
            // 
            // lblTglKembali
            // 
            this.lblTglKembali.AutoSize = true;
            this.lblTglKembali.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTglKembali.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblTglKembali.Location = new System.Drawing.Point(16, 284);
            this.lblTglKembali.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTglKembali.Name = "lblTglKembali";
            this.lblTglKembali.Size = new System.Drawing.Size(121, 19);
            this.lblTglKembali.TabIndex = 7;
            this.lblTglKembali.Text = "Tanggal Kembali";
            // 
            // dtpPinjam
            // 
            this.dtpPinjam.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpPinjam.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPinjam.Location = new System.Drawing.Point(19, 236);
            this.dtpPinjam.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpPinjam.Name = "dtpPinjam";
            this.dtpPinjam.Size = new System.Drawing.Size(267, 27);
            this.dtpPinjam.TabIndex = 6;
            // 
            // lblTglPinjam
            // 
            this.lblTglPinjam.AutoSize = true;
            this.lblTglPinjam.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTglPinjam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblTglPinjam.Location = new System.Drawing.Point(16, 211);
            this.lblTglPinjam.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTglPinjam.Name = "lblTglPinjam";
            this.lblTglPinjam.Size = new System.Drawing.Size(112, 19);
            this.lblTglPinjam.TabIndex = 5;
            this.lblTglPinjam.Text = "Tanggal Pinjam";
            // 
            // txtJumlah
            // 
            this.txtJumlah.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtJumlah.Location = new System.Drawing.Point(19, 162);
            this.txtJumlah.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(267, 27);
            this.txtJumlah.TabIndex = 4;
            // 
            // lblJumlah
            // 
            this.lblJumlah.AutoSize = true;
            this.lblJumlah.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblJumlah.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblJumlah.Location = new System.Drawing.Point(16, 138);
            this.lblJumlah.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblJumlah.Name = "lblJumlah";
            this.lblJumlah.Size = new System.Drawing.Size(106, 19);
            this.lblJumlah.TabIndex = 3;
            this.lblJumlah.Text = "Jumlah Pinjam";
            // 
            // txtNamaBarang
            // 
            this.txtNamaBarang.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNamaBarang.Location = new System.Drawing.Point(19, 89);
            this.txtNamaBarang.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNamaBarang.Name = "txtNamaBarang";
            this.txtNamaBarang.ReadOnly = true;
            this.txtNamaBarang.Size = new System.Drawing.Size(267, 27);
            this.txtNamaBarang.TabIndex = 2;
            // 
            // lblNamaBarang
            // 
            this.lblNamaBarang.AutoSize = true;
            this.lblNamaBarang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNamaBarang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblNamaBarang.Location = new System.Drawing.Point(16, 65);
            this.lblNamaBarang.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNamaBarang.Name = "lblNamaBarang";
            this.lblNamaBarang.Size = new System.Drawing.Size(101, 19);
            this.lblNamaBarang.TabIndex = 1;
            this.lblNamaBarang.Text = "Nama Barang";
            // 
            // lblFormPinjam
            // 
            this.lblFormPinjam.AutoSize = true;
            this.lblFormPinjam.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormPinjam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblFormPinjam.Location = new System.Drawing.Point(15, 20);
            this.lblFormPinjam.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFormPinjam.Name = "lblFormPinjam";
            this.lblFormPinjam.Size = new System.Drawing.Size(150, 21);
            this.lblFormPinjam.TabIndex = 0;
            this.lblFormPinjam.Text = "Form Peminjaman";
            // 
            // FormDashboardWarga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1046, 589);
            this.Controls.Add(this.panelPinjam);
            this.Controls.Add(this.lblTabel);
            this.Controls.Add(this.dgvBarang);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormDashboardWarga";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard Warga";
            this.Load += new System.EventHandler(this.FormDashboardWarga_Load);
            this.panelSidebar.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarang)).EndInit();
            this.panelPinjam.ResumeLayout(false);
            this.panelPinjam.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Label lblNamaWarga;
        private System.Windows.Forms.Label lblRole;
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
        private System.Windows.Forms.DateTimePicker dtpKembali;
        private System.Windows.Forms.Label lblTglKembali;
        private System.Windows.Forms.Button btnPinjam;
    }
}