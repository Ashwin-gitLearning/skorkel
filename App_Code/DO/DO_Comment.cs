using System;

/// <summary>
/// Summary description for DO_Comment
/// </summary>
public class DO_Comment
{
    public DO_Comment()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Variable Decleration
    public Int64 ParentID { get; set; }
    public Int64 OldParentID { get; set; }
    public Int64 CommentId { get; set; }
    public Int64 CaseId { get; set; }
    public String Comment { get; set; }
    public Int64 addedby { get; set; }
    public Int64 ContentTypeID { get; set; }
    public string CommentedText { get; set; }
    public Int64 intCommentAddedFor { get; set; }
    public string strOrigenalText { get; set; }
    #endregion
}