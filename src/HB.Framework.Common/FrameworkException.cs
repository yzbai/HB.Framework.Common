#nullable enable


namespace System
{
    public class FrameworkException : Exception
    {
        public virtual FrameworkExceptionType ExceptionType { get; set; }

        public FrameworkException(string? message) : base(message) { }

        public FrameworkException(string? message, Exception? innerException) : base(message, innerException) { }

        public FrameworkException() { }
    }

    public enum FrameworkExceptionType
    {
        Unkonwn = 0,
        Database = 1,
        EventBus = 2,
        KVStore = 3,
        AliyunSms = 4,
        AliyunOss = 5,
        RedisDatabase = 6,
        Authorization = 7,
        Identity = 8,
        Validation = 9,
        TCaptha = 10,
        Cache = 11,
        Client = 12,
    }
}

#nullable restore