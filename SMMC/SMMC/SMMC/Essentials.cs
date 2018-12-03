using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SMMC
{
    public class Essentials
    {
        public Essentials()
        {

        }

        public bool CheckforValid(String[] stringArray)
        {
            bool check = true;
            foreach (string element in stringArray)
            {
                if (element == "")
                {
                    check = false;
                }
            }
            return check;
        }
        public void BindList(string query, string textField, string dataField, DropDownList ddList, SqlConnection sqlConnection)
        {
            sqlConnection.Open();

            SqlCommand scmselect = new SqlCommand(query, sqlConnection);
            SqlDataReader sdrselect = scmselect.ExecuteReader();

            ddList.DataTextField = textField;
            ddList.DataValueField = dataField;
            ddList.DataSource = sdrselect;

            ddList.DataBind();

            sqlConnection.Close();
        }
        public void BindList(string query, string textField, string dataField, DropDownList ddList, SqlConnection sqlConnection, List<SqlParameter> prm)
        {
            sqlConnection.Open();

            SqlCommand scmselect = new SqlCommand(query, sqlConnection);
            scmselect.Parameters.AddRange(prm.ToArray());
            SqlDataReader sdrselect = scmselect.ExecuteReader();
            ddList.DataTextField = textField;
            ddList.DataValueField = dataField;
            ddList.DataSource = sdrselect;

            ddList.DataBind();

            sqlConnection.Close();
        }
        public void CheckBox(string query, string textField, string dataField, CheckBoxList ddList, SqlConnection sqlConnection)
        {
            sqlConnection.Open();

            SqlCommand scmselect = new SqlCommand(query, sqlConnection);
            SqlDataReader sdrselect = scmselect.ExecuteReader();

            ddList.DataTextField = textField;
            ddList.DataValueField = dataField;
            ddList.DataSource = sdrselect;

            ddList.DataBind();

            sqlConnection.Close();
        }

        public void CheckBox(string query, string textField, string dataField, CheckBoxList ddList, SqlConnection sqlConnection, List<SqlParameter> prm)
        {
            sqlConnection.Open();

            SqlCommand scmselect = new SqlCommand(query, sqlConnection);
            scmselect.Parameters.AddRange(prm.ToArray());
            SqlDataReader sdrselect = scmselect.ExecuteReader();

            ddList.DataTextField = textField;
            ddList.DataValueField = dataField;
            ddList.DataSource = sdrselect;

            ddList.DataBind();

            sqlConnection.Close();
        }

        public void RadioButtonBind(string query, string textField, string dataField, RadioButtonList ddList, SqlConnection sqlConnection)
        {
            sqlConnection.Open();

            SqlCommand scmselect = new SqlCommand(query, sqlConnection);
            SqlDataReader sdrselect = scmselect.ExecuteReader();

            ddList.DataTextField = textField;
            ddList.DataValueField = dataField;
            ddList.DataSource = sdrselect;

            ddList.DataBind();

            sqlConnection.Close();
        }
        public bool MoreThanZero(DataTable dataTable, Label label, string headerString)
        {
            if (dataTable.Rows.Count > 0)
            {
                label.Visible = true;
                label.Text = headerString;
                return true;
            }
            else
            {
                label.Visible = false;
                return false;
            }
        }
    }

}