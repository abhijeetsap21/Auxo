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
            GoogleOAuth2Client clientGoog = new GoogleOAuth2Client("960677902616-epsthers63eti97dncc299auh2spm2kh.apps.googleusercontent.com", "_TEIES4fkOXsamJXlPmmByCZ");
            IDictionary<string, string> extraData = new Dictionary<string, string>();
            OpenAuth.AuthenticationClients.Add("google", () => clientGoog, extraData);
        }
    }
}