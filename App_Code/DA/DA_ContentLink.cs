using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_CaseInsert
/// </summary>
/// 
namespace DA_SKORKEL
{

    public class DA_ContentLink
    {
        public DA_ContentLink()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum ContentLink
        {
            Add = 1, GetCaseComment = 2, AddDocLink = 3, GetUserWiseLink = 4, DeleteLinkUser = 5, GetUserWiseDocsLink = 6, DeleteDocsLinkUser = 7, AddAPIContentLink = 8, AddAPIDocLink = 9
        };


        SqlCommand cmd = new SqlCommand();

        public void AddEditDel_Case(DO_ContentLink objLink, DA_ContentLink.ContentLink flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelLink", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@LinkId", SqlDbType.BigInt).Value = objLink.LinkId;
            cmd.Parameters.Add("@ContentId", SqlDbType.BigInt).Value = objLink.ContentId;
            cmd.Parameters.Add("@LinkUrl", SqlDbType.VarChar, 5000).Value = objLink.LinkUrl;
            cmd.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objLink.addedby;
            cmd.Parameters.Add("@ContentTypeID", SqlDbType.BigInt).Value = objLink.ContentTypeID;
            cmd.Parameters.Add("@LinkedText", SqlDbType.VarChar, 500).Value = objLink.LinkedText;
            cmd.Parameters.Add("@LinkedContentId", SqlDbType.BigInt).Value = objLink.LinkedContentId;
            cmd.Parameters.Add("@LinkedContentTypeId", SqlDbType.BigInt).Value = objLink.LinkedContentTypeId;
            cmd.Parameters.Add("@StartIndex", SqlDbType.BigInt).Value = objLink.StartIndex;
            cmd.Parameters.Add("@EndIndex", SqlDbType.BigInt).Value = objLink.EndIndex;
            cmd.ExecuteNonQuery();

            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_ContentLink objLink, DA_ContentLink.ContentLink flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelLink", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@LinkId", SqlDbType.BigInt).Value = objLink.LinkId;
            da.SelectCommand.Parameters.Add("@ContentId", SqlDbType.BigInt).Value = objLink.ContentId;
            da.SelectCommand.Parameters.Add("@LinkUrl", SqlDbType.VarChar, 5000).Value = objLink.LinkUrl;
            da.SelectCommand.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objLink.addedby;
            da.SelectCommand.Parameters.Add("@ContentTypeID", SqlDbType.BigInt).Value = objLink.ContentTypeID;
            da.SelectCommand.Parameters.Add("@LinkedText", SqlDbType.VarChar, 500).Value = objLink.LinkedText;
            da.SelectCommand.Parameters.Add("@strLinkText", SqlDbType.VarChar, 500).Value = objLink.strLinkedText;
            da.SelectCommand.Parameters.Add("@LinkedContentId", SqlDbType.BigInt).Value = objLink.LinkedContentId;
            da.SelectCommand.Parameters.Add("@LinkedContentTypeId", SqlDbType.BigInt).Value = objLink.LinkedContentTypeId;
            da.SelectCommand.Parameters.Add("@StartIndex", SqlDbType.BigInt).Value = objLink.StartIndex;
            da.SelectCommand.Parameters.Add("@EndIndex", SqlDbType.BigInt).Value = objLink.EndIndex;
            da.SelectCommand.Parameters.Add("@intTagType", SqlDbType.Int).Value = objLink.intTagType;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}