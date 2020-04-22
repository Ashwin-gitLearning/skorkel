﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DO_Scrl_AddEditDelDocCategoryDetails
/// </summary>
public class DO_Scrl_AddEditDelDocCategoryDetails
{
	public DO_Scrl_AddEditDelDocCategoryDetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region variable Declaration

    public int DocCategoryId { get; set; }
    public int RegistrationId { get; set; }
    public int CategoryID { get; set; }

    private int _addedBy;
    public int AddedBy
    {
        get { return _addedBy; }
        set { _addedBy = value; }
    }

    private int _modifiedBy;
    public int ModifiedBy
    {
        get { return _modifiedBy; }
        set { _modifiedBy = value; }
    }

    private string _iPAddress;
    public string IPAddress
    {
        get { return _iPAddress; }
        set { _iPAddress = value; }
    }

    #endregion
}