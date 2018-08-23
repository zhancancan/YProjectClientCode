using LuaInterface;
using pure.asset.fbx;
using pure.database.structure.tree;
using pure.stateMachine.machine.core;
using pure.stateMachine.machine.runtime;
using pure.utils.debug;
using UnityEngine;

namespace machine.ai {
    [CpxAction(CommandActionType.FLOAT_TEXT)]
    public class AICommand_FloatText : CpxAction {
        public override RpxAction GetRuntime() {
            return RpxPool<Runtime>.Get().SetSource(this);
        }

        private class Runtime : ModelCommandImpl<AICommand_FloatText> {
            protected override void OnEnter() {
                _status = CompStatus.COMPLETE;
                LuaTable tb = machine.GetRuntimeData<LuaTable>(CpxState.TEMP_DATA);
                if (tb == null) {
                    GlobalLogger.LogError("no setting data retrieved");
                    return;
                }
                Vector3 pos;
                if (!tb.TryGet("position", out pos)) {
                    pos = model.position;
                }
                string prefab;
                if (!tb.TryGet("prefab", out prefab)) {
                    GlobalLogger.LogError(string.Format("{0} require prefab", this));
                    return;
                }
                string text;
                if (!tb.TryGet("text", out text)) {
                    GlobalLogger.LogError(string.Format("{0} require text", this));
                    return;
                }
                string body;
                if (!tb.TryGet("body", out body)) body = "body";
                string bone;
                if (!tb.TryGet("head", out bone)) bone = RoleBone.HEAD;
                Transform me = model.FindBone(body, bone);
                Vector3 src = me ? me.position : model.position;
                FlyTextEntity fly = new FlyTextEntity {
                    @from = src,
                    to = (src - pos) + src,
                    prefab = prefab,
                    text = text
                };
                fly.Initialize();
            }

            protected override void OnExit() {
                RpxPool<Runtime>.Release(this);
            }
        }
    }
}