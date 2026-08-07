using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace final
{
    public partial class signin : Form
    {
        Cafe user = new Cafe();
        public signin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();


        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please Enter Username");
                textBox1.Focus();
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please Enter Your Password");
                textBox2.Focus();
                return;
            }
            bool result = user.cansignin(textBox1.Text, textBox2.Text);

            if (result == false)
            {
                MessageBox.Show("Wrong Username or Password");
                return;
            }
            mainform mf =new mainform();
            this.Hide();
            mf.Show();
                
                

            

        }

        private void signin_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;



        }
        
    }
}
