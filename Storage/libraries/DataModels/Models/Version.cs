using System;
using System.Collections.Generic;
using System.Text;

namespace Technobee.Licensing.Storage.DataModels.Models
{
    public class Version
    {
        public int EditionID { get; private set; }
        public int ProductID { get; private set; }
        public string Value { get; private set; }
    }
}
