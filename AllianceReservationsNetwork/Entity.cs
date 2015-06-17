// AllianceReservationsNetwork/AllianceReservationsNetwork/Entity.cs
// Copyright © Daniel Waters 2015

namespace AllianceReservationsNetwork
{
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public abstract class Entity : BaseClass
    {
        private const string DataBaseFile = "db.txt";
        private const string EntityType = "ENTITY";
        public string Id { get; private set; }
        protected string Type { private get; set; }

        protected Entity()
        {
            this.Type = EntityType;
        }

        protected Entity(string line)
        {
            var parts = line.Split(Seperator);

            this.Id = parts[0];

            this.Type = parts[1];
        }

        public override string ToString()
        {
            return string.Join(Seperator.ToString(), new[] {this.Id, this.Type});
        }

        public override int GetHashCode()
        {
            return this.ToBytes().Aggregate(0, (current, b) => current + b);
        }

        private void GenerateId()
        {
            using (var sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(this.ToBytes().ToArray());

                var id = new StringBuilder();

                id.Append("0x");

                foreach (var b in hash)
                {
                    id.Append(b.ToString("X2"));
                }

                this.Id = id.ToString();
            }
        }

        public void Save()
        {
            this.GenerateId();

            if (Find(this.Id) != null)
            {
                this.Delete();
            }

            using (var writer = File.AppendText(DataBaseFile))
            {
                writer.WriteLine(this.ToString());
            }
        }

        public void Delete()
        {
            File.WriteAllLines(DataBaseFile, File.ReadAllLines(DataBaseFile).Where(l => l.Split(Seperator).First() != this.Id).ToArray());

            this.Id = null;
        }

        protected static Entity Find(string id)
        {
            using (var reader = new StreamReader(File.Open(DataBaseFile, FileMode.OpenOrCreate)))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(Seperator);

                    if (parts[0] == id)
                    {
                        switch (parts[1])
                        {
                            case Customer.CustomerType:
                                return new Customer(line);
                            case Company.CompanyType:
                                return new Company(line);
                        }
                    }
                }
            }

            return null;
        }
    }
}
