using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ScoreService
/// </summary>
public class ScoreService
{
    public class ScoreObject
    {
        public  int TotalScore;
        public int AScore;
        public int BScore;
        public int CScore;
        public int DScore;
        public ScoreObject()
        {
            TotalScore = AScore = BScore = CScore = DScore = 0;
        }
    }

    public static ScoreService.ScoreObject getScore(int UserID)
    {
        ScoreObject ret = new ScoreObject();
        DA_MyScore objDAScore = new DA_MyScore();
        DO_MyPoints objDOPoint = new DO_MyPoints();
        objDOPoint.UserID = UserID;
        DataTable dtScore = objDAScore.GetDataTable(objDOPoint, DA_MyScore.MyScore.GetScore);
        if (dtScore.Rows.Count > 0)
        {
            double tmpAScore = Convert.ToDouble(dtScore.Rows[0]["CategoryA"]) / 0.2;
            double tmpBScore = Convert.ToDouble(dtScore.Rows[0]["CategoryB"]) / 0.4;

            double tmpCScore = 0.0;
            double tmpDScore = 0.0;
            if (dtScore.Rows[0]["CPercentileScore"] != System.DBNull.Value)
                tmpCScore = Convert.ToDouble(dtScore.Rows[0]["CPercentileScore"]);
            if (dtScore.Rows[0]["DPercentileScore"] != System.DBNull.Value)
                tmpDScore = Convert.ToDouble(dtScore.Rows[0]["DPercentileScore"]);

            ret.AScore = Convert.ToInt32(tmpAScore);
            ret.BScore = Convert.ToInt32(tmpBScore);
            ret.CScore = Convert.ToInt32(tmpCScore);
            ret.DScore = Convert.ToInt32(tmpDScore);

            ret.TotalScore = Convert.ToInt32(
                (tmpAScore * 0.2) +
                (tmpBScore * 0.4) +
                (tmpCScore * 0.38) +
                (tmpDScore * 0.02)
                );

        }

        return ret;
    }
}