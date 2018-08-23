using edit.pure.inspector;
using edit.pure.treespace.core;

namespace plugins.tree.canvas.firefx {
    public abstract class FireFxCellBase : TreeCell {
        [Inspector(InspectorType.Int, -1000, true)]
        public int groupPhase = 0;
    }
}