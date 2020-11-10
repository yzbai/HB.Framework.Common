using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HB.Framework.Common
{
    public class EntityDto : ValidatableObject
    {
        [Required]
        public string Guid { get; set; } = null!;
    }
}
