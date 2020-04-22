using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_MyPoints
/// </summary>
public class DA_MyPoints
{

    SqlCommand cmd = new SqlCommand();
    DataTable dt=new DataTable();
    DO_MyPoints objDOPoint = new DO_MyPoints();
	public DA_MyPoints()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public enum MyPoint
    {
        GetPersonalPoint=1,GetTotalScore=2,GetDefactoScore=3,GetDefactoTotalScore=4,
    }
    

    public DataTable GetDataTable(DO_MyPoints objDOPoint, DA_MyPoints.MyPoint flag)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_GetPesonalPoints", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objDOPoint.UserID;
        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }

    public string GetTotalscore(DO_MyPoints objDOPoint, DA_MyPoints.MyPoint flag)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_GetPesonalPoints", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objDOPoint.UserID;
        da.Fill(dt);
        co.CloseConnection(conn);
        return dt.Rows[0]["totalScore"].ToString();
    }
}