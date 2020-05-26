#nullable enable

using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace System
{
    public static class GlobalSettings
    {
        [NotNull]public static CultureInfo Culture { get; set; } = CultureInfo.InvariantCulture;

        [NotNull]public static StringComparison Comparison { get; set; } = StringComparison.InvariantCulture;

        [NotNull]public static StringComparison ComparisonIgnoreCase { get; set; } = StringComparison.InvariantCultureIgnoreCase;

        [MaybeNull, DisallowNull]public static ILogger? Logger { get; set; }

        public static readonly string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        public static readonly string DebugTag = "HB_HB_HB";

    }
}

#nullable restore