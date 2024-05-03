using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Kyrca4
{
    public partial class Form4 : Form
    {
        public Form4(string s)
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "policeDBDataSet.Города". При необходимости она может быть перемещена или удалена.
            this.городаTableAdapter.Fill(this.policeDBDataSet.Города);
            string connecionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PoliceDB;Integrated Security=True";
            string sql = "SELECT Подозреваемые.Фамилия,Подозреваемые.Имя,Подозреваемые.Отчество,Города.Название_города, Подозреваемые.Адрес,Подозреваемые.Дата_рождения,Подозреваемые.Номер_телефона,Подозреваемые.Паспортные_данные, Пол.Пол FROM Подозреваемые JOIN Пол on Пол.id=Подозреваемые.Пол JOIN Города on Города.id=Подозреваемые.Город";
            using (SqlConnection connection = new SqlConnection(connecionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int pol = 0;
                if (radioButton1.Checked == true)
                {
                    pol = 1;
                }
                if (radioButton2.Checked == true)
                {
                    pol = 2;
                }
                string connecionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PoliceDB;Integrated Security=True";
                string sql = "Insert into Подозреваемые (Фамилия,Имя,Отчество,Город,Адрес,Дата_рождения,Номер_телефона,Паспортные_данные,Пол) Values (N'" + textBox1.Text + "',N'" + textBox2.Text + "',N'" + textBox3.Text + "', N'" + comboBox1.SelectedValue + "',N'" + textBox4.Text + "',N'" + maskedTextBox1.Text + "',N'" + maskedTextBox2.Text + "',N'" + maskedTextBox3.Text + "',N'" + pol + "')";
                using (SqlConnection connection = new SqlConnection(connecionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables["Подозреваемые"];
                }
                this.Close();
                MessageBox.Show("Запись добавлена", "Добавлено");
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так", "Ошибка");
                throw;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
