using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace final
{
    public partial class customer : Form
    {
        public customer()
        {
            InitializeComponent();
        }

        private void customer_Load(object sender, EventArgs e)
        {
            this.customersTableAdapter.Fill(this.dBcafeeDataSet.Customers);
            this.WindowState = FormWindowState.Maximized;


        }

        private void DisplayCustomers()
        {
            using (OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\user\Desktop\final_shahdtuffaha\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb"))
            {
                con.Open();
                string query = "SELECT customenr_id, customer_name, phone, email, date_insert FROM Customers";
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void button1_Click(object sender, EventArgs e) // Add Customer
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Fill all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\user\Desktop\final_shahdtuffaha\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb"))
            {
                con.Open();
                string query = "INSERT INTO Customers (customer_name, phone, email, date_insert) VALUES (@name, @phone, @email, @date)";
                using (OleDbCommand cmd = new OleDbCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@name", textBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@phone", int.Parse(textBox2.Text.Trim()));
                    cmd.Parameters.AddWithValue("@email", textBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.Date);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer added successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    DisplayCustomers();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) // Update Customer
        {
            if (dataGridView1.CurrentRow == null) return;
            dataGridView1.Columns[0].Name = "customenr_id"; 
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["customenr_id"].Value);

            using (OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\user\Desktop\final_shahdtuffaha\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb"))
            {
                con.Open();
                string query = "UPDATE Customers SET customer_name=@name, phone=@phone, email=@email WHERE customenr_id=@id";
                using (OleDbCommand cmd = new OleDbCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@name", textBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@phone", textBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", textBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer updated successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisplayCustomers();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e) // Delete Customer
        {
            if (dataGridView1.CurrentRow == null) return;

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value); 
            DialogResult result = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\user\Desktop\final_shahdtuffaha\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb"))
                {
                    con.Open();
                    string query = "DELETE FROM Customers WHERE customenr_id=@id";
                    using (OleDbCommand cmd = new OleDbCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Customer deleted successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        DisplayCustomers();
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) // Clear Fields
        {
            ClearFields();
        }

        private void dgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells["customer_name"].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells["phone"].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
            }
        }

        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            mainform mf=new mainform();
            this.Hide();
            mf.Show();

        }
    }
}
