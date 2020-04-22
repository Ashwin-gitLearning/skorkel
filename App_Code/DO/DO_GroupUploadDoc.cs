using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DO_GroupUploadDoc
/// </summary>
public class DO_GroupUploadDoc
{
	public DO_GroupUploadDoc()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int intUploadDocId { get; set; }
    public int intGroupId { get; set; }
    public int intGrpuploadDoclikeID { get; set; }
    public int intAddedBy { get; set; }
    public int intLikeDisLike { get; set; }
    public int intModifiedBy { get; set; }
    public int intUploadDocCommentId { get; set; }
    public int intUploadCommentLikeId { get; set; }

    public string strFilePath { get; set; }
    public string strIpAddress { get; set; }
    public string strComment { get; set; }
    //public string strFilePath { get; set; }
    //public string strFilePath { get; set; }
    //public string strFilePath { get; set; }

}