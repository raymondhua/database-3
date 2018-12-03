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
    public partial class InsertCertifications : System.Web.UI.Page
    {
        SqlConnection sqlConnection;
        Essentials essentials;
        protected void Page_Load(object sender, EventArgs e)
        {
            essentials = new Essentials();
            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            if (!Page.IsPostBack)
            {
                LoadPeople();
                LoadEnsembles();
            }
        }
        protected void PersonDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInstruments();
        }
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                InsertItems();
            }
            catch (SqlException ex)
            {
                SuccessLabelID.Text = "SQL error";
            }
            catch (Exception ex)
            {
                SuccessLabelID.Text = "Something went wrong!";
            }
            finally
            {
                SuccessLabelID.Text = "Information added into the database";
            }
        }
        private void LoadPeople()
        {
            string select = "SELECT ID, FirstName + ' ' + LastName AS Fullname FROM Person";
            essentials.BindList(select, "Fullname", "ID", PersonDDL, sqlConnection);
            PersonDDL.Items.Insert(0, new ListItem("", ""));
        }
        private void LoadInstruments()
        {
            string select = "SELECT Instrument FROM Certifications c INNER JOIN  Person p ON c.PersonID = p.ID INNER JOIN  Instruments i ON c.InstrumentID = i.Instrument WHERE PersonID = " + PersonDDL.SelectedValue;
            string mainQuery = "SELECT DISTINCT Instrument FROM Instruments i WHERE Instrument NOT IN ( " + select + " )";
            essentials.BindList(mainQuery, "Instrument", "Instrument", InstrumentDDL, sqlConnection);
        }
        private void LoadEnsembles()
        {
            string select = "SELECT * FROM Ensembles";
            essentials.BindList(select, "Level", "Level", CLDDL, sqlConnection);
        }
        private void InsertItems()
        {
            string query = "INSERT INTO Certifications VALUES(@PersonID, @InstrumentID, @Level, @ATCL)";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            sqlConnection.Open();
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = PersonDDL.SelectedValue},
                new SqlParameter("@InstrumentID", SqlDbType.VarChar) {Value = InstrumentDDL.SelectedValue},
                new SqlParameter("@Level", SqlDbType.Int) {Value = CLDDL.SelectedValue},
                new SqlParameter("@ATCL", SqlDbType.Bit) {Value = ATCL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());
            int code = cm.ExecuteNonQuery();
            sqlConnection.Close();
            LoadInstruments();
        }
    }
}