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
        public async Task<List<OrderModel>?> GetOrders()
        {            
            try
            {
                var commandText = @"
                    select 
                        a.ZaN_GIDNumer as Id, 
	                    CDN.NumerDokumentu
                        (
                            CDN.DokMapTypDokumentu
                            (
                                a.ZaN_GIDTyp,
                                a.ZaN_ZamTyp, 
                                a.ZaN_Rodzaj
                            ), 
                            0, 
                            0,
                            a.ZaN_ZamNumer, 
                            a.ZaN_ZamRok,
                            a.ZaN_ZamSeria, 
                            a.ZaN_ZamMiesiac
                        ) as Name,
                        b.Knt_Akronim as ContractorCode 
                    from 
                        CDN.ZamNag a
                    left join CDN.KntKarty b on 
		                b.Knt_GIDNumer = a.ZaN_KntNumer 
                    where
                        a.ZaN_ZamTyp = 1280 and
	                    a.ZaN_Stan in (3, 5) and 
	                    a.ZaN_Rodzaj = 4 	                    
                    order by
                        a.ZaN_GIDNumer desc
                ";

                using (var cmd = new SqlCommand(commandText, _sqlConn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var orders = new List<OrderModel>();

                        while (reader.Read())
                        {                            
                            orders.Add(new OrderModel
                            { 
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Contractor = reader["ContractorCode"].ToString()
                            });
                        }
                        
                        return orders;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError?.Invoke(ex.Message);
            }

            return null;
        }
    }    
}
