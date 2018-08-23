using System;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Linq;
using pure.refactor.serialize;

public class DeSerializeObjToMesh : MonoBehaviour
{
    public int count = 100;
    void Start()
    {
        DeSerializeAll();
        for (int i = 0; i < count; i++)
        {
            pathArr[i] = pathTemp;
        }

        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        for (int i = 0; i < pathArr.Length; i++)
        {
            DeSerializeByDisOrder(pathArr[i]);
        }
        stopWatch.Stop();
        TimeSpan ts = stopWatch.Elapsed;
        string elapsedTime = String.Format("{0:0000}:{1:0000}:{2:0000}:{3:0000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
        UnityEngine.Debug.Log("RunTime " + elapsedTime);
    }

    string pathTemp = "";
    string[] pathArr;
    public void DeSerializeAll()
    {
        pathArr = new string[count];
        string path = Application.dataPath + "/Res/Arts/Entrance";
        var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".bin"));
        for (int i = 0; i < files.ToArray().Length; i++)
        {
            pathTemp = files.ToArray()[i];
            //DeSerializeByDisOrder(files[j]);
        }
    }

    Regex r = new Regex(@"\\.+?\\(.+)\.bin");
    Vector3 pos = Vector3.zero;
    Vector2 uv = Vector2.zero;
    public void DeSerializeByDisOrder(string filepath)
    {
        FileStream fs = new FileStream(@filepath, FileMode.Open);
        byte[] byteAll = new byte[fs.Length];
        fs.Read(byteAll, 0, (int)fs.Length);

        ByteReader lba = new ByteReader(byteAll, byteAll.Length-8, 8);
        int vLength = (int)lba.ReadUnsignInt();
        int fLength = (int)lba.ReadUnsignInt();

        Vector3[] vArr = new Vector3[vLength];
        Vector2[] vtArr = new Vector2[vLength];
        int[] fArr = new int[fLength];

        ByteReader vba = new ByteReader(byteAll,0,vLength*12);
        ByteReader uvba = new ByteReader(byteAll, vLength * 12,vLength*12+vLength*8);
        ByteReader fba = new ByteReader(byteAll, vLength * 12 + vLength * 8, vLength * 12 + vLength * 8+fLength*4);

        for (int i = 0; i < vLength; i++)
        {
            pos.x = vba.ReadFloat();
            pos.y = vba.ReadFloat();
            pos.z = vba.ReadFloat();
            vArr[i] = pos;
            uv.x = uvba.ReadFloat();
            uv.y = uvba.ReadFloat();
            vtArr[i] = uv;
        }

        for (int i = 0; i < fLength; i++)
        {
            int v = fba.ReadShort();
            int uv = fba.ReadShort();
            fArr[i] = v - 1;
        }

        fs.Close();

        string name = r.Match(filepath).Groups[1].Value;
        GameObject obj = new GameObject(name);
        MeshFilter mf = obj.AddComponent<MeshFilter>();
        MeshRenderer mr = obj.AddComponent<MeshRenderer>();

        mf.mesh.vertices = vArr;
        mf.mesh.uv = vtArr;
        mf.mesh.triangles = fArr;
    }

}