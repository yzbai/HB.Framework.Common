using System;
using System.Collections.Generic;
using System.Text;

namespace HB.Framework.Common.Entities
{
    [AttributeUsage(AttributeTargets.Class)]
    public class KVStoreEntitySchemaAttribute : Attribute
    {
        public string? InstanceName { get; set; }
    }
}
