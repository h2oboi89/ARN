// AllianceReservationsNetwork/AllianceReservationsNetworkTests/ProgrammingTestFixture.cs
// Copyright © Daniel Waters 2015

namespace AllianceReservationsNetworkTests
{
    using System.IO;
    using AllianceReservationsNetwork;
    using NUnit.Framework;

    [TestFixture]
    public class ProgrammingTestFixture
    {
        private const string Street = "56 Main St";
        private const string City = "Mesa";
        private const string State = "AZ";
        private const string ZipCode = "85225";
        private const string FirstName = "John";
        private const string LastName = "Doe";
        private const string Name = "Alliance Reservations Network";

        [SetUp]
        public void Setup()
        {
            const string file = "db.txt";

            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        private static Address CreateAddress()
        {
            return new Address(Street, City, State, ZipCode);
        }

        [Test]
        public void AddressTest()
        {
            var address = CreateAddress();

            Assert.That(address.Street, Is.EqualTo(Street));
            Assert.That(address.City, Is.EqualTo(City));
            Assert.That(address.State, Is.EqualTo(State));
            Assert.That(address.ZipCode, Is.EqualTo(ZipCode));
        }

        [Test]
        public void CustomerTest()
        {
            var address = CreateAddress();

            var customer = new Customer(FirstName, LastName, address);

            Assert.That(customer.FirstName, Is.EqualTo(FirstName));
            Assert.That(customer.LastName, Is.EqualTo(LastName));
            Assert.That(customer.Address, Is.EqualTo(address));
        }

        [Test]
        public void CompanyTest()
        {
            var address = CreateAddress();

            var customer = new Company(Name, address);

            Assert.That(customer.Name, Is.EqualTo(Name));
            Assert.That(customer.Address, Is.EqualTo(address));
        }

        [Test]
        public void ProgrammerTest()
        {
            var address = CreateAddress();
            var customer = new Customer(FirstName, LastName, address);
            var company = new Company(Name, address);

            Assert.IsNullOrEmpty(customer.Id);
            customer.Save();
            Assert.IsNotNullOrEmpty(customer.Id);

            Assert.IsNullOrEmpty(company.Id);
            company.Save();
            Assert.IsNotNullOrEmpty(company.Id);

            var savedCustomer = Customer.Find(customer.Id);
            Assert.IsNotNull(savedCustomer);
            Assert.AreSame(customer.Address, address);
            Assert.AreEqual(savedCustomer.Address, address);
            Assert.AreEqual(customer.Id, savedCustomer.Id);
            Assert.AreEqual(customer.FirstName, savedCustomer.FirstName);
            Assert.AreEqual(customer.LastName, savedCustomer.LastName);
            Assert.AreEqual(customer, savedCustomer);
            Assert.AreNotSame(customer, savedCustomer);

            var savedCompany = Company.Find(company.Id);
            Assert.IsNotNull(savedCompany);
            Assert.AreSame(company.Address, address);
            Assert.AreEqual(savedCompany.Address, address);
            Assert.AreEqual(company.Id, savedCompany.Id);
            Assert.AreEqual(company.Name, savedCompany.Name);
            Assert.AreEqual(company, savedCompany);
            Assert.AreNotSame(company, savedCompany);

            customer.Delete();
            Assert.IsNullOrEmpty(customer.Id);
            Assert.IsNull(Customer.Find(customer.Id));

            company.Delete();
            Assert.IsNullOrEmpty(company.Id);
            Assert.IsNull(Customer.Find(company.Id));
        }
    }
}
