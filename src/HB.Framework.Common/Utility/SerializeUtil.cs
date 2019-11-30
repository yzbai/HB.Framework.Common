using MsgPack.Serialization;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace System
{
    public static class SerializeUtil
    {
        #region Json
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions();

        static SerializeUtil()
        {
            _jsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(null, false));
        }

        public static string ToJson(object entity)
        {
            ThrowIf.Null(entity, nameof(entity));

            return JsonSerializer.Serialize(entity, _jsonSerializerOptions);
        }

        public static T FromJson<T>(string jsonString)
        {
            return jsonString.IsNullOrEmpty() ? default : JsonSerializer.Deserialize<T>(jsonString, _jsonSerializerOptions);
        }

        public static object FromJson(Type type, string jsonString)
        {
            if (jsonString.IsNullOrEmpty())
            {
                return null;
            }

            ThrowIf.Null(type, nameof(type));

            return JsonSerializer.Deserialize(jsonString, type, _jsonSerializerOptions);
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
        }

        #endregion

        #region BinaryFormatter Serialize

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
            if (bytes.IsNullOrEmpty())
            {
                return null;
            }

            using MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            memoryStream.Write(bytes, 0, bytes.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return binaryFormatter.Deserialize(memoryStream);
        }

        #endregion


        #region MsgPack Serialize

        public static byte[] Pack<T>(T t)
        {
            MessagePackSerializer<T> serializer = MessagePackSerializer.Get<T>();
            using MemoryStream stream = new MemoryStream();

            serializer.Pack(stream, t);

            return stream.ToArray();
        }

        public static T UnPack<T>(byte[] bytes)
        {
            if(bytes.IsNullOrEmpty())
            {
                return default;
            }

            MessagePackSerializer<T> serializer = MessagePackSerializer.Get<T>();
            using MemoryStream stream = new MemoryStream(bytes);

            return serializer.Unpack(stream);
        }

        #endregion
    }

    public class IntToStringConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                ReadOnlySpan<byte> span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
                if (Utf8Parser.TryParse(span, out int number, out int bytesConsumed) && span.Length == bytesConsumed)
                {
                    return number;
                }

                if (Int32.TryParse(reader.GetString(), out number))
                {
                    return number;
                }
            }
            return reader.GetInt32();
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            ThrowIf.Null(writer, nameof(writer));

            writer.WriteStringValue(value.ToString(GlobalSettings.Culture));
        }
    }

    public class DoubleToStringConverter : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                ReadOnlySpan<byte> span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;

                if (Utf8Parser.TryParse(span, out double number, out int bytesConsumed) && span.Length == bytesConsumed)
                {
                    return number;
                }

                if (Double.TryParse(reader.GetString(), out number))
                {
                    return number;
                }
            }

            return reader.GetDouble();
        }

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            ThrowIf.Null(writer, nameof(writer));

            writer.WriteStringValue(value.ToString(GlobalSettings.Culture));
        }
    }
}
