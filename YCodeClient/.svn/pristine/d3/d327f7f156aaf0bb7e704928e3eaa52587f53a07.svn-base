using System.IO;
using UnityEditor;
using UnityEngine;

public class EditorSvnCommand {
    public static bool ignoreUpdateDll;

    [MenuItem("SVN/Commit Res", false, 1001)]
    public static void SvnCommitRes() {
        string path = string.Format("{0}/{1}", Application.dataPath, SvnTools.PathRes);
        if (Directory.Exists(path)) {
            SvnTools.SvnCommit(path);
            EditorUtil.ShowMessage("Res Commit Completes");
        }
    }

    [MenuItem("SVN/Update Res", false, 1000)]
    public static void SvnUpdatePlugins() {
        Debug.Log("forrceUpdateDll: " + ignoreUpdateDll);
        SvnTools.SvnExecute("update", "./");
        if (!ignoreUpdateDll) {
            string path = string.Format("{0}/{1}", Application.dataPath, SvnTools.PathDllScripts);
            if (Directory.Exists(path)) {
                if (!EditorUtility.DisplayDialog("check again", "是否确认删除代码并替换成dll", "ok", "cancel")) {
                    return;
                }
                FileUtil.DeleteFileOrDirectory(path);
            }
            SvnTools.SvnUpdate(SvnTools.RepositoryPlugins, SvnTools.PathPlugins);
        }
        SvnTools.SvnUpdate(SvnTools.RepositoryRes, SvnTools.PathRes);
        EditorUtil.ShowMessage("更新游戏资源完成");
    }

    [MenuItem("SVN/Reset ToroiseProc path", false, 1001)]
    public static void ResetSvnPath() {
        string filePath = EditorUtility.OpenFilePanel("Open ToroiseProc.exe", "C:\\", "exe");
        if (filePath + "" != string.Empty) {
            string filename = filePath;
            filename = filename.Replace(SvnTools.SvnBin, "");
            PlayerPrefs.SetString("_editorSvnBinPath", filename);
        }
    }
}