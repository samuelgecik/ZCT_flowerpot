using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

namespace Flowerpot
{
    public partial class flowerpot : System.Web.UI.Page
    {
        private static bool water = false;
        private static int light = 2;
        private static EventHandler buttonEvent;
        private string moisture, luminance;
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectString = "Host=ec2-54-216-185-51.eu-west-1.compute.amazonaws.com;Port=5432;User ID=ncfpnnvvzuycpl;Password=18ba18d3620bb3a090933851a3ee2110a6df7eb257f31d4dd65aee8f93ae149e;Database=d8chtgod94i64r;sslmode=Require;TrustServerCertificate=True;";
            using (NpgsqlConnection con = new NpgsqlConnection(connectString))
            {
                con.Open();
                NpgsqlCommand db = new NpgsqlCommand();
                db.Connection = con;
               
                luminance = new NpgsqlCommand("SELECT luminance FROM TABULKA WHERE id=(SELECT max(id) FROM tabulka);", con).ExecuteScalar().ToString();
                moisture = new NpgsqlCommand("SELECT moisture FROM TABULKA WHERE id=(SELECT max(id) FROM tabulka);", con).ExecuteScalar().ToString();

                con.Close();
                Label1.Text = "Moisture: " + moisture + '%';
                Label2.Text = "Luminosity: " + luminance + "lux";
            }
           
            
            Button2.Click += buttonEvent;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            water = !water;
            displaymessage.Visible = true;
            Toggle();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            light = 1;
            Toggle();
            buttonEvent = new EventHandler(this.Button2_Click2);
            ((Button)sender).Text = "Light on";

        }
        protected void Button2_Click2(object sender, EventArgs e)
        {
            light = 0;
            Toggle();
            buttonEvent = new EventHandler(this.Button2_Click3);
            ((Button)sender).Text = "Light off";
        }
        protected void Button2_Click3(object sender, EventArgs e)
        {
            light = 2;
            Toggle();
            buttonEvent = new EventHandler(this.Button2_Click);
            ((Button)sender).Text = "Light Auto";
        }


        protected void Toggle()
        {
            StreamWriter writer = new StreamWriter("D:/home/site/wwwroot/output.txt");

            if (water)
            {
                writer.WriteLine("1");
            }
            else
            {
                writer.WriteLine("0");
            }

            if (light == 0)
            {
                writer.WriteLine("0");
            }
            else if (light == 1)
            {
                writer.WriteLine("1");
            }
            else
            {
                writer.WriteLine("2");
            }

            writer.Close();
        }
    }
}