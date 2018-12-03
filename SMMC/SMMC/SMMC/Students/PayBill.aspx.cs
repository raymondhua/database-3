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

namespace SMMC.Students
{
    public partial class PayBill : System.Web.UI.Page
    {
        Essentials essentials;
        SqlConnection sqlConnection;
        SqlDataAdapter dataAdapter;
        DataSet studentDataSet;
        protected void Page_Load(object sender, EventArgs e)
        {
            essentials = new Essentials();
            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            if (!Page.IsPostBack)
            {
                string select = "SELECT DISTINCT ID, p.FirstName + ' ' + p.LastName AS Fullname FROM Student s INNER JOIN Person p ON s.PersonID = p.ID";
                essentials.BindList(select, "Fullname", "ID", StudentDDL, sqlConnection);
            }
            else
            {

            }


        }
        protected void StudentDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void SumbitButton_OnClick(object sender, EventArgs e)
        {
            InsertPayData();
            SuccessLabelID.Text = "Information added into database";
        }
        private void InsertPayData()
        {
            string query = "INSERT INTO Payments VALUES(@StudentID, @DatePaid, @Amount)";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@StudentID", SqlDbType.Int) {Value = StudentDDL.SelectedValue},
                new SqlParameter("@Amount", SqlDbType.VarChar) {Value = AmountID.Text},
                new SqlParameter("@DatePaid", SqlDbType.VarChar) {Value = DatePaidID.Text},
            };
            sqlConnection.Open();
            cm.Parameters.AddRange(prm.ToArray());
            int code = cm.ExecuteNonQuery();

            sqlConnection.Close();
        }

    }
}