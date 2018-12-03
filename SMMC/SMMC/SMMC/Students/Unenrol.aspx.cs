﻿using System;
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
    public partial class Unenrol : System.Web.UI.Page
    {
        SqlConnection sqlConnection;
        Essentials essentials;
        protected void Page_Load(object sender, EventArgs e)
        {
            essentials = new Essentials();
            sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                UnenrolStudents();
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
        private void LoadData()
        {
            string select = "SELECT ID, FirstName + ' ' + LastName AS Fullname FROM Person";
            essentials.CheckBox(select, "Fullname", "ID", ActorCheckBox, sqlConnection);
        }
        private void UnenrolStudents()
        {
            int count = 0;
            List<string> selected = ActorCheckBox.Items.Cast<ListItem>()
            .Where(li => li.Selected)
            .Select(li => li.Value)
            .ToList();
            string query = "DELETE FROM Person WHERE ID = @ID";
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
            SuccessLabel.Text = count.ToString() + " students(s) have been deleted";
            LoadData();
        }
    }
}