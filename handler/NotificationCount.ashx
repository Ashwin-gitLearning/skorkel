<%@ WebHandler Language="C#" Class="NotificationCount" %>

using System;
using System.Web;
using System.Data;
using DA_SKORKEL;
using System.Web.SessionState;

public class NotificationCount : IHttpHandler, IReadOnlySessionState
{

    DO_Registrationdetails objRegistration = new DO_Registrationdetails();
    DA_Registrationdetails objRegistrationDB = new DA_Registrationdetails();
    
    public void ProcessRequest (HttpContext context) {
        try
        {
            objRegistration.RegistrationId = Convert.ToInt32(context.Session["ExternalUserId"]);
            objRegistration.intNotificationCount = Convert.ToInt32(context.Request.QueryString["NotificationId"]);
            objRegistrationDB.AddEditDel_RegistrationDetails(objRegistration, DA_Registrationdetails.RegistrationDetails.UpdateNotifcationCount);
            string datatext = "";
            datatext = "1";
            context.Response.Write(datatext);
        }
        catch (Exception ex) { ex.Message.ToString(); }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}