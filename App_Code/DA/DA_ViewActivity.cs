using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for DA_ViewActivity
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DA_ViewActivity
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        private SqlCommand cmd;

        public enum Activity
        {
            GetActivityDate = 1, GetAllActivityByDate = 2
        };

        public DA_ViewActivity()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable GetDatatable(DO_ViewActivity objActivity, Activity flag)
        {
            DataTable dt = new DataTable();
            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_ViewAllActivity", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objActivity.RegistrationId;
            //da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = objNetwork.CurrentPageSize;
            //da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = objNetwork.CurrentPage;
            da.SelectCommand.Parameters.Add("@ActivityDate", SqlDbType.VarChar, 20).Value = objActivity.ActivityDate;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public void AddEditDel_BlogHeadingLikeShareTbl(DO_BlogLikeShare objblog, DA_BlogLikeShare.BlogHeadingShareLikeFlag flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("SP_BlogHeadingLikeShareTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intBlogHeadingLikeId", SqlDbType.BigInt).Value = objblog.intBlogHeadingLikeId;
            cmd.Parameters.Add("@strRepLiShStatus", SqlDbType.VarChar, 2).Value = objblog.strRepLiShStatus;
            cmd.Parameters.Add("@intBlogId", SqlDbType.Int).Value = objblog.intBlogId;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objblog.intAddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = objblog.strIpAddress;
            cmd.Parameters.Add("@strInviteeShare", SqlDbType.VarChar, 20).Value = objblog.strInviteeShare;
            cmd.Parameters.Add("@strLink", SqlDbType.VarChar, 200).Value = objblog.strLink;
            cmd.Parameters.Add("@strMessage", SqlDbType.VarChar, 200).Value = objblog.strMessage;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.VarChar, 200).Value = objblog.intAddedBy;
            cmd.Parameters.Add("@strBlogTitle", SqlDbType.VarChar, 20000).Value = objblog.strBlogTitle;
            //cmd.ExecuteNonQuery();
            objblog.intBlogHeadingLikeId = Convert.ToInt32(cmd.ExecuteScalar());

            co.CloseConnection(conn);
        }

    }
}