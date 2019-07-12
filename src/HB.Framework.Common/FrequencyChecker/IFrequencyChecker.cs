using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HB.Framework.Common
{
    public interface IFrequencyChecker
    {
        /// <summary>
        /// 检查这个资源是否可用
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="aliveTimeSpan"></param>
        /// <returns>返回true，表示可用；返回false，表示正被占用</returns>
        Task<bool> CheckAsync(string resourceType, string resource, TimeSpan aliveTimeSpan);

        /// <summary>
        /// 检查这个资源是否可用
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="aliveTimeSpan"></param>
        /// <returns>返回true，表示可用；返回false，表示正被占用</returns>
        bool Check(string resourceType, string resource, TimeSpan aliveTimeSpan);

        void Reset(string resourceType, string resource);

        Task ResetAsync(string resourceType, string resource);
    }
}
