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
    public partial class Update : System.Web.UI.Page
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
                HideOrShow(false);
            }
            else
            {

            }
        }
        protected void SheetMusicDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            HideOrShow(true);
            string query = "SELECT * FROM SheetMusic WHERE ID = @smID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@smID", SqlDbType.Int) {Value = SheetMusicDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());
            sqlConnection.Open();
            SqlDataReader sdr = cm.ExecuteReader();
            sdr.Read();
            CopiesAllowedID.Text = sdr["CopiesAllowed"].ToString();
            DistrubitedCopiesID.Text = sdr["DistrubitedCopies"].ToString();
            sqlConnection.Close();
        }
        protected void SumbitButton_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(CopiesAllowedID.Text) >= Convert.ToInt32(DistrubitedCopiesID.Text))
                {
                    UpdateSheetMusic();
                    SuccessLabelID.Text = "Information updated into database";
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
                SuccessLabelID.Text = "Something went wrong!";
            }
        }
        private void UpdateSheetMusic()
        {
            sqlConnection.Open();
            string query = "UPDATE SheetMusic SET CopiesAllowed = @CopiesAllowed, DistrubitedCopies = @DistrubitedCopies WHERE ID = @SheetMusicID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@CopiesAllowed", SqlDbType.Int) {Value = CopiesAllowedID.Text},
                new SqlParameter("@DistrubitedCopies", SqlDbType.Int) {Value = DistrubitedCopiesID.Text},
                new SqlParameter("@SheetMusicID", SqlDbType.Int) {Value = SheetMusicDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());
            int code = cm.ExecuteNonQuery();
            sqlConnection.Close();
        }
        private void HideOrShow(bool trueOrFalse)
        {
            CopiesAllowedLabelID.Visible = trueOrFalse;
            DistrubitedLabelID.Visible = trueOrFalse;
            CopiesAllowedID.Visible = trueOrFalse;
            DistrubitedCopiesID.Visible = trueOrFalse;
            SubmitButton.Visible = trueOrFalse;
        }
    }
}