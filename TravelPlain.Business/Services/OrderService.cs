using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelPlain.Business.DTO;
using TravelPlain.Business.Infrastructure;
using TravelPlain.Business.Interfaces;
using TravelPlain.Data.Interfaces;
using TravelPlain.Data.Models;

namespace TravelPlain.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;

        public OrderService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        private decimal CalculatePrice(Tour tour, Data.Models.Profile profile)
        {
            if (tour == null)
            {
                throw new ValidationException("There is no tour with such id.", "TourId");
            }
            if (profile == null)
            {
                throw new ValidationException("User does not exist.", "UserId");
            }

            var lastBusinessValues = _uow.BusinessValues
                .Get()
                .OrderByDescending(o => o.TimeSet)
                .FirstOrDefault();

            if (lastBusinessValues == null)
            {
                throw new ValidationException("No business values set");
            }

            decimal price = tour.Price;

            if (profile.Orders != null && profile.Orders.Count(o => o.Status == Enums.OrderStatus.Payed) > 0)
            {
                int nextDiscount = profile.Orders.Count * lastBusinessValues.DiscountPercentIncrement;

                if (nextDiscount > lastBusinessValues.DiscountPercentCap)
                {
                    nextDiscount = lastBusinessValues.DiscountPercentCap;
                }

                price -= price * ((decimal)nextDiscount / 100);
            }

            return price;

        }

        private void ChangeOrderStatus(Order order, Enums.OrderStatus newStatus)
        {
            if (order == null)
            {
                throw new ValidationException("Order does not exist", "Order");
            }

            order.Status = newStatus;
        }

        /// <summary>
        /// Creates new order
        /// </summary>
        /// <param name="data">Order data</param>
        public void Place(OrderDTO data)
        {
            var tour = _uow.Tours.Get(data.TourId);
            if (tour == null)
            {
                throw new ValidationException("Tour does not exist", "TourId");
            }
            if (!tour.IsAvailable)
            {
                throw new ValidationException("Tour is not available", "TourId");
            }
            var profile = _uow.Profiles.Get(data.ProfileId);

            try
            {
                var order = Mapper.Map<Order>(data);
                order.Price = CalculatePrice(tour, profile);
                order.Status = Enums.OrderStatus.Registered;
                order.Time = DateTime.Now;

                _uow.Orders.Add(order);
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public void Cancel(int orderId, string profileId)
        {
            var order = _uow.Orders.Get(orderId);
            if (order == null)
            {
                throw new ValidationException("Order does not exist", "OrderId");
            }

            var profile = _uow.Profiles.Get(profileId);
            if (profile == null)
            {
                throw new ValidationException("Profile does not exist", "ProfileId");
            }

            if (profile.Id == order.ProfileId)
            {
                try
                {
                    ChangeOrderStatus(order, Enums.OrderStatus.Cancelled);
                }
                catch (ValidationException)
                {
                    throw;
                }
            }
            else
            {
                throw new ValidationException("This profile does not have order with such id", "Profile");
            }
        }

        public OrderDTO GetById(int id) =>
            Mapper.Map<OrderDTO>(_uow.Orders.Get(id));


        public IEnumerable<OrderDTO> GetByProfileId(string profileId) =>
            _uow.Profiles.
                Get(profileId)?.
                Orders?.
                ToList()?.
                Select(o => Mapper.Map<OrderDTO>(o));

        /// <summary>
        /// Calculates price for the next user's order of particular tour
        /// </summary>
        /// <param name="tourId">Tour ID</param>
        /// <param name="userId">User ID</param>
        /// <returns>Price for the next order</returns>
        public decimal CalculatePrice(int tourId, string userId)
        {
            try
            {
                return CalculatePrice(_uow.Tours.Get(tourId), _uow.Profiles.Get(userId));
            }
            catch (ValidationException)
            {
                throw;
            }
        }

        public int SaveChanges() => _uow.SaveChanges();
    }
}
