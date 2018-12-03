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

namespace SMMC.Instruments
{
    public partial class View : System.Web.UI.Page
    {
        SqlConnection sqlConnection;
        SqlDataAdapter dataAdapter;
        DataSet instrumentDataSet;
        protected void Page_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }
        private void LoadData()
        {
            dataAdapter = new SqlDataAdapter();
            instrumentDataSet = new DataSet();
            

            string query = "SELECT * FROM Instruments";
            SqlCommand cm = new SqlCommand(query, sqlConnection);

            dataAdapter.SelectCommand = cm;

            sqlConnection.Open();
            dataAdapter.Fill(instrumentDataSet, "Instruments");


            DataTable actorTable = instrumentDataSet.Tables["Instruments"];

            DataTable dt = new DataTable();
            dt.Columns.Add("Instrument");
            dt.Columns.Add("StudentFee");
            dt.Columns.Add("OpenFee");
            dt.Columns.Add("HireFee");
            dt.Columns.Add("Comments");

            foreach (DataRow currentRecord in actorTable.Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["Instrument"] = currentRecord["Instrument"];
                dr1["StudentFee"] = CheckIfNull(currentRecord["StudentFee"]);
                dr1["OpenFee"] = CheckIfNull(currentRecord["OpenFee"]);
                dr1["HireFee"] = CheckIfNull(currentRecord["HireFee"]);
                dr1["Comments"] = currentRecord["Comments"];
                dt.Rows.Add(dr1);
            }

            InstrumentGridView.DataSource = dt;
            InstrumentGridView.DataBind();
        }
        private object CheckIfNull(object output)
        {
            if (output.ToString() == "")
            {
                return "N/A";
            }
            else
            {
                return output;
            }
        }
    }
}