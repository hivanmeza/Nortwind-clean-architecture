using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NorthWind.Entities.Enums;
using NorthWind.Entities.Exceptions;
using NorthWind.Entities.Interfaces;
using NorthWind.Entities.POCOEntities;

namespace NorthWind.UseCases.CreateOrder
{
    public class CreateOrderInteractor:IRequestHandler<CreateOrderInputPort,int>
    {
        readonly IOrderRepositoy OrderRepository;
        readonly IOrderDetailRepository OrderDetailRepository;
        readonly IUnitOfWork UnitOfWork;

        public CreateOrderInteractor(IOrderRepositoy orderRepository, IOrderDetailRepository orderDetailRepository,
            IUnitOfWork unitOfWork) => (OrderRepository, OrderDetailRepository, UnitOfWork) =
            (orderRepository, orderDetailRepository, unitOfWork);
        public async Task<int> Handle(CreateOrderInputPort request, CancellationToken cancellationToken)
        {
            Order Order = new Order
            {
                CustomerId = request.CustomerId,
                OrderDate = DateTime.Now,
                ShipAddress = request.ShipAddress,
                ShipCity = request.ShipCity,
                ShipCountry = request.ShipCountry,
                ShipPostalCode = request.ShipPostalCode,
                ShippingType = ShippingType.Road,
                DiscountType = DiscountType.Percentage,
                Discount = 10
            };
            OrderRepository.Crete(Order);
            foreach (var item in request.OrderDetails)
            {
                OrderDetailRepository.Create(
                    new OrderDetail
                    {
                        Order = Order,
                        ProductId = item.ProductId,
                        UnitPrice = item.UnitPrice,
                        Quantity = item.Quantity
                    }
                    );
            }

            try
            {
                await UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new GeneralException("Error al crear la orden", ex.Message);
            }

            return Order.Id;
        }
    }
}