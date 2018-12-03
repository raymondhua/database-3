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
    public partial class Update : System.Web.UI.Page
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
                SetAddressVisibleProperties(false);
                StudentDDL.Items.Insert(0, new ListItem("Select a student", ""));
            }
        }
        protected void StudentDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetAddressVisibleProperties(true);
            LoadStudentDetails();
        }
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateStudentDetails();
                UpdatePhoneDetails();
                UpdateAddressDetails();
                SuccessLabel.Text = "Information added into the database!";
            }
            catch (SqlException)
            {
                SuccessLabel.Text = "SQL error";
            }
            catch (Exception)
            {
                SuccessLabel.Text = "Something went wrong!";
            }
        }
        private void LoadStudents()
        {
            string select = "SELECT ID, FirstName + ' ' + LastName AS Fullname FROM Student s INNER JOIN Person p ON s.PersonID = p.ID";
            essentials.BindList(select, "Fullname", "ID", StudentDDL, sqlConnection);
        }
        private void SetAddressVisibleProperties(bool trueOrFalse)
        {
            OpenDivision.Visible = trueOrFalse;
            opLabelID.Visible = trueOrFalse;
            PhoneID.Visible = trueOrFalse;
            phoneLabelID.Visible = trueOrFalse;
            StreetID.Visible = trueOrFalse;
            SuburbID.Visible = trueOrFalse;
            CityID.Visible = trueOrFalse;
            Postcode.Visible = trueOrFalse;
            StreetLabel.Visible = trueOrFalse;
            SuburbLabel.Visible = trueOrFalse;
            CityLabel.Visible = trueOrFalse;
            PostcodeLabel.Visible = trueOrFalse;
        }
        private void LoadStudentDetails()
        {
            string query = "SELECT * FROM Student s INNER JOIN Person p ON s.PersonID = p.ID INNER JOIN PersonAddress pa ON p.ID = pa.AddressID INNER JOIN Address a ON pa.AddressID = a.ID WHERE p.ID = @PersonID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = StudentDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());
            sqlConnection.Open();
            SqlDataReader sdr = cm.ExecuteReader();
            sdr.Read();
            PhoneID.Text = sdr["PhoneNo"].ToString();
            if (sdr["OpenDivision"].ToString() == "0")
            {
                OpenDivision.SelectedIndex = 0;
            }
            else
            {
                OpenDivision.SelectedIndex = 1;
            }
            StreetID.Text = sdr["Street"].ToString();
            SuburbID.Text = sdr["Suburb"].ToString();
            CityID.Text = sdr["City"].ToString();
            Postcode.Text = sdr["Postcode"].ToString();
            sqlConnection.Close();

        }
        private void UpdateStudentDetails()
        {
            sqlConnection.Open();
            string query = "UPDATE Student SET OpenDivision = @OpenDivision WHERE PersonID = @PersonID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@OpenDivision", SqlDbType.Bit) {Value = OpenDivision.SelectedValue},
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = StudentDDL.SelectedValue},
            };

            cm.Parameters.AddRange(prm.ToArray());
            int code = cm.ExecuteNonQuery();

            sqlConnection.Close();
        }
        private void UpdatePhoneDetails()
        {
            sqlConnection.Open();
            string query = "UPDATE Person SET PhoneNo = @PhoneNo WHERE ID = @PersonID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PhoneNo", SqlDbType.Int) {Value = PhoneID.Text},
                new SqlParameter("@PersonID", SqlDbType.VarChar) {Value = StudentDDL.SelectedValue},
            };

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

        private string GetAddressID()
        {
            string addressID = "";
            string query = "SELECT * FROM Student s INNER JOIN Person p ON s.PersonID = p.ID INNER JOIN PersonAddress pa ON p.ID = pa.AddressID INNER JOIN Address a ON pa.AddressID = a.ID WHERE p.ID = @PersonID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = StudentDDL.SelectedValue},
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