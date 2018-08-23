using System.IO;
using System.Text;
using UnityEditor;

namespace plugins.wrapper.mediator {
    internal class MediatorWrapperCleaner {
        private const string CLEAN_CODE = @"//this source code was auto-generated, do not modify it
namespace registerWrap{ 
    public class MediatorWrap{
        public static void Init(){
        }
    }
}";

        internal void Start() {
            string dir = Path.GetDirectoryName(MediatorWrapperBuilder.FILE);
            if (string.IsNullOrEmpty(dir)) return;
            if (!Directory.Exists(dir)) {
                Directory.CreateDirectory(dir);
            }
            using (StreamWriter w = new StreamWriter(MediatorWrapperBuilder.FILE, false, Encoding.UTF8)) {
                w.Write(CLEAN_CODE);
                w.Flush();
                w.Close();
            }
            AssetDatabase.Refresh();
        }
    }
}