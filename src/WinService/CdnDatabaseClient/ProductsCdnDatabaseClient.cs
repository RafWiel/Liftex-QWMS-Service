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
        public async Task<List<ProductListModel>?> GetProducts(int warehouseId, int loadedCount = 0)
        {
            #pragma warning disable 0219

            string test = @"
                declare @warehouseId int
				set @warehouseId = 1             
            ";

            #pragma warning restore 0219

            try
            {
                var commandText = @"
                    select 
                        a.Twr_GIDNumer as Id, 
                        a.Twr_Kod as Code, 
                        a.Twr_Nazwa as Name,                        
                        a.Twr_JmCalkowita as IsNatural,
                        a.Twr_JmFormat as DecimalPlaces,
                        c.Twc_Wartosc as Price,
						isnull(z.Count, 0) as Count
                    from CDN.TwrKarty a
                    inner join CDN.TwrCeny c on
                        a.Twr_GIDNumer = c.TwC_TwrNumer and
                        a.Twr_CenaSpr = c.Twc_TwrLp
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
								CDN.TSToDate(Rez_DataWaznosci,0) >= getdate() --[PT] Tylko aktywne
							group by Rez_TwrNumer		
	                    ) r on z.TwZ_TwrNumer = r.Rez_TwrNumer
					) z on 
                        a.Twr_GIDNumer = z.TwZ_TwrNumer
                    where 
                        a.Twr_Typ in (1, 2) and
                        a.Twr_Archiwalny = 0   
                ";

                //if (filter.Length > 0)
                //{
                //    commandText += @"and 
                //        (
                //            a.Twr_Kod like + '%' + @filter + '%' or 
                //            a.Twr_Nazwa like + '%' + @filter + '%'                            
                //        )
                //    ";
                //}

                ////pokazuj stany zerowe
                //if (showEmptyInventory == false)
                //    commandText += @"
                //        and isnull(z.Count, 0) > 0
                //    ";

                ////pokaz tylko towary z nieustawionym kodem EAN
                //if (showOnlyNullBarcode)
                //    commandText += @"
                //        and len(Twr_Ean) = 0
                //    ";

                //commandText += @"
                //    order by a.Twr_Kod asc   
                //    offset (@loadedCount) rows 
                //    fetch next (50) rows only
                //";

                using (var cmd = new SqlCommand(commandText, _sqlConn))
                {
                    cmd.Parameters.Add(new SqlParameter("@warehouseId", warehouseId));
                    cmd.Parameters.Add(new SqlParameter("@loadedCount", loadedCount));

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var products = new List<ProductListModel>();
                        
                        while (reader.Read())
                        {
                            bool isNatural = Convert.ToBoolean(reader["IsNatural"]);

                            products.Add(new ProductListModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Code = reader["Code"].ToString(),
                                Name = reader["Name"].ToString(),
                                Count = Convert.ToDecimal(reader["Count"]),
                                MeasureUnitDecimalPlaces = isNatural ? 0 : Convert.ToInt32(reader["DecimalPlaces"]),
                                Price = Convert.ToDecimal(reader["Price"])
                            });
                        }

                        return products;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError?.Invoke(ex.Message);
            }

            return null;
        }

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

        public async Task<ProductDetailsModel?> GetProduct(int id, int warehouseId)
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
                        a.Twr_JmCalkowita as IsNatural,
                        a.Twr_JmFormat as DecimalPlaces,
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

                var detailsCount = await GetProductDetailsCount(id);

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
                            Count = Convert.ToInt32(reader["Count"]),
                            MeasureUnitDecimalPlaces = Convert.ToBoolean(reader["IsNatural"]) ? 0 : Convert.ToInt32(reader["DecimalPlaces"]),
                            Items = detailsCount
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

        public async Task<List<ProductDetailsCountModel>?> GetProductDetailsCount(int id)
        {
            #pragma warning disable 0219

            string test = @"
                declare @id int
                set @id = 1                
            ";

            #pragma warning restore 0219

            try
            {
                var commandText = @"
                    select 
	                    m.MAG_GIDNumer as WarehouseId, 
	                    m.MAG_Kod as WarehouseCode, 
	                    (isnull(z.SaleCount, 0) - isnull(r.ResCount, 0)) as SaleCount, 		
	                    isnull(z.MagCount, 0) as WarehouseCount, 
	                    isnull(r.ResCount, 0) as ReservationCount,	
                        isnull(z.Twr_JmCalkowita, 0) as IsNatural,
                        isnull(z.Twr_JmFormat, 0) as DecimalPlaces
                    from 
	                    CDN.Magazyny m 
                    left join 
                    (
	                    select 
		                    Rez_MagNumer, 
		                    sum(Rez_Ilosc - Rez_Zrealizowano) as ResCount -- [PT] Tylko ilości niezrealizowane							
	                    from 
		                    CDN.Rezerwacje 						
	                    where 
		                    Rez_TwrNumer = @id and 
		                    Rez_GIDTyp = 2576 and
				            CDN.TSToDate(Rez_DataWaznosci,0)>=getdate() --[PT] Tylko aktywne
	                    group by 
		                    Rez_MagNumer
                    ) r on 
	                    r.Rez_MagNumer = m.MAG_GIDNumer                         	
                    left join 
                    (
	                    select 
		                    z.Twz_MagNumer, 
		                    sum(z.Twz_IlMag) as MagCount, 
		                    sum(z.Twz_Ilosc) as SaleCount,
							t.Twr_JmCalkowita,
							t.Twr_JmFormat 
	                    from 
		                    CDN.TwrZasoby z 
						inner join CDN.TwrKarty t on
							z.Twz_TwrNumer = t.Twr_GIDNumer
	                    where 
		                    z.Twz_TwrNumer = @id 
	                    group by 
		                    z.Twz_MagNumer,
							t.Twr_JmCalkowita,
							t.Twr_JmFormat
                    ) z on 
	                    z.Twz_MagNumer = m.MAG_GIDNumer     
                    where
                        m.MAG_Pico = 0   
                ";

                using (var cmd = new SqlCommand(commandText, _sqlConn))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", id));                    

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var list = new List<ProductDetailsCountModel>();
                        while (reader.Read())
                        {
                            list.Add(new ProductDetailsCountModel
                            {
                                WarehouseId = Convert.ToInt32(reader["WarehouseId"]),
                                WarehouseCode = reader["WarehouseCode"].ToString(),
                                SaleCount = Convert.ToDecimal(reader["SaleCount"]),
                                WarehouseCount = Convert.ToDecimal(reader["WarehouseCount"]),
                                ReservationCount = Convert.ToDecimal(reader["ReservationCount"]),
                                MeasureUnitDecimalPlaces = Convert.ToBoolean(reader["IsNatural"]) ? 0 : Convert.ToInt32(reader["DecimalPlaces"]),
                            });
                        }

                        return list;
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
