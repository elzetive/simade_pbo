using simade_pbo.Service;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace simade_pbo
{
    public partial class FormDashboardKades : Form
    {
        Barang_service barang = new Barang_service();

        public FormDashboardKades()
        {
            InitializeComponent();

            lblTotal.Text = barang.hitungStatistik("TOTAL");
            lblTersedia.Text = barang.hitungStatistik("TERSEDIA");
            lblPinjam.Text = barang.hitungStatistik("DIPINJAM");
        }

        private void FormDashboardKades_Load(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void LoadChart()
        {
            chart1.Series.Clear();
            chart1.Titles.Clear();

            chart1.Titles.Add("Barang yang Paling Sering Dipinjam");

            ChartArea area = chart1.ChartAreas[0];

            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.Enabled = false;

            DataTable dt = barang.GrafikBarangTerpinjam();

            Series series = new Series();
            series.ChartType = SeriesChartType.Bar;
            series.Color = Color.RoyalBlue;
            series.IsValueShownAsLabel = true;
            series.Font = new Font("Poppins", 10, FontStyle.Bold);

            chart1.Series.Add(series);

            // HILANGKAN tulisan "Jumlah Dipinjam"
            chart1.Legends.Clear();

            foreach (DataRow row in dt.Rows)
            {
                string namaBarang = row["nama_barang"].ToString();
                int jumlah = Convert.ToInt32(row["jumlah"]);

                series.Points.AddXY(namaBarang, jumlah);
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            // Sudah berada di Dashboard
        }

        private void btnBarang_Click(object sender, EventArgs e)
        {
            FormBarangKades frm = new FormBarangKades();
            frm.Show();
            this.Hide();
        }

        private void btnRiwayat_Click(object sender, EventArgs e)
        {
            FormRiwayat frm = new FormRiwayat();
            frm.Show();
            this.Hide();
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            FormLogin login = new FormLogin();
            login.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }

        private void lblTersedia_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}