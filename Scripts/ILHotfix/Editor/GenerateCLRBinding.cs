﻿#if USE_HOT
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CsHotfix.Scripts.ILHotfix.Editor;
using UnityEditor;

namespace CsHotFix
{
    public static class GenerateCLRBinding
    {
        /// <summary>
        /// ILR 提供的方法,通过快速生成CLR绑定代码,避免通过"反射" 来调用所需要的类型.
        /// 减少GC,提高运行效率
        /// 默认情况下，ILRuntime中调用Unity主工程的方法，ILRuntime会通过反射对目标方法进行调用
        /// </summary>
        [UnityEditor.MenuItem("CsHotFix/CLR绑定")]
        static void Generate()
        {
            HashSet<System.Type> types = new HashSet<System.Type>();
            types.Add(typeof(int));
            types.Add(typeof(float));
            types.Add(typeof(long));
            types.Add(typeof(object));
            types.Add(typeof(string));
            types.Add(typeof(System.Array));
            types.Add(typeof(Vector2));
            types.Add(typeof(Vector3));
            types.Add(typeof(Quaternion));
            types.Add(typeof(GameObject));
            types.Add(typeof(Object));
            types.Add(typeof(Transform));
            types.Add(typeof(RectTransform));
            types.Add(typeof(Texture));
            types.Add(typeof(Texture2D));
            types.Add(typeof(Resources));
            types.Add(typeof(AssetBundle));
            types.Add(typeof(Camera));
            types.Add(typeof(SystemInfo));
            types.Add(typeof(Time));
            types.Add(typeof(CsHotFix.HotMgr));
            types.Add(typeof(CsHotFix.IL.Help));
            types.Add(typeof(CsHotFix.RefType));
            types.Add(typeof(System.Type));
            types.Add(typeof(System.Reflection.MethodInfo));
            types.Add(typeof(System.Reflection.FieldInfo));
            types.Add(typeof(System.Reflection.PropertyInfo));
            //types.Add(typeof(Debug));

            //所有DLL内的类型的真实C#类型都是ILTypeInstance
            types.Add(typeof(List<ILRuntime.Runtime.Intepreter.ILTypeInstance>));
            types.Add(typeof(ILRuntime.Runtime.Intepreter.ILTypeInstance));
            types.Add(typeof(System.Array));

            types.Add(typeof(System.Math));
            types.Add(typeof(System.Convert));
            types.Add(typeof(System.DateTime));
            types.Add(typeof(System.TimeSpan));
            types.Add(typeof(System.Text.StringBuilder));
            types.Add(typeof(Dictionary<ILRuntime.Runtime.Intepreter.ILTypeInstance, ILRuntime.Runtime.Intepreter.ILTypeInstance>));
            types.Add(typeof(Dictionary<int, ILRuntime.Runtime.Intepreter.ILTypeInstance>));
            types.Add(typeof(Dictionary<uint, ILRuntime.Runtime.Intepreter.ILTypeInstance>));
            types.Add(typeof(Dictionary<long, ILRuntime.Runtime.Intepreter.ILTypeInstance>));
            types.Add(typeof(Dictionary<ulong, ILRuntime.Runtime.Intepreter.ILTypeInstance>));
            types.Add(typeof(Dictionary<string, ILRuntime.Runtime.Intepreter.ILTypeInstance>));
            types.Add(typeof(System.Text.RegularExpressions.Regex));
            types.Add(typeof(System.Text.RegularExpressions.Group));
            types.Add(typeof(System.Text.RegularExpressions.Match));
            types.Add(typeof(System.Text.RegularExpressions.CaptureCollection));
            types.Add(typeof(System.Text.RegularExpressions.GroupCollection));
            types.Add(typeof(UnityEngine.UI.LayoutElement));
            types.Add(typeof(UnityEngine.UI.Text));
            types.Add(typeof(UnityEngine.UI.Image));
            types.Add(typeof(UnityEngine.UI.Graphic));
            types.Add(typeof(UnityEngine.EventSystems.UIBehaviour));
            types.Add(typeof(UnityEngine.MonoBehaviour));
            types.Add(typeof(UnityEngine.Color));
            types.Add(typeof(HashSet<long>));
            types.Add(typeof(HashSet<int>));
            types.Add(typeof(HashSet<string>));
            types.Add(typeof(System.Linq.Enumerable));
            types.Add(typeof(Canvas));
            types.Add(typeof(Matrix4x4));
            types.Add(typeof(UnityEngine.Video.VideoPlayer));
            types.Add(typeof(System.Enum));
            types.Add(typeof(Screen));
            types.Add(typeof(CsHotFix.Hotfix));
            types.Add(typeof(CsHotFix.L));

            ILRuntime.Runtime.CLRBinding.BindingCodeGenerator.GenerateBindingCode(types.ToList(), EditorConstant.CsHotfixOut);
            AssetDatabase.Refresh();
        }
    }
}
#endif