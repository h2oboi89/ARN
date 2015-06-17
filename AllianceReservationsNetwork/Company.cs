// AllianceReservationsNetwork/AllianceReservationsNetwork/Company.cs
// Copyright © Daniel Waters 2015

namespace AllianceReservationsNetwork
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Company : Entity
    {
        public const string CompanyType = "COMPANY";
        public readonly Address Address;
        public readonly string Name;

        public Company(string name, Address address)
        {
            this.Type = CompanyType;
            this.Name = name;
            this.Address = address;
        }

        public Company(string line) : base(line)
        {
            var parts = line.Split(Seperator).Skip(2).ToList();

            this.Name = parts[0];

            this.Address = new Address(string.Join(Seperator.ToString(), parts.Skip(1).ToArray()));
        }

        public override bool Equals(object obj)
        {
            var company = obj as Company;

            if (company == null)
            {
                return false;
            }

            return company.Name.Equals(this.Name) && company.Address.Equals(this.Address);
        }

        public new static Company Find(string id)
        {
            return Entity.Find(id) as Company;
        }

        public override IEnumerable<byte> ToBytes()
        {
            var bytes = new List<byte>();

            bytes.AddRange(Encoding.ASCII.GetBytes(this.Name));
            bytes.AddRange(this.Address.ToBytes());

            return bytes;
        }

        public override string ToString()
        {
            return string.Join(Seperator.ToString(), new[] {base.ToString(), this.Name, this.Address.ToString()});
        }
    }
}
