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

namespace SMMC.Tutors
{
    public partial class Insert : System.Web.UI.Page
    {
        SqlConnection sqlConnection;
        Essentials essentials;
        protected void Page_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            essentials = new Essentials();
            if (!Page.IsPostBack)
            {
                LoadItems();
                InstrumentLabelID.Visible = false;
                InstrumentDDL.Visible = false;
                LoadTutorTypes();
            }
        }
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessSubmittedData();
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
        protected void PersonDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PersonDDL.Text != "")
            {
                InstrumentLabelID.Visible = true;
                InstrumentDDL.Visible = true;
                LoadInstruments();
            }
            else
            {
                InstrumentLabelID.Visible = false;
                InstrumentDDL.Visible = false;
            }

        }
        private void LoadItems()
        {
            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            string select = "SELECT ID, FirstName + ' ' + LastName AS Fullname FROM Person WHERE ID IN (SELECT PersonID FROM Certifications WHERE (CertificationLevel = 8))";
            essentials.BindList(select, "Fullname", "ID", PersonDDL, sqlConnection);
        }
        private void LoadInstruments()
        {
            string select = "SELECT * FROM Certifications WHERE PersonID = @PersonID AND (CertificationLevel = 8) AND InstrumentID NOT IN(SELECT InstrumentID FROM Tutors WHERE PersonID = @PersonID)";
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = PersonDDL.SelectedValue},
            };
            essentials.BindList(select, "InstrumentID", "InstrumentID", InstrumentDDL, sqlConnection, prm);
            InstrumentDDL.Items.Insert(0, new ListItem("", ""));
        }
        private void LoadTutorTypes()
        {
            string select = "SELECT * FROM TutorType";
            essentials.BindList(select, "ID", "ID", EnsemblesDDL, sqlConnection);
            PersonDDL.Items.Insert(0, new ListItem("", ""));
        }

        private void ProcessSubmittedData()
        {
            string query = "INSERT INTO Tutors VALUES(@PersonID, @Instrument, @TutorType)";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = PersonDDL.SelectedValue},
                new SqlParameter("@Instrument", SqlDbType.VarChar) {Value = InstrumentDDL.SelectedValue},
                new SqlParameter("@TutorType", SqlDbType.VarChar) {Value = EnsemblesDDL.SelectedValue},
            };
            LoadInstruments();
            sqlConnection.Open();
            cm.Parameters.AddRange(prm.ToArray());
            int code = cm.ExecuteNonQuery();

            sqlConnection.Close();
        }
    }
}