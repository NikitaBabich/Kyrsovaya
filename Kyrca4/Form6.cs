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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kyrca4
{
    public partial class Form6 : Form
    {
        string connecionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PoliceDB;Integrated Security=True";
        bool vol = true;
        int id;
        public Form6(string s)
        {
            InitializeComponent();
            id = Convert.ToInt32(s);
            if (s != null)
            {
                vol = false;
                button2.Text = "Изменить";
                using (SqlConnection connection = new SqlConnection(connecionString))
                {
                    SqlCommand command = new SqlCommand("select * from Звания WHERE id = " + s, connection);
                    connection.Open();
                    SqlDataReader read = command.ExecuteReader();
                    while (read.Read())
                    {
                        textBox1.Text = (read["Наименование"].ToString());
                        textBox2.Text = (read["Оклад"].ToString());
                        textBox3.Text = (read["Обязанности"].ToString());
                        textBox4.Text = (read["Требования"].ToString());
                    }
                    read.Close();
                }
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            string sql = "SELECT Звания.Наименование,Звания.Оклад,Звания.Обязанности,Звания.Требования FROM Звания";
            using (SqlConnection connection = new SqlConnection(connecionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (vol = false)
            {
                try
                {
                    string sql = "Insert into Звания (Наименование, Оклад, Обязанности, Требования) Values (N'" + textBox1.Text + "','" + textBox2.Text + "',N'" + textBox3.Text + "',N'" + textBox4.Text + "')";
                    using (SqlConnection connection = new SqlConnection(connecionString))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        dataGridView1.DataSource = ds.Tables["Звания"];
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
                    string sql = "update Звания set Наименование = N'" + textBox1.Text + "', Оклад = N'" + textBox2.Text + "' , Обязанности = N'" + textBox3.Text + "', Требования = N'" + textBox4.Text + "' where id = " + id;
                    using (SqlConnection connection = new SqlConnection(connecionString))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        dataGridView1.DataSource = ds.Tables["Звания"];
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
