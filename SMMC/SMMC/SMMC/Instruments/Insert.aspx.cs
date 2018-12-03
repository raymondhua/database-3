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
        }
        protected void ButtonId_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessSubmittedData();
            }
            catch (SqlException ex)
            {
                SuccessLabel.Text = "SQL error";
            }
            catch (Exception ex)
            {
                SuccessLabel.Text = "Something went wrong - check fee values!";
            }
        }
        private void LoadItems()
        {
        }
        private void ProcessSubmittedData()
        {
            string[] items = new string[]
            {
              InstrumentID.Text, StudentFeeID.Text, OpenFeeID.Text, HireFeeID.Text
            };
            if (essentials.CheckforValid(items))
            {
                sqlConnection.Open();
                string query = "INSERT INTO Instruments VALUES (@Instrument, @StudentFee, @OpenFee, @HireFee, @Comments)";
                SqlCommand cm = new SqlCommand(query, sqlConnection);

                List<SqlParameter> prm = new List<SqlParameter>()
                {
                    new SqlParameter("@Instrument", SqlDbType.VarChar) {Value = InstrumentID.Text},
                    new SqlParameter("@StudentFee", SqlDbType.Decimal) {Value = StudentFeeID.Text},
                    new SqlParameter("@OpenFee", SqlDbType.Decimal) {Value = OpenFeeID.Text},
                    new SqlParameter("@HireFee", SqlDbType.Decimal) {Value = HireFeeID.Text},
                    new SqlParameter("@Comments", CommentsID.Text == "" ? DBNull.Value : (object)CommentsID.Text),
                };
                cm.Parameters.AddRange(prm.ToArray());

                int code = cm.ExecuteNonQuery();

                SuccessLabel.Text = "Your record has been saved with the following details!";

                sqlConnection.Close();
            }
            else
            {
                SuccessLabel.Text = "Your record is missing a few details";
            }
        }
    }
}