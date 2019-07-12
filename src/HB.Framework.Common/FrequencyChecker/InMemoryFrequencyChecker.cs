using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HB.Framework.Common
{
    public class InMemoryFrequencyChecker : IFrequencyChecker
    {
        private ConcurrentDictionary<string, long> timestamps = new ConcurrentDictionary<string, long>();

        public Task<bool> CheckAsync(string resourceType, string resource, TimeSpan aliveTimeSpan)
        {
            return Task.FromResult(Check(resourceType, resource, aliveTimeSpan));
        }

        public bool Check(string resourceType, string resource, TimeSpan aliveTimeSpan)
        {
            string key = GetKey(resourceType, resource);

            long currentTimestampSeconds = TimeUtil.CurrentTimestampSeconds();

            if (!timestamps.TryGetValue(key, out long storedTimestamp))
            {
                timestamps[key] = currentTimestampSeconds;

                return true;
            }

            if (TimeUtil.CurrentTimestampSeconds() - storedTimestamp > aliveTimeSpan.TotalSeconds)
            {
                timestamps[key] = currentTimestampSeconds;
                return true;
            }

            return false;
        }

        public void Reset(string resourceType, string resource)
        {
            string key = GetKey(resourceType, resource);

            timestamps.TryRemove(key, out _);
        }

        private static string GetKey(string resourceType, string resource)
        {
            return resourceType + resource;
        }

        public Task ResetAsync(string resourceType, string resource)
        {
            Reset(resourceType, resource);
            return Task.FromResult(0);
        }
    }
}
