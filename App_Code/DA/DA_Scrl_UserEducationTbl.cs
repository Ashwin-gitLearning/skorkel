using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

namespace DA_SKORKEL
{
    public class DA_Scrl_UserEducationTbl
    {
        private SqlCommand cmd;
        public enum Scrl_UserEducationTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, GetEducation = 6, GetAchievement = 7, GetEducationOnly = 8, GetAchivementOnly = 9, DeleteAchivement = 10, SingleRecordAchivemrnt = 11, InsertAchivement = 12, UpdateAchivement = 13, LoadYear = 14, GetDeegreeid = 15, GetInstituteid = 16,
            GetMilestones = 17, GetPosition = 18, GetCompLavel = 19, GetPositionList = 20, GetCompSingleRecord = 21
        };
        public enum Scrl_OrgPorfolio
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, GetEducation = 6, GetAchievement = 7,
        };
        public DA_Scrl_UserEducationTbl()
        { }

        public void AddEditDel_Scrl_UserEducationTbl(DO_Scrl_UserEducationTbl ObjScrl_UserEducationTbl, Scrl_UserEducationTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_UserEducationTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intEducationId", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intEducationId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intRegistrationId;
            cmd.Parameters.Add("@strInstituteName", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strInstituteName;
            cmd.Parameters.Add("@intMonth", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intMonth;
            cmd.Parameters.Add("@intYear", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intYear;
            cmd.Parameters.Add("@strDegree", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strDegree;
            cmd.Parameters.Add("@strGrade", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strGrade;
            cmd.Parameters.Add("@strSpclLibrary", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strSpclLibrary;
            cmd.Parameters.Add("@strEducationAchievement", SqlDbType.VarChar, 50).Value = ObjScrl_UserEducationTbl.strEducationAchievement;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intAddedBy;
            //  cmd.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserEducationTbl.dtModifiedOn;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = ObjScrl_UserEducationTbl.strIpAddress;
            cmd.Parameters.Add("@dtDate", SqlDbType.DateTime).Value = ObjScrl_UserEducationTbl.dtDate;
            cmd.Parameters.Add("@intToMonth", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intToMonth;
            cmd.Parameters.Add("@intToYear", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intToYear;
            cmd.Parameters.Add("@intAchivmentId", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intAchivmentId;

            cmd.Parameters.Add("@strCompititionName", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strCompititionName;
            cmd.Parameters.Add("@strPosition", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strPosition;
            cmd.Parameters.Add("@strAdditionalAward", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strAdditionalAward;
            cmd.Parameters.Add("@strAchDescription", SqlDbType.VarChar, 5000).Value = ObjScrl_UserEducationTbl.strAchDescription;
            cmd.Parameters.Add("@strMilestone", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strMilestone;
            cmd.Parameters.Add("@intDegreeId", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intDegreeId;
            cmd.Parameters.Add("@intInstituteId", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intInstituteId;
            cmd.Parameters.Add("@intPostionId", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intPostionId;
            cmd.Parameters.Add("@intMilestoneId", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intMilestoneId;
            cmd.Parameters.Add("@CompLavel", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.CompLavel;
            cmd.Parameters.Add("@CompId", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.CompID;
            cmd.Parameters.Add("@stud_percentile", SqlDbType.Decimal).Value = ObjScrl_UserEducationTbl.stud_percentile;
            cmd.Parameters.Add("@IsScorePublic", SqlDbType.Bit, 10).Value = ObjScrl_UserEducationTbl.strIsScorePublic;

            ObjScrl_UserEducationTbl.intOutId = Convert.ToInt32(cmd.ExecuteScalar());
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_UserEducationTbl ObjScrl_UserEducationTbl, Scrl_UserEducationTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserEducationTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intEducationId", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intEducationId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@strInstituteName", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strInstituteName;
            da.SelectCommand.Parameters.Add("@intMonth", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intMonth;
            da.SelectCommand.Parameters.Add("@intYear", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intYear;
            da.SelectCommand.Parameters.Add("@strDegree", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strDegree;
            da.SelectCommand.Parameters.Add("@strGrade", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strGrade;
            da.SelectCommand.Parameters.Add("@strSpclLibrary", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strSpclLibrary;
            da.SelectCommand.Parameters.Add("@strEducationAchievement", SqlDbType.VarChar, 50).Value = ObjScrl_UserEducationTbl.strEducationAchievement;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intAddedBy;
            //  da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserEducationTbl.dtModifiedOn;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = ObjScrl_UserEducationTbl.strIpAddress;
            da.SelectCommand.Parameters.Add("@intAchivmentId", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intAchivmentId;
            da.SelectCommand.Parameters.Add("@PositionIds", SqlDbType.VarChar, 200).Value = ObjScrl_UserEducationTbl.PositionIdList;
            da.SelectCommand.Parameters.Add("@stud_percentile", SqlDbType.Decimal).Value = ObjScrl_UserEducationTbl.stud_percentile;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public void AddEditDel_Scrl_OrgPortfolio(DO_Scrl_UserEducationTbl ObjScrl_UserEducationTbl, Scrl_OrgPorfolio Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_OrgPorfolio", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intEducationId", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intEducationId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intRegistrationId;
            cmd.Parameters.Add("@strInstituteName", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strInstituteName;
            cmd.Parameters.Add("@intMonth", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intMonth;
            cmd.Parameters.Add("@intYear", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intYear;
            cmd.Parameters.Add("@strDegree", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strDegree;
            cmd.Parameters.Add("@strGrade", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strGrade;
            cmd.Parameters.Add("@strSpclLibrary", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strSpclLibrary;
            cmd.Parameters.Add("@strEducationAchievement", SqlDbType.VarChar, 50).Value = ObjScrl_UserEducationTbl.strEducationAchievement;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intAddedBy;
            //  cmd.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserEducationTbl.dtModifiedOn;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = ObjScrl_UserEducationTbl.strIpAddress;
            cmd.Parameters.Add("@dtDate", SqlDbType.DateTime).Value = ObjScrl_UserEducationTbl.dtDate;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetOrgDataTable(DO_Scrl_UserEducationTbl ObjScrl_UserEducationTbl, Scrl_OrgPorfolio Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_OrgPorfolio", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intEducationId", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intEducationId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@strInstituteName", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strInstituteName;
            da.SelectCommand.Parameters.Add("@intMonth", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intMonth;
            da.SelectCommand.Parameters.Add("@intYear", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intYear;
            da.SelectCommand.Parameters.Add("@strDegree", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strDegree;
            da.SelectCommand.Parameters.Add("@strGrade", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strGrade;
            da.SelectCommand.Parameters.Add("@strSpclLibrary", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strSpclLibrary;
            da.SelectCommand.Parameters.Add("@strEducationAchievement", SqlDbType.VarChar, 50).Value = ObjScrl_UserEducationTbl.strEducationAchievement;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intAddedBy;
            //  da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserEducationTbl.dtModifiedOn;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = ObjScrl_UserEducationTbl.strIpAddress;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public int GetintDataTable(DO_Scrl_UserEducationTbl objDOEdu, Scrl_UserEducationTbl Flag)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection();
                SQLManager co = new SQLManager();
                conn = co.GetConnection();
                SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserEducationTbl", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
                da.SelectCommand.Parameters.Add("@intEducationId", SqlDbType.Int).Value = objDOEdu.intEducationId;
                da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objDOEdu.intRegistrationId;
                da.SelectCommand.Parameters.Add("@strInstituteName", SqlDbType.VarChar, 500).Value = objDOEdu.strInstituteName;
                da.SelectCommand.Parameters.Add("@intMonth", SqlDbType.Int).Value = objDOEdu.intMonth;
                da.SelectCommand.Parameters.Add("@intYear", SqlDbType.Int).Value = objDOEdu.intYear;
                da.SelectCommand.Parameters.Add("@strDegree", SqlDbType.VarChar, 500).Value = objDOEdu.strDegree;
                da.SelectCommand.Parameters.Add("@strGrade", SqlDbType.VarChar, 500).Value = objDOEdu.strGrade;
                da.SelectCommand.Parameters.Add("@strSpclLibrary", SqlDbType.VarChar, 500).Value = objDOEdu.strSpclLibrary;
                da.SelectCommand.Parameters.Add("@strEducationAchievement", SqlDbType.VarChar, 50).Value = objDOEdu.strEducationAchievement;
                da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objDOEdu.intAddedBy;
                //  da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserEducationTbl.dtModifiedOn;
                da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = objDOEdu.intModifiedBy;
                da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = objDOEdu.strIpAddress;
                da.SelectCommand.Parameters.Add("@intAchivmentId", SqlDbType.Int).Value = objDOEdu.intAchivmentId;
                da.SelectCommand.Parameters.Add("@intPostionId", SqlDbType.Int).Value = objDOEdu.intPostionId;
                da.SelectCommand.Parameters.Add("@intMilestoneId", SqlDbType.Int).Value = objDOEdu.intMilestoneId;
                da.SelectCommand.Parameters.Add("@CompLavel", SqlDbType.Int).Value = objDOEdu.CompLavel;
                da.SelectCommand.Parameters.Add("@strCompititionName", SqlDbType.VarChar, 200).Value = objDOEdu.strCompititionName;

                da.Fill(dt);
                co.CloseConnection(conn);
                return (Convert.ToInt32(dt.Rows[0]["intDegreeId"]));
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return 0;
            }
        }

        public int GetintDataTableAchievement(DO_Scrl_UserEducationTbl objDOEdu, Scrl_UserEducationTbl Flag)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection();
                SQLManager co = new SQLManager();
                conn = co.GetConnection();
                SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserEducationTbl", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
                da.SelectCommand.Parameters.Add("@strCompititionName", SqlDbType.VarChar, 200).Value = objDOEdu.strCompititionName;

                da.Fill(dt);
                co.CloseConnection(conn);
                return (Convert.ToInt32(dt.Rows[0]["CompetitionId"]));
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return 0;
            }
        }

        public int GetDataTableComp(DO_Scrl_UserEducationTbl objDOEdu, Scrl_UserEducationTbl scrl_UserEducationTbl)
        {
            return 0;
            //DataTable dt = new DataTable();
            //SqlConnection conn = new SqlConnection();
            //SQLManager co = new SQLManager();
            //conn = co.GetConnection();
            //SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserEducationTbl", conn);
            //da.SelectCommand.CommandType = CommandType.StoredProcedure;
            //da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            //da.SelectCommand.Parameters.Add("@intEducationId", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intEducationId;
            //da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intRegistrationId;
            //da.SelectCommand.Parameters.Add("@strInstituteName", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strInstituteName;
            //da.SelectCommand.Parameters.Add("@intMonth", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intMonth;
            //da.SelectCommand.Parameters.Add("@intYear", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intYear;
            //da.SelectCommand.Parameters.Add("@strDegree", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strDegree;
            //da.SelectCommand.Parameters.Add("@strGrade", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strGrade;
            //da.SelectCommand.Parameters.Add("@strSpclLibrary", SqlDbType.VarChar, 500).Value = ObjScrl_UserEducationTbl.strSpclLibrary;
            //da.SelectCommand.Parameters.Add("@strEducationAchievement", SqlDbType.VarChar, 50).Value = ObjScrl_UserEducationTbl.strEducationAchievement;
            //da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intAddedBy;
            ////  da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserEducationTbl.dtModifiedOn;
            //da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intModifiedBy;
            //da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = ObjScrl_UserEducationTbl.strIpAddress;
            //da.SelectCommand.Parameters.Add("@intAchivmentId", SqlDbType.Int).Value = ObjScrl_UserEducationTbl.intAchivmentId;

            //da.Fill(dt);
            //co.CloseConnection(conn);
            //return dt;
        }
    }
}
