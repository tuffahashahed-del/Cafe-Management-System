using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
namespace final
{
    class Cafe
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\USER\Desktop\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb");

        public bool cansignin(string strusername, string strpassword)
        {
            var dt = new DataTable();

            string query = @"SELECT * from Users WHERE user_name=@username_p AND [password] =@password_p";

            using (OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\USER\Desktop\final_shahdtuffaha\final_shahd\final\final\DBcafee.mdb"))
            {
                using (OleDbCommand com = new OleDbCommand(query, con))
                {
                    com.Parameters.AddWithValue("@username_p", strusername);
                    com.Parameters.AddWithValue("@password_p", strpassword);
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(com))
                    {
                        adapter.Fill(dt);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }







        }

    }
}
