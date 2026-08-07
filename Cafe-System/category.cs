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
    public partial class category : Form
    {
        

         
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\user\Desktop\final_shahdtuffaha\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb");
        public category()
        {
            InitializeComponent();
            displayCategories();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {

                {
                    con.Open();
                    string selectCategory = " SELECT * FROM category WHERE category_name =@cat";
                    using (OleDbCommand checkCat = new OleDbCommand(selectCategory, con))
                    {

                        checkCat.Parameters.AddWithValue("@cat", textBox1.Text.Trim());
                        OleDbDataAdapter adapter = new OleDbDataAdapter(checkCat);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            MessageBox.Show(textBox1.Text.Trim() + " already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        else
                        {
                            string insertdata = "INSERT INTO  category (category_name,is_avalible,date_insert)VALUES(@cat,@status,@date)";
                            using (OleDbCommand cmd = new OleDbCommand(insertdata, con))
                            {
                                cmd.Parameters.AddWithValue("@cat", textBox1.Text.Trim());
                                cmd.Parameters.AddWithValue("@status", bool.Parse(comboBox1.Text));
                                DateTime today = DateTime.Now.Date;

                                cmd.Parameters.AddWithValue("@date", today);
                                MessageBox.Show(today.ToString());
                                cmd.ExecuteNonQuery();
                                displayCategories();
                                MessageBox.Show("Added sucessfully", "Informatin Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                clearFields();
                            }
                            
                        }
                    }
                    con.Close();
                }
            }
        }




        private void button5_Click(object sender, EventArgs e)
        {
            mainform f4 = new mainform();
            f4.Show();
            this.Hide();

        }
        private void clearFields()
        {
            textBox1.Clear();
            comboBox1.SelectedIndex = -1;
        }
        

        public void displayCategories()
        {

            {

                string query = "SELECT * FROM category";
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, con))
                {

                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
                con.Close();
            }

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {



        }

        private void category_Load(object sender, EventArgs e)
        {
            displayCategories();
            this.WindowState = FormWindowState.Maximized;


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter category name to delete", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this category?",
                                                      "Confirm Delete",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (OleDbConnection connect = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\user\Desktop\final_shahdtuffaha\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb"))
                    {
                        connect.Open();

                        string deletedata = "DELETE FROM category WHERE category_name=@cat";
                        using (OleDbCommand cmd = new OleDbCommand(deletedata, connect))
                        {
                            cmd.Parameters.AddWithValue("@cat", textBox1.Text.Trim());

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Deleted successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                clearFields();
                                displayCategories();
                            }
                            else
                            {
                                MessageBox.Show("Category not found", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (OleDbConnection connect = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\user\Desktop\final_shahdtuffaha\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb"))
                {
                    connect.Open();
                    string updatedata = "UPDATE category SET is_avalible=@status, date_insert=@date WHERE category_name=@cat";
                    using (OleDbCommand cmd = new OleDbCommand(updatedata, connect))
                    {
                        cmd.Parameters.AddWithValue("@status", bool.Parse(comboBox1.Text));
                        DateTime today = DateTime.Now.Date;
                        cmd.Parameters.AddWithValue("@date", today);
                        cmd.Parameters.AddWithValue("@cat", textBox1.Text.Trim());

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Updated successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clearFields();
                            displayCategories();
                        }
                        else
                        {
                            MessageBox.Show("Category not found", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}

                        
          

   
                        
                
    
             
        
    

      
    

