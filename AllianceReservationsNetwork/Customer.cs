// AllianceReservationsNetwork/AllianceReservationsNetwork/Customer.cs
// Copyright © Daniel Waters 2015

namespace AllianceReservationsNetwork
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Customer : Entity
    {
        public const string CustomerType = "CUSTOMER";
        public readonly Address Address;
        public readonly string FirstName;
        public readonly string LastName;

        public Customer(string firstName, string lastName, Address address)
        {
            this.Type = CustomerType;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
        }

        public Customer(string line) : base(line)
        {
            var parts = line.Split(Seperator).Skip(2).ToList();

            this.FirstName = parts[0];

            this.LastName = parts[1];

            this.Address = new Address(string.Join(Seperator.ToString(), parts.Skip(2).ToArray()));
        }

        public override bool Equals(object obj)
        {
            var customer = obj as Customer;

            if (customer == null)
            {
                return false;
            }

            return customer.FirstName.Equals(this.FirstName) && customer.LastName.Equals(this.LastName) &&
                   customer.Address.Equals(this.Address);
        }

        public new static Customer Find(string id)
        {
            return Entity.Find(id) as Customer;
        }

        public override IEnumerable<byte> ToBytes()
        {
            var bytes = new List<byte>();

            bytes.AddRange(Encoding.ASCII.GetBytes(this.FirstName));
            bytes.AddRange(Encoding.ASCII.GetBytes(this.LastName));
            bytes.AddRange(this.Address.ToBytes());

            return bytes;
        }

        public override string ToString()
        {
            return string.Join(Seperator.ToString(),
                new[] {base.ToString(), this.FirstName, this.LastName, this.Address.ToString()});
        }
    }
}
