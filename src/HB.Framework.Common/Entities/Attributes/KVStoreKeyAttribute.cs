using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HB.Framework.Common.Entities
{
    /// <summary>
    /// 可以多个Key
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class KVStoreKeyAttribute : System.Attribute
    {
        public int Order { get; set; }

        public KVStoreKeyAttribute(int order = 0)
        {
            Order = order;
        }
    }
}
