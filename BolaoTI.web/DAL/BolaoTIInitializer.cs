using BolaoTI.web.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace BolaoTI.web.DAL
{
    public static class BolaoTIInitializer
    {
        public static void RegisterDataBase()
        {
            try
            {
                using (var context = new BolaoTIContext())
                {
                    if (!context.Database.Exists())
                    {                        
                        context.Database.Create();

                        if (!WebSecurity.Initialized)
                            WebSecurity.InitializeDatabaseConnection("BolaoTIContext", "UserProfile", "UserId", "UserName", autoCreateTables: true);
                    }
                    else
                    {
                        Database.SetInitializer(new MigrateDatabaseToLatestVersion<BolaoTIContext, Configuration>());
                        context.Database.Initialize(true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
            }

        }
    }
}