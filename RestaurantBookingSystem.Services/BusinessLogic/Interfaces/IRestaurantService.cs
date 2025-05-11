using RestaurantBookingSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBookingSystem.Services.BusinessLogic.Interfaces
{
    public interface IRestaurantService
    {
        public bool SaveNewResevation(Reservation reservation);

        public void ChangeTableStatus(int tableId, string status);

        public void CancelReservation(int id);

        public void ConfirmReservation(int id);
    }
}
