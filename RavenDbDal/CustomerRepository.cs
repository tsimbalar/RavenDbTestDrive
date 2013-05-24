using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstracts;
using Abstracts.PersistenceDtos;
using Raven.Client;

namespace RavenDbDal
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDocumentSession _session;

        public CustomerRepository(IDocumentSession session)
        {
            _session = session;
        }

        public IEnumerable<Customer> List(int skip, int take)
        {
            return _session.Query<Customer>().Skip(skip).Take(take);
        }

        public void Add(Customer customer)
        {
            _session.Store(customer);
            //_session.SaveChanges();
        }

        public Customer GetById(int id)
        {
            return _session.Load<Customer>(id);
        }
    }
}
