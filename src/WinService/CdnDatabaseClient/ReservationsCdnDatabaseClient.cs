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
using QWMS.Models.Reservations;

#nullable enable

namespace WinService.Database
{
    public partial class CdnDatabaseClient : IDisposable
    {
        public async Task<List<ReservationListModel>?> GetReservations(int productId, int warehouseId, int? page)
        {
            #pragma warning disable 0219

            string test = @"
                declare @productId int
                declare @warehouseId int
                declare @skipCount int				

                set @productId = 1  
                set @warehouseId = 1
                set @skipCount = 0                        
            ";

            #pragma warning restore 0219

            try
            {
                var commandText = @"
                    select 
                        a.Rez_GIDNumer as Id,
                        b.Knt_Akronim as Contractor, 
                        a.Rez_Ilosc as Count, 
                        CDN.NumerDokumentu
                        (
                            CDN.DokMapTypDokumentu
                            (
                                c.ZaN_GIDTyp,
                                c.ZaN_ZamTyp, 
                                c.ZaN_Rodzaj
                            ), 
                            0, 
                            0,
                            c.ZaN_ZamNumer, 
                            c.ZaN_ZamRok,
                            c.ZaN_ZamSeria, 
                            c.ZaN_ZamMiesiac
                        ) as OrderName,
                        d.Twr_JmCalkowita as IsNatural,
                        d.Twr_JmFormat as DecimalPlaces
                    from 
                        CDN.Rezerwacje a 
                    inner join 
                        CDN.KntKarty b on b.Knt_GIDNumer = a.Rez_KntNumer 
                    left join 
                        CDN.ZamNag c on (c.ZaN_GIDNumer = a.Rez_ZrdNumer and c.ZaN_GIDTyp = a.Rez_ZrdTyp) 
                    inner join 
                        CDN.TwrKarty d on a.Rez_TwrNumer = d.Twr_GIDNumer 
                    where 
                        a.Rez_TwrNumer = @productId and 
                        a.Rez_MagNumer = @warehouseId and 
                        a.Rez_GIDTyp = 2576 
                    order by 
                        b.Knt_Akronim asc 
                    offset (@skipCount) rows 
                    fetch next (50) rows only
                ";                                
                
                using (var cmd = new SqlCommand(commandText, _sqlConn))
                {
                    cmd.Parameters.Add(new SqlParameter("@productId", productId));
                    cmd.Parameters.Add(new SqlParameter("@warehouseId", warehouseId));
                    cmd.Parameters.Add(new SqlParameter("@skipCount", ((page ?? 1) - 1) * 50));

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var reservations = new List<ReservationListModel>();
                        
                        while (reader.Read())
                        {
                            bool isNatural = Convert.ToBoolean(reader["IsNatural"]);

                            reservations.Add(new ReservationListModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Contractor = reader["Contractor"].ToString(),
                                Count = Convert.ToDecimal(reader["Count"]),
                                OrderName = reader["OrderName"].ToString(),
                                MeasureUnitDecimalPlaces = isNatural ? 0 : Convert.ToInt32(reader["DecimalPlaces"]),
                            });
                        }

                        return reservations;
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
