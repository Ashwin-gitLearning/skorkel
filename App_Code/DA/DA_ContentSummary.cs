using System.Data;
using System.Data.SqlClient;
using SqlConn;
using System;

/// <summary>
/// Summary description for DA_CaseInsert
/// </summary>
/// 
namespace DA_SKORKEL
{

    public class DA_ContentSummary
    {
        public DA_ContentSummary()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum ContentSummary
        {
            Add = 1,
            GetContentSummary = 2,
            Update = 3,
            Delete = 4,
            SingleRecord = 5,
            AddUpdateSummary = 6,
            GetAllSum = 7, // To get summary by All
            UpdateSumLike =8 // To like unlike summary
        };
        public enum RecentActivities
        {
            AddRecentActivity = 1,GetRecentActivityDetails=2
        };
        SqlCommand cmd = new SqlCommand();

        public void AddEditDel_Summary(DO_ContentSummary objSummary, DA_ContentSummary.ContentSummary flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelContentSummary", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = (int)flag;
            cmd.Parameters.Add("@SummaryId", SqlDbType.BigInt).Value = objSummary.SummaryId;
            cmd.Parameters.Add("@ContentId", SqlDbType.BigInt).Value = objSummary.ContentId;
            cmd.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objSummary.addedby;
            cmd.Parameters.Add("@ContentTypeID", SqlDbType.BigInt).Value = objSummary.ContentTypeID;
            cmd.Parameters.Add("@SummaryText", SqlDbType.VarChar, 8000).Value = objSummary.SummaryText;
            //cmd.ExecuteNonQuery();
          // cmd.ExecuteScalar();
            objSummary.SummaryId = Convert.ToInt32(cmd.ExecuteScalar()); 
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_ContentSummary objSummary, DA_ContentSummary.ContentSummary flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelContentSummary", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@SummaryId", SqlDbType.BigInt).Value = objSummary.SummaryId;
            da.SelectCommand.Parameters.Add("@ContentId", SqlDbType.BigInt).Value = objSummary.ContentId;
            da.SelectCommand.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objSummary.addedby;
            da.SelectCommand.Parameters.Add("@ContentTypeID", SqlDbType.BigInt).Value = objSummary.ContentTypeID;
            da.SelectCommand.Parameters.Add("@SummaryText", SqlDbType.VarChar, 8000).Value = objSummary.SummaryText;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public void AddEditDel_RecentActivities(DO_ContentSummary objSummary, DA_ContentSummary.RecentActivities flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelRecentActivities", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intDocRecentActId", SqlDbType.BigInt).Value = objSummary.SummaryId;
            cmd.Parameters.Add("@ContentId", SqlDbType.BigInt).Value = objSummary.ContentId;
            cmd.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objSummary.addedby;
            cmd.Parameters.Add("@ContentTypeID", SqlDbType.BigInt).Value = objSummary.ContentTypeID;
            cmd.Parameters.Add("@Descrption", SqlDbType.VarChar, 8000).Value = objSummary.strDescrption;
            cmd.Parameters.Add("@strIPAddress", SqlDbType.VarChar, 8000).Value = objSummary.strIPAddress;
            cmd.Parameters.Add("@intTagType", SqlDbType.Int).Value = objSummary.intTagType;
            cmd.Parameters.Add("@intUserTypeId", SqlDbType.Int).Value = objSummary.intUserTypeId;
            cmd.Parameters.Add("@intPointId", SqlDbType.Int).Value = objSummary.PointId;
            cmd.ExecuteNonQuery();

            co.CloseConnection(conn);
        }
        public DataTable GetRecentActity(DO_ContentSummary objSummary, DA_ContentSummary.RecentActivities flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelRecentActivities", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@ContentId", SqlDbType.BigInt).Value = objSummary.ContentId;
            da.SelectCommand.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objSummary.addedby;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}