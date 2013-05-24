using System.Collections.Generic;

namespace Abstracts.PersistenceDtos
{
    public class Customer
    {

        public Customer()
        {
            Addresses = new List<Address>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<Address> Addresses { get; private set; }

    }

    public class Address
    {
        public string FirstLine  { get; set; }
        public string SecondLine { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public AddressType AddressType { get; set; }

    }

    public enum AddressType
    {
        NotSet = 0,
        Billing = 1,
        Delivery = 2
    }
}