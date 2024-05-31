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
using System.Threading;

namespace Kyrca4
{
    public partial class Form2 : Form
    {
        string connecionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PoliceDB;Integrated Security=True";
        bool vol=true;
        string id;
        public Form2(string s)
        {
            InitializeComponent();
            id = s;
            if (id != null)
            {
                vol = false;
                button1.Text = "Изменить";
                using (SqlConnection connection = new SqlConnection(connecionString))
                {
                    SqlCommand command = new SqlCommand("select * from Сотрудники WHERE id = " + id, connection);
                    connection.Open();
                    SqlDataReader read = command.ExecuteReader();
                    while (read.Read())
                    {
                        textBox1.Text = (read["Фамилия"].ToString());
                        textBox2.Text = (read["Имя"].ToString());
                        textBox3.Text = (read["Отчество"].ToString());
                        textBox4.Text = (read["Адрес"].ToString());
                        maskedTextBox2.Text= (read["Номер_телефона"].ToString());
                        maskedTextBox3.Text = (read["Паспортные_данные"].ToString());
                        maskedTextBox1.Text = (read["Дата_рождения"].ToString());
                        string pol = (read["Пол"].ToString());
                        if (pol == "1")
                        {
                            radioButton1.Checked = true;
                        }
                        if (pol == "2")
                        {
                            radioButton2.Checked = true;
                        }
                    }
                    read.Close();
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "policeDBDataSet.Звания". При необходимости она может быть перемещена или удалена.
            this.званияTableAdapter.Fill(this.policeDBDataSet.Звания);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "policeDBDataSet.Города". При необходимости она может быть перемещена или удалена.
            this.городаTableAdapter.Fill(this.policeDBDataSet.Города);
            string sql = "SELECT Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Звания.Наименование, Города.Название_города, Сотрудники.Адрес, Сотрудники.Дата_рождения, Сотрудники.Номер_телефона, Сотрудники.Паспортные_данные, Пол.Пол FROM Сотрудники JOIN Пол on Пол.id=Сотрудники.Пол JOIN Звания on Звания.id=Сотрудники.Звание JOIN Города on Города.id=Сотрудники.Город";
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
            if (vol==true)
            {
                try
                {
                    string pol = "";
                    if (radioButton1.Checked == true)
                    {
                        pol = "1";
                    }
                    if (radioButton2.Checked == true)
                    {
                        pol = "2";
                    }
                    string sql = "Insert into Сотрудники (Фамилия, Имя, Отчество, Звание, Город, Адрес, Дата_рождения, Номер_телефона, Паспортные_данные, Пол) Values (N'" + textBox1.Text + "',N'" + textBox2.Text + "',N'" + textBox3.Text + "',N'" + comboBox2.SelectedValue + "', N'" + comboBox1.SelectedValue + "',N'" + textBox4.Text + "',N'" + maskedTextBox1.Text + "',N'" + maskedTextBox2.Text + "',N'" + maskedTextBox3.Text + "',N'" + pol + "')";
                    using (SqlConnection connection = new SqlConnection(connecionString))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        dataGridView1.DataSource = ds.Tables["Сотрудники"];
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
                    string pol = "";
                    if (radioButton1.Checked == true)
                    {
                        pol = "1";
                    }
                    if (radioButton2.Checked == true)
                    {
                        pol = "2";
                    }
                    string sql = "update Сотрудники set Фамилия = N'" + textBox1.Text + "', Имя = N'" + textBox2.Text + "' , Отчество = N'" + textBox3.Text + "',Звание = '" + comboBox2.SelectedValue + "', Город = '" + comboBox1.SelectedValue + "', Адрес = N'" + textBox4.Text + "', Дата_рождения = N'" + maskedTextBox1.Text + "', Номер_телефона = N'" + maskedTextBox2.Text + "', Паспортные_данные= N'" + maskedTextBox3.Text + "', Пол= N'" + pol + "' where id = " + id;
                    using (SqlConnection connection = new SqlConnection(connecionString))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        dataGridView1.DataSource = ds.Tables["Сотрудники"];
                    }
                    this.Close();
                    MessageBox.Show("Запись обновлена", "Обновлено");
                }
                catch (Exception)
                {
                    MessageBox.Show("Что-то пошло не так", "Ошибка");
                    throw;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
