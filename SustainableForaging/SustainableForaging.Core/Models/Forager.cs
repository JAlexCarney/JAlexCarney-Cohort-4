using System;

namespace SustainableForaging.Core.Models
{
    public class Forager
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string State { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Forager forager &&
                   Id == forager.Id &&
                   FirstName == forager.FirstName &&
                   LastName == forager.LastName &&
                   State == forager.State;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FirstName, LastName, State);
        }
    }
}
