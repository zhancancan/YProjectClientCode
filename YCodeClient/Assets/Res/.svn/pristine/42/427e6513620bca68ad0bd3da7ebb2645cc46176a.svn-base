using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace plugins.terrain {
    public class TerrainShader {
        public static readonly List<TerrainShader> pool = new List<TerrainShader>();

        static TerrainShader() {
            Action<Type> handle = t => {
                string[] ns = Enum.GetNames(t);
                foreach (var n in ns) {
                    int r = (int) Enum.Parse(t, n);
                    pool.Add(new TerrainShader(r, n));
                }
            };
            handle(typeof (TerrainShaderType));
        }


        public static Shader GetShader(int type) {
            var t = pool.FirstOrDefault(o => o.type == type);
            return t != null ? t.shader : null;
        }


        public string name { get; private set; }

        public int type { get; private set; }

        public Shader shader { get; private set; }

        public TerrainShader(int t, string n) {
            type = t;
            name = n;
            string _shadername = "Terrain/" + name;
            shader = Shader.Find(_shadername);
        }
    }
}