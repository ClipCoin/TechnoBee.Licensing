using System;
using Technobee.Marketing.Core_Abstractions.Entities;

namespace Technobee.Licensing.Storage.DataModels.Models
{
    public class Product : Entity<int>
    {
        public string Guid { get; private set; }
        public string Name { get; private set; }
        public DateTime Timestamp { get; private set; }
    }
}
