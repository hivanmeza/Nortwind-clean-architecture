using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthWind.Entities.Interfaces;
using NorthWind.Entities.POCOEntities;
using NorthWind.Entities.Specifications;
using NorthWind.Repositories.EFCore.Datacontext;

namespace NorthWind.Repositories.EFCore.Repositories
{
    public class OrderRepository : IOrderRepositoy
    {
        private readonly NorthWindContext Context;
        public OrderRepository(NorthWindContext context) => Context = context;
        public void Crete(Order order)
        {
            Context.Add(order);
        }

        public IEnumerable<Order> GetOrdersBySpecification(Specification<Order> specification)
        {
            var ExpressionDelegate = specification.Expression.Compile();

            return Context.Orders.Where(ExpressionDelegate);
        }
    }
}
