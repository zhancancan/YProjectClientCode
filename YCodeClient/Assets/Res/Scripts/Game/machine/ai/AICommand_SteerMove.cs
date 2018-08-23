﻿using LuaInterface;
using pure.database.structure.tree;
using pure.entity.animation;
using pure.entity.interfaces;
using pure.refactor.property;
using pure.stateMachine.machine.core;
using pure.stateMachine.machine.runtime;
using pure.ticker;
using pure.utils.debug;
using pure.utils.mathTools;
using UnityEngine;

namespace machine.ai {
    [CpxAction(CommandActionType.STEER_MOVE)]
    public class AICommand_SteerMove : CpxAction {
        [Editable(0)]
        public float mass = 1;

        [Editable(1)]
        public float maxSpeed = 1.2f;

        [Editable(2)]
        public float maxForce = 2;

        [Editable(3)]
        public float arriveThreshold = 0.1f;

        [Editable(4)]
        public float duration = 0.6f;

        [Editable(5)]
        public float lastElasity = 0.1f;

        public override RpxAction GetRuntime() {
            return RpxPool<Runtime>.Get().SetSource(this);
        }

        private class Runtime : ModelCommandImpl<AICommand_SteerMove> {
            private long _duetime;
            private Steer _steer;

            private long _prevTime;

            private Vector3 _destiny;
            private Vector3 _start;
            private Vector3 _calcPos;
            private Vector3 _position;

            private bool _isLast;
            private bool _arrive;

            protected override void OnEnter() {
                _arrive = false;
                long now = SystemTime.GetTime();
                long extra = (long) (cpx.duration*1000);
                _duetime = now + extra;
                model.SetAnimator(AnimatorType.IS_RUNNING, AnimatorControllerParameterType.Bool, 1);
                _steer = model.GetAttach<Steer>() ?? model.Attach(new Steer());
                _start = model.position;
                _calcPos = _start;
                _position = _start;
                LuaTable tb = machine.GetRuntimeData<LuaTable>(CpxState.TEMP_DATA);
                Vector3 dest, pos;
                if (!tb.TryGet("position", out pos)) {
                    GlobalLogger.LogError(string.Format("{0} require position", this));
                    _status = CompStatus.COMPLETE;
                    return;
                }
                if (!tb.TryGet("forcast", out dest)) {
                    GlobalLogger.LogError(string.Format("{0} require forcast", this));
                    _status = CompStatus.COMPLETE;
                    return;
                }
                _isLast = (bool) tb["isLast"];
                if (now - _steer.prevTime > extra) {
                    _steer.prevTime = now;
                }
                _prevTime = _steer.prevTime;
                if (_isLast) {
                    _destiny = pos;
                } else {
                    Vector3 dir = dest - pos;
                    VectorMath.Truncate(ref dir, model.speed*0.1f);
                    _destiny = dir + pos;
                }
                _status = CompStatus.RUNING;
            }

            protected override void OnUpdate(long now) {
                if (now > _duetime) {
                    _status = CompStatus.COMPLETE;
                } else if (_isLast) {
                    CalcElastic(now - _prevTime);
                } else {
                    CalcSteering(now - _prevTime);
                    _steer.prevTime = now;
                }
                _prevTime = now;
            }

            private void CalcSteering(long delta) {
                if (!_arrive) {
                    float passtime = delta*0.001f;
                    float maxSpeed = model.speed*cpx.maxSpeed;
                    Vector3 desireVelocity = (_destiny - _calcPos).normalized*maxSpeed - _steer.velocity;
                    _steer.force += desireVelocity;
                    VectorMath.Truncate(ref _steer.force, cpx.maxForce);
                    _steer.force /= cpx.mass;
                    _steer.velocity += _steer.force*passtime;
                    VectorMath.Truncate(ref _steer.velocity, maxSpeed);
                    Vector3 v = _steer.velocity*passtime;
                    _calcPos += v;
                    _position = _calcPos;
                    float lerp = (_calcPos - _start).magnitude/(_destiny - _start).magnitude;
                    lerp = lerp > 1 ? 1 : lerp;
                    _position.y = _start.y + (_destiny.y - _start.y)*lerp;
                    model.MoveTo(_position);
                    model.orientation = VectorMath.CalcOrientation(_steer.velocity);
                    if ((_calcPos - _destiny).magnitude < cpx.arriveThreshold) {
                        _arrive = true;
                    }
                } else if (_isLast) {
                    _status = CompStatus.COMPLETE;
                }
            }

            private void CalcElastic(long delta) {
                float max = model.speed*delta;
                Vector3 diff = (_destiny - _position);
                Vector3 velocity = diff*cpx.lastElasity;
                VectorMath.Truncate(ref velocity, max);
                _position += velocity;
                model.MoveTo(_position);
                model.orientation = VectorMath.CalcOrientation(velocity);
                if (velocity.magnitude < 0.2f) {
                    _status = CompStatus.COMPLETE;
                }
            }

            protected override void OnExit() {
                if (_isLast && _steer != null) _steer.Reset();
                _steer = null;
                RpxPool<Runtime>.Release(this);
            }
        }

        private class Steer : IAuxiliary {
            public Vector3 velocity;
            public Vector3 force;
            public long prevTime = -1;

            public void Reset() {
                velocity = Vector3.zero;
                force = Vector3.zero;
                prevTime = -1;
            }

            void IAuxiliary.SetEntity(IModelEntity entity) {
            }

            void IAuxiliary.Enter() {
            }

            void IAuxiliary.UpdateNow(long now) {
            }

            void IAuxiliary.Exit() {
            }
        }
    }
}