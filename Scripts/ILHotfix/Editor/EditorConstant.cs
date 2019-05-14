using System;
using System.IO;
using CsHotFix;
using CsHotFix.Editor;
using UnityEditor;
using UnityEngine;

namespace CsHotfix.Scripts.ILHotfix.Editor
{
    public class EditorConstant
    {
        private const string hotfixIndendify = "__cshotfix_indentify__";

        private static string _hotfixRoot;

        private static void InitHotfixRoot()
        {
            if (string.IsNullOrEmpty(_hotfixRoot))
            {
                var guids = AssetDatabase.FindAssets(hotfixIndendify);
                if (guids.Length > 0)
                {
                    _hotfixRoot = Path.GetDirectoryName(AssetDatabase.GUIDToAssetPath(guids[0]));
                }
            }

            if (string.IsNullOrEmpty(_hotfixRoot))
                throw new Exception(hotfixIndendify + " 标识文件不存在,请检查插件是否完整.请将标识文件放在插件根目录");
        }

        public static string CsHotfixOut
        {
            get
            {
                InitHotfixRoot();
                return Path.Combine(_hotfixRoot, "AutoOut/");
            }
        }

        public static string ILRootPath
        {
            get
            {
                InitHotfixRoot();
                return Path.Combine(_hotfixRoot, "ILSource/");
            }
        }
    }
}