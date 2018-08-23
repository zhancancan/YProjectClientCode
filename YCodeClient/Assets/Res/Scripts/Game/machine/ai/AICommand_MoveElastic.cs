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
    [CpxAction(CommandActionType.ELASIC_MOVE)]
    public class AICommand_MoveElastic : CpxAction {
        [Editable(0)]
        public float elastic = 0.1f;

        [Editable(1)]
        public float maxSpeed = 1.2f;

        [Editable(2)]
        public float duration = 0.6f;

        [Editable(3)]
        public float updateThreshold = 5;

        public override RpxAction GetRuntime() {
            return RpxPool<Runtime>.Get().SetSource(this);
        }

        private class Runtime : ModelCommandImpl<AICommand_MoveElastic> {
            private long _duetime;
            private Vector3 _destiny;
            private Vector3 _position;

            private long _prevUpdate = -1;

            protected override void OnEnter() {
                long now = SystemTime.GetTime();
                long extra = (long) (cpx.duration*1000.0f);
                _duetime = now + extra;
                model.SetAnimator(AnimatorType.IS_RUNNING, AnimatorControllerParameterType.Bool, 1);
                LuaTable tb = machine.GetRuntimeData<LuaTable>(CpxState.TEMP_DATA);
                Vector3 dest;
                if (!tb.TryGet("forcast", out dest)) {
                    GlobalLogger.LogError(string.Format("{0} require forcast", this));
                    _status = CompStatus.COMPLETE;
                    return;
                }
                Vector3 pos;
                if (!tb.TryGet("position", out pos)) {
                    GlobalLogger.LogError(string.Format("{0} require position", this));
                    _status = CompStatus.COMPLETE;
                    return;
                }
                Vector3 dir = dest - pos;
                VectorMath.Truncate(ref dir, model.speed*0.1f);
                _destiny = dir + pos;
                _position = model.position;
                dir = _position - pos;
                if (dir.magnitude > cpx.updateThreshold) {
                    _position = pos;
                    model.MoveTo(_position);
                }
                _status = CompStatus.RUNING;
            }

            protected override void OnUpdate(long now) {
                float delta = (now - _prevUpdate)*0.001f;
                float max = model.speed*cpx.maxSpeed*delta;
                Vector3 diff = (_destiny - _position);
                Vector3 velocity = diff*cpx.elastic;
                VectorMath.Truncate(ref velocity, max);
                _position += velocity;
                model.MoveTo(_position);
                model.orientation = VectorMath.CalcOrientation(velocity);
                _prevUpdate = now;
            }

            protected override void OnExit() {
                RpxPool<Runtime>.Release(this);
            }

            public override bool running { get { return SystemTime.GetTime() <= _duetime; } }
        }
    }
}