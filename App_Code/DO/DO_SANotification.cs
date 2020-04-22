using System;

/// <summary>
/// Summary description for DO_Case
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_SANotification
    {
        public DO_SANotification()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string NotificationDetail { get; set; }
        public DateTime NotificationDateTime { get; set; }
        public int CurrentPage { get; set; }
        public int CurrentPageSize { get; set; }
        public int intNotificationId { get; set; }
    }
}