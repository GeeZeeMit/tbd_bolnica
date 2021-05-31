using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
namespace tbd_sql_bolnica
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["blonicaBD"].ConnectionString);

            sqlConnection.Open();

            if (sqlConnection.State == ConnectionState.Open)
            {
                MessageBox.Show("Подключение установлено");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand(
                $"INSERT INTO [Sotrudniki] (DolzhnostID, Familiya, Imya, Otchestvo, Pasport, Data_Ustroistva, Data_Uvolneniya, Nomer_Telefona)" +
                $" VALUES (@DolzhnostID, @Familiya, @Imya, @Otchestvo, @Pasport, @Data_Ustroistva, @Data_Uvolneniya, @Nomer_Telefona)",
                sqlConnection);

            DateTime dateUS = DateTime.Parse(textBox5.Text);
            DateTime dateUV = DateTime.Parse(textBox6.Text);

            command.Parameters.AddWithValue("DolzhnostID", textBox10.Text);
            command.Parameters.AddWithValue("Familiya", textBox1.Text);
            command.Parameters.AddWithValue("Imya", textBox2.Text);
            command.Parameters.AddWithValue("Otchestvo", textBox3.Text);
            command.Parameters.AddWithValue("Pasport", textBox4.Text);
            command.Parameters.AddWithValue("Data_Ustroistva", $"{dateUS.Month}/{dateUS.Day}/{dateUS.Year}");
            command.Parameters.AddWithValue("Data_Uvolneniya", $"{dateUV.Month}/{dateUV.Day}/{dateUV.Year}");
            command.Parameters.AddWithValue("Nomer_Telefona", textBox7.Text);

            MessageBox.Show(command.ExecuteNonQuery().ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand(
                $"INSERT INTO [Dolzhnost] (Nazvanie, Zarplata) VALUES (@Nazvanie, @Zarplata)",
                sqlConnection);

            command.Parameters.AddWithValue("Nazvanie", textBox8.Text);
            command.Parameters.AddWithValue("Zarplata", textBox9.Text);

            MessageBox.Show(command.ExecuteNonQuery().ToString());

        }
    }
}
