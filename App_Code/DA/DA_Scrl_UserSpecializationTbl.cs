using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

namespace DA_SKORKEL
{
 public class DA_Scrl_UserSpecializationTbl
 {
     private SqlCommand cmd;
     public enum Scrl_UserSpecializationTbl
     {
         Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, SelectCount = 6
     };
     public DA_Scrl_UserSpecializationTbl()
     {  }
public void AddEditDel_Scrl_UserSpecializationTbl(DO_Scrl_UserSpecializationTbl  ObjScrl_UserSpecializationTbl,  Scrl_UserSpecializationTbl  Flag)
 {
     SqlConnection conn = new SqlConnection();
     SQLManager co = new SQLManager();
     conn = co.GetConnection();
     cmd = new SqlCommand("SP_Scrl_UserSpecializationTbl", conn);
     cmd.CommandType = CommandType.StoredProcedure;
     cmd.Parameters.Add("@FlagNo",  SqlDbType.Int ).Value = Flag;
      cmd.Parameters.Add("@inSpecializationId", SqlDbType.Int).Value = ObjScrl_UserSpecializationTbl.inSpecializationId;
     cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserSpecializationTbl.intRegistrationId;
     cmd.Parameters.Add("@strSpecialization", SqlDbType.VarChar , 500 ).Value = ObjScrl_UserSpecializationTbl.strSpecialization;
     cmd.Parameters.Add("@intYear", SqlDbType.Int).Value = ObjScrl_UserSpecializationTbl.intYear;
     cmd.Parameters.Add("@strDescription", SqlDbType.VarChar , 1000 ).Value = ObjScrl_UserSpecializationTbl.strDescription;
     //cmd.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserSpecializationTbl.dtAddedOn;
     cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserSpecializationTbl.intAddedBy;
   //  cmd.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserSpecializationTbl.dtModifiedOn;
     cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserSpecializationTbl.intModifiedBy;
     cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar , 500 ).Value = ObjScrl_UserSpecializationTbl.strIpAddress;

     cmd.ExecuteNonQuery();
     co.CloseConnection(conn);
}
public DataTable GetDataTable(DO_Scrl_UserSpecializationTbl  ObjScrl_UserSpecializationTbl,  Scrl_UserSpecializationTbl  Flag)
 {
     DataTable dt = new DataTable();
     SqlConnection conn = new SqlConnection();
     SQLManager co = new SQLManager();
     conn = co.GetConnection();
     SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserSpecializationTbl", conn);
     da.SelectCommand.CommandType = CommandType.StoredProcedure;
          da.SelectCommand.Parameters.Add("@FlagNo",  SqlDbType.Int ).Value = Flag;
         da.SelectCommand.Parameters.Add("@inSpecializationId", SqlDbType.Int).Value = ObjScrl_UserSpecializationTbl.inSpecializationId;
   da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserSpecializationTbl.intRegistrationId;
   da.SelectCommand.Parameters.Add("@strSpecialization", SqlDbType.VarChar , 500 ).Value = ObjScrl_UserSpecializationTbl.strSpecialization;
   da.SelectCommand.Parameters.Add("@intYear", SqlDbType.Int).Value = ObjScrl_UserSpecializationTbl.intYear;
   da.SelectCommand.Parameters.Add("@strDescription", SqlDbType.VarChar , 1000 ).Value = ObjScrl_UserSpecializationTbl.strDescription;
  // da.SelectCommand.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserSpecializationTbl.dtAddedOn;
   da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserSpecializationTbl.intAddedBy;
  // da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserSpecializationTbl.dtModifiedOn;
   da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserSpecializationTbl.intModifiedBy;
   da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar , 500 ).Value = ObjScrl_UserSpecializationTbl.strIpAddress;

     da.Fill(dt);
     co.CloseConnection(conn);
     return dt;
 }
 } }
