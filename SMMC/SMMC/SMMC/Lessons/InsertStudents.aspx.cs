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
    public partial class InsertStudents : System.Web.UI.Page
    {
        Essentials essentials;
        SqlConnection sqlConnection;
        private string instrument;
        private int level;
        private int lessonID;
        protected void Page_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            essentials = new Essentials();
            if (!Page.IsPostBack)
            {
                level = 0;
                instrument = "";
                lessonID = 0;
                string select = "SELECT ID, CONVERT(VARCHAR(5),Time) + ' - Level ' + CONVERT(VARCHAR(3),Level) + ' - ' + InstrumentID AS LevelLabel FROM Lessons";
                essentials.BindList(select, "LevelLabel", "ID", LessonDDL, sqlConnection);
            }
        }
        protected void InsertInstrumentsButton_Click(object sender, EventArgs e)
        {
            try
            {
                InsertInstruments();
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
        protected void LessonDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLessonInformation();
        }
        private void InsertInstruments()
        {
            int count = 0;
            List<string> selected = StudentInstrumentCB.Items.Cast<ListItem>()
            .Where(li => li.Selected)
            .Select(li => li.Value)
            .ToList();
            string query = "INSERT INTO LessonStudents VALUES(@LessonID, @StudentID)";
            sqlConnection.Open();
            foreach (string select in selected)
            {
                SqlCommand cm = new SqlCommand(query, sqlConnection);
                List<SqlParameter> prm = new List<SqlParameter>()
                {
                    new SqlParameter("@LessonID", SqlDbType.Int) {Value = LessonDDL.SelectedValue},
                    new SqlParameter("@StudentID", SqlDbType.VarChar) {Value = select},
                };
                cm.Parameters.AddRange(prm.ToArray());

                int code = cm.ExecuteNonQuery();
                count++;
            }
            sqlConnection.Close();
            SuccessLabel.Text = count.ToString() + " instruments now added in the database";
            LoadLessonInformation();
        }
        private void LoadLessonInformation()
        {
            string query = "SELECT * FROM Lessons WHERE ID = @LessonID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@LessonID", SqlDbType.Int) {Value = LessonDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());
            sqlConnection.Open();
            SqlDataReader sdr = cm.ExecuteReader();
            sdr.Read();
            lessonID = Convert.ToInt32(sdr["ID"]);
            instrument = sdr["InstrumentID"].ToString();
            level = Convert.ToInt32(sdr["Level"]);
            sqlConnection.Close();

            query = "SELECT si.StudentID, p.FirstName + ' ' + p.LastName AS FullName FROM StudentInstrument si INNER JOIN  Person p ON si.StudentID  = p.ID WHERE si.InstrumentID = @InstrumentID AND si.StudentID IN (SELECT PersonID FROM Certifications WHERE CertificationLevel = @Level) AND si.StudentID NOT IN (SELECT StudentID FROM LessonStudents WHERE LessonID = @LessonID)";
            prm = new List<SqlParameter>()
            {
                new SqlParameter("@InstrumentID", SqlDbType.VarChar) {Value = instrument},
                new SqlParameter("@Level", SqlDbType.Int) {Value = level},
                new SqlParameter("@LessonID", SqlDbType.Int) {Value = LessonDDL.SelectedValue},
            };
            essentials.CheckBox(query, "FullName", "StudentID", StudentInstrumentCB, sqlConnection, prm);
        }
    }
}