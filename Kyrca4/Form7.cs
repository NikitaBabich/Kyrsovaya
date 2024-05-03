using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kyrca4
{
    public partial class Form7 : Form
    {
        string connecionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PoliceDB;Integrated Security=True";
        public Form7(string s)
        {
            InitializeComponent();
        }
        private void Form7_Load(object sender, EventArgs e)
        {
            string sql = "SELECT Дела.Код_дела,Виды_преступлений.Статья, Сотрудники.Фамилия, Пострадавшие.Фамилия,Подозреваемые.Фамилия FROM Дела JOIN Виды_преступлений on Виды_преступлений.id=Дела.Статья JOIN Сотрудники on Сотрудники.id=Дела.Сотрудник JOIN Пострадавшие on Пострадавшие.id=Дела.Пострадавший JOIN Подозреваемые on Подозреваемые.id=Дела.Подозреваемый";
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
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "Insert into Дела (Код_дела, Статья, Сотрудник, Пострадавший, Подозреваемый) Values (N'" + textBox1.Text + "','" + comboBox1.SelectedValue + "','" + comboBox2.SelectedValue + "','" + comboBox3.SelectedValue + "','" + comboBox4.SelectedValue + "')";
                using (SqlConnection connection = new SqlConnection(connecionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables["Звания"];
                }
                this.Close();
                MessageBox.Show("Запись " + textBox1.Text + " добавлена", "Добавлено");
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так", "Ошибка");
                throw;
            }
        }
    }
}
