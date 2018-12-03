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

namespace SMMC.Tutors
{
    public partial class Pay : System.Web.UI.Page
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
                string select = "SELECT DISTINCT p.ID, p.FirstName + ' ' + p.LastName AS Fullname FROM Tutors t INNER JOIN Person p ON t.PersonID = p.ID";
                essentials.BindList(select, "Fullname", "ID", TutorDDL, sqlConnection);
                LoadTutorTypeData();
            }
        }
        protected void TutorDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TutorDDL.SelectedValue != "")
            {
                string query = "SELECT p.FirstName, p.LastName, p.PhoneNo, SUM(cast(round(tt.HourlyRate*0.5,2) as numeric(36,2))) AS TotalPayPerWeek, SUM(cast(round(0.5,2) as numeric(36,2))) AS HoursWorked FROM Lessons l INNER JOIN LessonTutors lt ON l.ID = lt.LessonID INNER JOIN Tutors t ON lt.TutorID = t.ID INNER JOIN Person p ON t.PersonID = p.ID INNER JOIN TutorType tt ON t.Type = tt.ID WHERE l.InstrumentID = t.InstrumentID AND p.ID=@TutorID GROUP BY p.ID, p.FirstName, p.LastName, p.PhoneNo";
                List<SqlParameter> prm = new List<SqlParameter>()
                {
                    new SqlParameter("@TutorID", SqlDbType.Int) {Value = TutorDDL.Text},
                };
                LoadTutorTypeData(query, prm);
            }
            else
            {
                LoadTutorTypeData();
            }
        }
        private void LoadTutorTypeData(string query= "SELECT p.FirstName, p.LastName, p.PhoneNo, SUM(cast(round(tt.HourlyRate*0.5,2) as numeric(36,2))) AS TotalPayPerWeek, SUM(cast(round(0.5,2) as numeric(36,2))) AS HoursWorked FROM Lessons l INNER JOIN LessonTutors lt ON l.ID = lt.LessonID INNER JOIN Tutors t ON lt.TutorID = t.ID INNER JOIN Person p ON t.PersonID = p.ID INNER JOIN TutorType tt ON t.Type = tt.ID WHERE l.InstrumentID = t.InstrumentID GROUP BY p.ID, p.FirstName, p.LastName, p.PhoneNo", List<SqlParameter> prm=null)
        {
            dataAdapter = new SqlDataAdapter();
            studentDataSet = new DataSet();

            SqlCommand cm = new SqlCommand(query, sqlConnection);
            if (prm!=null)
            {
                cm.Parameters.AddRange(prm.ToArray());
            }
            dataAdapter.SelectCommand = cm;

            sqlConnection.Open();
            dataAdapter.Fill(studentDataSet, "Lessons");
            sqlConnection.Close();

            DataTable tutorTable = studentDataSet.Tables["Lessons"];

            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Phone");
            dt.Columns.Add("TotalPay");
            dt.Columns.Add("HoursWorked");

            foreach (DataRow currentRecord in tutorTable.Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["Name"] = currentRecord["FirstName"] + " " + currentRecord["LastName"];
                dr1["Phone"] = currentRecord["PhoneNo"];
                dr1["TotalPay"] = currentRecord["TotalPayPerWeek"];
                dr1["HoursWorked"] = currentRecord["HoursWorked"];
                dt.Rows.Add(dr1);
            }

            PayGridView.DataSource = dt;
            PayGridView.DataBind();

            sqlConnection.Close();
        }
    }
}