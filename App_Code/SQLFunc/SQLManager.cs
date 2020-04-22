using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

/// <summary>
/// This Class is for Sql Connection
/// </summary>
namespace SqlConn
{
    public class SQLManager
    {
        private SqlConnection conn;
        public SQLManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string GetConnectionString()
        {
            return ConfigurationManager.AppSettings["ConnectionString"];
        }

        public SqlConnection GetConnection()
        {
            string str = "";
            str = GetConnectionString();
            if (str == "")
            {
                return null;
            }
            try
            {
                conn = new SqlConnection(str);
                conn.Open();
            }
            catch (Exception exp)
            {
                conn = null;
                throw exp;
            }
            return conn;
        }

        public void CloseConnection(SqlConnection cn)
        {
            cn.Close();
        }

        public Boolean IsDuplicate(string TableName, string Parameter, string parameterValue)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = GetConnection();

                SqlCommand cmd = new SqlCommand("PL_CommanValidation_SP", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@Parameter1", SqlDbType.NVarChar, 50).Value = Parameter;
                cmd.Parameters.Add("@ParameterValue1", SqlDbType.VarChar, 500).Value = "'" + parameterValue + "'";
                cmd.Parameters.Add("@Parameter2", SqlDbType.VarChar, 500).Value = "";
                cmd.Parameters.Add("@PrimaryKey", SqlDbType.VarChar, 500).Value = "";
                cmd.Parameters.Add("@Table1", SqlDbType.NVarChar, 200).Value = TableName;

                int i = Convert.ToInt32(cmd.ExecuteScalar());
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                CloseConnection(conn);
            }

        }

