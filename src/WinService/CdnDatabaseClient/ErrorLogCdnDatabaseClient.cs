using InterProcessCommunication.gTools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinService.Models;
using WinService.Configuration;
using System.Text.RegularExpressions;

namespace WinService.Database
{
    public partial class CdnDatabaseClient : IDisposable
    {                  
        public void ErrorLog_Insert(string errorMessage)
        {
            //uwaga
            return;

            try
            {
                string commandText = @"                        
                    insert into CDN.DS_Techsam_WebApi_ErrorLog 
                    (
                        Timestamp,
                        ErrorMessage
                    )
                    values
                    (
                        @Timestamp,
                        @ErrorMessage
                    )
                ";

                using (var cmd = new SqlCommand(commandText, _sqlConn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Timestamp", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@ErrorMessage", errorMessage));

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                gLog.Write(ex.ToString());
            }
        }       
    }
}
