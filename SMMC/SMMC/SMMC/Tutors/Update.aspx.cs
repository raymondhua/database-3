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
    public partial class Update : System.Web.UI.Page
    {
        Essentials essentials;
        SqlConnection sqlConnection;
        protected void Page_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            essentials = new Essentials();
            if (!Page.IsPostBack)
            {
                string select = "SELECT t.ID, FirstName + ' ' + LastName + ' - ' + InstrumentID AS Name FROM Tutors t INNER JOIN  Person p ON t.PersonID  = p.ID";
                essentials.BindList(select, "Name", "ID", TutorDDL, sqlConnection);
            }
        }
        protected void TutorDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTutorData();
        }
        protected void SubmitButton_Click(object sender, EventArgs e)
        {

                UpdateTutorDetails();
                UpdatePhoneDetails();
                UpdateAddressDetails();
                SuccessLabel.Text = "Information added into the database";

        }
        private void LoadTutorData()
        {
            string query = "SELECT * FROM Tutors t INNER JOIN Person p ON t.PersonID = p.ID INNER JOIN PersonAddress pa ON p.ID = pa.PersonID INNER JOIN Address a ON pa.AddressID = a.ID WHERE t.ID = @PersonID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = TutorDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());
            sqlConnection.Open();
            SqlDataReader sdr = cm.ExecuteReader();
            sdr.Read();
            if (sdr["Type"].ToString() == "Head")
            {
                TutorType.SelectedValue = "Head";
            }
            else if (sdr["Type"].ToString() == "Senior")
            {
                TutorType.SelectedValue = "Senior";
            }
            else
            {
                TutorType.SelectedValue = "Junior";
            }
            PhoneID.Text = sdr["PhoneNo"].ToString();
            StreetID.Text = sdr["Street"].ToString();
            SuburbID.Text = sdr["Suburb"].ToString();
            CityID.Text = sdr["City"].ToString();
            Postcode.Text = sdr["Postcode"].ToString();
            sqlConnection.Close();
        }
        private void UpdateTutorDetails()
        {
            SqlCommand cm = new SqlCommand("UpdateTutor", sqlConnection);
            SqlDataAdapter adapt = new SqlDataAdapter(cm);
            adapt.SelectCommand.CommandType = CommandType.StoredProcedure;

            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@Type", SqlDbType.VarChar) {Value = TutorType.SelectedValue},
                new SqlParameter("@TutorID", SqlDbType.Int) {Value = TutorDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());

            DataTable dt = new DataTable();
            adapt.Fill(dt);
        }
        private void UpdatePhoneDetails()
        {
            string query = "UPDATE Person SET PhoneNo = @PhoneNo WHERE ID = @PersonID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PhoneNo", SqlDbType.VarChar) {Value = PhoneID.Text},
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = GetPersonID()},
            };
            sqlConnection.Open();
            cm.Parameters.AddRange(prm.ToArray());
            int code = cm.ExecuteNonQuery();

            sqlConnection.Close();
        }
        private void UpdateAddressDetails()
        {
            string query = "UPDATE Address SET Street = @Street, Suburb = @Suburb, City = @City, Postcode = @Postcode WHERE ID = @AddressID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@Street", SqlDbType.VarChar) {Value = StreetID.Text},
                new SqlParameter("@Suburb", SqlDbType.VarChar) {Value = SuburbID.Text},
                new SqlParameter("@City", SqlDbType.VarChar) {Value = CityID.Text},
                new SqlParameter("@Postcode", SqlDbType.Int) {Value = Postcode.Text},
                new SqlParameter("@AddressID", SqlDbType.Int) {Value = GetAddressID()},
            };
            sqlConnection.Open();
            cm.Parameters.AddRange(prm.ToArray());
            int code = cm.ExecuteNonQuery();

            sqlConnection.Close();
        }
        private string GetPersonID()
        {
            string personID = "";
            string query = "SELECT p.ID FROM Tutors t INNER JOIN Person p ON t.PersonID = p.ID INNER JOIN PersonAddress pa ON p.ID = pa.PersonID INNER JOIN Address a ON pa.AddressID = a.ID WHERE t.ID = @PersonID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = TutorDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());
            sqlConnection.Open();
            SqlDataReader sdr = cm.ExecuteReader();
            sdr.Read();
            personID = sdr["ID"].ToString();
            sqlConnection.Close();
            return personID;
        }
        private string GetAddressID()
        {
            string addressID = "";
            string query = "SELECT * FROM Tutors t INNER JOIN Person p ON t.PersonID = p.ID INNER JOIN PersonAddress pa ON p.ID = pa.PersonID INNER JOIN Address a ON pa.AddressID = a.ID WHERE t.ID = @PersonID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = TutorDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());
            sqlConnection.Open();
            SqlDataReader sdr = cm.ExecuteReader();
            sdr.Read();
            addressID = sdr["AddressID"].ToString();
            sqlConnection.Close();
            return addressID;
        }
    }
}