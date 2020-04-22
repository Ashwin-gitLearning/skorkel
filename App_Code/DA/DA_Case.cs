using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_CaseInsert
/// </summary>
/// 
namespace DA_SKORKEL
{

    public class DA_Case
    {

        public DA_Case()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public enum Case
        {
            Add = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5, GetYear = 7, SearchRecord = 8, GetCourt = 9, SearchSqlPaging = 10, GetTop5Year = 11,
            GetTop5Court = 12,GetCaseDesc=13
        };

        SqlCommand cmd = new SqlCommand();

        public DataTable GetDataTable(DO_Case objcase, DA_Case.Case flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("scrl_AddEditDeleteCase_SP", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objcase.CaseId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objcase.AddedBy;
            da.SelectCommand.Parameters.Add("@Condition", SqlDbType.VarChar, 500).Value = objcase.Condition;
            da.SelectCommand.Parameters.Add("@Jurisdiction", SqlDbType.VarChar, 500).Value = objcase.Jurisdiction;
            da.SelectCommand.Parameters.Add("@year", SqlDbType.Int).Value = objcase.year;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = objcase.CurrentPage;
            da.SelectCommand.Parameters.Add("@pagesize", SqlDbType.Int).Value = objcase.PageSize;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }

        public void AddEditDel_Case(DO_Case objcase, DA_Case.Case flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("scrl_AddEditDeleteCase_SP", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objcase.CaseId;
            cmd.Parameters.Add("@CaseTitle", SqlDbType.VarChar, 200).Value = objcase.CaseTitle;
            cmd.Parameters.Add("@Citation", SqlDbType.VarChar, 200).Value = objcase.Citation;
            cmd.Parameters.Add("@EnactmentCites", SqlDbType.VarChar, 200).Value = objcase.EnactmentCites;
            cmd.Parameters.Add("@Jurisdiction", SqlDbType.VarChar, 200).Value = objcase.Jurisdiction;
            cmd.Parameters.Add("@ProvisionofLaw", SqlDbType.VarChar).Value = objcase.ProvisionofLaw;
            cmd.Parameters.Add("@JudgementDate", SqlDbType.DateTime).Value = objcase.JudgementDate;
            cmd.Parameters.Add("@Cites", SqlDbType.VarChar, 200).Value = objcase.Cites;
            cmd.Parameters.Add("@CitedBy", SqlDbType.VarChar, 200).Value = objcase.CitedBy;
            cmd.Parameters.Add("@PartyNames", SqlDbType.VarChar, 200).Value = objcase.PartyNames;
            cmd.Parameters.Add("@BenchStrength", SqlDbType.VarChar, 200).Value = objcase.BenchStrength;
            cmd.Parameters.Add("@ContentSource", SqlDbType.VarChar, 200).Value = objcase.ContentSource;
            cmd.Parameters.Add("@Description", SqlDbType.VarChar, 8000).Value = objcase.Description;
            cmd.Parameters.Add("@JudgeNames", SqlDbType.VarChar, 200).Value = objcase.JudgeNames;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }



    }


}