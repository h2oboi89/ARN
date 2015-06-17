// AllianceReservationsNetwork/AllianceReservationsNetwork/BaseClass.cs
// Copyright © Daniel Waters 2015

namespace AllianceReservationsNetwork
{
    using System.Collections.Generic;

    public abstract class BaseClass
    {
        protected const char Seperator = ':';
        public abstract IEnumerable<byte> ToBytes();
    }
}
