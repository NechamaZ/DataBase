using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data.OracleClient;

namespace WpfApplication1
{
    public class DBSingleton
    {
        private static OracleConnection oracleConnection1 = null;
        public static OracleConnection getConnection()
        {
            if (oracleConnection1 == null)
            {
                OracleConnectionStringBuilder myCStringB = new OracleConnectionStringBuilder();

                //myCStringB.UserID = "pirian";//put in username
                //myCStringB.Password = "308528041";//put in password
                //myCStringB.DataSource = "labdbwin";//use this for connecting to

                myCStringB.UserID = "system";//put in username
                myCStringB.Password = "nechama2301";//put in password
                myCStringB.DataSource = "XE";//use this for connecting to

                oracleConnection1 = new OracleConnection(myCStringB.ConnectionString);
            }

            return oracleConnection1;
        }
    }
}

