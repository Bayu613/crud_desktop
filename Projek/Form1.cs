using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace Projek
{
    public partial class Form1 : Form
    {
        string koneksistring = "Server=localhost;Database=desktop;Uid=root;Pwd=;SslMode=None;";
        MySqlConnection koneksi;

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            koneksi = new MySqlConnection(koneksistring);
            try
            {
                koneksi.Open();
                MessageBox.Show("Connected to MySQL!");
                koneksi.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to connect: " + ex.Message);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (MySqlconnect())
            {
                try
                {
                    string query = "SELECT * FROM karyawan";
                    var cmd = new MySqlCommand(query, koneksi);
                    var da = new MySqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt); // Isi DataTable dengan data dari database
                    dataGridView1.DataSource = dt; // Tampilkan data di DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching data: " + ex.Message);
                }
                finally
                {
                    MySqldisconnect();
                }
            }
        }



        private bool MySqlconnect()
        {
            try
            {
                koneksi = new MySqlConnection(koneksistring);
                koneksi.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to connect to database: " + ex.Message);
                return false;
            }
        }


        private void MySqldisconnect()
        {
            if (koneksi != null && koneksi.State == System.Data.ConnectionState.Open)
            {
                koneksi.Close();
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Silahkan isi semua kolom!!");
                return;
            }
            string getnik = textBox1.Text;
            string getnama = textBox2.Text;
            string getjabatan = textBox3.Text;
            string getalamat = textBox4.Text;
            string getemail = textBox5.Text;
            string getnohp = textBox6.Text;

            if (MySqlconnect())
            {
                string query = "INSERT INTO karyawan (nik, nama, jabatan, alamat, email, nohp) " +
                               "VALUES (@nik, @nama, @jabatan, @alamat, @email, @nohp)";

                try
                {
                    var cmd = new MySqlCommand(query, koneksi);
                    cmd.Parameters.AddWithValue("@nik", getnik);
                    cmd.Parameters.AddWithValue("@nama", getnama);
                    cmd.Parameters.AddWithValue("@jabatan", getjabatan);
                    cmd.Parameters.AddWithValue("@alamat", getalamat);
                    cmd.Parameters.AddWithValue("@email", getemail);
                    cmd.Parameters.AddWithValue("@nohp", getnohp);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Ditambahakan!!!");
                    Tampil();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    MySqldisconnect();
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.PlaceholderText = "Masukkan NIP...";
            textBox2.PlaceholderText = "Masukkan Nama...";
            textBox3.PlaceholderText = "Masukkan Jabatan...";
            textBox4.PlaceholderText = "Masukkan Alamat...";
            textBox5.PlaceholderText = "Masukkan Email...";
            textBox6.PlaceholderText = "Masukkan NoTelp...";
            textBox7.PlaceholderText = "Masukkan Data...";


            Tampil(); // Load data terlebih dahulu
            if (dataGridView1.Columns.Count > 0) // Pastikan kolom sudah terisi
            {
                dataGridView1.Columns[0].HeaderText = "NIP";
                dataGridView1.Columns[1].HeaderText = "Nama Karyawan";
                dataGridView1.Columns[2].HeaderText = "Jabatan";
                dataGridView1.Columns[3].HeaderText = "Alamat";
                dataGridView1.Columns[4].HeaderText = "Email";
                dataGridView1.Columns[5].HeaderText = "Nomor Telephone";

            }

        }


        public void Tampil()
        {
            if (MySqlconnect())
            {
                try
                {
                    string query = "SELECT * FROM karyawan";
                    var cmd = new MySqlCommand(query, koneksi);
                    var da = new MySqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    MySqldisconnect();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string keyword = textBox7.Text.Trim(); // Ambil input dari tekbok untuk kata kunci

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("silakan masukkan data");
                return;
            }

            if (MySqlconnect())
            {
                try
                {
                    string query = "SELECT * FROM karyawan WHERE nik LIKE @keyword OR nama LIKE @keyword";
                    var cmd = new MySqlCommand(query, koneksi);
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%"); // Menambahkan wildcard untuk pencarian
                    var da = new MySqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Tidak ada Data yang cocok");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex.Message);
                }
                finally
                {
                    MySqldisconnect();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih Data Untuk Dihapus");
                return;
            }

            DialogResult result = MessageBox.Show("Apakah kamu yakin untuk menghapus data ini??!", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string selectedNIK = dataGridView1.SelectedRows[0].Cells["nik"].Value.ToString();

                if (MySqlconnect())
                {
                    try
                    {
                        string query = "DELETE FROM karyawan WHERE nik = @nik";
                        var cmd = new MySqlCommand(query, koneksi);
                        cmd.Parameters.AddWithValue("@nik", selectedNIK);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Berhasil Dihapus!!!");
                        Tampil();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error" + ex.Message);
                    }
                    finally
                    {
                        MySqldisconnect();
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("pilih baris yang akan diedit!!!!");
                return;
            }


            string selectedNIK = dataGridView1.SelectedRows[0].Cells["nik"].Value.ToString();


            string updatedNama = textBox2.Text.Trim();
            string updatedJabatan = textBox3.Text.Trim();
            string updatedAlamat = textBox4.Text.Trim();
            string updatedEmail = textBox5.Text.Trim();
            string updatedNoHp = textBox6.Text.Trim();


            if (string.IsNullOrWhiteSpace(updatedNama) ||
                string.IsNullOrWhiteSpace(updatedJabatan) ||
                string.IsNullOrWhiteSpace(updatedAlamat) ||
                string.IsNullOrWhiteSpace(updatedEmail) ||
                string.IsNullOrWhiteSpace(updatedNoHp))
            {
                MessageBox.Show("Silahkan Isi semua kolom!!!!");
                return;
            }

            if (MySqlconnect())
            {
                try
                {

                    string query = "UPDATE karyawan SET nama = @nama, jabatan = @jabatan, alamat = @alamat, email = @email, nohp = @nohp WHERE nik = @nik";
                    var cmd = new MySqlCommand(query, koneksi);


                    cmd.Parameters.AddWithValue("@nik", selectedNIK);
                    cmd.Parameters.AddWithValue("@nama", updatedNama);
                    cmd.Parameters.AddWithValue("@jabatan", updatedJabatan);
                    cmd.Parameters.AddWithValue("@alamat", updatedAlamat);
                    cmd.Parameters.AddWithValue("@email", updatedEmail);
                    cmd.Parameters.AddWithValue("@nohp", updatedNoHp);


                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data berhasil diedit!");
                    Tampil();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message);
                }
                finally
                {
                    MySqldisconnect();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
