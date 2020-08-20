﻿#nullable enable

using MsgPack.Serialization;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace System
{
    public static class SerializeUtil
    {
        #region Json
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions();

        static SerializeUtil()
        {
            _jsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            _jsonSerializerOptions.Converters.Add(new DictionaryTKeyEnumTValueConverter());
            _jsonSerializerOptions.PropertyNameCaseInsensitive = true;
        }

        public static string ToJson(object entity)
        {
            return JsonSerializer.Serialize(entity, _jsonSerializerOptions);
        }

        [return: MaybeNull]
        public static T FromJson<T>(string jsonString)
        {
            return jsonString.IsNullOrEmpty() ? default : JsonSerializer.Deserialize<T>(jsonString, _jsonSerializerOptions);
        }

        /// <summary>
        /// FromJsonAsync
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="responseStream"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        /// <exception cref="System.ArgumentNullException">Ignore.</exception>
        public static async Task<object> FromJsonAsync(Type dataType, Stream responseStream)
        {
            return await JsonSerializer.DeserializeAsync(responseStream, dataType, _jsonSerializerOptions).ConfigureAwait(false);
        }

        public static async Task<T> FromJsonAsync<T>(Stream responseStream)
        {
            return await JsonSerializer.DeserializeAsync<T>(responseStream, _jsonSerializerOptions).ConfigureAwait(false);
        }

        /// <summary>
        /// FromJson
        /// </summary>
        /// <param name="type"></param>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        /// <exception cref="System.ArgumentNullException">Ignore.</exception>
        public static object? FromJson(Type type, string jsonString)
        {
            if (jsonString.IsNullOrEmpty())
            {
                return null;
            }

            return JsonSerializer.Deserialize(jsonString, type, _jsonSerializerOptions);
        }

        /// <summary>
        /// FromJson
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="System.ArgumentException">Ignore.</exception>
        public static string? FromJson(string jsonString, string name)
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

        //https://blog.marcgravell.com/2020/03/why-do-i-rag-on-binaryformatter.html

        /// <summary>
        /// ToBytes
        /// </summary>
        /// <param name="thing"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        [Obsolete("Do not use BinaryFormatter, for reason https://blog.marcgravell.com/2020/03/why-do-i-rag-on-binaryformatter.html", true)]
        public static byte[] ToBytes(object thing)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using MemoryStream memoryStream = new MemoryStream();

            binaryFormatter.Serialize(memoryStream, thing);

            return memoryStream.ToArray();
        }

        /// <summary>
        /// ToObject
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        [Obsolete("Do not use BinaryFormatter, for reason https://blog.marcgravell.com/2020/03/why-do-i-rag-on-binaryformatter.html", true)]
        public static object? ToObject(byte[] bytes)
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

        /// <summary>
        /// PackAsync
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <exception cref="System.Runtime.Serialization.SerializationException"></exception>
        public static async Task<byte[]> PackAsync<T>([DisallowNull] T t) where T : class
        {
            MessagePackSerializer<T> serializer = MessagePackSerializer.Get<T>();

            return await serializer.PackSingleObjectAsync(t).ConfigureAwait(false);
        }

        /// <summary>
        /// UnPack
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        /// <exception cref="System.Runtime.Serialization.SerializationException"></exception>
        /// <exception cref="MsgPack.MessageTypeException"></exception>
        /// <exception cref="MsgPack.InvalidMessagePackStreamException"></exception>
        /// <exception cref="System.ArgumentNullException">Ignore.</exception>
        public static async Task<T?> UnPackAsync<T>(byte[]? bytes) where T : class
        {
            if (bytes.IsNullOrEmpty())
            {
                return null;
            }

            MessagePackSerializer<T> serializer = MessagePackSerializer.Get<T>();

            return await serializer.UnpackSingleObjectAsync(bytes).ConfigureAwait(false);
        }

        #endregion
    }

    public class DictionaryTKeyEnumTValueConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (!typeToConvert.IsGenericType)
            {
                return false;
            }

            if (typeToConvert.GetGenericTypeDefinition() != typeof(Dictionary<,>))
            {
                return false;
            }

            return typeToConvert.GetGenericArguments()[0].IsEnum;
        }

        public override JsonConverter CreateConverter(
            Type type,
            JsonSerializerOptions options)
        {
            Type keyType = type.GetGenericArguments()[0];
            Type valueType = type.GetGenericArguments()[1];

            JsonConverter converter = (JsonConverter)Activator.CreateInstance(
                typeof(DictionaryEnumConverterInner<,>).MakeGenericType(
                    new Type[] { keyType, valueType }),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                args: new object[] { options },
                culture: null);

            return converter;
        }

        private class DictionaryEnumConverterInner<TKey, TValue> :
            JsonConverter<Dictionary<TKey, TValue>> where TKey : struct, Enum
        {
            private readonly JsonConverter<TValue> _valueConverter;
            private Type _keyType;
            private Type _valueType;

            public DictionaryEnumConverterInner(JsonSerializerOptions options)
            {
                // For performance, use the existing converter if available.
                _valueConverter = (JsonConverter<TValue>)options
                    .GetConverter(typeof(TValue));

                // Cache the key and value types.
                _keyType = typeof(TKey);
                _valueType = typeof(TValue);
            }

            public override Dictionary<TKey, TValue> Read(
                ref Utf8JsonReader reader,
                Type typeToConvert,
                JsonSerializerOptions options)
            {
                if (reader.TokenType != JsonTokenType.StartObject)
                {
                    throw new JsonException();
                }

                Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        return dictionary;
                    }

                    // Get the key.
                    if (reader.TokenType != JsonTokenType.PropertyName)
                    {
                        throw new JsonException();
                    }

                    string propertyName = reader.GetString();

                    // For performance, parse with ignoreCase:false first.
                    if (!Enum.TryParse(propertyName, ignoreCase: false, out TKey key) &&
                        !Enum.TryParse(propertyName, ignoreCase: true, out key))
                    {
                        throw new JsonException(
                            $"Unable to convert \"{propertyName}\" to Enum \"{_keyType}\".");
                    }

                    // Get the value.
                    TValue v;
                    if (_valueConverter != null)
                    {
                        reader.Read();
                        v = _valueConverter.Read(ref reader, _valueType, options);
                    }
                    else
                    {
                        v = JsonSerializer.Deserialize<TValue>(ref reader, options);
                    }

                    // Add to dictionary.
                    dictionary.Add(key, v);
                }

                throw new JsonException();
            }

            public override void Write(
                Utf8JsonWriter writer,
                Dictionary<TKey, TValue> dictionary,
                JsonSerializerOptions options)
            {
                writer.WriteStartObject();

                foreach (KeyValuePair<TKey, TValue> kvp in dictionary)
                {
                    writer.WritePropertyName(kvp.Key.ToString());

                    if (_valueConverter != null)
                    {
                        _valueConverter.Write(writer, kvp.Value, options);
                    }
                    else
                    {
                        JsonSerializer.Serialize(writer, kvp.Value, options);
                    }
                }

                writer.WriteEndObject();
            }
        }
    }

    public class IntToStringConverter : JsonConverter<int>
    {
        /// <summary>
        /// Read
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="type"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">Ignore.</exception>
        public override int Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            try
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    ReadOnlySpan<byte> span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;

                    if (Utf8Parser.TryParse(span, out int number, out int bytesConsumed) && span.Length == bytesConsumed)
                    {
                        return number;
                    }

                    if (int.TryParse(reader.GetString(), out number))
                    {
                        return number;
                    }
                }

                return reader.GetInt32();
            }
            catch (InvalidOperationException)
            {
                return 0;
            }
        }

        /// <summary>
        /// Write
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <exception cref="System.ArgumentException">Ignore.</exception>
        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            try
            {
                writer.WriteStringValue(value.ToString(GlobalSettings.Culture));
            }
            catch (InvalidOperationException) { }
        }
    }

    public class DoubleToStringConverter : JsonConverter<double>
    {
        /// <summary>
        /// Read
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    ReadOnlySpan<byte> span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;

                    if (Utf8Parser.TryParse(span, out double number, out int bytesConsumed) && span.Length == bytesConsumed)
                    {
                        return number;
                    }

                    if (double.TryParse(reader.GetString(), out number))
                    {
                        return number;
                    }
                }

                return reader.GetDouble();
            }
            catch (InvalidOperationException)
            {
                return 0;
            }
        }

        /// <summary>
        /// Write
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <exception cref="System.ArgumentException">Ignore.</exception>
        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            try
            {
                writer.WriteStringValue(value.ToString(GlobalSettings.Culture));
            }
            catch (InvalidOperationException) { }
        }
    }
}
