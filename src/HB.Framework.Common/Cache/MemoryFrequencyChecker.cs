#nullable enable

using System;
using System.Collections.Concurrent;

namespace System
{
    public class MemoryFrequencyChecker
    {
        private readonly ConcurrentDictionary<string, long> _timestamps = new ConcurrentDictionary<string, long>();

        public bool Check(string resourceType, string resource, TimeSpan aliveTimeSpan)
        {
            string key = GetKey(resourceType, resource);

            long currentTimestampSeconds = TimeUtil.CurrentTimestampSeconds();

            if (!_timestamps.TryGetValue(key, out long storedTimestamp))
            {
                _timestamps[key] = currentTimestampSeconds;

                return true;
            }

            if (TimeUtil.CurrentTimestampSeconds() - storedTimestamp > aliveTimeSpan.TotalSeconds)
            {
                _timestamps[key] = currentTimestampSeconds;
                return true;
            }

            return false;
        }

        public void Reset(string resourceType, string resource)
        {
            string key = GetKey(resourceType, resource);

            _timestamps.TryRemove(key, out _);
        }

        private static string GetKey(string resourceType, string resource)
        {
            return resourceType + resource;
        }


    }
}
