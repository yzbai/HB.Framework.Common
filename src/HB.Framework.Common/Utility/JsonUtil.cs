using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace System
{
    public static class JsonUtil
    {
        #region Json

        private static readonly JsonSerializer jsonSerializer = JsonSerializer.CreateDefault();

        public static string ToJson(object entity)
        {
            StringWriter stringWriter = new StringWriter(new StringBuilder(256), CultureInfo.InvariantCulture);
            using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
            {
                jsonTextWriter.Formatting = jsonSerializer.Formatting;
                jsonSerializer.Serialize(jsonTextWriter, entity);
            }
            return stringWriter.ToString();
        }

        public static T FromJson<T>(string jsonString)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return default;
            }

            using (JsonTextReader reader = new JsonTextReader(new StringReader(jsonString)))
            {
                return jsonSerializer.Deserialize<T>(reader);
            }
        }

        public static string FromJson(string jsonString, string name)
        {
            JObject jObject = JObject.Parse(jsonString);

            return jObject[name].ToString();
        }

        public static object FromJson(Type type, string jsonString)
        {
            if (string.IsNullOrWhiteSpace(jsonString) || type == null)
            {
                return default;
            }

            using (JsonTextReader reader = new JsonTextReader(new StringReader(jsonString)))
            {
                return jsonSerializer.Deserialize(reader, type);
            }
        }

        public static T FromStream<T>(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                return jsonSerializer.Deserialize<T>(jsonTextReader);
            }
        }

        #endregion

        #region object to bytes

        public static T DeSerialize<T>(byte[] buffer)
        {
            if (buffer == null)
            {
                return default;
            }

            return FromJson<T>(Encoding.UTF8.GetString(buffer));
        }

        public static object DeSerialize(Type type, byte[] buffer)
        {
            if (buffer == null)
            {
                return null;
            }

            return FromJson(type, Encoding.UTF8.GetString(buffer));
        }


        public static byte[] Serialize(object item)
        {
            if (item == null)
            {
                return null;
            }

            return Encoding.UTF8.GetBytes(ToJson(item));
        }
        #endregion
    }
}
