using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetOpenAuth.GoogleOAuth2;
using Microsoft.AspNet.Membership.OpenAuth;

namespace NewLetter.App_Start
{
    public class AuthConfig
    {
        public static void RegisterAuth()
        {
            GoogleOAuth2Client clientGoog = new GoogleOAuth2Client("1012224335701-pcd6masnuirmjvlo2k4flgqsqkh0m1va.apps.googleusercontent.com", "pyn2SgPH6jhhCmdduL9-B_Hh");
            IDictionary<string, string> extraData = new Dictionary<string, string>();
            OpenAuth.AuthenticationClients.Add("google", () => clientGoog, extraData);
        }
    }
}