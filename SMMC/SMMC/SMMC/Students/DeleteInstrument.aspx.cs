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

namespace SMMC.Students
{
    public partial class DeleteInstrument : System.Web.UI.Page
    {
        SqlConnection sqlConnection;
        Essentials essentials;
        protected void Page_Load(object sender, EventArgs e)
        {
            essentials = new Essentials();
            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            if (!Page.IsPostBack)
            {
                LoadStudents();
                DeleteInstrumentsButton.Visible = false;
            }
        }
        protected void SelectPersonButton_Click(object sender, EventArgs e)
        {
            DeleteInstrumentsButton.Visible = true;
            LoadInstruments();
        }
        protected void DeleteInstrumentsButton_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteItems();
                LoadInstruments();
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
        private void LoadStudents()
        {
            string select = "SELECT ID, FirstName + ' ' + LastName AS Fullname FROM Person";
            essentials.BindList(select, "Fullname", "ID", PersonDDL, sqlConnection);
        }
        private void LoadInstruments()
        {
            string select = "SELECT ID, i.Instrument FROM StudentInstrument si INNER JOIN  Student s ON si.StudentID = s.PersonID INNER JOIN  Instruments i ON si.InstrumentID = i.Instrument WHERE s.PersonID = " + PersonDDL.SelectedValue;
            essentials.CheckBox(select, "Instrument", "ID", ActorCheckBox, sqlConnection);
        }
        private void DeleteItems()
        {
            int count = 0;
            List<string> selected = ActorCheckBox.Items.Cast<ListItem>()
            .Where(li => li.Selected)
            .Select(li => li.Value)
            .ToList();
            string query = "DELETE FROM StudentInstrument WHERE ID = @ID";
            sqlConnection.Open();
            foreach (string select in selected)
            {
                SqlCommand cm = new SqlCommand(query, sqlConnection);
                List<SqlParameter> prm = new List<SqlParameter>()
                {
                    new SqlParameter("@ID", SqlDbType.Int) {Value = select},
                };
                cm.Parameters.AddRange(prm.ToArray());

                int code = cm.ExecuteNonQuery();
                count++;
            }
            sqlConnection.Close();
            SuccessLabel.Text = count.ToString() + " instruments(s) have been deleted";
        }
    }
}