using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Flowerpot
{
    public partial class OneShotSQL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectString = "Host=ec2-54-216-185-51.eu-west-1.compute.amazonaws.com;Port=5432;User ID=ncfpnnvvzuycpl;Password=18ba18d3620bb3a090933851a3ee2110a6df7eb257f31d4dd65aee8f93ae149e;Database=d8chtgod94i64r;sslmode=Require;TrustServerCertificate=True;";
            using (NpgsqlConnection con = new NpgsqlConnection(connectString))
            {
                con.Open();
                NpgsqlCommand db = new NpgsqlCommand();
                db.Connection = con;
                
                db.CommandText = "drop table if exists tabulka;";
                db.ExecuteNonQuery(); 
                
                db.CommandText = "CREATE TABLE tabulka(id int PRIMARY KEY,moisture int, luminance int);";
                db.ExecuteNonQuery();

                db.CommandText = "insert into tabulka values('1','6','6');";
                db.ExecuteNonQuery();

                TextBox1.Text = new NpgsqlCommand("SELECT moisture FROM TABULKA WHERE id=(SELECT max(id) FROM tabulka);", con).ExecuteScalar().ToString();


                con.Close();

            }
        }
    }
}