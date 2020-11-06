using System;
using System.Collections.Generic;
using System.Text;

namespace System
{

    public class DeviceInfos
    {
        private const string _seprator = "_-_";

        public string Name { get; set; } = "UnKown";

        public string Model { get; set; } = "UnKown";

        public string OSVersion { get; set; } = "UnKown";

        public string Platform { get; set; } = "UnKown";

        public DeviceIdiom Idiom { get; set; } = DeviceIdiom.Unknown;

        public string Type { get; set; } = "UnKown";

        public override string ToString()
        {
            return $"{Name}{_seprator}{Model}{_seprator}{OSVersion}{_seprator}{Platform}{_seprator}{Idiom}{_seprator}{Type}";
        }

        public static DeviceInfos? FromString(string? infoString)
        {
            if (string.IsNullOrEmpty(infoString))
            {
                return null;
            }

            string[] splits = infoString.Split(_seprator, StringSplitOptions.RemoveEmptyEntries);

            if (splits.Length != 6)
            {
                return new DeviceInfos();
            }

            if (!Enum.TryParse<DeviceIdiom>(splits[4], out DeviceIdiom idiom))
            {
                idiom = DeviceIdiom.Unknown;
            }

            return new DeviceInfos
            {
                Name = splits[0],
                Model = splits[1],
                OSVersion = splits[2],
                Platform = splits[3],
                Idiom = idiom,
                Type = splits[5]
            };
        }
    }
}
