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
    public partial class dashboard : Form
    {
        OleDbConnection connect = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\user\Desktop\final_shahdtuffaha\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb");

        public dashboard()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dashboard_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;


            
           
            DisplayTotalCustomers();     
            DisplayTotalOrders();        
            DisplayTotalProfit();        
            DisplayTodaysProfit();       
            DisplayOrderDetails();


        }
        public void DisplayOrderDetails()
        {
            using (OleDbConnection connect = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\user\Desktop\final_shahdtuffaha\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb"))
            {
                connect.Open();
                string query = "SELECT * FROM order_details";
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connect))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt; 
                }
            }
        }
        public void DisplayTotalCustomers()
        {
            using (OleDbConnection connect = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\user\Desktop\final_shahdtuffaha\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb"))
            {
                connect.Open();

                string query = "SELECT COUNT(*) FROM Customers"; // يحسب عدد الصفوف في الجدول

                using (OleDbCommand cmd = new OleDbCommand(query, connect))
                {
                    object result = cmd.ExecuteScalar(); // يرجع القيمة المفردة (عدد الزبائن)
                    label6.Text = result.ToString();
                    
                }
            }
        }

        public void DisplayTotalOrders()
        {
            using (OleDbConnection connect = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\user\Desktop\final_shahdtuffaha\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb"))
            {
                connect.Open();

                
                string query = "SELECT COUNT(order_id) FROM Orders";

                using (OleDbCommand cmd = new OleDbCommand(query, connect))
                {
                    object result = cmd.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                        label5.Text = "0";
                    else
                        label5.Text = result.ToString();
                    
                }
            }
        }
        public void DisplayTotalProfit()
        {
            using (OleDbConnection connect = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\user\Desktop\final_shahdtuffaha\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb"))
            {
                connect.Open();

                string query = "SELECT SUM(total_price) FROM order_details";

                using (OleDbCommand cmd = new OleDbCommand(query, connect))
                {
                    object result = cmd.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                        label8.Text = "0";
                    else
                        label8.Text = result.ToString();
                }
            }
        }
        public void DisplayTodaysProfit()
        {
            using (OleDbConnection connect = new OleDbConnection(
                @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\user\Desktop\final_shahdtuffaha\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb"))
            {
                connect.Open();

                DateTime today = DateTime.Today.Date;

                string query = @"
                    SELECT SUM(order_details.total_price)
                    FROM order_details
                    INNER JOIN Orders ON order_details.order_id = Orders.order_id
                    WHERE Orders.order_date = ? ";

                using (OleDbCommand cmd = new OleDbCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("?", today);
                    

                    object result = cmd.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                        label7.Text = "0";
                    else
                        label7.Text = result.ToString();
                }
            }
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
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


        
    