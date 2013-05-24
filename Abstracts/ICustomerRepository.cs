using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstracts.PersistenceDtos;

namespace Abstracts
{
    public interface ICustomerRepository
    {

        IEnumerable<Customer> List(int skip, int take);

        void Add(Customer customer);
        Customer GetById(int id);
    }
}
