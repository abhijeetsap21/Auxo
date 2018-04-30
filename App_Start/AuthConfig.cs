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
            GoogleOAuth2Client clientGoog = new GoogleOAuth2Client("1012224335701-5k6q9ho24c9cvm8mofu2vrpormc1eecc.apps.googleusercontent.com", "cfDVpTl5RxJVe8qxP8lkLU3j");
            IDictionary<string, string> extraData = new Dictionary<string, string>();
            OpenAuth.AuthenticationClients.Add("google", () => clientGoog, extraData);
        }
    }
}