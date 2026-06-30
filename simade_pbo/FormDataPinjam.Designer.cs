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
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnKembali = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.lblNamaWarga = new System.Windows.Forms.Label();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeaderDataPinjam = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.dgvTabelList = new System.Windows.Forms.DataGridView();
            this.Peminjam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Barang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Waktu_Pinjam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Waktu_Kembali = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aksi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelSidebar.SuspendLayout();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabelList)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.panelSidebar.Controls.Add(this.btnKembali);
            this.panelSidebar.Controls.Add(this.btnDashboard);
            this.panelSidebar.Controls.Add(this.lblNamaWarga);
            this.panelSidebar.Controls.Add(this.btnLogOut);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(256, 775);
            this.panelSidebar.TabIndex = 2;
            // 
            // btnKembali
            // 
            this.btnKembali.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnKembali.FlatAppearance.BorderSize = 0;
            this.btnKembali.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKembali.ForeColor = System.Drawing.Color.White;
            this.btnKembali.Location = new System.Drawing.Point(25, 148);
            this.btnKembali.Margin = new System.Windows.Forms.Padding(4);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(211, 46);
            this.btnKembali.TabIndex = 4;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = false;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(25, 86);
            this.btnDashboard.Margin = new System.Windows.Forms.Padding(4);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(211, 46);
            this.btnDashboard.TabIndex = 3;
            this.btnDashboard.Text = "Tambah Admin";
            this.btnDashboard.UseVisualStyleBackColor = false;
            // 
            // lblNamaWarga
            // 
            this.lblNamaWarga.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblNamaWarga.ForeColor = System.Drawing.Color.White;
            this.lblNamaWarga.Location = new System.Drawing.Point(11, 22);
            this.lblNamaWarga.Name = "lblNamaWarga";
            this.lblNamaWarga.Size = new System.Drawing.Size(236, 28);
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
            this.btnLogOut.Location = new System.Drawing.Point(25, 617);
            this.btnLogOut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(211, 46);
            this.btnLogOut.TabIndex = 2;
            this.btnLogOut.Text = "Log Out";
            this.btnLogOut.UseVisualStyleBackColor = false;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblHeaderDataPinjam);
            this.panelHeader.Controls.Add(this.btnExit);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(256, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(996, 70);
            this.panelHeader.TabIndex = 3;
            // 
            // lblHeaderDataPinjam
            // 
            this.lblHeaderDataPinjam.AutoSize = true;
            this.lblHeaderDataPinjam.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderDataPinjam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblHeaderDataPinjam.Location = new System.Drawing.Point(25, 16);
            this.lblHeaderDataPinjam.Name = "lblHeaderDataPinjam";
            this.lblHeaderDataPinjam.Size = new System.Drawing.Size(302, 37);
            this.lblHeaderDataPinjam.TabIndex = 0;
            this.lblHeaderDataPinjam.Text = "SIMADE - Data Pinjam";
            this.lblHeaderDataPinjam.Click += new System.EventHandler(this.lblHeaderDataPinjam_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Red;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(1045, 21);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(45, 30);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            // 
            // dgvTabelList
            // 
            this.dgvTabelList.AllowUserToAddRows = false;
            this.dgvTabelList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTabelList.BackgroundColor = System.Drawing.Color.White;
            this.dgvTabelList.ColumnHeadersHeight = 35;
            this.dgvTabelList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTabelList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Peminjam,
            this.Barang,
            this.Waktu_Pinjam,
            this.Waktu_Kembali,
            this.aksi});
            this.dgvTabelList.Location = new System.Drawing.Point(300, 86);
            this.dgvTabelList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvTabelList.Name = "dgvTabelList";
            this.dgvTabelList.ReadOnly = true;
            this.dgvTabelList.RowHeadersWidth = 51;
            this.dgvTabelList.Size = new System.Drawing.Size(913, 489);
            this.dgvTabelList.TabIndex = 18;
            // 
            // Peminjam
            // 
            this.Peminjam.HeaderText = "Nama Peminjam";
            this.Peminjam.MinimumWidth = 6;
            this.Peminjam.Name = "Peminjam";
            this.Peminjam.ReadOnly = true;
            // 
            // Barang
            // 
            this.Barang.HeaderText = "Nama Barang";
            this.Barang.MinimumWidth = 6;
            this.Barang.Name = "Barang";
            this.Barang.ReadOnly = true;
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
            // aksi
            // 
            this.aksi.HeaderText = "Aksi";
            this.aksi.MinimumWidth = 6;
            this.aksi.Name = "aksi";
            this.aksi.ReadOnly = true;
            // 
            // FormDataPinjam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 775);
            this.Controls.Add(this.dgvTabelList);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelSidebar);
            this.Name = "FormDataPinjam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panelSidebar.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabelList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Button btnKembali;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Label lblNamaWarga;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeaderDataPinjam;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dgvTabelList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Peminjam;
        private System.Windows.Forms.DataGridViewTextBoxColumn Barang;
        private System.Windows.Forms.DataGridViewTextBoxColumn Waktu_Pinjam;
        private System.Windows.Forms.DataGridViewTextBoxColumn Waktu_Kembali;
        private System.Windows.Forms.DataGridViewTextBoxColumn aksi;
    }
}