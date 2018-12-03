using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data.OleDb;

namespace SMMC
{
    public partial class _Default : Page
    {
        SqlConnection sqlConnection;
        Essentials essentials;
        protected void Page_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            essentials = new Essentials();
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            string query = "SELECT COUNT(DISTINCT PersonID) AS TotalStudents FROM Student";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            sqlConnection.Open();
            SqlDataReader sdr = cm.ExecuteReader();
            sdr.Read();

            Students.Text = "There are " + sdr["TotalStudents"].ToString() + " students enrolled by SMMC";
            sqlConnection.Close();

            query = "SELECT COUNT(DISTINCT PersonID) AS TotalTutors FROM Tutors";
            cm = new SqlCommand(query, sqlConnection);
            sqlConnection.Open();
            sdr = cm.ExecuteReader();
            sdr.Read();

            Tutors.Text = "There are " + sdr["TotalTutors"].ToString() + " tutors employed by SMMC";
            sqlConnection.Close();

            query = "SELECT COUNT(DISTINCT ID) AS TotalLessons FROM Lessons";
            cm = new SqlCommand(query, sqlConnection);
            sqlConnection.Open();
            sdr = cm.ExecuteReader();
            sdr.Read();

            Lessons.Text = "There are " + sdr["TotalLessons"].ToString() + " lessons held at SMMC";
            sqlConnection.Close();
        }
    }
}