using LuaInterface;
using pure.database.structure.tree;
using pure.entity.renderer;
using pure.stateMachine.machine.core;
using pure.stateMachine.machine.runtime;
using pure.utils.debug;

namespace machine.ai {
    [CpxAction(CommandActionType.ADD_BUFF)]
    public class AICommand_Buff : CpxAction {
        public override RpxAction GetRuntime() {
            return RpxPool<Runtime>.Get().SetSource(this);
        }

        private class Runtime : ModelCommandImpl<AICommand_Buff> {
            protected override void OnEnter() {
                _status = CompStatus.COMPLETE;
                LuaTable tb = machine.GetRuntimeData<LuaTable>(CpxState.TEMP_DATA);
                if (tb == null) {
                    GlobalLogger.LogError("no setting data retrieved");
                    return;
                }
                string prefab;
                if (!tb.TryGet("prefab", out prefab)) {
                    GlobalLogger.LogError(string.Format("{0} need prefab", this));
                    return;
                }
                double endTime;
                if (!tb.TryGet("duetime", out endTime)) {
                    GlobalLogger.LogError(string.Format("{0} need duetime", this));
                    return;
                }
                string bone;
                if (!tb.TryGet("bone", out bone)) bone = string.Empty;
                string body;
                if (!tb.TryGet("body", out body)) body = "body";
                BuffRenderer renderer = new BuffRenderer(prefab, body, bone, (long) endTime);
                model.Attach(renderer);
            }

            protected override void OnExit() {
                RpxPool<Runtime>.Release(this);
            }
        }
    }
}