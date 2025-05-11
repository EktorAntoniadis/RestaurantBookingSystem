using RestaurantBookingSystem.Data.Models;
using RestaurantBookingSystem.Operations.Repositories.Interfaces;
using RestaurantBookingSystem.Services.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBookingSystem.Services.BusinessLogic.Implementations
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUserRepository _userRepository;
        public RestaurantService(
            IRestaurantRepository restaurantRepository,
            IUserRepository userRepository
            )
        {
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
            _userRepository = userRepository;
        }

        public void CancelReservation(int id)
        {
            var reservation = _restaurantRepository.GetReservationById(id);
            var reservationStatuses = _restaurantRepository.GetReservationStatuses();
            var cancelledStatuts = reservationStatuses.FirstOrDefault(x => x.Status == "Cancelled");

            var orderStatuses = _restaurantRepository.GetOrderStatuses();
            var cancelledOrder = orderStatuses.FirstOrDefault(x => x.Status == "Cancelled");

            reservation.ReservationStatus = cancelledStatuts;
            reservation.TableOrder.OrderStatus = cancelledOrder;

            _restaurantRepository.UpdateReservation(reservation);
        }

        public void ChangeTableStatus(int tableId, string status)
        {
            throw new NotImplementedException();
        }

        public void ConfirmReservation(int id)
        {
            var reservation = _restaurantRepository.GetReservationById(id);
            var reservationStatuses = _restaurantRepository.GetReservationStatuses();
            var confirmedStatus = reservationStatuses.FirstOrDefault(x => x.Status == "Confirmed");

            reservation.ReservationStatus = confirmedStatus;

            _restaurantRepository.UpdateReservation(reservation);
        }

        public bool SaveNewResevation(Reservation reservation)
        {
            if (reservation.RestaurantId == 0)
            {
                throw new ArgumentException("Reservation is not associated with the restaurant");
            }

            if (reservation.TableOrder.TableId == 0)
            {
                throw new ArgumentException("Reservation must be associated with a table");
            }

            try
            {
                var customer = _userRepository.FindCustomer(reservation.Customer);
                if(customer != null)
                {
                    reservation.Customer = null;
                    reservation.CustomerId = customer.Id;
                }
                else
                {
                    var newCustomer = reservation.Customer;
                    newCustomer.RegistrationDate = DateOnly.FromDateTime(DateTime.Today);
                    newCustomer.RegistrationTime = TimeOnly.FromDateTime(DateTime.Now);
                    newCustomer.Email = "-";
                    newCustomer.Username = "-";
                    newCustomer.Password = "-";
                    reservation.Customer = newCustomer;
                }

                _restaurantRepository.AddReservation(reservation);
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
    }
}
