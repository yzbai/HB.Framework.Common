﻿#nullable enable

using System;
using System.Collections.Generic;

namespace HB.Framework.Common.Api
{
    public class ApiError
    {
        public ErrorCode Code { get; set; }

        public string? Message { get; set; }

        public IDictionary<string, IEnumerable<string>> ModelStates { get; private set; } = new Dictionary<string, IEnumerable<string>>();

        public ApiError()
        {
        }

        public ApiError(ErrorCode code, string? message = null)
        {
            Code = code;
            Message = message ?? code.ToString();
        }

        public ApiError(ErrorCode code, IDictionary<string, IEnumerable<string>> modelStates) : this(code: code, message: null)
        {
            modelStates.ForEach(kv =>
            {
                ModelStates.Add(kv.Key, new List<string>(kv.Value));
            });
        }
    }
}