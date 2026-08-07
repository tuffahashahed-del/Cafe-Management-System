using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.IO;
namespace final
{
    public partial class inventory : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\USER\Desktop\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb");
    
        public inventory()
        {
            InitializeComponent();
            displaycat();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void displaycat()

        {

            using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\user\Desktop\final_shahdtuffaha\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb"))
            {
                conn.Open();
                string selectcat = "SELECT category_id, category_name FROM category WHERE is_avalible=True";
                using (OleDbCommand cmd = new OleDbCommand(selectcat, conn))
                {
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());

                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "category_name"; 
                    comboBox1.ValueMember = "category_id";     
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == "" || textBox3.Text == "" || comboBox1.SelectedIndex == -1 || textBox1.Text == "" || textBox4.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("Empty fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\USER\Desktop\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb"))
                {
                    conn.Open();
                    string basedirec = AppDomain.CurrentDomain.BaseDirectory;

                    string insertdata = "INSERT INTO Products (product_name, price, category_id, stock, is_avalible, date_insert) " + "VALUES (?, ?, ?, ?, ?, ?)";
                    ///string relatuvepath=Path.Combine(
                    using (OleDbCommand cmd = new OleDbCommand(insertdata, conn))
                    {
                        
                        cmd.Parameters.AddWithValue("?", textBox3.Text.Trim());
                        cmd.Parameters.AddWithValue("?", decimal.Parse(textBox4.Text.Trim()));
                        cmd.Parameters.AddWithValue("?",int.Parse(comboBox1.SelectedValue.ToString()));
                        cmd.Parameters.AddWithValue("?", int.Parse(textBox1.Text.Trim()));
                        cmd.Parameters.AddWithValue("?", bool.Parse(comboBox2.Text));
                        DateTime today = DateTime.Now.Date;
                        cmd.Parameters.AddWithValue("?", today);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("added successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            displayProducts();
                            
                            displaycat();

                        }
                        else
                        {
                            MessageBox.Show("not added", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }







                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            

        }

        private void inventory_Load(object sender, EventArgs e)
        {
            displayProducts();
            this.WindowState = FormWindowState.Maximized;



        }
        private void clearfilds()
        {
            textBox1.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox7.Clear();
            comboBox2.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            clearfilds();
        }
        public void displayProducts()
        {
            using (OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\USER\Desktop\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb")) 
            {
                con.Open();
                string query = "SELECT * FROM products";
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt; 
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox7.Text)) 
            {
                MessageBox.Show("Please enter product ID to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this product?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\USER\Desktop\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb"))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM products WHERE ID=?";
                    using (OleDbCommand cmd = new OleDbCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("?", int.Parse(textBox7.Text.Trim()));
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Deleted successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clearfilds();
                            displayProducts(); // تحديث DataGridView مباشرة
                        }
                        else
                        {
                            MessageBox.Show("Product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox7.Text) || textBox3.Text == "" || comboBox1.SelectedIndex == -1 || textBox1.Text == "" || textBox4.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("Please fill all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\USER\Desktop\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb"))
            {
                conn.Open();
                string updateQuery = @"UPDATE products 
                       SET product_name = ?, price = ?, category_id = ?, stock = ?, is_avalible = ?, date_insert = ?
                       WHERE ID = ?";
                using (OleDbCommand cmd = new OleDbCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("?", textBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("?", decimal.Parse(textBox4.Text.Trim()));
                    cmd.Parameters.AddWithValue("?", int.Parse(comboBox1.SelectedValue.ToString()));
                    cmd.Parameters.AddWithValue("?", int.Parse(textBox1.Text.Trim()));
                    cmd.Parameters.AddWithValue("?", bool.Parse(comboBox2.Text));
                    cmd.Parameters.AddWithValue("?", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("?", int.Parse(textBox7.Text.Trim()));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearfilds();
                        displayProducts(); // تحديث DataGridView مباشرة
                    }
                    else
                    {
                        MessageBox.Show("Product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainform mf = new mainform();
            this.Hide();
            mf.Show();
        }

    }
}
    
    