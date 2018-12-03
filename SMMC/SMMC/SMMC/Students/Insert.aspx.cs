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
                SetParent1VisibleProperties(false);
                ExistingParentDDL.Visible = false;
                SelectParentLabel.Visible = false;
                string query = "SELECT ID, FirstName + ' ' + LastName AS Name FROM Person";
                essentials.BindList(query, "Name", "ID", ExistingParentDDL, sqlConnection);
                ExistingParentDDL.Items.Insert(0, new ListItem("Enter new parent", ""));
                ExistingParentDDL.Items.Insert(0, new ListItem("", ""));
                AddressList.Items.Insert(0, new ListItem("Enter new address", ""));
                AddressList.Items.Insert(0, new ListItem("", ""));
                query = "SELECT ID, FirstName + ' ' + LastName AS Name FROM Person WHERE ID NOT IN (SELECT PersonID FROM Student)";
                essentials.BindList(query, "Name", "ID", PersonDDL, sqlConnection);
                PersonDDL.Items.Insert(0, new ListItem("Enter new person", ""));
                PersonDDL.Items.Insert(0, new ListItem("", ""));
            }
        }
        protected void ButtonId_Click(object sender, EventArgs e)
        {
            try
            {
                if (PersonDDL.SelectedValue != "")
                {
                    ProcessExistingPerson();
                }
                else
                {
                    ProcessSubmittedData();
                }
                SuccessLabel.Text = "Your record added into the database";
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
        protected void AddressList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(AddressList.SelectedValue!="")
            {
                SetAddressVisibleProperties(false);
            }
            else
            {
                SetAddressVisibleProperties(true);
            }

        }
        protected void PersonDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PersonDDL.SelectedValue != "")
            {
                HideInfo(false);
                SetAddressVisibleProperties(false);
            }
            else
            {
                HideInfo(true);
                SetAddressVisibleProperties(true);
            }

        }
        protected void ParentRRL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ParentRRL.Text != "True")
            {
                ExistingParentDDL.Visible = false;
                SelectParentLabel.Visible = false;
            }
            else
            {
                ExistingParentDDL.Visible = true;
                SelectParentLabel.Visible = true;
                SetParent1VisibleProperties(true);
            }
        }
        protected void ExistingParentDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ExistingParentDDL.Text != "")
            {
                SetParent1VisibleProperties(false);
            }
            else
            {
                SetParent1VisibleProperties(true);
            }
        }
        private void SetParent1VisibleProperties(bool trueOrFalse)
        {
            ParentFNLabel.Visible = trueOrFalse;
            ParentFNID.Visible = trueOrFalse;
            ParentLNLabel.Visible = trueOrFalse;
            ParentLNID.Visible = trueOrFalse;
            ParentDOB.Visible = trueOrFalse;
            ParentDOBLabel.Visible = trueOrFalse;
            ParentsPhoneLabel.Visible = trueOrFalse;
            ParentsPhoneID.Visible = trueOrFalse;
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
        private void HideInfo(bool trueOrFalse)
        {
            FirstNameID.Visible = trueOrFalse;
            LastNameID.Visible = trueOrFalse;
            BirthID.Visible = trueOrFalse;
            PhoneID.Visible = trueOrFalse;
            AddressList.Visible = trueOrFalse;

            firstNameLabelId.Visible = trueOrFalse;
            lastNameLabelId.Visible = trueOrFalse;
            dobLabelID.Visible = trueOrFalse;
            phoneLabelID.Visible = trueOrFalse;
            AddressHeaderLabel.Visible = trueOrFalse;
            addressLabelID.Visible = trueOrFalse;
        }

        private void LoadItems()
        {
            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            string select = "SELECT ID, Street + ', ' + Suburb + ', ' + City + ', ' + CONVERT(VARCHAR(12),Postcode) AS FullAddress FROM Address";
            essentials.BindList(select, "FullAddress", "ID", AddressList, sqlConnection);
        }
        private void ProcessExistingPerson()
        {
            int addressID = 0;
            int personID = Convert.ToInt32(PersonDDL.SelectedValue);
            sqlConnection.Open();
            SqlCommand cm = new SqlCommand("INSERT INTO Student VALUES(@PersonID, @OpenDivison)", sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
                {
                    new SqlParameter("@PersonID", SqlDbType.Int) {Value = personID},
                    new SqlParameter("@OpenDivison", SqlDbType.Bit) {Value = OpenDivision.SelectedValue},
                };
            cm.Parameters.AddRange(prm.ToArray());
            int code = cm.ExecuteNonQuery();

            sqlConnection.Close();

            string query = "SELECT * FROM PersonAddress WHERE PersonID = @PersonID";
            cm = new SqlCommand(query, sqlConnection);
            prm = new List<SqlParameter>()
            {
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = personID},
            };
            cm.Parameters.AddRange(prm.ToArray());
            sqlConnection.Open();
            SqlDataReader sdr = cm.ExecuteReader();
            sdr.Read();
            addressID = Convert.ToInt32(sdr["AddressID"]);
            sqlConnection.Close();
            if (ParentRRL.Text == "True")
            {
                SubmitParentData(personID, addressID);
            }
        }
        private void ProcessSubmittedData()
        {
            string[] items = new string[]
            {
              FirstNameID.Text, LastNameID.Text, BirthID.Text, PhoneID.Text, 
            };
            string[] items2 = new string[]
            {
              ParentFNID.Text, ParentLNID.Text, ParentsPhoneID.Text,
            };
            if (essentials.CheckforValid(items))
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

                sqlConnection.Open();
                cm = new SqlCommand("INSERT INTO Student VALUES(@PersonID, @OpenDivison)", sqlConnection);
                prm = new List<SqlParameter>()
                {
                    new SqlParameter("@PersonID", SqlDbType.Int) {Value = personID},
                    new SqlParameter("@OpenDivison", SqlDbType.Bit) {Value = OpenDivision.SelectedValue},
                };
                cm.Parameters.AddRange(prm.ToArray());
                int code = cm.ExecuteNonQuery();

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
                code = cm.ExecuteNonQuery();
                sqlConnection.Close();

                if (ParentRRL.Text == "True")
                {
                    SubmitParentData(personID, addressID);
                }
            }
            else
            {
                SuccessLabel.Text = "Your record is missing a few details";
            }
        }

        private void SubmitParentData(int studentID, int addressID)
        {
            int parentID = 0;
            SqlCommand cm = null;
            List<SqlParameter> prm = null;
            int code = 0;
            if (ExistingParentDDL.Text == "")
            {
                sqlConnection.Open();
                cm = new SqlCommand("INSERT INTO Person output INSERTED.ID VALUES(@ParentFirstName, @ParentLastName, @ParentDOB, @ParentPhone)", sqlConnection);
                prm = new List<SqlParameter>()
                {
                    new SqlParameter("@ParentFirstName", SqlDbType.VarChar) {Value = ParentFNID.Text},
                    new SqlParameter("@ParentLastName", SqlDbType.VarChar) {Value = ParentLNID.Text},
                    new SqlParameter("@ParentDOB", SqlDbType.Date) {Value = ParentDOB.Text},
                    new SqlParameter("@ParentPhone", SqlDbType.VarChar) {Value = ParentsPhoneID.Text},
                };
                cm.Parameters.AddRange(prm.ToArray());
                parentID = (int)cm.ExecuteScalar();
                sqlConnection.Close();

                sqlConnection.Open();
                cm = new SqlCommand("INSERT INTO PersonAddress VALUES(@ParentID, @AddressID)", sqlConnection);
                prm = new List<SqlParameter>()
                    {
                        new SqlParameter("@ParentID", SqlDbType.Int) {Value = parentID},
                        new SqlParameter("@AddressID", SqlDbType.Int) {Value = addressID},
                    };
                cm.Parameters.AddRange(prm.ToArray());
                code = cm.ExecuteNonQuery();
                sqlConnection.Close();
            }
            else
            {
                parentID = Convert.ToInt32(ExistingParentDDL.SelectedValue);
            }
            sqlConnection.Open();
            cm = new SqlCommand("INSERT INTO Parents VALUES(@StudentID, @ParentID)", sqlConnection);
            prm = new List<SqlParameter>()
            {
                new SqlParameter("@StudentID", SqlDbType.Int) {Value = studentID},
                new SqlParameter("@ParentID", SqlDbType.Int) {Value = parentID},
            };
            cm.Parameters.AddRange(prm.ToArray());
            code = cm.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}