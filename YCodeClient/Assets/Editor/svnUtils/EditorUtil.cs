using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using Debug = UnityEngine.Debug;

public class EditorUtil {
    public class Platform {
        public static string GetPlatformFolder(BuildTarget target) {
            switch (target) {
                case BuildTarget.Android:
                    return "android";
                case BuildTarget.iOS:
                    return "ios";
                case BuildTarget.WebGL:
                    return "webgl";
                case BuildTarget.StandaloneOSXIntel:
                case BuildTarget.StandaloneOSXIntel64:
                case BuildTarget.StandaloneOSXUniversal:
                    return "osx";
                default:
                    return "windows";
            }
        }
    }

    /// <summary>
    ///     显示进度条
    /// </summary>
    /// <param name="message"></param>
    /// <param name="progress"></param>
    public static void ShowProgress(string message, float progress) {
        EditorUtility.DisplayProgressBar("Hold On", message, progress);
    }

    /// <summary>
    ///     显示提示消息
    /// </summary>
    /// <param name="message"></param>
    /// <param name="title"></param>
    public static void ShowMessage(string message, string title = null, string ok = null) {
        //EditorUtility.DisplayDialog(title ?? "操作完成", message, ok ?? "确定");
        EditorUtility.ClearProgressBar();
    }

    /// <summary>
    ///     执行系统命令
    /// </summary>
    /// <param name="command"></param>
    /// <param name="argument"></param>
    public static void ProcessCommand(string command, string argument) {
        ProcessStartInfo start = new ProcessStartInfo(command) {
            Arguments = argument,
            CreateNoWindow = false,
            ErrorDialog = true,
            UseShellExecute = true
        };

        if (start.UseShellExecute) {
            start.RedirectStandardOutput = false;
            start.RedirectStandardError = false;
            start.RedirectStandardInput = false;
        } else {
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            start.RedirectStandardInput = true;
            start.StandardOutputEncoding = Encoding.UTF8;
            start.StandardErrorEncoding = Encoding.UTF8;
        }

        Process p = Process.Start(start);
        if (p == null) return;

        if (!start.UseShellExecute) {
            Debug.Log(p.StandardOutput);
            Debug.Log(p.StandardError);
        }

        p.WaitForExit();
        p.Close();
    }

    /// <summary>
    ///     拷贝目录
    /// </summary>
    /// <param name="fromDir"></param>
    /// <param name="toDir"></param>
    public static void CopyDir(string fromDir, string toDir) {
        if (!Directory.Exists(fromDir))
            return;

        if (!Directory.Exists(toDir)) {
            Directory.CreateDirectory(toDir);
        }

        string[] files = Directory.GetFiles(fromDir);
        int i = 1, len = files.Length;
        foreach (string formFileName in files) {
            string fileName = Path.GetFileName(formFileName);
            if (fileName == null) continue;
            string toFileName = Path.Combine(toDir, fileName);
            CopyFile(formFileName, toFileName);

            ShowProgress("Copying \"" + formFileName + "\" To \"" + toFileName + "\"",
                (float) (i + 1)/len);
        }
        string[] fromDirs = Directory.GetDirectories(fromDir);
        foreach (string fromDirName in fromDirs) {
            string dirName = Path.GetFileName(fromDirName);
            if (dirName == null) continue;
            string toDirName = Path.Combine(toDir, dirName);
            CopyDir(fromDirName, toDirName);
        }
    }

    public static void CopyFile(string formFileName, string toFileName) {
        try {
            File.Copy(formFileName, toFileName, true);
        } catch (Exception ex) {
            Debug.LogError(ex.Message);
        }
    }

    /// <summary>
    ///     移动目录
    /// </summary>
    /// <param name="fromDir"></param>
    /// <param name="toDir"></param>
    public static void MoveDir(string fromDir, string toDir) {
        if (!Directory.Exists(fromDir))
            return;

        CopyDir(fromDir, toDir);
        Directory.Delete(fromDir, true);
    }

    public static string GetFileMd5(string fileName) {
        try {
            FileStream file = new FileStream(fileName, FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0, len = retVal.Length; i < len; i++) {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        } catch (Exception ex) {
            throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
        }
    }
}