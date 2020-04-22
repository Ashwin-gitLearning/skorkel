using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_WallShare
/// </summary>
public class DA_WallShare
{
	public DA_WallShare()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private SqlCommand cmd;

    public enum ShareWall
    {
        Add = 1,GetSharedinviteeId=2
    };


    public DataTable GetDataTable(DO_WallShare objWallShare, ShareWall flag)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_AddEditDelWallShare", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
      // da.SelectCommand.Parameters.Add("@StateId", SqlDbType.BigInt).Value = objGroupShare.StateId;
       // da.SelectCommand.Parameters.Add("@StateName", SqlDbType.NVarChar).Value = objGroupShare.StateName;
       // da.SelectCommand.Parameters.Add("@intCountryId", SqlDbType.BigInt).Value = objGroupShare.CountryID;

        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;

    }
    
    public void AddEditDelWallShare(DO_WallShare objWallShare, ShareWall flag)
    {

        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();

        cmd = new SqlCommand("Scrl_AddEditDelWallShare", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        cmd.Parameters.Add("@strMessage", SqlDbType.VarChar).Value = objWallShare.strMessage;
        cmd.Parameters.Add("@strLink", SqlDbType.VarChar).Value = objWallShare.strLink;
        cmd.Parameters.Add("@strInvitee", SqlDbType.VarChar).Value = objWallShare.strInvitee;
        cmd.Parameters.Add("@intStatusUpdateId", SqlDbType.Int).Value = objWallShare.intStatusUpdateId;
        cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = objWallShare.intAddedBy;
        cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = objWallShare.strIPAddress;
        cmd.ExecuteNonQuery();
        co.CloseConnection(conn);
    }
}