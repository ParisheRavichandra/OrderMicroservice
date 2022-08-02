using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Aggregates;
using OrderManagement.Domain.Aggregates.OrderAggregate;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Domain.Specifications;
using OrderManagement.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T:EntityBase,IAggregateRoot
    {
        private readonly OrderManagementContext context;

        public Repository(OrderManagementContext context)
        {
            this.context = context;
        }

        public T Add(T item)
        {
            return context.Add(item).Entity;
        }

       

        public IReadOnlyCollection<T> Get()
        {
            var Data = context.Set<T>().ToList();
            return Data.AsReadOnly();
        }

        
        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }
        public T GetById(long id)
        {
            return context.Set<T>().Find(id);
        }

        public IReadOnlyCollection<T> GetBySpec(SpecificationBase<T> spec)
        {
            IQueryable<T> Set = context.Set<T>();
            foreach (var include in spec.Includes)
                Set = Set.Include(include);
            var Query = Set.Where(spec.ToExpression().Compile());
            return Query.ToList().AsReadOnly();
        }

        public T Remove(T item)
        {
            return context.Remove(item).Entity;
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

        public T Update(T item)
        {
            return context.Update(item).Entity;
        }
        public Order_Item Add(Order_Item order_item)
        {
            return context.Add(order_item).Entity;
        }

      
    }

    

    
}
