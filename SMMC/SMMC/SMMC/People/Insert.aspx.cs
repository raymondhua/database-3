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
            }
            else
            {

            }
        }
        protected void ButtonId_Click(object sender, EventArgs e)
        {
            string[] items = new string[]
            {
                FirstNameID.Text, LastNameID.Text, BirthID.Text, PhoneID.Text,
            };
            if (essentials.CheckforValid(items))
            {
                ProcessSubmittedData();
            }
            else
            {
                //Label1.Text = "Your record is missing a few details";
            }
        }
        protected void AddressList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AddressList.Text != "")
            {
                SetAddressVisibleProperties(false);
            }
            else
            {
                SetAddressVisibleProperties(true);
            }

        }
        private void SetAddressVisibleProperties(bool trueOrFalse)
        {
            StreetID.Visible = trueOrFalse;
            SuburbID.Visible = trueOrFalse;
            CityID.Visible = trueOrFalse;
            Postcode.Visible = trueOrFalse;
            StreetLabel.Visible = trueOrFalse;
            SuburbLabel.Visible = trueOrFalse;
            CityLabel.Visible = trueOrFalse;
            PostcodeLabel.Visible = trueOrFalse;
        }
        private void LoadItems()
        {
            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            string select = "SELECT ID, Street + ', ' + Suburb + ', ' + City + ', ' + CONVERT(VARCHAR(12),Postcode) AS FullAddress FROM Address";
            essentials.BindList(select, "FullAddress", "ID", AddressList, sqlConnection);
            AddressList.Items.Insert(0, new ListItem("Enter new address below", ""));
        }
        private void ProcessSubmittedData()
        {
            int addressID = 0;
            int personID = 0;
            sqlConnection.Open();
            string query = "INSERT INTO Person output INSERTED.ID VALUES (@FirstName, @LastName, @DOB, @Phone)";
            SqlCommand cm = new SqlCommand(query, sqlConnection);

            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@FirstName", SqlDbType.VarChar) {Value = FirstNameID.Text},
                new SqlParameter("@LastName", SqlDbType.VarChar) {Value = LastNameID.Text},
                new SqlParameter("@DOB", SqlDbType.Date) {Value = BirthID.Text},
                new SqlParameter("@Phone", SqlDbType.VarChar) {Value = PhoneID.Text},
            };
            cm.Parameters.AddRange(prm.ToArray());
            personID = (int)cm.ExecuteScalar();

            sqlConnection.Close();
            if (AddressList.SelectedValue == "")
            {
                sqlConnection.Open();
                cm = new SqlCommand("INSERT INTO Address output INSERTED.ID VALUES (@Street, @Suburb, @City, @Postcode)", sqlConnection);
                prm = new List<SqlParameter>()
                {
                    new SqlParameter("@Street", SqlDbType.VarChar) {Value = StreetID.Text},
                    new SqlParameter("@Suburb", SqlDbType.VarChar) {Value = SuburbID.Text},
                    new SqlParameter("@City", SqlDbType.VarChar) {Value = CityID.Text},
                    new SqlParameter("@Postcode", SqlDbType.Int) {Value = Postcode.Text},
                };
                cm.Parameters.AddRange(prm.ToArray());
                addressID = (int)cm.ExecuteScalar();
                sqlConnection.Close();
            }
            else
            {
                addressID = Convert.ToInt32(AddressList.SelectedValue);
            }
            sqlConnection.Open();
            cm = new SqlCommand("INSERT INTO PersonAddress VALUES(@PersonID, @AddressID)", sqlConnection);
            prm = new List<SqlParameter>()
            {
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = personID},
                new SqlParameter("@AddressID", SqlDbType.Int) {Value = addressID},
            };
            cm.Parameters.AddRange(prm.ToArray());
            int code = cm.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
