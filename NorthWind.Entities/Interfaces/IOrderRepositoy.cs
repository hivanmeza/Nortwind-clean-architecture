using System.Collections.Generic;
using NorthWind.Entities.POCOEntities;
using NorthWind.Entities.Specifications;

namespace NorthWind.Entities.Interfaces
{
    public interface IOrderRepositoy
    {
        void Crete(Order order);
        IEnumerable<Order> GetOrdersBySpecification(Specification<Order> specification);
    }
}