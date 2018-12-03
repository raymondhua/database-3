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
    public partial class ViewAllStudents : System.Web.UI.Page
    {
        SqlConnection sqlConnection;
        SqlDataAdapter dataAdapter;
        DataSet studentDataSet;
        protected void Page_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            LoadItems();
        }

        private void LoadItems()
        {
            dataAdapter = new SqlDataAdapter();
            studentDataSet = new DataSet();


            string query = "SELECT p.FirstName, p.LastName, CASE WHEN s.OpenDivision = 0 THEN 'No' ELSE 'Yes' END as OpenDivision, l.InstrumentID, l.Level, l.Time FROM LessonStudents ls INNER JOIN  Lessons l ON ls.LessonID = l.ID INNER JOIN  Student s ON ls.StudentID = s.PersonID  INNER JOIN  Person p ON s.PersonID = p.ID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);

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
                dr1["Instrument"] = currentRecord["InstrumentID"];
                dr1["Level"] = currentRecord["Level"];
                dr1["Time"] = currentRecord["Time"];
                dt.Rows.Add(dr1);
            }

            AllStudentsGridView.DataSource = dt;
            AllStudentsGridView.DataBind();

        }
    }
}