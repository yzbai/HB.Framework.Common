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

    }
}

#nullable restore