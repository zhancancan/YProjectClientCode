﻿using pure.ai.aimachine.core;
using pure.entity.renderer;
using pure.treeComp;
using pure.utils.debug;
using pure.utils.fileTools;
using pure.utils.input;
using registerWrap;
using UnityEngine;
#if UNITY_EDITOR
using edit.pure.treespace.core;
using System.Xml.Linq;
using edit.pure.machine.core;
using edit.pure.machine.serializer;
using edit.pure.treespace;
using pure.ai.aimachine.host;
using pure.entity.core;
using pure.entity.interfaces;
using pure.refactor.serialize;
using pure.scene.mono;
using pure.stateMachine.machine.core;
using pure.ticker;
using pure.treeComp.firefx.bullet;
using pure.treeComp.firefx.core;
using pure.ui.hud;
using UnityEditor;

#endif

namespace mono.test {
    public class HostMachineDebuger : MonoBehaviour {
#if UNITY_EDITOR

        private class SpeedRetriever : ISpeedSource {
            public float speed { get; internal set; }
        }

        private class TestHost : TestEntity {
            internal TestHost(GameObject go, float sp) {
                Attach(new FixRenderer(go, go.GetComponentInChildren<Animator>()));
                MoveTo(go.transform.position);
                collideBody = new ModelBody(this, new SpeedRetriever {speed = sp});
            }
        }

        public DefaultAsset machineFile;
        public CamSetting_Dll followCamera;
        public float speed = 5;
        private TestEntity _hostEntity;
        //private CamaraFollowController _camCtrl;

        [SerializeField]
        private Object[] _fxAssets = new Object[0];

        private FireFxRoot[] _fireSettings = new FireFxRoot[0];

        internal void OnEnable() {
            if (!followCamera) {
                Debug.LogError("no camera found");
                return;
            }
            FileTools.Start();
            //LuaManager.Init();
            //new Lua_Starter(() => { Debug.Log("lua init complete"); }).Start();
            GameObject o = GameObject.Find("HudCanvas");
            if (o) HudManager.panel = o.GetComponent<RectTransform>();
            _hostEntity = new TestHost(gameObject, speed);
            _hostEntity.Initialize();
            MachineWrap.Init();
            TreeWrap.Init();
            string stream = FileTools.wwwStreamingAssetsPath;
            if (string.IsNullOrEmpty(stream)) return;
            CpxRetriever r = new CpxRetriever {
                actionRetriever = CpxMachineFactory.GetAction,
                stateRetriever = CpxMachineFactory.GetState,
                conditionRetriever = CpxMachineFactory.GetCondition
            };
            MachineFactory.Start();
            MachineChart m = MachineFileManager.Load(AssetDatabase.GetAssetPath(machineFile));
            CpxController ctrl = new ChartToMachine().Transfer(m, r);
            _hostEntity.runtimeController.Inititalize(ctrl);
            TreeEditorFactory.Start();
            _fireSettings = new FireFxRoot[_fxAssets.Length];
            for (int i = 0, len = _fireSettings.Length; i < len; i++) {
                _fireSettings[i] = GetFireSetting(_fxAssets[i]);
            }
        }

        internal void Update() {
            if (_hostEntity == null) return;
            SystemTime.Update();
            long now = SystemTime.GetTime();
            TouchSystem.UpdateNow(now);
            _hostEntity.UpdateNow(now);
            GenericStaticTickerManager<Bullet>.UpdateNow(now);
            MachineCenter.debugger.Update();
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                Fire(0);
            } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                Fire(1);
            } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                Fire(2);
            } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
                Fire(3);
            }
        }

        internal void LateUpdate() {
            //if (_camCtrl != null) _camCtrl.LateUpdate();
            HudManager.UpdateNow(SystemTime.GetTime());
            GlobalLogger.Release();
        }

        private void Fire(int skillid) {
            if (skillid >= _fireSettings.Length) return;
            _hostEntity.runtimeController.SetRuntimeData(AIMachineParameter.RT_FX_SETTING, _fireSettings[skillid]);
            _hostEntity.runtimeController.SetTrigger(AIMachineParameter.SKILL_TRIGGER, SystemTime.GetTime());
        }

        private static FireFxRoot GetFireSetting(Object t) {
            TreeWrap.Init();
            string path = AssetDatabase.GetAssetPath(t);
            XDocument doc = XDocument.Load(path);
            XElement root = doc.Root;
            string canvasType = StringDataTools.XAttribute(root, "type");
            TreeCanvas canvas = TreeEditorFactory.GetCanvas(canvasType);
            canvas.Read(root);
            FireFxRoot r = new FireFxRoot();
            new TreeToRuntimeComp(canvas, TreeFactory.CreateInstance, r).Transfer();
            return r;
        }

        internal void OnDestroy() {
            MachineCenter.debugger.Reset();
        }
#endif
    }
}