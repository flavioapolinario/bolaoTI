using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using BolaoTI.web.Models;
using WebMatrix.WebData;
using System.Data.Entity;
using BolaoTI.web.DAL;
using System.Data.Entity.Infrastructure;

namespace BolaoTI.web
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {            
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");            

            //OAuthWebSecurity.RegisterGoogleClient();

            //OAuthWebSecurity.RegisterFacebookClient(appId: "1374432606175487",
            //                                                appSecret: "5716f0f320a20d178bbdeb24ebcd3034");                
        }
    }
}
