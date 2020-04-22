using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;


namespace DA_SKORKEL
{
    public class DA_Scrl_GroupEventsTbl
    {
        private SqlCommand cmd;
        public enum Scrl_GroupEventsTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, AddComment = 6, BindChildList = 7, AddLike = 8, GetCommentLikeUserLists = 9, GetEventDetailsByDate = 10, GetEventDetailsByDates = 11
        };
        public enum Scrl_GroupOrgEventsTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, AddComment = 6, BindChildList = 7, AddLike = 8, GetCommentLikeUserLists = 9, GetEventDetailsByDate = 10
        };

        public DA_Scrl_GroupEventsTbl()
        { }

        public void AddEditDel_Scrl_GroupEventsTbl(DO_Scrl_GroupEventsTbl ObjScrl_UserEventsTbl, Scrl_GroupEventsTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_GroupEventsTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intGrpEventtId", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intGrpEventId;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intGroupId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intRegistrationId;
            cmd.Parameters.Add("@strTitle", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strTitle;
            cmd.Parameters.Add("@intMonth", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intMonth;
            cmd.Parameters.Add("@intYear", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intYear;
            cmd.Parameters.Add("@strLocation", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strLocation;
            cmd.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserEventsTbl.strDescription;
            cmd.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strComment;

            cmd.Parameters.Add("@dtFromDate", SqlDbType.DateTime).Value = ObjScrl_UserEventsTbl.dtFromDate;
            cmd.Parameters.Add("@dtTodate", SqlDbType.DateTime).Value = ObjScrl_UserEventsTbl.dtTodate;
            cmd.Parameters.Add("@strPriority", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strPriority;
            cmd.Parameters.Add("@strColor", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strColor;

            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intAddedBy;
            //  cmd.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserEventsTbl.dtModifiedOn;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strIpAddress;
            cmd.Parameters.Add("@strContactNumber", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strContactNumber;
            cmd.Parameters.Add("@strContactPerson", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strContactPerson;

            ObjScrl_UserEventsTbl.intNewEventID = Convert.ToInt32(cmd.ExecuteScalar());
            //cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_GroupEventsTbl ObjScrl_UserEventsTbl, Scrl_GroupEventsTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_GroupEventsTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intGrpEventtId", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intGrpEventId;
            da.SelectCommand.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intGroupId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@strTitle", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strTitle;
            da.SelectCommand.Parameters.Add("@intMonth", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intMonth;
            da.SelectCommand.Parameters.Add("@intYear", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intYear;
            da.SelectCommand.Parameters.Add("@strLocation", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strLocation;
            da.SelectCommand.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserEventsTbl.strDescription;
            da.SelectCommand.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strComment;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intAddedBy;
            // da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserEventsTbl.dtModifiedOn;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strIpAddress;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.CurrentPageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.CurrentPage;
            da.SelectCommand.Parameters.Add("@dtFromDate", SqlDbType.DateTime).Value = ObjScrl_UserEventsTbl.dtFromDate;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public void AddEditDel_Like(DO_Scrl_GroupEventsTbl objLike, Scrl_GroupEventsTbl flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            cmd = new SqlCommand("SP_Scrl_GroupEventsTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intLikeId", SqlDbType.BigInt).Value = objLike.intLikeId;
            cmd.Parameters.Add("@intLikeDisLike", SqlDbType.BigInt).Value = objLike.intLikeDisLike;
            cmd.Parameters.Add("@intCommentId", SqlDbType.BigInt).Value = objLike.intCommentId;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objLike.intAddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = objLike.strIpAddress;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetLikeDataTable(DO_Scrl_GroupEventsTbl objLike, Scrl_GroupEventsTbl flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("SP_Scrl_GroupEventsTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intLikeId", SqlDbType.BigInt).Value = objLike.intLikeId;
            da.SelectCommand.Parameters.Add("@intLikeDisLike", SqlDbType.BigInt).Value = objLike.intLikeDisLike;
            da.SelectCommand.Parameters.Add("@intCommentId", SqlDbType.BigInt).Value = objLike.intCommentId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objLike.intAddedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = objLike.strIpAddress;


            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }


        //organization Event

        public void AddEditDel_Scrl_GroupOrgEventsTbl(DO_Scrl_GroupEventsTbl ObjScrl_UserEventsTbl, Scrl_GroupOrgEventsTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_GroupOrgEventsTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intGrpEventtId", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intGrpEventId;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intGroupId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intRegistrationId;
            cmd.Parameters.Add("@strTitle", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strTitle;
            cmd.Parameters.Add("@intMonth", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intMonth;
            cmd.Parameters.Add("@intYear", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intYear;
            cmd.Parameters.Add("@strLocation", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strLocation;
            cmd.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserEventsTbl.strDescription;
            cmd.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strComment;

            cmd.Parameters.Add("@dtFromDate", SqlDbType.DateTime).Value = ObjScrl_UserEventsTbl.dtFromDate;
            cmd.Parameters.Add("@dtTodate", SqlDbType.DateTime).Value = ObjScrl_UserEventsTbl.dtTodate;
            cmd.Parameters.Add("@strPriority", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strPriority;
            cmd.Parameters.Add("@strColor", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strColor;

            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intAddedBy;
            //  cmd.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserEventsTbl.dtModifiedOn;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strIpAddress;
            cmd.Parameters.Add("@strContactNumber", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strContactNumber;
            cmd.Parameters.Add("@strContactPerson", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strContactPerson;
            cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.OrgId;

            ObjScrl_UserEventsTbl.intNewEventID = Convert.ToInt32(cmd.ExecuteScalar());
            //cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetOrgDataTable(DO_Scrl_GroupEventsTbl ObjScrl_UserEventsTbl, Scrl_GroupOrgEventsTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_GroupOrgEventsTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intGrpEventtId", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intGrpEventId;
            da.SelectCommand.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intGroupId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@strTitle", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strTitle;
            da.SelectCommand.Parameters.Add("@intMonth", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intMonth;
            da.SelectCommand.Parameters.Add("@intYear", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intYear;
            da.SelectCommand.Parameters.Add("@strLocation", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strLocation;
            da.SelectCommand.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserEventsTbl.strDescription;
            da.SelectCommand.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strComment;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intAddedBy;
            // da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserEventsTbl.dtModifiedOn;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strIpAddress;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.CurrentPageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.CurrentPage;
            da.SelectCommand.Parameters.Add("@dtFromDate", SqlDbType.DateTime).Value = ObjScrl_UserEventsTbl.dtFromDate;
            da.SelectCommand.Parameters.Add("@Orgid", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.OrgId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public void AddEditDel_OrgLike(DO_Scrl_GroupEventsTbl objLike, Scrl_GroupOrgEventsTbl flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            cmd = new SqlCommand("SP_Scrl_GroupOrgEventsTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intLikeId", SqlDbType.BigInt).Value = objLike.intLikeId;
            cmd.Parameters.Add("@intLikeDisLike", SqlDbType.BigInt).Value = objLike.intLikeDisLike;
            cmd.Parameters.Add("@intCommentId", SqlDbType.BigInt).Value = objLike.intCommentId;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objLike.intAddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = objLike.strIpAddress;
            cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = objLike.OrgId;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetOrgLikeDataTable(DO_Scrl_GroupEventsTbl objLike, Scrl_GroupOrgEventsTbl flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("SP_Scrl_GroupOrgEventsTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intLikeId", SqlDbType.BigInt).Value = objLike.intLikeId;
            da.SelectCommand.Parameters.Add("@intLikeDisLike", SqlDbType.BigInt).Value = objLike.intLikeDisLike;
            da.SelectCommand.Parameters.Add("@intCommentId", SqlDbType.BigInt).Value = objLike.intCommentId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objLike.intAddedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = objLike.strIpAddress;
            da.SelectCommand.Parameters.Add("@OrgId", SqlDbType.Int).Value = objLike.OrgId;


            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
