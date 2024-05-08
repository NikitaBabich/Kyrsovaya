using Microsoft.AnalysisServices;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.ReportingServices.Diagnostics.Internal;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Kyrca4
{
    public partial class Authorization : Form
    {
        string connecionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PoliceDB;Integrated Security=True";
        private string Authorize(string login, string password)
        {
            SqlConnection connection = new SqlConnection(connecionString);
            login = textBox1.Text;
            password = textBox2.Text;
            string sql = "SELECT [User-Role].RoleID FROM [User-Role] JOIN Users on Users.id= [User-Role].UserID where Users.Login = '" + login + "' and Users.Password = '" + password + "'";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                object result = command.ExecuteScalar();
                connection.Close();
                return result != null ? result.ToString() : null;
            }
        }
        public Authorization()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            if (password !="") 
            {
                string vol = Authorize(login, password);
                if (vol != null)
                {
                    using (SqlConnection connection = new SqlConnection(connecionString))
                    {
                        SqlCommand command = new SqlCommand("select Фамилия, Имя, Отчество from Users WHERE login = '" + login+"'", connection);
                        connection.Open();
                        SqlDataReader read = command.ExecuteReader();
                        while (read.Read())
                        {
                            string f = (read["Фамилия"].ToString());
                            string i = (read["Имя"].ToString());
                            string o = (read["Отчество"].ToString());
                            string fio = f+" "+i+" "+o;
                            MessageBox.Show("Вы вошли как:\n" + fio, "Успешно");
                            textBox2.Clear();
                            Program.Context.MainForm = new Form1(vol,fio);
                            this.Close();
                            Program.Context.MainForm.Show();
                        }
                        read.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Сотрудник не найден\nПроверьте логин и пароль", "Ошибка");
                    textBox2.Clear();
                }
            }
            else
            {
                MessageBox.Show("Введите пароль", "Ошибка");
            }
        }
    }
}
