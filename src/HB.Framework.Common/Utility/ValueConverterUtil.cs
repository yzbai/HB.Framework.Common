using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace System
{
    public static class ValueConverterUtil
    {
        private static readonly Dictionary<Type, Func<object, object>> _convertFunDict = new Dictionary<Type, Func<object, object>>();

        static ValueConverterUtil()
        {
            #region type to type

            _convertFunDict[typeof(byte)] = o => { return Convert.ToByte(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(sbyte)] = o => { return Convert.ToSByte(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(short)] = o => { return Convert.ToInt16(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(ushort)] = o => { return Convert.ToUInt16(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(int)] = o => { return Convert.ToInt32(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(uint)] = o => { return Convert.ToUInt32(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(long)] = o => { return Convert.ToInt64(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(ulong)] = o => { return Convert.ToUInt64(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(float)] = o => { return Convert.ToSingle(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(double)] = o => { return Convert.ToDouble(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(decimal)] = o => { return Convert.ToDecimal(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(bool)] = o => { return Convert.ToBoolean(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(string)] = o => { return Convert.ToString(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(char)] = o => { return Convert.ToChar(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(Guid)] = o => { return Guid.Parse(o.ToString()); };
            _convertFunDict[typeof(DateTime)] = o => { return Convert.ToDateTime(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(DateTimeOffset)] = o => { return (DateTimeOffset)DateTime.SpecifyKind(Convert.ToDateTime(o, GlobalSettings.Culture), DateTimeKind.Utc); };
            _convertFunDict[typeof(TimeSpan)] = o => { return Convert.ToDateTime(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(byte[])] = o => { return SerializeUtil.ToBytes(o); };
            _convertFunDict[typeof(byte?)] = o => { return o == null ? null : (object)Convert.ToByte(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(sbyte?)] = o => { return o == null ? null : (object)Convert.ToSByte(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(short?)] = o => { return o == null ? null : (object)Convert.ToInt16(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(ushort?)] = o => { return o == null ? null : (object)Convert.ToUInt16(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(int?)] = o => { return o == null ? null : (object)Convert.ToInt32(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(uint?)] = o => { return o == null ? null : (object)Convert.ToUInt32(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(long?)] = o => { return o == null ? null : (object)Convert.ToInt64(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(ulong?)] = o => { return o == null ? null : (object)Convert.ToUInt64(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(float?)] = o => { return o == null ? null : (object)Convert.ToSingle(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(double?)] = o => { return o == null ? null : (object)Convert.ToDouble(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(decimal?)] = o => { return o == null ? null : (object)Convert.ToDecimal(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(bool?)] = o => { return o == null ? null : (object)Convert.ToBoolean(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(char?)] = o => { return o == null ? null : (object)Convert.ToChar(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(Guid?)] = o => { return o == null ? null : (object)Guid.Parse(o.ToString()); };
            _convertFunDict[typeof(DateTime?)] = o => { return o == null ? null : (object)Convert.ToDateTime(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(DateTimeOffset?)] = o => { return o == null ? null : (DateTimeOffset?)DateTime.SpecifyKind(Convert.ToDateTime(o, GlobalSettings.Culture), DateTimeKind.Utc); };
            _convertFunDict[typeof(TimeSpan?)] = o => { return o == null ? null : (object)Convert.ToDateTime(o, GlobalSettings.Culture); };
            _convertFunDict[typeof(object)] = o => { return o ?? null; };
            _convertFunDict[typeof(DBNull)] = o => { return o == null ? null : DBNull.Value; };

            #endregion
        }

        /// <summary>
        /// 将数据库的值转换为内存C#值
        /// 用在从数据库查询后，数据库值转为类型值
        /// </summary>
        /// <returns>The value to type value.</returns>
        /// <param name="targetType">想要转成的C#类型</param>
        /// <param name="dbValue">Db value.</param>
        public static object DbValueToTypeValue(object dbValue, Type targetType)
        {
            ThrowIf.Null(targetType, nameof(targetType));
            ThrowIf.Null(dbValue, nameof(dbValue));

            if (dbValue.GetType() == typeof(DBNull))
            {
                return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
            }

            if (targetType.IsEnum)
            {
                return Enum.Parse(targetType, dbValue.ToString(), true);
                //return Convert.ToInt32(value, GlobalSettings.Culture);
            }

            if (targetType.IsAssignableFrom(typeof(IList<string>)))
            {
                return StringUtil.StringToList(dbValue.ToString());
            }

            if (targetType.IsAssignableFrom(typeof(IDictionary<string, string>)))
            {
                return StringUtil.StringToDictionary(dbValue.ToString());
            }

            Func<object, object> convertFn = _convertFunDict[targetType];
            return convertFn(dbValue);
        }

        /// <summary>
        /// 将C#值转换为字符串，便于拼接SQL字符串
        /// </summary>
        /// <returns>The value to db value.</returns>
        /// <param name="value">Value.</param>
        public static string TypeValueToStringValue(object value)
        {
            string valueStr;

            if (value != null)
            {
                Type type = value.GetType();

                if (type.IsEnum)
                {
                    valueStr = ((Enum)value).ToString();
                    //valueStr = ((Int32)value).ToString(GlobalSettings.Culture);
                }
                else if (typeof(IList<string>).IsAssignableFrom(type))
                {
                    valueStr = StringUtil.ListToString(value as IList<string>);
                }
                else if (typeof(IDictionary<string, string>).IsAssignableFrom(type))
                {
                    valueStr = StringUtil.DictionaryToString(value as IDictionary<string, string>);
                }
                else if (type == typeof(string))
                {
                    valueStr = (string)value;
                }
                else if (type == typeof(DateTime))
                {
                    valueStr = ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss", GlobalSettings.Culture);
                }
                else if (type == typeof(DateTimeOffset))
                {
                    valueStr = ((DateTimeOffset)value).ToString("yyyy-MM-dd HH:mm:ss", GlobalSettings.Culture);
                }
                else if (type == typeof(bool))
                {
                    valueStr = (bool)value ? "1" : "0";
                }
                else if (type == typeof(DBNull))
                {
                    valueStr = null;
                }
                else
                {
                    valueStr = value.ToString();
                }
            }
            else
            {
                valueStr = null;
            }

            return valueStr;
        }
    }
}