        public Boolean IsDuplicateEdit(string TableName, string Parameter, string parameterValue, string PrimaryKey, string PrimaryKeyvalue)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = GetConnection();
                SqlCommand cmd = new SqlCommand("PL_CommanValidation_SP", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = 2;
                cmd.Parameters.Add("@Parameter1", SqlDbType.NVarChar, 200).Value = Parameter;
                cmd.Parameters.Add("@ParameterValue1", SqlDbType.VarChar, 500).Value = "'" + parameterValue + "'";
                cmd.Parameters.Add("@Parameter2", SqlDbType.VarChar, 500).Value = PrimaryKey;
                cmd.Parameters.Add("@PrimaryKey", SqlDbType.VarChar, 500).Value = "'" + PrimaryKeyvalue + "'";
                cmd.Parameters.Add("@Table1", SqlDbType.NVarChar, 200).Value = TableName;

                int i = Convert.ToInt32(cmd.ExecuteScalar());
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                CloseConnection(conn);
            }
        }

        public bool IsDeciNumber(String str)
        {
            // Regex objregex = new Regex("[^0-9]");
            Regex objregex = new Regex(@"[^\d*\.\d{1,2}$]");
            return !objregex.IsMatch(str);
        }

        public int CharCount(String strSource, String strToCount)
        {
            int iCount = 0;
            int iPos = strSource.IndexOf(strToCount);
            while (iPos != -1)
            {
                iCount++;
                strSource = strSource.Substring(iPos + 1);
                iPos = strSource.IndexOf(strToCount);
            }
            return iCount;
        }

        public Boolean IsDuplicateValue(string TableName, string Parameter1, string Parameter2, string parameterValue1, string parameterValue2)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = GetConnection();
                SqlCommand cmd = new SqlCommand("MT_CommanValidationDuplicate_SP", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@Parameter1", SqlDbType.NVarChar, 200).Value = Parameter1;
                cmd.Parameters.Add("@Parameter2", SqlDbType.NVarChar, 200).Value = Parameter2;
                cmd.Parameters.Add("@ParameterValue1", SqlDbType.VarChar, 500).Value = "'" + parameterValue1 + "'";
                if (parameterValue2 == "")
                {
                    cmd.Parameters.Add("@ParameterValue2", Convert.DBNull);
                }
                else
                {
                    cmd.Parameters.Add("@ParameterValue2", SqlDbType.VarChar, 500).Value = "'" + parameterValue2 + "'";
                }
                //cmd.Parameters.Add("@ParameterValue2", SqlDbType.VarChar, 500).Value = "'" + parameterValue2 + "'";
                cmd.Parameters.Add("@Table1", SqlDbType.NVarChar, 200).Value = TableName;

                int i = Convert.ToInt32(cmd.ExecuteScalar());
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                CloseConnection(conn);
            }
        }

        public Boolean IsDuplicateUser(string TableName, string Parameter, string parameterValue, string AddedByColumn, Int64 AddedBy)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = GetConnection();

                SqlCommand cmd = new SqlCommand("PL_CommanValidation_SP", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = 3;
                cmd.Parameters.Add("@Parameter1", SqlDbType.NVarChar, 50).Value = Parameter;
                cmd.Parameters.Add("@ParameterValue1", SqlDbType.VarChar, 500).Value = "'" + parameterValue + "'";
                cmd.Parameters.Add("@Parameter2", SqlDbType.VarChar, 500).Value = "";
                cmd.Parameters.Add("@PrimaryKey", SqlDbType.VarChar, 500).Value = "";
                cmd.Parameters.Add("@Table1", SqlDbType.NVarChar, 200).Value = TableName;
                cmd.Parameters.Add("@AddedByColumn", SqlDbType.NVarChar, 200).Value = AddedByColumn;
                cmd.Parameters.Add("@AddedBy", SqlDbType.VarChar, 100).Value = AddedBy;

                int i = Convert.ToInt32(cmd.ExecuteScalar());
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                CloseConnection(conn);
            }

        }

        public Boolean IsDuplicateEditUser(string TableName, string Parameter, string parameterValue, string PrimaryKey, string PrimaryKeyvalue, string AddedByColumn, Int64 AddedBy)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = GetConnection();
                SqlCommand cmd = new SqlCommand("PL_CommanValidation_SP", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = 4;
                cmd.Parameters.Add("@Parameter1", SqlDbType.NVarChar, 200).Value = Parameter;
                cmd.Parameters.Add("@ParameterValue1", SqlDbType.VarChar, 500).Value = "'" + parameterValue + "'";
                cmd.Parameters.Add("@Parameter2", SqlDbType.VarChar, 500).Value = PrimaryKey;
                cmd.Parameters.Add("@PrimaryKey", SqlDbType.VarChar, 500).Value = "'" + PrimaryKeyvalue + "'";
                cmd.Parameters.Add("@Table1", SqlDbType.NVarChar, 200).Value = TableName;
                cmd.Parameters.Add("@AddedByColumn", SqlDbType.NVarChar, 200).Value = AddedByColumn;
                cmd.Parameters.Add("@AddedBy", SqlDbType.VarChar, 100).Value = AddedBy;
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                CloseConnection(conn);
            }
        }

        public Boolean IsDuplicateUserDelete(string TableName, string Parameter, string parameterValue, string AddedByColumn, Int64 AddedBy, string Condition)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = GetConnection();

                SqlCommand cmd = new SqlCommand("PL_CommanValidation_SP", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = 5;
                cmd.Parameters.Add("@Parameter1", SqlDbType.NVarChar, 50).Value = Parameter;
                cmd.Parameters.Add("@ParameterValue1", SqlDbType.VarChar, 500).Value = "'" + parameterValue + "'";
                cmd.Parameters.Add("@Parameter2", SqlDbType.VarChar, 500).Value = "";
                cmd.Parameters.Add("@PrimaryKey", SqlDbType.VarChar, 500).Value = "";
                cmd.Parameters.Add("@Table1", SqlDbType.NVarChar, 200).Value = TableName;
                cmd.Parameters.Add("@AddedByColumn", SqlDbType.NVarChar, 200).Value = AddedByColumn;
                cmd.Parameters.Add("@AddedBy", SqlDbType.VarChar, 100).Value = AddedBy;
                cmd.Parameters.Add("@Cond", SqlDbType.VarChar, 100).Value = Condition;
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                CloseConnection(conn);
            }

        }

        public Boolean IsDuplicateEditUserDelete(string TableName, string Parameter, string parameterValue, string PrimaryKey, string PrimaryKeyvalue, string AddedByColumn, Int64 AddedBy, string Condition)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = GetConnection();
                SqlCommand cmd = new SqlCommand("PL_CommanValidation_SP", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = 6;
                cmd.Parameters.Add("@Parameter1", SqlDbType.NVarChar, 200).Value = Parameter;
                cmd.Parameters.Add("@ParameterValue1", SqlDbType.VarChar, 500).Value = "'" + parameterValue + "'";
                cmd.Parameters.Add("@Parameter2", SqlDbType.VarChar, 500).Value = PrimaryKey;
                cmd.Parameters.Add("@PrimaryKey", SqlDbType.VarChar, 500).Value = "'" + PrimaryKeyvalue + "'";
                cmd.Parameters.Add("@Table1", SqlDbType.NVarChar, 200).Value = TableName;
                cmd.Parameters.Add("@AddedByColumn", SqlDbType.NVarChar, 200).Value = AddedByColumn;
                cmd.Parameters.Add("@AddedBy", SqlDbType.VarChar, 100).Value = AddedBy;
                cmd.Parameters.Add("@Cond", SqlDbType.VarChar, 100).Value = Condition;
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                CloseConnection(conn);
            }
        }
    }
}