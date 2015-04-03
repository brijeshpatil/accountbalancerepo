using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SampleApp
{
    /// <summary>
    /// Summary description for SampleAppWeb
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SampleAppWeb : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        public string GetValue(string acno, string balance)
        {
            if (acno != "" && balance != "")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
                SqlDataAdapter da = new SqlDataAdapter("select bal from BalanceTbl where bid=@BID", con);
                da.SelectCommand.Parameters.AddWithValue("@BID", acno);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0 && Convert.ToDecimal(dt.Rows[0]["bal"].ToString()) > Convert.ToDecimal(balance))
                {

                    return "Transaction Completed...";
                }
                else
                {
                    return "Not Enough money in account";
                }
            }
            else
            {
                return "";
            }
        }

        [WebMethod]
        public string InsertBalance(string acno, string balance)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
            SqlCommand cmd = new SqlCommand("insert into BalanceTbl values(@acno,@bal)", con);
            cmd.Parameters.AddWithValue("@acno", acno);
            cmd.Parameters.AddWithValue("@bal", balance);
            con.Open();
            int x = cmd.ExecuteNonQuery();
            con.Close();
            if (x != 0)
            {
                return "Inserted Successfully..";
            }
            else
            {
                return "There should be some error..";
            }
        }

    }
}
