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


namespace SMMC.Students
{
    public partial class InsertInstrument : System.Web.UI.Page
    {
        SqlConnection sqlConnection;
        Essentials essentials;
        protected void Page_Load(object sender, EventArgs e)
        {
            essentials = new Essentials();
            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            if (!Page.IsPostBack)
            {
                LoadStudents();
                DeleteInstrumentsButton.Visible = false;
            }
        }
        protected void SelectStudentButton_Click(object sender, EventArgs e)
        {
            DeleteInstrumentsButton.Visible = true;
            LoadInstruments();
        }
        protected void DeleteInstrumentsButton_Click(object sender, EventArgs e)
        {
            try
            {
                InsertItems();
                LoadInstruments();
                Label1.Text = "Instrument have been inserted";
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
        private void LoadStudents()
        {
            string select = "SELECT ID, FirstName + ' ' + LastName AS Fullname FROM Person";
            essentials.BindList(select, "Fullname", "ID", PersonDDL, sqlConnection);
        }
        private void LoadInstruments()
        {
            string select = "SELECT i.Instrument FROM StudentInstrument si INNER JOIN  Student s ON si.StudentID = s.PersonID INNER JOIN  Instruments i ON si.InstrumentID = i.Instrument WHERE s.PersonID = " + PersonDDL.SelectedValue;
            string mainQuery = "SELECT DISTINCT Instrument FROM Instruments i WHERE Instrument NOT IN ( " + select + " )";
            essentials.BindList(mainQuery, "Instrument", "Instrument", InstrumentDDL, sqlConnection);
        }
        private void InsertItems()
        {
            string query = "INSERT INTO StudentInstrument VALUES(@StudentID, @InstrumentID, @Hire, 0)";
            sqlConnection.Open();
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@StudentID", SqlDbType.Int) {Value = PersonDDL.SelectedValue},
                new SqlParameter("@InstrumentID", SqlDbType.VarChar) {Value = InstrumentDDL.SelectedValue},
                new SqlParameter("@Hire", SqlDbType.Bit) {Value = HireDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());
            int code = cm.ExecuteNonQuery();

            query = "IF NOT EXISTS (SELECT * FROM Certifications WHERE PersonID = @StudentID AND InstrumentID = @InstrumentID) BEGIN INSERT INTO Certifications(PersonID, InstrumentID) VALUES(@StudentID, @InstrumentID) END";
            cm = new SqlCommand(query, sqlConnection);
            prm = new List<SqlParameter>()
            {
                new SqlParameter("@StudentID", SqlDbType.Int) {Value = PersonDDL.SelectedValue},
                new SqlParameter("@InstrumentID", SqlDbType.VarChar) {Value = InstrumentDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());
            code = cm.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}