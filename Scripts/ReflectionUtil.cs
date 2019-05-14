using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

namespace CsHotFix
{
    public static class ReflectionUtil
    {
        public static readonly string[] CommonAsmExceptions =
        {
            "^Mono\\.*",
            "^Boo\\.Lang.*$",
            "^System.*$",
            "^I18N",
            "^Unity.*$",
            "mscorlib"
        };

        private static List<string> skipAsms = new List<string>();

        public static Type[] FindTypes(IEnumerable<string> fullQualifiedNames,
            IEnumerable<string> exceptionAsmExps = null)
        {
            var result = new List<Type>();
            var asms = AppDomain.CurrentDomain.GetAssemblies();
            var fullNames = fullQualifiedNames is string[]
                ? fullQualifiedNames as string[]
                : fullQualifiedNames.ToArray();
            foreach (var asm in asms)
            {
                if (skipAsms.Contains(asm.FullName))
                    continue;
                if (exceptionAsmExps != null)
                {
                    bool isException = false;
                    var asmName = asm.GetName().Name;
                    foreach (var exp in exceptionAsmExps)
                    {
                        if (Regex.IsMatch(asmName, exp))
                        {
                            isException = true;
                            break;
                        }
                    }

                    if (isException)
                        continue;
                }

                Type[] types = null;
                try
                {
                    types = asm.GetTypes();
                }
                catch (Exception e)
                {
                    Debug.Log("无法加载程序集内的类： " + asm.FullName);
                    Debug.LogException(e);
                    skipAsms.Add(asm.FullName);
                }

                if (types == null)
                    continue;
                foreach (var type in types)
                {
                    if (fullNames.Contains(type.FullName))
                    {
                        result.Add(type);
                    }
                }
            }

            return result.ToArray();
        }

        public static Type FindType(string fullQualifiedName, IEnumerable<string> exceptionAsmExps = null)
        {
            var result = FindTypes(new[] {fullQualifiedName}, exceptionAsmExps);
            return result.Length > 0 ? result[0] : null;
        }

        /// <summary>
        /// 从所有的程序集(除去系统程序集)中找到对应类型/接口的所有子类/实现者的"实例","实例","实例",重要的事情说三遍
        /// </summary>
        /// <typeparam name="T">源父类/接口</typeparam>
        /// <returns></returns>
        public static IEnumerable<T> FindSubClassesInAllAssemblies<T>() where T : class
        {
            var list = new List<T>();
            var asms = AppDomain.CurrentDomain.GetAssemblies();
            var type = typeof(T);
            FindSubClassesInAssembliesInternal<T>(asms, type, list, true, CommonAsmExceptions);
            return list;
        }

        /// <summary>
        /// 从类型"T"的程序集中找到对应类型/接口的所有子类/实现者的实例的"实例","实例","实例",重要的事情说三遍
        /// </summary>
        /// <typeparam name="T">源父类/接口</typeparam>
        /// <returns></returns>
        public static IEnumerable<T> FindSubClassesInTypeAssembly<T>() where T : class
        {
            var list = new List<T>();
            var t = typeof(T);
            FindSubClassesInAssembliesInternal<T>(new[] {t.Assembly}, t, list);
            return list;
        }

        /// <summary>
        /// 从指定的程序集中找到对应类型/接口的所有子类/实现者的实例的"实例","实例","实例",重要的事情说三遍
        /// </summary>
        /// <typeparam name="T">源父类/接口</typeparam>
        /// <param name="asms">程序集列表</param>
        /// <param name="exceptionAsmExps">例外的程序集正则匹配模板</param>
        /// <returns></returns>
        public static IEnumerable<T> FindSubClassesInAssemblies<T>(Assembly[] asms,
            IEnumerable<string> exceptionAsmExps = null) where T : class
        {
            var list = new List<T>();
            var t = typeof(T);
            FindSubClassesInAssembliesInternal<T>(asms, t, list, true, exceptionAsmExps);
            return list;
        }

        /// <summary>
        /// 从所有的程序集(除去系统程序集)中找到对应类型/接口的所有子类/实现者的"类","类","类",重要的事情说三遍
        /// </summary>
        /// <typeparam name="T">源父类/接口</typeparam>
        /// <returns></returns>
        public static IEnumerable<Type> FindSubTypesInAllAssemblies<T>() where T : class
        {
            var list = new List<Type>();
            var asms = AppDomain.CurrentDomain.GetAssemblies();
            var type = typeof(T);
            FindSubClassesInAssembliesInternal<T>(asms, type, list, false, CommonAsmExceptions);
            return list;
        }

        /// <summary>
        /// 从类型"T"的程序集中找到对应类型/接口的所有子类/实现者的实例的"类","类","类",重要的事情说三遍
        /// </summary>
        /// <typeparam name="T">源父类/接口</typeparam>
        /// <returns></returns>
        public static IEnumerable<Type> FindSubTypesInTypeAssembly<T>() where T : class
        {
            var list = new List<Type>();
            var t = typeof(T);
            FindSubClassesInAssembliesInternal<T>(new[] {t.Assembly}, t, list, false);
            return list;
        }

        /// <summary>
        /// 从指定的程序集中找到对应类型/接口的所有子类/实现者的实例的"类","类","类",重要的事情说三遍
        /// </summary>
        /// <typeparam name="T">源父类/接口</typeparam>
        /// <returns></returns>
        public static IEnumerable<Type> FindSubTypesInAssemblies<T>(Assembly[] asms,
            IEnumerable<string> exceptionAsmExps = null) where T : class
        {
            var list = new List<Type>();
            var t = typeof(T);
            FindSubClassesInAssembliesInternal<T>(asms, t, list, false, exceptionAsmExps);
            return list;
        }

        private static void FindSubClassesInAssembliesInternal<T>(Assembly[] asms, Type type, IList list,
            bool createInstances = true, IEnumerable<string> exceptionAsmExps = null) where T : class
        {
            foreach (var asm in asms)
            {
                if (skipAsms.Contains(asm.FullName))
                    continue;
                if (exceptionAsmExps != null)
                {
                    bool isException = false;
                    var asmName = asm.GetName().Name;
                    foreach (var exp in exceptionAsmExps)
                    {
                        if (Regex.IsMatch(asmName, exp))
                        {
                            isException = true;
                            break;
                        }
                    }

                    if (isException)
                        continue;
                }

                Type[] types = null;
                try
                {
                    types = asm.GetTypes();
                }
                catch (Exception e)
                {
                    Debug.Log("无法加载程序集内的类： " + asm.FullName);
                    Debug.LogException(e);
                    skipAsms.Add(asm.FullName);
                }

                if (types == null)
                    continue;

                foreach (var t in types)
                {
                    if (t.IsAbstract || t.IsInterface)
                        continue;
                    if (t == type)
                        continue;
                    if (!type.IsAssignableFrom(t))
                        continue;
                    if (createInstances)
                    {
                        T obj;
                        try
                        {
                            if (Application.isPlaying)
                                Debug.Log("Creating Instance of Type: " + t.FullName);
                            obj = asm.CreateInstance(t.FullName) as T;
                        }
                        catch (Exception e)
                        {
                            Debug.LogException(e);
                            obj = null;
                        }

                        if (obj != null)
                            list.Add(obj);
                    }
                    else
                        list.Add(t);
                }
            }
        }
    }
}