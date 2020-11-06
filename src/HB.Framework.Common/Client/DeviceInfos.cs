using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace System
{
    public class DeviceInfos
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Model { get; set; } = null!;

        [Required]
        public string OSVersion { get; set; } = null!;

        [Required]
        public string Platform { get; set; } = null!;

        [Required]
        public DeviceIdiom Idiom { get; set; } = DeviceIdiom.Unknown;

        [Required]
        public string Type { get; set; } = null!;
    }
}
