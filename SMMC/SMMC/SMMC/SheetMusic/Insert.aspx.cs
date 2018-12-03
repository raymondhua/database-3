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
                InstrumentCBL.Visible = false;
                InstrumentDDL.Visible = false;
                InstrumentID.Visible = false;
                InstrumentsID.Visible = false;
                SubmitButton.Visible = false;
                string query = "SELECT * FROM Instruments";
                essentials.CheckBox(query, "Instrument", "Instrument", InstrumentCBL, sqlConnection);
                essentials.BindList(query, "Instrument", "Instrument", InstrumentDDL, sqlConnection);
            }
            else
            {

            }
        }
        protected void OrchestralRBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(OrchestralRBL.Text=="True")
            {
                HideSelection(true);
            }
            else
            {
                HideSelection(false);
            }
            SubmitButton.Visible = true;
        }
        protected void SumbitButton_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(CopiesAllowedID.Text) >= Convert.ToInt32(DistrubitedCopiesID.Text))
                {
                    SubmitSheetMusicData();
                    SuccessLabelID.Text = "Information added into database";
                }
                else
                {
                    SuccessLabelID.Text = "Information not inserted: Copies allowed is higher than distrubited";
                }
            }
            catch (SqlException ex)
            {
                SuccessLabelID.Text = "SQL error";
            }
            catch (Exception ex)
            {
                SuccessLabelID.Text = "Numeric values only allowed!";
            }
        }
        private void SubmitSheetMusicData()
        {
            sqlConnection.Open();

            string query = "INSERT INTO SheetMusic output INSERTED.ID VALUES (@Name, @CopiesAllowed, @Distrubited, @Orchestral)";
            SqlCommand cm = new SqlCommand(query, sqlConnection);

            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@Name", SqlDbType.VarChar) {Value = NameID.Text},
                new SqlParameter("@CopiesAllowed", SqlDbType.Int) {Value = CopiesAllowedID.Text},
                new SqlParameter("@Distrubited", SqlDbType.Int) {Value = DistrubitedCopiesID.Text},
                new SqlParameter("@Orchestral", SqlDbType.Bit) {Value = OrchestralRBL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());
            int smID = (int)cm.ExecuteScalar();
            sqlConnection.Close();
            if (OrchestralRBL.Text == "True")
            {
                InsertInstrumentsData(smID);
            }
            else
            {
                InsertInstrumentData(smID);
            }
        }
        private void InsertInstrumentsData(int sheetMusicID)
        {
            int count = 0;
            List<string> selected = InstrumentCBL.Items.Cast<ListItem>()
            .Where(li => li.Selected)
            .Select(li => li.Value)
            .ToList();
            string query = "INSERT INTO SheetMusicInstruments VALUES(@SheetMusicID, @InstrumentID)";
            sqlConnection.Open();
            foreach (string select in selected)
            {
                SqlCommand cm = new SqlCommand(query, sqlConnection);
                List<SqlParameter> prm = new List<SqlParameter>()
                {
                    new SqlParameter("@SheetMusicID", SqlDbType.Int) {Value = sheetMusicID},
                    new SqlParameter("@InstrumentID", SqlDbType.VarChar) {Value = select},
                };
                cm.Parameters.AddRange(prm.ToArray());

                int code = cm.ExecuteNonQuery();
                count++;
            }
            sqlConnection.Close();
        }
        private void InsertInstrumentData(int sheetMusicID)
        {
            string query = "INSERT INTO SheetMusicInstruments VALUES(@SheetMusicID, @InstrumentID)";
            sqlConnection.Open();
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@SheetMusicID", SqlDbType.Int) {Value = sheetMusicID},
                new SqlParameter("@InstrumentID", SqlDbType.VarChar) {Value = InstrumentDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());
            int code = cm.ExecuteNonQuery();
            sqlConnection.Close();
        }
        private void HideSelection(bool trueOrFalse)
        {
            InstrumentCBL.Visible = trueOrFalse;
            InstrumentsID.Visible = trueOrFalse;
            InstrumentID.Visible = !trueOrFalse;
            InstrumentDDL.Visible = !trueOrFalse;
        }
        
    }
}