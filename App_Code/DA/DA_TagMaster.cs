using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_TagMaster
/// </summary>
public class DA_TagMaster
{
	public DA_TagMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private SqlCommand cmd;
    public enum TagMaster
    {
        Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, SelectCount = 6
    };

    public void AddEditDel_Scrl_TagMaster(DO_TagMaster ObjScrl, TagMaster Flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();
        cmd = new SqlCommand("Scrl_AddEditDelTagMaster", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = ObjScrl.Id;
        cmd.Parameters.Add("@intTag", SqlDbType.Int).Value = ObjScrl.intTag;

        cmd.ExecuteNonQuery();
        co.CloseConnection(conn);
    }
    public DataTable GetDataTable(DO_TagMaster ObjScrl, TagMaster Flag)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter("Scrl_AddEditDelTagMaster", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
        da.SelectCommand.Parameters.Add("@Id", SqlDbType.Int).Value = ObjScrl.Id;
        da.SelectCommand.Parameters.Add("@intTag", SqlDbType.Int).Value = ObjScrl.intTag; 

        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }

}