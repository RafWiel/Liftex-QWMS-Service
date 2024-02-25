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
using QWMS.Models.Orders;
using WinService.Configuration;

#nullable enable

namespace WinService.Database
{
    public partial class CdnDatabaseClient : IDisposable
    {
        public async Task<List<OrderListModel>?> GetOrders(string? search, int? page)
        {
            string test = @"                                            
                declare @search varchar(100)
                declare @skipCount int
                set @skipCount = 0
                set @search = 'k1'
            ";
            
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
	                    a.ZaN_Rodzaj = 4 and
						(
							@search is null or @search is not null and 
							(
								b.Knt_Akronim like '%' + @search + '%' or                           
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
								) like '%' + @search + '%'                             
							) 							 
						)						    
                    order by a.ZaN_GIDNumer desc
                    offset (@skipCount) rows 
                    fetch next (50) rows only
                ";

                using (var cmd = new SqlCommand(commandText, _sqlConn))
                {                    
                    cmd.Parameters.Add(search != null ? new SqlParameter("@search", search) : new SqlParameter("@search", DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@skipCount", ((page ?? 1) - 1) * 50));

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var orders = new List<OrderListModel>();

                        while (reader.Read())
                        {                            
                            orders.Add(new OrderListModel
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
