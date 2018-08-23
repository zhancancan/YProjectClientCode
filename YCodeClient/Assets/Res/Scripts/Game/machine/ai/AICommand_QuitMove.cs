﻿using LuaInterface;
using pure.database.structure.tree;
using pure.entity.animation;
using pure.refactor.property;
using pure.stateMachine.machine.core;
using pure.stateMachine.machine.runtime;
using pure.ticker;
using pure.utils.debug;
using pure.utils.mathTools;
using UnityEngine;

namespace machine.ai {
    [CpxAction(CommandActionType.QUIT_MOVE)]
    public class AICommand_QuitMove : CpxAction {
        [Editable(0)]
        public float elastic = 0.3f;

        [Editable(1)]
        public float threshold = 0.05f;

        public override RpxAction GetRuntime() {
            return RpxPool<Runtime>.Get().SetSource(this);
        }

        private class Runtime : ModelCommandImpl<AICommand_QuitMove> {
            private Vector3 _position;
            private Vector3 _destiny;
            private long _prevTime;

            protected override void OnEnter() {
                model.SetAnimator(AnimatorType.IS_RUNNING, AnimatorControllerParameterType.Bool, 0);
                LuaTable tb = machine.GetRuntimeData<LuaTable>(CpxState.TEMP_DATA);
                bool isLast = (bool) tb["isLast"];
                if (isLast) {
                    if (!tb.TryGet("forcast", out _destiny)) {
                        GlobalLogger.LogError(string.Format("{0} require forcast", this));
                        _status = CompStatus.COMPLETE;
                        return;
                    }
                    _position = model.position;
                    _status = CompStatus.RUNING;
                } else {
                    _status = CompStatus.COMPLETE;
                }
                _prevTime = SystemTime.GetTime();
            }

            protected override void OnUpdate(long now) {
                CalcElastic(now - _prevTime);
                _prevTime = now;
            }

            private void CalcElastic(long delta) {
                float max = model.speed*delta;
                Vector3 diff = (_destiny - _position);
                Vector3 velocity = diff*cpx.elastic;
                VectorMath.Truncate(ref velocity, max);
                _position += velocity;
                model.MoveTo(_position);
                model.orientation = VectorMath.CalcOrientation(velocity);
                if (velocity.magnitude < 0.1f) {
                    _status = CompStatus.COMPLETE;
                }
            }

            protected override void OnExit() {
                RpxPool<Runtime>.Release(this);
            }
        }
    }
}