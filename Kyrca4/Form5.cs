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
    public partial class Form5 : Form
    {
        public Form5(string s)
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            string connecionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PoliceDB;Integrated Security=True";
            string sql = "SELECT Виды_преступлений.Статья,Виды_преступлений.Наименование,Виды_преступлений.Наказание FROM Виды_преступлений";
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
                string connecionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PoliceDB;Integrated Security=True";
                string sql = "Insert into Виды_преступлений (Наименование, Статья, Наказание) Values (N'" + textBox1.Text + "','" + textBox2.Text + "',N'" + textBox3.Text + "')";
                using (SqlConnection connection = new SqlConnection(connecionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables["Виды_преступлений"];
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
    }
}
