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

#nullable enable
namespace WinService.Database
{
    public partial class CdnDatabaseClient : IDisposable
    {
        public async Task<int?> GetProductId(string ean)
        {
            try
            {
                var commandText = @"
                    select 
                        a.TwK_TwrNumer
                    from 
                        CDN.TwrKody a
                    inner join 
                        CDN.TwrKarty b on a.TwK_TwrNumer = b.Twr_GIDNumer
                    where 
                        a.TwK_Kod = @ean and
                        b.Twr_Archiwalny = 0   
                ";

                using (var cmd = new SqlCommand(commandText, _sqlConn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ean", ean));

                    object o = await cmd.ExecuteScalarAsync();
                    if (o != null)
                        return Convert.ToInt32(o);                    
                }
            }
            catch (Exception ex)
            {
                LogError?.Invoke(ex.Message);
            }

            return null;
        }

        public async Task<ProductDetailsModel?> GetProductDetails(int id, int warehouseId)
        {
            #pragma warning disable 0219

            string test = @"
                declare @id int
                declare @warehouseId int
				set @id = 1
                set @warehouseId = 1
            ";

            #pragma warning restore 0219

            try
            {
                var commandText = @"
                    select top 1   
						a.Twr_GIDNumer as Id,
                        a.Twr_Kod as Code, 
                        a.Twr_Nazwa as Name,                         
                        --a.Twr_Wartosc1, 
                        a.Twr_EAN as Ean,
                        --a.Twr_Jm,
                        --a.Twr_JmCalkowita,
                        --a.Twr_JmFormat,
                        b.Twc_Wartosc as Price,
                        isnull(z.Count, 0) as Count
                    from 
                        CDN.TwrKarty a
                    inner join CDN.TwrCeny b on
                        a.Twr_GIDNumer = b.TwC_TwrNumer and
                        a.Twr_CenaSpr = b.Twc_TwrLp
                    left join
					(
						select	
							Twz_TwrNumer,
							(isnull(z.SaleCount, 0) - isnull(r.ResCount, 0)) as Count							
						from
						(
							select 
								Twz_TwrNumer,
								sum(Twz_Ilosc) as SaleCount 
							from 
								CDN.TwrZasoby 
							where 								
								Twz_MagNumer = @warehouseId
							group by 
		                        Twz_TwrNumer
						) z
						left join
						(
							select 
								Rez_TwrNumer,
								sum(Rez_Ilosc - Rez_Zrealizowano) as ResCount -- [PT] Tylko ilości niezrealizowane	
							from 
								CDN.Rezerwacje 
							where 								
								Rez_MagNumer = @warehouseId and
								Rez_GIDTyp = 2576 and
								CDN.TSToDate(Rez_DataWaznosci,0)>=getdate() --[PT] Tylko aktywne
							group by 
								Rez_TwrNumer																		
	                    ) r on z.TwZ_TwrNumer = r.Rez_TwrNumer
					) z on 
                        a.Twr_GIDNumer = z.TwZ_TwrNumer
                    where 
                        a.Twr_GIDNumer = @id and
                        a.Twr_Archiwalny = 0      
                ";

                using (var cmd = new SqlCommand(commandText, _sqlConn))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.Parameters.Add(new SqlParameter("@warehouseId", warehouseId));

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {                        
                        if (!reader.Read())
                            return null;

                        return new ProductDetailsModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Code = reader["Code"].ToString(),
                            Name = reader["Name"].ToString(),
                            Ean = reader["Ean"].ToString(),
                            Price = Convert.ToInt32(reader["Price"]),
                            Count = Convert.ToInt32(reader["Count"])
                        };                        
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
