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

namespace SMMC.SheetMusic
{
    public partial class UpdateTutors : System.Web.UI.Page
    {
        SqlConnection sqlConnection;
        Essentials essentials;
        protected void Page_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            essentials = new Essentials();
            if (!Page.IsPostBack)
            {
                string query = "SELECT * FROM SheetMusic";
                essentials.BindList(query, "Name", "ID", SheetMusicDDL, sqlConnection);
            }
            else
            {

            }
        }
        protected void SheetMusicDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "SELECT t.ID, FirstName + ' ' + LastName + ' - ' + t.InstrumentID AS Name FROM Tutors t INNER JOIN Person p ON t.PersonID=p.ID WHERE InstrumentID IN (SELECT InstrumentID FROM SheetMusic sm INNER JOIN SheetMusicInstruments smi ON sm.ID=smi.SheetMusicID WHERE sm.ID=@SheetMusicID) AND t.ID IN (SELECT TutorID FROM SheetMusicTutors WHERE SheetMusicID = @SheetMusicID)";
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@SheetMusicID", SqlDbType.Int) {Value = SheetMusicDDL.SelectedValue},
            };
            essentials.BindList(query, "Name", "ID", TutorDDL, sqlConnection, prm);
            TutorDDL.Items.Insert(0, new ListItem("", ""));
        }
        protected void TutorDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
            LoadTutorData();
        }
        protected void SumbitButton_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(GivenCopiesID.Text) <= Convert.ToInt32(LoadData()))
                {
                    if (Convert.ToInt32(GivenCopiesID.Text) >= Convert.ToInt32(GivenToStudentsID.Text))
                    {
                        UpdateSheetMusic();
                        LoadTutorData();
                        LoadData();
                        SuccessLabelID.Text = "Information updated to the database";
                    }
                    else
                    {
                        SuccessLabelID.Text = "Information not updated: Copies given to students is higher than copies given";
                    }
                }
                else
                {
                    SuccessLabelID.Text = "Information not updated: Copies allowed is higher than distrubited";
                }
            }
            catch (SqlException ex)
            {
                SuccessLabelID.Text = "SQL error";
            }
            catch (Exception ex)
            {
                SuccessLabelID.Text = "Numeric values only valid in input!";
            }

        }
        private int LoadData()
        {
            int copies = 0;
            string query = "SELECT DistrubitedCopies-(SELECT GivenCopies FROM SheetMusic sm  INNER JOIN SheetMusicTutors smt ON sm.ID = smt.SheetMusicID WHERE sm.ID = @SheetMusicID GROUP BY GivenCopies) AS CopiesToBeReturned FROM SheetMusic sm WHERE sm.ID = @SheetMusicID GROUP BY DistrubitedCopies";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@SheetMusicID", SqlDbType.Int) {Value = SheetMusicDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());
            sqlConnection.Open();
            SqlDataReader sdr = cm.ExecuteReader();
            sdr.Read();

            string temp = sdr["CopiesToBeReturned"].ToString();
            if (temp != "")
            {
                copies = Convert.ToInt32(sdr["CopiesToBeReturned"]);
            }
            else
            {
                sqlConnection.Close();
                query = "SELECT DistrubitedCopies AS CopiesToBeReturned FROM SheetMusic sm WHERE sm.ID = @SheetMusicID GROUP BY DistrubitedCopies";
                cm = new SqlCommand(query, sqlConnection);
                prm = new List<SqlParameter>()
                {
                new SqlParameter("@SheetMusicID", SqlDbType.Int) {Value = SheetMusicDDL.SelectedValue},
                };
                cm.Parameters.AddRange(prm.ToArray());
                sqlConnection.Open();
                sdr = cm.ExecuteReader();
                sdr.Read();
                copies = Convert.ToInt32(sdr["CopiesToBeReturned"]);
            }
            DistrubitedCopies.Text = copies.ToString() + " copies remaining";
            sqlConnection.Close();
            return copies;
        }
        private void LoadTutorData()
        {
            string query = "SELECT * FROM SheetMusicTutors WHERE SheetMusicID=@SheetMusicID AND TutorID=@TutorID";
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@SheetMusicID", SqlDbType.Int) {Value = SheetMusicDDL.SelectedValue},
                new SqlParameter("@TutorID", SqlDbType.Int) {Value = TutorDDL.SelectedValue},
            };
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            cm.Parameters.AddRange(prm.ToArray());
            sqlConnection.Open();
            SqlDataReader sdr = cm.ExecuteReader();
            sdr.Read();
            GivenCopiesID.Text = sdr["GivenCopies"].ToString();
            GivenToStudentsID.Text = sdr["GivenToStudents"].ToString();
            sqlConnection.Close();
        }
        private void UpdateSheetMusic()
        {
            try
            {
                sqlConnection.Open();
                string query = "UPDATE SheetMusicTutors SET GivenCopies=@GivenCopies, GivenToStudents=@GivenToStudents WHERE SheetMusicID=@SheetMusicID AND TutorID=@TutorID";
                SqlCommand cm = new SqlCommand(query, sqlConnection);
                List<SqlParameter> prm = new List<SqlParameter>()
                {
                    new SqlParameter("@SheetMusicID", SqlDbType.Int) {Value = SheetMusicDDL.SelectedValue},
                    new SqlParameter("@TutorID", SqlDbType.Int) {Value = TutorDDL.SelectedValue},
                    new SqlParameter("@GivenCopies", SqlDbType.Int) {Value = GivenCopiesID.Text},
                    new SqlParameter("@GivenToStudents", SqlDbType.Int) {Value = GivenToStudentsID.Text},
                };
                cm.Parameters.AddRange(prm.ToArray());
                int code = cm.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (SqlException ex)
            {
                SuccessLabelID.Text = "SQL error";
            }
            catch (Exception ex)
            {
                SuccessLabelID.Text = "There was another issue!";
            }

        }
    }
}