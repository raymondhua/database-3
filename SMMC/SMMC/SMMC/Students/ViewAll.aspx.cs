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
    public partial class ViewAll : System.Web.UI.Page
    {

        SqlConnection sqlConnection;
        SqlDataAdapter dataAdapter;
        DataSet studentDataSet;
        protected void Page_Load(object sender, EventArgs e)
        {

            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            if (!Page.IsPostBack)
            {
                LoadAllStudentData();
                LoadStudentInstrumentData();
            }
        }
        private void LoadAllStudentData()
        {
            dataAdapter = new SqlDataAdapter();
            studentDataSet = new DataSet();


            string query = "SELECT * FROM PersonAddress pa INNER JOIN  Student s ON pa.PersonID = s.PersonID INNER JOIN  Address a ON pa.AddressID = a.ID INNER JOIN  Person p ON pa.PersonID = p.ID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);

            dataAdapter.SelectCommand = cm;

            sqlConnection.Open();
            dataAdapter.Fill(studentDataSet, "Student");
            sqlConnection.Close();

            DataTable studentTable = studentDataSet.Tables["Student"];

            DataTable dt = new DataTable();
            dt.Columns.Add("FirstName");
            dt.Columns.Add("LastName");
            dt.Columns.Add("DateOfBirth");
            dt.Columns.Add("PhoneNo");
            dt.Columns.Add("Street");
            dt.Columns.Add("Suburb");
            dt.Columns.Add("City");
            dt.Columns.Add("Postcode");

            foreach (DataRow currentRecord in studentTable.Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["FirstName"] = currentRecord["FirstName"];
                dr1["LastName"] = currentRecord["LastName"];
                dr1["DateOfBirth"] = DateTime.Parse((currentRecord["DateOfBirth"].ToString())).ToString("dd/MM/yyyy") ;
                dr1["PhoneNo"] = currentRecord["PhoneNo"];
                dr1["Street"] = currentRecord["Street"];
                dr1["Suburb"] = currentRecord["Suburb"];
                dr1["City"] = currentRecord["City"];
                dr1["Postcode"] = currentRecord["Postcode"];
                dt.Rows.Add(dr1);
            }

            AllStudentsGridView.DataSource = dt;
            AllStudentsGridView.DataBind();

        }

        private void LoadStudentInstrumentData()
        {
            dataAdapter = new SqlDataAdapter();
            studentDataSet = new DataSet();


            string query = "SELECT p.FirstName, p.LastName, CASE WHEN s.OpenDivision = 0 THEN 'No' ELSE 'Yes' END as OpenDivision, i.Instrument, CASE WHEN si.Hire = 0 THEN 'No' ELSE 'Yes' END as Hire, CASE WHEN s.OpenDivision = 0 THEN i.StudentFee ELSE i.OpenFee END as StudentFee, CASE WHEN si.Hire = 0 THEN 0 ELSE i.HireFee END as HireFee, c1.CertificationLevel, CASE WHEN c1.ATCL = 0 THEN 'No' ELSE 'Yes' END as ATCL, e.TypeID FROM StudentInstrument si INNER JOIN  Person p ON si.StudentID = p.ID INNER JOIN  Student s ON p.ID = s.PersonID INNER JOIN  Instruments i ON si.InstrumentID = i.Instrument INNER JOIN Certifications c1 ON i.Instrument = c1.InstrumentID INNER JOIN Certifications c2 ON p.ID = c2.PersonID INNER JOIN Ensembles e ON c1.CertificationLevel = e.Level WHERE c1.CertificationLevel = c2.CertificationLevel AND c1.InstrumentID=c2.InstrumentID";
            SqlCommand cm = new SqlCommand(query, sqlConnection);

            dataAdapter.SelectCommand = cm;

            sqlConnection.Open();
            dataAdapter.Fill(studentDataSet, "StudentInstrument");
            sqlConnection.Close();

            DataTable studentTable = studentDataSet.Tables["StudentInstrument"];

            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("OpenDivision");
            dt.Columns.Add("Instrument");
            dt.Columns.Add("Hire");
            dt.Columns.Add("StudentFee");
            dt.Columns.Add("HireFee");
            dt.Columns.Add("CertificationLevel");
            dt.Columns.Add("ATCL");
            dt.Columns.Add("Type");

            foreach (DataRow currentRecord in studentTable.Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["Name"] = currentRecord["FirstName"] + " " + currentRecord["LastName"];
                dr1["OpenDivision"] = currentRecord["OpenDivision"];
                dr1["Instrument"] = currentRecord["Instrument"];
                dr1["Hire"] = currentRecord["Hire"];
                dr1["StudentFee"] = currentRecord["StudentFee"];
                dr1["HireFee"] = currentRecord["HireFee"];
                dr1["CertificationLevel"] = currentRecord["CertificationLevel"];
                dr1["ATCL"] = currentRecord["ATCL"];
                dr1["Type"] = currentRecord["TypeID"];
                dt.Rows.Add(dr1);
            }

            StudentInstrumentsGridView.DataSource = dt;
            StudentInstrumentsGridView.DataBind();

        }
    }
}