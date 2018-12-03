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
                NameLabel.Text = "View A Student";
                string select = "SELECT ID, FirstName + ' ' + LastName AS Fullname FROM Person p INNER JOIN Student s ON p.ID=s.PersonID";
                essentials.BindList(select, "Fullname", "ID", PersonDDL, sqlConnection);
            }
        }
        protected void SelectPersonButton_Click(object sender, EventArgs e)
        {
            LoadStudentData();
            LoadStudentInstrumentData();
            LoadPerformanceData();
            LoadPayments();
            LoadParentsData();
        }
        private void LoadStudentData()
        {
            string query = "SELECT p.FirstName, p.LastName, p.DateOfBirth, a.Street + ', ' + a.Suburb + ', ' + a.City + ', ' + CONVERT(VARCHAR(12),Postcode) as Address, p.PhoneNo, CASE WHEN s.OpenDivision = 0 THEN 'No' ELSE 'Yes' END as OpenDivision FROM PersonAddress pa INNER JOIN Student s ON pa.PersonID = s.PersonID INNER JOIN  Address a ON pa.AddressID = a.ID INNER JOIN  Person p ON pa.PersonID = p.ID WHERE s.PersonID = @PersonID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = PersonDDL.Text},
            };
            cm.Parameters.AddRange(prm.ToArray());
            sqlConnection.Open();
            SqlDataReader sdr = cm.ExecuteReader();
            sdr.Read();
            NameLabel.Text = sdr["FirstName"].ToString() + " " + sdr["LastName"].ToString();
            BirthdayID.Text = "Birthday: "; BirthdayOutputID.Text = DateTime.Parse((sdr["DateOfBirth"].ToString())).ToString("dd/MM/yyyy");
            PhoneID.Text = "Phone: "; PhoneOutputID.Text = sdr["PhoneNo"].ToString();
            AddressID.Text = "Address: "; AddressOutputID.Text = sdr["Address"].ToString();
            OpenDivID.Text = "Open division: "; OpenDivOutputID.Text = sdr["OpenDivision"].ToString();
            sqlConnection.Close();

        }
        private void LoadStudentInstrumentData()
        {
            dataAdapter = new SqlDataAdapter();
            studentDataSet = new DataSet();

            string query = "SELECT p.FirstName, p.LastName, CASE WHEN s.OpenDivision = 0 THEN 'No' ELSE 'Yes' END as OpenDivision, i.Instrument, CASE WHEN si.Hire = 0 THEN 'No' ELSE 'Yes' END as Hire, CASE WHEN s.OpenDivision = 0 THEN i.StudentFee ELSE i.OpenFee END as StudentFee, CASE WHEN si.Hire = 0 THEN 0 ELSE i.HireFee END as HireFee FROM StudentInstrument si INNER JOIN  Student s ON si.StudentID = s.PersonID INNER JOIN  Person p ON s.PersonID = p.ID INNER JOIN  Instruments i ON si.InstrumentID = i.Instrument WHERE s.PersonID = @PersonID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PersonID", SqlDbType.VarChar) {Value = PersonDDL.Text},
            };
            cm.Parameters.AddRange(prm.ToArray());
            dataAdapter.SelectCommand = cm;

            sqlConnection.Open();
            dataAdapter.Fill(studentDataSet, "StudentInstrument");
            sqlConnection.Close();

            DataTable studentTable = studentDataSet.Tables["StudentInstrument"];

            DataTable dt = new DataTable();
            dt.Columns.Add("Instrument");
            dt.Columns.Add("Hire");
            dt.Columns.Add("StudentFee");
            dt.Columns.Add("HireFee");

            decimal totalStudentFee = 0;
            decimal totalHireFee = 0;
            foreach (DataRow currentRecord in studentTable.Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["Instrument"] = currentRecord["Instrument"];
                dr1["Hire"] = currentRecord["Hire"];
                dr1["StudentFee"] = currentRecord["StudentFee"];
                dr1["HireFee"] = currentRecord["HireFee"]; ;
                dt.Rows.Add(dr1);
                if(currentRecord["StudentFee"] != null)
                {
                    totalStudentFee += Convert.ToDecimal(currentRecord["StudentFee"]);
                }
                if (currentRecord["HireFee"] != null)
                {
                    totalHireFee += Convert.ToDecimal(currentRecord["HireFee"]);
                }
            }

            bool visable = essentials.MoreThanZero(dt, InstrumentsLabel, "Instruments playing");

            StudentInstrumentsGridView.DataSource = dt;
            StudentInstrumentsGridView.DataBind();

            if (visable == true)
            {
                TotalStudentFee.Text = "Total student fee: $" + totalStudentFee;
                TotalHireFee.Text = "Total hire fee: $" + totalHireFee;
                TotalFee.Text = "Total fee: $" + (totalStudentFee + totalHireFee);
            }
            else
            {
                TotalStudentFee.Visible = false;
                TotalHireFee.Visible = false;
                TotalFee.Visible = false;
            }

            sqlConnection.Close();

        }

        private void LoadPayments()
        {
            dataAdapter = new SqlDataAdapter();
            studentDataSet = new DataSet();

            string query = "SELECT * FROM Payments b WHERE StudentID = @PersonID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = PersonDDL.Text},
            };
            cm.Parameters.AddRange(prm.ToArray());
            dataAdapter.SelectCommand = cm;

            sqlConnection.Open();
            dataAdapter.Fill(studentDataSet, "Payments");
            sqlConnection.Close();

            DataTable studentTable = studentDataSet.Tables["Payments"];

            DataTable dt = new DataTable();
            dt.Columns.Add("DatePaid");
            dt.Columns.Add("Amount");

            foreach (DataRow currentRecord in studentTable.Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["DatePaid"] = currentRecord["DatePaid"];
                dr1["Amount"] = currentRecord["Amount"];
                dt.Rows.Add(dr1);
            }

            essentials.MoreThanZero(dt, PaymentLabel, "Payment history");

            PaymentHistory.DataSource = dt;
            PaymentHistory.DataBind();

            sqlConnection.Close();

        }
        private void LoadParentsData()
        {
            dataAdapter = new SqlDataAdapter();
            studentDataSet = new DataSet();

            string query = "SELECT * FROM Parents pa INNER JOIN Person pe ON pa.ParentID = pe.ID WHERE pa.StudentID = @PersonID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = PersonDDL.Text},
            };
            cm.Parameters.AddRange(prm.ToArray());
            dataAdapter.SelectCommand = cm;

            sqlConnection.Open();
            dataAdapter.Fill(studentDataSet, "Parents");
            sqlConnection.Close();

            DataTable studentTable = studentDataSet.Tables["Parents"];

            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Phone");

            foreach (DataRow currentRecord in studentTable.Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["Name"] = currentRecord["FirstName"] + "  " + currentRecord["LastName"];
                dr1["Phone"] = currentRecord["PhoneNo"];
                dt.Rows.Add(dr1);
            }

            essentials.MoreThanZero(dt, ParentsLabel, "Parent information");

            ParentsGridView.DataSource = dt;
            ParentsGridView.DataBind();

            sqlConnection.Close();

        }

        private void LoadPerformanceData()
        {
            dataAdapter = new SqlDataAdapter();
            studentDataSet = new DataSet();

            string query = "SELECT CONVERT(date, p.Date) AS Date, p.Time, v.Name, si.InstrumentID FROM PerformancesStudent ps INNER JOIN Performances p ON ps.PerformancesID = p.ID INNER JOIN Venues v ON p.VenueID = v.ID INNER JOIN StudentInstrument si ON ps.StudentID = si.StudentID INNER JOIN Student st ON si.StudentID = st.PersonID INNER JOIN Person pe ON si.StudentID = pe.ID WHERE si.InstrumentID=ps.InstrumentID AND st.PersonID = @PersonID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@PersonID", SqlDbType.Int) {Value = PersonDDL.Text},
            };
            cm.Parameters.AddRange(prm.ToArray());
            dataAdapter.SelectCommand = cm;

            sqlConnection.Open();
            dataAdapter.Fill(studentDataSet, "PerformancesStudent");
            sqlConnection.Close();

            DataTable tutorTable = studentDataSet.Tables["PerformancesStudent"];

            DataTable dt = new DataTable();
            dt.Columns.Add("Date");
            dt.Columns.Add("Time");
            dt.Columns.Add("Venue");
            dt.Columns.Add("Instrument");

            foreach (DataRow currentRecord in tutorTable.Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["Date"] = DateTime.Parse((currentRecord["Date"].ToString())).ToString("dd/MM/yyyy");
                dr1["Time"] = currentRecord["Time"];
                dr1["Venue"] = currentRecord["Name"];
                dr1["Instrument"] = currentRecord["InstrumentID"];
                dt.Rows.Add(dr1);
            }

            essentials.MoreThanZero(dt, PerformancesLabel, "Performances");

            PerformancesGV.DataSource = dt;
            PerformancesGV.DataBind();

            sqlConnection.Close();
        }
    }
}