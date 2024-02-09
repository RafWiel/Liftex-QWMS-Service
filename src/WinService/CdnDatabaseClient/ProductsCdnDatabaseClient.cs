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

namespace WinService.Database
{
    public partial class CdnDatabaseClient : IDisposable
    {
        public int GetProductId(string code)
        {
            if (string.IsNullOrEmpty(code))
                return 0;

            try
            {
                var commandText = @"
                    select 
                        Twr_GIDNumer
                    from 
                        CDN.TwrKarty
                    where
                        Twr_Kod = @code and
                        Twr_GIDTyp = @type and
                        Twr_Archiwalny = 0                    
                ";

                using (var cmd = new SqlCommand(commandText, _sqlConn))
                {
                    cmd.Parameters.Add(new SqlParameter("@code", code));
                    cmd.Parameters.Add(new SqlParameter("@type", ObjectType.Product));

                    var obj = cmd.ExecuteScalar();
                    if (obj == null)
                        return 0;
                    
                    return Convert.ToInt32(obj);
                }
            }
            catch (Exception ex)
            {
                LogError?.Invoke(ex.Message);
            }

            return 0;
        }
    }    
}
