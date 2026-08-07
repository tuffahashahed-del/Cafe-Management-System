using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace final
{
    public partial class Form3 : Form
    {
        private static string myconn =@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\user\Desktop\final_shahdtuffaha\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb";
        public Form3()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            mainform f4 = new mainform();
            f4.Show();
            this.Hide();

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;


        }

        private void button1_Click(object sender, EventArgs e)
        {
       
            string username = textBox1.Text;
            string password = textBox2.Text;
            string confirm = textBox3.Text;
           

            if (username == "" || password == "")
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Passwords do not match");
                return;
            }

            using (OleDbConnection con = new OleDbConnection(myconn))
            {
                con.Open();
                string query = "INSERT INTO users (username, [password]) " + "VALUES (@u, @p)";
                using (OleDbCommand cmd = new OleDbCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", password);
                    

                    cmd.ExecuteNonQuery();

                }
                MessageBox.Show("Sign up successful ✅");
                con.Close();
            }
        }

        
    }
}
