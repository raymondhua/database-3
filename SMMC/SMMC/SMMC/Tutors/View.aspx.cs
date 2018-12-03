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

namespace SMMC.Tutors
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
                NameLabel.Text = "View A Tutor";
                string select = "SELECT DISTINCT p.ID, p.FirstName + ' ' + p.LastName AS Fullname FROM Tutors t INNER JOIN Person p ON t.PersonID = p.ID";
                essentials.BindList(select, "Fullname", "ID", PersonDDL, sqlConnection);
            }
        }
        protected void TutorDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTutorData();
            LoadTutorTypeData();
            LoadTutorLessonData();
        }
        private void LoadTutorData()
        {
            string query = "SELECT p.ID, p.FirstName, p.LastName, a.Street + ', ' + a.Suburb + ', ' + a.City + ', ' + CONVERT(VARCHAR(12),Postcode) as Address, p.PhoneNo FROM PersonAddress pa INNER JOIN Tutors t ON pa.PersonID = t.PersonID INNER JOIN  Address a ON pa.AddressID = a.ID INNER JOIN  Person p ON pa.PersonID = p.ID WHERE t.PersonID = @PersonID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = PersonDDL.Text},
            };
            cm.Parameters.AddRange(prm.ToArray());
            sqlConnection.Open();
            SqlDataReader sdr = cm.ExecuteReader();
            sdr.Read();
            NameLabel.Text = sdr["FirstName"].ToString() + " " + sdr["LastName"].ToString();
            PhoneID.Text = "Phone: "; PhoneOutputID.Text = sdr["PhoneNo"].ToString();
            AddressID.Text = "Address: "; AddressOutputID.Text = sdr["Address"].ToString();
            sqlConnection.Close();

        }
        private void LoadTutorTypeData()
        {
            dataAdapter = new SqlDataAdapter();
            studentDataSet = new DataSet();

            string query = "SELECT * FROM Tutors t INNER JOIN TutorType tt ON t.Type = tt.ID WHERE t.PersonID = @PersonID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = PersonDDL.Text},
            };
            cm.Parameters.AddRange(prm.ToArray());
            dataAdapter.SelectCommand = cm;

            sqlConnection.Open();
            dataAdapter.Fill(studentDataSet, "Tutors");
            sqlConnection.Close();

            DataTable tutorTable = studentDataSet.Tables["Tutors"];

            DataTable dt = new DataTable();
            dt.Columns.Add("Instrument");
            dt.Columns.Add("Type");

            foreach (DataRow currentRecord in tutorTable.Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["Instrument"] = currentRecord["InstrumentID"];
                dr1["Type"] = currentRecord["Type"];
                dt.Rows.Add(dr1);
            }

            essentials.MoreThanZero(dt, InstrumentsLabel, "Instruments playing");

            TutorTypeGridView.DataSource = dt;
            TutorTypeGridView.DataBind();

            sqlConnection.Close();
        }
        private void LoadTutorLessonData()
        {
            dataAdapter = new SqlDataAdapter();
            studentDataSet = new DataSet();

            string query = "SELECT l.InstrumentID, l.Level, l.Time, cast(round(tt.HourlyRate*0.5,2) as numeric(36,2)) AS Pay FROM Lessons l INNER JOIN LessonTutors lt ON l.ID=lt.LessonID INNER JOIN Tutors t ON lt.TutorID=t.ID INNER JOIN TutorType tt ON t.Type=tt.ID WHERE l.InstrumentID=t.InstrumentID AND t.PersonID = @PersonID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = PersonDDL.Text},
            };
            cm.Parameters.AddRange(prm.ToArray());
            dataAdapter.SelectCommand = cm;

            sqlConnection.Open();
            dataAdapter.Fill(studentDataSet, "LessonTutors");
            sqlConnection.Close();

            DataTable studentTable = studentDataSet.Tables["LessonTutors"];

            DataTable dt = new DataTable();
            dt.Columns.Add("Instrument");
            dt.Columns.Add("Level");
            dt.Columns.Add("Time");
            dt.Columns.Add("Pay");

            decimal lessonPay = 0;
            foreach (DataRow currentRecord in studentTable.Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["Instrument"] = currentRecord["InstrumentID"];
                dr1["Level"] = currentRecord["Level"];
                dr1["Time"] = currentRecord["Time"];
                dr1["Pay"] = currentRecord["Pay"]; ;
                dt.Rows.Add(dr1);
                if (currentRecord["Pay"] != null)
                {
                    lessonPay += Convert.ToDecimal(currentRecord["Pay"]);
                }
            }

            bool visable = essentials.MoreThanZero(dt, LessonsLabel, "Lessons teaching");

            LessonsGridView.DataSource = dt;
            LessonsGridView.DataBind();

            if(visable==true)
            {
                TotalLessonPay.Visible = true;
                TotalLessonPay.Text = "Total lesson pay: $" + lessonPay;
            }
            else
            {
                TotalLessonPay.Visible = false;
            }
            
            sqlConnection.Close();

        }
    }
}