using System.Collections;
using game.entity.game;
using pure.asset.manager;
using pure.asset.manager.utils;
using pure.cpp;
using pure.net.socket;
using pure.scene.pathFinder.cpp;
using pure.ticker;
using pure.utils.debug;
using pure.utils.fileTools;
using pure.utils.memory;
using pure.utils.native;
using registerWrap;
using UnityEngine;
#if UNITY_EDITOR
using edit.pure.pack;
using mono.fbx;

#endif

namespace main {
    public class GameStarter : MonoBehaviour {
        public string loginMachine;

        public string configHttp = string.Empty;

        private GameEntity _entity;

        protected void Start() {
#if UNITY_EDITOR
            AssetManager.readMode = PackCenter.settingData
                ? PackCenter.settingData.readMode
                : AssetManager.ReadMode.AssetDataBase;
            AssetUtils.fireFxManager.retriver = PortraitAct_Skill.GetFireSetting;
            FxFileUtils.GetFireSetting = PortraitAct_Skill.GetFireSetting;
#else
            AssetManager.readMode = AssetManager.ReadMode.AssetBundle; 
#endif
            StreamManagerCenter.Reset();
            DontDestroyOnLoad(gameObject);
            PresetSystem();
            StartCoroutine(DoStart());
        }

        private void PresetSystem() {
            AssetUtils.isPlayingMode = true;
            NativeManager.debugConfigHttp = configHttp;
            FileTools.Start();
            TickerCenter.Init();
            GameTicker.Start();
            NavCpp.Start();
            CppCenter.Start();
            GlobalLogger.writeLogFile = false;
        }

        private IEnumerator DoStart() {
            NativeRequest_Start s = new NativeRequest_Start();
            NativeManager.Send(s);
            while (!s.isComplete) yield return null;
            IEnumerator e = InitBind();
            while (e.MoveNext()) yield return null;
            StopAllCoroutines();
            _entity = new GameEntity {loginMachine = loginMachine};
            _entity.Initialize();
        }

        private IEnumerator InitBind() {
            IStartRunner[] runners = {
                new Start_Async {action = MachineWrap.Init},
                new Start_Async {action = TreeWrap.Init},
                new Start_Async {action = MediatorWrap.Init},
                new Start_Async {action = PropertyWrap.Init},
                new Start_Async {action = MonoWrap.Init},
                new Starter_Lua()
            };
            for (int i = 0, len = runners.Length; i < len; i++) runners[i].Start();
            while (true) {
                bool allComplete = true;
                for (int i = 0, len = runners.Length; i < len; i++) {
                    if (!runners[i].complete) allComplete = false;
                }
                if (allComplete) break;
                yield return null;
            }
            Debug.Log("runner complete");
        }

#if UNITY_EDITOR
        protected void OnApplicationQuit() {
            LuaServer.Close();
            GameObjectPool.FlushPool(GameObjectPool.LifeMode.LongLife);
            GameObjectPool.FlushPool(GameObjectPool.LifeMode.SceneLife);
            Debug.Log("on application quit");
            AssetUtils.isPlayingMode = false;
        }

        protected void OnDestroy() {
            AssetManager.readMode = AssetManager.ReadMode.AssetDataBase;
        }
#endif
    }
}