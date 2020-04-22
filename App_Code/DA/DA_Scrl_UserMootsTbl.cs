using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;


namespace DA_SKORKEL
{
 public class DA_Scrl_UserMootsTbl
 {
     private SqlCommand cmd;
     public enum Scrl_UserMootsTbl
     {
         Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, SelectCount = 6
     };
     public DA_Scrl_UserMootsTbl()
     {  }
public void AddEditDel_Scrl_UserMootsTbl(DO_Scrl_UserMootsTbl  ObjScrl_UserMootsTbl,  Scrl_UserMootsTbl  Flag)
 {
     SqlConnection conn = new SqlConnection();
     SQLManager co = new SQLManager();
     conn = co.GetConnection();
     cmd = new SqlCommand("SP_Scrl_UserMootsTbl", conn);
     cmd.CommandType = CommandType.StoredProcedure;
     cmd.Parameters.Add("@FlagNo",  SqlDbType.Int ).Value = Flag;
      cmd.Parameters.Add("@intMootId", SqlDbType.Int).Value = ObjScrl_UserMootsTbl.intMootId;
     cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserMootsTbl.intRegistrationId;
     cmd.Parameters.Add("@strTitle", SqlDbType.VarChar , 500 ).Value = ObjScrl_UserMootsTbl.strTitle;
     cmd.Parameters.Add("@intMonth", SqlDbType.Int).Value = ObjScrl_UserMootsTbl.intMonth;
     cmd.Parameters.Add("@intYear", SqlDbType.Int).Value = ObjScrl_UserMootsTbl.intYear;
     cmd.Parameters.Add("@strLocation", SqlDbType.VarChar , 500 ).Value = ObjScrl_UserMootsTbl.strLocation;
     cmd.Parameters.Add("@strJudges", SqlDbType.VarChar , 500 ).Value = ObjScrl_UserMootsTbl.strJudges;
     cmd.Parameters.Add("@strDescription", SqlDbType.VarChar , 1000 ).Value = ObjScrl_UserMootsTbl.strDescription;
   //  cmd.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserMootsTbl.dtAddedOn;
     cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserMootsTbl.intAddedBy;
     //cmd.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserMootsTbl.dtModifiedOn;
     cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserMootsTbl.intModifiedBy;
     cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar , 500 ).Value = ObjScrl_UserMootsTbl.strIpAddress;

     cmd.ExecuteNonQuery();
     co.CloseConnection(conn);
}
public DataTable GetDataTable(DO_Scrl_UserMootsTbl  ObjScrl_UserMootsTbl,  Scrl_UserMootsTbl  Flag)
 {
     DataTable dt = new DataTable();
     SqlConnection conn = new SqlConnection();
     SQLManager co = new SQLManager();
     conn = co.GetConnection();
     SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserMootsTbl", conn);
     da.SelectCommand.CommandType = CommandType.StoredProcedure;
          da.SelectCommand.Parameters.Add("@FlagNo",  SqlDbType.Int ).Value = Flag;
         da.SelectCommand.Parameters.Add("@intMootId", SqlDbType.Int).Value = ObjScrl_UserMootsTbl.intMootId;
   da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserMootsTbl.intRegistrationId;
   da.SelectCommand.Parameters.Add("@strTitle", SqlDbType.VarChar , 500 ).Value = ObjScrl_UserMootsTbl.strTitle;
   da.SelectCommand.Parameters.Add("@intMonth", SqlDbType.Int).Value = ObjScrl_UserMootsTbl.intMonth;
   da.SelectCommand.Parameters.Add("@intYear", SqlDbType.Int).Value = ObjScrl_UserMootsTbl.intYear;
   da.SelectCommand.Parameters.Add("@strLocation", SqlDbType.VarChar , 500 ).Value = ObjScrl_UserMootsTbl.strLocation;
   da.SelectCommand.Parameters.Add("@strJudges", SqlDbType.VarChar , 500 ).Value = ObjScrl_UserMootsTbl.strJudges;
   da.SelectCommand.Parameters.Add("@strDescription", SqlDbType.VarChar , 1000 ).Value = ObjScrl_UserMootsTbl.strDescription;
   //da.SelectCommand.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserMootsTbl.dtAddedOn;
   da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserMootsTbl.intAddedBy;
   //da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserMootsTbl.dtModifiedOn;
   da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserMootsTbl.intModifiedBy;
   da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar , 500 ).Value = ObjScrl_UserMootsTbl.strIpAddress;

     da.Fill(dt);
     co.CloseConnection(conn);
     return dt;
 }
 } }
