using System.IO;
using CsHotFix;
using UnityEngine;

namespace CsHotFix
{
#if UNITY_EDITOR


    public class EditorResLoad : IResLoad
    {
        private string rootPath
        {
            get { return Application.dataPath + "/../"; }
        }

        private Stream GetStream(string filepath)
        {
            if (!File.Exists(filepath))
            {
                return null;
            }

            try
            {
                return new MemoryStream(File.ReadAllBytes(filepath));
            }
            catch (System.Exception ex)
            {
                L.LogException(ex);
            }

            return null;
        }

        public string DllPath()
        {
            return rootPath + "Data/Hot.dll";
        }

        public Stream GetDllStream()
        {
            return GetStream(DllPath());
        }

        public Stream GetPDBStream()
        {
            return GetStream(rootPath + "Data/Hot.pdb");
        }

        public Stream GetMDBStream()
        {
            return GetStream(rootPath + "Data/Hot.mdb");
        }
    }

#endif
}