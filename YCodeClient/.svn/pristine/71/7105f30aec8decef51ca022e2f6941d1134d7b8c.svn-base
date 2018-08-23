using System.IO;
using UnityEditor;
using UnityEngine;

public class SvnTools {
    public static string SvnBin = "TortoiseProc.exe";

    public static string RepositoryPlugins = "http://devsvn3.oa.9wee.com/svn/y_code_client/Plugins";
    public static string RepositoryRes = "http://devsvn3.oa.9wee.com/svn/y_art/Res";
    public static string RepositoryDllScripts = "http://devsvn3.oa.9wee.com/svn/y_code_client/DLLScripts"; 

    public static string PathPlugins = "Plugins";
    public static string PathRes = "Res";
    public static string PathDllScripts = "DLLScripts";

    public static void SvnUpdate(string repository, string path) {
        string svnPath = string.Format("{0}/{1}/.svn", Application.dataPath, path);
        if (Directory.Exists(svnPath)) {
            SvnExecute("update", "Assets/" + path);
        } else {
            SvnExecute("checkout", "Assets/" + path, string.Format("/url:{0}", repository));
        }
    }

    public static void SvnCommit(string path) {
        SvnExecute("commit", path);
    }

    public static void SvnExecute(string cmd, string path, string exParams = "") {
        string procDir = PlayerPrefs.GetString("_editorSvnBinPath", string.Empty);
        if (procDir == string.Empty)
            procDir = "C:/Program Files/TortoiseSVN/bin";
        string svnBinPath = string.Format("{0}/{1}", procDir, SvnBin); 
        if (!File.Exists(svnBinPath)) {
            string filePath = EditorUtility.OpenFilePanel("Open ToroiseProc.exe", "C:\\", "exe");
            if (filePath + "" != string.Empty) {
                string filename = filePath;
                svnBinPath = filename;
                filename = filename.Replace(SvnBin, "");
                PlayerPrefs.SetString("_editorSvnBinPath", filename);
            }
        } 






        if (File.Exists(svnBinPath)) {
            //EditorUtil.ProcessCommand SvnBin,
            EditorUtil.ProcessCommand(svnBinPath,
                string.Format("/command:{0} /path:{1} {2} /closeonend:2", cmd, Path.GetFullPath(path), exParams));
        } else {
            EditorUtility.DisplayDialog("Waring", "no ToroiseProc.exe Found", "OK");
        }
    }
}