// AllianceReservationsNetwork/AllianceReservationsNetwork/Address.cs
// Copyright © Daniel Waters 2015

namespace AllianceReservationsNetwork
{
    using System.Collections.Generic;
    using System.Text;

    public class Address : BaseClass
    {
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string ZipCode { get; }

        public Address(string street, string city, string state, string zipCode)
        {
            this.Street = street;
            this.City = city;
            this.State = state;
            this.ZipCode = zipCode;
        }

        public Address(string line)
        {
            var parts = line.Split(Seperator);

            this.Street = parts[0];
            this.City = parts[1];
            this.State = parts[2];
            this.ZipCode = parts[3];
        }

        public override bool Equals(object obj)
        {
            var address = obj as Address;

            if (address == null)
            {
                return false;
            }

            return address.Street.Equals(this.Street) && address.City.Equals(this.City) &&
                   address.State.Equals(this.State) && address.ZipCode.Equals(this.ZipCode);
        }

        public override IEnumerable<byte> ToBytes()
        {
            var bytes = new List<byte>();

            bytes.AddRange(Encoding.ASCII.GetBytes(this.Street));
            bytes.AddRange(Encoding.ASCII.GetBytes(this.City));
            bytes.AddRange(Encoding.ASCII.GetBytes(this.State));
            bytes.AddRange(Encoding.ASCII.GetBytes(this.ZipCode));

            return bytes;
        }

        public override string ToString()
        {
            return string.Join(Seperator.ToString(), new[] {this.Street, this.City, this.State, this.ZipCode});
        }
    }
}
