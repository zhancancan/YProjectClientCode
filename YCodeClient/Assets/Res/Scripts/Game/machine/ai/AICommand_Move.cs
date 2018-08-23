using LuaInterface;
using pure.ai.aimachine.runner;
using pure.database.structure.tree;
using pure.entity.animation;
using pure.entity.interfaces;
using pure.refactor.property;
using pure.stateMachine.machine.core;
using pure.stateMachine.machine.runtime;
using pure.ticker;
using pure.utils.debug;
using UnityEngine;

namespace machine.ai {
    [CpxAction(CommandActionType.MOVE)]
    public class AICommand_Move : CpxAction {
        [Editable(0)]
        public float threshold;

        [Editable(1)]
        public float duration = 0.6f;

        [Editable(2)]
        public float forcastLen = 0.1f;

        [Editable(3)]
        public float maxSpeedMulitple = 1.1f;

        public override RpxAction GetRuntime() {
            return RpxPool<Runtime>.Get().SetSource(this);
        }

        private class Runtime : ModelCommandImpl<AICommand_Move>, ISpeedSource {
            private Vector3[] _path = new Vector3[3];
            private MoveRunner_Path3D _runner;

            protected override void OnEnter() {
                long now = SystemTime.GetTime();
                model.SetAnimator(AnimatorType.IS_RUNNING, AnimatorControllerParameterType.Bool, 1);
                LuaTable tb = machine.GetRuntimeData<LuaTable>(CpxState.TEMP_DATA);
                Vector3 np;
                if (!tb.TryGet("position", out np)) {
                    GlobalLogger.LogError(string.Format("{0} require position", this));
                    _status = CompStatus.COMPLETE;
                    return;
                }
                Vector3 dest;
                if (!tb.TryGet("forcast", out dest)) {
                    GlobalLogger.LogError(string.Format("{0} require forcast", this));
                    _status = CompStatus.COMPLETE;
                    return;
                }
                Vector3 mp = model.position;
                Vector3 gap = np - mp;
                float offset = gap.magnitude;
                if (offset > cpx.threshold) {
                    model.MoveTo(np);
                    mp = np;
                }
                _path[0] = mp;
                _path[1] = np;
                _path[2] = dest;
                _runner = MoveRunner_Path3D.Get();
                _runner.Set(model, this);
                _runner.Preset(_path, now, 0);
                _runner.updateAnim = false;
                _runner.Enter();
                _status = CompStatus.RUNING;
            }

            protected override void OnUpdate(long now) {
                if (_runner != null) {
                    _runner.UpdateNow(now);
                    if (_runner.finished) {
                        ClearRunner();
                        _status = CompStatus.COMPLETE;
                    }
                }
            }

            private void ClearRunner() {
                if (_runner != null) {
                    _runner.Exit();
                    _runner = null;
                }
            }

            protected override void OnExit() {
                ClearRunner();
                RpxPool<Runtime>.Release(this);
            }

            public float speed { get { return model.speed; } }
        }
    }
}