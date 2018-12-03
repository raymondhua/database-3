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

namespace SMMC.SheetMusic
{
    public partial class View : System.Web.UI.Page
    {
        Essentials essentials;
        SqlConnection sqlConnection;
        SqlDataAdapter dataAdapter;
        DataSet studentDataSet;
        protected void Page_Load(object sender, EventArgs e)
        {
            essentials = new Essentials();
            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            if (!Page.IsPostBack)
            {
                string select = "SELECT * FROM SheetMusic";
                essentials.BindList(select, "Name", "ID", SheetMusicDDL, sqlConnection);
            }
        }
        protected void SheetMusicDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSheetMusicInfo();
            LoadInstrumentInfo();
            LoadTutorInfo();
        }
        private void LoadSheetMusicInfo()
        {
            string query = "SELECT * from SheetMusic WHERE ID = @SheetMusicID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@SheetMusicID", SqlDbType.Int) {Value = SheetMusicDDL.Text},
            };
            cm.Parameters.AddRange(prm.ToArray());
            sqlConnection.Open();
            SqlDataReader sdr = cm.ExecuteReader();
            sdr.Read();
            NameID.Text = "Name:"; NameOutputID.Text = sdr["Name"].ToString();
            CopiesAllowedID.Text = "Copies allowed:"; CopiesAllowedOutputID.Text = sdr["CopiesAllowed"].ToString();
            DistrubitedCopiesID.Text = "Distrubited copies:"; DistrubitedCopiesOutputID.Text = sdr["DistrubitedCopies"].ToString();
            OrchestralID.Text = "Orchestral:";
            if (sdr["Orchestral"].ToString()=="True")
            {
                OrchestralLabelID.Text = "Yes";
            }
            else
            {
                OrchestralLabelID.Text = "No";
            }
            sqlConnection.Close();


            query = "SELECT GivenCopies FROM SheetMusic sm INNER JOIN SheetMusicTutors smt ON sm.ID = smt.SheetMusicID WHERE sm.ID = SheetMusicID GROUP BY GivenCopies";
            cm = new SqlCommand(query, sqlConnection);
            prm = new List<SqlParameter>()
            {
                new SqlParameter("@SheetMusicID", SqlDbType.Int) {Value = SheetMusicDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());
            sqlConnection.Open();
            sdr = cm.ExecuteReader();
            sdr.Read();
            CopiesGivenToTutorsID.Text = "Copies given to tutors:"; CopiesGivenToTutorsOutputID.Text = sdr["GivenCopies"].ToString();
            sqlConnection.Close();

            query = "SELECT GivenToStudents FROM SheetMusic sm INNER JOIN SheetMusicTutors smt ON sm.ID = smt.SheetMusicID WHERE sm.ID = SheetMusicID GROUP BY GivenToStudents";
            cm = new SqlCommand(query, sqlConnection);
            prm = new List<SqlParameter>()
            {
                new SqlParameter("@SheetMusicID", SqlDbType.Int) {Value = SheetMusicDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());
            sqlConnection.Open();
            sdr = cm.ExecuteReader();
            sdr.Read();
            CopiesGivenToStudentsID.Text = "Copies given to students:"; CopiesGivenToStudentsOuptutID.Text = sdr["GivenToStudents"].ToString();
            sqlConnection.Close();
        }
        private void LoadInstrumentInfo()
        {
            dataAdapter = new SqlDataAdapter();
            studentDataSet = new DataSet();

            string query = "SELECT * FROM SheetMusicInstruments WHERE SheetMusicID = @SheetMusicID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@SheetMusicID", SqlDbType.Int) {Value = SheetMusicDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());
            dataAdapter.SelectCommand = cm;

            sqlConnection.Open();
            dataAdapter.Fill(studentDataSet, "SheetMusicInstruments");
            sqlConnection.Close();

            DataTable studentTable = studentDataSet.Tables["SheetMusicInstruments"];

            DataTable dt = new DataTable();
            dt.Columns.Add("Instrument");

            foreach (DataRow currentRecord in studentTable.Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["Instrument"] = currentRecord["InstrumentID"];
                dt.Rows.Add(dr1);
            }

            InstrumentGridView.DataSource = dt;
            InstrumentGridView.DataBind();

            sqlConnection.Close();
        }
        private void LoadTutorInfo()
        {
            dataAdapter = new SqlDataAdapter();
            studentDataSet = new DataSet();

            string query = "SELECT FirstName + ' ' + LastName AS Fullname, GivenCopies, GivenToStudents, GivenCopies-GivenToStudents AS CopiesRemaining FROM SheetMusicTutors smt INNER JOIN Tutors t ON smt.TutorID = t.ID INNER JOIN Person p ON t.PersonID = p.ID WHERE SheetMusicID = @SheetMusicID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@SheetMusicID", SqlDbType.Int) {Value = SheetMusicDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());
            dataAdapter.SelectCommand = cm;

            sqlConnection.Open();
            dataAdapter.Fill(studentDataSet, "SheetMusicTutors");
            sqlConnection.Close();

            DataTable studentTable = studentDataSet.Tables["SheetMusicTutors"];

            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("GivenCopies");
            dt.Columns.Add("GivenToStudents");
            dt.Columns.Add("CopiesRemaining");

            foreach (DataRow currentRecord in studentTable.Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["Name"] = currentRecord["Fullname"];
                dr1["GivenCopies"] = currentRecord["GivenCopies"];
                dr1["GivenToStudents"] = currentRecord["GivenToStudents"];
                dr1["CopiesRemaining"] = currentRecord["CopiesRemaining"]; ;
                dt.Rows.Add(dr1);
            }

            TutorsGridView.DataSource = dt;
            TutorsGridView.DataBind();

            sqlConnection.Close();
        }
    }
}