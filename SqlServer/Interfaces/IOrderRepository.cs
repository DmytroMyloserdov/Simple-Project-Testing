using SqlServer.Entities;
using System;
using System.Collections.Generic;

namespace SqlServer.Interfaces
{
    public interface IOrderRepository
    {
        Order GetById(Guid id);

        IEnumerable<Order> GetAll();

        void Add(Order item);

        void SaveChanges();
    }
}
