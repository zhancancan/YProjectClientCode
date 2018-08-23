﻿using pure.entity.animation;
using pure.entity.interfaces;
using pure.stateMachine.machine.core;
using pure.stateMachine.machine.runtime;
using UnityEngine;

namespace machine.ai {
    [CpxAction(CommandActionType.DEAD)]
    public class AICommand_Dead : CpxAction {
        public override RpxAction GetRuntime() {
            return RpxPool<Runtime>.Get().SetSource(this);
        }

        private class Runtime : RpxAction<AICommand_Dead> {
            protected override void OnEnter() {
                IAnimatable a = entity as IAnimatable;
                if (a != null) {
                    a.SetAnimator(AnimatorType.IS_DEAD, AnimatorControllerParameterType.Bool, 1);
                }
            
            }

            public override bool running { get { return true; } }

            protected override void OnExit() {
                RpxPool<Runtime>.Release(this);
            }
        }
    }
}