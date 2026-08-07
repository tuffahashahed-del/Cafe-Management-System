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
    public partial class mainform : Form
    {
        public mainform()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dashboard d = new dashboard();
            d.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            shop sh =new shop();
            sh.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            category c = new category();
            c.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            customer cus = new customer();
            cus.Show();
            this.Hide();
        }


        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            inventory inv = new inventory();
            inv.Show();
            this.Hide();
        }

        private void mainform_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;


        }
    }
}
