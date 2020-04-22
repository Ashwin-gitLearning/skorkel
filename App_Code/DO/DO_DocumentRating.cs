using System;


/// <summary>
/// Summary description for DO_DocumentRating
/// </summary>
public class DO_DocumentRating
{
	public DO_DocumentRating()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Variable Decleration

    public Int64 RatingId { get; set; }
    public Int64 TagId { get; set; }
    public Int64 Rating { get; set; }
    public string TagType { get; set; }
    public Int64 ContentId { get; set; }
    public Int64 addedby { get; set; }
    public Int64 ContentTypeID { get; set; }



    #endregion
}