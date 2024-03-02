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
using QWMS.Models.Products;
using WinService.Configuration;

#nullable enable

namespace WinService.Database
{
    public partial class CdnDatabaseClient : IDisposable
    {
        public async Task<List<BarcodeListModel>?> GetBarcodes(int productId, int? page)
        {
            #pragma warning disable 0219

            string test = @"
                declare @productId int
                declare @skipCount int				

                set @productId = 1                  
                set @skipCount = 0                
            ";

            #pragma warning restore 0219

            try
            {
                var commandText = @"
                    select
                        TwK_Id as Id,
                        TwK_Kod as Code, 
                        TwK_Jm as MeasureUnit
                    from 
                        CDN.TwrKody					
                    where 
                        TwK_TwrNumer = @productId and
                        TwK_Rodzaj = 0
                    order by    
                        TwK_Id asc  
                    offset (@skipCount) rows 
                    fetch next (50) rows only
                ";                                
                
                using (var cmd = new SqlCommand(commandText, _sqlConn))
                {
                    cmd.Parameters.Add(new SqlParameter("@productId", productId));                    
                    cmd.Parameters.Add(new SqlParameter("@skipCount", ((page ?? 1) - 1) * 50));

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var barcodes = new List<BarcodeListModel>();
                        
                        while (reader.Read())
                        {                            
                            barcodes.Add(new BarcodeListModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Code = reader["Code"].ToString(),
                                MeasureUnit = reader["MeasureUnit"].ToString(),                                
                            });
                        }

                        return barcodes;
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
