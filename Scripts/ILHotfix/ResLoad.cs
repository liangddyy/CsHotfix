using UnityEngine;
using System.IO;
namespace CsHotFix
{


    public interface IResLoad
    {
        string DllPath();

        Stream GetDllStream();

        Stream GetPDBStream();
        Stream GetMDBStream();
    }

#if UNITY_EDITOR
   
#endif
}