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

namespace final
{
    

    public partial class shop : Form
    {
        public int ord_id;
        public int prod_id;
        public double unit_price;
        public double total=0;

        public shop()
        {
            InitializeComponent();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            mainform mf = new mainform();
            this.Hide();
            mf.Show();
        }

        private void shop_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            comboBoxCategory.Enabled = false;
            comboBoxProduct.Enabled = false;
            load_category();
            load_product();
           
        }
        public void load_category(){
            using (OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\USER\OneDrive\Desktop\lastfinal_shahdtuffaha\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb"))
            {
                con.Open();

                using (OleDbCommand cmd = new OleDbCommand("SELECT category_id, category_name FROM category WHERE is_avalible=? ORDER BY category_name", con))
                {
                    cmd.Parameters.AddWithValue("?", true);
                    DataTable dt = new DataTable();
                    
                    using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    comboBoxCategory.DataSource = dt;
                    comboBoxCategory.DisplayMember = "category_name"; // اللي يبين للمستخدم
                    comboBoxCategory.ValueMember = "category_id";     // القيمة الحقيقية (ID)
                    comboBoxCategory.SelectedIndex = -1;              // بدون اختيار أول ما يفتح
                }
            }
        }
        public void load_product() {
            using (OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\USER\OneDrive\Desktop\lastfinal_shahdtuffaha\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb"))
            {
                con.Open();

                using (OleDbCommand cmd = new OleDbCommand("SELECT product_id , product_name FROM products INNER JOIN category ON products.category_id = category.category_id WHERE products.is_avalible=? AND category.is_avalible=? ORDER BY products.product_name ", con))
                {
                    cmd.Parameters.AddWithValue("?", true);
                    cmd.Parameters.AddWithValue("?", true);
                    DataTable dt = new DataTable();

                    using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    comboBoxProduct.DataSource = dt;
                    comboBoxProduct.DisplayMember = "product_name"; 
                    comboBoxProduct.ValueMember = "product_id";     
                    comboBoxProduct.SelectedIndex = -1;              
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\USER\OneDrive\Desktop\lastfinal_shahdtuffaha\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb"))
            {
                con.Open();

                using (OleDbCommand cmd = new OleDbCommand("SELECT product_id , product_name FROM products INNER JOIN category ON products.category_id = category.category_id WHERE products.is_avalible=? AND category.is_avalible=? AND category.category_name=? ORDER BY products.product_name ", con))
                {
                    cmd.Parameters.AddWithValue("?", true);
                    cmd.Parameters.AddWithValue("?", true);
                    cmd.Parameters.AddWithValue("?", comboBoxCategory.Text);
                    DataTable dt = new DataTable();

                    using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    comboBoxProduct.DataSource = dt;
                    comboBoxProduct.DisplayMember = "product_name";
                    comboBoxProduct.ValueMember = "product_id";
                    comboBoxProduct.SelectedIndex = -1;
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            comboBoxCategory.Enabled = true;
            comboBoxProduct.Enabled = true;
            using (OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\USER\OneDrive\Desktop\lastfinal_shahdtuffaha\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb"))
            {
                
                
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand("INSERT INTO Orders ( order_date, total_amount,paid_amount,change_amount) VALUES (@date,@total,@paid,@change)", con))
                    {
                        DateTime today = DateTime.Today.Date;
                        cmd.Parameters.AddWithValue("@date", today);
                        cmd.Parameters.AddWithValue("@total", 0);
                        cmd.Parameters.AddWithValue("@paid", 0);
                        cmd.Parameters.AddWithValue("@change", 0);
                        cmd.ExecuteNonQuery();


                    }
                    using (OleDbCommand cmd = new OleDbCommand("SELECT TOP 1 [order_id] FROM [Orders] ORDER BY [order_id] DESC", con))
                    {
                        object result = cmd.ExecuteScalar();
                        ord_id = (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
                    }
                    
                    
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBoxCategory.Text == "" || comboBoxCategory.SelectedIndex == -1 || comboBoxProduct.Text == "" || comboBoxProduct.SelectedIndex == -1 || textBox2.Text == "")
            {
                MessageBox.Show("Please fill all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else {
                using (OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\USER\OneDrive\Desktop\lastfinal_shahdtuffaha\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb"))
                {


                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand("SELECT product_id from Products WHERE product_name=?", con))
                    {
                        cmd.Parameters.AddWithValue("?",comboBoxProduct.Text);
                        object result = cmd.ExecuteScalar();
                        prod_id = (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
                    }
                    using (OleDbCommand cmd = new OleDbCommand("SELECT price from Products WHERE product_name=?", con))
                    {
                        cmd.Parameters.AddWithValue("?", comboBoxProduct.Text);
                        object result = cmd.ExecuteScalar();
                        unit_price = (result == null || result == DBNull.Value) ? 0 : Convert.ToDouble(result);
                    }
                    using (OleDbCommand cmd = new OleDbCommand("INSERT INTO order_details ( order_id, product_id,quntity,price,total_price) VALUES (@order_id,@product_id,@quntity,@price,@total_price)", con))
                    {
                        DateTime today = DateTime.Today.Date;
                        cmd.Parameters.AddWithValue("@order_id", ord_id);
                        cmd.Parameters.AddWithValue("@product_id", prod_id);
                        cmd.Parameters.AddWithValue("@quntity", int.Parse(textBox2.Text));
                        cmd.Parameters.AddWithValue("@price", unit_price);
                        cmd.Parameters.AddWithValue("@total_price", unit_price*int.Parse(textBox2.Text));
                        cmd.ExecuteNonQuery();


                    }
                    total += (unit_price * int.Parse(textBox2.Text));
                    label5.Text = total.ToString();
                    label6.Text = total.ToString();
                    comboBoxCategory.Text = "";
                    comboBoxProduct.Text = "";
                    textBox2.Text = "";
                    using (OleDbCommand cmd = new OleDbCommand("UPDATE Orders SET total_amount=?, paid_amount=? WHERE order_id=?", con))
                    {
                        cmd.Parameters.Add("?", total);
                        cmd.Parameters.Add("?",total);  
                        cmd.Parameters.Add("?", ord_id);

                        cmd.ExecuteNonQuery();
                    }
                    using (OleDbCommand cmd = new OleDbCommand("SELECT   p.product_name, od.quntity, od.price, od.total_price FROM order_details AS od INNER JOIN Products AS p ON od.product_id = p.product_id WHERE od.order_id = ? ORDER BY od.ID DESC", con)) {
                        cmd.Parameters.AddWithValue("?",ord_id);
                        DataTable dt = new DataTable();
                        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                        dataGridView1.DataSource = dt;
                    }


                }
                
                
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (OleDbConnection con = new OleDbConnection(
                @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\USER\OneDrive\Desktop\lastfinal_shahdtuffaha\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb"))
            {
                con.Open();

                
                using (OleDbCommand cmd = new OleDbCommand(
                    "DELETE FROM order_details WHERE ID = " +
                    "(SELECT TOP 1 ID FROM order_details WHERE order_id = ? ORDER BY ID DESC)", con))
                {
                    cmd.Parameters.AddWithValue("?", ord_id); 
                    cmd.ExecuteNonQuery();
                }

                
                double newTotal = 0;
                using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT SUM(total_price) FROM order_details WHERE order_id = ?", con))
                {
                    cmd.Parameters.AddWithValue("?", ord_id);
                    object r = cmd.ExecuteScalar();
                    newTotal = (r == null || r == DBNull.Value) ? 0 : Convert.ToDouble(r);
                }

                total = newTotal;
                label5.Text = total.ToString("0.00");
                label6.Text = total.ToString("0.00");

                
                using (OleDbCommand cmd = new OleDbCommand(
                    "UPDATE Orders SET total_amount=?, paid_amount=? WHERE order_id=?", con))
                {
                    cmd.Parameters.AddWithValue("?", total);
                    cmd.Parameters.AddWithValue("?", total);
                    cmd.Parameters.AddWithValue("?", ord_id);
                    cmd.ExecuteNonQuery();
                }

                
                using (OleDbCommand cmd = new OleDbCommand(
                    "SELECT p.product_name, od.quntity, od.price, od.total_price " +
                    "FROM order_details AS od " +
                    "INNER JOIN Products AS p ON od.product_id = p.product_id " +
                    "WHERE od.order_id = ? ORDER BY od.ID DESC", con))
                {
                    cmd.Parameters.AddWithValue("?", ord_id);
                    DataTable dt = new DataTable();
                    using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    dataGridView1.DataSource = dt;
                }
            }
        }


        private void button7_Click(object sender, EventArgs e)
        {
            comboBoxCategory.Enabled=false;
            comboBoxProduct.Enabled=false;
            label5.Text="0.00";
            label6.Text="0.00";
            total = 0;
            comboBoxProduct.Text = "";
            comboBoxCategory.Text = "";
            textBox2.Text = "";
            dataGridView1.DataSource = null;

            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
 


        
    }
}