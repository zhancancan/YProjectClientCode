using System.IO;
using System.Text;
using UnityEditor;

namespace plugins.wrapper.machine {
    internal class MachineWrapperCleaner {
        private const string CLEAN_CODE = @"//this source code was auto-generated, do not modify it
namespace registerWrap{ 
    public class MachineWrap{
        public static void Init(){
        }
    }
}";

        internal void Start() {
            string dir = Path.GetDirectoryName(MachineWrapperBuilder.FILE);
            if (string.IsNullOrEmpty(dir)) return;
            if (!Directory.Exists(dir)) {
                Directory.CreateDirectory(dir);
            }
            using (StreamWriter w = new StreamWriter(MachineWrapperBuilder.FILE, false, Encoding.UTF8)) {
                w.Write(CLEAN_CODE);
                w.Flush();
                w.Close();
            }
            AssetDatabase.Refresh();
        }
    }
}