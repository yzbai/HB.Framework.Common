﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;

namespace HB.Framework.Common.Utility
{
    public static class ReflectUtil
    {
        public static IEnumerable<Assembly> GetAllAssemblies()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            return Directory
                .GetFiles(path, "*.dll")
                .Select(f => Assembly.LoadFile(f));
        }

        public static IEnumerable<Type> GetAllTypeByCondition(Func<Type, bool> condition)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            return Directory
                .GetFiles(path, "*.dll")
                .SelectMany(file => Assembly.LoadFile(file).GetTypes())
                .Where(t => condition(t));
        }

        //TODO: Use metacontext later when update into .net 3.0
        public static IEnumerable<Type> GetAllTypeByCondition(IList<string> assembliesToCheck, Func<Type, bool> condition)
        {
            return assembliesToCheck.ThrowIfNullOrEmpty(nameof(assembliesToCheck))
                .SelectMany(assemblyName => Assembly.Load(assemblyName).GetTypes())
                .Where(t => condition(t));
        }

        //public static string GetTypeSimpleName(this Type type)
        //{
        //    type.Name
        //}
    }
}
