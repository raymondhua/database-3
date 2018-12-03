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


namespace SMMC.People
{
    public partial class UpdateCertifications : System.Web.UI.Page
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
                VisableItems(false);
                InstrumentDDL.Visible = false;
                PersonDDL.Items.Insert(0, new ListItem("Select a person", ""));
            }
        }
        protected void PersonDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            VisableItems(false);
            LoadInstruments();
            InstrumentDDL.Visible = true;
            InstrumentDDL.Items.Insert(0, new ListItem("Select an instrument", ""));
        }
        protected void InstrumentDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            VisableItems(true);
            LoadCertifications();
        }
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            UpdateCertification();
        }
        private void LoadStudents()
        {
            string select = "SELECT ID, FirstName + ' ' + LastName AS Fullname FROM Person";
            essentials.BindList(select, "Fullname", "ID", PersonDDL, sqlConnection);
        }
        private void LoadInstruments()
        {
            string select = "SELECT * FROM Certifications c INNER JOIN  Person p ON c.PersonID = p.ID INNER JOIN  Instruments i ON c.InstrumentID = i.Instrument WHERE c.PersonID = " + PersonDDL.SelectedValue;
            essentials.BindList(select, "Instrument", "Instrument", InstrumentDDL, sqlConnection);
        }
        private void VisableItems(bool trueOrFalse)
        {
            opLabelID.Visible = trueOrFalse;
            ATCL.Visible = trueOrFalse;
            CertificationLevelID.Visible = trueOrFalse;
            levelLabelId.Visible = trueOrFalse;
            SubmitButton.Visible = trueOrFalse;
        }
        private void LoadCertifications()
        {
            string query = "SELECT * FROM Certifications c INNER JOIN Person p ON c.PersonID = p.ID INNER JOIN  Instruments i ON c.InstrumentID = i.Instrument WHERE c.PersonID = @PersonID AND c.InstrumentID = @InstrumentID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = PersonDDL.SelectedValue},
                new SqlParameter("@InstrumentID", SqlDbType.VarChar) {Value = InstrumentDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());
            sqlConnection.Open();
            SqlDataReader sdr = cm.ExecuteReader();
            sdr.Read();
            CertificationLevelID.Text = sdr["CertificationLevel"].ToString();
            ATCL.Text = sdr["ATCL"].ToString();
            sqlConnection.Close();

        }
        private void UpdateCertification()
        {
            try
            {
                sqlConnection.Open();
                string query = "UPDATE Certifications SET CertificationLevel = @CertificationLevel, ATCL = @ATCL WHERE PersonID = @PersonID AND InstrumentID = @InstrumentID";
                SqlCommand cm = new SqlCommand(query, sqlConnection);
                List<SqlParameter> prm = new List<SqlParameter>()
                {
                    new SqlParameter("@CertificationLevel", SqlDbType.Int) {Value = CertificationLevelID.Text},
                    new SqlParameter("@ATCL", SqlDbType.VarChar) {Value = ATCL.SelectedValue},
                    new SqlParameter("@PersonID", SqlDbType.Int) {Value = PersonDDL.SelectedValue},
                    new SqlParameter("@InstrumentID", SqlDbType.VarChar) {Value = InstrumentDDL.SelectedValue},
                };
                cm.Parameters.AddRange(prm.ToArray());
                int code = cm.ExecuteNonQuery();

                sqlConnection.Close();
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
    }
}