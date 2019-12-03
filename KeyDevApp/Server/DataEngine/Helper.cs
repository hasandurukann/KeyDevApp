using System;
using System.Collections.Generic;
using System.Text;

namespace KeyDevApp.Server.DataEngine
{
    public class Helper
    {
        public static string GetConnectionString(string name = "DapperDB")
        {
            return "Server=.;Database=keydev_db01;Trusted_Connection=True;";
        }
    }
}
