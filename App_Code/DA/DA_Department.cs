using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SqlConn;


/// <summary>
/// Summary description for DA_Department
/// </summary>
/// 
namespace DA_SKORKEL
{
public class DA_Department
{
	 
          public DA_Department()
        {
                        //
            // TODO: Add constructor logic here
            //
        }

        public enum Department
        {
            Add = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5 , CheckInCourse=6
        };

        SqlCommand cmd = new SqlCommand();

        public DataTable GetDataTable(DO_Department objDepartment, Department flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelDepartment", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@DeptId", SqlDbType.Int).Value = objDepartment.DepartmentId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objDepartment.AddedBy;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;                                                                                                                                                                                                    

        }

        public void AddEditDel_Department(DO_Department  objDepartment, DA_Department.Department flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelDepartment", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@DeptId", SqlDbType.Int).Value = objDepartment.DepartmentId ;
            cmd.Parameters.Add("@DeptName", SqlDbType.VarChar, 200).Value = objDepartment.DepartmentName;
            cmd.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objDepartment.AddedBy;
            cmd.Parameters.Add("@ModifiedBy", SqlDbType.BigInt).Value = objDepartment.ModifiedBy;
                     
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }





        
}
}

