﻿using System.Collections;
using pure.asset.manager;
using pure.asset.manager.utils;
using pure.database.interfaces;
using pure.mvc.view;
using pure.scene.manager;
using pure.stateMachine.interfaces;
using pure.ticker;
using pure.ui.hud;
using pure.utils.debug;
using pure.utils.mathTools;
using pure.utils.memory;
using UnityEngine;

namespace machine.ai {
    public class FlyTextEntity : IEntity {
        private IEnumerator _enumerator;

        private GameObject _model;

        string IEntity.name { get { return string.Empty; } set { } }

        string IData.id { get { return string.Empty; } set { } }

        public bool isDisposed { get { return false; } }

        public Vector3 from { private get; set; }
        public Vector3 to { private get; set; }

        public string prefab { private get; set; }
        public string text { private get; set; }

        private long _initTime = -1;

        public void Initialize() {
            _initTime = -1;
            GameTicker.entityTicker.Add(this);
        }

        public void UpdateNow(long now = -1) {
            if (_initTime == -1) _initTime = now;
            if (!_model && _enumerator == null) {
                Camera cam = SceneCenter.mainCamera ?? Camera.current ?? Camera.main;
                bool inc = VectorMath.InCamera(cam, from);
                if (inc) {
                    _enumerator = Create();
                }
            }
            if (_enumerator != null && !_enumerator.MoveNext()) {
                _enumerator = null;
                Dispose();
            } else if (now - _initTime > 2000) {
                Dispose();
            }
        }

        private IEnumerator Create() {
            string file;
            if (!AssetUtils.ParseFile(prefab, out file)) {
                yield break;
            }
            IEnumerator e = AssetUtils.Load(AssetType.Prefab, file);
            while (e.MoveNext()) yield return null;
            e = AssetUtils.PickAsyn<GameObject>(AssetType.Prefab, file);
            while (e.MoveNext()) yield return null;
            GameObject p = AssetUtils.Pick<GameObject>(AssetType.Prefab, file);
            if (!p) {
                GlobalLogger.LogError(string.Format("prefab read error at {0}", file));
                yield break;
            }
            _model = GameObjectPool.Get(p, GameObjectPool.LifeMode.SceneLife);
            Transform t = _model.transform;
            t.SetParent(CanvasManager.sceneCanvas);
            t.position = from;
            _model.SetActive(true);
            Transform child = t.GetChild(0);
            float orient = VectorMath.CalcOrientation(to - from);
            t.eulerAngles = new Vector3(0, orient, 0);
            Text3D_Dll t3 = child.GetComponent<Text3D_Dll>();
            if (t3) {
                t3.text = text;
                e = t3.DoAnimation();
                while (e.MoveNext()) yield return null;
            }
        }

        public void Dispose() {
            if (_model != null) {
                GameObjectPool.ReturnInstance(_model);
                _model = null;
            }
            GameTicker.entityTicker.Remove(this);
            _enumerator = null;
            _initTime = -1;
        }
    }
}