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

namespace SMMC.Lessons
{
    public partial class View : System.Web.UI.Page
    {
        Essentials essentials;
        SqlConnection sqlConnection;
        SqlDataAdapter dataAdapter;
        DataSet studentDataSet;
        protected void Page_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            essentials = new Essentials();
            if (!Page.IsPostBack)
            {
                string select = "SELECT ID, CONVERT(VARCHAR(5),Time) + ' - Level ' + CONVERT(VARCHAR(3),Level) + ' - ' + InstrumentID AS LevelLabel FROM Lessons";
                essentials.BindList(select, "LevelLabel", "ID", LessonDDL, sqlConnection);
            }
        }
        protected void SelectLessonButton_Click(object sender, EventArgs e)
        {
            LoadLessonInformation();
            LoadStudentData();
            LoadTeacherData();
        }
        private void LoadLessonInformation()
        {
            string query = "SELECT * FROM Lessons WHERE ID = @LessonID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@LessonID", SqlDbType.Int) {Value = LessonDDL.Text},
            };
            cm.Parameters.AddRange(prm.ToArray());
            sqlConnection.Open();
            SqlDataReader sdr = cm.ExecuteReader();
            sdr.Read();
            InstrumentID.Text = "Level: "; InstrumentOutputID.Text = sdr["Level"].ToString();
            TimeID.Text = "Instrument: "; TimeOutputID.Text = sdr["InstrumentID"].ToString();
            LevelID.Text = "Time: "; LevelOutputID.Text = sdr["Time"].ToString();
            sqlConnection.Close();

        }
        private void LoadStudentData()
        {
            dataAdapter = new SqlDataAdapter();
            studentDataSet = new DataSet();


            string query = "SELECT p.FirstName, p.LastName, CASE WHEN s.OpenDivision = 0 THEN 'No' ELSE 'Yes' END as OpenDivision, l.InstrumentID, l.Level, l.Time FROM LessonStudents ls INNER JOIN  Lessons l ON ls.LessonID = l.ID INNER JOIN  Student s ON ls.StudentID = s.PersonID  INNER JOIN  Person p ON s.PersonID = p.ID WHERE l.ID = @LessonID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@LessonID", SqlDbType.Int) {Value = LessonDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());

            dataAdapter.SelectCommand = cm;
            sqlConnection.Open();
            dataAdapter.Fill(studentDataSet, "Student");
            sqlConnection.Close();

            DataTable studentTable = studentDataSet.Tables["Student"];

            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("OpenDivision");
            dt.Columns.Add("Instrument");
            dt.Columns.Add("Level");
            dt.Columns.Add("Time");

            foreach (DataRow currentRecord in studentTable.Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["Name"] = currentRecord["FirstName"] + " " + currentRecord["LastName"];
                dr1["OpenDivision"] = currentRecord["OpenDivision"];
                dt.Rows.Add(dr1);
            }

            StudentsGridView.DataSource = dt;
            StudentsGridView.DataBind();

        }
        private void LoadTeacherData()
        {
            dataAdapter = new SqlDataAdapter();
            studentDataSet = new DataSet();


            string query = "SELECT p.FirstName, p.LastName, l.InstrumentID, l.Level, l.Time FROM LessonTutors lt INNER JOIN Lessons l ON lt.LessonID = l.ID INNER JOIN  Tutors t ON lt.TutorID = t.ID INNER JOIN  Person p ON t.PersonID = p.ID WHERE l.InstrumentID=t.InstrumentID AND l.ID = @LessonID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@LessonID", SqlDbType.Int) {Value = LessonDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());

            dataAdapter.SelectCommand = cm;

            sqlConnection.Open();
            dataAdapter.Fill(studentDataSet, "Student");
            sqlConnection.Close();

            DataTable studentTable = studentDataSet.Tables["Student"];

            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Instrument");

            foreach (DataRow currentRecord in studentTable.Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["Name"] = currentRecord["FirstName"] + " " + currentRecord["LastName"];
                dt.Rows.Add(dr1);
            }

            TutorsGridView.DataSource = dt;
            TutorsGridView.DataBind();

        }
    }
}