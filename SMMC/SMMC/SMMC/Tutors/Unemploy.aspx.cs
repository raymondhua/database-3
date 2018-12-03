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
    public partial class Unemploy : System.Web.UI.Page
    {
        Essentials essentials;
        SqlConnection sqlConnection;
        protected void Page_Load(object sender, EventArgs e)
        {
            essentials = new Essentials();
            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            if (!Page.IsPostBack)
            {
                string select = "SELECT DISTINCT p.ID, p.FirstName + ' ' + p.LastName AS Fullname FROM Tutors t INNER JOIN Person p ON t.PersonID = p.ID";
                essentials.BindList(select, "Fullname", "ID", TutorDDL, sqlConnection);
                
            }
        }
        protected void TutorDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string select = "SELECT * FROM Tutors t INNER JOIN Person p ON t.PersonID = p.ID INNER JOIN PersonAddress pa ON p.ID = pa.PersonID INNER JOIN Address a ON pa.AddressID = a.ID WHERE p.ID = @PersonID";
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = TutorDDL.SelectedValue},
            };
            essentials.BindList(select, "InstrumentID", "InstrumentID", InstrumentDDL, sqlConnection, prm);
            InstrumentDDL.Items.Insert(0, new ListItem("Delete all instruments", ""));
        }
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if(InstrumentDDL.Text == "")
            {
                DeleteAllInstruments();
            }
            else
            {
                DeleteInstrument();
            }
        }

        private void DeleteInstrument()
        {
            SqlCommand cm = new SqlCommand("DELETE * FROM Tutors WHERE PersonID=@PersonID AND InstrumentID=@InstrumentID", sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = TutorDDL.SelectedValue},
                new SqlParameter("@InstrumentID", SqlDbType.Int) {Value = InstrumentDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());
            sqlConnection.Open();
            int code = cm.ExecuteNonQuery();
            sqlConnection.Close();
        }

        private void DeleteAllInstruments()
        {
            SqlCommand cm = new SqlCommand("DeleteTutor", sqlConnection);
            SqlDataAdapter adapt = new SqlDataAdapter(cm);
            adapt.SelectCommand.CommandType = CommandType.StoredProcedure;

            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = TutorDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());

            DataTable dt = new DataTable();
            adapt.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                SuccessLabel.Text = "Connection Succedded";
            }
            else
            {
                SuccessLabel.Text = "Connection Fails";
            }
        }
    }
}