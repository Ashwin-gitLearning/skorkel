using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;


namespace DA_SKORKEL
{
    public class DA_Scrl_UserExperienceTbl
    {
        private SqlCommand cmd;
        public enum Scrl_UserExperienceTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, SelectCount = 6,InsertUserSkill=7,GetUserSkill=8,DeleteUserSkill=9,UpdateUserSkill=10,GetSkillByUserId=11,GetSkillWithoudUkey=12,GetSkillUnqId=13,UpdateSkill=14,InsertUserExpSkill=15,DeleteSkills=16,
            GetEndronseCount=17,DeleteUserSkills=18,
        };
        public enum Scrl_OrgExperience
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, SelectCount = 6
        };
        public DA_Scrl_UserExperienceTbl()
        { }
        public void AddEditDel_Scrl_UserExperienceTbl(DO_Scrl_UserExperienceTbl ObjScrl_UserExperienceTbl, Scrl_UserExperienceTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_UserExperienceTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intExperienceId", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intExperienceId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intRegistrationId;
            cmd.Parameters.Add("@strCompanyName", SqlDbType.VarChar, 500).Value = ObjScrl_UserExperienceTbl.strCompanyName;
            cmd.Parameters.Add("@strDesignation", SqlDbType.VarChar, 500).Value = ObjScrl_UserExperienceTbl.strDesignation;

            cmd.Parameters.Add("@dtFromDate", SqlDbType.DateTime).Value = ObjScrl_UserExperienceTbl.dtFromDate;
            cmd.Parameters.Add("@dtToDate", SqlDbType.DateTime).Value = ObjScrl_UserExperienceTbl.dtToDate;

            cmd.Parameters.Add("@inFromtMonth", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.inFromtMonth;
            cmd.Parameters.Add("@intFromYear", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intFromYear;
            cmd.Parameters.Add("@intToMonth", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intToMonth;
            cmd.Parameters.Add("@intToYear", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intToYear;
            cmd.Parameters.Add("@strLocation", SqlDbType.VarChar, 500).Value = ObjScrl_UserExperienceTbl.strLocation;
            cmd.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserExperienceTbl.strDescription;
            cmd.Parameters.Add("@bitAtPresent", SqlDbType.Bit).Value = ObjScrl_UserExperienceTbl.bitAtPresent;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intAddedBy;
            // cmd.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserExperienceTbl.dtModifiedOn;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = ObjScrl_UserExperienceTbl.strIpAddress;
            cmd.Parameters.Add("@strBarNo", SqlDbType.VarChar, 50).Value = ObjScrl_UserExperienceTbl.strBarNo;
            cmd.Parameters.Add("@strUnqId", SqlDbType.VarChar, 500).Value = ObjScrl_UserExperienceTbl.strUnqId;
            cmd.Parameters.Add("@strSkillName", SqlDbType.VarChar, 500).Value = ObjScrl_UserExperienceTbl.strSkillName;
            cmd.Parameters.Add("@intUserSkillId", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intUserSkillId;

            ObjScrl_UserExperienceTbl.intOutId = Convert.ToInt32(cmd.ExecuteScalar());
            co.CloseConnection(conn);
        }
        public DataTable GetDataTable(DO_Scrl_UserExperienceTbl ObjScrl_UserExperienceTbl, Scrl_UserExperienceTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserExperienceTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intExperienceId", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intExperienceId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@strCompanyName", SqlDbType.VarChar, 500).Value = ObjScrl_UserExperienceTbl.strCompanyName;
            da.SelectCommand.Parameters.Add("@strDesignation", SqlDbType.VarChar, 500).Value = ObjScrl_UserExperienceTbl.strDesignation;
            da.SelectCommand.Parameters.Add("@inFromtMonth", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.inFromtMonth;
            da.SelectCommand.Parameters.Add("@intFromYear", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intFromYear;
            da.SelectCommand.Parameters.Add("@intToMonth", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intToMonth;
            da.SelectCommand.Parameters.Add("@intToYear", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intToYear;
            da.SelectCommand.Parameters.Add("@strLocation", SqlDbType.VarChar, 500).Value = ObjScrl_UserExperienceTbl.strLocation;
            da.SelectCommand.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserExperienceTbl.strDescription;
            da.SelectCommand.Parameters.Add("@bitAtPresent", SqlDbType.Bit).Value = ObjScrl_UserExperienceTbl.bitAtPresent;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intAddedBy;
            // da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserExperienceTbl.dtModifiedOn;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = ObjScrl_UserExperienceTbl.strIpAddress;
            da.SelectCommand.Parameters.Add("@strUnqId", SqlDbType.VarChar, 500).Value = ObjScrl_UserExperienceTbl.strUnqId;
            da.SelectCommand.Parameters.Add("@strSkillName", SqlDbType.VarChar, 500).Value = ObjScrl_UserExperienceTbl.strSkillName;
            da.SelectCommand.Parameters.Add("@intUserSkillId", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intUserSkillId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }


        public void AddEditDel_Scrl_OrgExperienceTbl(DO_Scrl_UserExperienceTbl ObjScrl_UserExperienceTbl, Scrl_OrgExperience Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_OrgExperience", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intExperienceId", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intExperienceId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intRegistrationId;
            cmd.Parameters.Add("@strCompanyName", SqlDbType.VarChar, 500).Value = ObjScrl_UserExperienceTbl.strCompanyName;
            cmd.Parameters.Add("@strDesignation", SqlDbType.VarChar, 500).Value = ObjScrl_UserExperienceTbl.strDesignation;

            cmd.Parameters.Add("@dtFromDate", SqlDbType.DateTime).Value = ObjScrl_UserExperienceTbl.dtFromDate;
            cmd.Parameters.Add("@dtToDate", SqlDbType.DateTime).Value = ObjScrl_UserExperienceTbl.dtToDate;

            cmd.Parameters.Add("@inFromtMonth", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.inFromtMonth;
            cmd.Parameters.Add("@intFromYear", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intFromYear;
            cmd.Parameters.Add("@intToMonth", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intToMonth;
            cmd.Parameters.Add("@intToYear", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intToYear;
            cmd.Parameters.Add("@strLocation", SqlDbType.VarChar, 500).Value = ObjScrl_UserExperienceTbl.strLocation;
            cmd.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserExperienceTbl.strDescription;
            cmd.Parameters.Add("@bitAtPresent", SqlDbType.Bit).Value = ObjScrl_UserExperienceTbl.bitAtPresent;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intAddedBy;
            // cmd.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserExperienceTbl.dtModifiedOn;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserExperienceTbl.strIpAddress;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
        public DataTable GetOrgDataTable(DO_Scrl_UserExperienceTbl ObjScrl_UserExperienceTbl, Scrl_OrgExperience Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_OrgExperience", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intExperienceId", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intExperienceId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@strCompanyName", SqlDbType.VarChar, 500).Value = ObjScrl_UserExperienceTbl.strCompanyName;
            da.SelectCommand.Parameters.Add("@strDesignation", SqlDbType.VarChar, 500).Value = ObjScrl_UserExperienceTbl.strDesignation;
            da.SelectCommand.Parameters.Add("@inFromtMonth", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.inFromtMonth;
            da.SelectCommand.Parameters.Add("@intFromYear", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intFromYear;
            da.SelectCommand.Parameters.Add("@intToMonth", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intToMonth;
            da.SelectCommand.Parameters.Add("@intToYear", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intToYear;
            da.SelectCommand.Parameters.Add("@strLocation", SqlDbType.VarChar, 500).Value = ObjScrl_UserExperienceTbl.strLocation;
            da.SelectCommand.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserExperienceTbl.strDescription;
            da.SelectCommand.Parameters.Add("@bitAtPresent", SqlDbType.Bit).Value = ObjScrl_UserExperienceTbl.bitAtPresent;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intAddedBy;
            // da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserExperienceTbl.dtModifiedOn;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserExperienceTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserExperienceTbl.strIpAddress;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
