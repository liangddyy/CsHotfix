using System;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using CsHotfix.Scripts.ILHotfix.Editor;
using CsHotFix;

namespace PackTool
{
    public class OpenHotMacro : TemplatesMarco
    {
        const string marco = "USE_HOT";
        const string path = "";

        const string MenuItemOpen = "CsHotFix/插件/开启";
        const string MenuItemClose = "CsHotFix/插件/取消";

        [MenuItem(MenuItemClose, true)]
        public static bool BufCannelToggle()
        {
            MacroDefine macroDefine = new MacroDefine();
            return macroDefine.has(marco);
        }

        [MenuItem(MenuItemOpen, true)]
        public static bool BufOpenToggle()
        {
            MacroDefine macroDefine = new MacroDefine();
            return !macroDefine.has(marco);
        }

        [MenuItem(MenuItemClose, false, 1)]
        public static void BufCannel()
        {
            SetEnable(false, marco, path);
            OpenHotMDBMacro.BufCannel();
            OpenHotPDBMacro.BufCannel();
        }

        [MenuItem(MenuItemOpen, false, 0)]
        public static void BufOpen()
        {
            var ilApp = ReflectionUtil.FindType("ILRuntime.Runtime.Enviorment.AppDomain");
            if (ilApp == null)
            {
                Debug.LogError("没有ILRuntime插件");
                return;
            }

            var file = Path.Combine(Application.dataPath, "csc.rsp");
            if (!File.Exists(file))
            {
                File.WriteAllText(file,"-unsafe");
            }
            
            SetEnable(true, marco, path);
        }
    }

    public class OpenHotPDBMacro : TemplatesMarco
    {
        const string marco = "USE_PDB";
        const string path = "";

        const string MenuItemOpen = "CsHotFix/插件/调试/PDB开启";
        const string MenuItemClose = "CsHotFix/插件/调试/PDB取消";

        [MenuItem(MenuItemClose, true)]
        public static bool BufCannelToggle()
        {
            MacroDefine macroDefine = new MacroDefine();
            return macroDefine.has(marco);
        }

        [MenuItem(MenuItemOpen, true)]
        public static bool BufOpenToggle()
        {
            MacroDefine macroDefine = new MacroDefine();
            return !macroDefine.has(marco);
        }

        [MenuItem(MenuItemClose)]
        public static void BufCannel()
        {
            SetEnable(false, marco, path);
        }

        [MenuItem(MenuItemOpen)]
        public static void BufOpen()
        {
            OpenHotMacro.BufOpen();
            OpenHotMDBMacro.BufCannel();
            SetEnable(true, marco, path);
        }
    }

    public class OpenHotMDBMacro : TemplatesMarco
    {
        const string marco = "USE_MDB";
        const string path = "";

        const string MenuItemOpen = "CsHotFix/插件/调试/MDB开启";
        const string MenuItemClose = "CsHotFix/插件/调试/MDB取消";

        [MenuItem(MenuItemClose, true)]
        public static bool BufCannelToggle()
        {
            MacroDefine macroDefine = new MacroDefine();
            return macroDefine.has(marco);
        }

        [MenuItem(MenuItemOpen, true)]
        public static bool BufOpenToggle()
        {
            MacroDefine macroDefine = new MacroDefine();
            return !macroDefine.has(marco);
        }

        [MenuItem(MenuItemClose)]
        public static void BufCannel()
        {
            SetEnable(false, marco, path);
        }

        [MenuItem(MenuItemOpen)]
        public static void BufOpen()
        {
            OpenHotMacro.BufOpen();
            OpenHotPDBMacro.BufCannel();
            SetEnable(true, marco, path);
        }
    }

    public class ILRuntimeCleaner
    {
        const string MenuItemClean = "CsHotFix/插件/清理ILRuntime多余文件";

        [MenuItem(MenuItemClean,false,2)]
        public static void CleanILRuntimeFile()
        {
            var root = EditorConstant.ILRootPath;
            
            var dirs = new List<string>();
            dirs.Add("ILRuntime/Properties/");
            
            var files = new List<string>();
            files.Add("AssemblyInfo.cs");
            files.Add("Consts.cs");
            files.Add("cecil.snk");
            files.Add("Mono.Cecil.csproj");
            files.Add("Mono.Cecil.props");
            files.Add("Mono.Cecil.sln");
            files.Add("Mono.Cecil.Tests.props");
            files.Add("NetStandard.props");
            files.Add("Mono.Cecil.Mdb.csproj");
            files.Add("Mono.Cecil.Pdb.csproj");
            files.Add("ILRuntime.csproj");
            files.Add("ProjectInfo.cs");

            dirs.ForEach(x=>DeleteDir(Path.Combine(root,x)));
            
            var tmp = Directory.GetFiles(EditorConstant.ILRootPath, "*.*", SearchOption.AllDirectories);
            for (var i = tmp.Length - 1; i >= 0; i--)
            {
                if (files.Contains(Path.GetFileName(tmp[i])))
                {
                    DeleteFile(tmp[i]);
                }
            }
            
            AssetDatabase.Refresh();
        }

        private static void DeleteDir(string path)
        {
            if (Directory.Exists(path))
            {
                Debug.Log("删除文件夹:" + path);
                Directory.Delete(path, true);
            }
        }
        
        private static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                Debug.Log("删除文件:" + path);
                File.Delete(path);
            }
        }
    }
}