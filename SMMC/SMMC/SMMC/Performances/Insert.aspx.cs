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
                MajorDDL.Items.Insert(0, new ListItem("False", "False"));
                MajorDDL.Items.Insert(0, new ListItem("True", "True"));
            }
            else
            {

            }
        }
        protected void MajorDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "";
            if(MajorDDL.SelectedValue=="True")
            {
                query = "SELECT * FROM Venues WHERE Name= 'Dunedin Town Hall'";
            }
            else
            {
                query = "SELECT * FROM Venues";
            }
            essentials.BindList(query, "Name", "ID", VenueDDL, sqlConnection);
        }
        protected void SumbitButton_OnClick(object sender, EventArgs e)
        {
            try
            {
                InsertPerformances();
            }
            catch (SqlException ex)
            {
                SuccessLabel.Text = "SQL error";
            }
            catch (Exception ex)
            {
                SuccessLabel.Text = "Something went wrong!";
            }
            finally
            {
                SuccessLabel.Text = "Information now added in database";
            }
        }
        private void InsertPerformances()
        {
            sqlConnection.Open();
            string query = "INSERT INTO Performances VALUES(@Venue, @Date, @Time, @Major)";
            SqlCommand cm = new SqlCommand(query, sqlConnection);

            List<SqlParameter> prm = new List<SqlParameter>()
                {
                    new SqlParameter("@Major", SqlDbType.VarChar) {Value = MajorDDL.SelectedValue},
                    new SqlParameter("@Date", SqlDbType.Date) {Value = DateID.Text},
                    new SqlParameter("@Time", SqlDbType.Time) {Value = TimeID.Text},
                    new SqlParameter("@Venue", SqlDbType.Int) {Value = VenueDDL.SelectedValue},
                };
            cm.Parameters.AddRange(prm.ToArray());
            int code = cm.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}