using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Flowerpot
{
    public partial class Receiver : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             string moisture = Request.QueryString["moist"];
             string luminance = Request.QueryString["lum"];
            int max_id;
            string connectString = "Host=ec2-54-216-185-51.eu-west-1.compute.amazonaws.com;Port=5432;User ID=ncfpnnvvzuycpl;Password=18ba18d3620bb3a090933851a3ee2110a6df7eb257f31d4dd65aee8f93ae149e;Database=d8chtgod94i64r;sslmode=Require;TrustServerCertificate=True;";
            using (NpgsqlConnection con = new NpgsqlConnection(connectString))
            {
                con.Open();
                NpgsqlCommand db = new NpgsqlCommand();
                db.Connection = con;
                /*
                db.CommandText = "drop table if exists tabulka;";
                db.ExecuteNonQuery(); 
                
                db.CommandText = "create table tabulka(moisture int, luminance int);";
                db.ExecuteNonQuery();

                db.CommandText = "insert into tabulka values('6','6');";
                db.ExecuteNonQuery();
                */
                max_id = int.Parse(new NpgsqlCommand("SELECT MAX(ID) FROM TABULKA;", con).ExecuteScalar().ToString());
                max_id++;
                db.CommandText = "insert into TABULKA values("+max_id+", "+moisture+", "+luminance+");";
                db.ExecuteNonQuery();

                con.Close();
                
            }
            
        }
    }
}