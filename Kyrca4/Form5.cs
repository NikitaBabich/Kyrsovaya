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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kyrca4
{
    public partial class Form5 : Form
    {
        string connecionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PoliceDB;Integrated Security=True";
        bool vol = true;
        string id;
        public Form5(string s)
        {
            InitializeComponent();
            id = s;
            if (id != null)
            {
                vol = false;
                button2.Text = "Изменить";
                using (SqlConnection connection = new SqlConnection(connecionString))
                {
                    SqlCommand command = new SqlCommand("select * from Виды_преступлений WHERE id = " + id, connection);
                    connection.Open();
                    SqlDataReader read = command.ExecuteReader();
                    while (read.Read())
                    {
                        textBox1.Text = (read["Наименование"].ToString());
                        textBox2.Text = (read["Статья"].ToString());
                        textBox3.Text = (read["Наказание"].ToString());
                    }
                    read.Close();
                }
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
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
            if (vol == true)
            {
                try
                {
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
            else
            {
                try
                {
                    string sql = "update Виды_преступлений set Наименование = N'" + textBox1.Text + "', Статья = N'" + textBox2.Text + "' , Наказание = N'" + textBox3.Text + "' where id = " + id;
                    using (SqlConnection connection = new SqlConnection(connecionString))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        dataGridView1.DataSource = ds.Tables["Виды_преступлений"];
                    }
                    this.Close();
                    MessageBox.Show("Запись изменена", "Изменено");
                }
                catch (Exception)
                {
                    MessageBox.Show("Что-то пошло не так", "Ошибка");
                    throw;
                }
            }
        }
    }
}
