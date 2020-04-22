using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_GroupShare
/// </summary>
public class DA_GroupShare
{
	public DA_GroupShare()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private SqlCommand cmd;

    public enum ShareGroup
    {
        Add = 1
    };
    public enum ShareOrgGroup
    {
        Add = 1
    };


    //public DataTable GetDataTable(DO_GroupShare objGroupShare, State flag)
    //{
    //    DataTable dt = new DataTable();
    //    SqlConnection conn = new SqlConnection();
    //    SQLManager co = new SQLManager();

    //    conn = co.GetConnection();
    //    SqlDataAdapter da = new SqlDataAdapter();
    //    da.SelectCommand = new SqlCommand("scrl_StateAddEditDel_SP", conn);
    //    da.SelectCommand.CommandType = CommandType.StoredProcedure;
    //    da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
    //    da.SelectCommand.Parameters.Add("@StateId", SqlDbType.BigInt).Value = objGroupShare.StateId;
    //    da.SelectCommand.Parameters.Add("@StateName", SqlDbType.NVarChar).Value = objGroupShare.StateName;
    //    da.SelectCommand.Parameters.Add("@intCountryId", SqlDbType.BigInt).Value = objGroupShare.CountryID;

    //    da.Fill(dt);
    //    co.CloseConnection(conn);
    //    return dt;

    //}

    public void AddEditDel_GroupShare(DO_GroupShare objGroupShare, ShareGroup flag)
    {

        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();

        cmd = new SqlCommand("Scrl_AddEditDelGroupShare", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        cmd.Parameters.Add("@strMessage", SqlDbType.VarChar).Value = objGroupShare.strMessage;
        cmd.Parameters.Add("@strLink", SqlDbType.VarChar).Value = objGroupShare.strLink;
        cmd.Parameters.Add("@strInvitee", SqlDbType.VarChar).Value = objGroupShare.strInvitee;
        cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = objGroupShare.intGroupId;
        cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = objGroupShare.intAddedBy;       
        cmd.ExecuteNonQuery();
        co.CloseConnection(conn);
    }

     //Mohsin Faras(10 Feb 2014)
    public void AddEditDel_OrgGroupShare(DO_GroupShare objGroupShare, ShareOrgGroup flag)
    {

        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();

        cmd = new SqlCommand("Scrl_AddEditDelOrgGroupShare", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        cmd.Parameters.Add("@strMessage", SqlDbType.VarChar).Value = objGroupShare.strMessage;
        cmd.Parameters.Add("@strLink", SqlDbType.VarChar).Value = objGroupShare.strLink;
        cmd.Parameters.Add("@strInvitee", SqlDbType.VarChar).Value = objGroupShare.strInvitee;
        cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = objGroupShare.intGroupId;
        cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = objGroupShare.intAddedBy;
        cmd.Parameters.Add("@intOrgnisationID", SqlDbType.Int).Value = objGroupShare.intOrgnisationID;
        cmd.ExecuteNonQuery();
        co.CloseConnection(conn);
    }
}