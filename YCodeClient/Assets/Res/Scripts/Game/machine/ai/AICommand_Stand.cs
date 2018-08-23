using pure.entity.animation;
using pure.entity.interfaces;
using pure.refactor.property;
using pure.stateMachine.machine.core;
using pure.stateMachine.machine.runtime;
using UnityEngine;

namespace machine.ai {
    [CpxAction(CommandActionType.STAND)]
    public class AICommand_Stand : CpxAction {
        [Editable(1)]
        public float updateInterval;

        [Editable(2)]
        public float standModePercent;

        public override RpxAction GetRuntime() {
            return RpxPool<Runtime>.Get().SetSource(this);
        }

        private class Runtime : RpxAction<AICommand_Stand> {
            protected override void OnEnter() {
                IAnimatable a = entity as IAnimatable;
                if (a != null) {
                    a.SetAnimator(AnimatorType.IS_RUNNING, AnimatorControllerParameterType.Bool, 0);
                }
            }

            public override bool running { get { return true; } }
        }
    }
}