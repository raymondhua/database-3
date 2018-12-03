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

namespace SMMC.Performances
{
    public partial class View : System.Web.UI.Page
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
                NameLabel.Text = "View A Performance";
                string select = "SELECT p.ID, Name + ' - ' + CONVERT(VARCHAR(12),p.Date) + ' - ' + CONVERT(VARCHAR(12),p.Time) AS PerformanceInfo FROM Performances p INNER JOIN Venues v ON p.VenueID=v.ID";
                essentials.BindList(select, "PerformanceInfo", "ID", PerformanceDDL, sqlConnection);
            }
            else
            {

            }
        }
        protected void SelectPerformanceButton_Click(object sender, EventArgs e)
        {
            LoadVenueInfo();
            LoadStudentsInfo();
        }
        private void LoadVenueInfo()
        {
            string query = "SELECT Name as VenueName, v.Street + ', ' + v.Suburb + ', ' + v.City + ', ' + v.Postcode as Address, v.Phone FROM Performances p INNER JOIN Venues v ON p.VenueID=v.ID WHERE p.ID=@PerformanceID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PerformanceID", SqlDbType.Int) {Value = PerformanceDDL.Text},
            };
            cm.Parameters.AddRange(prm.ToArray());
            sqlConnection.Open();
            SqlDataReader sdr = cm.ExecuteReader();
            sdr.Read();
            NameID.Text = "Name: "; NameOutputID.Text = sdr["VenueName"].ToString();
            PhoneID.Text = "Phone: "; PhoneOutputID.Text = sdr["Phone"].ToString();
            AddressID.Text = "Address: "; AddressOutputID.Text = sdr["Address"].ToString();
            sqlConnection.Close();
        }
        private void LoadStudentsInfo()
        {
            dataAdapter = new SqlDataAdapter();
            studentDataSet = new DataSet();

            string query = "SELECT pe.FirstName + ' ' + pe.LastName AS FullName, si.InstrumentID FROM PerformancesStudent ps INNER JOIN Performances p ON ps.PerformancesID = p.ID INNER JOIN StudentInstrument si ON ps.StudentID = si.StudentID INNER JOIN Student st ON si.StudentID = st.PersonID INNER JOIN Person pe ON si.StudentID = pe.ID WHERE si.InstrumentID=ps.InstrumentID AND p.ID=@PerformanceID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PerformanceID", SqlDbType.Int) {Value = PerformanceDDL.Text},
            };
            cm.Parameters.AddRange(prm.ToArray());
            dataAdapter.SelectCommand = cm;

            sqlConnection.Open();
            dataAdapter.Fill(studentDataSet, "PerformancesStudent");
            sqlConnection.Close();

            DataTable tutorTable = studentDataSet.Tables["PerformancesStudent"];

            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Instrument");

            foreach (DataRow currentRecord in tutorTable.Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["Name"] = currentRecord["FullName"];
                dr1["Instrument"] = currentRecord["InstrumentID"];
                dt.Rows.Add(dr1);
            }

            PerformancesStudentGridView.DataSource = dt;
            PerformancesStudentGridView.DataBind();

            sqlConnection.Close();
        }
    }
}