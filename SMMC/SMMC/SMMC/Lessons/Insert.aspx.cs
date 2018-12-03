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
    public partial class Insert : System.Web.UI.Page
    {
        Essentials essentials;
        SqlConnection sqlConnection;
        protected void Page_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            essentials = new Essentials();
            if (!Page.IsPostBack)
            {
                string select = "SELECT * FROM Ensembles";
                essentials.BindList(select, "Level", "Level", LevelDDL, sqlConnection);
                select = "SELECT * FROM Instruments";
                essentials.BindList(select, "Instrument", "Instrument", InstrumentDDL, sqlConnection);
            }
        }
        protected void InstrumentDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInstruments();
        }
        protected void LevelDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LevelDDL.Text == "7" || LevelDDL.Text == "8")
            {
                string select = "SELECT t.ID, FirstName + ' ' + LastName AS Name FROM Tutors t INNER JOIN Person p ON t.PersonID  = p.ID WHERE InstrumentID = @InstrumentID AND (t.Type='Head' OR t.Type='Senior')";
                LoadInstruments(select);
            }
            else
            {
                LoadInstruments();
            }
        }
        protected void SumbitButton_OnClick(object sender, EventArgs e)
        {
            SubmitProccessedData();
            SuccessLabel.Text = "Information now added in the database";
        }
        private void LoadInstruments(string select = "SELECT t.ID, FirstName + ' ' + LastName + ' - ' + InstrumentID AS Name FROM Tutors t INNER JOIN Person p ON t.PersonID  = p.ID WHERE InstrumentID = @InstrumentID")
        {
            List<SqlParameter> prm = new List<SqlParameter>()
            {
               new SqlParameter("@InstrumentID", SqlDbType.VarChar) {Value = InstrumentDDL.SelectedValue},
            };
            essentials.BindList(select, "Name", "ID", TutorDDL, sqlConnection, prm);
        }
        private void SubmitProccessedData()
        {
            sqlConnection.Open();
            string query = "INSERT INTO Lessons output INSERTED.ID VALUES (@Level, @InstrumentID, @Time)";
            SqlCommand cm = new SqlCommand(query, sqlConnection);

            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@Level", SqlDbType.Int) {Value = LevelDDL.SelectedValue},
                new SqlParameter("@InstrumentID", SqlDbType.VarChar) {Value = InstrumentDDL.SelectedValue},
                new SqlParameter("@Time", SqlDbType.Time) {Value = TimeID.Text},
            };
            cm.Parameters.AddRange(prm.ToArray());
            int lessonID = (int)cm.ExecuteScalar();

            sqlConnection.Close();

            sqlConnection.Open();
            cm = new SqlCommand("INSERT INTO LessonTutors VALUES(@LessonID, @TutorID)", sqlConnection);
            prm = new List<SqlParameter>()
            {
                new SqlParameter("@LessonID", SqlDbType.Int) {Value = lessonID},
                new SqlParameter("@TutorID", SqlDbType.Int) {Value = TutorDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());
            int code = cm.ExecuteNonQuery();

            sqlConnection.Close();
        }
    }
}