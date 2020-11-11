using System;

namespace HB.Framework.Common.Entities
{
    /// <summary>
    /// 可以多个Key
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class KVStoreKeyAttribute : System.Attribute
    {
        public KVStoreKeyAttribute()
        {
        }
    }
}