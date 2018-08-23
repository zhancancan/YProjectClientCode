using pure.database.structure.tree;
using pure.entity.core;
using pure.stateMachine.machine.core;
using pure.stateMachine.machine.runtime;

namespace machine.ai {
    public abstract class ModelCommandImpl<T> : RpxAction<T> where T : CpxAction {
        protected CompStatus _status = CompStatus.IDLE;

        public override bool running { get { return _status == CompStatus.RUNING; } }

        protected ModelEntity model { get { return entity as ModelEntity; } }
    }
}