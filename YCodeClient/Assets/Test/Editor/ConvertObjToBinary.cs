using pure.refactor.serialize;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ConvertObjToBinary
{
    [MenuItem("ZCC/SerializeDisOrder")]
    public static void GetAllFile()
    {
        UnityEngine.Debug.Log(Application.persistentDataPath);
        string path = Application.dataPath + "/Res/Arts/Entrance";
        var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".obj"));

        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        for (int j = 0; j < files.ToArray().Length; j++)
        {
            SerializeDisOrder(files.ToArray()[j]);
        }
        stopWatch.Stop();
        TimeSpan ts = stopWatch.Elapsed;
        string elapsedTime = String.Format("{0:0000}:{1:0000}:{2:0000}:{3:0000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
        UnityEngine.Debug.Log("RunTime " + elapsedTime);
    }

    public static void SerializeDisOrder(string originPath)
    {
        string currentPath = originPath.Replace(".obj", ".bin");
        if (File.Exists(currentPath)) File.Delete(currentPath);
        FileStream fs = new FileStream(currentPath, FileMode.Create);
        StreamReader read = new StreamReader(originPath);

        ByteArray ba = new ByteArray();
        string line = "";
        uint vlength = 0;
        uint flength = 0;
        while ((line = read.ReadLine()) != null)
        {
            line = line.Trim();
            if (line != "#Pure Terrain File")
            {
                string[] data = line.Split(' ');
                switch (data[0].Trim())
                {
                    case "v":
                        ba.WriteFloat(float.Parse(data[1].Trim()));
                        ba.WriteFloat(float.Parse(data[2].Trim()));
                        ba.WriteFloat(float.Parse(data[3].Trim()));
                        vlength++;
                        break;
                    case "vt":
                        ba.WriteFloat(float.Parse(data[1].Trim()));
                        ba.WriteFloat(float.Parse(data[2].Trim()));
                        break;
                    case "f":
                        ba.WriteShort(short.Parse(data[1].Trim().Split('/')[0]));
                        ba.WriteShort(short.Parse(data[1].Trim().Split('/')[1]));
                        ba.WriteShort(short.Parse(data[2].Trim().Split('/')[0]));
                        ba.WriteShort(short.Parse(data[2].Trim().Split('/')[1]));
                        ba.WriteShort(short.Parse(data[3].Trim().Split('/')[0]));
                        ba.WriteShort(short.Parse(data[3].Trim().Split('/')[1]));
                        flength += 3;
                        break;
                    default: break;
                }
            }
        }

        ba.WriteUnsignInt(vlength);
        ba.WriteUnsignInt(flength);

        byte[] all = ba.ToBuffer();
        fs.Write(all,0,all.Length);
        read.Close();
        fs.Flush();
        fs.Close();
    }

}

