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

namespace SMMC.Instruments
{
    public partial class Update : System.Web.UI.Page
    {
        Essentials essentials;
        SqlConnection sqlConnection;
        protected void Page_Load(object sender, EventArgs e)
        {
            essentials = new Essentials();
            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            string select = "SELECT Instrument from Instruments";
            essentials.BindList(select, "Instrument", "Instrument", InstrumentDDL, sqlConnection);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateInstrument();
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

        protected void InstrumentDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStudentDetails();
        }
        private void LoadStudentDetails()
        {
            string query = "SELECT * from Instruments WHERE Instrument=@Instrument";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@Instrument", SqlDbType.VarChar) {Value = InstrumentDDL.SelectedValue},
            };
            cm.Parameters.AddRange(prm.ToArray());
            sqlConnection.Open();
            SqlDataReader sdr = cm.ExecuteReader();
            sdr.Read();
            HireFeeID.Text = sdr["HireFee"].ToString();
            OpenFeeID.Text = sdr["OpenFee"].ToString();
            CommentsID.Text = sdr["Comments"].ToString();
            StudentFeeID.Text = sdr["StudentFee"].ToString();
            sqlConnection.Close();

        }
        private void UpdateInstrument()
        {
            sqlConnection.Open();
            string query = "UPDATE Instruments SET StudentFee = @StudentFee, OpenFee = @OpenFee, HireFee = @HireFee, Comments = @Comments WHERE Instrument = @Instrument";
            SqlCommand cm = new SqlCommand(query, sqlConnection);

            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@Instrument", SqlDbType.VarChar) {Value = InstrumentDDL.SelectedValue},
                new SqlParameter("@StudentFee", StudentFeeID.Text == "" ? DBNull.Value : (object)StudentFeeID.Text),
                new SqlParameter("@OpenFee", OpenFeeID.Text == "" ? DBNull.Value : (object)OpenFeeID.Text),
                new SqlParameter("@HireFee", HireFeeID.Text == "" ? DBNull.Value : (object)HireFeeID.Text),
                new SqlParameter("@Comments", CommentsID.Text == "" ? DBNull.Value : (object)CommentsID.Text),
            };
            cm.Parameters.AddRange(prm.ToArray());

            int code = cm.ExecuteNonQuery();
            sqlConnection.Close();
            SuccessLabel.Text = "Your record has been updated!";
        }

    }
}