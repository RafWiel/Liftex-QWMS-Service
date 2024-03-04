using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QWMS.Models.Reservations;

namespace WinService.Interfaces
{
    #nullable enable

    public interface IReservationsService
    {
        Task<List<ReservationListModel>?> Get(int productId, int? page);        
    }
}
