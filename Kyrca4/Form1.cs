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

    public partial class Form1 : Form
    {
        //Запросы
        string connecionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PoliceDB;Integrated Security=True";
        string Dela = "SELECT Дела.id,Дела.Код_дела,Виды_преступлений.Статья, Сотрудники.Фамилия, Пострадавшие.Фамилия,Подозреваемые.Фамилия FROM Дела JOIN Виды_преступлений on Виды_преступлений.id=Дела.Статья JOIN Сотрудники on Сотрудники.id=Дела.Сотрудник JOIN Пострадавшие on Пострадавшие.id=Дела.Пострадавший JOIN Подозреваемые on Подозреваемые.id=Дела.Подозреваемый";
        string Sotr = "SELECT Сотрудники.id,Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Звания.Наименование, Города.Название_города, Сотрудники.Адрес, Сотрудники.Дата_рождения, Сотрудники.Номер_телефона, Сотрудники.Паспортные_данные, Пол.Пол FROM Сотрудники JOIN Звания on Звания.id=Сотрудники.Звание JOIN Пол on Пол.id=Сотрудники.Пол JOIN Города on Города.id=Сотрудники.Город";
        string Postr = "SELECT Пострадавшие.id,Пострадавшие.Фамилия,Пострадавшие.Имя,Пострадавшие.Отчество,Города.Название_города,Пострадавшие.Адрес,Пострадавшие.Дата_рождения,Пострадавшие.Номер_телефона,Пострадавшие.Паспортные_данные,Пострадавшие.Дата_обращения,Пол.Пол FROM Пострадавшие JOIN Пол on Пол.id=Пострадавшие.Пол JOIN Города on Города.id=Пострадавшие.Город";
        string Podozr = "SELECT Подозреваемые.id,Подозреваемые.Фамилия,Подозреваемые.Имя,Подозреваемые.Отчество,Города.Название_города, Подозреваемые.Адрес,Подозреваемые.Дата_рождения,Подозреваемые.Номер_телефона,Подозреваемые.Паспортные_данные, Пол.Пол FROM Подозреваемые JOIN Пол on Пол.id=Подозреваемые.Пол JOIN Города on Города.id=Подозреваемые.Город";
        string Prest = "SELECT Виды_преступлений.id,Виды_преступлений.Статья,Виды_преступлений.Наименование,Виды_преступлений.Наказание FROM Виды_преступлений";
        string Zvan = "SELECT Звания.id,Звания.Наименование,Звания.Оклад,Звания.Обязанности,Звания.Требования FROM Звания";
        //Метод обращения к запросу
        public void Using(string sql)
        {
            using (SqlConnection connection = new SqlConnection(connecionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
            }
        }
        string tfio;
        string tvol;
        string priv;
        public Form1(string vol, string fio)
        {
            tfio = fio;
            tvol = vol;
            InitializeComponent();
            if (tvol == "1")
            {
                priv = "Все возможности";
            }
            if (tvol == "2")
            {
                priv = "Добавление и изменение данных";
                button6.Enabled = false;
            }
            if (tvol == "3")
            {
                priv = "Чтение";
                изменитьДанныеToolStripMenuItem.Enabled = false;
                button1.Enabled = false;
                button6.Enabled = false;
            }
            comboBox1.SelectedIndex = 0;
        }


        //Выбор таблицы через меню
        private void делаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            string sql = Dela;
            Using(sql);
        }
        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 1;
            string sql = Sotr;
            Using(sql);
        }

        private void пострадавшиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 2;
            string sql = Postr;
            Using(sql);
        }

        private void преступленияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 3;
            string sql = Podozr;
            Using(sql);
        }

        private void видыПреступленийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 4;
            string sql = Prest;
            Using(sql);
        }

        private void званияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 5;
            string sql = Zvan;
            Using(sql);
        }
        //Выбор таблиц через ячейки
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                textBox1.Text = null;
                this.Text = "Учет дел";
                label1.Text = "Поиск дела по коду";
                button1.Text = "Добавить дело";
                button6.Text = "Удалить дело";
                string sql = Dela;
                Using(sql);
            }
            if (comboBox1.SelectedIndex==1)
            {
                textBox1.Text = null;
                this.Text = "Список сотрудников";
                label1.Text = "Поиск сотрудника по фамилии, имени или отчеству";
                button1.Text = "Добавить сотрудника";
                button6.Text = "Удалить сотрудника";
                string sql = Sotr;
                Using(sql);
            }
            if (comboBox1.SelectedIndex == 2)
            {
                textBox1.Text = null;
                this.Text = "Список пострадавших";
                label1.Text = "Поиск пострадавшего по фамилии, имени или отчеству";
                button1.Text = "Добавить пострадавшего";
                button6.Text = "Удалить пострадавшего";
                string sql = Postr;
                Using(sql);
            }
            if (comboBox1.SelectedIndex == 3)
            {
                textBox1.Text = null;
                this.Text = "Список подозреваемых";
                label1.Text = "Поиск подозреваемого по фамилии, имени или отчеству";
                button1.Text = "Добавить подозреваемого";
                button6.Text = "Удалить подозреваемого";
                string sql = Podozr;
                Using(sql);
            }
            if (comboBox1.SelectedIndex == 4)
            {
                textBox1.Text = null;
                this.Text = "Список преступлений";
                label1.Text = "Поиск преступлений по статьям";
                button1.Text = "Добавить вид преступления";
                button6.Text = "Удалить вид преступления";
                string sql = Prest;
                Using(sql);
            }
            if (comboBox1.SelectedIndex == 5)
            {
                textBox1.Text = null;
                this.Text = "Список званий";
                label1.Text = "Поиск звания по названию";
                button1.Text = "Добавить звание";
                button6.Text = "Удалить звание";
                string sql = Zvan;
                Using(sql);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                string sql = Dela;
                Using(sql);
            }
            if (comboBox1.SelectedIndex == 1)
            {
                string sql = Sotr;
                Using(sql);
            }
            if (comboBox1.SelectedIndex == 2)
            {
                string sql = Postr;
                Using(sql);
            }
            if (comboBox1.SelectedIndex == 3)
            {
                string sql = Podozr;
                Using(sql);
            }
            if (comboBox1.SelectedIndex == 4)
            {
                string sql = Prest;
                Using(sql);
            }
            if (comboBox1.SelectedIndex == 5)
            {
                string sql = Zvan;
                Using(sql);
            }
        }
        //Поиск данных
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                string sql = Dela + " WHERE Код_дела LIKE N'" + textBox1.Text + "%'";
                Using(sql);
            }
            if (comboBox1.SelectedIndex == 1)
            {
                string sql = Sotr + " WHERE Фамилия LIKE N'" + textBox1.Text + "%' or Имя LIKE N'" + textBox1.Text + "%' or Отчество LIKE N'" + textBox1.Text + "%'";
                Using(sql);
            }
            if (comboBox1.SelectedIndex == 2)
            {
                string sql = Postr + " WHERE Фамилия LIKE N'" + textBox1.Text + "%' or Имя LIKE N'" + textBox1.Text + "%' or Отчество LIKE N'" + textBox1.Text + "%'";
                Using(sql);
            }
            if (comboBox1.SelectedIndex == 3)
            {
                string sql = Podozr + " WHERE Фамилия LIKE N'" + textBox1.Text + "%' or Имя LIKE N'" + textBox1.Text + "%' or Отчество LIKE N'" + textBox1.Text + "%'";
                Using(sql);
            }
            if (comboBox1.SelectedIndex == 4)
            {
                string sql = Prest + " WHERE Наименование LIKE N'" + textBox1.Text + "%'";
                Using(sql);
            }
            if (comboBox1.SelectedIndex == 5)
            {
                string sql = Zvan + " WHERE Наименование LIKE N'" + textBox1.Text + "%'";
                Using(sql);
            }
        }

        //Удаление данных
        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                string s = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string sql = "DELETE FROM Дела WHERE id = " + s;
                using (SqlConnection connection = new SqlConnection(connecionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables["Дела"];
                }
                sql = Dela;
                Using(sql);
            }
            if (comboBox1.SelectedIndex == 1)
            {
                string s = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string sql = "DELETE FROM Сотрудники WHERE id = " + s;
                using (SqlConnection connection = new SqlConnection(connecionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables["Сотрудники"];
                }
                sql = Sotr;
                Using(sql);
            }
            if (comboBox1.SelectedIndex == 2)
            {
                string s = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string sql = "DELETE FROM Пострадавшие WHERE id =" + s;
                using (SqlConnection connection = new SqlConnection(connecionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables["Пострадавшие"];
                }
                sql = Postr;
                Using(sql);
            }
            if (comboBox1.SelectedIndex == 3)
            {
                string s = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string sql = "DELETE FROM Подозреваемые WHERE id = " + s;
                using (SqlConnection connection = new SqlConnection(connecionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables["Преступники"];
                }
                sql = Podozr;
                Using(sql);
            }
            if (comboBox1.SelectedIndex == 4)
            {
                string s = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string sql = "DELETE FROM Виды_преступлений WHERE id = " + s;
                using (SqlConnection connection = new SqlConnection(connecionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables["Виды_преступлений"];
                }
                sql = Prest;
                Using(sql);
            }
            if (comboBox1.SelectedIndex == 5)
            {
                string s = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string sql = "DELETE FROM Звания WHERE id = " + s;
                using (SqlConnection connection = new SqlConnection(connecionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables["Звания"];
                }
                sql = Zvan;
                Using(sql);
            }
            MessageBox.Show("Запись удалена", "Удаление");
        }
        //Момент выбора таблиц: кнопки
        private void button2_Click_1(object sender, EventArgs e)
        {
            int x = comboBox1.SelectedIndex;
            if (x == 0)
            {
                x=5;
            }
            else
            {
                x -= 1;
            }
            comboBox1.SelectedIndex = x;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            int x = comboBox1.SelectedIndex;
            if (x == 5)
            {
                x=0;
            }
            else
            {
                x += 1;
            }
            comboBox1.SelectedIndex = x;
        }

        //Для меню справки
        private void справкаToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Учет дел отделения полиции \n\nВерсия: 2.2 \n\nСДЕЛАЙ РЕДАКТИРОВАНИЕ", "Справка");
        }

        private void updateLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("30.04.24 \nПроцесс заполнения отчета 2 часть", "Log");
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Context.MainForm = new Authorization();
            this.Close();
            Program.Context.MainForm.Show();
        }

        //Значение кнопки добавить
        private void button1_Click(object sender, EventArgs e)
        {
            string s = null;
            if (comboBox1.SelectedIndex == 0)
            {
                Form7 f7 = new Form7(s);
                f7.Show();
            }
            if (comboBox1.SelectedIndex == 1)
            {
                Form2 f2 = new Form2(s);
                f2.Show();
            }
            if (comboBox1.SelectedIndex == 2)
            {
                Form3 f3 = new Form3(s);
                f3.Show();
            }
            if (comboBox1.SelectedIndex == 3)
            {
                Form4 f4 = new Form4(s);
                f4.Show();
            }
            if (comboBox1.SelectedIndex == 4)
            {
                Form5 f5 = new Form5(s);
                f5.Show();
            }
            if (comboBox1.SelectedIndex == 5)
            {
                Form6 f6 = new Form6(s);
                f6.Show();
            }
        }

        //Закрыть форму
        private void button4_Click(object sender, EventArgs e)
        {
            Program.Context.MainForm = new Authorization();
            this.Close();
            Program.Context.MainForm.Show();
        }

        private void изменитьДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = null;
            s = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            if (comboBox1.SelectedIndex == 0)
            {
                Form7 f7 = new Form7(s);
                f7.Show();
            }
            if (comboBox1.SelectedIndex == 1)
            {
                Form2 f2 = new Form2(s);
                f2.Show();
            }
            if (comboBox1.SelectedIndex == 2)
            {
                Form3 f3 = new Form3(s);
                f3.Show();
            }
            if (comboBox1.SelectedIndex == 3)
            {
                Form4 f4 = new Form4(s);
                f4.Show();
            }
            if (comboBox1.SelectedIndex == 4)
            {
                Form5 f5 = new Form5(s);
                f5.Show();
            }
            if (comboBox1.SelectedIndex == 5)
            {
                Form6 f6 = new Form6(s);
                f6.Show();
            }
        }

        private void моиДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(tfio + "\nПрава доступа: "+priv, "Сотрудник");
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                