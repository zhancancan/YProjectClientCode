using pure.treeComp;
using registerWrap;
using UnityEngine;
#if UNITY_EDITOR
using edit.pure.treespace.core;
using edit.pure.firefx;
using edit.pure.treespace;
using pure.ticker;
using pure.treeComp.firefx.core;

#endif

namespace mono.fbx {
    public class FireFxTreeDebuger : MonoBehaviour {
#if UNITY_EDITOR
        [SerializeField]
        public GameObject hero;

        [SerializeField]
        public GameObject[] targets = new GameObject[0];

        private EtFireFxContoller _controller;
        private Vector3 _prevPosition;

        public void Start() {
            GameTicker.Start();
            TickerCenter.Init();
        }

        public void Play(TreeCanvas canvas) {
            if (_controller == null) {
                _prevPosition = hero.transform.position;
                FireFxRoot r = new FireFxRoot();
                TreeWrap.Init();
                TreeEditorFactory.Start();
                new TreeToRuntimeComp(canvas, TreeFactory.CreateInstance, r).Transfer();
                _controller = new EtFireFxContoller(hero, r) {targetPos = hero.transform.position};
                FireFxInfo[] infos = new FireFxInfo[targets.Length];
                for (int i = 0; i < targets.Length; i++) {
                    FireFxInfo info = new FireFxInfo(new FireEnemy(targets[i]));
                    if (i == 0) {
                        _controller.mainTargetInfo = info;
                        _controller.targetPos = info.position;
                        _controller.endHostPos = info.position;
                    }
                    infos[i] = info;
                }
                _controller.targetInfos = infos;
                _controller.completeEvent.AddListener(OnComplete);
                GameTicker.tickerManager.Add(_controller);
                _controller.Start();
            } else {
                Debug.Log("the current firefx control is running");
            }
        }

        private void OnComplete() {
            if (_controller != null) {
                GameTicker.tickerManager.Remove(_controller);
                _controller.Dispose();
                _controller = null;
                hero.transform.position = _prevPosition;
            }
            Debug.Log("all complete");
        }
#endif
    }
}