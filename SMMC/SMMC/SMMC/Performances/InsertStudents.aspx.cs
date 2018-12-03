using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Globalization;

namespace SMMC.Performances
{
    public partial class InsertStudents : System.Web.UI.Page
    {
        SqlConnection sqlConnection;
        Essentials essentials;
        protected void Page_Load(object sender, EventArgs e)
        {
            essentials = new Essentials();
            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            if (!Page.IsPostBack)
            {
                LoadPerformances();
                StudentDDL.Visible = false;
            }
        }
        protected void SelectPerformanceButton_Click(object sender, EventArgs e)
        {
            StudentDDL.Visible = true;
            LoadStudents();
        }
        protected void SelectStudentButton_Click(object sender, EventArgs e)
        {
            StudentDDL.Visible = true;
            LoadInstruments();
        }
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                InsertStudentsInfo();
                LoadStudents();
            }
            catch (SqlException ex)
            {
                SuccessLabel.Text = "SQL error";
            }
            catch (Exception ex)
            {
                SuccessLabel.Text = "Something went wrong!";
            }
        }
        private void LoadPerformances()
        {
            string select = "SELECT p.ID, Name + ' - ' + CONVERT(VARCHAR(12),p.Date) + ' - ' + CONVERT(VARCHAR(12),p.Time) AS PerformanceInfo FROM Performances p INNER JOIN Venues v ON p.VenueID=v.ID";
            essentials.BindList(select, "PerformanceInfo", "ID", PerformanceDDL, sqlConnection);
        }
        private void LoadStudents()
        {
            string select = "SELECT DISTINCT p.ID, p.FirstName + ' ' + p.LastName AS FullName FROM StudentInstrument si INNER JOIN Student s ON si.StudentID = s.PersonID INNER JOIN Person p ON s.PersonID = p.ID WHERE s.PersonID NOT IN (SELECT si.StudentID FROM PerformancesStudent ps INNER JOIN Performances p ON ps.PerformancesID=p.ID INNER JOIN StudentInstrument si ON ps.StudentID=si.StudentID WHERE ps.InstrumentID=si.InstrumentID AND p.ID = @PerformanceID)";
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                 new SqlParameter("@PerformanceID", SqlDbType.Int) {Value = PerformanceDDL.SelectedValue},
            };
            essentials.BindList(select, "FullName", "ID", StudentDDL, sqlConnection, prm);
        }
        private void LoadInstruments()
        {
            string select = "SELECT InstrumentID FROM StudentInstrument si WHERE si.StudentID = @StudentID";
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                 new SqlParameter("@StudentID", SqlDbType.Int) {Value = StudentDDL.SelectedValue},
            };
            essentials.BindList(select, "InstrumentID", "InstrumentID", InstrumentDDL, sqlConnection, prm);
        }
        private void InsertStudentsInfo()
        {
            string query = "INSERT INTO PerformancesStudent VALUES(@PerformanceID,@StudentID,@InstrtumentID)";
            sqlConnection.Open();
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PerformanceID", SqlDbType.Int) {Value = PerformanceDDL.SelectedValue},
                new SqlParameter("@StudentID", SqlDbType.Int) {Value = StudentDDL.SelectedValue},
                new SqlParameter("@InstrtumentID", SqlDbType.VarChar) {Value = InstrumentDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());

            int code = cm.ExecuteNonQuery();
            sqlConnection.Close();
            SuccessLabel.Text =  " people have been inserted";
        }
    }
}