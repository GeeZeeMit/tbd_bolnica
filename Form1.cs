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
        private SqlCommand command;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["blonicaBD"].ConnectionString);

            sqlConnection.Open();

        }

        // Ввод данных
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

            procedure($"EXECUTE procedure_Dolzhnost @Nazvanie = N'{textBox8.Text}', @Zarplata = N'{textBox9.Text}';");

        }

        private void procedure(string str)
        {
            SqlCommand command = new SqlCommand(str, sqlConnection);
            if (command.ExecuteNonQuery().ToString() == "-1")
            {
                MessageBox.Show("Ошибка!");
            }
            command.Dispose();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            procedure($"EXECUTE procedure_Analizi @Nazvanie = N'{textBox11.Text}';");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            procedure($"EXECUTE procedure_Lekarstva @Nazvanie = N'{textBox13.Text}', @Opisanie = N'{textBox14.Text}', @Strana_Proizvoditelya = N'{textBox15.Text}';");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DateTime dateP = DateTime.Parse(textBox28.Text);
            DateTime dateV = DateTime.Parse(textBox30.Text);

            procedure($"EXECUTE procedure_Istoria_Bolezni @DiagnoziID = N'{textBox26.Text}', " +
                $"@PacientiID = N'{textBox27.Text}', @Data_Postupleniya = N'{dateP.Month}/{dateP.Day}/{dateP.Year}'," +
                $" @SotrudnikiID = N'{textBox29.Text}', @Data_Vipiski = N'{dateV.Month}/{dateV.Day}/{dateV.Year}';");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            procedure($"EXECUTE procedure_Diagnozi @Nazvanie = N'{textBox16.Text}', @Opisanie = N'{textBox17.Text}';");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            procedure($"EXECUTE procedure_Pacienti @Familiya = N'{textBox18.Text}', @Imya = N'{textBox19.Text}', @Otchestvo = N'{textBox20.Text}'," +
                $" @Pasport = N'{textBox21.Text}';");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            DateTime dateL = DateTime.Parse(textBox22.Text);

            procedure($"EXECUTE procedure_Lechenie @Data_Lesheniya = N'{dateL.Month}/{dateL.Day}/{dateL.Year}', @SotrudnikiID = N'{textBox23.Text}', @LekarstvaID = N'{textBox24.Text}'," +
                $" @Kolichestvo = N'{textBox25.Text}';");
        }

        // Таблицы
        

        private void button3_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                "SELECT * FROM Должность",
                sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                "SELECT * FROM Диагнозы",
                sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                "SELECT * FROM Анализы",
                sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                "SELECT * FROM История_Болезни",
                sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                "SELECT * FROM Лекарства",
                sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                "SELECT * FROM Лечение",
                sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                "SELECT * FROM Пациенты",
                sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                "SELECT * FROM Сотрудники",
                sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];
        }

        
    }
}
