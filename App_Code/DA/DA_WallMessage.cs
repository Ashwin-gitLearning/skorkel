using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_WallMessage
/// </summary>
public class DA_WallMessage
{

    private SqlCommand cmd;
    public enum WallMessage
    {
        Add = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, AllRecommendations = 6, UpdateAskRecommendation = 7, AddTempSubCat = 8, GetDocumentSubCat = 9, RemoveTempSubCat = 10, DeleteContext = 11, GetMyEndorse = 12, AddChildRecomendation = 13,
        SelectMessageDetails=14,CheckMessagesend=15,checkmessage=16,AddSearchPeoplemsg=17,
    };

	public DA_WallMessage()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// Message SEND
    /// </summary>
    /// <param name="ObjScrl_UserHonorsTbl"></param>
    /// <param name="Flag"></param>
    public void Scrl_AddEditDelWallMessage(DO_WallMessage ObjScrl_UserHonorsTbl, WallMessage Flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();
        cmd = new SqlCommand("Scrl_AddEditDelWallMessage", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
        cmd.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intInvitedUserId;
        cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intRegistrationId;
        cmd.Parameters.Add("@intMessageId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intMessageId;
        cmd.Parameters.Add("@StrRecommendation", SqlDbType.VarChar, 20000).Value = ObjScrl_UserHonorsTbl.StrRecommendation;
        cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intGroupId;
        cmd.Parameters.Add("@strSubject", SqlDbType.VarChar, 500).Value = ObjScrl_UserHonorsTbl.strSubject;
        cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intAddedBy;
        cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl_UserHonorsTbl.strIpAddress;
        cmd.Parameters.Add("@intSkillId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intSkillId;
        cmd.Parameters.Add("@strTotalGrpMemberID", SqlDbType.VarChar, 5000).Value = ObjScrl_UserHonorsTbl.strTotalGrpMemberID;
        cmd.Parameters.Add("@striInvitedUserId", SqlDbType.VarChar,100).Value = ObjScrl_UserHonorsTbl.striInvitedUserId;

        ObjScrl_UserHonorsTbl.intOutWallMessageId = Convert.ToInt32(cmd.ExecuteScalar());
        co.CloseConnection(conn);
    }

    public DataTable GetDataTable(DO_WallMessage ObjScrl_UserHonorsTbl, WallMessage Flag)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter("Scrl_AddEditDelWallMessage", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
        da.SelectCommand.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intInvitedUserId;
        da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intRegistrationId;
        da.SelectCommand.Parameters.Add("@intMessageId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intMessageId;
        da.SelectCommand.Parameters.Add("@StrRecommendation", SqlDbType.VarChar, 1000).Value = ObjScrl_UserHonorsTbl.StrRecommendation;
        da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intAddedBy;
        da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl_UserHonorsTbl.strIpAddress;
        da.SelectCommand.Parameters.Add("@intSkillId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intSkillId;
        da.SelectCommand.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intGroupId;
        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }
}