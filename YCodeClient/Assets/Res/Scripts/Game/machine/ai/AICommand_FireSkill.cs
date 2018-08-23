﻿using System;
using LuaInterface;
using pure.ai.aimachine.runner;
using pure.asset.manager.utils;
using pure.database.structure.tree;
using pure.entity.interfaces;
using pure.stateMachine.machine.core;
using pure.stateMachine.machine.runtime;
using pure.treeComp.firefx.core;
using pure.utils.debug;
using pure.utils.mathTools;
using UnityEngine;

namespace machine.ai {
    [CpxAction(CommandActionType.FIRE_SKILL)]
    public class AICommand_FireSkill : CpxAction {
        public override RpxAction GetRuntime() {
            return RpxPool<Runtime>.Get().SetSource(this);
        }

        private class Runtime : ModelCommandImpl<AICommand_FireSkill> {
            private IFightableEntity _fighter;
            private FireFxRoot _fx;
            private FxFireRunner _fireRunner;

            protected override void OnEnter() {
                _status = CompStatus.COMPLETE;
                object obj = machine.GetRuntimeData<object>(CpxState.TEMP_DATA);
                if (obj == null) {
                    GlobalLogger.LogError("no setting data retrieved");
                    return;
                }
                _fighter = entity as IFightableEntity;
                if (_fighter == null) return;
                _fx = GetFireFx(obj);
                if (_fx == null) return;
                LuaTable tb = obj as LuaTable;
                Vector3 pos;
                if (tb != null && tb.TryGet("position", out pos)) {
                    model.MoveTo(pos);
                }
                float orient;
                if (tb != null && tb.TryGet("orient", out orient)) {
                    Debug.Log(string.Format("model.orient ={0}, data.orient ={1}", model.orientation, orient));
                    model.orientation = orient;
                }
                _status = CompStatus.RUNING;
                _fireRunner = new FxFireRunner(_fighter, _fx) {
                    targetInfos = new FireFxInfo[0],
                    targetPos = GetTargetPosition(tb)
                };
                _fireRunner.completeEvent.AddListener(OnComplete);
                try {
                    _fireRunner.Start();
                } catch (Exception e) {
                    GlobalLogger.LogError(e);
                    _status = CompStatus.COMPLETE;
                }
            }

            private FireFxRoot GetFireFx(object obj) {
                if (obj is FireFxRoot) return obj as FireFxRoot;
                string setting = string.Empty;
                if (obj is string) {
                    setting = obj as string;
                } else if (obj is LuaTable) {
                    setting = (obj as LuaTable).GetValue<string>("fx");
                }
                FireFxRoot fx;
                if (!string.IsNullOrEmpty(setting) && FxFileUtils.TryGet(setting, out fx)) {
                    return fx;
                }
                return null;
            }

            private Vector3 GetTargetPosition(LuaTable tb) {
                Vector3 forcast;
                if (tb != null && tb.TryGet("forcast", out forcast)) {
                    if (forcast != model.position) return forcast;
                }
                Vector3 v = Vector3.forward*10;
                VectorMath.RotateVector3(ref v, model.orientation);
                v += model.position;
                return v;
            }

            protected override void OnUpdate(long now) {
                if (_fireRunner != null) {
                    try {
                        _fireRunner.UpdateNow(now);
                    } catch (Exception e) {
                        GlobalLogger.LogError(e);
                        _status = CompStatus.COMPLETE;
                    }
                }
            }

            private void OnComplete() {
                _status = CompStatus.COMPLETE;
                RemoveRunner();
            }

            private void RemoveRunner() {
                if (_fireRunner != null) {
                    _fireRunner.Dispose();
                    _fireRunner = null;
                }
                if (_fighter != null && _fighter.collideBody != null) {
                    lock (_fighter.collideBody) {
                        _fighter.collideBody.SetDirty();
                    }
                    _fighter = null;
                }
            }

            protected override void OnExit() {
                RemoveRunner();
                RpxPool<Runtime>.Release(this);
            }
        }
    }
}