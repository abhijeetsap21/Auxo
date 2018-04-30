using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NewLetter.Models
{
    public class NotificationComponenet
    {
        private oriondbEntities db = new oriondbEntities();
        public void RegisterNotification(DateTime dt)
        {
            string constr = ConfigurationManager.ConnectionStrings["NotificationConnection"].ConnectionString;
            string sqlCommond = @"select qenMialSendInterestedID from qenMialSendInterested  where convert(date, dataIsCreated)> @dt ";
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand(sqlCommond, con);
                cmd.Parameters.AddWithValue("@dt",dt);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.Notification = null;
                SqlDependency sqldep = new SqlDependency(cmd);
                sqldep.OnChange += sqlDep_onChange;
                using (SqlDataReader reader=cmd.ExecuteReader())
                {
                }
            }
        }
        void sqlDep_onChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info == SqlNotificationInfo.Insert)
            {
                SqlDependency sqldep = sender as SqlDependency;
                sqldep.OnChange += sqlDep_onChange;
                var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                notificationHub.Clients.All.notify("added");
            }
        }

    
    }
}