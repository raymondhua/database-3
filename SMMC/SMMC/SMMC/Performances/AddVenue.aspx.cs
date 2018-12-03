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

namespace SMMC.Performances
{
    public partial class AddVenue : System.Web.UI.Page
    {
        Essentials essentials;
        SqlConnection sqlConnection;
        protected void Page_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            essentials = new Essentials();
        }
        protected void InsertButton_Click(object sender, EventArgs e)
        {
            try
            {
                InsertVenue();
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
                SuccessLabel.Text = "The details are now added to database";
            }
        }
        private void InsertVenue()
        {
            string query = "INSERT INTO Venues VALUES (@Name, @Address, @Suburb, @City, @Postcode, @Phone)";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            sqlConnection.Open();
            List<SqlParameter> prm = new List<SqlParameter>()
                {
                    new SqlParameter("@Name", SqlDbType.VarChar) {Value = NameID.Text},
                    new SqlParameter("@Address", SqlDbType.VarChar) {Value = AddressID.Text},
                    new SqlParameter("@Suburb", SqlDbType.VarChar) {Value = SuburbID.Text},
                    new SqlParameter("@City", SqlDbType.VarChar) {Value = CityID.Text},
                    new SqlParameter("@Postcode", SqlDbType.VarChar) {Value = PostcodeID.Text},
                    new SqlParameter("@Phone", SqlDbType.VarChar) {Value = PhoneID.Text},
                };
            cm.Parameters.AddRange(prm.ToArray());
            int code = cm.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}