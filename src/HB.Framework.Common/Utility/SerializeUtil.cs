using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;

namespace System
{
    public static class SerializeUtil
    {
        #region Json

        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

        public static string ToJson(object entity)
        {
            ThrowIf.Null(entity, nameof(entity));

            return JsonSerializer.Serialize(entity, _jsonSerializerOptions);
            //return JsonConvert.SerializeObject(entity);

        }

        public static T FromJson<T>(string jsonString)
        {
            ThrowIf.NullOrEmpty(jsonString, nameof(jsonString));

            return JsonSerializer.Deserialize<T>(jsonString, _jsonSerializerOptions);
        }

        public static object FromJson(Type type, string jsonString)
        {
            ThrowIf.Null(type, nameof(type));
            ThrowIf.NullOrEmpty(jsonString, nameof(jsonString));

            return JsonSerializer.Deserialize(jsonString, type, _jsonSerializerOptions);

            //return JsonConvert.DeserializeObject(jsonString, type);
        }

        public static string FromJson(string jsonString, string name)
        {
            JsonDocument jsonDocument = JsonDocument.Parse(jsonString);

            JsonElement rootElement = jsonDocument.RootElement;

            if (rootElement.TryGetProperty(name, out JsonElement jsonElement))
            {
                return jsonElement.GetString();
            }

            return null;

            //JObject jObject = JObject.Parse(jsonString);

            //return jObject[name].ToString();
        }

        #endregion

        #region Binary Serialize

        public static byte[] ToBytes(object thing)
        {
            ThrowIf.Null(thing, nameof(thing));

            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using MemoryStream memoryStream = new MemoryStream();

            binaryFormatter.Serialize(memoryStream, thing);

            return memoryStream.ToArray();
        }

        public static object ToObject(byte[] bytes)
        {
            ThrowIf.NullOrEmpty(bytes, nameof(bytes));

            using MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            memoryStream.Write(bytes, 0, bytes.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return binaryFormatter.Deserialize(memoryStream);
        }

        #endregion
    }
}
